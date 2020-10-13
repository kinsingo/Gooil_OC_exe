using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Concurrent;

namespace TcpCommunication
{
    public class RvsTcpClient
    {
        private const int bufferSize = 1024;

        public delegate void DataReceivedHandler(object sender, ClientDataReceivedEventArgs e);
        public delegate void LogMessageHandler(object sender, LogEventArgs e);
        public event DataReceivedHandler DataReceived;
        public event LogMessageHandler LogMessage;

        public bool IsShutDown { get; private set; }
        public string Description { get; private set; }

        private TcpClient client;
        private bool TcpInitalized = false;
        private IPAddress ipAddress;
        private int port = 3978;
        private int reconnectionPeriod = 5000;
        private bool reconnecting = false;
        private ThreadPriority threadPriority = ThreadPriority.Normal;
        private BlockingCollection<byte[]> sendQueue;
        private IPacketParser packetParser;

        /// <summary>
        /// Constructs an RvsTcpClient object.
        /// </summary>
        /// <param name="ip">The remote IP address to connect to.</param>
        /// <param name="port">The remote port to connect to.</param>
        /// <param name="reconnectionPeriod">The time interval (in milliseconds) between reconnection attempts if connection is lost.</param>
        public RvsTcpClient(IPAddress ip, int port, int reconnectionPeriod)
            : this(ip, port, reconnectionPeriod, null) { }

        /// <summary>
        /// Constructs an RvsTcpClient object.  
        /// The received stream will be split into packets using the provided IPacketParser.
        /// </summary>
        /// <param name="ip">The remote IP address to connect to.</param>
        /// <param name="port">The remote port to connect to.</param>
        /// <param name="reconnectionPeriod">The time interval (in milliseconds) between reconnection attempts if connection is lost.</param>
        /// <param name="parser">Parser used to split the incoming stream into packets.</param>
        public RvsTcpClient(IPAddress ip, int port, int reconnectionPeriod, IPacketParser parser)
            : this(ip, port, reconnectionPeriod, parser, ThreadPriority.Normal, "") { }

        /// <summary>
        /// Constructs an RvsTcpClient object.  
        /// Passing in a ThreadPriority lower than "Normal" should reduce the CPU usage of the receiving and sending threads.
        /// </summary>
        /// <param name="ip">The remote IP address to connect to.</param>
        /// <param name="port">The remote port to connect to.</param>
        /// <param name="reconnectionPeriod">The time interval (in milliseconds) between reconnection attempts if connection is lost.</param>
        /// <param name="priority">The priority of the listening thread.</param>
        public RvsTcpClient(IPAddress ip, int port, int reconnectionPeriod, ThreadPriority priority) 
            : this(ip, port, reconnectionPeriod, null, priority, "Client") { }

        /// <summary>
        /// Constructs an RvsTcpClient object.  
        /// The received stream will be split into packets using the provided IPacketParser.
        /// Passing in a ThreadPriority lower than "Normal" should reduce the CPU usage of the receiving and sending threads.
        /// </summary>
        /// <param name="ip">The remote IP address to connect to.</param>
        /// <param name="port">The remote port to connect to.</param>
        /// <param name="reconnectionPeriod">The time interval (in milliseconds) between reconnection attempts if connection is lost.</param>
        /// <param name="parser">Parser used to split the incoming stream into packets.</param>
        /// <param name="priority">The priority of the listening thread.</param>
        /// <param name="description">A description for this object.</param>
        public RvsTcpClient(IPAddress ip, int port, int reconnectionPeriod, IPacketParser parser, ThreadPriority priority, string description)
        {
            IsShutDown = false;
            Description = description;
            ipAddress = ip;
            this.port = port;
            this.reconnectionPeriod = reconnectionPeriod;
            packetParser = parser;
            threadPriority = priority;
            sendQueue = new BlockingCollection<byte[]>();
        }

        /// <summary>
        /// Attemps to connect to the remote host and begins listening for incoming data.
        /// </summary>
        /// <returns>Returns true if connection was successful.  Returns false if connection failed.</returns>
        public bool Initialize()
        {
            if (IsShutDown)
            {
                WriteToLog(Description + " is Shutdown in Initialize.  " + Description + " will not be intialized");
                return false;
            }

            try
            {
                //If the client gets closed, client won't be null but its members will be null
                //To properly handle reconnection, we need to see if client.Client is null as well
                if (client == null || client.Client == null || !client.Connected)
                {
                    WriteToLog("Trying to Initialize TCP connection.");
                    if (client == null)
                    {
                        client = new TcpClient(ipAddress.ToString(), port);
                    }
                    else
                    {
                        client.Close();
                        client = new TcpClient(ipAddress.ToString(), port);
                    }

                    ProcessSendQueue();
                    SetupStreamReading();
                    TcpInitalized = true;
                    WriteToLog(Description + " successfully connected.");
                }
            }
            catch (Exception)
            {
                TcpInitalized = false;
                return false;
            }
            return true;
        }

        /// <summary>
        /// Asynchronously attempts to connect to the remote host and begins listening for incoming data.
        /// Will continuously attempt to reconnect until connection succeeds.
        /// </summary>
        public void InitializeAsync()
        {
            Task initTask = new Task( () =>
            {
                while(!Initialize() && !IsShutDown)
                {
                    System.Threading.Thread.Sleep(reconnectionPeriod);
                }
            });

            initTask.Start();
        }

        /// <summary>
        /// Closes the connection to the remote host.
        /// </summary>
        public void Close()
        {
            IsShutDown = true;
            sendQueue.CompleteAdding();

            if (client != null)
            {
                try
                {
                    client.Close();
                }
                catch (Exception) { }

                TcpInitalized = false;
            }
        }

        /// <summary>
        /// Sends data to the remote host.
        /// </summary>
        /// <param name="payload">The data to send to the remote host.</param>
        /// <exception cref="System.InvalidOperationException">Thrown if the client is not connected or was shut down.</exception>
        public void Send(byte[] payload)
        {            
            if (client == null)
            {
                throw new InvalidOperationException("Client is not connected.");
            }

            if (IsShutDown || sendQueue.IsCompleted)
            {
                throw new InvalidOperationException("Client has been shut down");
            }

            if (packetParser != null) { payload = packetParser.PreparePacketForSend(payload); }

            sendQueue.Add(payload);
        }

        protected virtual void OnDataReceived(ClientDataReceivedEventArgs e)
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

        private void ProcessSendQueue()
        {
            Thread sendThread = new Thread(() =>
            {
                foreach (var b in sendQueue.GetConsumingEnumerable())
                {
                    if (client != null && client.Client != null) { client.Client.Send(b); }
                }
            });

            sendThread.Priority = threadPriority;
            sendThread.Name = "TcpClientSendThread";
            sendThread.Start();
        }

        private void WriteToLog(string message)
        {
            OnLogMessage(new LogEventArgs(message));
        }

        private void ReConnectClient()
        {
            if (reconnecting || client == null) 
            {
                return;
            }

            reconnecting = true;
            client.Close();
            WriteToLog("Lost Communications with Server.  Now attempting to reconnect every " + reconnectionPeriod + " seconds.");

            while (!TcpInitalized && !IsShutDown)
            {
                System.Threading.Thread.Sleep(reconnectionPeriod);
                Initialize();
            }

            reconnecting = false;
        }

        private void SetupStreamReading()
        {
            Thread receiveThread = new Thread(() =>
            {
                try
                {
                    while (true)
                    {
                        List<Socket> receiveList = new List<Socket>();
                        receiveList.Add(client.Client);

                        //Every 5 seconds, if we have not received anything, wake up and check the status of the socket
                        Socket.Select(receiveList, null, null, 5 * 1000000);    //Timeout for this function is in microseconds

                        if (receiveList.Count > 0)
                        {
                            //We have received data
                            byte[] bytes = new byte[bufferSize];

                            if (IsShutDown || client.Client == null) { break; }

                            int nbrBytesRead = client.Client.Receive(bytes);

                            if (nbrBytesRead > 0)
                            {
                                try
                                {
                                    if (packetParser != null)
                                    {
                                        //Split up packets here
                                        List<byte[]> packets = packetParser.GetPacketsFromBuffer(bytes, nbrBytesRead);
                                        foreach (var p in packets) { OnDataReceived(new ClientDataReceivedEventArgs(p, ((IPEndPoint)client.Client.RemoteEndPoint).Address)); }
                                    }
                                    else
                                    {
                                        //Just send the stream data
                                        OnDataReceived(new ClientDataReceivedEventArgs(bytes, ((IPEndPoint)client.Client.RemoteEndPoint).Address));
                                    }
                                }
                                catch (Exception ex) 
                                {
                                    WriteToLog("Exception after raising OnDataReceived: " + ex.Message);
                                }
                            }
                            else
                            {
                                //Select gave us a socket to read, but no data was available.
                                //This indicates that the connection was closed from the other side 
                                client.Close();
                                break;
                            }
                        }
                        else
                        {
                            //We have not received data.  Write zero bytes to the socket to see if we're still connected.
                            //We need to do this to check for ungraceful shutdown or cable issues.
                            client.Client.Send(new byte[] { });
                        }

                        if (IsShutDown || !(client.Connected)) { break; }
                    }

                    if (!IsShutDown && (client.Client == null || !client.Connected))
                    {
                        WriteToLog("Lost connection to server");
                        TcpInitalized = false;
                        ReConnectClient();
                    }
                }
                catch (Exception ex)
                {
                    if (!IsShutDown)
                    {
                        WriteToLog("Caught exception in ReceiveTcpData");
                        WriteToLog(ex.Message);
                        TcpInitalized = false;
                        ReConnectClient();
                    }
                }
            });

            receiveThread.Priority = threadPriority;
            receiveThread.Name = "TcpClientReceiveThread";
            receiveThread.Start();
        }      
    }
}
