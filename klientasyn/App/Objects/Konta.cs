namespace Klient.App.Objects
{
    public class Konta
    {
        public string Kontakt { get; set; }
        public string Nazwa { get; set; }
        public bool Status { get; set; } = false;
        public Konta(string user, string nick, bool status)
        {
            Kontakt = user;
            Nazwa = nick;
            Status = status;
        }
    }
}
