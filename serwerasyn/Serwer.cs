using System;
using System.Windows.Forms;
using System.Threading;
using serwer.App.Controllers;
using System.Threading.Tasks;

namespace serwer
{
    public partial class Serwer : Form
    {
        //public Thread wątek;
        //public Thread delete;
        public static ManualResetEvent deleted =
        new ManualResetEvent(false);
        private static bool stop = false;
        public CancellationTokenSource source = new CancellationTokenSource();

        public Serwer()
        {
            InitializeComponent();
        }

        public void Start_Click(object sender, EventArgs e)
        {
            Obliczenia obliczenia = new Obliczenia();
            obliczenia.LoadMsgs();
            obliczenia.WczytanieKont();
            
            CancellationToken token = source.Token;
            TaskFactory factory = new TaskFactory(token);

            factory.StartNew(AsynchronousSocketListener.StartListening, token);
            factory.StartNew(DeleteOldData, token);
            
            textBox1.Text = "Working...";
            /*
            wątek = new Thread(new ThreadStart(AsynchronousSocketListener.StartListening))
            {
                IsBackground = true
            };
            wątek.Start();


            delete = new Thread(new ThreadStart(DeleteOldData))
            {
                IsBackground = true
            };
            delete.Start();
            */
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
            /*
            wątek.Abort();
            delete.Join();
            delete.Abort();
            wątek.Join();
            */
            stop = true;
            source.Cancel();
            source.Dispose();
            Obliczenia obliczenia = new Obliczenia();
            obliczenia.Reset();
            textBox1.Text = "Stoped";
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





