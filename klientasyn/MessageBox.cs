using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;


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

            if(this.message != "")
            {
                KlientAplikacja klient = new KlientAplikacja();
                string res = klient.Wiadomość(this.name, this.message);
                ChatWindow.AppendText(res + Environment.NewLine);
                TextToSend.Text = "";
            }


        }

        private void MessageBox_Load(object sender, EventArgs e)
        {

        }

        private void TextToSend_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChatWindow_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
