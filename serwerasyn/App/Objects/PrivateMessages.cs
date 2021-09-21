using System.Collections.Generic;

namespace serwer.App.Objects
{
    class PrivateMessages
    {
        public string Name{ get; set;}
        public HashSet<Messages> Messages { get; set; } = new HashSet<Messages>();
    }
}
