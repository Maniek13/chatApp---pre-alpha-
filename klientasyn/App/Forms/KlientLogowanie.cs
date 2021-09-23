using System;
using System.Windows.Forms;
using System.Threading;
using Klient.App.Objects;
using Klient.App;

namespace Klient
{
    public partial class KlientLogowanie : Form
    {
        public string Logowanie()
        {
            return Login.Text;
        }

        public KlientLogowanie()
        {
            this.FormClosed += Close;
            InitializeComponent();
        }

        private void Zaloguj_Click(object sender, EventArgs e)
        {
            Responde.msg = "LOG" + Login.Text + "$" + hasło.Text;
            LogOrRegisterInquiry();
        }


        private void Zarejestruj_Click(object sender, EventArgs e)
        {
            Responde.msg = "REJ" + Login.Text + "$" + hasło.Text;
            LogOrRegisterInquiry();
        }

        private void LogOrRegisterInquiry()
        {
            Responde.odebrano.Reset();
            AsynchronousClient asynchronousClient = new AsynchronousClient();
            Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();
            Responde.odebrano.WaitOne();
            wątek.Abort();
            wątek.Join();

            if (Responde.msg.StartsWith("ok"))
            {
                UserList();
                KlientAplikacja nowy = new KlientAplikacja();
                nowy.Show();
                this.Hide();
            }
            else if (String.Compare(Responde.msg, "connection problem") != 0)
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = Responde.msg;
            }
            else
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = Responde.msg;
            }
        }

        private void UserList()
        {
            Account.usser = Login.Text;

            string temp = Responde.msg.Substring(2);

            while (temp != "")
            {
                int index = temp.IndexOf("$");
                Accounts.users.Add((new Konta(temp.Substring(0, index), temp.Substring(0, index), false)));
                temp = temp.Substring(index + 1);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            KlientAplikacja nowy = new KlientAplikacja();
            nowy.Show();
            this.Hide();
        }

        private void Login_TextChanged(object sender, EventArgs e)
        {
        }

        private void hasło_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void KlientLogowanie_Load(object sender, EventArgs e)
        {
        }

        private void Close(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}