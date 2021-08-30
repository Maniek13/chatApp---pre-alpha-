using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace Klient
{
    public partial class KlientLogowanie : Form
    {
        public static String komunikat = "";
        public static string Osoba = "";
        public static ManualResetEvent odebrano =
            new ManualResetEvent(false);
        public static List<Konta> users = new List<Konta>();

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
            komunikat = "LOG" + Login.Text + "$" + hasło.Text;

            odebrano.Reset();
            Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();
            odebrano.WaitOne();
 
                if (komunikat.StartsWith("ok"))
                {
                    UserList();
                 
                    KlientAplikacja nowy = new KlientAplikacja();
                    nowy.Show();
                    this.Hide();
                }
                else if (komunikat != "connection problem")
                {
                    Login.Clear();
                    hasło.Clear();
                    textBox1.Text = komunikat;

                }
                else
                {
                    Login.Clear();
                    hasło.Clear();
                    textBox1.Text = komunikat;
                }
        }


        private void Zarejestruj_Click(object sender, EventArgs e)
        {
            odebrano.Reset();
            komunikat = "REJ" + Login.Text + "$" + hasło.Text;

            Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();
            odebrano.WaitOne();
                  
            if (komunikat.StartsWith("ok"))
            {
                UserList();
                KlientAplikacja nowy = new KlientAplikacja();
                nowy.Show();
                this.Hide();
            }
            else if(komunikat != "connection problem")
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = komunikat;
            }
            else
            {
                Login.Clear();
                hasło.Clear();
                textBox1.Text = komunikat;
            }
        }

        private void UserList()
        {
            Osoba = Login.Text;

            string temp = komunikat.Substring(2);

            while (temp != "")
            {
                int index = temp.IndexOf("$");
                users.Add((new Konta(temp.Substring(0, index), temp.Substring(0, index))));
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
