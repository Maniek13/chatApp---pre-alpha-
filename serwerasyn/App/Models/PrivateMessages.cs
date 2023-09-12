using System.Collections.Generic;

namespace serwer.App.Models
{
    class PrivateMessages
    {
        public string Name { get; set; }
        public List<Messages> Messages { get; set; } = new List<Messages>();
    }
}
