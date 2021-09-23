using System;
using System.Windows.Forms;
using System.Threading;
using Klient.App.Objects;
using Klient.App;
using Klient.App.Controllers;
using System.Threading.Tasks;

namespace Klient
{
    public partial class KlientAplikacja : Form
    {
        public static ManualResetEvent wyswietlono = new ManualResetEvent(false);
        public static ManualResetEvent showsContacts = new ManualResetEvent(false);
        public CancellationTokenSource source = new CancellationTokenSource();
        private static bool stop = false;
        private string contact = "";

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
            this.FormClosed += Close;
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
            string contactToSend = "";
            if (Kontakty.SelectedItem != null)
            {
                contactToSend = SelectedContactName();
            }

            string wiadomość = Wiadomosc.Text;

            MessagesController messagesController = new MessagesController();
            messagesController.Wiadomość(contactToSend, wiadomość);
            Wiadomosc.Text = "";
        }

        private string SelectedContactName()
        {
            string contactToSend;
            string str = Kontakty.SelectedItem.ToString();
            if (str.Contains("online"))
            {
                contactToSend = str.Substring(0, str.Length - 7);
            }
            else
            {
                contactToSend = str;
            }
            return contactToSend;
        }

        private void checkedListBox1_ItemCheck(object sender, EventArgs e)
        {
        }

        private void Komunikaty_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            WyświetlKontakty();
            MessagesController messagesController = new MessagesController();

            CancellationToken token = source.Token;
            TaskFactory factory = new TaskFactory(token);

            factory.StartNew(WyświetlWiadomosći, token);
            factory.StartNew(ShowContacts, token);
            
            /*
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
            */
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
            if(Kontakty.SelectedItem != null)
            {
                if (Kontakty.SelectedItem.ToString() == contact)
                {
                    Kontakty.ClearSelected();
                    contact = "";
                }
                else
                {
                    contact = Kontakty.SelectedItem.ToString();
                }
            }
        }

        private void Kontakty_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Kontakty.SelectedItem != null)
            {
                string osoba = SelectedContactName();
                Konta temp = Accounts.users.Find(x => x.Nazwa.Contains(osoba));

                MessageBox messageBox = new MessageBox(temp.Kontakt);
                messageBox.Show();
            }
        }

        public void WyświetlWiadomosći()
        {
            while (stop == false)
            {
                if (DateTime.Now.Second % 3 == 0)
                {
                    Responde.comunicatsMsg = "Wyswietl wiadomosci"+ Account.usser;
                    Wiadomości();
                    wyswietlono.WaitOne();
                    wyswietlono.Reset();
                    wyswietlono.WaitOne(1000);
                }
            }
        }

        public void ShowContacts()
        {
            while (stop == false)
            {
                if (DateTime.Now.Second % 5 == 0)
                {
                    Responde.contactsMsg = "Active ussers" + Account.usser;
                    Contacts();
                    showsContacts.WaitOne();
                    showsContacts.Reset();
                    showsContacts.WaitOne(1000);
                }
            }
        }

        private void Wiadomości()
        {
            Responde.comunicats.Reset();
            AsynchronousClient asynchronousClient = new AsynchronousClient(false);
            Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();

            Responde.comunicats.WaitOne();
            wątek.Abort();
            wątek.Join();

            try
            {
                if (!this.IsDisposed)
                {
                    Invoke(new Action(() =>
                    {
                        if (!Responde.comunicatsMsg.StartsWith("ok") && Responde.comunicatsMsg != "0" && Responde.comunicatsMsg != "connection problem" && Responde.comunicatsMsg != "")
                        {
                            Komunikaty.AppendText(Responde.comunicatsMsg);
                        }
                    }));
                }
            }
            catch (InvalidOperationException)
            {
                wyswietlono.Set();
            }

            wyswietlono.Set();
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
            wątek.Abort();
            wątek.Join();

            try
            {
                if (!this.IsDisposed)
                {
                    Invoke(new Action(() =>
                    {
                        if (Responde.contactsMsg != "ok"  && Responde.contactsMsg != "connection problem" && Responde.contactsMsg != "")
                        {
                            string temp = Responde.contactsMsg.Substring(2);

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
                                else
                                {
                                    Kontakty.Items.Add(el.Nazwa + " online");
                                }

                            });
                        }
                    }));
                }
            }
            catch (InvalidOperationException)
            {
                showsContacts.Set();
            }

            showsContacts.Set();
        }


        private void Close(object sender, EventArgs e)
        {
            stop = true;
            source.Cancel();
            source.Dispose();
            Environment.Exit(0);
        }
    }
}