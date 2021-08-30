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
            InitializeComponent();
        }

        private void Zaloguj_Click(object sender, EventArgs e)
        {
            Responde.komunikat = "LOG" + Login.Text + "$" + hasło.Text;

            Responde.odebrano.Reset();
            Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();
            Responde.odebrano.WaitOne();

            if (Responde.komunikat.StartsWith("ok"))
            {
                UserList();

                KlientAplikacja nowy = new KlientAplikacja();
                nowy.Show();
                this.Hide();
            }
            else if (Responde.komunikat != "connection problem")
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = Responde.komunikat;

            }
            else
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = Responde.komunikat;
            }
        }


        private void Zarejestruj_Click(object sender, EventArgs e)
        {
            Responde.odebrano.Reset();
            Responde.komunikat = "REJ" + Login.Text + "$" + hasło.Text;

            Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();
            Responde.odebrano.WaitOne();

            if (Responde.komunikat.StartsWith("ok"))
            {
                UserList();
                KlientAplikacja nowy = new KlientAplikacja();
                nowy.Show();
                this.Hide();
            }
            else if (Responde.komunikat != "connection problem")
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = Responde.komunikat;
            }
            else
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = Responde.komunikat;
            }
        }

        private void UserList()
        {
            Account.usser = Login.Text;

            string temp = Responde.komunikat.Substring(2);

            while (temp != "")
            {
                int index = temp.IndexOf("$");
                Accounts.users.Add((new Konta(temp.Substring(0, index), temp.Substring(0, index))));
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
    }
}