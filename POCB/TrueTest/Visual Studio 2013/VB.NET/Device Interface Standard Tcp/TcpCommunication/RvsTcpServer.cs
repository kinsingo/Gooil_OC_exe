using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Threading;

namespace TcpCommunication
{
    public class RvsTcpServer
    {
        private const int bufferSize = 1024;

        public delegate void DataReceivedHandler(object sender, ServerDataReceivedEventArgs e);
        public delegate void LogMessageHandler(object sender, LogEventArgs e);
        public event DataReceivedHandler DataReceived;
        public event LogMessageHandler LogMessage;

        private TcpListener listener;
        private CancellationTokenSource tokenSource;
        private CancellationToken cancelToken;
        private ThreadPriority priority;
        private IPacketParser packetParser;

        /// <summary>
        /// Constructs an RvsTcpServer object.
        /// </summary>
        /// <param name="ip">The local IP address of the server.</param>
        /// <param name="port">The local port of the server.</param>
        public RvsTcpServer(IPAddress ip, int port)
            : this(ip, port, null, ThreadPriority.Normal) { }

        /// <summary>
        /// Constructs an RvsTcpServer object.
        /// The received stream will be split into packets using the provided IPacketParser.
        /// </summary>
        /// <param name="ip">The local IP address of the server.</param>
        /// <param name="port">The local port of the server.</param>
        /// <param name="parser">Parser used to split the incoming stream into packets.</param>
        public RvsTcpServer(IPAddress ip, int port, IPacketParser parser)
            : this(ip, port, parser, ThreadPriority.Normal) { }

        /// <summary>
        /// Constructs an RvsTcpServer object.
        /// The received stream will be split into packets using the provided IPacketParser.
        /// Passing in a ThreadPriority lower than "Normal" should reduce the CPU usage of the receiving thread.
        /// </summary>
        /// <param name="ip">The local IP address of the server.</param>
        /// <param name="port">The local port of the server.</param>
        /// <param name="parser">Parser used to split the incoming stream into packets.</param>
        /// <param name="priority">The priority of the listening thread.</param>
        public RvsTcpServer(IPAddress ip, int port, IPacketParser parser, ThreadPriority priority)
        {
            listener = new TcpListener(ip, port);
            packetParser = parser;
            this.priority = priority;
        }

        /// <summary>
        /// Starts accepting clients and listening for data.
        /// </summary>
        public void Start()
        {
            tokenSource = new CancellationTokenSource();

            try
            {
                cancelToken = tokenSource.Token;
                StartListener();
            }
            catch(Exception e)
            {
                WriteToLog("Error: " + e.Message);
            }
        }

        /// <summary>
        /// Stops the server from receiving clients and listening for data.
        /// </summary>
        public void Stop()
        {
            WriteToLog("Shutting down server " + ((IPEndPoint)(listener.LocalEndpoint)).Address.ToString());

            if (tokenSource != null && !tokenSource.IsCancellationRequested) 
            { 
                tokenSource.Cancel();
                tokenSource.Dispose();
            }

            if (listener != null) { listener.Stop(); }          
        }

        /// <summary>
        /// Sends data to a remote host.
        /// </summary>
        /// <param name="response">The data to send to the remote host.</param>
        /// <param name="client">The remote host to send the response to.</param>
        public void SendResponse(byte[] response, TcpClient client)
        {
            lock (client)
            {
                if (packetParser != null) { response = packetParser.PreparePacketForSend(response); }
                client.Client.Send(response);
            }
        }

        protected virtual void OnDataReceived(ServerDataReceivedEventArgs e)
        {
            DataReceivedHandler handler = DataReceived;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnLogMessage(LogEventArgs e)
        {
            LogMessageHandler handler = LogMessage;

            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void WriteToLog(string message)
        {
            OnLogMessage(new LogEventArgs(message));
        }

        private void StartListener()
        {
            try
            {
                listener.Start();
                WriteToLog("Started server " + ((IPEndPoint)(listener.LocalEndpoint)).Address.ToString());

                Thread acceptClientsThread = new Thread(DoBeginAcceptTcpClient);
                acceptClientsThread.Priority = priority;
                acceptClientsThread.Start();
            }
            catch(Exception e)
            {
                WriteToLog("Exception: " + e.Message);
                Stop();
            }
        }

        private void DoBeginAcceptTcpClient()
        {
            try
            {
                while (true)
                {
                    if (cancelToken.IsCancellationRequested) return;

                    TcpClient client = listener.AcceptTcpClient();
                    WriteToLog("Connected to client " + ((IPEndPoint)client.Client.RemoteEndPoint).Address);
                    ReadDataFromClient(client);
                }
            }
            catch(SocketException ex)
            {
                //AcceptTcpClient throws an exception when the blocking call is canceled.
                //This is normal behavior, so there's no need to log that exception.
                //Need to consider alternative solution where we check for pending clients before calling AcceptTcpClient.
                if (ex.SocketErrorCode != SocketError.Interrupted) { WriteToLog("Caught exception in AcceptTcpClient: " + ex.Message); }
            }
        }

        private void ReadDataFromClient(TcpClient acceptedClient)
        {
            WriteToLog("Reading from socket.");

            Thread receiveThread = new Thread( () => ReceiveTcpData(acceptedClient));
            receiveThread.Priority = priority;
            receiveThread.Start();
        }

        private void ReceiveTcpData(TcpClient acceptedClient)
        {
            try
            {
                while(true)
                {
                    if (cancelToken.IsCancellationRequested)
                    {
                        acceptedClient.Close();
                        return;
                    }

                    List<Socket> receivingSockets = new List<Socket>();
                    receivingSockets.Add(acceptedClient.Client);

                    //Timeout for this function is in microseconds
                    //Wait for 5 seconds without receiving any data before checking to see if we're still connected
                    Socket.Select(receivingSockets, null, null, 5 * 1000000);

                    if (receivingSockets.Count > 0)
                    {
                        byte[] bytes = new byte[bufferSize];

                        int numberBytesRead = acceptedClient.Client.Receive(bytes);

                        if (numberBytesRead > 0)
                        {
                            try
                            {
                                if (packetParser != null)
                                {
                                    //Split up packets here
                                    List<byte[]> packets = packetParser.GetPacketsFromBuffer(bytes, numberBytesRead);
                                    foreach (var p in packets) { OnDataReceived(new ServerDataReceivedEventArgs(p, acceptedClient)); }
                                }
                                else
                                {
                                    //Just send the stream data
                                    OnDataReceived(new ServerDataReceivedEventArgs(bytes, acceptedClient));
                                }
                            }
                            catch (Exception ex)
                            {
                                WriteToLog("Caught exception from OnDataReceived event handler: " + ex.Message);
                            }
                        }
                        else
                        {
                            //Select gave us a socket to read from but there was no data available
                            //This indicates that the connection was closed from the other side
                            acceptedClient.Close();
                            return;
                        }
                    }
                    else
                    {
                        //We have not received data.  Check the socket.  Write zero bytes to the socket to see if we're still connected.
                        //We need to do this to check for ungraceful shutdown or cable issues.
                        acceptedClient.Client.Send(new byte[] { });
                    }

                    if (!(acceptedClient.Connected))
                    {
                        //Client disconnected.  Stop the receive loop.
                        acceptedClient.Close();
                        return;
                    }
                }
            }
            catch (SocketException se)
            {
                WriteToLog("Socket error: " + se.Message);
                return;
            }
            catch (Exception ex)
            {
                WriteToLog("Error: " + ex.Message);
                return;
            }
        }
    }
}
