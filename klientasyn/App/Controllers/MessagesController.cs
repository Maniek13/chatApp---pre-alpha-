using System;
using System.Threading;
using Klient.App.Objects;

namespace Klient.App.Controllers
{
    class MessagesController
    {
        public string SendMsg(string contact, string wiadomość, bool priv)
        {
            string login = Account.usser;

            if (wiadomość != "")
            {
                string odp = "";
                bool error = false;
                Responde.odebrano.Reset();

                if (priv == true)
                {
                    Responde.komunikat = "Wiadomosc od:Priv" + login + "#" + wiadomość + "%" + contact + "&" + DateTime.Now;
                    odp = login + " do " + contact + ": " + wiadomość;
                }
                else if (contact != "")
                {
                    Responde.komunikat = "Wiadomosc od:" + login + "#" + wiadomość + "%" + contact + "&" + DateTime.Now;
                    odp = "ok";
                }
                else
                {
                    Responde.komunikat = "Wiadomosc od:" + login + "#" + wiadomość + "%&" + DateTime.Now;
                    odp = "ok";
                }

                AsynchronousClient asynchronousClient = new AsynchronousClient();
                Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
                {
                    IsBackground = true
                };
                wątek.Start();
                Responde.odebrano.WaitOne();

             
                if (priv != true)
                {
                    if (Responde.komunikat != "ok")
                    {
                        error = true;
                        odp = Responde.komunikat;
                    }
                }

            

                if (error != true)
                {
                    return odp;
                }
                else
                {
                    return "error";
                }

            }
            else
            {
                return "error";
            }
        }

        public string Wiadomość(string contact, string wiadomosc)
        {
            string odp;
            string osoba = "";
            if (contact != "")
            {
                osoba = contact;
            }

            Konta temp = Accounts.users.Find(x => x.Nazwa == osoba);
            
            if(temp != null)
            {
                odp = SendMsg(temp.Kontakt, wiadomosc, false);
            }
            else
            {
                odp = SendMsg("", wiadomosc, false);
            }
           
            

            return odp;
        }

        public string Wiadomość(string contact, string wiadomość, bool priv)
        {
            return SendMsg(contact, wiadomość, priv);
        }

    }
}
