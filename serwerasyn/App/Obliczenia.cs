using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace serwer
{
    public class Obliczenia
    {
        private static List<Userspasword> users = new List<Userspasword>();

        public string Start(string wiadomość)
        {
            Wczytano();

            string z = "";

            z = Wiadomość(wiadomość);
            if (z == "")
            {
                z = Wiadomości(wiadomość);
                if (z == "")
                {
                    z = Logowanie(wiadomość);
                    if (z == "")
                    {
                        z = Rejestracja(wiadomość);
                    }
                }
            }

            return z;
        }

        private string Path()
        {
            string path = Application.StartupPath;
            int x = path.IndexOf(@"\bin\Debug", 0, path.Length);
            path = path.Remove(x).Replace(@"\", @"\\");
            return path;
        }

        public void WczytanieKont()
        {
            string[] konta = File.ReadAllLines(Path() + "\\dane\\users.txt"); //nazwa + $ + hasło

            foreach (string line in konta)
            {
                int z = line.IndexOf("$");
                string login = line.Substring(0, z);
                string password = line.Substring(z + 1);
                users.Add(new Userspasword(login, password));
            }
        }
        public void Wczytano()
        {
            if (users.Count() == 0)
            {
                WczytanieKont();
            }

        }
        public string Logowanie(string msg)
        {
            //"LOG"+ Login.Text + "$" + hasło.Text;
            if (msg.StartsWith("LOG"))
            {
                string temp = msg.Substring(3);
                int t = temp.IndexOf("$");
                string password = temp.Substring(t + 1);
                string login = temp.Substring(0, t);

                if (users.Exists(x => x.RegisteredUser == login && x.Pasword == password))
                {
                    string usser_list = "";
                    foreach (Userspasword user in users)
                    {
                        if (user.RegisteredUser != login)
                        {
                            usser_list += user.RegisteredUser + '$';
                        }
                    }
                    return "ok" + usser_list;
                }
                else
                {
                    return "Nie istnieje";
                }
            }
            return "";
        }
        public string Rejestracja(string msg)
        {
            //"REJ"Login.Text + "$" + hasło.Text;
            if (msg.StartsWith("REJ"))
            {
                string temp = msg.Substring(3);
                int t = temp.IndexOf("$");
                string password = temp.Substring(t + 1);
                string login = temp.Substring(0, t);

                if (users.Exists(x => x.RegisteredUser == login && x.Pasword == password))
                {
                    return "Istnieje";
                }
                else
                {
                    users.Add(new Userspasword(login, password));

                    

                    string usser_list = "";
                    foreach (Userspasword user in users)
                    {
                        if (user.RegisteredUser != login)
                        {
                            usser_list += user.RegisteredUser + '$';
                        }
                    }
                    ZapisKont();
                    return "ok" + usser_list;
                }
            }
            else
            {
                return "";
            }
        }

        public string Wiadomość(string msg)
        {
            //"Wiadomosc od:" + login + "#" + wiadomość + "%" + osoba;

            if (msg.StartsWith("Wiadomosc od:"))
            {
                string adresat = "";
                string temp = msg.Substring(13);
                int z = temp.IndexOf("#");
                string login = temp.Substring(0, z);

                temp = temp.Substring(z + 1);

                z = temp.LastIndexOf("%");
                string wiadomość = temp.Substring(0, z);
                temp = temp.Substring(z + 1);

                z = temp.LastIndexOf("&");
                if(z != 0)
                {
                    adresat = temp.Substring(0, z);
                }
                else
                {
                    adresat = "";
                }
                
                string czas = temp.Substring(z + 1);

                FileStream plik = new FileStream(Path() + "\\dane\\wiadomości" + adresat + ".txt", FileMode.OpenOrCreate);

                StreamReader sr = new StreamReader(plik);
                sr.ReadToEnd();

                StreamWriter f = new StreamWriter(plik);
                f.WriteLine(login + "$" + wiadomość + "&" + czas);
                f.Close();
                plik.Close();

                return "ok";
            }
            else
            {
                return "";
            }
        }
        public string Wiadomości(string msg)
        {

            if (msg.StartsWith("Wyswietl wiadomosci"))
            {
                String data = DateTime.Now.ToString();
                return "not implement: " + data;
            }
            else
            {
                return "";
            }
        }
        public void ZapisKont()
        {
            if(users.Count != 0)
            {
               File.Delete(Path() + @"\\dane\\users.txt");

                FileStream plik = new FileStream(Path() + @"\\dane\\users.txt", FileMode.OpenOrCreate);

                StreamWriter f = new StreamWriter(plik);

                while (users.Count != 0)
                {
                    f.WriteLine(users.First().RegisteredUser + "$" + users.First().Pasword);
                    users.RemoveAt(0);
                }

                f.Close();
                plik.Close();
            }   
        }
    }
}
