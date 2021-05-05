﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Configuration;
using System.Diagnostics;

namespace Klient
{


    public partial class KlientAplikacja : Form
    {

       
        public static TcpClient tcpclient = new TcpClient();
        public static ManualResetEvent wyswietlono =
             new ManualResetEvent(false);

     /* Dodawanie kont
        private KlientLogowanie _client;

        public KlientAplikacja(KlientLogowanie client)
        {
            this._client = client;
            InitializeComponent();
           
        }
     */
     
        public KlientAplikacja()
        {
            InitializeComponent();
        }

        private void WyświetlKontakty()
        {
            foreach (Konta Konta in KlientLogowanie.users)
            {
                kontakty.Items.Add(Konta.Nazwa);
            }
            kontakty.EndUpdate();
        }

        public void DodajKontakt(string nick)
        {
            kontakty.BeginUpdate();
            kontakty.Items.Add(nick);
            kontakty.EndUpdate();
        }


        private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void Wyślij_Click(object sender, EventArgs e)
        {
            Wiadomość();

        }
     
        private void Kontakty_SelectedIndexChanged(object sender, EventArgs e)
        {

            
        }

        private void Komunikaty_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            WyświetlKontakty();
            Thread msg = new Thread(new ThreadStart(WyświetlWiadomosći));
            msg.IsBackground = true;
            msg.Start();
            //dodawanie kont
            Dodaj.Hide();
        }   

        private void Komunikaty_TextChanged(object sender, EventArgs e)
        {
 
        }

        private void wiadomosc_TextChanged(object sender, EventArgs e)
        {

        }
        private void WyświetlWiadomosći()
        {
            bool temp = true;
            while( temp == true)
            {
                if(DateTime.Now.Second % 3  == 0)
                {   
                    Wiadomości();
                    wyswietlono.WaitOne();
                    wyswietlono.Reset();
                    wyswietlono.WaitOne(1000);                  
                }
            }
        
        }
        private void Wiadomości()
        { 
            KlientLogowanie.komunikat = "Wyswietl wiadomosci";
            KlientLogowanie.odebrano.Reset();

            Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient));
            wątek.IsBackground = true;
            wątek.Start();

            KlientLogowanie.odebrano.WaitOne();
            
            try
            {
                if (!this.IsDisposed)
                {
                    Invoke(new Action(() =>
                    {
                    if (KlientLogowanie.komunikat != "")
                    {
                        Komunikaty.AppendText(KlientLogowanie.komunikat + Environment.NewLine);
                    }
                    }));
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (InvalidOperationException)
            {

            }
            wyswietlono.Set();
        }
        private void Wiadomość()
        {
            string login = KlientLogowanie.Osoba;

            if (kontakty.SelectedIndex >= 0)
            {
                string osoba = kontakty.SelectedItem.ToString();

                Konta temp = KlientLogowanie.users.Find(x => x.Nazwa.Contains(osoba));

                string wiadomość = wiadomosc.Text;
                KlientLogowanie.odebrano.Reset();

                KlientLogowanie.komunikat = "Wiadomosc od:" + login + "#" + wiadomość + "%" + temp.Kontakt;
                Thread wątek = new Thread(new ThreadStart(AsynchronousClient.StartClient));
                wątek.IsBackground = true;
                wątek.Start();
                KlientLogowanie.odebrano.WaitOne();


                try
                {
                    if (!this.IsDisposed)
                    {
                        Invoke(new Action(() =>
                        {
                            Komunikaty.AppendText(KlientLogowanie.komunikat + Environment.NewLine);
                        }));
                    }
                }
                catch (InvalidOperationException)
                {

                }



                wiadomosc.Text = "";
            }
            
        }
        private void Dodaj_Click(object sender, EventArgs e)
        {
            NewUser konta = new NewUser(this);
            konta.Show();
  
        }


    
        private void kontakty_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }
    }
    public class Konta
    {
        public string Kontakt { get; set; }
        public string Nazwa { get; set; }
        public Konta(string user, string nick)
        {
            Kontakt = user;

            Nazwa = nick;

        }
    }



    
}
