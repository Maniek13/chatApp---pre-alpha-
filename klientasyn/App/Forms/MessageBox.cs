using Klient.App;
using Klient.App.Controllers;
using Klient.App.Models;
using Klient.App.StaticMembers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Klient
{
    public partial class MessageBox : Form
    {
        private readonly string name = "";
        private string message = "";
        public CancellationTokenSource sourceMsgBox = new CancellationTokenSource();
        private readonly string filename = "";
        private TaskFactory factory;
        private bool stop = false;


        public MessageBox(UserPrivateMessageBox pm)
        {
            this.FormClosed += (sender, e) => CloseMessageBox(pm);
            this.name = pm.User.Account;
            List<string> ussers = new List<string> { pm.User.Account, UserAccount.User };

            ussers.Sort(delegate (string x, string y)
            {
                return x.CompareTo(y);
            });

            filename = ussers[0] + ussers[1];
            InitializeComponent();
        }

        private void SendMessage_BtnClick(object sender, EventArgs e)
        {
            this.message = TextToSendField.Text;

            if (String.Compare(this.message, "") != 0)
            {
                MessagesController messagesController = new MessagesController();
                messagesController.Wiadomość(this.name, this.message, true);
                TextToSendField.Text = "";
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

        private void Messages()
        {
            while (stop == false)
            {
                if (DateTime.Now.Second % 2 == 0)
                {
                    var msg = PrivChatEVT.chatEVTDatas.Find(el => el.Name == filename);
                    msg.Msg = $"Wyswietl wiadomosci#{filename}%{UserAccount.User}";
                    ShowMessages();
                    msg.MsgsShowed.WaitOne();
                    msg.MsgsShowed.Reset();
                    msg.MsgsShowed.WaitOne(1000);
                }
            }
        }

        private void ShowMessages()
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
                usserChatData.MsgsShowed.Set();
            }

            usserChatData.MsgsShowed.Set();
        }

        private void CloseMessageBox(UserPrivateMessageBox pm)
        {
            pm.IsOpen = false;
            stop = true;
            sourceMsgBox.Cancel();
            sourceMsgBox.Dispose();
        }
    }
}