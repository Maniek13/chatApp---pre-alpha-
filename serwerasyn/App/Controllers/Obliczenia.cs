using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using serwer.App.Objects;

namespace serwer.App.Controllers
{
    public class Obliczenia
    {
        private static readonly List<Userspasword> users = new List<Userspasword>();
        private static readonly List<Usser> activeUsers = new List<Usser>();
        private static readonly List<Messages> messages = new List<Messages>();

        public void Reset()
        {
            users.Clear();
            activeUsers.Clear();
            messages.Clear();
        }

        public async Task<string> Start(string wiadomość)
        {

            string z;

            z = Wiadomość(wiadomość);
            if (z == "")
            {
                z = Wiadomości(wiadomość);
                if (z == "")
                {
                    z = ActiveUssers(wiadomość);
                    if (z == "")
                    {
                        z = Logowanie(wiadomość);
                        if (z == "")
                        {
                            z = await Rejestracja(wiadomość);
                        }
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
            /* dodawanie do pliku
            string[] konta = File.ReadAllLines(Path() + "\\dane\\ussers\\ussers.txt"); //nazwa + $ + hasło

            foreach (string line in konta)
            {
                int z = line.IndexOf("$");
                string login = line.Substring(0, z);
                string password = line.Substring(z + 1);
                users.Add(new Userspasword(login, password));
            }

            */
            UsserController usserController = new UsserController();
            List<Models.Usser> ussers = usserController.FindUsser();

            ussers.ForEach(delegate (Models.Usser el)
            {
                users.Add(new Userspasword(el.Name, el.Password));
            });
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
                    if(!activeUsers.Exists(el => el.Name == login))
                    {
                        activeUsers.Add(new Usser { Name = login, Time = DateTime.Now });
                    }

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
        public async Task<string> Rejestracja(string msg)
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
                    UsserController usr = new UsserController();
                    int ok = await usr.AddUsser(new Models.Usser{Name = login, Password = password });

                    if(ok == 1)
                    {
                        users.Add(new Userspasword(login, password));

                        if (!activeUsers.Exists(el => el.Name == login))
                        {
                            activeUsers.Add(new Usser { Name = login, Time = DateTime.Now });
                        }

                        string usser_list = "";
                        foreach (Userspasword user in users)
                        {
                            if (user.RegisteredUser != login)
                            {
                                usser_list += user.RegisteredUser + '$';
                            }
                        }


                        //   ZapisKont();  save data to file

                        return "ok" + usser_list;
                    }
                    else
                    {
                        return "";
                    }
                   
                }
            }
            else
            {
                return "";
            }
        }

        public string ActiveUssers(string msg)
        {
            //"Active ussersLOGIN";

            if (msg.StartsWith("Active ussers"))
            {
                string login = msg.Substring(13);

                string usser_list = "";

                Usser who = activeUsers.Find(el => el.Name == login);
                who.Time = DateTime.Now;


                foreach (Usser usser in activeUsers)
                {
                    if (usser.Name != login)
                    {
                        if (usser.Time < DateTime.Now.AddSeconds(-30))
                        {
                            activeUsers.Remove(usser);
                        }
                        else
                        {
                            usser_list += usser.Name + '$';
                        }
                    }
                }

                return "ok" + usser_list;
            }
            else
            {
                return "";
            }

        }

        public string Wiadomość(string msg)
        {
            //"Wiadomosc od:" + login + "#" + wiadomość + "%" + osoba;
            bool priv = false;
            if (msg.StartsWith("Wiadomosc od:"))
            {
                string temp;
                if (msg.StartsWith("Wiadomosc od:Priv"))
                {
                    temp = msg.Substring(17);
                    priv = true;
                }
                else
                {
                    temp = msg.Substring(13);
                }

                string adresat;

                int z = temp.IndexOf("#");
                string login = temp.Substring(0, z);

                temp = temp.Substring(z + 1);

                z = temp.LastIndexOf("%");
                string wiadomość = temp.Substring(0, z);
                temp = temp.Substring(z + 1);

                z = temp.LastIndexOf("&");
                if (z != 0)
                {
                    adresat = temp.Substring(0, z);
                }
                else
                {
                    adresat = "";
                }

                string czas = temp.Substring(z + 1);

                
                if (priv != true)
                {
                    //plik = new FileStream(Path() + "\\dane\\messages\\public.txt", FileMode.OpenOrCreate);

                    activeUsers.ForEach(delegate(Usser us){
                        messages.Add(new Messages { Showed = false, Text = wiadomość, From = login, To = adresat, Login = us.Name, Date = Convert.ToDateTime(czas) });
                    });
                    
                }
                else
                {
                    FileStream plik;
                    plik = new FileStream(Path() + "\\dane\\messages\\" + adresat + ".txt", FileMode.OpenOrCreate);
                    StreamReader sr = new StreamReader(plik);
                    sr.ReadToEnd();

                    StreamWriter f = new StreamWriter(plik);
                    f.WriteLine(login + "$" + adresat + "#" + wiadomość + "&" + czas);
                    f.Close();
                    plik.Close();
                }

               

                return "ok";
            }
            else
            {
                return "";
            }
        }
        public string Wiadomości(string msg)
        {
            string odp = "";

            if (msg.StartsWith("Wyswietl wiadomosci"))
            {
                String date = DateTime.Now.ToString();
                if (msg.StartsWith("Wyswietl wiadomosciFirst"))
                {
                    return "not implement jet";
                }
                else
                {
                    string login = msg.Substring(19);

                    var temp2 = messages.FindAll(el => el.Login == login);
                   
                    temp2.ForEach(delegate (Messages message)
                    {
                        if(message.Showed == false)
                        {
                            string oneMsg;
                            if (message.To != "")
                            {
                                oneMsg = message.From + " do " + message.To + ": ";
                            }
                            else
                            {
                                oneMsg = message.From + ": ";
                            }

                            oneMsg += message.Text;
                            odp += message.Date.ToString("d/M/yy H:ss") + " " + oneMsg + Environment.NewLine;
                            message.Showed = true;
                        }
                    });

                    /* save public msgs
                    var usserMessage = usserMessages.Find(el => el.Name == login);
                    var dt = usserMessage.Time;

                    string[] msgs = File.ReadAllLines(Path() + "\\dane\\messages\\public.txt");

                    foreach (string line in msgs)
                    {
                        int dateIndex = line.LastIndexOf("&");
                        string msgDate = line.Substring(dateIndex + 1);
                        DateTime temp = Convert.ToDateTime(msgDate);
                        int lenght;

                        if (temp.AddSeconds(1) > dt)
                        {
                            int dateIndexLast = line.LastIndexOf("#");
                            lenght = dateIndex - dateIndexLast;
                            string msgOne = line.Substring(dateIndexLast + 1, lenght - 1);
                            int toIndex = line.LastIndexOf("$");
                            lenght = dateIndexLast - toIndex;
                            string to = line.Substring(toIndex + 1, lenght - 1);
                            string from = line.Substring(0, toIndex);

                            string oneMsg;
                            if (to != "")
                            {
                                oneMsg = from + " do " + to + ": ";
                            }
                            else
                            {
                                oneMsg = from + ": ";
                            }

                            oneMsg += msgOne;
                            odp += temp.ToString("d/M/yy H:ss") + " " + oneMsg + Environment.NewLine;
                        }
                    }
                    usserMessages.Find(el => el.Name == login).Time = DateTime.Now;
                    */

                    return odp;
                }
            }
            else
            {
                return "";
            }
        }
        
        public void ZapisKont()
        {
            if (users.Count != 0)
            {
                File.Delete(Path() + @"\\dane\\ussers\\ussers.txt");

                FileStream plik = new FileStream(Path() + @"\\dane\\ussers\\ussers.txt", FileMode.OpenOrCreate);

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
        

        public void DeleteOldMessages()
        {
            /* delete from public textfile
            var path = Path() + "\\dane\\messages\\public.txt";
            string[] msgs = File.ReadAllLines(path);
            String date = DateTime.Now.ToString();
            File.Delete(path);

            FileStream plik = new FileStream(path, FileMode.OpenOrCreate);
            StreamWriter newFile = new StreamWriter(plik);

            foreach (string line in msgs)
            {
                int dateIndex = line.LastIndexOf("&");
                string msgDate = line.Substring(dateIndex + 1);
                DateTime temp = Convert.ToDateTime(msgDate);

                if (temp.AddSeconds(-30) > Convert.ToDateTime(date))
                {
                    newFile.WriteLine(line);
                }
            }
            newFile.Close();
            plik.Close();
            */

            messages.RemoveAll(el => el.Showed == true);
            Serwer.deleted.Set();
        }
    }
}
