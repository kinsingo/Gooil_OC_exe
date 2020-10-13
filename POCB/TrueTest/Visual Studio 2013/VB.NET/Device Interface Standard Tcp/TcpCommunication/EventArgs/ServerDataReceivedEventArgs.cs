using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace TcpCommunication
{
    public class ServerDataReceivedEventArgs : EventArgs
    {        
        public Byte[] Data { get; set; }
        public TcpClient Client { get; set; }

        public ServerDataReceivedEventArgs(byte[] bytes, TcpClient client)
        {
            Data = bytes;
            Client = client;
        }
    }
}
