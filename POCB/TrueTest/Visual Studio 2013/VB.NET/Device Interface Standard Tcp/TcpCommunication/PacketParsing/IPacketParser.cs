using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpCommunication
{
    public interface IPacketParser
    {
        /// <summary>
        /// Takes a buffer of received data and returns the packets contained in the buffer.
        /// </summary>
        /// <param name="buffer">The buffer containing received data.</param>
        /// <param name="numberBytesRead">The number of bytes that were received.</param>
        /// <returns>Returns a list of packets.</returns>
        List<byte[]> GetPacketsFromBuffer(byte[] buffer, int numberBytesRead);

        /// <summary>
        /// Formats a payload to conform to a packet structure before it is sent.
        /// </summary>
        /// <param name="payload">The payload to be sent.</param>
        /// <returns>Returns a formatted payload.</returns>
        byte[] PreparePacketForSend(IList<byte> payload);
    }
}
