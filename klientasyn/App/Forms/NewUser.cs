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
            var konto = new UsersAccount(textBox1.Text, textBox2.Text, false);
            UserPrivateMessageBox pm = new UserPrivateMessageBox()
            {
                User = konto,
                IsOpen = false,
            };
            Accounts.Users.Add(pm);
            this._client.AddContact(textBox2.Text);
            this.Hide();
        }
    }
}
