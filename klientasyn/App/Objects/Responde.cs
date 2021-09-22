using System;
using System.Collections.Generic;
using System.Threading;

namespace Klient.App.Objects
{
    class Responde
    {
        public static String msg = "";
        public static String contactsMsg = "";
        public static String comunicatsMsg = "";
        public static ManualResetEvent odebrano =
            new ManualResetEvent(false);
        public static ManualResetEvent contacts =
            new ManualResetEvent(false);
        public static ManualResetEvent comunicats =
           new ManualResetEvent(false);
        public static List<PrivChatEVT> privChat = new List<PrivChatEVT>();
    }
}
