namespace Klient
{
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
