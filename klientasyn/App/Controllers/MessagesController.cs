using System;
using System.Text;
using System.Threading;
using Klient.App.Objects;

namespace Klient.App.Controllers
{
    class MessagesController
    {
        private readonly string login = Account.usser;

        public void SendMsg(string contact, string wiadomość, bool priv)
        {
            if (String.Compare(wiadomość, "") != 0)
            {
                Responde.odebrano.Reset();

                if (priv == true)
                {
                    Responde.msg = $"Wiadomosc od:Priv{login}#{wiadomość}%{contact}&{DateTime.Now}";
                }
                else if (contact != "")
                {
                    Responde.msg = $"Wiadomosc od:{login}#{wiadomość}%{contact}&{DateTime.Now}";
                }
                else
                {
                    Responde.msg = $"Wiadomosc od:{login}#{wiadomość}%&{DateTime.Now}";
                }

                AsynchronousClient asynchronousClient = new AsynchronousClient();
                Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
                {
                    IsBackground = true
                };
                wątek.Start();
                Responde.odebrano.WaitOne();
            }
        }

        public void Wiadomość(string contact, string wiadomosc)
        {
            Konta temp = Accounts.users.Find(x => x.Nazwa == contact);
            
            if(temp != null)
            {
                SendMsg(temp.Kontakt, wiadomosc, false);
            }
            else
            {
                SendMsg("", wiadomosc, false);
            }
        }

        public void Wiadomość(string contact, string wiadomość, bool priv)
        {
            SendMsg(contact, wiadomość, priv);
        }

        public string ShowNewMsgs(string contact)
        {
            Responde.odebrano.Reset();
            Responde.msg = $"Wyswietl wiadomosci#{contact}";

            return GetData();
        }

        public static string GetData()
        {
            AsynchronousClient asynchronousClient = new AsynchronousClient();
            Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();
            Responde.odebrano.WaitOne();

            return Responde.msg;
        }
    }
}
