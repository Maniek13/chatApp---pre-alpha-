using Klient.App.Models;
using Klient.App.StaticMembers;
using System;
using System.Linq;
using System.Threading;

namespace Klient.App.Controllers
{
    class MessagesController
    {
        private readonly string login = UserAccount.User;

        public void SendMsg(string contact, string wiadomość, bool priv)
        {
            if (String.Compare(wiadomość, "") != 0)
            {
                Responde.received.Reset();

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
                Responde.received.WaitOne();
            }
        }

        public void Wiadomość(string contact, string wiadomosc)
        {
            UserPrivateMessageBox temp = Accounts.Users.ToList().Find(x => x.User.ShowedName == contact); ;

            if (temp != null)
            {
                SendMsg(temp.User.Account, wiadomosc, false);
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
            Responde.received.Reset();
            Responde.msg = $"Wyswietl wiadomosci#{contact}";

            return GetData();
        }

        public string GetData()
        {
            AsynchronousClient asynchronousClient = new AsynchronousClient();
            Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();
            Responde.received.WaitOne();

            return Responde.msg;
        }
    }
}
