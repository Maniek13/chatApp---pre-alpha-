using System;
using System.Windows.Forms;
using System.Threading;
using serwer.App.Controllers;
using System.Threading.Tasks;
using serwer.App.Helper;

namespace serwer
{
    public partial class Serwer : Form
    {
        //public Thread wątek;
        //public Thread delete;
        public static ManualResetEvent deleted = new ManualResetEvent(false);
        public static ManualResetEvent st = new ManualResetEvent(false);

        private static bool stop = false;
        public CancellationTokenSource source;

        AsynchronousSocketListener listener;

        public Serwer()
        {
            listener = new AsynchronousSocketListener();
            InitializeComponent();
        }

        public void Start_Click(object sender, EventArgs e)
        {
            ServerHelpers.IsStopedApp = false;
            Stop.Enabled = true;
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
                }
            }, token);


            textBox1.Text = $"Working on ip: {ServerHelpers.Ip}";
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

        private void Form1_Load(object sender, EventArgs e)
        {   
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            stop = true;
            source.Cancel();
            source.Dispose();

            ServerHelpers.IsStopedApp = true;

            Obliczenia obliczenia = new Obliczenia();
            obliczenia.Reset();
            textBox1.Text = "Stoped";
            Stop.Enabled = false;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
        }
    }
}





