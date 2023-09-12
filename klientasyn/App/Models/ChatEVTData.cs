using System.Threading;

namespace Klient.App.Models
{
    class ChatEVTData
    {
        public string Name { get; set; }
        public ManualResetEvent Set = new ManualResetEvent(false);
        public ManualResetEvent MsgsShowed = new ManualResetEvent(false);
        public string Msg { get; set; }
    }
}
