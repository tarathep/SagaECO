namespace SagaLogin.Manager
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaLogin.Network.Client;
    using SagaLogin.Packets.Client;
    using SagaLogin.Packets.Map;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="LoginClientManager" />.
    /// </summary>
    public sealed class LoginClientManager : ClientManager
    {
        /// <summary>
        /// Defines the clients.
        /// </summary>
        private List<LoginClient> clients;

        /// <summary>
        /// Defines the check.
        /// </summary>
        public Thread check;

        /// <summary>
        /// Prevents a default instance of the <see cref="LoginClientManager"/> class from being created.
        /// </summary>
        private LoginClientManager()
        {
            this.clients = new List<LoginClient>();
            this.commandTable = new Dictionary<ushort, Packet>();
            this.commandTable.Add((ushort)1, (Packet)new CSMG_SEND_VERSION());
            this.commandTable.Add((ushort)10, (Packet)new CSMG_PING());
            this.commandTable.Add((ushort)42, (Packet)new CSMG_CHAR_STATUS());
            this.commandTable.Add((ushort)160, (Packet)new CSMG_CHAR_CREATE());
            this.commandTable.Add((ushort)165, (Packet)new CSMG_CHAR_DELETE());
            this.commandTable.Add((ushort)167, (Packet)new CSMG_CHAR_SELECT());
            this.commandTable.Add((ushort)31, (Packet)new CSMG_LOGIN());
            this.commandTable.Add((ushort)50, (Packet)new CSMG_REQUEST_MAP_SERVER());
            this.commandTable.Add((ushort)201, (Packet)new CSMG_CHAT_WHISPER());
            this.commandTable.Add((ushort)210, (Packet)new CSMG_FRIEND_ADD());
            this.commandTable.Add((ushort)212, (Packet)new CSMG_FRIEND_ADD_REPLY());
            this.commandTable.Add((ushort)215, (Packet)new CSMG_FRIEND_DELETE());
            this.commandTable.Add((ushort)225, (Packet)new CSMG_FRIEND_DETAIL_UPDATE());
            this.commandTable.Add((ushort)230, (Packet)new CSMG_FRIEND_MAP_UPDATE());
            this.commandTable.Add((ushort)260, (Packet)new CSMG_RING_EMBLEM_NEW());
            this.commandTable.Add((ushort)265, (Packet)new CSMG_RING_EMBLEM());
            this.commandTable.Add((ushort)370, (Packet)new CSMG_WRP_REQUEST());
            this.commandTable.Add((ushort)65520, (Packet)new INTERN_LOGIN_REGISTER());
            this.commandTable.Add((ushort)65521, (Packet)new INTERN_LOGIN_REQUEST_CONFIG());
            this.waitressQueue = new AutoResetEvent(true);
            this.check = new Thread(new ThreadStart(((ClientManager)this).checkCriticalArea));
            this.check.Name = string.Format("DeadLock checker({0})", (object)this.check.ManagedThreadId);
        }

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static LoginClientManager Instance
        {
            get
            {
                return LoginClientManager.Nested.instance;
            }
        }

        /// <summary>
        /// Gets the Clients.
        /// </summary>
        public List<LoginClient> Clients
        {
            get
            {
                return this.clients;
            }
        }

        /// <summary>
        /// The NetworkLoop.
        /// </summary>
        /// <param name="maxNewConnections">The maxNewConnections<see cref="int"/>.</param>
        public override void NetworkLoop(int maxNewConnections)
        {
            for (int index = 0; this.listener.Pending() && index < maxNewConnections; ++index)
            {
                Socket mSock = this.listener.AcceptSocket();
                mSock.RemoteEndPoint.ToString().Substring(0, mSock.RemoteEndPoint.ToString().IndexOf(':'));
                Logger.ShowInfo("New client from: " + mSock.RemoteEndPoint.ToString(), (Logger)null);
                this.clients.Add(new LoginClient(mSock, this.commandTable));
            }
        }

        /// <summary>
        /// The OnClientDisconnect.
        /// </summary>
        /// <param name="client_t">The client_t<see cref="SagaLib.Client"/>.</param>
        public override void OnClientDisconnect(SagaLib.Client client_t)
        {
            this.clients.Remove((LoginClient)client_t);
        }

        /// <summary>
        /// The FindClient.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="LoginClient"/>.</returns>
        public LoginClient FindClient(ActorPC pc)
        {
            IEnumerable<LoginClient> source = this.clients.Where<LoginClient>((Func<LoginClient, bool>)(c => !c.IsMapServer && c.selectedChar != null)).ToList<LoginClient>().Where<LoginClient>((Func<LoginClient, bool>)(c => (int)c.selectedChar.CharID == (int)pc.CharID));
            if (source.Count<LoginClient>() != 0)
                return source.First<LoginClient>();
            return (LoginClient)null;
        }

        /// <summary>
        /// The FindClient.
        /// </summary>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        /// <returns>The <see cref="LoginClient"/>.</returns>
        public LoginClient FindClient(uint charID)
        {
            IEnumerable<LoginClient> source = this.clients.Where<LoginClient>((Func<LoginClient, bool>)(c => !c.IsMapServer && c.selectedChar != null)).ToList<LoginClient>().Where<LoginClient>((Func<LoginClient, bool>)(c => (int)c.selectedChar.CharID == (int)charID));
            if (source.Count<LoginClient>() != 0)
                return source.First<LoginClient>();
            return (LoginClient)null;
        }

        /// <summary>
        /// The FindClient.
        /// </summary>
        /// <param name="charName">The charName<see cref="string"/>.</param>
        /// <returns>The <see cref="LoginClient"/>.</returns>
        public LoginClient FindClient(string charName)
        {
            IEnumerable<LoginClient> source = this.clients.Where<LoginClient>((Func<LoginClient, bool>)(c => !c.IsMapServer && c.selectedChar != null)).ToList<LoginClient>().Where<LoginClient>((Func<LoginClient, bool>)(c => c.selectedChar.Name == charName));
            if (source.Count<LoginClient>() != 0)
                return source.First<LoginClient>();
            return (LoginClient)null;
        }

        /// <summary>
        /// The FindClientAccount.
        /// </summary>
        /// <param name="accountName">The accountName<see cref="string"/>.</param>
        /// <returns>The <see cref="LoginClient"/>.</returns>
        public LoginClient FindClientAccount(string accountName)
        {
            IEnumerable<LoginClient> source = this.clients.Where<LoginClient>((Func<LoginClient, bool>)(c => !c.IsMapServer && c.account != null)).ToList<LoginClient>().Where<LoginClient>((Func<LoginClient, bool>)(c => c.account.Name == accountName));
            if (source.Count<LoginClient>() != 0)
                return source.First<LoginClient>();
            return (LoginClient)null;
        }

        /// <summary>
        /// Defines the <see cref="Nested" />.
        /// </summary>
        private class Nested
        {
            /// <summary>
            /// Defines the instance.
            /// </summary>
            internal static readonly LoginClientManager instance = new LoginClientManager();
        }
    }
}
