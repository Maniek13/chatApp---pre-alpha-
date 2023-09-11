using serwer.App.DbControllers;
using serwer.App.Helper;
using serwer.App.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace serwer.App.Controllers
{
    public class Obliczenia
    {
        private static readonly HashSet<Userspasword> users = new HashSet<Userspasword>();
        private static readonly HashSet<Usser> activeUsers = new HashSet<Usser>();
        private static readonly HashSet<Messages> messages = new HashSet<Messages>();
        private static readonly HashSet<PrivateMessages> privateMessages = new HashSet<PrivateMessages>();

        public void Reset()
        {
            users.Clear();
            activeUsers.Clear();
            messages.Clear();
        }

        public async Task<string> Start(string wiadomość)
        {
            if (ServerHelpers.IsStopedApp)
            {
                return "connection problem";
            }

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
            UsserController usserController = new UsserController();


            List<Models.Usser> ussers = usserController.FindAllUssers().Select( el => Converter.ConvertToUser(el)).ToList();


            if(ussers != null)
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

                if (users.FirstOrDefault(x => x.RegisteredUser == login && x.Pasword == password) != null)
                {
                    return $"ok{UsserListLoginRegister(login)}";
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

                if (users.FirstOrDefault(x => x.RegisteredUser == login && x.Pasword == password) != null)
                {
                    return "Istnieje";
                }
                else
                {
                    UsserController usr = new UsserController();
                    int ok = await usr.AddUsser(new DbModels.Usser{Name = login, Password = password });

                    if(ok == 1)
                    {
                        users.Add(new Userspasword(login, password));
                        return $"ok{UsserListLoginRegister(login)}";
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

        public string UsserListLoginRegister(string login)
        {
            string usser_list = "";

            if (activeUsers.FirstOrDefault(el => el.Name == login) == null)
            {
                activeUsers.Add(new Usser { Name = login, Time = DateTime.Now });
            }

            foreach (Userspasword user in users)
            {
                if (user.RegisteredUser != login)
                {
                    usser_list += $"{user.RegisteredUser}$";
                }
            }

            return usser_list;
        }

        public string ActiveUssers(string msg)
        {
            //"Active ussersLOGIN";
            if (msg.StartsWith("Active ussers"))
            {
                string login = msg.Substring(13);

                string usser_list = "";

                Usser who = activeUsers.FirstOrDefault(el => el.Name == login);
                if (who == null)
                    return "reset client";

                who.Time = DateTime.Now;


                foreach (Usser usser in activeUsers)
                {
                    if (String.Compare(usser.Name, login) != 0)
                    {
                        if (usser.Time < DateTime.Now.AddSeconds(-30))
                        {
                            activeUsers.Remove(usser);
                        }
                        else
                        {
                            usser_list += ($"{usser.Name}$");
                        }
                    }
                }

                return $"ok{usser_list}";
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
                    foreach(Usser us in activeUsers)
                    {
                        messages.Add(new Messages { Showed = false, Text = wiadomość, From = login, To = adresat, Login = us.Name, Date = Convert.ToDateTime(czas) });
                    }
                    
                }
                else
                {
                    FileStream plik;
                    List<string> ussers = new List<string> { adresat, login };

                    ussers.Sort(delegate (string x, string y)
                    {
                        return x.CompareTo(y);
                    });


                    plik = new FileStream(Path() + $"\\dane\\messages\\{ussers[0]}{ussers[1]}.txt", FileMode.OpenOrCreate);
                    StreamReader sr = new StreamReader(plik);
                    sr.ReadToEnd();

                    StreamWriter f = new StreamWriter(plik);
                    f.WriteLine($"{login}${adresat}#{wiadomość}&{czas}");
                    f.Close();
                    plik.Close();

                    if (privateMessages.FirstOrDefault(el => el.Name == ussers[0] + ussers[1]) == null)
                    {
                        privateMessages.Add(new PrivateMessages { Name = ussers[0] + ussers[1] });
                    }

                    privateMessages.FirstOrDefault(el => el.Name == ussers[0] + ussers[1]).Messages.Add(new Messages { Showed = false, Text = wiadomość, From = login, To = adresat, Login = login, Date = Convert.ToDateTime(czas) });
                    privateMessages.FirstOrDefault(el => el.Name == ussers[0] + ussers[1]).Messages.Add(new Messages { Showed = false, Text = wiadomość, From = login, To = adresat, Login = adresat, Date = Convert.ToDateTime(czas) });
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
            if (msg.StartsWith("Wyswietl wiadomosci"))
            {
                String date = DateTime.Now.ToString();

                if (msg.StartsWith("Wyswietl wiadomosci#")) //Wyswietl wiadomosci#filename%login
                {
                    string temp = msg.Substring(20);
                    int indexOfPercent = temp.IndexOf("%");

                    string FileName = temp.Substring(0, indexOfPercent);
                    string Login = temp.Substring(indexOfPercent + 1);

                    var msgs = privateMessages.FirstOrDefault(el => el.Name == FileName);

                    if(msgs == null)
                    {
                        return "0";
                    }
                    return MessagesToString(msgs.Messages.Where(m => m.Login == Login).ToHashSet());
                }
                else
                {
                    string login = msg.Substring(19);
                    var msgs = messages.Where(el => el.Login == login).ToHashSet<Messages>();
                    return MessagesToString(msgs);
                }
            }
            else
            {
                return "";
            }
        }

        private static string MessagesToString(HashSet<Messages> messages)
        {
            string odp = "";

            foreach(Messages message in messages)
            {
                if (message.Showed == false)
                {
                    string oneMsg;
                    if (String.Compare(message.To, "") != 0)
                    {
                        oneMsg = $"{message.From} do {message.To}: ";
                    }
                    else
                    {
                        oneMsg = $"{message.From}: ";
                    }

                    oneMsg += message.Text;
                    odp += $"{message.Date.ToString("d/M/yy H:mm")} {oneMsg}{Environment.NewLine}";
                    message.Showed = true;
                }
            }
            return odp == "" ? "0" : odp;
        }

        public void DeleteOldMessages()
        {
            messages.RemoveWhere(el => el.Showed == true);
            Serwer.deleted.Set();
        }

        public void LoadMsgs()
        {
            var path = Path() + "\\dane\\messages\\";
            Directory.GetFiles(path);
            List<string> files = Directory.GetFiles(path).ToList<string>();

            foreach (string file in files)
            {
                int index = file.LastIndexOf("\\");
                int nameLenght = file.Length - 5 - index;
                string fileName = file.Substring(index+1, nameLenght);
                string[] msgs = File.ReadAllLines(file);
                privateMessages.Add(new PrivateMessages { Name = fileName });

                foreach (string line in msgs)
                {
                    int dateIndex = line.LastIndexOf("&");
                    string msgDate = line.Substring(dateIndex + 1);
                    DateTime temp = Convert.ToDateTime(msgDate);
                    int lenght;

                    int dateIndexLast = line.LastIndexOf("#");
                    lenght = dateIndex - dateIndexLast;
                    string msgOne = line.Substring(dateIndexLast + 1, lenght - 1);
                    int toIndex = line.LastIndexOf("$");
                    lenght = dateIndexLast - toIndex;
                    string to = line.Substring(toIndex + 1, lenght - 1);
                    string from = line.Substring(0, toIndex);

                    privateMessages.FirstOrDefault(el => el.Name == fileName).Messages.Add(new Messages { Showed = false, Text = msgOne, From = from, To = to, Login = from, Date = temp });
                    privateMessages.FirstOrDefault(el => el.Name == fileName).Messages.Add(new Messages { Showed = false, Text = msgOne, From = from, To = to, Login = to, Date = temp });
                }

            }
        }

    }
}
