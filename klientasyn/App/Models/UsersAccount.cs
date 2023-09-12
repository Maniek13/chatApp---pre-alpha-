namespace Klient.App.Models
{
    public class UsersAccount
    {
        public string Account { get; set; }
        public string ShowedName { get; set; }
        public bool Status { get; set; } = false;
        public UsersAccount(string user, string nick, bool status)
        {
            Account = user;
            ShowedName = nick;
            Status = status;
        }
    }
}
