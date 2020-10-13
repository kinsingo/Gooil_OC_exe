using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TcpCommunication
{
    public class LengthPrefixPacketParser : IPacketParser
    {
        private List<byte> packetBuffer = new List<byte>();

        /// <summary>
        /// Takes a buffer of received data and returns the packets contained in the buffer.
        /// Requires packets to be prefixed with their length.  Length must be two bytes and big-endian.
        /// </summary>
        /// <param name="buffer">The buffer containing received data.</param>
        /// <param name="numberBytesRead">The number of bytes that were received.</param>
        /// <returns>Returns a list of packets.</returns>
        public List<byte[]> GetPacketsFromBuffer(byte[] buffer, int numberBytesRead)
        {
            var packets = new List<byte[]>();
            if (buffer.Count(b => b > 0) == 0) return packets;
            
            packetBuffer.AddRange(buffer.Take(numberBytesRead));

            if (packetBuffer.Count > 1)
            {
                int packetLength = (int)((packetBuffer[0] << 8) + packetBuffer[1]);

                while (packetBuffer.Count >= packetLength + 2 && packetLength > 0)
                {
                    //Extract packet from buffer
                    byte[] packet = packetBuffer.GetRange(2, packetLength).ToArray();   //First two bytes are the packet length, so we don't want to include those
                    packets.Add(packet);

                    //Remove packet from buffer
                    packetBuffer = packetBuffer.GetRange(packetLength + 2, packetBuffer.Count - packetLength - 2);

                    packetLength = packetBuffer.Count > 1 ? (int)((packetBuffer[0] << 8) + packetBuffer[1]) : 0;
                }
            }

            return packets;
        }

        /// <summary>
        /// Prefixes the payload with its length.  Length consists of two bytes and is big-endian.  
        /// </summary>
        /// <param name="payload">The payload to be sent.</param>
        /// <returns>Returns a length-prefixed payload.</returns>
        public byte[] PreparePacketForSend(IList<byte> payload)
        {
            byte length2 = (byte)payload.Count;
            byte length1 = (byte)(payload.Count >> 8);
            var messageWithLength = new List<byte>() { length1, length2 };
            messageWithLength.AddRange(payload);
            return messageWithLength.ToArray();
        }
    }
}
