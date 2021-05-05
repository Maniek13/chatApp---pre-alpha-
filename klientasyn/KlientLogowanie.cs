using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Klient
{
    public partial class KlientLogowanie : Form
    {
        public static String komunikat = "";
        public static string Osoba = "";
        public static ManualResetEvent odebrano =
            new ManualResetEvent(false);
        public static List<Konta> users = new List<Konta>();

        public string Logowanie()
        {
            return Login.Text;
        }
     
        public KlientLogowanie()
        {
            InitializeComponent();            
        }

        private void Zaloguj_Click(object sender, EventArgs e)
        {    
            komunikat = "LOG" + Login.Text + "$" + hasło.Text;

            odebrano.Reset();
            Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient));
            wątek.IsBackground = true;
            wątek.Start();
            odebrano.WaitOne();
 
                if (komunikat.StartsWith("ok"))
                {
                    Osoba = Login.Text;

                    string temp = komunikat.Substring(2);
                    
                    while(temp != "")
                    {
                    int index = temp.IndexOf("$");
                    users.Add((new Konta(temp.Substring(0, index), temp.Substring(0, index))));
                    temp = temp.Substring(index+1);

                    }
                 

                    KlientAplikacja nowy = new KlientAplikacja();
                    nowy.Show();
                    this.Hide();
                }
                else if (komunikat != "connection problem")
                {
                    Login.Clear();
                    hasło.Clear();
                    textBox1.Text = komunikat;

                }
                else
                {
                    Login.Clear();
                    hasło.Clear();
                    textBox1.Text = komunikat;
                }
        }


        private void Zarejestruj_Click(object sender, EventArgs e)
        {
            odebrano.Reset();
            komunikat = "REJ" + Login.Text + "$" + hasło.Text;

            Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient));
            wątek.IsBackground = true;
            wątek.Start();
            odebrano.WaitOne();
                  
            if (komunikat == "Ok")
            {
                KlientAplikacja nowy = new KlientAplikacja();
                nowy.Show();
                this.Hide();
            }
            else if(komunikat != "connection problem")
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = komunikat;
            }
            else
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = komunikat;
            }
        }
        
        private void Button1_Click(object sender, EventArgs e)
        {
            KlientAplikacja nowy = new KlientAplikacja();
            nowy.Show();
            this.Hide();
        }

        private void Login_TextChanged(object sender, EventArgs e)
        {
        }

        private void hasło_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void KlientLogowanie_Load(object sender, EventArgs e)
        {
        }
    }

   
    public class StateObject
    {
        public Socket workSocket = null; 
        public const int BufferSize = 256;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }

    public class AsynchronousClient : KlientLogowanie
    {
        private const int port = 11000;

        private static ManualResetEvent connectDone =
            new ManualResetEvent(false);
        private static ManualResetEvent sendDone =
            new ManualResetEvent(false);
        private static ManualResetEvent receiveDone =
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


                if(connectDone.WaitOne(500) == true)
                {
                    sendDone.Reset();
                    Send(client, komunikat);
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
            komunikat = response;
            odebrano.Set();
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
                StateObject state = new StateObject();
                state.workSocket = client;

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


    }

}
