using System;

namespace serwer.App.Models
{
    class Messages
    {
        public string Text { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool Showed { get; set; }
        public string Login { get; set; }
        public DateTime Date { get; set; }
    }
}
