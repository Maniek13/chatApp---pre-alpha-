using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Klient.App.Objects;

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

        private static String response = "";

        public static void StartClient()
        {

            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry("127.0.0.1");
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
                    Send(client, Responde.komunikat);
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
                }
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }
            Responde.komunikat = response;
            Responde.odebrano.Set();
            Thread.CurrentThread.Abort();
        }

        private static void ConnectCallback(IAsyncResult ar)
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

        private static void Receive(Socket client)
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

        private static void ReceiveCallback(IAsyncResult ar)
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
                    receiveDone.Set();
                }
                else
                {

                }
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }
        }

        private static void Send(Socket client, String data)
        {

            byte[] byteData = Encoding.ASCII.GetBytes(data);

            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
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
    }
}