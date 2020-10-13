using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpCommunication
{
    public class LogEventArgs : EventArgs
    {
        public string Message { get; set; }

        public LogEventArgs(string message)
        {
            Message = message;
        }
    }
}
