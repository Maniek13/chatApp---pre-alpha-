using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace Klient
{
    public partial class KlientAplikacja : Form
    {
        public static TcpClient tcpclient = new TcpClient();
        public static ManualResetEvent wyswietlono =
             new ManualResetEvent(false);

        /* Dodawanie kont
           private KlientLogowanie _client;

           public KlientAplikacja(KlientLogowanie client)
           {
               this._client = client;
               InitializeComponent();

           }
        */

        public KlientAplikacja()
        {
            InitializeComponent();
        }

        private void WyświetlKontakty()
        {
            foreach (Konta Konta in KlientLogowanie.users)
            {
                Kontakty.Items.Add(Konta.Nazwa);
            }
            Kontakty.EndUpdate();
        }

        public void DodajKontakt(string nick)
        {
            Kontakty.BeginUpdate();
            Kontakty.Items.Add(nick);
            Kontakty.EndUpdate();
        }

        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Wyślij_Click(object sender, EventArgs e)
        {
            Wiadomość();

        }

        private void Kontakty_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void Komunikaty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            WyświetlKontakty();
            Thread msg = new Thread(new ThreadStart(WyświetlWiadomosći))
            {
                IsBackground = true
            };
            msg.Start();
            //dodawanie kont
            Dodaj.Hide();
        }

        private void Komunikaty_TextChanged(object sender, EventArgs e)
        {

        }

        private void Wiadomosc_TextChanged(object sender, EventArgs e)
        {

        }
        private void WyświetlWiadomosći()
        {
            /*bool temp = false;
            while (temp == true)
            {
                if (DateTime.Now.Second % 3 == 0)
                {
                    Wiadomości();
                    wyswietlono.WaitOne();
                    wyswietlono.Reset();
                    wyswietlono.WaitOne(1000);
                }
            }*/

            Wiadomości();
        }
        private void Wiadomości()
        {
            KlientLogowanie.komunikat = "Wyswietl wiadomosci";
            KlientLogowanie.odebrano.Reset();

            Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();

            KlientLogowanie.odebrano.WaitOne();

            try
            {
                if (!this.IsDisposed)
                {
                    Invoke(new Action(() =>
                    {
                        if (KlientLogowanie.komunikat != "")
                        {
                            Komunikaty.AppendText(KlientLogowanie.komunikat + Environment.NewLine);
                        }
                    }));
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (InvalidOperationException)
            {

            }
            wyswietlono.Set();
        }

        private void Wiadomość()
        {
            string wiadomość = Wiadomosc.Text;
            string osoba = Kontakty.SelectedItem.ToString();

            Konta temp = KlientLogowanie.users.Find(x => x.Nazwa.Contains(osoba));

            string odp = SendMsg(temp.Kontakt, wiadomość, false);

            Komunikaty.AppendText(odp + Environment.NewLine);

        }

        public string Wiadomość(string contact, string wiadomość)
        {
            return SendMsg(contact, wiadomość, true);
        }

        private void Dodaj_Click(object sender, EventArgs e)
        {
            NewUser konta = new NewUser(this);
            konta.Show();
        }

        private void Kontakty_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            string osoba = Kontakty.SelectedItem.ToString();
            if (osoba != "")
            {
                Konta temp = KlientLogowanie.users.Find(x => x.Nazwa.Contains(osoba));

                MessageBox messageBox = new MessageBox(temp.Kontakt);

                messageBox.Show();

            }
        }

        private string SendMsg(string contact, string wiadomość, bool priv)
        {
            string login = KlientLogowanie.Osoba;

            if (wiadomość != "")
            {
                string odp = "";
                bool error = false;
                KlientLogowanie.odebrano.Reset();

                if (priv == true)
                {
                    KlientLogowanie.komunikat = "Wiadomosc od:" + login + "#" + wiadomość + "%" + contact + "&" + DateTime.Now;
                    odp = login + " do " + contact + ": " + wiadomość;
                }
                else if (Kontakty.SelectedIndex >= 0)
                {
                    KlientLogowanie.komunikat = "Wiadomosc od:" + login + "#" + wiadomość + "%" + contact + "&" + DateTime.Now;
                    odp = login + " do " + contact + ": " + wiadomość;
                }
                else
                {
                    KlientLogowanie.komunikat = "Wiadomosc od:" + login + "#" + wiadomość + "%&" + DateTime.Now;
                    odp = login + ": " + wiadomość;

                }

                Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient))
                {
                    IsBackground = true
                };
                wątek.Start();
                KlientLogowanie.odebrano.WaitOne();

                try
                {
                    if(priv != true)
                    {
                        if (!this.IsDisposed)
                        {
                            Invoke(new Action(() =>
                            {
                                if (KlientLogowanie.komunikat != "ok")
                                {
                                    error = true;
                                    odp = KlientLogowanie.komunikat;
                                }

                            }));
                        }
                    }
                   
                }
                catch (InvalidOperationException)
                {
                    return "error";
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
    }
       
}
