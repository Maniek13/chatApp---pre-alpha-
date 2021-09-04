using System;
using System.Windows.Forms;
using System.Threading;
using Klient.App.Objects;
using Klient.App;
using System.Linq;

namespace Klient
{
    public partial class KlientAplikacja : Form
    {
        public static ManualResetEvent wyswietlono = new ManualResetEvent(false);
        public static ManualResetEvent showsContacts = new ManualResetEvent(false);

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

            /*
            if (odp != "ok")
            {
                Komunikaty.AppendText(odp + Environment.NewLine);
            }
            */
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

            Thread contacts = new Thread(new ThreadStart(ShowContacts))
            {
                IsBackground = true
            };
            contacts.Start();
            //dodawanie kont
            //Dodaj.Hide();
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
            AsynchronousClient asynchronousClient = new AsynchronousClient();
            Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
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
                        if (!Responde.komunikat.StartsWith("ok"))
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
            wątek.Abort();
            wyswietlono.Set();
        }

        public void WyświetlWiadomosći()
        {
            bool temp = true;

            while (temp == true)
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    Responde.komunikat = "Wyswietl wiadomosci"+ Account.usser;
                    Wiadomości();
                    wyswietlono.WaitOne();
                    wyswietlono.Reset();
                    wyswietlono.WaitOne(1000);
                }
            }
        }

        public void ShowContacts()
        {
            bool temp = true;

            while (temp == true)
            {
                if (DateTime.Now.Second % 5 == 0)
                {
                    Responde.contactsKomunikat = "Active ussers" + Account.usser;
                    Contacts();
                    showsContacts.WaitOne();
                    showsContacts.Reset();
                    showsContacts.WaitOne(1000);
                }
            }
        }

        private void Contacts()
        {
            Responde.contacts.Reset();

            AsynchronousClient asynchronousClient = new AsynchronousClient(true);
            Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();

            Responde.contacts.WaitOne();

            try
            {
                if (!this.IsDisposed)
                {
                    Invoke(new Action(() =>
                    {
                        if (Responde.contactsKomunikat != "ok" && Responde.contactsKomunikat != "connection problem" )
                        {
                            string temp = Responde.contactsKomunikat.Substring(2);


                            while (temp != "")
                            {
                                int index = temp.IndexOf("$");

                                var konto = new Konta(temp.Substring(0, index), temp.Substring(0, index), true);

                                if (Accounts.users.Exists(el => el.Nazwa == konto.Nazwa))
                                {
                                    bool status = Accounts.users.Find(el => el.Nazwa == konto.Nazwa).Status;
                                    if (status == false)
                                    {
                                        Accounts.users.Find(el => el.Nazwa == konto.Nazwa).Status = true;

                                    }
                                }
                                else
                                {
                                    Accounts.users.Add(konto);
                                }

                                temp = temp.Substring(index + 1);
                            }

                            Accounts.users.ForEach(delegate (Konta el) {


                                if (Kontakty.Items.Contains(el.Nazwa))
                                {
                                    if (el.Status == true)
                                    {
                                        Kontakty.Items.Remove(el.Nazwa);
                                        Kontakty.Items.Add(el.Nazwa + " online");
                                    }
                                }
                                else if (Kontakty.Items.Contains(el.Nazwa + " online"))
                                {
                                    if (el.Status == false)
                                    {
                                        Kontakty.Items.Remove(el.Nazwa + " online");
                                        Kontakty.Items.Add(el.Nazwa);
                                    }
                                }

                            });
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

            wątek.Abort();
            showsContacts.Set();
        }
    }
}