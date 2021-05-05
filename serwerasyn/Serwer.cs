using System;
using System.Windows.Forms;
using System.Threading;

namespace serwer
{
    public partial class Serwer : Form
    {
        public Serwer()
        {
            InitializeComponent();
        }

        public Thread wątek;

        public void Start_Click(object sender, EventArgs e)
        {
            textBox1.Text = "Working...";
            wątek = new Thread(new ThreadStart(AsynchronousSocketListener.StartListening));
            wątek.IsBackground = true;
            wątek.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        private void Stop_Click(object sender, EventArgs e)
        {
            Obliczenia obliczenia = new Obliczenia();
            obliczenia.ZapisKont();
            wątek.Abort();
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





