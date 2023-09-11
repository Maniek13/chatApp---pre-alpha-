using System.Net.Sockets;
using System.Text;

namespace serwer.App.StaticMembers
{
    public class AppDate
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024000;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}
