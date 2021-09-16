using System.Collections.Generic;

namespace serwer.App.Objects
{
    class PrivateMessages
    {
        public string Name{ get; set;}
        public List<Messages> messages { get; set; } = new List<Messages>();
    }
}
