using serwer.App.Helper;
using serwer.App.Objects;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace serwer.App.Controllers
{
    public class AsynchronousSocketListener
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        private IPAddress[] ipv4Addresses;
        private IPEndPoint localEndPoint;
        private Socket listener;

        public AsynchronousSocketListener()
        {
            ipv4Addresses = Array.FindAll(
                Dns.GetHostEntry(string.Empty).AddressList,
                a => a.AddressFamily == AddressFamily.InterNetwork);

            localEndPoint = new IPEndPoint(ipv4Addresses[0], 11000);
            listener = new Socket(ipv4Addresses[0].AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            ServerHelpers.Ip = ipv4Addresses[0].ToString();
        }

        public void StartListening()
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);
            Serwer msg = new Serwer();
            try
            {
                while (true)
                {
                    allDone.Reset();
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);
                    allDone.WaitOne();
                   
                }
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }
        }

        public void AcceptCallback(IAsyncResult ar)
        {
            try
            {
                allDone.Set();

                Socket listener = (Socket)ar.AsyncState;
                Socket handler = listener.EndAccept(ar);
                StateObject state = new StateObject
                {
                    workSocket = handler
                };
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
                allDone.Set();
            }
            
        }

        public async void ReadCallback(IAsyncResult ar)
        {
            try
            {
                String content;

                StateObject state = (StateObject)ar.AsyncState;
                Socket handler = state.workSocket;

                int bytesRead = handler.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));

                    content = state.sb.ToString();

                    Obliczenia obliczenia = new Obliczenia();
                    content = await obliczenia.Start(content);

                    Send(handler, content);
                }
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }
            
        }

        private void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;
                int bytesSent = handler.EndSend(ar);
                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception p)
            {
                Console.WriteLine("Error..... " + p.StackTrace);
            }
        }

    }
}
