using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Klient;

namespace Klient.App.Objects
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

                Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient))
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
        
            string osoba = "";
            if (contact != "")
            {
                osoba = contact;
            }

            Konta temp = Accounts.users.Find(x => x.Nazwa.Contains(osoba));

           
            string odp = SendMsg(temp.Kontakt, wiadomosc, false);

            return odp;
        }

        public string Wiadomość(string contact, string wiadomość, bool priv)
        {
            return SendMsg(contact, wiadomość, true);
        }
    }
}
