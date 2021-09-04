using Klient.App.Objects;
using System;
using System.Windows.Forms;

namespace Klient
{
    public partial class NewUser : Form
    {
        private readonly KlientAplikacja _client;

        public NewUser(KlientAplikacja client)
        {
            this._client = client;
            InitializeComponent();      
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            Nowy();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }
        public void Nowy()
        {
            Accounts.users.Add(new Konta(textBox1.Text, textBox2.Text, false));
            this._client.DodajKontakt(textBox2.Text);
            this.Hide();
        }

        private void NewUser_Load(object sender, EventArgs e)
        {

        }
    }
}
