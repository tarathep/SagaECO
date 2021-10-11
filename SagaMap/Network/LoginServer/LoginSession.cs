namespace SagaMap.Network.LoginServer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Packets.Login;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="LoginSession" />.
    /// </summary>
    public class LoginSession : SagaLib.Client
    {
        /// <summary>
        /// The state of this session. Changes from NOT_IDENTIFIED to IDENTIFIED or REJECTED..
        /// </summary>
        public LoginSession.SESSION_STATE state = LoginSession.SESSION_STATE.CONNECTED;

        /// <summary>
        /// Defines the sock.
        /// </summary>
        private Socket sock;

        /// <summary>
        /// Defines the commandTable.
        /// </summary>
        private Dictionary<ushort, Packet> commandTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginSession"/> class.
        /// </summary>
        public LoginSession()
        {
            this.commandTable = new Dictionary<ushort, Packet>();
            this.commandTable.Add((ushort)65522, (Packet)new INTERN_LOGIN_REQUEST_CONFIG_ANSWER());
            this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Connect();
        }

        /// <summary>
        /// The Connect.
        /// </summary>
        public void Connect()
        {
            int num = 5;
            while (num >= 0)
            {
                bool flag;
                try
                {
                    this.sock.Connect((EndPoint)new IPEndPoint(IPAddress.Parse(Singleton<Configuration>.Instance.LoginHost), Singleton<Configuration>.Instance.LoginPort));
                    flag = true;
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Failed... Trying again in 5sec", (Logger)null);
                    Logger.ShowError(ex.ToString(), (Logger)null);
                    Thread.Sleep(5000);
                    flag = false;
                }
                --num;
                if (flag)
                {
                    Logger.ShowInfo("Successfully connected to the loginserver", (Logger)null);
                    this.state = LoginSession.SESSION_STATE.CONNECTED;
                    try
                    {
                        this.netIO = new NetIO(this.sock, this.commandTable, (SagaLib.Client)this);
                        this.netIO.SetMode(NetIO.Mode.Client);
                        Packet p = new Packet(8U);
                        p.data[7] = (byte)16;
                        this.netIO.SendPacket(p, true, true);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowWarning(ex.StackTrace, (Logger)null);
                    }
                    return;
                }
            }
            Logger.ShowError("Cannot connect to the loginserver,please check the configuration!", (Logger)null);
        }

        /// <summary>
        /// The OnConnect.
        /// </summary>
        public override void OnConnect()
        {
            this.state = LoginSession.SESSION_STATE.NOT_IDENTIFIED;
            int num = Singleton<Configuration>.Instance.HostedMaps.Count / 200;
            for (int index1 = 0; index1 < num; ++index1)
            {
                INTERN_LOGIN_REGISTER internLoginRegister = new INTERN_LOGIN_REGISTER();
                internLoginRegister.Password = Singleton<Configuration>.Instance.LoginPass;
                List<uint> uintList = new List<uint>();
                for (int index2 = index1 * 200; index2 < (index1 + 1) * 200; ++index2)
                    uintList.Add(Singleton<Configuration>.Instance.HostedMaps[index2]);
                internLoginRegister.HostedMaps = uintList;
                this.netIO.SendPacket((Packet)internLoginRegister);
            }
            INTERN_LOGIN_REGISTER internLoginRegister1 = new INTERN_LOGIN_REGISTER();
            internLoginRegister1.Password = Singleton<Configuration>.Instance.LoginPass;
            List<uint> uintList1 = new List<uint>();
            for (int index = num * 200; index < Singleton<Configuration>.Instance.HostedMaps.Count; ++index)
                uintList1.Add(Singleton<Configuration>.Instance.HostedMaps[index]);
            internLoginRegister1.HostedMaps = uintList1;
            this.netIO.SendPacket((Packet)internLoginRegister1);
            this.netIO.SendPacket((Packet)new INTERN_LOGIN_REQUEST_CONFIG()
            {
                Version = Singleton<Configuration>.Instance.Version
            });
        }

        /// <summary>
        /// The OnGetConfig.
        /// </summary>
        /// <param name="p">The p<see cref="INTERN_LOGIN_REQUEST_CONFIG_ANSWER"/>.</param>
        public void OnGetConfig(INTERN_LOGIN_REQUEST_CONFIG_ANSWER p)
        {
            if (p.AuthOK)
            {
                Singleton<Configuration>.Instance.StartupSetting = p.StartupSetting;
                Logger.ShowInfo("Got Configuration from login server:");
                foreach (PC_RACE key in Singleton<Configuration>.Instance.StartupSetting.Keys)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("[Info]");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Configuration for Race[");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(key.ToString());
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("]");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(":\r\n      " + Singleton<Configuration>.Instance.StartupSetting[key].ToString());
                    Console.ResetColor();
                }
                this.state = LoginSession.SESSION_STATE.IDENTIFIED;
            }
            else
            {
                Logger.ShowError("FATAL: Request Rejected from loginserver,terminating");
                this.state = LoginSession.SESSION_STATE.REJECTED;
            }
        }

        /// <summary>
        /// The OnDisconnect.
        /// </summary>
        public override void OnDisconnect()
        {
            this.sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            this.Connect();
        }

        /// <summary>
        /// Defines the SESSION_STATE.
        /// </summary>
        public enum SESSION_STATE
        {
            /// <summary>
            /// Defines the CONNECTED.
            /// </summary>
            CONNECTED,

            /// <summary>
            /// Defines the DISCONNECTED.
            /// </summary>
            DISCONNECTED,

            /// <summary>
            /// Defines the NOT_IDENTIFIED.
            /// </summary>
            NOT_IDENTIFIED,

            /// <summary>
            /// Defines the IDENTIFIED.
            /// </summary>
            IDENTIFIED,

            /// <summary>
            /// Defines the REJECTED.
            /// </summary>
            REJECTED,
        }
    }
}
