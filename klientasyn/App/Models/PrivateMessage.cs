using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Klient.App.Models
{
    public class PrivateMessage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Konta _user { get; set; }
        private bool _isOpen { get; set; }


        public Konta User
        {
            get { return _user; 
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


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (propertyName != string.Empty)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
