using System.Net.Sockets;
using System.Text;

namespace Klient.App.Objects
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1048000;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}
