using serwer.App.Controllers;
using serwer.App.Helper;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serwer
{
    public partial class Serwer : Form
    {
        public static ManualResetEvent deleted = new ManualResetEvent(false);
        public static ManualResetEvent st = new ManualResetEvent(false);

        private static bool stop = false;
        public CancellationTokenSource source;

        private readonly AsynchronousSocketListener listener;

        public Serwer()
        {
            listener = new AsynchronousSocketListener();
            InitializeComponent();
            StopBtn.Enabled = false;
        }

        public void Start_Click(object sender, EventArgs e)
        {
            ServerHelpers.IsStopedApp = false;
            StopBtn.Enabled = true;
            StartBtn.Enabled = false;
            Obliczenia obliczenia = new Obliczenia();
            obliczenia.LoadMsgs();
            obliczenia.WczytanieKont();

            source = new CancellationTokenSource();
            CancellationToken token = source.Token;


            Task.Factory.StartNew(() =>
            {
                while (true)
                {

                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    listener.StartListening();
                    Thread.Sleep(1000);
                }
            }, token);

            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                    DeleteOldData();
                    Thread.Sleep(1000);
                }
            }, token);


            StatusBox.Text = $"Working on ip: {ServerHelpers.Ip}";
        }

        private void DeleteOldData()
        {
            Obliczenia obl = new Obliczenia();
            obl.DeleteOldMessages();
            deleted.WaitOne();
            deleted.Reset();
            deleted.WaitOne(1000);

            while (stop == false)
            {
                if (DateTime.Now.Second % 10 == 0)
                {
                    obl.DeleteOldMessages();
                    deleted.WaitOne();
                    deleted.Reset();
                    deleted.WaitOne(1000);
                }
            }
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            stop = true;
            source.Cancel();
            source.Dispose();

            ServerHelpers.IsStopedApp = true;

            Obliczenia obliczenia = new Obliczenia();
            obliczenia.Reset();
            StatusBox.Text = "Stoped";
            StopBtn.Enabled = false;
            StartBtn.Enabled = true;
        }
    }
}





