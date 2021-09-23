using Klient.App.Objects;
using System;
using System.Windows.Forms;
using Klient.App.Controllers;
using System.Collections.Generic;
using System.Threading;
using Klient.App;
using System.Threading.Tasks;

namespace Klient
{
    public partial class MessageBox : Form
    {
        private readonly string name;
        private string message;
        public CancellationTokenSource sourceMsgBox = new CancellationTokenSource();
        private readonly string filename;
        private TaskFactory factory;
        private bool stop = false;

        public MessageBox(string name)
        {
            this.FormClosed += CloseMessageBox;
            this.name = name;
            List<string> ussers = new List<string> { name, Account.usser };

            ussers.Sort(delegate (string x, string y)
            {
                return x.CompareTo(y);
            });

            filename = ussers[0] + ussers[1];
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.message = TextToSend.Text;

            if (String.Compare(this.message, "") != 0)
            {
                MessagesController messagesController = new MessagesController();
                messagesController.Wiadomość(this.name, this.message, true);
                TextToSend.Text = "";
            }
        }

        private void MessageBox_Load(object sender, EventArgs e)
        {
            ContactName.Text = this.name;

            CancellationToken token = sourceMsgBox.Token;
            factory = new TaskFactory(token);

            ChatEVTData chatEVTData = new ChatEVTData
            {
                Name = filename
            };

            PrivChatEVT.chatEVTDatas.Add(chatEVTData);

            factory.StartNew(Messages, token);
           
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
            while (stop == false)
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    var msg = PrivChatEVT.chatEVTDatas.Find(el => el.Name == filename);
                    msg.Msg = "Wyswietl wiadomosci#" + filename + "%" + Account.usser;
                    Wiadomości();
                    msg.msgsShowed.WaitOne();
                    msg.msgsShowed.Reset();
                    msg.msgsShowed.WaitOne(1000);
                }
            }
        }

        private void Wiadomości()
        {
            var usserChatData = PrivChatEVT.chatEVTDatas.Find(el => el.Name == filename);
            usserChatData.Set.Reset();

            AsynchronousClient asynchronousClient = new AsynchronousClient(filename);
            Thread wątek = new Thread(new ThreadStart(asynchronousClient.StartClient))
            {
                IsBackground = true
            };
            wątek.Start();

            usserChatData.Set.WaitOne();
            wątek.Abort();
            wątek.Join();

            try
            {
                if (!this.IsDisposed)
                {
                    Invoke(new Action(() =>
                    {
                        if (!usserChatData.Msg.StartsWith("ok") && String.Compare(usserChatData.Msg, "0") != 0 && String.Compare(usserChatData.Msg, "connection problem") != 0)
                        {
                            ChatWindow.AppendText(usserChatData.Msg);
                        }
                    }));
                }
            }
            catch (InvalidOperationException)
            {
                usserChatData.msgsShowed.Set();
            }

            usserChatData.msgsShowed.Set();
        }

        private void CloseMessageBox(object sender, EventArgs e)
        {
            stop = true;
            sourceMsgBox.Cancel();
            sourceMsgBox.Dispose();
        }
    }
}