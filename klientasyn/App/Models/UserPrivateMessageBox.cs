using Klient.App.Helpers;

namespace Klient.App.Models
{
    public class UserPrivateMessageBox : PropertyChange
    {
        private UsersAccount _user { get; set; }
        private bool _isOpen { get; set; }

        public UsersAccount User
        {
            get
            {
                return _user;
            }
            set
            {
                if (value != _user)
                {
                    _user = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (value != _isOpen)
                {
                    _isOpen = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
