namespace serwer
{
    public class Userspasword
    {
        public string RegisteredUser { get; set; }
        public string Pasword { get; set; }
        public Userspasword(string log, string pass)
        {
            RegisteredUser = log;
            Pasword = pass;
        }
    }
}
