namespace SagaLib
{
    using System;
    using System.Collections.Generic;
    using System.Net.Sockets;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="NetIO" />.
    /// </summary>
    public class NetIO
    {
        /// <summary>
        /// Defines the buffer.
        /// </summary>
        private byte[] buffer = new byte[4];

        /// <summary>
        /// Defines the firstLevelLenth.
        /// </summary>
        private ushort firstLevelLenth = 4;

        /// <summary>
        /// Defines the receiveStamp.
        /// </summary>
        private DateTime receiveStamp = DateTime.Now;

        /// <summary>
        /// Defines the sendStamp.
        /// </summary>
        private DateTime sendStamp = DateTime.Now;

        /// <summary>
        /// Defines the callbackSize.
        /// </summary>
        private AsyncCallback callbackSize;

        /// <summary>
        /// Defines the callbackData.
        /// </summary>
        private AsyncCallback callbackData;

        /// <summary>
        /// Defines the callbackKeyExchange.
        /// </summary>
        private AsyncCallback callbackKeyExchange;

        /// <summary>
        /// Defines the callbackSend.
        /// </summary>
        private AsyncCallback callbackSend;

        /// <summary>
        /// Defines the sock.
        /// </summary>
        public Socket sock;

        /// <summary>
        /// Defines the Crypt.
        /// </summary>
        public Encryption Crypt;

        /// <summary>
        /// Defines the stream.
        /// </summary>
        private NetworkStream stream;

        /// <summary>
        /// Defines the client.
        /// </summary>
        private Client client;

        /// <summary>
        /// Defines the isDisconnected.
        /// </summary>
        private bool isDisconnected;

        /// <summary>
        /// Defines the disconnecting.
        /// </summary>
        private bool disconnecting;

        /// <summary>
        /// Defines the keyAlreadyReceived.
        /// </summary>
        private int keyAlreadyReceived;

        /// <summary>
        /// Defines the lastSize.
        /// </summary>
        private int lastSize;

        /// <summary>
        /// Defines the alreadyReceived.
        /// </summary>
        private int alreadyReceived;

        /// <summary>
        /// Defines the waitCounter.
        /// </summary>
        internal int waitCounter;

        /// <summary>
        /// Defines the receivedBytes.
        /// </summary>
        private int receivedBytes;

        /// <summary>
        /// Defines the sentBytes.
        /// </summary>
        private int sentBytes;

        /// <summary>
        /// Defines the avarageReceive.
        /// </summary>
        private int avarageReceive;

        /// <summary>
        /// Defines the avarageSend.
        /// </summary>
        private int avarageSend;

        /// <summary>
        /// Command table contains the commands that need to be called when a
        /// packet is received. Key will be the packet type.
        /// </summary>
        private Dictionary<ushort, Packet> commandTable;

        /// <summary>
        /// Defines the OnReceivePacket.
        /// </summary>
        public event NetIO.PacketEventArg OnReceivePacket;

        /// <summary>
        /// Defines the OnSendPacket.
        /// </summary>
        public event NetIO.PacketEventArg OnSendPacket;

        /// <summary>
        /// Gets a value indicating whether Disconnected.
        /// </summary>
        public bool Disconnected
        {
            get
            {
                return this.isDisconnected;
            }
        }

        /// <summary>
        /// Gets or sets the FirstLevelLength.
        /// </summary>
        public ushort FirstLevelLength
        {
            get
            {
                return this.firstLevelLenth;
            }
            set
            {
                this.firstLevelLenth = value;
            }
        }

        /// <summary>
        /// Gets the UpStreamBand.
        /// </summary>
        public int UpStreamBand
        {
            get
            {
                return this.avarageSend;
            }
        }

        /// <summary>
        /// Gets the DownStreamBand.
        /// </summary>
        public int DownStreamBand
        {
            get
            {
                return this.avarageReceive;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NetIO"/> class.
        /// </summary>
        /// <param name="sock">The sock<see cref="Socket"/>.</param>
        /// <param name="commandTable">The commandTable<see cref="Dictionary{ushort, Packet}"/>.</param>
        /// <param name="client">The client<see cref="Client"/>.</param>
        public NetIO(Socket sock, Dictionary<ushort, Packet> commandTable, Client client)
        {
            this.sock = sock;
            this.stream = new NetworkStream(sock);
            this.commandTable = commandTable;
            this.client = client;
            this.Crypt = new Encryption();
            this.callbackSize = new AsyncCallback(this.ReceiveSize);
            this.callbackData = new AsyncCallback(this.ReceiveData);
            this.callbackKeyExchange = new AsyncCallback(this.ReceiveKeyExchange);
            this.callbackSend = new AsyncCallback(this.OnSent);
            this.isDisconnected = false;
        }

        /// <summary>
        /// The StartPacketParsing.
        /// </summary>
        private void StartPacketParsing()
        {
            if (this.sock.Connected)
            {
                this.client.OnConnect();
                try
                {
                    this.stream.BeginRead(this.buffer, 0, 4, this.callbackSize, (object)null);
                }
                catch (Exception ex1)
                {
                    Logger.ShowError(ex1, (Logger)null);
                    try
                    {
                        this.Disconnect();
                    }
                    catch (Exception ex2)
                    {
                    }
                    Logger.ShowWarning("Invalid packet head from:" + this.sock.RemoteEndPoint.ToString(), (Logger)null);
                }
            }
            else
                this.Disconnect();
        }

        /// <summary>
        /// The SetMode.
        /// </summary>
        /// <param name="mode">The mode<see cref="NetIO.Mode"/>.</param>
        public void SetMode(NetIO.Mode mode)
        {
            switch (mode)
            {
                case NetIO.Mode.Server:
                    try
                    {
                        byte[] buffer = new byte[8];
                        this.keyAlreadyReceived = 8;
                        this.stream.BeginRead(buffer, 0, 8, this.callbackKeyExchange, (object)buffer);
                        break;
                    }
                    catch (Exception ex)
                    {
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        break;
                    }
                case NetIO.Mode.Client:
                    try
                    {
                        byte[] buffer = new byte[529];
                        int count = this.sock.Available >= 529 ? 529 : this.sock.Available;
                        this.keyAlreadyReceived = count;
                        this.stream.BeginRead(buffer, 0, count, this.callbackKeyExchange, (object)buffer);
                        break;
                    }
                    catch (Exception ex)
                    {
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        break;
                    }
            }
        }

        /// <summary>
        /// The ReceiveKeyExchange.
        /// </summary>
        /// <param name="ar">The ar<see cref="IAsyncResult"/>.</param>
        private void ReceiveKeyExchange(IAsyncResult ar)
        {
            try
            {
                if (this.isDisconnected)
                    return;
                if (!this.sock.Connected)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                }
                else
                {
                    try
                    {
                        this.stream.EndRead(ar);
                    }
                    catch (Exception ex)
                    {
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                    byte[] asyncState = (byte[])ar.AsyncState;
                    if (this.keyAlreadyReceived < asyncState.Length)
                    {
                        int count = asyncState.Length - this.keyAlreadyReceived;
                        if (count > 1024)
                            count = 1024;
                        if (count > this.sock.Available)
                            count = this.sock.Available;
                        try
                        {
                            this.stream.BeginRead(asyncState, this.keyAlreadyReceived, count, this.callbackKeyExchange, (object)asyncState);
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                            ClientManager.EnterCriticalArea();
                            this.Disconnect();
                            ClientManager.LeaveCriticalArea();
                            return;
                        }
                        this.keyAlreadyReceived += count;
                    }
                    else if (asyncState.Length == 8)
                    {
                        Packet p = new Packet(529U);
                        p.PutUInt(1U, (ushort)4);
                        p.PutByte((byte)50, (ushort)8);
                        p.PutUInt(256U, (ushort)9);
                        this.Crypt.MakePrivateKey();
                        string str = Conversions.bytes2HexString(Encryption.Module.getBytes());
                        p.PutBytes(Encoding.ASCII.GetBytes(str.ToLower()), (ushort)13);
                        p.PutUInt(256U, (ushort)269);
                        string s = Conversions.bytes2HexString(this.Crypt.GetKeyExchangeBytes());
                        p.PutBytes(Encoding.ASCII.GetBytes(s), (ushort)273);
                        this.SendPacket(p, true, true);
                        try
                        {
                            byte[] buffer = new byte[260];
                            int count = this.sock.Available >= 260 ? 260 : this.sock.Available;
                            this.keyAlreadyReceived = count;
                            this.stream.BeginRead(buffer, 0, count, this.callbackKeyExchange, (object)buffer);
                        }
                        catch (Exception ex)
                        {
                            ClientManager.EnterCriticalArea();
                            this.Disconnect();
                            ClientManager.LeaveCriticalArea();
                        }
                    }
                    else if (asyncState.Length == 260)
                    {
                        this.Crypt.MakeAESKey(Encoding.ASCII.GetString(new Packet()
                        {
                            data = asyncState
                        }.GetBytes((ushort)256, (ushort)4)));
                        this.StartPacketParsing();
                    }
                    else
                    {
                        if (asyncState.Length != 529)
                            return;
                        byte[] bytes = new Packet()
                        {
                            data = asyncState
                        }.GetBytes((ushort)256, (ushort)273);
                        this.Crypt.MakePrivateKey();
                        Packet p = new Packet(260U);
                        p.PutUInt(256U, (ushort)0);
                        string s = Conversions.bytes2HexString(this.Crypt.GetKeyExchangeBytes());
                        p.PutBytes(Encoding.ASCII.GetBytes(s), (ushort)4);
                        this.SendPacket(p, true, true);
                        this.Crypt.MakeAESKey(Encoding.ASCII.GetString(bytes));
                        this.StartPacketParsing();
                    }
                }
            }
            catch (Exception ex)
            {
                this.Disconnect();
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The Disconnect.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                if (this.isDisconnected)
                    return;
                this.isDisconnected = true;
                try
                {
                    if (!this.disconnecting)
                        this.client.OnDisconnect();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex, (Logger)null);
                }
                this.disconnecting = true;
                try
                {
                    Logger.ShowInfo(this.sock.RemoteEndPoint.ToString() + " disconnected", (Logger)null);
                }
                catch (Exception ex)
                {
                }
                try
                {
                    this.stream.Close();
                }
                catch (Exception ex)
                {
                }
                try
                {
                    this.sock.Close();
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex1)
            {
                Logger.ShowError(ex1, (Logger)null);
                try
                {
                    this.stream.Close();
                }
                catch (Exception ex2)
                {
                }
                try
                {
                    this.sock.Close();
                }
                catch (Exception ex2)
                {
                }
            }
        }

        /// <summary>
        /// The ReceiveSize.
        /// </summary>
        /// <param name="ar">The ar<see cref="IAsyncResult"/>.</param>
        private void ReceiveSize(IAsyncResult ar)
        {
            try
            {
                if (this.isDisconnected)
                    return;
                if (this.buffer[0] == byte.MaxValue)
                {
                    if (this.buffer[1] == byte.MaxValue & this.buffer[2] == byte.MaxValue)
                    {
                        if (this.buffer[3] == byte.MaxValue)
                        {
                            ClientManager.EnterCriticalArea();
                            this.Disconnect();
                            ClientManager.LeaveCriticalArea();
                            return;
                        }
                    }
                }
                try
                {
                    this.stream.EndRead(ar);
                }
                catch (Exception ex)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                Array.Reverse((Array)this.buffer);
                uint num1 = BitConverter.ToUInt32(this.buffer, 0) + 4U;
                if (num1 < 4U)
                {
                    Logger.ShowWarning(this.sock.RemoteEndPoint.ToString() + " error: packet size is < 4", (Logger)null);
                }
                else
                {
                    byte[] buffer = new byte[(num1 + 4U)];
                    this.buffer[0] = byte.MaxValue;
                    this.buffer[1] = byte.MaxValue;
                    this.buffer[2] = byte.MaxValue;
                    this.buffer[3] = byte.MaxValue;
                    this.lastSize = (int)num1;
                    uint num2 = this.sock.Available >= this.lastSize ? (uint)this.lastSize : (uint)this.sock.Available;
                    if (num2 > 1024U)
                    {
                        num2 = 1024U;
                        this.alreadyReceived = 1024;
                    }
                    else
                        this.alreadyReceived = (int)num2;
                    try
                    {
                        this.stream.BeginRead(buffer, 4, (int)num2, this.callbackData, (object)buffer);
                    }
                    catch (Exception ex)
                    {
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, (Logger)null);
            }
        }

        /// <summary>
        /// The ReceiveData.
        /// </summary>
        /// <param name="ar">The ar<see cref="IAsyncResult"/>.</param>
        private void ReceiveData(IAsyncResult ar)
        {
            try
            {
                if (this.isDisconnected)
                    return;
                try
                {
                    this.stream.EndRead(ar);
                }
                catch (Exception ex)
                {
                    ClientManager.EnterCriticalArea();
                    this.Disconnect();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                byte[] asyncState = (byte[])ar.AsyncState;
                if (this.alreadyReceived < this.lastSize && this.lastSize > 0)
                {
                    int count = this.lastSize - this.alreadyReceived;
                    if (count > 1024)
                        count = 1024;
                    if (this.sock.Available == 0)
                    {
                        this.waitCounter = 0;
                        while (this.sock.Available == 0)
                        {
                            if (this.waitCounter > 300)
                            {
                                Logger.ShowWarning("Receive Timeout for client:" + this.client.ToString());
                                ClientManager.EnterCriticalArea();
                                this.Disconnect();
                                ClientManager.LeaveCriticalArea();
                                return;
                            }
                            Thread.Sleep(100);
                            ++this.waitCounter;
                        }
                    }
                    if (count > this.sock.Available)
                        count = this.sock.Available;
                    try
                    {
                        this.stream.BeginRead(asyncState, 4 + this.alreadyReceived, count, this.callbackData, (object)asyncState);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                    this.alreadyReceived += count;
                }
                else
                {
                    byte[] numArray = this.Crypt.Decrypt(asyncState, 8);
                    DateTime now = DateTime.Now;
                    this.receivedBytes += numArray.Length;
                    if ((now - this.receiveStamp).TotalSeconds > 10.0)
                    {
                        this.avarageReceive = (int)((double)this.receivedBytes / (now - this.receiveStamp).TotalSeconds);
                        this.receivedBytes = 0;
                        this.receiveStamp = now;
                    }
                    Packet packet = new Packet();
                    packet.data = numArray;
                    uint num1 = packet.GetUInt((ushort)4);
                    uint num2 = 0;
                    if (num1 > 0U)
                    {
                        if (num1 < 1024000U)
                        {
                            while (num2 < num1)
                            {
                                uint num3 = this.firstLevelLenth != (ushort)4 ? (uint)packet.GetUShort((ushort)(8U + num2)) : packet.GetUInt((ushort)(8U + num2));
                                uint num4 = num2 + (uint)this.firstLevelLenth;
                                if (num3 + num4 <= num1)
                                {
                                    Packet p = new Packet();
                                    p.data = packet.GetBytes((ushort)num3, (ushort)(8U + num4));
                                    num2 = num4 + num3;
                                    this.ProcessPacket(p);
                                }
                                else
                                    break;
                            }
                        }
                    }
                    try
                    {
                        this.stream.BeginRead(this.buffer, 0, 4, this.callbackSize, (object)null);
                    }
                    catch (Exception ex)
                    {
                        ClientManager.EnterCriticalArea();
                        this.Disconnect();
                        ClientManager.LeaveCriticalArea();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, (Logger)null);
            }
        }

        /// <summary>
        /// The ProcessPacket.
        /// </summary>
        /// <param name="p">The p<see cref="Packet"/>.</param>
        private void ProcessPacket(Packet p)
        {
            if (p.data.Length < 2)
                return;
            ClientManager.AddThread(string.Format("PacketParser({0}),Opcode:0x{1:X4}", (object)Thread.CurrentThread.ManagedThreadId, (object)p.ID), Thread.CurrentThread);
            Packet packet1;
            this.commandTable.TryGetValue(p.ID, out packet1);
            if (packet1 != null)
            {
                Packet p1 = packet1.New();
                p1.data = p.data;
                p1.size = (uint)(ushort)p.data.Length;
                ClientManager.EnterCriticalArea();
                try
                {
                    p1.Parse(this.client);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
                if (this.OnSendPacket != null)
                    this.OnReceivePacket(p1);
            }
            else if (this.commandTable.ContainsKey(ushort.MaxValue))
            {
                Packet packet2 = this.commandTable[ushort.MaxValue].New();
                packet2.data = p.data;
                packet2.size = (uint)(ushort)p.data.Length;
                ClientManager.EnterCriticalArea();
                try
                {
                    packet2.Parse(this.client);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
            }
            else
                Logger.ShowDebug(string.Format("Unknown Packet:0x{0:X4}\r\n       Data:{1}", (object)p.ID, (object)this.DumpData(p)), Logger.CurrentLogger);
            ClientManager.RemoveThread(string.Format("PacketParser({0}),Opcode:0x{1:X4}", (object)Thread.CurrentThread.ManagedThreadId, (object)p.ID));
        }

        /// <summary>
        /// The DumpData.
        /// </summary>
        /// <param name="p">The p<see cref="Packet"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string DumpData(Packet p)
        {
            string str = "";
            for (int index = 0; index < p.data.Length; ++index)
            {
                str += string.Format("{0:X2} ", (object)p.data[index]);
                if ((index + 1) % 16 == 0 && index != 0)
                    str += "\r\n            ";
            }
            return str;
        }

        /// <summary>
        /// The SendPacket.
        /// </summary>
        /// <param name="p">The p<see cref="Packet"/>.</param>
        /// <param name="nolength">The nolength<see cref="bool"/>.</param>
        /// <param name="noWarper">The noWarper<see cref="bool"/>.</param>
        public void SendPacket(Packet p, bool nolength, bool noWarper)
        {
            if (this.isDisconnected)
                return;
            if (this.OnSendPacket != null)
                this.OnSendPacket(p);
            if (!noWarper)
            {
                byte[] numArray1 = new byte[p.data.Length + (int)this.firstLevelLenth];
                Array.Copy((Array)p.data, 0, (Array)numArray1, (int)this.firstLevelLenth, p.data.Length);
                p.data = numArray1;
                if (this.firstLevelLenth == (ushort)4)
                    p.SetLength();
                else
                    p.PutUShort((ushort)(p.data.Length - 2), (ushort)0);
                byte[] numArray2 = new byte[p.data.Length + 4];
                Array.Copy((Array)p.data, 0, (Array)numArray2, 4, p.data.Length);
                p.data = numArray2;
                p.SetLength();
                byte[] numArray3 = new byte[p.data.Length + 4];
                Array.Copy((Array)p.data, 0, (Array)numArray3, 4, p.data.Length);
                p.data = numArray3;
            }
            if (!nolength)
            {
                int num = 16 - (p.data.Length - 8) % 16;
                if (num != 0)
                {
                    byte[] numArray = new byte[p.data.Length + num];
                    Array.Copy((Array)p.data, 0, (Array)numArray, 0, p.data.Length);
                    p.data = numArray;
                }
                p.PutUInt((uint)(p.data.Length - 8), (ushort)0);
            }
            this.sentBytes += p.data.Length;
            DateTime now = DateTime.Now;
            if ((now - this.sendStamp).TotalSeconds > 10.0)
            {
                this.avarageSend = (int)((double)this.sentBytes / (now - this.sendStamp).TotalSeconds);
                this.sentBytes = 0;
                this.sendStamp = now;
            }
            try
            {
                byte[] buffer = this.Crypt.Encrypt(p.data, 8);
                this.stream.BeginWrite(buffer, 0, buffer.Length, this.callbackSend, (object)null);
            }
            catch (Exception ex)
            {
                if (this.client == null)
                    return;
                Logger.ShowError(ex);
                this.Disconnect();
                this.client = (Client)null;
            }
        }

        /// <summary>
        /// The OnSent.
        /// </summary>
        /// <param name="ar">The ar<see cref="IAsyncResult"/>.</param>
        private void OnSent(IAsyncResult ar)
        {
            try
            {
                this.stream.EndWrite(ar);
            }
            catch
            {
            }
        }

        /// <summary>
        /// The SendPacket.
        /// </summary>
        /// <param name="p">The p<see cref="Packet"/>.</param>
        /// <param name="noWarper">The noWarper<see cref="bool"/>.</param>
        public void SendPacket(Packet p, bool noWarper)
        {
            this.SendPacket(p, false, noWarper);
        }

        /// <summary>
        /// The SendPacket.
        /// </summary>
        /// <param name="p">The p<see cref="Packet"/>.</param>
        public void SendPacket(Packet p)
        {
            this.SendPacket(p, false);
        }

        /// <summary>
        /// Defines the Mode.
        /// </summary>
        public enum Mode
        {
            /// <summary>
            /// Defines the Server.
            /// </summary>
            Server,

            /// <summary>
            /// Defines the Client.
            /// </summary>
            Client,
        }

        /// <summary>
        /// The PacketEventArg.
        /// </summary>
        /// <param name="p">The p<see cref="Packet"/>.</param>
        public delegate void PacketEventArg(Packet p);
    }
}
