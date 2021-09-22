using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Klient.App.Objects
{
    class ChatEVTData
    {
        public string Name { get; set; }
        public ManualResetEvent Set = new ManualResetEvent(false);
        public ManualResetEvent msgsShowed = new ManualResetEvent(false);
        public string Msg { get; set; }
    }
}
