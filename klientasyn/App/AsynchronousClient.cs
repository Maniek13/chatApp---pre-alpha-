using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Klient.App.Objects;
using System.Configuration;

namespace Klient.App
{
    public class AsynchronousClient : KlientLogowanie
    {
        private const int port = 11000;

        private static readonly ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static readonly ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static readonly ManualResetEvent receiveDone =
            new ManualResetEvent(false);

        private string response = "";
        private readonly bool? _isContacts;
        private readonly string _FileName = "";

        public AsynchronousClient(bool isContacts)
        {
            _isContacts = isContacts;
        }

        public AsynchronousClient()
        {
            _isContacts = null;
        }

        public AsynchronousClient(string FileName)
        {
            _isContacts = null;
            _FileName = FileName;
        }

        public void StartClient()
        {

            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry(ConfigurationSettings.AppSettings["IpAddress"]);
                IPAddress ipAddress = ipHostInfo.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, port);

                Socket client = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);
                connectDone.Reset();
                client.BeginConnect(remoteEP,
                    new AsyncCallback(ConnectCallback), client);

                if (connectDone.WaitOne(500) == true)
                {
                    sendDone.Reset();

                    if (_isContacts == true)
                    {
                        Send(client, Responde.contactsMsg);
                    }
                    else if(_isContacts == false)
                    {
                        Send(client, Responde.comunicatsMsg);
                    }
                    else if(String.Compare(_FileName, "") != 0)
                    {
                        string msgToSend = PrivChatEVT.chatEVTDatas.Find(el => el.Name == _FileName).Msg;
                        Send(client, msgToSend);
                    }
                    else
                    {
                        Send(client, Responde.msg);
                    }

                    sendDone.WaitOne();

                    receiveDone.Reset();
                    Receive(client);
                    receiveDone.WaitOne();

                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                }
                else
                {
                    response = "connection problem";
                    Error.IsError = true;
                    Error.ExceptionMsg = response;
                }

                RespondeStatusAndMsg();

            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }

            Thread.CurrentThread.Abort();
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                connectDone.Set();
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                StateObject state = new StateObject
                {
                    workSocket = client
                };

                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);

                    response = state.sb.ToString();
                }

                receiveDone.Set();
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }
        }

        private void Send(Socket client, String data)
        {

            byte[] byteData = Encoding.ASCII.GetBytes(data);

            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket client = (Socket)ar.AsyncState;
                client.EndSend(ar);
                sendDone.Set();
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }
        }

        private void AsynchronousClient_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AsynchronousClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Name = "AsynchronousClient";
            this.Load += new System.EventHandler(this.AsynchronousClient_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void AsynchronousClient_Load_1(object sender, EventArgs e)
        {

        }

        private void RespondeStatusAndMsg()
        {
            if (_isContacts == true)
            {
                Responde.contactsMsg = response;
                Responde.contacts.Set();
            }
            else if (_isContacts == false)
            {
                Responde.comunicatsMsg = response;
                Responde.comunicats.Set();
            }
            else if (String.Compare(_FileName, "") != 0)
            {
                var resp = PrivChatEVT.chatEVTDatas.Find(el => el.Name == _FileName);
                resp.Msg = response;
                resp.Set.Set();
            }
            else
            {
                Responde.msg = response;
                Responde.odebrano.Set();
            }
        }
    }
}