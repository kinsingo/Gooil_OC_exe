using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace TcpCommunication
{
    public class ClientDataReceivedEventArgs : EventArgs
    {
        public Byte[] Data { get; set; }
        public IPAddress RemoteIpAddress { get; set; }

        public ClientDataReceivedEventArgs(byte[] bytes, IPAddress remoteIp)
        {
            Data = bytes;
            RemoteIpAddress = remoteIp;
        }
    }
}
