using System;
using System.Windows.Forms;
using System.Threading;

namespace serwer
{
    public partial class Serwer : Form
    {
        public Thread wątek;
        public Thread delete;
        public static ManualResetEvent deleted =
        new ManualResetEvent(false);

        public Serwer()
        {
            InitializeComponent();
        }

        public void Start_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Working...";
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

        }
        private void DeleteOldData()
        {
            bool temp = true;

            Obliczenia obl = new Obliczenia();
            obl.DeleteOldMessages();
            deleted.WaitOne();
            deleted.Reset();
            deleted.WaitOne(1000);

            while (temp == true)
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
            Obliczenia obliczenia = new Obliczenia();
            wątek.Abort();
            delete.Abort();
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





