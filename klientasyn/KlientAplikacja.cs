using System;
using System.Windows.Forms;
using System.Threading;
using Klient.App.Objects;

namespace Klient
{
    public partial class KlientAplikacja : Form
    {
        public static ManualResetEvent wyswietlono = new ManualResetEvent(false);

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
            foreach (Konta Konta in Accounts.users)
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
            string contact = "";
            if (Kontakty.SelectedItem != null)
            {
                contact = Kontakty.SelectedItem.ToString();
            }

            string wiadomość = Wiadomosc.Text;

            MessagesController messagesController = new MessagesController();
            string odp = messagesController.Wiadomość(contact, wiadomość);

            if (odp != "ok")
            {
                Komunikaty.AppendText(odp);
            }

            Wiadomosc.Text = "";
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
            MessagesController messagesController = new MessagesController();
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

        private void Dodaj_Click(object sender, EventArgs e)
        {
            NewUser konta = new NewUser(this);
            konta.Show();
        }

        private void Kontakty_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void Kontakty_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Kontakty.SelectedItem != null)
            {
                string osoba = Kontakty.SelectedItem.ToString();
                Konta temp = Accounts.users.Find(x => x.Nazwa.Contains(osoba));

                MessageBox messageBox = new MessageBox(temp.Kontakt);

                messageBox.Show();
            }
        }

        private void Wiadomości()
        {
            Responde.odebrano.Reset();

            Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();

            Responde.odebrano.WaitOne();

            try
            {
                if (!this.IsDisposed)
                {
                    Invoke(new Action(() =>
                    {
                        if (Responde.komunikat != "ok")
                        {
                            Komunikaty.AppendText(Responde.komunikat);
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

        public void WyświetlWiadomosći()
        {
            bool temp = true;

            while (temp == true)
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    Responde.komunikat = "Wyswietl wiadomosci";
                    Wiadomości();
                    wyswietlono.WaitOne();
                    wyswietlono.Reset();
                    wyswietlono.WaitOne(1000);
                }
            }
        }
    }

}