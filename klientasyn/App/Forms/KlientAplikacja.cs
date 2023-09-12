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

        public ObservableCollection<UserPrivateMessageBox> Messages
        {
            get => ContactsBox.DataSource as ObservableCollection<UserPrivateMessageBox>;
            set => ContactsBox.DataSource = value;
        }
        public KlientAplikacja()
        {
            stop = false;
            this.FormClosed += Close;
            InitializeComponent();
            Messages = new ObservableCollection<UserPrivateMessageBox>(Accounts.Users);
            ContactsBox.Format += ContactsShowText;
        }
        private void ContactsShowText(object sender, ListControlConvertEventArgs e)
        {
            if (e.ListItem is UserPrivateMessageBox pm)
            {
                string status = pm.User.Status == true ? "online" : "";
                e.Value = $"{pm.User.ShowedName} {status}";
            }
        }

        public void AddContact(string nick)
        {
            ContactsBox.BeginUpdate();
            ContactsBox.Items.Add(new UserPrivateMessageBox() { User = new UsersAccount(nick, nick, false), IsOpen = false });
            ContactsBox.EndUpdate();
        }

        private void SendMsg_BtnClick(object sender, EventArgs e)
        {
            string contactToSend = "";
            if (ContactsBox.SelectedItem != null)
            {
                contactToSend = SelectedContactName();
            }

            string wiadomość = MessageField.Text;

            MessagesController messagesController = new MessagesController();
            messagesController.Wiadomość(contactToSend, wiadomość);

            if (Error.IsError)
            {
                MessagesBox.AppendText(Error.ExceptionMsg);
                ConnectionProblemLogout();
                Error.IsError = false;
            }

            MessageField.Text = "";
        }

        private string SelectedContactName()
        {
            UserPrivateMessageBox pm = (UserPrivateMessageBox)ContactsBox.SelectedItem;
            return pm.User.Account;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MessagesController messagesController = new MessagesController();

            CancellationToken token = source.Token;
            TaskFactory factory = new TaskFactory(token);

            factory.StartNew(ShowMessages, token);
            factory.StartNew(ShowContacts, token);
        }

        private void Dodaj_Click(object sender, EventArgs e)
        {
            NewUser konta = new NewUser(this);
            konta.Show();
        }

        private void Contacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ContactsBox.SelectedItem != null)
            {
                UserPrivateMessageBox pm = (UserPrivateMessageBox)ContactsBox.SelectedItem;
                if (String.Compare(pm.User.ShowedName.ToString(), contact) == 0)
                {
                    ContactsBox.ClearSelected();
                    contact = "";
                }
                else
                {
                    contact = pm.User.ShowedName.ToString();
                }
            }
        }

        private void Contacts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UserPrivateMessageBox pm = (UserPrivateMessageBox)ContactsBox.SelectedItem;

            if (ContactsBox.SelectedItem != null && pm.IsOpen == false)
            {
                ((UserPrivateMessageBox)ContactsBox.SelectedItem).IsOpen = true;

                MessageBox messageBox = new MessageBox((UserPrivateMessageBox)ContactsBox.SelectedItem);
                messageBox.Show();
            }
        }

        public void ShowMessages()
        {
            while (stop == false)
            {
                if (DateTime.Now.Second % 3 == 0)
                {
                    Responde.comunicatsMsg = $"Wyswietl wiadomosci{UserAccount.User}";
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
                    Responde.contactsMsg = $"Active ussers{UserAccount.User}";
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
                            MessagesBox.AppendText(Responde.comunicatsMsg);
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

                            List<UserPrivateMessageBox> activeUsers = new List<UserPrivateMessageBox>();

                            while (String.Compare(temp, "") != 0)
                            {
                                int index = temp.IndexOf("$");

                                var konto = new UsersAccount(temp.Substring(0, index), temp.Substring(0, index), true);

                                UserPrivateMessageBox pm;

                                if (!Accounts.Users.ToList().Exists(el => el.User.ShowedName == konto.ShowedName))
                                {
                                    pm = new UserPrivateMessageBox
                                    {
                                        User = konto,
                                        IsOpen = false
                                    };

                                    Accounts.Users.Add(pm);
                                    isChanged = true;
                                }
                                else
                                {
                                    pm = Accounts.Users.Find(el => el.User.ShowedName == konto.ShowedName);

                                    if (pm.User.Status == false)
                                    {
                                        Accounts.Users.Find(el => el.User.ShowedName == konto.ShowedName).User.Status = true;
                                        isChanged = true;
                                    }
                                }

                                activeUsers.Add(pm);

                                temp = temp.Substring(index + 1);
                            }

                            foreach (var user in Accounts.Users.Except(activeUsers))
                            {
                                if (user.User.Status)
                                {
                                    isChanged = true;
                                    Accounts.Users.Find(el => el.User == user.User).User.Status = false;
                                }

                            }

                            if (isChanged)
                                Messages = new ObservableCollection<UserPrivateMessageBox>(Accounts.Users);
                        }
                    }));
                }

            }
            catch (InvalidOperationException ex)
            {
                MessagesBox.AppendText(ex.Message);
            }

            showsContacts.Set();
        }

        private void Close(object sender, EventArgs e)
        {
            stop = true;
            source.Cancel();
            source.Dispose();

            if (!ErrorLogout)
                Environment.Exit(0);
        }

        private void ConnectionProblemLogout()
        {
            if (String.Compare(Error.ExceptionMsg, "connection problem") == 0)
            {
                Error.IsError = false;
                ErrorLogout = true;
                ContactsBox = null;
                MessagesBox = null;
                Accounts.Users.Clear();

                this.Close();
                KlientLogowanie klientLogowanie = new KlientLogowanie("Server was down");
                klientLogowanie.Show();
            }
        }
    }
}