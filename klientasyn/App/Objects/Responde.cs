using System;
using System.Threading;

namespace Klient.App.Objects
{
    class Responde
    {
        public static String komunikat = "";
        public static String contactsKomunikat = "";
        public static ManualResetEvent odebrano =
            new ManualResetEvent(false);
        public static ManualResetEvent contacts =
            new ManualResetEvent(false);
    }
}
