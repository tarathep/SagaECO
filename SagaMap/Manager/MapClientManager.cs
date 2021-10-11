namespace SagaMap.Manager
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="MapClientManager" />.
    /// </summary>
    public sealed class MapClientManager : ClientManager
    {
        /// <summary>
        /// Defines the clients.
        /// </summary>
        private List<MapClient> clients;

        /// <summary>
        /// Defines the check.
        /// </summary>
        public Thread check;

        /// <summary>
        /// Prevents a default instance of the <see cref="MapClientManager"/> class from being created.
        /// </summary>
        private MapClientManager()
        {
            this.clients = new List<MapClient>();
            this.commandTable = new Dictionary<ushort, Packet>();
            this.commandTable.Add((ushort)10, (Packet)new CSMG_SEND_VERSION());
            this.commandTable.Add((ushort)16, (Packet)new CSMG_LOGIN());
            this.commandTable.Add((ushort)30, new Packet());
            this.commandTable.Add((ushort)31, (Packet)new CSMG_LOGOUT());
            this.commandTable.Add((ushort)50, (Packet)new CSMG_PING());
            this.commandTable.Add((ushort)184, new Packet());
            this.commandTable.Add((ushort)509, (Packet)new CSMG_CHAR_SLOT());
            this.commandTable.Add((ushort)520, (Packet)new CSMG_PLAYER_STATS_UP());
            this.commandTable.Add((ushort)546, (Packet)new CSMG_PLAYER_ELEMENTS());
            this.commandTable.Add((ushort)551, (Packet)new CSMG_SKILL_LEARN());
            this.commandTable.Add((ushort)555, (Packet)new CSMG_SKILL_LEVEL_UP());
            this.commandTable.Add((ushort)525, (Packet)new CSMG_ACTOR_REQUEST_PC_INFO());
            this.commandTable.Add((ushort)600, (Packet)new CSMG_PLAYER_STATS_PRE_CALC());
            this.commandTable.Add((ushort)702, (Packet)new CSMG_NPC_JOB_SWITCH());
            this.commandTable.Add((ushort)1000, (Packet)new CSMG_CHAT_PUBLIC());
            this.commandTable.Add((ushort)1030, (Packet)new CSMG_CHAT_PARTY());
            this.commandTable.Add((ushort)1040, (Packet)new CSMG_CHAT_RING());
            this.commandTable.Add((ushort)1050, (Packet)new CSMG_CHAT_SIGN());
            this.commandTable.Add((ushort)1510, (Packet)new CSMG_NPC_EVENT_START());
            this.commandTable.Add((ushort)1536, (Packet)new CSMG_NPC_INPUTBOX());
            this.commandTable.Add((ushort)1541, (Packet)new CSMG_NPC_SELECT());
            this.commandTable.Add((ushort)1556, (Packet)new CSMG_NPC_SHOP_BUY());
            this.commandTable.Add((ushort)1558, (Packet)new CSMG_NPC_SHOP_SELL());
            this.commandTable.Add((ushort)1559, (Packet)new CSMG_NPC_SHOP_CLOSE());
            this.commandTable.Add((ushort)1591, (Packet)new CSMG_DEM_CHIP_CLOSE());
            this.commandTable.Add((ushort)1592, (Packet)new CSMG_DEM_CHIP_CATEGORY());
            this.commandTable.Add((ushort)1596, (Packet)new CSMG_DEM_CHIP_BUY());
            this.commandTable.Add((ushort)1601, (Packet)new CSMG_VSHOP_CLOSE());
            this.commandTable.Add((ushort)1610, (Packet)new CSMG_VSHOP_CATEGORY_REQUEST());
            this.commandTable.Add((ushort)1620, (Packet)new CSMG_VSHOP_BUY());
            this.commandTable.Add((ushort)2000, (Packet)new CSMG_ITEM_DROP());
            this.commandTable.Add((ushort)2020, (Packet)new CSMG_ITEM_GET());
            this.commandTable.Add((ushort)2500, (Packet)new CSMG_ITEM_USE());
            this.commandTable.Add((ushort)2530, (Packet)new CSMG_ITEM_MOVE());
            this.commandTable.Add((ushort)2535, (Packet)new CSMG_ITEM_EQUIPT());
            this.commandTable.Add((ushort)2551, (Packet)new CSMG_ITEM_WARE_CLOSE());
            this.commandTable.Add((ushort)2555, (Packet)new CSMG_ITEM_WARE_GET());
            this.commandTable.Add((ushort)2557, (Packet)new CSMG_ITEM_WARE_PUT());
            this.commandTable.Add((ushort)2570, (Packet)new CSMG_TRADE_REQUEST());
            this.commandTable.Add((ushort)2580, (Packet)new CSMG_TRADE_CONFIRM());
            this.commandTable.Add((ushort)2581, (Packet)new CSMG_TRADE_PERFORM());
            this.commandTable.Add((ushort)2582, (Packet)new CSMG_TRADE_CANCEL());
            this.commandTable.Add((ushort)2587, (Packet)new CSMG_TRADE_ITEM());
            this.commandTable.Add((ushort)2573, (Packet)new CSMG_TRADE_REQUEST_ANSWER());
            this.commandTable.Add((ushort)3990, new Packet());
            this.commandTable.Add((ushort)3999, (Packet)new CSMG_SKILL_ATTACK());
            this.commandTable.Add((ushort)4003, (Packet)new CSMG_PLAYER_RETURN_HOME());
            this.commandTable.Add((ushort)4005, (Packet)new CSMG_SKILL_CHANGE_BATTLE_STATUS());
            this.commandTable.Add((ushort)4050, new Packet());
            this.commandTable.Add((ushort)4600, (Packet)new CSMG_PLAYER_MOVE());
            this.commandTable.Add((ushort)4606, (Packet)new CSMG_PLAYER_MAP_LOADED());
            this.commandTable.Add((ushort)4630, (Packet)new CSMG_CHAT_EMOTION());
            this.commandTable.Add((ushort)4635, (Packet)new CSMG_CHAT_MOTION());
            this.commandTable.Add((ushort)4811, (Packet)new CSMG_NPC_PET_SELECT());
            this.commandTable.Add((ushort)4999, (Packet)new CSMG_SKILL_CAST());
            this.commandTable.Add((ushort)5047, (Packet)new CSMG_NPC_SYNTHESE());
            this.commandTable.Add((ushort)5049, (Packet)new CSMG_NPC_SYNTHESE_FINISH());
            this.commandTable.Add((ushort)5050, (Packet)new CSMG_CHAT_SIT());
            this.commandTable.Add((ushort)5061, (Packet)new CSMG_ITEM_ENHANCE_SELECT());
            this.commandTable.Add((ushort)5063, (Packet)new CSMG_ITEM_ENHANCE_CONFIRM());
            this.commandTable.Add((ushort)5065, (Packet)new CSMG_ITEM_ENHANCE_CLOSE());
            this.commandTable.Add((ushort)5081, (Packet)new CSMG_ITEM_FUSION());
            this.commandTable.Add((ushort)5085, (Packet)new CSMG_ITEM_FUSION_CANCEL());
            this.commandTable.Add((ushort)5091, (Packet)new CSMG_IRIS_ADD_SLOT_ITEM_SELECT());
            this.commandTable.Add((ushort)5093, (Packet)new CSMG_IRIS_ADD_SLOT_CONFIRM());
            this.commandTable.Add((ushort)5095, (Packet)new CSMG_IRIS_ADD_SLOT_CANCEL());
            this.commandTable.Add((ushort)5131, (Packet)new CSMG_IRIS_CARD_ASSEMBLE());
            this.commandTable.Add((ushort)5133, (Packet)new CSMG_IRIS_CARD_ASSEMBLE_CANCEL());
            this.commandTable.Add((ushort)6010, (Packet)new CSMG_POSSESSION_REQUEST());
            this.commandTable.Add((ushort)6015, (Packet)new CSMG_POSSESSION_CANCEL());
            this.commandTable.Add((ushort)6120, (Packet)new CSMG_GOLEM_SHOP_SELL());
            this.commandTable.Add((ushort)6122, (Packet)new CSMG_GOLEM_SHOP_SELL_CLOSE());
            this.commandTable.Add((ushort)6123, (Packet)new CSMG_GOLEM_SHOP_SELL_SETUP());
            this.commandTable.Add((ushort)6130, (Packet)new CSMG_GOLEM_WAREHOUSE());
            this.commandTable.Add((ushort)6132, (Packet)new CSMG_GOLEM_WAREHOUSE_SET());
            this.commandTable.Add((ushort)6136, (Packet)new CSMG_GOLEM_WAREHOUSE_GET());
            this.commandTable.Add((ushort)6140, (Packet)new CSMG_GOLEM_SHOP_OPEN());
            this.commandTable.Add((ushort)6143, new Packet());
            this.commandTable.Add((ushort)6147, (Packet)new CSMG_GOLEM_SHOP_SELL_BUY());
            this.commandTable.Add((ushort)6170, (Packet)new CSMG_GOLEM_SHOP_BUY());
            this.commandTable.Add((ushort)6172, (Packet)new CSMG_GOLEM_SHOP_BUY_CLOSE());
            this.commandTable.Add((ushort)6173, (Packet)new CSMG_GOLEM_SHOP_BUY_SETUP());
            this.commandTable.Add((ushort)6181, new Packet());
            this.commandTable.Add((ushort)6183, (Packet)new CSMG_GOLEM_SHOP_BUY_SELL());
            this.commandTable.Add((ushort)6545, (Packet)new CSMG_QUEST_DETAIL_REQUEST());
            this.commandTable.Add((ushort)6501, (Packet)new CSMG_QUEST_SELECT());
            this.commandTable.Add((ushort)6601, (Packet)new CSMG_PARTY_INVITE());
            this.commandTable.Add((ushort)6603, (Packet)new CSMG_PARTY_INVITE_ANSWER());
            this.commandTable.Add((ushort)6605, (Packet)new CSMG_PARTY_QUIT());
            this.commandTable.Add((ushort)6610, (Packet)new CSMG_PARTY_KICK());
            this.commandTable.Add((ushort)6615, (Packet)new CSMG_PARTY_NAME());
            this.commandTable.Add((ushort)6830, (Packet)new CSMG_RING_INVITE());
            this.commandTable.Add((ushort)6839, (Packet)new CSMG_RING_INVITE_ANSWER(false));
            this.commandTable.Add((ushort)6840, (Packet)new CSMG_RING_INVITE_ANSWER(true));
            this.commandTable.Add((ushort)6845, (Packet)new CSMG_RING_QUIT());
            this.commandTable.Add((ushort)6850, (Packet)new CSMG_RING_KICK());
            this.commandTable.Add((ushort)6870, (Packet)new CSMG_RING_RIGHT_SET());
            this.commandTable.Add((ushort)6875, (Packet)new CSMG_RING_EMBLEM_UPLOAD());
            this.commandTable.Add((ushort)6901, (Packet)new CSMG_COMMUNITY_BBS_CLOSE());
            this.commandTable.Add((ushort)6910, (Packet)new CSMG_COMMUNITY_BBS_POST());
            this.commandTable.Add((ushort)6920, (Packet)new CSMG_COMMUNITY_BBS_REQUEST_PAGE());
            this.commandTable.Add((ushort)7050, (Packet)new CSMG_COMMUNITY_RECRUIT_CREATE());
            this.commandTable.Add((ushort)7060, (Packet)new CSMG_COMMUNITY_RECRUIT_DELETE());
            this.commandTable.Add((ushort)7070, (Packet)new CSMG_COMMUNITY_RECRUIT());
            this.commandTable.Add((ushort)7080, (Packet)new CSMG_COMMUNITY_RECRUIT_JOIN());
            this.commandTable.Add((ushort)7086, (Packet)new CSMG_COMMUNITY_RECRUIT_REQUEST_ANS());
            this.commandTable.Add((ushort)7160, (Packet)new CSMG_FGARDEN_EQUIPT());
            this.commandTable.Add((ushort)7170, (Packet)new CSMG_FGARDEN_FURNITURE_SETUP());
            this.commandTable.Add((ushort)7175, (Packet)new CSMG_FGARDEN_FURNITURE_USE());
            this.commandTable.Add((ushort)7180, (Packet)new CSMG_FGARDEN_FURNITURE_REMOVE());
            this.commandTable.Add((ushort)7185, (Packet)new CSMG_FGARDEN_FURNITURE_RECONFIG());
            this.commandTable.Add((ushort)7500, (Packet)new CSMG_PLAYER_GREETINGS());
            this.commandTable.Add((ushort)7600, (Packet)new CSMG_IRIS_CARD_OPEN());
            this.commandTable.Add((ushort)7602, (Packet)new CSMG_IRIS_CARD_CLOSE());
            this.commandTable.Add((ushort)7606, (Packet)new CSMG_IRIS_CARD_INSERT());
            this.commandTable.Add((ushort)7611, (Packet)new CSMG_IRIS_CARD_REMOVE());
            this.commandTable.Add((ushort)7625, (Packet)new CSMG_IRIS_CARD_LOCK());
            this.commandTable.Add((ushort)7751, (Packet)new CSMG_DEM_DEMIC_CLOSE());
            this.commandTable.Add((ushort)7756, (Packet)new CSMG_DEM_DEMIC_INITIALIZE());
            this.commandTable.Add((ushort)7758, (Packet)new CSMG_DEM_DEMIC_CONFIRM());
            this.commandTable.Add((ushort)7760, (Packet)new CSMG_DEM_STATS_PRE_CALC());
            this.commandTable.Add((ushort)7771, (Packet)new CSMG_DEM_COST_LIMIT_CLOSE());
            this.commandTable.Add((ushort)7772, (Packet)new CSMG_DEM_COST_LIMIT_BUY());
            this.commandTable.Add((ushort)7805, (Packet)new CSMG_DEM_FORM_CHANGE());
            this.commandTable.Add((ushort)7811, (Packet)new CSMG_DEM_PARTS_CLOSE());
            this.commandTable.Add((ushort)7815, (Packet)new CSMG_DEM_PARTS_EQUIP());
            this.commandTable.Add((ushort)7816, (Packet)new CSMG_DEM_PARTS_UNEQUIP());
            this.waitressQueue = new AutoResetEvent(true);
            this.check = new Thread(new ThreadStart(((ClientManager)this).checkCriticalArea));
            this.check.Name = string.Format("DeadLock checker({0})", (object)this.check.ManagedThreadId);
            this.check.Start();
        }

        /// <summary>
        /// The Abort.
        /// </summary>
        public void Abort()
        {
            this.check.Abort();
            this.packetCoordinator.Abort();
        }

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static MapClientManager Instance
        {
            get
            {
                return MapClientManager.Nested.instance;
            }
        }

        /// <summary>
        /// The Announce.
        /// </summary>
        /// <param name="txt">The txt<see cref="string"/>.</param>
        public void Announce(string txt)
        {
            try
            {
                foreach (MapClient mapClient in this.OnlinePlayer)
                {
                    try
                    {
                        mapClient.SendAnnounce(txt);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
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
                Logger.ShowInfo(string.Format(Singleton<LocalManager>.Instance.Strings.NEW_CLIENT, (object)mSock.RemoteEndPoint.ToString()), (Logger)null);
                this.clients.Add(new MapClient(mSock, this.commandTable));
            }
        }

        /// <summary>
        /// Gets the Clients.
        /// </summary>
        public List<MapClient> Clients
        {
            get
            {
                return this.clients;
            }
        }

        /// <summary>
        /// The OnClientDisconnect.
        /// </summary>
        /// <param name="client_t">The client_t<see cref="SagaLib.Client"/>.</param>
        public override void OnClientDisconnect(SagaLib.Client client_t)
        {
            this.clients.Remove((MapClient)client_t);
        }

        /// <summary>
        /// The FindClient.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="MapClient"/>.</returns>
        public MapClient FindClient(ActorPC pc)
        {
            return this.FindClient(pc.CharID);
        }

        /// <summary>
        /// The GetClient.
        /// </summary>
        /// <param name="actorID">The actorID<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaLib.Client"/>.</returns>
        public override SagaLib.Client GetClient(uint actorID)
        {
            IEnumerable<MapClient> source = this.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => (int)c.Character.ActorID == (int)actorID));
            if (source.Count<MapClient>() != 0)
                return (SagaLib.Client)source.First<MapClient>();
            return (SagaLib.Client)null;
        }

        /// <summary>
        /// Gets the OnlinePlayer.
        /// </summary>
        public List<MapClient> OnlinePlayer
        {
            get
            {
                List<MapClient> mapClientList = new List<MapClient>();
                foreach (MapClient client in this.clients)
                {
                    if (!client.netIO.Disconnected && client.Character != null && client.Character.Online)
                        mapClientList.Add(client);
                }
                return mapClientList;
            }
        }

        /// <summary>
        /// The FindClient.
        /// </summary>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        /// <returns>The <see cref="MapClient"/>.</returns>
        public MapClient FindClient(uint charID)
        {
            IEnumerable<MapClient> source = this.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => (int)c.Character.CharID == (int)charID));
            if (source.Count<MapClient>() != 0)
                return source.First<MapClient>();
            return (MapClient)null;
        }

        /// <summary>
        /// Defines the <see cref="Nested" />.
        /// </summary>
        private class Nested
        {
            /// <summary>
            /// Defines the instance.
            /// </summary>
            internal static readonly MapClientManager instance = new MapClientManager();
        }
    }
}
