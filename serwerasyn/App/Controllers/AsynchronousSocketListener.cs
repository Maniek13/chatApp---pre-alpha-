using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using serwer.App.Objects;

namespace serwer.App.Controllers
{
    public class AsynchronousSocketListener
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);

        public static void StartListening()
        {
             IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

          //  IPHostEntry ipHostInfo = Dns.GetHostEntry("127.0.0.1");

            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);


            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            Serwer msg = new Serwer();

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

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

        public static void AcceptCallback(IAsyncResult ar)
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

        public async static void ReadCallback(IAsyncResult ar)
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

        private static void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }

        private static void SendCallback(IAsyncResult ar)
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
