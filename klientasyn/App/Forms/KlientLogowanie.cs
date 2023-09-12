using Klient.App;
using Klient.App.Models;
using Klient.App.StaticMembers;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Klient
{
    public partial class KlientLogowanie : Form
    {
        public string Logowanie()
        {
            return LoginField.Text;
        }

        public KlientLogowanie()
        {
            this.FormClosed += Close;
            InitializeComponent();
        }

        public KlientLogowanie(string msg)
        {
            this.FormClosed += Close;
            InitializeComponent();
            StatusBox.Text = msg;
        }


        private void LoginBtn_Click(object sender, EventArgs e)
        {
            Responde.msg = $"LOG{LoginField.Text}${PasswordField.Text}";
            LogOrRegisterInquiry();
        }


        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            Responde.msg = $"REJ{LoginField.Text}${PasswordField.Text}";
            LogOrRegisterInquiry();
        }

        private void LogOrRegisterInquiry()
        {
            Responde.received.Reset();
            AsynchronousClient asynchronousClient = new AsynchronousClient();
            Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();
            Responde.received.WaitOne();
            wątek.Abort();
            wątek.Join();

            if (Responde.msg.StartsWith("ok"))
            {
                UserList();
                KlientAplikacja nowy = new KlientAplikacja();
                nowy.Show();
                this.Hide();
            }
            else if (String.Compare(Responde.msg, "connection problem") != 0 || String.Compare(Responde.msg, "Istnieje") != 0 || String.Compare(Responde.msg, "Nie istnieje") != 0)
            {
                LoginField.Clear();
                PasswordField.Clear();
                StatusBox.Text = Responde.msg;
            }
            else
            {
                LoginField.Clear();
                PasswordField.Clear();
                StatusBox.Text = Responde.msg;
            }
        }

        private Accounts UserList()
        {
            Accounts accounts = new Accounts();
            UserAccount.User = LoginField.Text;

            string temp = Responde.msg.Substring(2);

            while (temp != "")
            {
                int index = temp.IndexOf("$");

                UserPrivateMessageBox pm = new UserPrivateMessageBox()
                {
                    User = new UsersAccount(temp.Substring(0, index), temp.Substring(0, index), false),
                    IsOpen = false,
                };

                Accounts.Users.Add(pm);
                temp = temp.Substring(index + 1);
            }

            return accounts;
        }

        private void Close(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}