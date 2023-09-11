using Klient.App.Models;
using Klient.App.StaticMembers;
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

        private void NewUser_BtnClick(object sender, EventArgs e)
        { 
            Nowy();
        }
        public void Nowy()
        {
            var konto = new Konta(textBox1.Text, textBox2.Text, false);
            PrivateMessage pm = new PrivateMessage()
            {
                User = konto,
                IsOpen = false,
            };
            Accounts.users.Add(pm);
            this._client.DodajKontakt(textBox2.Text);
            this.Hide();
        }

        private void NewUser_Load(object sender, EventArgs e)
        {

        }
    }
}
