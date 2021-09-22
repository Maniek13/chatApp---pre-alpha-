﻿using System.Net.Sockets;
using System.Text;

namespace serwer.App.Objects
{
    public class StateObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024000;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
    }
}
