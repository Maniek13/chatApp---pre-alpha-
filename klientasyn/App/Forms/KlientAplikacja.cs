using Klient.App;
using Klient.App.Controllers;
using Klient.App.Models;
using Klient.App.StaticMembers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klient
{
    public partial class KlientAplikacja : Form
    {
        public static ManualResetEvent wyswietlono = new ManualResetEvent(false);
        public static ManualResetEvent showsContacts = new ManualResetEvent(false);
        public CancellationTokenSource source = new CancellationTokenSource();
        private static bool stop = false;
        private string contact = "";
        private bool ErrorLogout = false;

        /* Dodawanie kont
           private KlientLogowanie _client;
           public KlientAplikacja(KlientLogowanie client)
           {
               this._client = client;
               InitializeComponent();
           }
        */

        public ObservableCollection<PrivateMessage> Messages
        {
            get => Kontakty.DataSource as ObservableCollection<PrivateMessage>;
            set => Kontakty.DataSource = value;
        }
        public KlientAplikacja()
        {
            stop = false;
            this.FormClosed += Close;
            InitializeComponent();
            Messages = new ObservableCollection<PrivateMessage>(Accounts.users);
            Kontakty.Format += KontaktyShowText;
        }
        private void KontaktyShowText(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is PrivateMessage pm)
            {
                string status = pm.User.Status == true ? "online" : "";
                e.Value = $"{pm.User.Nazwa} {status}";
            }
        }

        public void DodajKontakt(string nick)
        {
            Kontakty.BeginUpdate();
            Kontakty.Items.Add(new PrivateMessage() { User = new Konta(nick, nick, false), IsOpen = false });
            Kontakty.EndUpdate();
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

            if (Error.IsError)
            {
                Komunikaty.AppendText(Error.ExceptionMsg);
                ConnectionProblemLogout();
                Error.IsError = false;
            }
            
            Wiadomosc.Text = "";
        }

        private string SelectedContactName()
        {
            PrivateMessage pm = (PrivateMessage)Kontakty.SelectedItem;
            return pm.User.Kontakt;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MessagesController messagesController = new MessagesController();

            CancellationToken token = source.Token;
            TaskFactory factory = new TaskFactory(token);

            factory.StartNew(WyświetlWiadomosći, token);
            factory.StartNew(ShowContacts, token);
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
                PrivateMessage pm = (PrivateMessage)Kontakty.SelectedItem;
                if (String.Compare(pm.User.Nazwa.ToString(),contact) == 0)
                {
                    Kontakty.ClearSelected();
                    contact = "";
                }
                else
                {
                    contact = pm.User.Nazwa.ToString();
                }
            }
        }

        private void Kontakty_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PrivateMessage pm = (PrivateMessage)Kontakty.SelectedItem;

            if (Kontakty.SelectedItem != null && pm.IsOpen == false)
            {
                ((PrivateMessage)Kontakty.SelectedItem).IsOpen = true;

                MessageBox messageBox = new MessageBox((PrivateMessage)Kontakty.SelectedItem);
                messageBox.Show(); 
            }
        }

        public void WyświetlWiadomosći()
        {
            while (stop == false)
            {
                if (DateTime.Now.Second % 3 == 0)
                {
                    Responde.comunicatsMsg = $"Wyswietl wiadomosci{Account.usser}";
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
                    Responde.contactsMsg = $"Active ussers{Account.usser}";
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
                        if (String.Compare(Responde.comunicatsMsg, "connection problem") == 0)
                        {
                            Error.IsError = true;
                            Error.ExceptionMsg = Responde.comunicatsMsg;
                            ConnectionProblemLogout();
                        }

                        if (!Responde.comunicatsMsg.StartsWith("ok") && String.Compare(Responde.comunicatsMsg, "0") != 0 && String.Compare(Responde.comunicatsMsg, "connection problem") != 0 && String.Compare(Responde.comunicatsMsg, "") != 0)
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
                        bool isChanged = false;

                        if (String.Compare(Responde.contactsMsg, "connection problem") != 0 && String.Compare(Responde.contactsMsg, "") != 0)
                        {
                            string temp = Responde.contactsMsg.Substring(2);

                            List<PrivateMessage> activeUsers = new List<PrivateMessage>();

                            while (String.Compare(temp, "") != 0)
                            {
                                int index = temp.IndexOf("$");

                                var konto = new Konta(temp.Substring(0, index), temp.Substring(0, index), true);

                                PrivateMessage pm;

                                if (!Accounts.users.ToList().Exists(el => el.User.Nazwa == konto.Nazwa))
                                {
                                    pm = new PrivateMessage
                                    {
                                        User = konto,
                                        IsOpen = false
                                    };

                                    Accounts.users.Add(pm);
                                    isChanged = true;
                                }
                                else
                                {
                                    pm = Accounts.users.Find(el => el.User.Nazwa == konto.Nazwa);

                                    if (pm.User.Status == false)
                                    {
                                        Accounts.users.Find(el => el.User.Nazwa == konto.Nazwa).User.Status = true;
                                        isChanged = true;
                                    }
                                }

                                activeUsers.Add(pm);
                                
                                temp = temp.Substring(index + 1);
                            }

                            foreach(var user in Accounts.users.Except(activeUsers))
                            {
                                if(user.User.Status)
                                {
                                    isChanged = true;
                                    Accounts.users.Find(el => el.User == user.User).User.Status = false;
                                }
                                
                            }

                            if (isChanged)
                                Messages = new ObservableCollection<PrivateMessage>(Accounts.users);
                        }
                    }));
                }
            
            }
            catch (InvalidOperationException ex)
            {
                Komunikaty.AppendText(ex.Message);
            }

            showsContacts.Set();
        }

        private void Close(object sender, EventArgs e)
        {
            stop = true;
            source.Cancel();
            source.Dispose();

            if(!ErrorLogout)
                Environment.Exit(0);
        }

        private void ConnectionProblemLogout()
        {
            if (String.Compare(Error.ExceptionMsg, "connection problem") == 0)
            {
                Error.IsError = false;
                ErrorLogout = true;
                Kontakty = null;
                Komunikaty = null;
                Accounts.users.Clear();
                
                this.Close();
                KlientLogowanie klientLogowanie = new KlientLogowanie("Server was down");
                klientLogowanie.Show();
            }
        }
    }
}