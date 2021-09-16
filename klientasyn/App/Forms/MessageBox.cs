using Klient.App.Objects;
using System;
using System.Windows.Forms;
using Klient.App.Controllers;
using System.Collections.Generic;

namespace Klient
{
    public partial class MessageBox : Form
    {
        private readonly string name;
        private string message;

        public MessageBox(string name)
        {
            this.name = name;

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.message = TextToSend.Text;

            if (this.message != "")
            {
                MessagesController messagesController = new MessagesController();
                messagesController.Wiadomość(this.name, this.message, true);
                TextToSend.Text = "";
            }
        }

        private void MessageBox_Load(object sender, EventArgs e)
        {
            ContactName.Text = this.name;

            MessagesController messagesController = new MessagesController();

            List<string> usrs = new List<string> { name, Account.usser };
            usrs.Sort(delegate (string x, string y)
            {
                return x.CompareTo(y);
            });

            string res = messagesController.ShowMsgs(usrs[0] + usrs[1]);

            if(res != "-1")
            {
                ChatWindow.AppendText(res + Environment.NewLine);
            }
            
        }

        private void TextToSend_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChatWindow_TextChanged(object sender, EventArgs e)
        {

        }

        private void ContactName_Click(object sender, EventArgs e)
        {

        }

        private void Messages()
        {
        }
    }
}