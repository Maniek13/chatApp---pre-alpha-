using System.Collections.Generic;

namespace serwer.App.Objects
{
    class PrivateMessages
    {
        public string Name{ get; set;}
        public List<Messages> Messages { get; set; } = new List<Messages>();
    }
}
