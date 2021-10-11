namespace SagaMap.Network.Client
{
    using SagaDB;
    using SagaDB.Actor;
    using SagaDB.DEMIC;
    using SagaDB.ECOShop;
    using SagaDB.FGarden;
    using SagaDB.Iris;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Marionette;
    using SagaDB.Npc;
    using SagaDB.ODWar;
    using SagaDB.Quests;
    using SagaDB.Ring;
    using SagaDB.Skill;
    using SagaDB.Theater;
    using SagaLib;
    using SagaLogin.Configurations;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Dungeon;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Packets.Client;
    using SagaMap.Packets.Server;
    using SagaMap.PC;
    using SagaMap.Scripting;
    using SagaMap.Skill;
    using SagaMap.Skill.Additions.Global;
    using SagaMap.Tasks.Golem;
    using SagaMap.Tasks.Item;
    using SagaMap.Tasks.PC;
    using SagaMap.Tasks.Skill;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="MapClient" />.
    /// </summary>
    public class MapClient : SagaLib.Client
    {
        /// <summary>
        /// Defines the firstLogin.
        /// </summary>
        public bool firstLogin = true;

        /// <summary>
        /// Defines the currentVShopCategory.
        /// </summary>
        private uint currentVShopCategory = 0;

        /// <summary>
        /// Defines the skillDelay.
        /// </summary>
        private DateTime skillDelay = DateTime.Now;

        /// <summary>
        /// Defines the attackStamp.
        /// </summary>
        private DateTime attackStamp = DateTime.Now;

        /// <summary>
        /// Defines the hackStamp.
        /// </summary>
        private DateTime hackStamp = DateTime.Now;

        /// <summary>
        /// Defines the hackCount.
        /// </summary>
        private int hackCount = 0;

        /// <summary>
        /// Defines the trading.
        /// </summary>
        private bool trading = false;

        /// <summary>
        /// Defines the confirmed.
        /// </summary>
        private bool confirmed = false;

        /// <summary>
        /// Defines the performed.
        /// </summary>
        private bool performed = false;

        /// <summary>
        /// Defines the npcTrade.
        /// </summary>
        public bool npcTrade = false;

        /// <summary>
        /// Defines the tradeItems.
        /// </summary>
        private List<uint> tradeItems = (List<uint>)null;

        /// <summary>
        /// Defines the tradeCounts.
        /// </summary>
        private List<ushort> tradeCounts = (List<ushort>)null;

        /// <summary>
        /// Defines the tradingGold.
        /// </summary>
        private uint tradingGold = 0;

        /// <summary>
        /// Defines the npcTradeItem.
        /// </summary>
        public List<SagaDB.Item.Item> npcTradeItem = (List<SagaDB.Item.Item>)null;

        /// <summary>
        /// Defines the demCLBuy.
        /// </summary>
        public bool demCLBuy = false;

        /// <summary>
        /// Defines the demParts.
        /// </summary>
        public bool demParts = false;

        /// <summary>
        /// Defines the demic.
        /// </summary>
        public bool demic = false;

        /// <summary>
        /// Defines the chipShop.
        /// </summary>
        public bool chipShop = false;

        /// <summary>
        /// Defines the currentChipCategory.
        /// </summary>
        private uint currentChipCategory = 0;

        /// <summary>
        /// Defines the itemEnhance.
        /// </summary>
        public bool itemEnhance = false;

        /// <summary>
        /// Defines the itemFusion.
        /// </summary>
        public bool itemFusion = false;

        /// <summary>
        /// Defines the moveStamp.
        /// </summary>
        public DateTime moveStamp = DateTime.Now;

        /// <summary>
        /// Defines the hpmpspStamp.
        /// </summary>
        public DateTime hpmpspStamp = DateTime.Now;

        /// <summary>
        /// Defines the moveCheckStamp.
        /// </summary>
        public DateTime moveCheckStamp = DateTime.Now;

        /// <summary>
        /// Defines the irisAddSlot.
        /// </summary>
        public bool irisAddSlot = false;

        /// <summary>
        /// Defines the irisCardAssemble.
        /// </summary>
        public bool irisCardAssemble = false;

        /// <summary>
        /// Defines the irisAddSlotMaterial.
        /// </summary>
        private uint irisAddSlotMaterial = 0;

        /// <summary>
        /// Defines the irisAddSlotItem.
        /// </summary>
        private uint irisAddSlotItem = 0;

        /// <summary>
        /// Defines the irisCardItem.
        /// </summary>
        private uint irisCardItem = 0;

        /// <summary>
        /// Defines the golemLogout.
        /// </summary>
        private bool golemLogout = false;

        /// <summary>
        /// Defines the needSendGolem.
        /// </summary>
        private bool needSendGolem = false;

        /// <summary>
        /// Defines the fgTakeOff.
        /// </summary>
        public bool fgTakeOff = false;

        /// <summary>
        /// Defines the ping.
        /// </summary>
        public DateTime ping = DateTime.Now;

        /// <summary>
        /// Defines the currentWarehouse.
        /// </summary>
        public WarehousePlace currentWarehouse = WarehousePlace.Current;

        /// <summary>
        /// Defines the partyPartner.
        /// </summary>
        private ActorPC partyPartner;

        /// <summary>
        /// Defines the questID.
        /// </summary>
        public uint questID;

        /// <summary>
        /// Defines the client_Version.
        /// </summary>
        private string client_Version;

        /// <summary>
        /// Defines the frontWord.
        /// </summary>
        private uint frontWord;

        /// <summary>
        /// Defines the backWord.
        /// </summary>
        private uint backWord;

        /// <summary>
        /// Defines the account.
        /// </summary>
        private Account account;

        /// <summary>
        /// Defines the chara.
        /// </summary>
        private ActorPC chara;

        /// <summary>
        /// Defines the map.
        /// </summary>
        public SagaMap.Map map;

        /// <summary>
        /// Defines the state.
        /// </summary>
        public MapClient.SESSION_STATE state;

        /// <summary>
        /// Defines the AI.
        /// </summary>
        public MobAI AI;

        /// <summary>
        /// Defines the npcSelectResult.
        /// </summary>
        public int npcSelectResult;

        /// <summary>
        /// Defines the npcShopClosed.
        /// </summary>
        public bool npcShopClosed;

        /// <summary>
        /// Defines the currentEventID.
        /// </summary>
        private uint currentEventID;

        /// <summary>
        /// Defines the currentEvent.
        /// </summary>
        private Event currentEvent;

        /// <summary>
        /// Defines the scriptThread.
        /// </summary>
        public Thread scriptThread;

        /// <summary>
        /// Defines the syntheseItem.
        /// </summary>
        public Dictionary<uint, uint> syntheseItem;

        /// <summary>
        /// Defines the syntheseFinished.
        /// </summary>
        public bool syntheseFinished;

        /// <summary>
        /// Defines the currentShop.
        /// </summary>
        public Shop currentShop;

        /// <summary>
        /// Defines the inputContent.
        /// </summary>
        public string inputContent;

        /// <summary>
        /// Defines the npcJobSwitch.
        /// </summary>
        public bool npcJobSwitch;

        /// <summary>
        /// Defines the npcJobSwitchRes.
        /// </summary>
        public bool npcJobSwitchRes;

        /// <summary>
        /// Defines the vshopClosed.
        /// </summary>
        public bool vshopClosed;

        /// <summary>
        /// Defines the selectedPet.
        /// </summary>
        public uint selectedPet;

        /// <summary>
        /// Defines the lastAttackRandom.
        /// </summary>
        private short lastAttackRandom;

        /// <summary>
        /// Defines the lastCastRandom.
        /// </summary>
        private short lastCastRandom;

        /// <summary>
        /// Defines the nextCombo.
        /// </summary>
        public uint nextCombo;

        /// <summary>
        /// Defines the tradingTarget.
        /// </summary>
        private ActorPC tradingTarget;

        /// <summary>
        /// Defines the ringPartner.
        /// </summary>
        public ActorPC ringPartner;

        /// <summary>
        /// Defines the itemFusionEffect.
        /// </summary>
        public uint itemFusionEffect;

        /// <summary>
        /// Defines the itemFusionView.
        /// </summary>
        public uint itemFusionView;

        /// <summary>
        /// Defines the bbsClose.
        /// </summary>
        public bool bbsClose;

        /// <summary>
        /// Defines the bbsCurrentPage.
        /// </summary>
        public int bbsCurrentPage;

        /// <summary>
        /// Defines the bbsID.
        /// </summary>
        public uint bbsID;

        /// <summary>
        /// Defines the bbsCost.
        /// </summary>
        public uint bbsCost;

        /// <summary>
        /// Defines the logger.
        /// </summary>
        private SagaMap.PacketLogger.PacketLogger logger;

        /// <summary>
        /// The OnPartyName.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PARTY_NAME"/>.</param>
        public void OnPartyName(CSMG_PARTY_NAME p)
        {
            if (this.Character.Party == null || p.Name == "" || this.Character.Party.Leader != this.Character)
                return;
            this.Character.Party.Name = p.Name;
            Singleton<PartyManager>.Instance.UpdatePartyName(this.Character.Party);
        }

        /// <summary>
        /// The OnPartyKick.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PARTY_KICK"/>.</param>
        public void OnPartyKick(CSMG_PARTY_KICK p)
        {
            if (this.Character.Party == null || this.Character.Party.Leader != this.Character)
                return;
            SSMG_PARTY_KICK ssmgPartyKick = new SSMG_PARTY_KICK();
            if (this.Character.Party.IsMember(p.CharID))
                Singleton<PartyManager>.Instance.DeleteMember(this.Character.Party, p.CharID, SSMG_PARTY_DELETE.Result.KICKED);
            else
                ssmgPartyKick.Result = -1;
            this.netIO.SendPacket((Packet)ssmgPartyKick);
        }

        /// <summary>
        /// The OnPartyQuit.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PARTY_QUIT"/>.</param>
        public void OnPartyQuit(CSMG_PARTY_QUIT p)
        {
            SSMG_PARTY_QUIT ssmgPartyQuit = new SSMG_PARTY_QUIT();
            if (this.Character.Party == null)
                ssmgPartyQuit.Result = -1;
            else if (this.Character != this.Character.Party.Leader)
                Singleton<PartyManager>.Instance.DeleteMember(this.Character.Party, this.Character.CharID, SSMG_PARTY_DELETE.Result.QUIT);
            else
                Singleton<PartyManager>.Instance.PartyDismiss(this.Character.Party);
            this.netIO.SendPacket((Packet)ssmgPartyQuit);
        }

        /// <summary>
        /// The OnPartyInviteAnswer.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PARTY_INVITE_ANSWER"/>.</param>
        public void OnPartyInviteAnswer(CSMG_PARTY_INVITE_ANSWER p)
        {
            if (this.partyPartner == null || (int)this.partyPartner.CharID != (int)p.CharID)
                return;
            MapClient mapClient = MapClient.FromActorPC(this.partyPartner);
            if (mapClient.Character.Party != null)
            {
                if (mapClient.Character.Party.MemberCount >= 8)
                    return;
                Singleton<PartyManager>.Instance.AddMember(mapClient.Character.Party, this.Character);
            }
            else
                Singleton<PartyManager>.Instance.AddMember(Singleton<PartyManager>.Instance.CreateParty(this.partyPartner), this.Character);
        }

        /// <summary>
        /// The OnPartyInvite.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PARTY_INVITE"/>.</param>
        public void OnPartyInvite(CSMG_PARTY_INVITE p)
        {
            MapClient client = MapClientManager.Instance.FindClient(p.CharID);
            if (client != null)
            {
                SSMG_PARTY_INVITE_RESULT partyInviteResult = new SSMG_PARTY_INVITE_RESULT();
                if (client.Character.Party != null)
                    partyInviteResult.InviteResult = SSMG_PARTY_INVITE_RESULT.Result.PLAYER_ALREADY_IN_PARTY;
                else if (this.Character.Party != null)
                {
                    if (this.Character.Party.MemberCount == 8)
                    {
                        partyInviteResult.InviteResult = SSMG_PARTY_INVITE_RESULT.Result.PARTY_MEMBER_EXCEED;
                    }
                    else
                    {
                        partyInviteResult.InviteResult = SSMG_PARTY_INVITE_RESULT.Result.OK;
                        SSMG_PARTY_INVITE ssmgPartyInvite = new SSMG_PARTY_INVITE();
                        ssmgPartyInvite.CharID = this.Character.CharID;
                        ssmgPartyInvite.Name = this.Character.Name;
                        client.partyPartner = this.Character;
                        client.netIO.SendPacket((Packet)ssmgPartyInvite);
                    }
                }
                else
                {
                    partyInviteResult.InviteResult = SSMG_PARTY_INVITE_RESULT.Result.OK;
                    SSMG_PARTY_INVITE ssmgPartyInvite = new SSMG_PARTY_INVITE();
                    ssmgPartyInvite.CharID = this.Character.CharID;
                    ssmgPartyInvite.Name = this.Character.Name;
                    client.partyPartner = this.Character;
                    client.netIO.SendPacket((Packet)ssmgPartyInvite);
                }
                this.netIO.SendPacket((Packet)partyInviteResult);
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_PARTY_INVITE_RESULT()
                {
                    InviteResult = SSMG_PARTY_INVITE_RESULT.Result.PLAYER_NOT_EXIST
                });
        }

        /// <summary>
        /// The SendPartyInfo.
        /// </summary>
        public void SendPartyInfo()
        {
            if (this.Character.Party == null)
                return;
            SSMG_PARTY_INFO ssmgPartyInfo = new SSMG_PARTY_INFO();
            ssmgPartyInfo.Party(this.Character.Party, this.Character);
            SSMG_PARTY_NAME ssmgPartyName = new SSMG_PARTY_NAME();
            ssmgPartyName.Party(this.Character.Party, (SagaDB.Actor.Actor)this.Character);
            this.netIO.SendPacket((Packet)ssmgPartyInfo);
            this.netIO.SendPacket((Packet)ssmgPartyName);
            this.SendPartyMember();
        }

        /// <summary>
        /// The SendPartyMeDelete.
        /// </summary>
        /// <param name="reason">The reason<see cref="SSMG_PARTY_DELETE.Result"/>.</param>
        public void SendPartyMeDelete(SSMG_PARTY_DELETE.Result reason)
        {
            this.netIO.SendPacket((Packet)new SSMG_PARTY_DELETE()
            {
                PartyID = this.Character.Party.ID,
                PartyName = this.Character.Party.Name,
                Reason = reason
            });
        }

        /// <summary>
        /// The SendPartyMemberDelete.
        /// </summary>
        /// <param name="pc">The pc<see cref="uint"/>.</param>
        public void SendPartyMemberDelete(uint pc)
        {
            this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER()
            {
                PartyIndex = -1,
                CharID = pc,
                CharName = ""
            });
        }

        /// <summary>
        /// The SendPartyMemberPosition.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendPartyMemberPosition(ActorPC pc)
        {
            if (this.Character.Party == null || !this.Character.Party.IsMember(pc) || !pc.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_POSITION()
            {
                PartyIndex = this.Character.Party.IndexOf(pc),
                CharID = pc.CharID,
                MapID = pc.MapID,
                X = SagaLib.Global.PosX16to8(pc.X, Singleton<MapManager>.Instance.GetMap(pc.MapID).Width),
                Y = SagaLib.Global.PosY16to8(pc.Y, Singleton<MapManager>.Instance.GetMap(pc.MapID).Height)
            });
        }

        /// <summary>
        /// The SendPartyMemberDeungeonPosition.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendPartyMemberDeungeonPosition(ActorPC pc)
        {
            if (this.Character.Party == null || !this.Character.Party.IsMember(pc) || !this.map.IsDungeon)
                return;
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(pc.MapID);
            if (map.IsDungeon)
                this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_DUNGEON_POSITION()
                {
                    CharID = pc.CharID,
                    MapID = map.ID,
                    X = map.DungeonMap.X,
                    Y = map.DungeonMap.Y,
                    Dir = map.DungeonMap.Dir
                });
        }

        /// <summary>
        /// The SendPartyMemberDetail.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendPartyMemberDetail(ActorPC pc)
        {
            if (this.Character.Party == null || !this.Character.Party.IsMember(pc) || !pc.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_DETAIL()
            {
                PartyIndex = (uint)this.Character.Party.IndexOf(pc),
                CharID = pc.CharID,
                Job = pc.Job,
                Level = pc.Level,
                JobLevel = pc.CurrentJobLevel
            });
        }

        /// <summary>
        /// The SendPartyMemberState.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendPartyMemberState(ActorPC pc)
        {
            if (this.Character.Party == null || !this.Character.Party.IsMember(pc))
                return;
            byte num = this.Character.Party.IndexOf(pc);
            this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_STATE()
            {
                PartyIndex = (uint)num,
                CharID = pc.CharID,
                Online = pc.Online
            });
        }

        /// <summary>
        /// The SendPartyMemberHPMPSP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendPartyMemberHPMPSP(ActorPC pc)
        {
            if (this.Character.Party == null || !this.Character.Party.IsMember(pc))
                return;
            byte num = this.Character.Party.IndexOf(pc);
            this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_HPMPSP()
            {
                PartyIndex = num,
                CharID = pc.CharID,
                HP = pc.HP,
                MaxHP = pc.MaxHP,
                MP = pc.MP,
                MaxMP = pc.MaxMP,
                SP = pc.SP,
                MaxSP = pc.MaxSP
            });
        }

        /// <summary>
        /// The SendPartyMemberInfo.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendPartyMemberInfo(ActorPC pc)
        {
            if (this.Character.Party == null || !this.Character.Party.IsMember(pc))
                return;
            if (pc.Online)
            {
                try
                {
                    byte num = this.Character.Party.IndexOf(pc);
                    this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_STATE()
                    {
                        PartyIndex = (uint)num,
                        CharID = pc.CharID,
                        Online = pc.Online
                    });
                    this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_POSITION()
                    {
                        PartyIndex = num,
                        CharID = pc.CharID,
                        MapID = pc.MapID,
                        X = SagaLib.Global.PosX16to8(pc.X, Singleton<MapManager>.Instance.GetMap(pc.MapID).Width),
                        Y = SagaLib.Global.PosY16to8(pc.Y, Singleton<MapManager>.Instance.GetMap(pc.MapID).Height)
                    });
                    this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_DETAIL()
                    {
                        PartyIndex = (uint)num,
                        CharID = pc.CharID,
                        Job = pc.Job,
                        Level = pc.Level,
                        JobLevel = pc.CurrentJobLevel
                    });
                    this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_HPMPSP()
                    {
                        PartyIndex = num,
                        CharID = pc.CharID,
                        HP = pc.HP,
                        MaxHP = pc.MaxHP,
                        MP = pc.MP,
                        MaxMP = pc.MaxMP,
                        SP = pc.SP,
                        MaxSP = pc.MaxSP
                    });
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }

        /// <summary>
        /// The SendPartyMember.
        /// </summary>
        private void SendPartyMember()
        {
            if (this.Character.Party == null)
                return;
            foreach (byte key in this.Character.Party.Members.Keys)
                this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER()
                {
                    PartyIndex = (int)key,
                    CharID = this.Character.Party[key].CharID,
                    CharName = this.Character.Party[key].Name,
                    Leader = (this.Character.Party.Leader == this.Character.Party[key])
                });
            SagaDB.Party.Party party = this.Character.Party;
            foreach (byte key in party.Members.Keys)
                this.SendPartyMemberInfo(party[key]);
        }

        /// <summary>
        /// The OnPossessionRequest.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_POSSESSION_REQUEST"/>.</param>
        public void OnPossessionRequest(CSMG_POSSESSION_REQUEST p)
        {
            ActorPC actor = (ActorPC)this.Map.GetActor(p.ActorID);
            PossessionPosition possessionPosition = p.PossessionPosition;
            int num = this.TestPossesionPosition(actor, possessionPosition);
            if (num >= 0)
            {
                this.Character.Buff.憑依準備 = true;
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
                int reduce = 0;
                if (this.Character.Status.Additions.ContainsKey("TranceSpdUp"))
                    reduce = ((DefaultPassiveSkill)this.Character.Status.Additions["TranceSpdUp"])["TranceSpdUp"];
                Possession possession = new Possession(this, actor, possessionPosition, p.Comment, reduce);
                this.Character.Tasks.Add("Possession", (MultiRunTask)possession);
                possession.Activate();
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_POSSESSION_RESULT()
                {
                    FromID = this.Character.ActorID,
                    ToID = uint.MaxValue,
                    Result = num
                });
        }

        /// <summary>
        /// The OnPossessionCancel.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_POSSESSION_CANCEL"/>.</param>
        public void OnPossessionCancel(CSMG_POSSESSION_CANCEL p)
        {
            PossessionPosition possessionPosition = p.PossessionPosition;
            if (possessionPosition == PossessionPosition.NONE)
            {
                SagaDB.Actor.Actor actor = this.Map.GetActor(this.Character.PossessionTarget);
                if (actor == null)
                    return;
                PossessionArg possessionArg = new PossessionArg();
                possessionArg.fromID = this.Character.ActorID;
                possessionArg.cancel = true;
                possessionArg.result = (int)this.Character.PossessionPosition;
                possessionArg.x = SagaLib.Global.PosX16to8(this.Character.X, this.Map.Width);
                possessionArg.y = SagaLib.Global.PosY16to8(this.Character.Y, this.Map.Height);
                possessionArg.dir = (byte)((uint)this.Character.Dir / 45U);
                if (actor.type == ActorType.ITEM)
                {
                    SagaDB.Item.Item possessionItem = this.GetPossessionItem(this.Character, this.Character.PossessionPosition);
                    possessionItem.PossessionedActor = (ActorPC)null;
                    possessionItem.PossessionOwner = (ActorPC)null;
                    this.Character.PossessionTarget = 0U;
                    this.Character.PossessionPosition = PossessionPosition.NONE;
                    possessionArg.toID = uint.MaxValue;
                    this.Map.DeleteActor(actor);
                }
                else if (actor.type == ActorType.PC)
                {
                    ActorPC actorPc = (ActorPC)actor;
                    possessionArg.toID = actorPc.ActorID;
                    SagaDB.Item.Item possessionItem1 = this.GetPossessionItem(actorPc, this.Character.PossessionPosition);
                    if (possessionItem1.PossessionOwner != this.Character)
                    {
                        possessionItem1.PossessionedActor = (ActorPC)null;
                        this.Character.PossessionTarget = 0U;
                        this.Character.PossessionPosition = PossessionPosition.NONE;
                    }
                    else
                    {
                        SagaDB.Item.Item possessionItem2 = this.GetPossessionItem(this.Character, this.Character.PossessionPosition);
                        possessionItem2.PossessionedActor = (ActorPC)null;
                        possessionItem2.PossessionOwner = (ActorPC)null;
                        this.Character.PossessionTarget = 0U;
                        this.Character.PossessionPosition = PossessionPosition.NONE;
                        CSMG_ITEM_MOVE p1 = new CSMG_ITEM_MOVE();
                        p1.data = new byte[9];
                        p1.InventoryID = possessionItem1.Slot;
                        p1.Target = ContainerType.BODY;
                        p1.Count = (ushort)1;
                        MapClient.FromActorPC(actorPc).OnItemMove(p1, true);
                        int num = (int)actorPc.Inventory.DeleteItem(possessionItem1.Slot, 1);
                        ((PCEventHandler)actorPc.e).Client.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                        {
                            InventorySlot = possessionItem1.Slot
                        });
                        this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHANGE_EQUIP, (MapEventArgs)null, (SagaDB.Actor.Actor)actorPc, true);
                    }
                    possessionItem1.PossessionedActor = (ActorPC)null;
                    possessionItem1.PossessionOwner = (ActorPC)null;
                    Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                    this.SendPlayerInfo();
                    Singleton<StatusFactory>.Instance.CalcStatus(actorPc);
                    ((PCEventHandler)actorPc.e).Client.SendPlayerInfo();
                }
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.POSSESSION, (MapEventArgs)possessionArg, (SagaDB.Actor.Actor)this.Character, true);
            }
            else
            {
                SagaDB.Item.Item possessionItem1 = this.GetPossessionItem(this.Character, possessionPosition);
                if (possessionItem1 == null || possessionItem1.PossessionedActor == null)
                    return;
                PossessionArg possessionArg = new PossessionArg();
                possessionArg.fromID = possessionItem1.PossessionedActor.ActorID;
                possessionArg.toID = this.Character.ActorID;
                possessionArg.cancel = true;
                possessionArg.result = (int)possessionItem1.PossessionedActor.PossessionPosition;
                possessionArg.x = SagaLib.Global.PosX16to8(this.Character.X, this.Map.Width);
                possessionArg.y = SagaLib.Global.PosY16to8(this.Character.Y, this.Map.Height);
                possessionArg.dir = (byte)((uint)this.Character.Dir / 45U);
                if (possessionItem1.PossessionOwner != this.Character && possessionItem1.PossessionOwner != null)
                {
                    SagaDB.Item.Item possessionItem2 = this.GetPossessionItem(possessionItem1.PossessionedActor, possessionItem1.PossessionedActor.PossessionPosition);
                    possessionItem2.PossessionedActor = (ActorPC)null;
                    possessionItem2.PossessionOwner = (ActorPC)null;
                    CSMG_ITEM_MOVE p1 = new CSMG_ITEM_MOVE();
                    p1.data = new byte[9];
                    p1.InventoryID = possessionItem1.Slot;
                    p1.Target = ContainerType.BODY;
                    p1.Count = (ushort)1;
                    this.OnItemMove(p1, true);
                    int num = (int)this.Character.Inventory.DeleteItem(possessionItem1.Slot, 1);
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                    {
                        InventorySlot = possessionItem1.Slot
                    });
                    this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHANGE_EQUIP, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
                    this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.POSSESSION, (MapEventArgs)possessionArg, (SagaDB.Actor.Actor)this.Character, true);
                    if (((PCEventHandler)possessionItem1.PossessionedActor.e).Client.state == MapClient.SESSION_STATE.DISCONNECTED)
                    {
                        ActorItem actorItem = this.PossessionItemAdd(possessionItem1.PossessionedActor, possessionItem1.PossessionedActor.PossessionPosition, "");
                        possessionItem1.PossessionedActor.PossessionTarget = actorItem.ActorID;
                        MapServer.charDB.SaveChar(possessionItem1.PossessionedActor, false, false);
                        MapServer.accountDB.WriteUser(possessionItem1.PossessionedActor.Account);
                        return;
                    }
                }
                else
                {
                    if (!possessionItem1.PossessionedActor.Online)
                        possessionArg.fromID = uint.MaxValue;
                    this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.POSSESSION, (MapEventArgs)possessionArg, (SagaDB.Actor.Actor)this.Character, true);
                }
                possessionItem1.PossessionedActor.PossessionTarget = 0U;
                possessionItem1.PossessionedActor.PossessionPosition = PossessionPosition.NONE;
                possessionItem1.PossessionedActor = (ActorPC)null;
                possessionItem1.PossessionOwner = (ActorPC)null;
                Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                this.SendPlayerInfo();
            }
        }

        /// <summary>
        /// The PossessionPerform.
        /// </summary>
        /// <param name="target">The target<see cref="ActorPC"/>.</param>
        /// <param name="position">The position<see cref="PossessionPosition"/>.</param>
        /// <param name="comment">The comment<see cref="string"/>.</param>
        public void PossessionPerform(ActorPC target, PossessionPosition position, string comment)
        {
            int num = this.TestPossesionPosition(target, position);
            if (num >= 0)
            {
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.POSSESSION, (MapEventArgs)new PossessionArg()
                {
                    fromID = this.Character.ActorID,
                    toID = target.ActorID,
                    result = num,
                    comment = comment
                }, (SagaDB.Actor.Actor)this.Character, true);
                string str = "";
                switch (position)
                {
                    case PossessionPosition.RIGHT_HAND:
                        str = Singleton<LocalManager>.Instance.Strings.POSSESSION_RIGHT;
                        break;
                    case PossessionPosition.LEFT_HAND:
                        str = Singleton<LocalManager>.Instance.Strings.POSSESSION_LEFT;
                        break;
                    case PossessionPosition.NECK:
                        str = Singleton<LocalManager>.Instance.Strings.POSSESSION_NECK;
                        break;
                    case PossessionPosition.CHEST:
                        str = Singleton<LocalManager>.Instance.Strings.POSSESSION_ARMOR;
                        break;
                }
                this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.POSSESSION_DONE, (object)str));
                if (target == this.Character)
                {
                    this.Character.PossessionTarget = this.PossessionItemAdd(this.Character, position, comment).ActorID;
                    this.Character.PossessionPosition = position;
                }
                else
                {
                    MapClient.FromActorPC(target).SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.POSSESSION_DONE, (object)str));
                    this.Character.PossessionTarget = target.ActorID;
                    this.Character.PossessionPosition = position;
                    this.GetPossessionItem(target, position).PossessionedActor = this.Character;
                }
                if (!this.Character.Tasks.ContainsKey("PossessionRecover"))
                {
                    PossessionRecover possessionRecover = new PossessionRecover(this);
                    this.Character.Tasks.Add("PossessionRecover", (MultiRunTask)possessionRecover);
                    possessionRecover.Activate();
                }
                Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
                Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                this.SendPlayerInfo();
                Singleton<StatusFactory>.Instance.CalcStatus(target);
                ((PCEventHandler)target.e).Client.SendPlayerInfo();
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_POSSESSION_RESULT()
                {
                    FromID = this.Character.ActorID,
                    ToID = uint.MaxValue,
                    Result = num
                });
        }

        /// <summary>
        /// The TestPossesionPosition.
        /// </summary>
        /// <param name="target">The target<see cref="ActorPC"/>.</param>
        /// <param name="pos">The pos<see cref="PossessionPosition"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int TestPossesionPosition(ActorPC target, PossessionPosition pos)
        {
            SagaDB.Item.Item obj = (SagaDB.Item.Item)null;
            if (this.Character.PossesionedActors.Count != 0 || (this.Character.Buff.憑依準備 || this.Character.PossessionTarget != 0U))
                return -1;
            if (target.Buff.憑依準備)
                return -17;
            if (this.Character.Marionette != null || target.Marionette != null)
                return -15;
            if (Math.Abs((int)target.Level - (int)this.Character.Level) > 30)
                return -4;
            if (target.PossessionTarget != 0U)
                return -16;
            if (this.chara.Race == PC_RACE.DEM)
                return -29;
            if (target.Race == PC_RACE.DEM && target.Form == DEM_FORM.MACHINA_FORM)
                return -31;
            if (Math.Abs((int)target.X - (int)this.Character.X) > 300 || Math.Abs((int)target.Y - (int)this.Character.Y) > 300)
                return -21;
            if (target.PossesionedActors.Count >= 3)
                return -8;
            if (this.Character.Pet != null && this.Character.Pet.Ride)
                return -27;
            switch (pos)
            {
                case PossessionPosition.RIGHT_HAND:
                    if (!target.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                        return -5;
                    obj = target.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                    break;
                case PossessionPosition.LEFT_HAND:
                    if (!target.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        return -5;
                    obj = target.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                    break;
                case PossessionPosition.NECK:
                    if (!target.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                        return -5;
                    obj = target.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE];
                    break;
                case PossessionPosition.CHEST:
                    if (!target.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                        return -5;
                    obj = target.Inventory.Equipments[EnumEquipSlot.UPPER_BODY];
                    break;
            }
            if (obj == null)
                return -5;
            if (obj.PossessionedActor != null)
                return -6;
            if (obj.BaseData.itemType == ItemType.CARD || obj.BaseData.itemType == ItemType.ARROW || obj.BaseData.itemType == ItemType.BULLET)
                return -7;
            return (int)pos;
        }

        /// <summary>
        /// The GetPossessionItem.
        /// </summary>
        /// <param name="target">The target<see cref="ActorPC"/>.</param>
        /// <param name="pos">The pos<see cref="PossessionPosition"/>.</param>
        /// <returns>The <see cref="SagaDB.Item.Item"/>.</returns>
        private SagaDB.Item.Item GetPossessionItem(ActorPC target, PossessionPosition pos)
        {
            SagaDB.Item.Item obj = (SagaDB.Item.Item)null;
            switch (pos)
            {
                case PossessionPosition.RIGHT_HAND:
                    if (target.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        obj = target.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND];
                        break;
                    }
                    break;
                case PossessionPosition.LEFT_HAND:
                    if (target.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                    {
                        obj = target.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                        break;
                    }
                    break;
                case PossessionPosition.NECK:
                    if (target.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                    {
                        obj = target.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE];
                        break;
                    }
                    break;
                case PossessionPosition.CHEST:
                    if (target.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                    {
                        obj = target.Inventory.Equipments[EnumEquipSlot.UPPER_BODY];
                        break;
                    }
                    break;
            }
            return obj;
        }

        /// <summary>
        /// The PossessionItemAdd.
        /// </summary>
        /// <param name="target">The target<see cref="ActorPC"/>.</param>
        /// <param name="position">The position<see cref="PossessionPosition"/>.</param>
        /// <param name="comment">The comment<see cref="string"/>.</param>
        /// <returns>The <see cref="ActorItem"/>.</returns>
        private ActorItem PossessionItemAdd(ActorPC target, PossessionPosition position, string comment)
        {
            SagaDB.Item.Item possessionItem = this.GetPossessionItem(target, position);
            if (possessionItem == null)
                return (ActorItem)null;
            possessionItem.PossessionedActor = target;
            possessionItem.PossessionOwner = target;
            ActorItem actorItem = new ActorItem(possessionItem);
            actorItem.e = (ActorEventHandler)new ItemEventHandler((SagaDB.Actor.Actor)actorItem);
            actorItem.MapID = target.MapID;
            actorItem.X = target.X;
            actorItem.Y = target.Y;
            actorItem.Comment = comment;
            this.Map.RegisterActor((SagaDB.Actor.Actor)actorItem);
            actorItem.invisble = false;
            this.Map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorItem);
            return actorItem;
        }

        /// <summary>
        /// The GetPossessionTarget.
        /// </summary>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        private ActorPC GetPossessionTarget()
        {
            if (this.Character.PossessionTarget == 0U)
                return (ActorPC)null;
            SagaDB.Actor.Actor actor = this.Map.GetActor(this.Character.PossessionTarget);
            if (actor == null || actor.type != ActorType.PC)
                return (ActorPC)null;
            return (ActorPC)actor;
        }

        /// <summary>
        /// The PossessionPrepareCancel.
        /// </summary>
        private void PossessionPrepareCancel()
        {
            if (!this.Character.Buff.憑依準備)
                return;
            this.Character.Buff.憑依準備 = false;
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
            if (this.Character.Tasks.ContainsKey("Possession"))
            {
                this.Character.Tasks["Possession"].Deactivate();
                this.Character.Tasks.Remove("Possession");
            }
        }

        /// <summary>
        /// The OnQuestDetailRequest.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_QUEST_DETAIL_REQUEST"/>.</param>
        public void OnQuestDetailRequest(CSMG_QUEST_DETAIL_REQUEST p)
        {
            if (!Factory<QuestFactory, QuestInfo>.Instance.Items.ContainsKey(p.QuestID))
                return;
            QuestInfo questInfo = Factory<QuestFactory, QuestInfo>.Instance.Items[p.QuestID];
            uint mapid1 = 0;
            uint mapid2 = 0;
            uint mapid3 = 0;
            string info1 = " ";
            string info2 = " ";
            string info3 = " ";
            NPC npc1 = (NPC)null;
            NPC npc2 = (NPC)null;
            NPC npc3 = (NPC)null;
            if (Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(questInfo.NPCSource))
                npc2 = Factory<NPCFactory, NPC>.Instance.Items[questInfo.NPCSource];
            if (Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(questInfo.NPCDestination))
                npc3 = Factory<NPCFactory, NPC>.Instance.Items[questInfo.NPCDestination];
            if (npc1 != null)
            {
                mapid1 = npc1.MapID;
                info1 = npc1.Name;
            }
            if (npc2 != null)
            {
                mapid2 = npc2.MapID;
                info2 = npc2.Name;
            }
            if (npc3 != null)
            {
                mapid3 = npc3.MapID;
                info3 = npc3.Name;
            }
            SSMG_QUEST_DETAIL ssmgQuestDetail = new SSMG_QUEST_DETAIL();
            ssmgQuestDetail.SetDetail(questInfo.QuestType, questInfo.Name, mapid1, mapid2, mapid3, info1, info2, info3, questInfo.MapID1, questInfo.MapID2, questInfo.MapID3, questInfo.ObjectID1, questInfo.ObjectID2, questInfo.ObjectID3, (uint)questInfo.Count1, (uint)questInfo.Count2, (uint)questInfo.Count3, questInfo.TimeLimit, 0U);
            this.netIO.SendPacket((Packet)ssmgQuestDetail);
        }

        /// <summary>
        /// The OnQuestSelect.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_QUEST_SELECT"/>.</param>
        public void OnQuestSelect(CSMG_QUEST_SELECT p)
        {
            this.questID = p.QuestID;
        }

        /// <summary>
        /// The SendQuestInfo.
        /// </summary>
        public void SendQuestInfo()
        {
            Quest quest = this.Character.Quest;
            uint mapid1 = 0;
            uint mapid2 = 0;
            uint mapid3 = 0;
            string info1 = " ";
            string info2 = " ";
            string info3 = " ";
            if (quest == null)
                return;
            SSMG_QUEST_ACTIVATE ssmgQuestActivate = new SSMG_QUEST_ACTIVATE();
            NPC npc1 = (NPC)null;
            NPC npc2 = (NPC)null;
            NPC npc3 = quest.NPC;
            if (Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(quest.Detail.NPCSource))
                npc1 = Factory<NPCFactory, NPC>.Instance.Items[quest.Detail.NPCSource];
            if (Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(quest.Detail.NPCDestination))
                npc2 = Factory<NPCFactory, NPC>.Instance.Items[quest.Detail.NPCDestination];
            if (npc3 != null)
            {
                mapid1 = npc3.MapID;
                info1 = npc3.Name;
            }
            if (npc1 != null)
            {
                mapid2 = npc1.MapID;
                info2 = npc1.Name;
            }
            if (npc2 != null)
            {
                mapid3 = npc2.MapID;
                info3 = npc2.Name;
            }
            ssmgQuestActivate.SetDetail(quest.QuestType, quest.Name, mapid1, mapid2, mapid3, info1, info2, info3, quest.Status, quest.Detail.MapID1, quest.Detail.MapID2, quest.Detail.MapID3, quest.Detail.ObjectID1, quest.Detail.ObjectID2, quest.Detail.ObjectID3, (uint)quest.Detail.Count1, (uint)quest.Detail.Count2, (uint)quest.Detail.Count3, quest.Detail.TimeLimit, 0U);
            this.netIO.SendPacket((Packet)ssmgQuestActivate);
        }

        /// <summary>
        /// The SendQuestPoints.
        /// </summary>
        public void SendQuestPoints()
        {
            SSMG_QUEST_POINT ssmgQuestPoint = new SSMG_QUEST_POINT();
            if (this.Character.QuestNextResetTime > DateTime.Now)
            {
                ssmgQuestPoint.ResetTime = (uint)(this.Character.QuestNextResetTime - DateTime.Now).TotalHours;
            }
            else
            {
                int totalHours = (int)(DateTime.Now - this.Character.QuestNextResetTime).TotalHours;
                if (totalHours > 24000)
                {
                    this.Character.QuestNextResetTime = DateTime.Now + new TimeSpan(0, Singleton<Configuration>.Instance.QuestUpdateTime, 0, 0);
                }
                else
                {
                    this.Character.QuestRemaining += (ushort)((totalHours / Singleton<Configuration>.Instance.QuestUpdateTime + 1) * Singleton<Configuration>.Instance.QuestUpdateAmount);
                    if ((int)this.Character.QuestRemaining > Singleton<Configuration>.Instance.QuestPointsMax)
                        this.Character.QuestRemaining = (ushort)Singleton<Configuration>.Instance.QuestPointsMax;
                    this.Character.QuestNextResetTime += new TimeSpan(0, (totalHours / Singleton<Configuration>.Instance.QuestUpdateTime + 1) * Singleton<Configuration>.Instance.QuestUpdateTime, 0, 0);
                }
                ssmgQuestPoint.ResetTime = (uint)(this.Character.QuestNextResetTime - DateTime.Now).TotalHours;
            }
            ssmgQuestPoint.QuestPoint = this.Character.QuestRemaining;
            this.netIO.SendPacket((Packet)ssmgQuestPoint);
        }

        /// <summary>
        /// The SendQuestCount.
        /// </summary>
        public void SendQuestCount()
        {
            if (this.Character.Quest == null)
                return;
            this.netIO.SendPacket((Packet)new SSMG_QUEST_COUNT_UPDATE()
            {
                Count1 = this.Character.Quest.CurrentCount1,
                Count2 = this.Character.Quest.CurrentCount2,
                Count3 = this.Character.Quest.CurrentCount3
            });
            if (this.Character.Quest.Status != QuestStatus.FAILED && (this.Character.Quest.CurrentCount1 == this.Character.Quest.Detail.Count1 && this.Character.Quest.CurrentCount2 == this.Character.Quest.Detail.Count2 && this.Character.Quest.CurrentCount3 == this.Character.Quest.Detail.Count3 && this.Character.Quest.QuestType != QuestType.TRANSPORT))
            {
                this.Character.Quest.Status = QuestStatus.COMPLETED;
                this.SendQuestStatus();
            }
        }

        /// <summary>
        /// The SendQuestTime.
        /// </summary>
        public void SendQuestTime()
        {
            if (this.Character.Quest == null)
                return;
            SSMG_QUEST_RESTTIME_UPDATE questResttimeUpdate = new SSMG_QUEST_RESTTIME_UPDATE();
            if (this.Character.Quest.EndTime > DateTime.Now)
                questResttimeUpdate.RestTime = (int)(this.Character.Quest.EndTime - DateTime.Now).TotalMinutes;
            else if (this.Character.Quest.Status != QuestStatus.COMPLETED)
            {
                this.Character.Quest.Status = QuestStatus.FAILED;
                this.SendQuestStatus();
            }
            this.netIO.SendPacket((Packet)questResttimeUpdate);
        }

        /// <summary>
        /// The SendQuestStatus.
        /// </summary>
        public void SendQuestStatus()
        {
            if (this.Character.Quest == null)
                return;
            this.netIO.SendPacket((Packet)new SSMG_QUEST_STATUS_UPDATE()
            {
                Status = this.Character.Quest.Status
            });
        }

        /// <summary>
        /// The SendQuestList.
        /// </summary>
        /// <param name="quests">The quests<see cref="List{QuestInfo}"/>.</param>
        public void SendQuestList(List<QuestInfo> quests)
        {
            this.netIO.SendPacket((Packet)new SSMG_QUEST_LIST()
            {
                Quests = quests
            });
        }

        /// <summary>
        /// The SendQuestWindow.
        /// </summary>
        public void SendQuestWindow()
        {
            this.netIO.SendPacket((Packet)new SSMG_QUEST_WINDOW());
        }

        /// <summary>
        /// The SendQuestDelete.
        /// </summary>
        public void SendQuestDelete()
        {
            this.netIO.SendPacket((Packet)new SSMG_QUEST_DELETE());
        }

        /// <summary>
        /// The QuestMobKilled.
        /// </summary>
        /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
        /// <param name="party">The party<see cref="bool"/>.</param>
        public void QuestMobKilled(ActorMob mob, bool party)
        {
            if (this.Character.Quest == null || this.Character.Quest.QuestType != QuestType.HUNT || party && !this.Character.Quest.Detail.Party || (int)mob.MapID != (int)this.Character.Quest.Detail.MapID1 && (int)mob.MapID != (int)this.Character.Quest.Detail.MapID2 && (int)mob.MapID != (int)this.Character.Quest.Detail.MapID3 && (this.Character.Quest.Detail.MapID1 != 0U || this.Character.Quest.Detail.MapID2 != 0U || this.Character.Quest.Detail.MapID3 != 0U) && ((this.Character.Quest.Detail.MapID1 != 60000000U || !this.map.IsDungeon) && ((int)this.Character.Quest.Detail.MapID1 != (int)(this.map.ID / 1000U) * 1000 || !this.map.IsMapInstance) && ((int)this.Character.Quest.Detail.MapID2 != (int)(this.map.ID / 1000U) * 1000 || !this.map.IsMapInstance)) && ((int)this.Character.Quest.Detail.MapID3 != (int)(this.map.ID / 1000U) * 1000 || !this.map.IsMapInstance))
                return;
            if ((int)this.Character.Quest.Detail.ObjectID1 == (int)mob.MobID)
                ++this.Character.Quest.CurrentCount1;
            if (this.Character.Quest.Detail.ObjectID1 == 0U && this.Character.Quest.Detail.Count1 != 0)
                ++this.Character.Quest.CurrentCount1;
            if ((int)this.Character.Quest.Detail.ObjectID2 == (int)mob.MobID)
                ++this.Character.Quest.CurrentCount2;
            if ((int)this.Character.Quest.Detail.ObjectID3 == (int)mob.MobID)
                ++this.Character.Quest.CurrentCount3;
            if (this.Character.Quest.CurrentCount1 > this.Character.Quest.Detail.Count1)
                this.Character.Quest.CurrentCount1 = this.Character.Quest.Detail.Count1;
            if (this.Character.Quest.CurrentCount2 > this.Character.Quest.Detail.Count2)
                this.Character.Quest.CurrentCount2 = this.Character.Quest.Detail.Count2;
            if (this.Character.Quest.CurrentCount3 > this.Character.Quest.Detail.Count3)
                this.Character.Quest.CurrentCount3 = this.Character.Quest.Detail.Count3;
            this.SendQuestCount();
        }

        /// <summary>
        /// Gets or sets the Character.
        /// </summary>
        public ActorPC Character
        {
            get
            {
                return this.chara;
            }
            set
            {
                this.chara = value;
            }
        }

        /// <summary>
        /// Gets or sets the Map.
        /// </summary>
        public SagaMap.Map Map
        {
            get
            {
                return this.map;
            }
            set
            {
                this.map = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MapClient"/> class.
        /// </summary>
        /// <param name="mSock">The mSock<see cref="Socket"/>.</param>
        /// <param name="mCommandTable">The mCommandTable<see cref="Dictionary{ushort, Packet}"/>.</param>
        public MapClient(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, (SagaLib.Client)this);
            this.netIO.SetMode(NetIO.Mode.Server);
            this.netIO.FirstLevelLength = (ushort)2;
            if (!this.netIO.sock.Connected)
                return;
            this.OnConnect();
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            try
            {
                string str1 = "";
                string str2 = "";
                if (this.netIO != null)
                    str1 = this.netIO.sock.RemoteEndPoint.ToString();
                if (this.chara != null)
                    str2 = this.chara.Name;
                if (str1 != "" || str2 != "")
                    return string.Format("{0}({1})", (object)str2, (object)str1);
                return nameof(MapClient);
            }
            catch (Exception ex)
            {
                return nameof(MapClient);
            }
        }

        /// <summary>
        /// The FromActorPC.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="MapClient"/>.</returns>
        public static MapClient FromActorPC(ActorPC pc)
        {
            return ((PCEventHandler)pc.e).Client;
        }

        /// <summary>
        /// The OnConnect.
        /// </summary>
        public override void OnConnect()
        {
        }

        /// <summary>
        /// The SendHack.
        /// </summary>
        private void SendHack()
        {
            try
            {
                if ((this.hackStamp - DateTime.Now).TotalMinutes >= 10.0)
                {
                    this.hackCount = 0;
                    this.hackStamp = DateTime.Now;
                }
                ++this.hackCount;
                if (this.hackCount > 10)
                {
                    foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                        mapClient.SendAnnounce("WPE防卫娘：" + this.Character.Name + "酱，H(ack)是不行的哦！WPE什么的最讨厌了>_<");
                    this.netIO.Disconnect();
                }
                else
                {
                    if (this.hackCount <= 2)
                        return;
                    this.SendSystemMessage("WPE防卫娘：你、你、你好像在H(ack)了!那、那个，要是现在就停止的话，暂时就放过你,哼～~(ˇˍˇ）");
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// The OnDisconnect.
        /// </summary>
        public override void OnDisconnect()
        {
            this.npcSelectResult = 0;
            this.npcShopClosed = true;
            try
            {
                this.state = MapClient.SESSION_STATE.DISCONNECTED;
                MapClientManager.Instance.Clients.Remove(this);
                if (this.scriptThread != null)
                {
                    try
                    {
                        this.scriptThread.Abort();
                    }
                    catch
                    {
                    }
                }
                if (this.Character == null)
                    return;
                this.Character.VisibleActors.Clear();
                Logger.ShowInfo(string.Format(Singleton<LocalManager>.Instance.Strings.PLAYER_LOG_OUT, (object)this.Character.Name));
                Logger.ShowInfo(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + (object)MapClientManager.Instance.OnlinePlayer.Count);
                MapServer.shouldRefreshStatistic = true;
                if (this.Character.HP == 0U)
                {
                    this.Character.HP = 1U;
                    if (this.Character.SaveMap == 0U)
                    {
                        this.Character.SaveMap = 10023100U;
                        this.Character.SaveX = (byte)250;
                        this.Character.SaveY = (byte)132;
                    }
                    if (Singleton<Configuration>.Instance.HostedMaps.Contains(this.Character.SaveMap))
                    {
                        MapInfo mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[this.Character.SaveMap];
                        this.Character.MapID = this.Character.SaveMap;
                        this.Character.X = SagaLib.Global.PosX8to16(this.Character.SaveX, mapInfo.width);
                        this.Character.Y = SagaLib.Global.PosY8to16(this.Character.SaveY, mapInfo.height);
                    }
                }
                if (this.Character.FGarden != null)
                {
                    if (this.Character.FGarden.RopeActor != null)
                    {
                        Singleton<MapManager>.Instance.GetMap(this.Character.FGarden.RopeActor.MapID).DeleteActor((SagaDB.Actor.Actor)this.Character.FGarden.RopeActor);
                        if (Singleton<ScriptManager>.Instance.Events.ContainsKey(this.Character.FGarden.RopeActor.EventID))
                            Singleton<ScriptManager>.Instance.Events.Remove(this.Character.FGarden.RopeActor.EventID);
                        this.Character.FGarden.RopeActor = (ActorEvent)null;
                    }
                    if (this.Character.FGarden.RoomMapID != 0U)
                    {
                        SagaMap.Map map1 = Singleton<MapManager>.Instance.GetMap(this.Character.FGarden.RoomMapID);
                        SagaMap.Map map2 = Singleton<MapManager>.Instance.GetMap(this.Character.FGarden.MapID);
                        map1.ClientExitMap = map2.ClientExitMap;
                        map1.ClientExitX = map2.ClientExitX;
                        map1.ClientExitY = map2.ClientExitY;
                        Singleton<MapManager>.Instance.DeleteMapInstance(map1.ID);
                        this.Character.FGarden.RoomMapID = 0U;
                    }
                    if (this.Character.FGarden.MapID != 0U)
                    {
                        Singleton<MapManager>.Instance.DeleteMapInstance(this.Character.FGarden.MapID);
                        this.Character.FGarden.MapID = 0U;
                    }
                }
                if (this.Map.IsMapInstance && this.Character.PossessionTarget == 0U && !this.golemLogout)
                {
                    this.Character.MapID = this.Map.ClientExitMap;
                    SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(this.Map.ClientExitMap);
                    this.Character.X = SagaLib.Global.PosX8to16(this.Map.ClientExitX, map.Width);
                    this.Character.Y = SagaLib.Global.PosY8to16(this.Map.ClientExitY, map.Width);
                }
                this.Character.Online = false;
                if (this.logger != null)
                {
                    this.logger.Dispose();
                    this.logger = (SagaMap.PacketLogger.PacketLogger)null;
                }
                if (this.Character.Marionette != null)
                    this.MarionetteDeactivate(true);
                Singleton<RecruitmentManager>.Instance.DeleteRecruitment(this.Character);
                Singleton<PartyManager>.Instance.PlayerOffline(this.Character.Party, this.Character);
                Singleton<RingManager>.Instance.PlayerOffline(this.Character.Ring, this.Character);
                foreach (ActorPC possesionedActor in this.Character.PossesionedActors)
                {
                    SagaDB.Item.Item possessionItem = this.GetPossessionItem(this.Character, possesionedActor.PossessionPosition);
                    if (possessionItem.PossessionOwner != this.Character && possessionItem.PossessionOwner != null)
                    {
                        ActorItem actorItem = this.PossessionItemAdd(possesionedActor, possesionedActor.PossessionPosition, "");
                        possesionedActor.PossessionTarget = actorItem.ActorID;
                        int num = (int)this.Character.Inventory.DeleteItem(possessionItem.Slot, 1);
                        this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.POSSESSION, (MapEventArgs)new PossessionArg()
                        {
                            fromID = possesionedActor.ActorID,
                            toID = possesionedActor.ActorID,
                            result = (int)possesionedActor.PossessionPosition
                        }, (SagaDB.Actor.Actor)possesionedActor, true);
                    }
                    else if (possesionedActor != this.Character)
                    {
                        possesionedActor.PossessionTarget = 0U;
                        if (possesionedActor.Online)
                        {
                            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.POSSESSION, (MapEventArgs)new PossessionArg()
                            {
                                fromID = possesionedActor.ActorID,
                                toID = possesionedActor.PossessionTarget,
                                cancel = true,
                                result = (int)possesionedActor.PossessionPosition,
                                x = SagaLib.Global.PosX16to8(possesionedActor.X, this.Map.Width),
                                y = SagaLib.Global.PosY16to8(possesionedActor.Y, this.Map.Height),
                                dir = (byte)((uint)possesionedActor.Dir / 45U)
                            }, (SagaDB.Actor.Actor)possesionedActor, true);
                        }
                        else
                        {
                            if (Singleton<MapManager>.Instance.GetMap(possesionedActor.MapID) != null)
                                Singleton<MapManager>.Instance.GetMap(possesionedActor.MapID).DeleteActor((SagaDB.Actor.Actor)possesionedActor);
                            MapServer.charDB.SaveChar(possesionedActor, false, false);
                            MapServer.accountDB.WriteUser(possesionedActor.Account);
                            MapClient.FromActorPC(possesionedActor).DisposeActor();
                            possesionedActor.Account = (Account)null;
                            continue;
                        }
                    }
                    MapServer.charDB.SaveChar(possesionedActor, false, false);
                    MapServer.accountDB.WriteUser(possesionedActor.Account);
                }
                if (this.golemLogout && this.chara.PossessionTarget == 0U)
                {
                    this.Character.Golem.MapID = this.Character.MapID;
                    this.Character.Golem.X = this.Character.X;
                    this.Character.Golem.Y = this.Character.Y;
                    this.Character.Golem.Dir = this.Character.Dir;
                    this.Character.Golem.Owner = this.Character;
                    this.Character.Golem.e = (ActorEventHandler)new NullEventHandler();
                    if (this.Character.Golem.GolemType >= GolemType.Plant && this.Character.Golem.GolemType <= GolemType.Strange)
                    {
                        MobEventHandler mobEventHandler = new MobEventHandler((ActorMob)this.Character.Golem);
                        this.Character.Golem.e = (ActorEventHandler)mobEventHandler;
                        mobEventHandler.AI.Mode = new AIMode(0);
                        mobEventHandler.AI.X_Ori = this.Character.X;
                        mobEventHandler.AI.Y_Ori = this.Character.Y;
                        mobEventHandler.AI.X_Spawn = this.Character.X;
                        mobEventHandler.AI.Y_Spawn = this.Character.Y;
                        mobEventHandler.AI.MoveRange = (short)((int)this.map.Width * 100);
                        mobEventHandler.AI.Start();
                        if (this.Character.Golem.GolemType != GolemType.Buy)
                        {
                            GolemTask golemTask = new GolemTask(this.Character.Golem);
                            golemTask.Activate();
                            this.Character.Golem.Tasks.Add("GolemTask", (MultiRunTask)golemTask);
                        }
                    }
                    this.map.RegisterActor((SagaDB.Actor.Actor)this.Character.Golem);
                    this.Character.Golem.invisble = false;
                    --this.Character.Golem.Item.Durability;
                    this.map.OnActorVisibilityChange((SagaDB.Actor.Actor)this.Character.Golem);
                }
                this.chara.VisibleActors.Clear();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            this.Map.DeleteActor((SagaDB.Actor.Actor)this.Character);
            MapServer.charDB.SaveChar(this.Character);
            MapServer.accountDB.WriteUser(this.Character.Account);
            this.Character.Inventory.WareHouse = (Dictionary<WarehousePlace, List<SagaDB.Item.Item>>)null;
            if (this.Character.Pet != null)
                this.DeletePet();
            if (!this.golemLogout && this.Character.PossessionTarget == 0U)
            {
                this.DisposeActor();
            }
            else
            {
                foreach (string key in this.chara.Tasks.Keys)
                    this.chara.Tasks[key].Deactivate();
                this.chara.Tasks.Clear();
            }
        }

        /// <summary>
        /// The DisposeActor.
        /// </summary>
        public void DisposeActor()
        {
            this.Character.ClearTaskAddition();
            this.chara.Inventory = (Inventory)null;
            this.chara.Golem = (ActorGolem)null;
            this.chara.Stamp.Dispose();
            this.chara.FGarden = (SagaDB.FGarden.FGarden)null;
            this.chara.Status = (Status)null;
            this.chara.ClearVarialbes();
            this.chara.Marionette = (SagaDB.Marionette.Marionette)null;
            this.chara.NPCStates.Clear();
            this.chara.Skills = (Dictionary<uint, SagaDB.Skill.Skill>)null;
            this.chara.Skills2 = (Dictionary<uint, SagaDB.Skill.Skill>)null;
            this.chara.SkillsReserve = (Dictionary<uint, SagaDB.Skill.Skill>)null;
            this.chara.Elements.Clear();
            this.chara.Pet = (ActorPet)null;
            this.chara.Quest = (Quest)null;
            this.chara.Ring = (SagaDB.Ring.Ring)null;
            this.chara.Slave.Clear();
            this.chara.Account = (Account)null;
            this.chara = (ActorPC)null;
        }

        /// <summary>
        /// The OnNPCPetSelect.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_PET_SELECT"/>.</param>
        public void OnNPCPetSelect(CSMG_NPC_PET_SELECT p)
        {
            this.selectedPet = p.Result;
        }

        /// <summary>
        /// The OnVShopBuy.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_VSHOP_BUY"/>.</param>
        public void OnVShopBuy(CSMG_VSHOP_BUY p)
        {
            if (this.vshopClosed)
                return;
            uint[] items = p.Items;
            uint[] counts = p.Counts;
            uint[] numArray1 = new uint[items.Length];
            int[] numArray2 = new int[items.Length];
            int index1 = 0;
            uint num = 0;
            foreach (uint key in items)
            {
                if (Factory<ECOShopFactory, ShopCategory>.Instance.Items.ContainsKey(this.currentVShopCategory))
                {
                    ShopCategory shopCategory = Factory<ECOShopFactory, ShopCategory>.Instance.Items[this.currentVShopCategory];
                    if (shopCategory.Items.ContainsKey(key))
                    {
                        numArray1[index1] = shopCategory.Items[key].points;
                        numArray2[index1] = shopCategory.Items[key].rental;
                    }
                }
                ++index1;
            }
            for (int index2 = 0; index2 < items.Length; ++index2)
                num += numArray1[index2] * counts[index2];
            if (this.Character.VShopPoints >= num)
            {
                this.Character.UsedVShopPoints += num;
                this.Character.VShopPoints -= num;
                for (int index2 = 0; index2 < items.Length; ++index2)
                {
                    SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(items[index2]);
                    obj.Stack = (ushort)counts[index2];
                    if (numArray2[index2] > 0)
                    {
                        obj.Rental = true;
                        obj.RentalTime = DateTime.Now + new TimeSpan(0, numArray2[index2], 0);
                    }
                    Logger.LogItemGet(Logger.EventType.ItemVShopGet, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("VShopBuy Count:{0}", (object)obj.Stack), false);
                    this.AddItem(obj, true);
                }
            }
        }

        /// <summary>
        /// The OnVShopClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_VSHOP_CLOSE"/>.</param>
        public void OnVShopClose(CSMG_VSHOP_CLOSE p)
        {
            this.vshopClosed = true;
        }

        /// <summary>
        /// The OnVShopCategoryRequest.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_VSHOP_CATEGORY_REQUEST"/>.</param>
        public void OnVShopCategoryRequest(CSMG_VSHOP_CATEGORY_REQUEST p)
        {
            if (this.vshopClosed)
                return;
            ShopCategory shopCategory = Factory<ECOShopFactory, ShopCategory>.Instance.Items[p.Page + 1U];
            this.netIO.SendPacket((Packet)new SSMG_VSHOP_INFO_HEADER()
            {
                Page = p.Page
            });
            this.currentVShopCategory = p.Page + 1U;
            foreach (uint key in shopCategory.Items.Keys)
                this.netIO.SendPacket((Packet)new SSMG_VSHOP_INFO()
                {
                    Point = shopCategory.Items[key].points,
                    ItemID = key,
                    Comment = shopCategory.Items[key].comment
                });
            this.netIO.SendPacket((Packet)new SSMG_VSHOP_INFO_FOOTER());
        }

        /// <summary>
        /// The OnNPCJobSwitch.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_JOB_SWITCH"/>.</param>
        public void OnNPCJobSwitch(CSMG_NPC_JOB_SWITCH p)
        {
            if (!this.npcJobSwitch)
                return;
            this.npcJobSwitchRes = false;
            if (p.Unknown != 0)
            {
                this.npcJobSwitchRes = true;
                SagaDB.Item.Item obj = this.Character.Inventory.GetItem(Singleton<Configuration>.Instance.JobSwitchReduceItem, Inventory.SearchType.ITEM_ID);
                if (obj != null || p.ItemUseCount == 0U)
                {
                    if (obj != null)
                    {
                        if ((uint)obj.Stack < p.ItemUseCount)
                            return;
                        this.DeleteItem(obj.Slot, (ushort)p.ItemUseCount, true);
                    }
                    this.Character.SkillsReserve.Clear();
                    int num1 = 0;
                    if (this.Character.Job == this.Character.Job2X)
                        num1 = (int)this.Character.JobLevel2X / 10;
                    if (this.Character.Job == this.Character.Job2T)
                        num1 = (int)this.Character.JobLevel2T / 10;
                    if (num1 >= p.Skills.Length)
                    {
                        foreach (ushort skill in p.Skills)
                        {
                            if (this.Character.Skills2.ContainsKey((uint)skill))
                                this.Character.SkillsReserve.Add((uint)skill, this.Character.Skills2[(uint)skill]);
                        }
                    }
                    this.ResetSkill((byte)2);
                    if (this.Character.Job == this.Character.Job2X)
                    {
                        this.Character.Job = this.Character.Job2T;
                        int num2 = (int)((long)((int)this.Character.JobLevel2T / 5) - (long)p.ItemUseCount);
                        if (num2 <= 0)
                            num2 = 0;
                        if ((int)this.Character.SkillPoint2T > num2)
                            this.Character.SkillPoint2T -= (ushort)num2;
                        else
                            this.Character.SkillPoint2T = (ushort)0;
                        this.Character.JobLevel2T -= (byte)num2;
                        this.Character.JEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2T, LevelType.JLEVEL2T);
                    }
                    else
                    {
                        this.Character.Job = this.Character.Job2X;
                        int num2 = (int)((long)((int)this.Character.JobLevel2X / 5) - (long)p.ItemUseCount);
                        if (num2 <= 0)
                            num2 = 0;
                        if ((int)this.Character.SkillPoint2X > num2)
                            this.Character.SkillPoint2X -= (ushort)num2;
                        else
                            this.Character.SkillPoint2X = (ushort)0;
                        this.Character.JobLevel2X -= (byte)num2;
                        this.Character.JEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2X, LevelType.JLEVEL2);
                    }
                    Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                    this.SendPlayerInfo();
                    this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
                    {
                        effectID = 4131U,
                        actorID = this.Character.ActorID
                    }, (SagaDB.Actor.Actor)this.Character, true);
                }
            }
            this.npcJobSwitch = false;
        }

        /// <summary>
        /// The OnNPCInputBox.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_INPUTBOX"/>.</param>
        public void OnNPCInputBox(CSMG_NPC_INPUTBOX p)
        {
            this.inputContent = p.Content;
        }

        /// <summary>
        /// The OnNPCShopBuy.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_SHOP_BUY"/>.</param>
        public void OnNPCShopBuy(CSMG_NPC_SHOP_BUY p)
        {
            uint[] goods = p.Goods;
            uint[] counts = p.Counts;
            ushort stack;
            if (this.currentShop != null)
            {
                uint num1 = 0;
                switch (this.currentShop.ShopType)
                {
                    case ShopType.None:
                        num1 = (uint)this.Character.Gold;
                        break;
                    case ShopType.CP:
                        num1 = this.Character.CP;
                        break;
                    case ShopType.ECoin:
                        num1 = this.Character.ECoin;
                        break;
                }
                for (int index = 0; index < goods.Length; ++index)
                {
                    if (this.currentShop.Goods.Contains(goods[index]))
                    {
                        SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(goods[index]);
                        obj.Stack = (ushort)counts[index];
                        short num2 = 0;
                        if (this.currentShop.ShopType == ShopType.None)
                            num2 = this.chara.Status.buy_rate;
                        uint num3 = (uint)((double)obj.BaseData.price * ((double)((long)this.currentShop.SellRate + (long)num2) / 100.0));
                        if (num3 == 0U)
                            num3 = 1U;
                        uint num4 = num3 * (uint)obj.Stack;
                        if (num1 >= num4)
                        {
                            stack = obj.Stack;
                            num1 -= num4;
                            Logger.LogItemGet(Logger.EventType.ItemNPCGet, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("ShopBuy Count:{0}", (object)obj.Stack), false);
                            this.AddItem(obj, true);
                        }
                    }
                }
                switch (this.currentShop.ShopType)
                {
                    case ShopType.None:
                        this.Character.Gold = (int)num1;
                        break;
                    case ShopType.CP:
                        this.Character.CP = num1;
                        break;
                    case ShopType.ECoin:
                        this.Character.ECoin = num1;
                        break;
                }
                this.Character.Inventory.CalcPayloadVolume();
                this.SendCapacity();
            }
            else if (this.currentEvent != null)
            {
                int gold = this.Character.Gold;
                for (int index = 0; index < goods.Length; ++index)
                {
                    if (this.currentEvent.Goods.Contains(goods[index]))
                    {
                        SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(goods[index]);
                        obj.Stack = (ushort)counts[index];
                        int num1 = (int)((double)obj.BaseData.price * ((double)(100 + (int)this.Character.Status.buy_rate) / 100.0));
                        if (num1 == 0)
                            num1 = 1;
                        int num2 = num1 * (int)obj.Stack;
                        if (gold >= num2)
                        {
                            stack = obj.Stack;
                            gold -= num2;
                            Logger.LogItemGet(Logger.EventType.ItemNPCGet, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("AddItem Count:{0}", (object)obj.Stack), false);
                            this.AddItem(obj, true);
                        }
                    }
                }
                this.Character.Gold = gold;
                this.Character.Inventory.CalcPayloadVolume();
                this.SendCapacity();
            }
        }

        /// <summary>
        /// The OnNPCShopSell.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_SHOP_SELL"/>.</param>
        public void OnNPCShopSell(CSMG_NPC_SHOP_SELL p)
        {
            uint[] goods = p.Goods;
            uint[] counts = p.Counts;
            if (this.currentShop != null)
            {
                uint num1 = 0;
                for (int index = 0; index < goods.Length; ++index)
                {
                    SagaDB.Item.Item obj = this.Character.Inventory.GetItem(goods[index]);
                    if (obj == null)
                        return;
                    Logger.LogItemLost(Logger.EventType.ItemNPCLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("NPCShopSell Count:{0}", (object)counts[index]), false);
                    this.DeleteItem(goods[index], (ushort)counts[index], true);
                    uint num2 = (uint)((double)(obj.BaseData.price * counts[index]) * ((double)((long)this.currentShop.BuyRate + (long)this.Character.Status.sell_rate) / 100.0));
                    num1 += num2;
                }
                this.Character.Gold += (int)num1;
                this.Character.Inventory.CalcPayloadVolume();
                this.SendCapacity();
            }
            else if (this.currentEvent != null)
            {
                uint num1 = 0;
                for (int index = 0; index < goods.Length; ++index)
                {
                    SagaDB.Item.Item obj = this.Character.Inventory.GetItem(goods[index]);
                    if (obj == null)
                        return;
                    Logger.LogItemLost(Logger.EventType.ItemNPCLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("NPCShopSell Count:{0}", (object)counts[index]), false);
                    this.DeleteItem(goods[index], (ushort)counts[index], true);
                    uint num2 = (uint)((double)(obj.BaseData.price * counts[index]) * ((double)(10 + (int)this.Character.Status.sell_rate) / 100.0));
                    num1 += num2;
                }
                this.Character.Gold += (int)num1;
                this.Character.Inventory.CalcPayloadVolume();
                this.SendCapacity();
            }
        }

        /// <summary>
        /// The OnNPCShopClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_SHOP_CLOSE"/>.</param>
        public void OnNPCShopClose(CSMG_NPC_SHOP_CLOSE p)
        {
            this.npcShopClosed = true;
        }

        /// <summary>
        /// The OnNPCSelect.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_SELECT"/>.</param>
        public void OnNPCSelect(CSMG_NPC_SELECT p)
        {
            this.npcSelectResult = (int)p.Result;
        }

        /// <summary>
        /// The OnNPCSynthese.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_SYNTHESE"/>.</param>
        public void OnNPCSynthese(CSMG_NPC_SYNTHESE p)
        {
            uint synId = p.SynID;
            uint count = p.Count;
            if (this.syntheseItem.ContainsKey(synId))
                return;
            this.syntheseItem.Add(synId, count);
        }

        /// <summary>
        /// The OnNPCSyntheseFinish.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_SYNTHESE_FINISH"/>.</param>
        public void OnNPCSyntheseFinish(CSMG_NPC_SYNTHESE_FINISH p)
        {
            this.syntheseFinished = true;
        }

        /// <summary>
        /// The OnNPCEventStart.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_NPC_EVENT_START"/>.</param>
        public void OnNPCEventStart(CSMG_NPC_EVENT_START p)
        {
            if (this.scriptThread == null)
            {
                if (p.EventID < 20000000U || p.EventID >= 4026531840U)
                {
                    if (p.EventID >= 11000000U)
                    {
                        if (Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(p.EventID))
                        {
                            NPC npc = Factory<NPCFactory, NPC>.Instance.Items[p.EventID];
                            uint num = !this.map.IsMapInstance ? this.map.ID : this.map.ID / 100U * 100U;
                            if ((int)npc.MapID == (int)num)
                            {
                                if (Math.Abs((int)this.Character.X - (int)SagaLib.Global.PosX8to16(npc.X, this.map.Width)) > 700 || Math.Abs((int)this.Character.Y - (int)SagaLib.Global.PosY8to16(npc.Y, this.map.Height)) > 700)
                                {
                                    this.SendEventStart();
                                    this.SendCurrentEvent(p.EventID);
                                    this.SendEventEnd();
                                    return;
                                }
                            }
                            else
                            {
                                this.SendEventStart();
                                this.SendCurrentEvent(p.EventID);
                                this.SendEventEnd();
                                return;
                            }
                        }
                    }
                    else if (p.EventID != 10000315U && p.EventID != 10000316U)
                    {
                        if (this.map.Info.events.ContainsKey(p.EventID))
                        {
                            byte[] numArray = this.map.Info.events[p.EventID];
                            byte num1 = SagaLib.Global.PosX16to8(this.chara.X, this.map.Width);
                            byte num2 = SagaLib.Global.PosY16to8(this.chara.Y, this.map.Height);
                            bool flag = false;
                            for (int index = 0; index < numArray.Length / 2; ++index)
                            {
                                if (Math.Abs((int)numArray[index * 2] - (int)num1) <= 3 && Math.Abs((int)numArray[index * 2 + 1] - (int)num2) <= 3)
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            if (!flag)
                            {
                                this.SendHack();
                                this.SendEventStart();
                                this.SendCurrentEvent(p.EventID);
                                this.SendEventEnd();
                                return;
                            }
                        }
                        else
                        {
                            this.SendHack();
                            this.SendEventStart();
                            this.SendCurrentEvent(p.EventID);
                            this.SendEventEnd();
                            return;
                        }
                    }
                    this.EventActivate(p.EventID);
                }
                else
                {
                    this.SendEventStart();
                    this.SendCurrentEvent(p.EventID);
                    this.SendEventEnd();
                }
            }
            else
            {
                this.SendEventStart();
                this.SendCurrentEvent(p.EventID);
                this.SendEventEnd();
            }
        }

        /// <summary>
        /// The EventActivate.
        /// </summary>
        /// <param name="EventID">The EventID<see cref="uint"/>.</param>
        public void EventActivate(uint EventID)
        {
            if (Singleton<ScriptManager>.Instance.Events.ContainsKey(EventID))
            {
                Thread thread = new Thread(new ThreadStart(this.RunScript));
                thread.Name = string.Format("ScriptThread({0}) of player:{1}", (object)thread.ManagedThreadId, (object)this.Character.Name);
                ClientManager.AddThread(thread);
                if (this.scriptThread != null)
                {
                    Logger.ShowDebug("current script thread != null, currently running:" + this.currentEventID.ToString(), Logger.defaultlogger);
                    this.scriptThread.Abort();
                }
                this.currentEventID = EventID;
                this.scriptThread = thread;
                thread.Start();
            }
            else
            {
                this.SendEventStart();
                this.SendCurrentEvent(EventID);
                this.SendNPCMessageStart();
                if (this.account.GMLevel > (byte)0)
                    this.SendNPCMessage(EventID, string.Format(Singleton<LocalManager>.Instance.Strings.NPC_EventID_NotFound, (object)EventID), (ushort)131, "System Error");
                else
                    this.SendNPCMessage(EventID, string.Format(Singleton<LocalManager>.Instance.Strings.NPC_EventID_NotFound_Msg, (object)EventID), (ushort)131, "");
                this.SendNPCMessageEnd();
                this.SendEventEnd();
                Logger.ShowWarning("No script loaded for EventID:" + (object)EventID);
            }
        }

        /// <summary>
        /// The RunScript.
        /// </summary>
        private void RunScript()
        {
            ClientManager.EnterCriticalArea();
            Event @event = (Event)null;
            try
            {
                @event = Singleton<ScriptManager>.Instance.Events[this.currentEventID];
                if (this.currentEventID < 4294901760U)
                {
                    this.SendEventStart();
                    this.SendCurrentEvent(this.currentEventID);
                }
                this.currentEvent = @event;
                this.currentEvent.CurrentPC = this.Character;
                bool flag = true;
                if (this.Character.Quest != null)
                {
                    if ((int)this.Character.Quest.Detail.NPCSource == (int)@event.EventID)
                    {
                        if (this.Character.Quest.CurrentCount1 == 0 && this.Character.Quest.Status == QuestStatus.OPEN)
                        {
                            this.Character.Quest.CurrentCount1 = 1;
                            @event.OnTransportSource(this.Character);
                            @event.OnQuestUpdate(this.Character, this.Character.Quest);
                            flag = false;
                        }
                        else if (this.Character.Quest.CurrentCount2 == 1)
                        {
                            @event.OnTransportCompleteSrc(this.Character);
                            flag = false;
                        }
                    }
                    if ((int)this.Character.Quest.Detail.NPCDestination == (int)@event.EventID)
                    {
                        if (this.Character.Quest.CurrentCount2 == 0 && this.Character.Quest.Status == QuestStatus.OPEN)
                        {
                            @event.OnTransportDest(this.Character);
                            if (this.Character.Quest.CurrentCount3 == 0)
                            {
                                this.Character.Quest.CurrentCount2 = 1;
                                this.Character.Quest.Status = QuestStatus.COMPLETED;
                                @event.OnQuestUpdate(this.Character, this.Character.Quest);
                                this.SendQuestStatus();
                                flag = false;
                            }
                        }
                        else
                        {
                            @event.OnTransportCompleteDest(this.Character);
                            flag = false;
                        }
                    }
                }
                if (flag)
                    this.currentEvent.OnEvent(this.Character);
                if (this.currentEventID < 4294901760U)
                    this.SendEventEnd();
            }
            catch (ThreadAbortException ex)
            {
                try
                {
                    this.scriptThread = (Thread)null;
                    if (@event != null)
                        @event.CurrentPC = (ActorPC)null;
                    this.currentEvent = (Event)null;
                    ClientManager.RemoveThread(Thread.CurrentThread.Name);
                    if (this.Character != null)
                        Logger.ShowWarning(string.Format("Player:{0} logged out while script thread is still running, terminating the script thread!", (object)this.Character.Name));
                }
                catch
                {
                }
                ClientManager.LeaveCriticalArea();
            }
            catch (Exception ex)
            {
                try
                {
                    if (this.Character.Online)
                    {
                        if (this.Character.Account.GMLevel > (byte)2)
                        {
                            this.SendNPCMessageStart();
                            this.SendNPCMessage(this.currentEventID, "Script Error(" + Singleton<ScriptManager>.Instance.Events[this.currentEventID].ToString() + "):" + ex.Message, (ushort)131, "System Error");
                            this.SendNPCMessageEnd();
                        }
                        this.SendEventEnd();
                    }
                    Logger.ShowWarning("Script Error(" + Singleton<ScriptManager>.Instance.Events[this.currentEventID].ToString() + "):" + ex.Message + "\r\n" + ex.StackTrace);
                }
                catch
                {
                }
            }
            if (@event != null)
                @event.CurrentPC = (ActorPC)null;
            this.scriptThread = (Thread)null;
            this.currentEvent = (Event)null;
            ClientManager.RemoveThread(Thread.CurrentThread.Name);
            ClientManager.LeaveCriticalArea();
        }

        /// <summary>
        /// The SendEventStart.
        /// </summary>
        private void SendEventStart()
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_NPC_EVENT_START());
        }

        /// <summary>
        /// The SendEventEnd.
        /// </summary>
        private void SendEventEnd()
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_NPC_EVENT_END());
        }

        /// <summary>
        /// The SendCurrentEvent.
        /// </summary>
        /// <param name="eventid">The eventid<see cref="uint"/>.</param>
        public void SendCurrentEvent(uint eventid)
        {
            this.netIO.SendPacket((Packet)new SSMG_NPC_CURRENT_EVENT()
            {
                EventID = eventid
            });
        }

        /// <summary>
        /// The SendNPCMessageStart.
        /// </summary>
        public void SendNPCMessageStart()
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_NPC_MESSAGE_START());
        }

        /// <summary>
        /// The SendNPCMessageEnd.
        /// </summary>
        public void SendNPCMessageEnd()
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_NPC_MESSAGE_END());
        }

        /// <summary>
        /// The SendNPCMessage.
        /// </summary>
        /// <param name="npcID">The npcID<see cref="uint"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="motion">The motion<see cref="ushort"/>.</param>
        /// <param name="title">The title<see cref="string"/>.</param>
        public void SendNPCMessage(uint npcID, string message, ushort motion, string title)
        {
            if (!this.Character.Online)
                return;
            SSMG_NPC_MESSAGE ssmgNpcMessage = new SSMG_NPC_MESSAGE();
            ssmgNpcMessage.SetMessage(npcID, (byte)1, message, motion, title);
            this.netIO.SendPacket((Packet)ssmgNpcMessage);
        }

        /// <summary>
        /// The SendNPCWait.
        /// </summary>
        /// <param name="wait">The wait<see cref="uint"/>.</param>
        public void SendNPCWait(uint wait)
        {
            this.netIO.SendPacket((Packet)new SSMG_NPC_WAIT()
            {
                Wait = wait
            });
        }

        /// <summary>
        /// The SendNPCPlaySound.
        /// </summary>
        /// <param name="soundID">The soundID<see cref="uint"/>.</param>
        /// <param name="loop">The loop<see cref="byte"/>.</param>
        /// <param name="volume">The volume<see cref="uint"/>.</param>
        /// <param name="balance">The balance<see cref="byte"/>.</param>
        public void SendNPCPlaySound(uint soundID, byte loop, uint volume, byte balance)
        {
            this.netIO.SendPacket((Packet)new SSMG_NPC_PLAY_SOUND()
            {
                SoundID = soundID,
                Loop = loop,
                Volume = volume,
                Balance = balance
            });
        }

        /// <summary>
        /// The SendNPCShowEffect.
        /// </summary>
        /// <param name="actorID">The actorID<see cref="uint"/>.</param>
        /// <param name="x">The x<see cref="byte"/>.</param>
        /// <param name="y">The y<see cref="byte"/>.</param>
        /// <param name="effectID">The effectID<see cref="uint"/>.</param>
        /// <param name="oneTime">The oneTime<see cref="bool"/>.</param>
        public void SendNPCShowEffect(uint actorID, byte x, byte y, uint effectID, bool oneTime)
        {
            this.netIO.SendPacket((Packet)new SSMG_NPC_SHOW_EFFECT()
            {
                ActorID = actorID,
                EffectID = effectID,
                X = x,
                Y = y,
                OneTime = oneTime
            });
        }

        /// <summary>
        /// The SendNPCStates.
        /// </summary>
        public void SendNPCStates()
        {
            if (!this.Character.NPCStates.ContainsKey(this.map.ID))
                return;
            foreach (uint key in this.chara.NPCStates[this.map.ID].Keys)
            {
                if (this.chara.NPCStates[this.map.ID][key])
                    this.netIO.SendPacket((Packet)new SSMG_NPC_SHOW()
                    {
                        NPCID = key
                    });
                else
                    this.netIO.SendPacket((Packet)new SSMG_NPC_HIDE()
                    {
                        NPCID = key
                    });
            }
        }

        /// <summary>
        /// Sets the SkillDelay.
        /// </summary>
        public DateTime SkillDelay
        {
            set
            {
                this.skillDelay = value;
            }
        }

        /// <summary>
        /// The OnSkillLvUP.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SKILL_LEVEL_UP"/>.</param>
        public void OnSkillLvUP(CSMG_SKILL_LEVEL_UP p)
        {
            SSMG_SKILL_LEVEL_UP ssmgSkillLevelUp = new SSMG_SKILL_LEVEL_UP();
            ushort skillId = p.SkillID;
            byte num = 0;
            if (Singleton<SkillFactory>.Instance.SkillList(this.Character.JobBasic).ContainsKey((uint)skillId))
                num = (byte)1;
            else if (Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2X).ContainsKey((uint)skillId))
                num = (byte)2;
            else if (Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2T).ContainsKey((uint)skillId))
                num = (byte)3;
            if (num == (byte)0)
            {
                ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_EXIST;
            }
            else
            {
                if (num == (byte)1)
                {
                    if (!this.Character.Skills.ContainsKey((uint)skillId))
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    else if (this.Character.SkillPoint < (ushort)1)
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                    else if ((int)this.Character.Skills[(uint)skillId].Level == (int)this.Character.Skills[(uint)skillId].MaxLevel)
                    {
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                    }
                    else
                    {
                        --this.Character.SkillPoint;
                        this.Character.Skills[(uint)skillId] = Singleton<SkillFactory>.Instance.GetSkill((uint)skillId, (byte)((uint)this.Character.Skills[(uint)skillId].Level + 1U));
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                        ssmgSkillLevelUp.SkillID = skillId;
                    }
                }
                if (num == (byte)2)
                {
                    if (!this.Character.Skills2.ContainsKey((uint)skillId))
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    else if (this.Character.SkillPoint2X < (ushort)1)
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                    else if ((int)this.Character.Skills2[(uint)skillId].Level == (int)this.Character.Skills2[(uint)skillId].MaxLevel)
                    {
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                    }
                    else
                    {
                        --this.Character.SkillPoint2X;
                        this.Character.Skills2[(uint)skillId] = Singleton<SkillFactory>.Instance.GetSkill((uint)skillId, (byte)((uint)this.Character.Skills2[(uint)skillId].Level + 1U));
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                        ssmgSkillLevelUp.SkillID = skillId;
                    }
                }
                if (num == (byte)3)
                {
                    if (!this.Character.Skills2.ContainsKey((uint)skillId))
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    else if (this.Character.SkillPoint2T < (ushort)1)
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                    else if ((int)this.Character.Skills2[(uint)skillId].Level == (int)this.Character.Skills2[(uint)skillId].MaxLevel)
                    {
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                    }
                    else
                    {
                        --this.Character.SkillPoint2T;
                        this.Character.Skills2[(uint)skillId] = Singleton<SkillFactory>.Instance.GetSkill((uint)skillId, (byte)((uint)this.Character.Skills2[(uint)skillId].Level + 1U));
                        ssmgSkillLevelUp.Result = SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                        ssmgSkillLevelUp.SkillID = skillId;
                    }
                }
            }
            ssmgSkillLevelUp.SkillPoints = this.Character.SkillPoint;
            if (this.Character.Job == this.Character.Job2X)
            {
                ssmgSkillLevelUp.SkillPoints2 = this.Character.SkillPoint2X;
                ssmgSkillLevelUp.Job = (byte)1;
            }
            else if (this.Character.Job == this.Character.Job2T)
            {
                ssmgSkillLevelUp.SkillPoints2 = this.Character.SkillPoint2T;
                ssmgSkillLevelUp.Job = (byte)2;
            }
            else
                ssmgSkillLevelUp.Job = (byte)0;
            this.netIO.SendPacket((Packet)ssmgSkillLevelUp);
            this.SendSkillList();
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
            this.SendPlayerInfo();
        }

        /// <summary>
        /// The OnSkillLearn.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SKILL_LEARN"/>.</param>
        public void OnSkillLearn(CSMG_SKILL_LEARN p)
        {
            SSMG_SKILL_LEARN ssmgSkillLearn = new SSMG_SKILL_LEARN();
            ushort skillId = p.SkillID;
            byte num = 0;
            if (Singleton<SkillFactory>.Instance.SkillList(this.Character.JobBasic).ContainsKey((uint)skillId))
                num = (byte)1;
            else if (Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2X).ContainsKey((uint)skillId))
                num = (byte)2;
            else if (Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2T).ContainsKey((uint)skillId))
                num = (byte)3;
            switch (num)
            {
                case 0:
                    ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_EXIST;
                    break;
                case 1:
                    byte skill1 = Singleton<SkillFactory>.Instance.SkillList(this.Character.JobBasic)[(uint)skillId];
                    if (this.Character.Skills.ContainsKey((uint)skillId))
                    {
                        ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                        goto default;
                    }
                    else
                    {
                        if (this.Character.SkillPoint < (ushort)3)
                        {
                            ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill2 = Singleton<SkillFactory>.Instance.GetSkill((uint)skillId, (byte)1);
                            if ((int)this.Character.JobLevel1 < (int)skill1)
                            {
                                ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }
                            else
                            {
                                this.Character.SkillPoint -= (ushort)3;
                                this.Character.Skills.Add((uint)skillId, skill2);
                                ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.OK;
                                ssmgSkillLearn.SkillID = skillId;
                                ssmgSkillLearn.SkillPoints = this.Character.SkillPoint;
                                if (this.Character.Job == this.Character.Job2X)
                                    ssmgSkillLearn.SkillPoints2 = this.Character.SkillPoint2X;
                                else if (this.Character.Job == this.Character.Job2T)
                                    ssmgSkillLearn.SkillPoints2 = this.Character.SkillPoint2T;
                            }
                        }
                        goto default;
                    }
                case 2:
                    byte skill3 = Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2X)[(uint)skillId];
                    if (this.Character.Skills2.ContainsKey((uint)skillId))
                    {
                        ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                        goto default;
                    }
                    else
                    {
                        if (this.Character.SkillPoint2X < (ushort)3)
                        {
                            ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill2 = Singleton<SkillFactory>.Instance.GetSkill((uint)skillId, (byte)1);
                            if ((int)this.Character.JobLevel2X < (int)skill3)
                            {
                                ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }
                            else
                            {
                                this.Character.SkillPoint2X -= (ushort)3;
                                this.Character.Skills2.Add((uint)skillId, skill2);
                                ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.OK;
                                ssmgSkillLearn.SkillID = skillId;
                                ssmgSkillLearn.SkillPoints = this.Character.SkillPoint;
                                ssmgSkillLearn.SkillPoints2 = this.Character.SkillPoint2X;
                            }
                        }
                        goto default;
                    }
                case 3:
                    byte skill4 = Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2T)[(uint)skillId];
                    if (this.Character.Skills2.ContainsKey((uint)skillId))
                        ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                    else if (this.Character.SkillPoint2T < (ushort)3)
                    {
                        ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                    }
                    else
                    {
                        SagaDB.Skill.Skill skill2 = Singleton<SkillFactory>.Instance.GetSkill((uint)skillId, (byte)1);
                        if ((int)this.Character.JobLevel2T < (int)skill4)
                        {
                            ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                        }
                        else
                        {
                            this.Character.SkillPoint2T -= (ushort)3;
                            this.Character.Skills2.Add((uint)skillId, skill2);
                            ssmgSkillLearn.Result = SSMG_SKILL_LEARN.LearnResult.OK;
                            ssmgSkillLearn.SkillID = skillId;
                            ssmgSkillLearn.SkillPoints = this.Character.SkillPoint;
                            ssmgSkillLearn.SkillPoints2 = this.Character.SkillPoint2T;
                        }
                    }
                    goto default;
                default:
                    {
                        break;
                    }
            }
            this.netIO.SendPacket((Packet)ssmgSkillLearn);
            this.SendSkillList();
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
            this.SendPlayerInfo();
        }

        /// <summary>
        /// The OnSkillAttack.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SKILL_ATTACK"/>.</param>
        public void OnSkillAttack(CSMG_SKILL_ATTACK p)
        {
            if ((int)p.Random == (int)this.lastAttackRandom && this.chara.Account.GMLevel < (byte)2)
            {
                this.SendHack();
                if (this.hackCount > 2)
                    return;
            }
            this.lastAttackRandom = p.Random;
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            SagaDB.Actor.Actor actor = this.Map.GetActor(p.ActorID);
            if (actor == null || DateTime.Now < this.attackStamp)
            {
                SkillArg skillArg = new SkillArg();
                skillArg.sActor = this.Character.ActorID;
                skillArg.type = (ATTACK_TYPE)255;
                skillArg.affectedActors.Add((SagaDB.Actor.Actor)this.Character);
                skillArg.Init();
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.ATTACK, (MapEventArgs)skillArg, (SagaDB.Actor.Actor)this.Character, true);
            }
            else if (actor.HP == 0U)
            {
                SkillArg skillArg = new SkillArg();
                skillArg.sActor = this.Character.ActorID;
                skillArg.type = (ATTACK_TYPE)255;
                skillArg.affectedActors.Add((SagaDB.Actor.Actor)this.Character);
                skillArg.Init();
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.ATTACK, (MapEventArgs)skillArg, (SagaDB.Actor.Actor)this.Character, true);
            }
            else
            {
                SkillArg skillArg = new SkillArg();
                Singleton<SkillHandler>.Instance.Attack((SagaDB.Actor.Actor)this.Character, actor, skillArg);
                if (skillArg.affectedActors.Count > 0)
                    this.attackStamp = DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)((double)skillArg.delay / (2.0 * (double)skillArg.affectedActors.Count) * 0.899999976158142));
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.ATTACK, (MapEventArgs)skillArg, (SagaDB.Actor.Actor)this.Character, true);
            }
        }

        /// <summary>
        /// The OnSkillChangeBattleStatus.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SKILL_CHANGE_BATTLE_STATUS"/>.</param>
        public void OnSkillChangeBattleStatus(CSMG_SKILL_CHANGE_BATTLE_STATUS p)
        {
            this.Character.BattleStatus = p.Status;
            this.SendChangeStatus();
        }

        /// <summary>
        /// The OnSkillCast.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SKILL_CAST"/>.</param>
        public void OnSkillCast(CSMG_SKILL_CAST p)
        {
            this.OnSkillCast(p, true);
        }

        /// <summary>
        /// The checkSkill.
        /// </summary>
        /// <param name="skillID">The skillID<see cref="uint"/>.</param>
        /// <param name="skillLV">The skillLV<see cref="byte"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool checkSkill(uint skillID, byte skillLV)
        {
            SSMG_SKILL_LIST ssmgSkillList = new SSMG_SKILL_LIST();
            List<SagaDB.Skill.Skill> skillList = new List<SagaDB.Skill.Skill>();
            bool flag = this.map.Info.Flag.Test(MapFlags.Dominion);
            Dictionary<uint, byte> dictionary1 = Singleton<SkillFactory>.Instance.SkillList(this.Character.JobBasic);
            Dictionary<uint, byte> dictionary2 = Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2X);
            Dictionary<uint, byte> dictionary3 = Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2T);
            if (!flag)
            {
                if (this.chara.Skills.ContainsKey(skillID) && (int)this.chara.Skills[skillID].Level >= (int)skillLV || this.chara.Skills2.ContainsKey(skillID) && (int)this.chara.Skills2[skillID].Level >= (int)skillLV || this.chara.SkillsReserve.ContainsKey(skillID) && (int)this.chara.SkillsReserve[skillID].Level >= (int)skillLV)
                    return true;
            }
            else
            {
                if (this.chara.Skills.ContainsKey(skillID) && (int)this.chara.Skills[skillID].Level >= (int)skillLV && (!dictionary1.ContainsKey(skillID) || (int)this.chara.DominionJobLevel >= (int)dictionary1[skillID]) || this.chara.Job == this.chara.Job2X && this.chara.Skills2.ContainsKey(skillID) && ((int)this.chara.Skills2[skillID].Level >= (int)skillLV && dictionary2.ContainsKey(skillID)) && (int)this.chara.DominionJobLevel >= (int)dictionary2[skillID])
                    return true;
                if (this.chara.Job == this.chara.Job2T && this.chara.Skills2.ContainsKey(skillID) && ((int)this.chara.Skills2[skillID].Level >= (int)skillLV && dictionary3.ContainsKey(skillID)) && (int)this.chara.DominionJobLevel >= (int)dictionary3[skillID])
                    return true;
                if (this.chara.SkillsReserve.ContainsKey(skillID) && this.Character.DominionReserveSkill && (int)this.chara.SkillsReserve[skillID].Level >= (int)skillLV)
                    return true;
            }
            if (this.Character.JobJoint != PC_JOB.NONE)
            {
                foreach (KeyValuePair<uint, byte> keyValuePair in Singleton<SkillFactory>.Instance.SkillList(this.Character.JobJoint).Where<KeyValuePair<uint, byte>>((Func<KeyValuePair<uint, byte>, bool>)(c => (int)c.Value <= (int)this.Character.JointJobLevel)))
                {
                    if ((int)keyValuePair.Key == (int)skillID && (int)this.chara.JointJobLevel >= (int)keyValuePair.Value)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// The OnSkillCast.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SKILL_CAST"/>.</param>
        /// <param name="useMPSP">The useMPSP<see cref="bool"/>.</param>
        public void OnSkillCast(CSMG_SKILL_CAST p, bool useMPSP)
        {
            this.OnSkillCast(p, useMPSP, false);
        }

        /// <summary>
        /// The OnSkillCast.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SKILL_CAST"/>.</param>
        /// <param name="useMPSP">The useMPSP<see cref="bool"/>.</param>
        /// <param name="nocheck">The nocheck<see cref="bool"/>.</param>
        public void OnSkillCast(CSMG_SKILL_CAST p, bool useMPSP, bool nocheck)
        {
            if ((!this.checkSkill((uint)p.SkillID, p.SkillLv) && this.chara.Account.GMLevel < (byte)2 || (int)p.Random == (int)this.lastCastRandom && this.chara.Account.GMLevel < (byte)2) && !nocheck)
            {
                this.SendHack();
                if (this.hackCount > 2)
                    return;
            }
            this.lastCastRandom = p.Random;
            SagaDB.Skill.Skill skill1 = Singleton<SkillFactory>.Instance.GetSkill((uint)p.SkillID, p.SkillLv);
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            SkillArg skill2 = new SkillArg();
            skill2.sActor = this.Character.ActorID;
            skill2.dActor = p.ActorID;
            skill2.skill = skill1;
            skill2.x = p.X;
            skill2.y = p.Y;
            skill2.argType = SkillArg.ArgType.Cast;
            ushort num1;
            ushort num2;
            if (this.Character.PossessionTarget != 0U)
            {
                num1 = (ushort)((uint)skill1.SP * 2U);
                num2 = (ushort)((uint)skill1.MP * 2U);
            }
            else
            {
                num1 = skill1.SP;
                num2 = skill1.MP;
            }
            if (this.Character.Status.zenList.Contains((ushort)skill1.ID) || this.Character.Status.darkZenList.Contains((ushort)skill1.ID))
                num2 *= (ushort)2;
            if (!useMPSP)
            {
                num1 = (ushort)0;
                num2 = (ushort)0;
            }
            skill2.useMPSP = useMPSP;
            if (DateTime.Now >= this.skillDelay || (int)this.nextCombo == (int)skill2.skill.ID)
            {
                if (this.Character.SP >= (uint)num1 && this.Character.MP >= (uint)num2)
                {
                    skill2.result = (short)Singleton<SkillHandler>.Instance.TryCast((SagaDB.Actor.Actor)this.Character, this.Map.GetActor(skill2.dActor), skill2);
                    if (this.GetPossessionTarget() != null && (this.GetPossessionTarget().Buff.Dead && skill2.skill.ID != 3055U))
                        skill2.result = (short)-27;
                    if (this.scriptThread != null)
                        skill2.result = (short)-59;
                    if (skill1.NoPossession && (this.chara.Buff.憑依準備 || this.chara.PossessionTarget != 0U))
                        skill2.result = (short)-25;
                    if (skill1.NotBeenPossessed && this.chara.PossesionedActors.Count > 0)
                        skill2.result = (short)-24;
                    if (this.Character.Tasks.ContainsKey("SkillCast"))
                        skill2.result = (short)-8;
                    if (skill2.result == (short)0)
                    {
                        skill2.delay = (uint)((double)skill1.CastTime * (1.0 - (double)((int)this.Character.Status.cspd + (int)this.Character.Status.cspd_skill) / 1000.0));
                        if (this.Character.Status.delayCancelList.ContainsKey((ushort)skill2.skill.ID))
                        {
                            int delayCancel = this.Character.Status.delayCancelList[(ushort)skill2.skill.ID];
                            skill2.delay = (uint)((double)skill2.delay * (1.0 - (double)delayCancel / 100.0));
                        }
                        this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SKILL, (MapEventArgs)skill2, (SagaDB.Actor.Actor)this.Character, true);
                        if (skill1.CastTime > 0 && (int)this.nextCombo != (int)skill2.skill.ID)
                        {
                            SkillCast skillCast = new SkillCast(this, skill2);
                            this.Character.Tasks.Add("SkillCast", (MultiRunTask)skillCast);
                            skillCast.Activate();
                            this.nextCombo = 0U;
                        }
                        else
                        {
                            this.nextCombo = 0U;
                            this.OnSkillCastComplete(skill2);
                        }
                    }
                    else
                        this.Character.e.OnActorSkillUse((SagaDB.Actor.Actor)this.Character, (MapEventArgs)skill2);
                }
                else
                {
                    int num3 = this.Character.SP >= (uint)num1 ? 1 : (this.Character.MP >= (uint)num2 ? 1 : 0);
                    skill2.result = num3 != 0 ? (this.Character.SP >= (uint)num1 ? (short)-15 : (short)-16) : (short)-1;
                    this.Character.e.OnActorSkillUse((SagaDB.Actor.Actor)this.Character, (MapEventArgs)skill2);
                }
            }
            else
            {
                skill2.result = (short)-30;
                this.Character.e.OnActorSkillUse((SagaDB.Actor.Actor)this.Character, (MapEventArgs)skill2);
            }
        }

        /// <summary>
        /// The OnSkillCastComplete.
        /// </summary>
        /// <param name="skill">The skill<see cref="SkillArg"/>.</param>
        public void OnSkillCastComplete(SkillArg skill)
        {
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            if (skill.dActor != uint.MaxValue)
            {
                SagaDB.Actor.Actor actor = this.Map.GetActor(skill.dActor);
                if (actor != null)
                {
                    skill.argType = SkillArg.ArgType.Active;
                    if (skill.useMPSP)
                    {
                        uint num1 = (uint)skill.skill.MP;
                        uint num2 = (uint)skill.skill.SP;
                        if (this.Character.Status.zenList.Contains((ushort)skill.skill.ID) || this.Character.Status.darkZenList.Contains((ushort)skill.skill.ID))
                            num1 = (uint)(ushort)(num1 * 2U);
                        if (this.Character.Status.doubleUpList.Contains((ushort)skill.skill.ID))
                            num2 = (uint)(ushort)(num2 * 2U);
                        if (this.Character.PossessionTarget == 0U)
                        {
                            this.Character.MP -= num1;
                            this.Character.SP -= num2;
                        }
                        else
                        {
                            this.Character.MP -= (uint)(ushort)(num1 * 2U);
                            this.Character.SP -= (uint)(ushort)(num2 * 2U);
                        }
                        this.SendActorHPMPSP((SagaDB.Actor.Actor)this.Character);
                    }
                    Singleton<SkillHandler>.Instance.SkillCast((SagaDB.Actor.Actor)this.Character, actor, skill);
                }
                else
                {
                    skill.result = (short)-11;
                    this.Character.e.OnActorSkillUse((SagaDB.Actor.Actor)this.Character, (MapEventArgs)skill);
                }
            }
            else
            {
                skill.argType = SkillArg.ArgType.Active;
                if (skill.useMPSP)
                {
                    this.Character.MP -= (uint)skill.skill.MP;
                    this.Character.SP -= (uint)skill.skill.SP;
                    this.SendActorHPMPSP((SagaDB.Actor.Actor)this.Character);
                }
                Singleton<SkillHandler>.Instance.SkillCast((SagaDB.Actor.Actor)this.Character, (SagaDB.Actor.Actor)this.Character, skill);
            }
            if (this.Character.Pet != null && this.Character.Pet.Ride)
                Singleton<SkillHandler>.Instance.ProcessPetGrowth((SagaDB.Actor.Actor)this.Character.Pet, PetGrowthReason.UseSkill);
            this.skillDelay = !this.Character.Status.delayCancelList.ContainsKey((ushort)skill.skill.ID) ? DateTime.Now + new TimeSpan(0, 0, 0, 0, skill.skill.Delay) : DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)((double)skill.skill.Delay * (1.0 - (double)this.Character.Status.delayCancelList[(ushort)skill.skill.ID] / 100.0)));
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SKILL, (MapEventArgs)skill, (SagaDB.Actor.Actor)this.Character, true);
            if (skill.skill.Effect != 0U && skill.showEffect)
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
                {
                    actorID = skill.dActor,
                    effectID = skill.skill.Effect,
                    x = skill.x,
                    y = skill.y
                }, (SagaDB.Actor.Actor)this.Character, true);
            if (this.Character.Tasks.ContainsKey("AutoCast"))
                this.Character.Tasks["AutoCast"].Activate();
            else if (skill.autoCast.Count != 0)
            {
                this.Character.Buff.CannotMove = true;
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
                AutoCast autoCast = new AutoCast((SagaDB.Actor.Actor)this.Character, skill);
                this.Character.Tasks.Add("AutoCast", (MultiRunTask)autoCast);
                autoCast.Activate();
            }
        }

        /// <summary>
        /// The SendChangeStatus.
        /// </summary>
        public void SendChangeStatus()
        {
            if (this.Character.Tasks.ContainsKey("Regeneration"))
            {
                this.Character.Tasks["Regeneration"].Deactivate();
                this.Character.Tasks.Remove("Regeneration");
            }
            if (this.Character.Motion != MotionType.NONE && this.Character.Motion != MotionType.DEAD)
            {
                this.Character.Motion = MotionType.NONE;
                this.Character.MotionLoop = false;
            }
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHANGE_STATUS, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The SendRevive.
        /// </summary>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public void SendRevive(byte level)
        {
            this.Character.Buff.Dead = false;
            this.Character.Buff.紫になる = false;
            this.Character.Motion = MotionType.STAND;
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
            float num = 0.0f;
            switch (level)
            {
                case 1:
                    num = 0.1f;
                    break;
                case 2:
                    num = 0.2f;
                    break;
                case 3:
                    num = 0.45f;
                    break;
                case 4:
                    num = 0.5f;
                    break;
                case 5:
                    num = 0.75f;
                    break;
            }
            this.Character.HP = (uint)((double)this.Character.MaxHP * (double)num);
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SKILL, (MapEventArgs)new SkillArg()
            {
                sActor = this.Character.ActorID,
                dActor = 0U,
                skill = Singleton<SkillFactory>.Instance.GetSkill(10002U, level),
                x = (byte)0,
                y = (byte)0,
                hp = new List<int>(),
                sp = new List<int>(),
                mp = new List<int>(),
                flag = {
          AttackFlag.HP_HEAL
        },
                affectedActors = {
          (SagaDB.Actor.Actor) this.Character
        },
                argType = SkillArg.ArgType.Active
            }, (SagaDB.Actor.Actor)this.Character, true);
            if (!this.Character.Tasks.ContainsKey("AutoSave"))
            {
                AutoSave autoSave = new AutoSave(this.Character);
                this.Character.Tasks.Add("AutoSave", (MultiRunTask)autoSave);
                autoSave.Activate();
            }
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
            this.SendPlayerInfo();
        }

        /// <summary>
        /// The SendSkillList.
        /// </summary>
        public void SendSkillList()
        {
            SSMG_SKILL_LIST ssmgSkillList = new SSMG_SKILL_LIST();
            List<SagaDB.Skill.Skill> list = new List<SagaDB.Skill.Skill>();
            bool ifDominion = this.map.Info.Flag.Test(MapFlags.Dominion);
            Dictionary<uint, byte> dictionary1;
            Dictionary<uint, byte> dictionary2;
            Dictionary<uint, byte> dictionary3;
            if (ifDominion)
            {
                dictionary1 = new Dictionary<uint, byte>();
                dictionary2 = new Dictionary<uint, byte>();
                dictionary3 = new Dictionary<uint, byte>();
            }
            else
            {
                dictionary1 = Singleton<SkillFactory>.Instance.SkillList(this.Character.JobBasic);
                dictionary2 = Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2X);
                dictionary3 = Singleton<SkillFactory>.Instance.SkillList(this.Character.Job2T);
            }
            foreach (uint id in dictionary1.Keys.Where<uint>((Func<uint, bool>)(c => !this.Character.Skills.ContainsKey(c))))
            {
                SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(id, (byte)0);
                list.Add(skill);
            }
            foreach (SagaDB.Skill.Skill skill in this.Character.Skills.Values)
                list.Add(skill);
            ssmgSkillList.Skills(list, (byte)0, this.Character.JobBasic, ifDominion, this.Character);
            this.netIO.SendPacket((Packet)ssmgSkillList);
            if (this.Character.Job == this.Character.Job2X)
            {
                list.Clear();
                foreach (uint id in dictionary2.Keys.Where<uint>((Func<uint, bool>)(c => !this.Character.Skills2.ContainsKey(c))))
                {
                    SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(id, (byte)0);
                    list.Add(skill);
                }
                foreach (SagaDB.Skill.Skill skill in this.Character.Skills2.Values)
                    list.Add(skill);
                ssmgSkillList.Skills(list, (byte)1, this.Character.Job2X, ifDominion, this.Character);
                this.netIO.SendPacket((Packet)ssmgSkillList);
            }
            else
            {
                list.Clear();
                foreach (uint id in dictionary3.Keys.Where<uint>((Func<uint, bool>)(c => !this.Character.Skills2.ContainsKey(c))))
                {
                    SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(id, (byte)0);
                    list.Add(skill);
                }
                foreach (SagaDB.Skill.Skill skill in this.Character.Skills2.Values)
                    list.Add(skill);
                ssmgSkillList.Skills(list, (byte)2, this.Character.Job2T, ifDominion, this.Character);
                this.netIO.SendPacket((Packet)ssmgSkillList);
            }
            if (this.map.Info.Flag.Test(MapFlags.Dominion))
            {
                if (this.Character.DominionReserveSkill)
                    this.netIO.SendPacket((Packet)new SSMG_SKILL_RESERVE_LIST()
                    {
                        Skills = this.Character.SkillsReserve.Values.ToList<SagaDB.Skill.Skill>()
                    });
                else
                    this.netIO.SendPacket((Packet)new SSMG_SKILL_RESERVE_LIST()
                    {
                        Skills = new List<SagaDB.Skill.Skill>()
                    });
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_SKILL_RESERVE_LIST()
                {
                    Skills = this.Character.SkillsReserve.Values.ToList<SagaDB.Skill.Skill>()
                });
            if (this.Character.JobJoint != PC_JOB.NONE)
            {
                list.Clear();
                foreach (KeyValuePair<uint, byte> keyValuePair in Singleton<SkillFactory>.Instance.SkillList(this.Character.JobJoint).Where<KeyValuePair<uint, byte>>((Func<KeyValuePair<uint, byte>, bool>)(c => (int)c.Value <= (int)this.Character.JointJobLevel)))
                {
                    SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(keyValuePair.Key, (byte)1);
                    list.Add(skill);
                }
                this.netIO.SendPacket((Packet)new SSMG_SKILL_JOINT_LIST()
                {
                    Skills = list
                });
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_SKILL_JOINT_LIST()
                {
                    Skills = new List<SagaDB.Skill.Skill>()
                });
        }

        /// <summary>
        /// The SendSkillDummy.
        /// </summary>
        /// <param name="skillid">The skillid<see cref="uint"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public void SendSkillDummy(uint skillid, byte level)
        {
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SKILL, (MapEventArgs)new SkillArg()
            {
                sActor = this.Character.ActorID,
                dActor = 0U,
                skill = Singleton<SkillFactory>.Instance.GetSkill(skillid, level),
                x = (byte)0,
                y = (byte)0,
                hp = new List<int>(),
                sp = new List<int>(),
                mp = new List<int>(),
                flag = {
          AttackFlag.NONE
        },
                argType = SkillArg.ArgType.Active
            }, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The ResetSkill.
        /// </summary>
        /// <param name="job">1为1转，2为2转.</param>
        public void ResetSkill(byte job)
        {
            int num = 0;
            List<uint> uintList = new List<uint>();
            switch (job)
            {
                case 1:
                    foreach (SagaDB.Skill.Skill skill in this.Character.Skills.Values)
                    {
                        if (Singleton<SkillFactory>.Instance.SkillList(this.Character.JobBasic).ContainsKey(skill.ID))
                        {
                            num += (int)skill.Level + 2;
                            uintList.Add(skill.ID);
                        }
                    }
                    this.Character.SkillPoint += (ushort)num;
                    using (List<uint>.Enumerator enumerator = uintList.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                            this.Character.Skills.Remove(enumerator.Current);
                        break;
                    }
                case 2:
                    foreach (SagaDB.Skill.Skill skill in this.Character.Skills2.Values)
                    {
                        if (Singleton<SkillFactory>.Instance.SkillList(this.Character.Job).ContainsKey(skill.ID))
                        {
                            num += (int)skill.Level + 2;
                            uintList.Add(skill.ID);
                        }
                    }
                    foreach (uint key in uintList)
                        this.Character.Skills2.Remove(key);
                    if (this.Character.Job == this.Character.Job2X)
                        this.Character.SkillPoint2X += (ushort)num;
                    if (this.Character.Job == this.Character.Job2T)
                    {
                        this.Character.SkillPoint2T += (ushort)num;
                        break;
                    }
                    break;
            }
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
        }

        /// <summary>
        /// The MarionetteActivate.
        /// </summary>
        /// <param name="marionetteID">The marionetteID<see cref="uint"/>.</param>
        public void MarionetteActivate(uint marionetteID)
        {
            this.MarionetteActivate(marionetteID, true, true);
        }

        /// <summary>
        /// The MarionetteActivate.
        /// </summary>
        /// <param name="marionetteID">The marionetteID<see cref="uint"/>.</param>
        /// <param name="delay">The delay<see cref="bool"/>.</param>
        /// <param name="duration">The duration<see cref="bool"/>.</param>
        public void MarionetteActivate(uint marionetteID, bool delay, bool duration)
        {
            SagaDB.Marionette.Marionette marionette1 = Singleton<MarionetteFactory>.Instance[marionetteID];
            if (marionette1 == null)
                return;
            SagaMap.Tasks.PC.Marionette marionette2 = new SagaMap.Tasks.PC.Marionette(this, marionette1.Duration);
            if (this.Character.Tasks.ContainsKey("Marionette") && duration)
            {
                this.MarionetteDeactivate();
                this.Character.Tasks["Marionette"].Deactivate();
                this.Character.Tasks.Remove("Marionette");
            }
            if (!duration && this.Character.Marionette != null)
            {
                foreach (uint skill in this.Character.Marionette.skills)
                {
                    if (Singleton<SkillFactory>.Instance.GetSkill(skill, (byte)1) != null && this.Character.Skills.ContainsKey(skill))
                        this.Character.Skills.Remove(skill);
                }
                Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
            }
            if (!this.Character.Tasks.ContainsKey("Marionette"))
            {
                this.Character.Tasks.Add("Marionette", (MultiRunTask)marionette2);
                marionette2.Activate();
            }
            if (delay)
                this.Character.NextMarionetteTime = this.Character.Status.Additions.ContainsKey("MarioTimeUp") ? DateTime.Now + new TimeSpan(0, 0, (int)((double)marionette1.Delay * 0.600000023841858)) : DateTime.Now + new TimeSpan(0, 0, marionette1.Delay);
            this.Character.Marionette = marionette1;
            this.SendCharInfoUpdate();
            foreach (uint skill1 in marionette1.skills)
            {
                SagaDB.Skill.Skill skill2 = Singleton<SkillFactory>.Instance.GetSkill(skill1, (byte)1);
                if (skill2 != null && !this.Character.Skills.ContainsKey(skill1))
                {
                    skill2.NoSave = true;
                    this.Character.Skills.Add(skill1, skill2);
                    if (!skill2.BaseData.active)
                        Singleton<SkillHandler>.Instance.SkillCast((SagaDB.Actor.Actor)this.Character, (SagaDB.Actor.Actor)this.Character, new SkillArg()
                        {
                            skill = skill2
                        });
                }
            }
            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
            this.SendPlayerInfo();
        }

        /// <summary>
        /// The MarionetteDeactivate.
        /// </summary>
        public void MarionetteDeactivate()
        {
            this.MarionetteDeactivate(false);
        }

        /// <summary>
        /// The MarionetteDeactivate.
        /// </summary>
        /// <param name="disconnecting">The disconnecting<see cref="bool"/>.</param>
        public void MarionetteDeactivate(bool disconnecting)
        {
            if (this.Character.Marionette == null)
                return;
            SagaDB.Marionette.Marionette marionette = this.Character.Marionette;
            this.Character.Marionette = (SagaDB.Marionette.Marionette)null;
            if (!disconnecting)
                this.SendCharInfoUpdate();
            foreach (uint skill in marionette.skills)
            {
                if (Singleton<SkillFactory>.Instance.GetSkill(skill, (byte)1) != null && this.Character.Skills.ContainsKey(skill))
                    this.Character.Skills.Remove(skill);
            }
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
            if (disconnecting)
                return;
            this.SendPlayerInfo();
            this.SendMotion(MotionType.JOY, (byte)0);
        }

        /// <summary>
        /// The OnFGardenFurnitureUse.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FGARDEN_FURNITURE_USE"/>.</param>
        public void OnFGardenFurnitureUse(CSMG_FGARDEN_FURNITURE_USE p)
        {
            SagaDB.Actor.Actor actor = Singleton<MapManager>.Instance.GetMap(this.Character.MapID).GetActor(p.ActorID);
            if (actor == null || actor.type != ActorType.FURNITURE)
                return;
            SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(((ActorFurniture)actor).ItemID);
            if (obj.BaseData.eventID == 0U)
                return;
            this.EventActivate(obj.BaseData.eventID);
        }

        /// <summary>
        /// The OnFGardenFurnitureReconfig.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FGARDEN_FURNITURE_RECONFIG"/>.</param>
        public void OnFGardenFurnitureReconfig(CSMG_FGARDEN_FURNITURE_RECONFIG p)
        {
            if (this.Character.FGarden == null)
                return;
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(this.Character.MapID);
            SagaDB.Actor.Actor actor = map.GetActor(p.ActorID);
            if (actor == null || actor.type != ActorType.FURNITURE)
                return;
            if ((int)this.Character.MapID != (int)this.Character.FGarden.MapID && (int)this.Character.MapID != (int)this.Character.FGarden.RoomMapID)
                this.netIO.SendPacket((Packet)new SSMG_FG_FURNITURE_RECONFIG()
                {
                    ActorID = actor.ActorID,
                    X = actor.X,
                    Y = actor.Y,
                    Z = ((ActorFurniture)actor).Z,
                    Dir = actor.Dir
                });
            else
                map.MoveActor(SagaMap.Map.MOVE_TYPE.START, actor, new short[3]
                {
          p.X,
          p.Y,
          p.Z
                }, p.Dir, (ushort)200);
        }

        /// <summary>
        /// The OnFGardenFurnitureRemove.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FGARDEN_FURNITURE_REMOVE"/>.</param>
        public void OnFGardenFurnitureRemove(CSMG_FGARDEN_FURNITURE_REMOVE p)
        {
            if (this.Character.FGarden == null || (int)this.Character.MapID != (int)this.Character.FGarden.MapID && (int)this.Character.MapID != (int)this.Character.FGarden.RoomMapID)
                return;
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(this.Character.MapID);
            SagaDB.Actor.Actor actor = map.GetActor(p.ActorID);
            if (actor == null || actor.type != ActorType.FURNITURE)
                return;
            ActorFurniture actorFurniture = (ActorFurniture)actor;
            map.DeleteActor(actor);
            SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(actorFurniture.ItemID);
            obj.PictID = actorFurniture.PictID;
            if ((int)this.Character.MapID == (int)this.Character.FGarden.MapID)
                this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Remove(actorFurniture);
            else
                this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Remove(actorFurniture);
            this.AddItem(obj, false);
            this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.FG_FUTNITURE_REMOVE, (object)actorFurniture.Name, (object)(this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Count + this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Count), (object)Singleton<Configuration>.Instance.MaxFurnitureCount));
        }

        /// <summary>
        /// The OnFGardenFurnitureSetup.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FGARDEN_FURNITURE_SETUP"/>.</param>
        public void OnFGardenFurnitureSetup(CSMG_FGARDEN_FURNITURE_SETUP p)
        {
            if (this.Character.FGarden == null || (int)this.Character.MapID != (int)this.Character.FGarden.MapID && (int)this.Character.MapID != (int)this.Character.FGarden.RoomMapID)
                return;
            if ((long)(this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Count + this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Count) < (long)Singleton<Configuration>.Instance.MaxFurnitureCount)
            {
                SagaDB.Item.Item obj = this.Character.Inventory.GetItem(p.InventorySlot);
                ActorFurniture actorFurniture = new ActorFurniture();
                this.DeleteItem(p.InventorySlot, (ushort)1, false);
                actorFurniture.MapID = this.Character.MapID;
                actorFurniture.ItemID = obj.ItemID;
                SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(actorFurniture.MapID);
                actorFurniture.X = p.X;
                actorFurniture.Y = p.Y;
                actorFurniture.Z = p.Z;
                actorFurniture.Dir = p.Dir;
                actorFurniture.Name = obj.BaseData.name;
                actorFurniture.PictID = obj.PictID;
                actorFurniture.e = (ActorEventHandler)new NullEventHandler();
                map.RegisterActor((SagaDB.Actor.Actor)actorFurniture);
                actorFurniture.invisble = false;
                map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorFurniture);
                if ((int)this.Character.MapID == (int)this.Character.FGarden.MapID)
                    this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Add(actorFurniture);
                else
                    this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Add(actorFurniture);
                this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.FG_FUTNITURE_SETUP, (object)actorFurniture.Name, (object)(this.Character.FGarden.Furnitures[FurniturePlace.GARDEN].Count + this.Character.FGarden.Furnitures[FurniturePlace.ROOM].Count), (object)Singleton<Configuration>.Instance.MaxFurnitureCount));
            }
            else
                this.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.FG_FUTNITURE_MAX);
        }

        /// <summary>
        /// The OnFGardenEquipt.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FGARDEN_EQUIPT"/>.</param>
        public void OnFGardenEquipt(CSMG_FGARDEN_EQUIPT p)
        {
            if (this.Character.FGarden == null || (int)this.Character.MapID != (int)this.Character.FGarden.MapID && (int)this.Character.MapID != (int)this.Character.FGarden.RoomMapID)
                return;
            if (p.InventorySlot != uint.MaxValue)
            {
                SagaDB.Item.Item obj = this.Character.Inventory.GetItem(p.InventorySlot);
                if (obj == null)
                    return;
                if (this.Character.FGarden.FGardenEquipments[p.Place] != 0U)
                {
                    this.AddItem(Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(this.Character.FGarden.FGardenEquipments[p.Place], true), false);
                    this.netIO.SendPacket((Packet)new SSMG_FG_EQUIPT()
                    {
                        ItemID = 0U,
                        Place = p.Place
                    });
                }
                if (p.Place == FGardenSlot.GARDEN_MODELHOUSE && this.Character.FGarden.FGardenEquipments[FGardenSlot.GARDEN_MODELHOUSE] == 0U)
                    this.netIO.SendPacket((Packet)new SSMG_NPC_SET_EVENT_AREA()
                    {
                        EventID = 10000315U,
                        StartX = 6U,
                        StartY = 7U,
                        EndX = 6U,
                        EndY = 7U
                    });
                this.Character.FGarden.FGardenEquipments[p.Place] = obj.ItemID;
                this.netIO.SendPacket((Packet)new SSMG_FG_EQUIPT()
                {
                    ItemID = obj.ItemID,
                    Place = p.Place
                });
                this.DeleteItem(p.InventorySlot, (ushort)1, false);
            }
            else
            {
                uint fgardenEquipment = this.Character.FGarden.FGardenEquipments[p.Place];
                if (fgardenEquipment != 0U)
                    this.AddItem(Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(fgardenEquipment, true), false);
                this.Character.FGarden.FGardenEquipments[p.Place] = 0U;
                this.netIO.SendPacket((Packet)new SSMG_FG_EQUIPT()
                {
                    ItemID = 0U,
                    Place = p.Place
                });
                if (p.Place == FGardenSlot.GARDEN_MODELHOUSE)
                    this.netIO.SendPacket((Packet)new SSMG_NPC_CANCEL_EVENT_AREA()
                    {
                        StartX = 6U,
                        StartY = 7U,
                        EndX = 6U,
                        EndY = 7U
                    });
            }
        }

        /// <summary>
        /// The OnChat.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAT_PUBLIC"/>.</param>
        public void OnChat(CSMG_CHAT_PUBLIC p)
        {
            if (Singleton<AtCommand>.Instance.ProcessCommand(this, p.Content))
                return;
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAT, (MapEventArgs)new ChatArg()
            {
                content = p.Content
            }, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The OnChatParty.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAT_PARTY"/>.</param>
        public void OnChatParty(CSMG_CHAT_PARTY p)
        {
            if (this.Character == null)
                return;
            Singleton<PartyManager>.Instance.PartyChat(this.Character.Party, this.Character, p.Content);
        }

        /// <summary>
        /// The OnMotion.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAT_MOTION"/>.</param>
        public void OnMotion(CSMG_CHAT_MOTION p)
        {
            ChatArg chatArg = new ChatArg();
            chatArg.motion = p.Motion;
            chatArg.loop = p.Loop;
            this.Character.Motion = chatArg.motion;
            this.Character.MotionLoop = chatArg.loop == (byte)1;
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.MOTION, (MapEventArgs)chatArg, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The OnEmotion.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAT_EMOTION"/>.</param>
        public void OnEmotion(CSMG_CHAT_EMOTION p)
        {
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.EMOTION, (MapEventArgs)new ChatArg()
            {
                emotion = p.Emotion
            }, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The OnSit.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAT_SIT"/>.</param>
        public void OnSit(CSMG_CHAT_SIT p)
        {
            ChatArg chatArg = new ChatArg();
            if (this.Character.Motion != MotionType.SIT)
            {
                chatArg.motion = MotionType.SIT;
                chatArg.loop = (byte)1;
                this.Character.Motion = MotionType.SIT;
                this.Character.MotionLoop = true;
                if (!this.Character.Tasks.ContainsKey("Regeneration"))
                {
                    Regeneration regeneration = new Regeneration(this);
                    this.Character.Tasks.Add("Regeneration", (MultiRunTask)regeneration);
                    regeneration.Activate();
                }
            }
            else
            {
                if (this.Character.Tasks.ContainsKey("Regeneration"))
                {
                    this.Character.Tasks["Regeneration"].Deactivate();
                    this.Character.Tasks.Remove("Regeneration");
                }
                chatArg.motion = MotionType.STAND;
                chatArg.loop = (byte)0;
                this.Character.Motion = MotionType.NONE;
                this.Character.MotionLoop = false;
            }
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.MOTION, (MapEventArgs)chatArg, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The OnSign.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAT_SIGN"/>.</param>
        public void OnSign(CSMG_CHAT_SIGN p)
        {
            this.Character.Sign = p.Content;
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SIGN_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The SendMotion.
        /// </summary>
        /// <param name="motion">The motion<see cref="MotionType"/>.</param>
        /// <param name="loop">The loop<see cref="byte"/>.</param>
        public void SendMotion(MotionType motion, byte loop)
        {
            ChatArg chatArg = new ChatArg();
            chatArg.motion = motion;
            chatArg.loop = loop;
            this.Character.Motion = chatArg.motion;
            this.Character.MotionLoop = chatArg.loop == (byte)1;
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.MOTION, (MapEventArgs)chatArg, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The SendSystemMessage.
        /// </summary>
        /// <param name="content">The content<see cref="string"/>.</param>
        public void SendSystemMessage(string content)
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_CHAT_PUBLIC()
            {
                ActorID = uint.MaxValue,
                Message = content
            });
        }

        /// <summary>
        /// The SendSystemMessage.
        /// </summary>
        /// <param name="message">The message<see cref="SSMG_SYSTEM_MESSAGE.Messages"/>.</param>
        public void SendSystemMessage(SSMG_SYSTEM_MESSAGE.Messages message)
        {
            this.netIO.SendPacket((Packet)new SSMG_SYSTEM_MESSAGE()
            {
                Message = message
            });
        }

        /// <summary>
        /// The SendChatParty.
        /// </summary>
        /// <param name="sender">The sender<see cref="string"/>.</param>
        /// <param name="content">The content<see cref="string"/>.</param>
        public void SendChatParty(string sender, string content)
        {
            this.netIO.SendPacket((Packet)new SSMG_CHAT_PARTY()
            {
                Sender = sender,
                Content = content
            });
        }

        /// <summary>
        /// The OnTradeRequest.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_TRADE_REQUEST"/>.</param>
        public void OnTradeRequest(CSMG_TRADE_REQUEST p)
        {
            SagaDB.Actor.Actor actor = this.Map.GetActor(p.ActorID);
            if (actor == null || actor.type != ActorType.PC)
                return;
            ActorPC pc = (ActorPC)actor;
            MapClient mapClient = MapClient.FromActorPC(pc);
            SSMG_TRADE_REQUEST_RESULT tradeRequestResult = new SSMG_TRADE_REQUEST_RESULT();
            if (mapClient.trading)
                tradeRequestResult.Result = -3;
            else if (this.trading)
            {
                tradeRequestResult.Result = -1;
            }
            else
            {
                this.tradingTarget = pc;
                mapClient.SendTradeRequest(this.Character);
            }
            this.netIO.SendPacket((Packet)tradeRequestResult);
        }

        /// <summary>
        /// The OnTradeRequestAnswer.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_TRADE_REQUEST_ANSWER"/>.</param>
        public void OnTradeRequestAnswer(CSMG_TRADE_REQUEST_ANSWER p)
        {
            if (this.tradingTarget == null || (int)this.tradingTarget.MapID != (int)this.Character.MapID)
                return;
            MapClient mapClient = MapClient.FromActorPC(this.tradingTarget);
            if (p.Answer == (byte)1)
            {
                this.trading = true;
                mapClient.trading = true;
                this.confirmed = false;
                this.performed = false;
                mapClient.confirmed = false;
                mapClient.performed = false;
                this.SendTradeStart();
                this.SendTradeStatus(true, false);
                mapClient.SendTradeStart();
                mapClient.SendTradeStatus(true, false);
            }
            else
            {
                this.tradingTarget = (ActorPC)null;
                mapClient.tradingTarget = (ActorPC)null;
                mapClient.netIO.SendPacket((Packet)new SSMG_TRADE_REQUEST_RESULT()
                {
                    Result = -6
                });
            }
        }

        /// <summary>
        /// The OnTradeItemNPC.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_TRADE_ITEM"/>.</param>
        private void OnTradeItemNPC(CSMG_TRADE_ITEM p)
        {
            if (this.tradeItems != null && this.tradeItems.Count != 0)
            {
                this.confirmed = false;
                this.performed = false;
                this.SendTradeStatus(true, false);
            }
            this.tradeItems = p.InventoryID;
            this.tradeCounts = p.Count;
            this.tradingGold = p.Gold;
        }

        /// <summary>
        /// The OnTradeItem.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_TRADE_ITEM"/>.</param>
        public void OnTradeItem(CSMG_TRADE_ITEM p)
        {
            if (this.npcTrade)
            {
                this.OnTradeItemNPC(p);
            }
            else
            {
                if (this.tradingTarget == null)
                    return;
                MapClient mapClient = MapClient.FromActorPC(this.tradingTarget);
                if (this.tradeItems != null && this.tradeItems.Count != 0)
                {
                    this.confirmed = false;
                    mapClient.confirmed = false;
                    this.performed = false;
                    mapClient.performed = false;
                    this.SendTradeStatus(true, false);
                    mapClient.SendTradeStatus(true, false);
                }
                this.tradeItems = p.InventoryID;
                this.tradeCounts = p.Count;
                this.tradingGold = p.Gold;
                SSMG_TRADE_ITEM_HEAD ssmgTradeItemHead = new SSMG_TRADE_ITEM_HEAD();
                mapClient.netIO.SendPacket((Packet)ssmgTradeItemHead);
                for (int index = 0; index < this.tradeItems.Count; ++index)
                {
                    SSMG_TRADE_ITEM_INFO ssmgTradeItemInfo = new SSMG_TRADE_ITEM_INFO();
                    SagaDB.Item.Item obj = this.Character.Inventory.GetItem(this.tradeItems[index]).Clone();
                    if (obj.BaseData.noTrade || obj.BaseData.itemType == ItemType.DEMIC_CHIP)
                    {
                        this.tradeItems[index] = 0U;
                        this.tradeCounts[index] = (ushort)0;
                    }
                    else if (obj.PossessionOwner != null && (int)obj.PossessionOwner.CharID != (int)this.chara.CharID)
                    {
                        this.tradeItems[index] = 0U;
                        this.tradeCounts[index] = (ushort)0;
                    }
                    else
                    {
                        if ((int)obj.Stack < (int)this.tradeCounts[index])
                            this.tradeCounts[index] = obj.Stack;
                        obj.Stack = this.tradeCounts[index];
                        ssmgTradeItemInfo.Item = obj;
                        ssmgTradeItemInfo.InventorySlot = this.tradeItems[index];
                        ssmgTradeItemInfo.Container = ContainerType.BODY;
                        mapClient.netIO.SendPacket((Packet)ssmgTradeItemInfo);
                    }
                }
                mapClient.netIO.SendPacket((Packet)new SSMG_TRADE_GOLD()
                {
                    Gold = this.tradingGold
                });
                SSMG_TRADE_ITEM_FOOT ssmgTradeItemFoot = new SSMG_TRADE_ITEM_FOOT();
                mapClient.netIO.SendPacket((Packet)ssmgTradeItemFoot);
            }
        }

        /// <summary>
        /// The OnTradeConfirm.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_TRADE_CONFIRM"/>.</param>
        public void OnTradeConfirm(CSMG_TRADE_CONFIRM p)
        {
            if (this.npcTrade)
            {
                switch (p.State)
                {
                    case 0:
                        this.confirmed = false;
                        break;
                    case 1:
                        this.confirmed = true;
                        break;
                }
                if (this.confirmed)
                    this.SendTradeStatus(false, true);
            }
            if (this.tradingTarget == null)
                return;
            switch (p.State)
            {
                case 0:
                    this.confirmed = false;
                    break;
                case 1:
                    this.confirmed = true;
                    break;
            }
            if (!this.confirmed || !MapClient.FromActorPC(this.tradingTarget).confirmed)
                return;
            this.SendTradeStatus(false, true);
            MapClient.FromActorPC(this.tradingTarget).SendTradeStatus(false, true);
        }

        /// <summary>
        /// The OnTradePerform.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_TRADE_PERFORM"/>.</param>
        public void OnTradePerform(CSMG_TRADE_PERFORM p)
        {
            if (this.npcTrade)
            {
                this.PerformTradeNPC();
            }
            else
            {
                if (this.tradingTarget == null)
                    return;
                switch (p.State)
                {
                    case 0:
                        this.performed = false;
                        break;
                    case 1:
                        this.performed = true;
                        break;
                }
                MapClient mapClient = MapClient.FromActorPC(this.tradingTarget);
                if (!this.performed || !mapClient.performed)
                    return;
                if ((long)this.Character.Gold >= (long)this.tradingGold && (long)mapClient.Character.Gold >= (long)mapClient.tradingGold && (long)this.Character.Gold + (long)mapClient.tradingGold < 100000000L && (long)mapClient.Character.Gold + (long)this.tradingGold < 100000000L)
                {
                    this.SendTradeEnd(2);
                    this.PerformTrade();
                    mapClient.SendTradeEnd(2);
                    mapClient.PerformTrade();
                }
                this.SendTradeEnd(1);
                mapClient.SendTradeEnd(1);
            }
        }

        /// <summary>
        /// The OnTradeCancel.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_TRADE_CANCEL"/>.</param>
        public void OnTradeCancel(CSMG_TRADE_CANCEL p)
        {
            if (this.npcTrade)
            {
                this.npcTradeItem = new List<SagaDB.Item.Item>();
                this.npcTrade = false;
                this.SendTradeEnd(3);
            }
            else
            {
                if (this.tradingTarget == null)
                    return;
                MapClient.FromActorPC(this.tradingTarget).SendTradeEnd(3);
                this.SendTradeEnd(3);
            }
        }

        /// <summary>
        /// The PerformTradeNPC.
        /// </summary>
        private void PerformTradeNPC()
        {
            this.npcTradeItem = new List<SagaDB.Item.Item>();
            this.SendTradeEnd(2);
            if (this.tradeItems != null)
            {
                for (int index = 0; index < this.tradeItems.Count; ++index)
                {
                    SagaDB.Item.Item obj = this.Character.Inventory.GetItem(this.tradeItems[index]).Clone();
                    obj.Stack = this.tradeCounts[index];
                    Logger.LogItemLost(Logger.EventType.ItemNPCLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("NPCTrade Count:{0}", (object)obj.Stack), false);
                    this.DeleteItem(this.tradeItems[index], this.tradeCounts[index], true);
                    this.npcTradeItem.Add(obj);
                }
            }
            this.Character.Gold -= (int)this.tradingGold;
            this.SendGoldUpdate();
            this.performed = true;
            this.npcTrade = false;
            this.SendTradeEnd(1);
        }

        /// <summary>
        /// The PerformTrade.
        /// </summary>
        public void PerformTrade()
        {
            if (this.tradingTarget == null)
                return;
            MapClient mapClient = MapClient.FromActorPC(this.tradingTarget);
            if (this.tradeItems != null)
            {
                for (int index = 0; index < this.tradeItems.Count; ++index)
                {
                    if (this.tradeItems[index] != 0U)
                    {
                        SagaDB.Item.Item obj = this.Character.Inventory.GetItem(this.tradeItems[index]).Clone();
                        obj.Stack = this.tradeCounts[index];
                        Logger.LogItemLost(Logger.EventType.ItemTradeLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("Trade Count:{0} To:{1}({2})", (object)this.tradeCounts[index], (object)mapClient.Character.Name, (object)mapClient.Character.CharID), false);
                        this.DeleteItem(this.tradeItems[index], this.tradeCounts[index], true);
                        Logger.LogItemGet(Logger.EventType.ItemTradeGet, mapClient.Character.Name + "(" + (object)mapClient.Character.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("Trade Count:{0} From:{1}({2})", (object)obj.Stack, (object)this.Character.Name, (object)this.Character.CharID), false);
                        mapClient.AddItem(obj, true);
                    }
                }
            }
            this.Character.Gold -= (int)this.tradingGold;
            this.SendGoldUpdate();
            mapClient.Character.Gold += (int)this.tradingGold;
            mapClient.SendGoldUpdate();
        }

        /// <summary>
        /// The SendTradeEnd.
        /// </summary>
        /// <param name="type">1，清空变量，2，发送结束封包，3，两个都执行.</param>
        public void SendTradeEnd(int type)
        {
            if (type == 1 || type == 3)
            {
                this.tradeCounts = (List<ushort>)null;
                this.tradeItems = (List<uint>)null;
                this.trading = false;
                this.tradingGold = 0U;
                this.tradingTarget = (ActorPC)null;
                this.confirmed = false;
                this.performed = false;
            }
            if (type != 2 && type != 3)
                return;
            this.netIO.SendPacket((Packet)new SSMG_TRADE_END());
        }

        /// <summary>
        /// The SendTradeStatus.
        /// </summary>
        /// <param name="canConfirm">The canConfirm<see cref="bool"/>.</param>
        /// <param name="canPerform">The canPerform<see cref="bool"/>.</param>
        public void SendTradeStatus(bool canConfirm, bool canPerform)
        {
            this.netIO.SendPacket((Packet)new SSMG_TRADE_STATUS()
            {
                Confirm = canConfirm,
                Perform = canPerform
            });
        }

        /// <summary>
        /// The SendTradeStart.
        /// </summary>
        public void SendTradeStart()
        {
            if (this.tradingTarget == null)
                return;
            SSMG_TRADE_START ssmgTradeStart = new SSMG_TRADE_START();
            ssmgTradeStart.SetPara(this.tradingTarget.Name, 0);
            this.netIO.SendPacket((Packet)ssmgTradeStart);
        }

        /// <summary>
        /// The SendTradeStartNPC.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public void SendTradeStartNPC(string name)
        {
            if (!this.npcTrade)
                return;
            SSMG_TRADE_START ssmgTradeStart = new SSMG_TRADE_START();
            ssmgTradeStart.SetPara(name, 1);
            this.netIO.SendPacket((Packet)ssmgTradeStart);
            this.SendTradeStatus(true, false);
        }

        /// <summary>
        /// The SendTradeRequest.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendTradeRequest(ActorPC pc)
        {
            this.netIO.SendPacket((Packet)new SSMG_TRADE_REQUEST()
            {
                Name = pc.Name
            });
            this.tradingTarget = pc;
        }

        /// <summary>
        /// The OnRingEmblemUpload.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_RING_EMBLEM_UPLOAD"/>.</param>
        public void OnRingEmblemUpload(CSMG_RING_EMBLEM_UPLOAD p)
        {
            SSMG_RING_EMBLEM_UPLOAD_RESULT emblemUploadResult = new SSMG_RING_EMBLEM_UPLOAD_RESULT();
            if (this.Character.Ring == null)
                return;
            if (this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(RingRight.RingMaster) || this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(RingRight.Ring2ndMaster))
            {
                if (p.Data[0] == (byte)137)
                {
                    if ((long)this.Character.Ring.Fame >= (long)Singleton<Configuration>.Instance.RingFameNeededForEmblem)
                    {
                        emblemUploadResult.Result = SSMG_RING_EMBLEM_UPLOAD_RESULT.Results.OK;
                        MapServer.charDB.RingEmblemUpdate(this.Character.Ring, p.Data);
                    }
                    else
                        emblemUploadResult.Result = SSMG_RING_EMBLEM_UPLOAD_RESULT.Results.FAME_NOT_ENOUGH;
                }
                else
                    emblemUploadResult.Result = SSMG_RING_EMBLEM_UPLOAD_RESULT.Results.WRONG_FORMAT;
            }
            this.netIO.SendPacket((Packet)emblemUploadResult);
        }

        /// <summary>
        /// The OnChatRing.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAT_RING"/>.</param>
        public void OnChatRing(CSMG_CHAT_RING p)
        {
            if (this.Character.Ring == null)
                return;
            Singleton<RingManager>.Instance.RingChat(this.Character.Ring, this.Character, p.Content);
        }

        /// <summary>
        /// The OnRingRightSet.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_RING_RIGHT_SET"/>.</param>
        public void OnRingRightSet(CSMG_RING_RIGHT_SET p)
        {
            if (this.Character.Ring == null || !this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(RingRight.RingMaster) && !this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(RingRight.Ring2ndMaster))
                return;
            Singleton<RingManager>.Instance.SetMemberRight(this.Character.Ring, p.CharID, p.Right);
        }

        /// <summary>
        /// The OnRingKick.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_RING_KICK"/>.</param>
        public void OnRingKick(CSMG_RING_KICK p)
        {
            if (this.Character.Ring == null || !this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(RingRight.KickRight))
                return;
            Singleton<RingManager>.Instance.DeleteMember(this.Character.Ring, this.Character.Ring.GetMember(p.CharID), SSMG_RING_QUIT.Reasons.KICK);
        }

        /// <summary>
        /// The OnRingQuit.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_RING_QUIT"/>.</param>
        public void OnRingQuit(CSMG_RING_QUIT p)
        {
            SSMG_RING_QUIT_RESULT ssmgRingQuitResult = new SSMG_RING_QUIT_RESULT();
            if (this.Character.Ring == null)
                ssmgRingQuitResult.Result = -1;
            else if (this.Character != this.Character.Ring.Leader)
                Singleton<RingManager>.Instance.DeleteMember(this.Character.Ring, this.Character, SSMG_RING_QUIT.Reasons.LEAVE);
            else
                Singleton<RingManager>.Instance.RingDismiss(this.Character.Ring);
            this.netIO.SendPacket((Packet)ssmgRingQuitResult);
        }

        /// <summary>
        /// The OnRingInviteAnswer.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_RING_INVITE_ANSWER"/>.</param>
        /// <param name="accepted">The accepted<see cref="bool"/>.</param>
        public void OnRingInviteAnswer(CSMG_RING_INVITE_ANSWER p, bool accepted)
        {
            if (accepted)
            {
                SSMG_RING_INVITE_ANSWER_RESULT inviteAnswerResult = new SSMG_RING_INVITE_ANSWER_RESULT();
                if (this.ringPartner != null)
                {
                    if (this.Character.Ring == null)
                    {
                        if (this.ringPartner.Ring.MemberCount < this.ringPartner.Ring.MaxMemberCount)
                        {
                            inviteAnswerResult.Resault = SSMG_RING_INVITE_ANSWER_RESULT.Resaults.OK;
                            Singleton<RingManager>.Instance.AddMember(this.ringPartner.Ring, this.Character);
                        }
                        else
                            inviteAnswerResult.Resault = SSMG_RING_INVITE_ANSWER_RESULT.Resaults.MEMBER_EXCEED;
                    }
                    else
                        inviteAnswerResult.Resault = SSMG_RING_INVITE_ANSWER_RESULT.Resaults.ALREADY_IN_RING;
                }
                else
                    inviteAnswerResult.Resault = SSMG_RING_INVITE_ANSWER_RESULT.Resaults.CANNOT_FIND_TARGET;
                this.netIO.SendPacket((Packet)inviteAnswerResult);
            }
            this.ringPartner = (ActorPC)null;
        }

        /// <summary>
        /// The OnRingInvite.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_RING_INVITE"/>.</param>
        public void OnRingInvite(CSMG_RING_INVITE p)
        {
            MapClient client = MapClientManager.Instance.FindClient(p.CharID);
            SSMG_RING_INVITE_RESULT ringInviteResult = new SSMG_RING_INVITE_RESULT();
            if (client != null)
            {
                if (this.Character.Ring != null)
                {
                    if (client.Character.Ring == null)
                    {
                        if (this.Character.Ring.Rights[this.Character.Ring.IndexOf(this.Character)].Test(RingRight.AddRight))
                        {
                            ringInviteResult.Resault = SSMG_RING_INVITE_RESULT.Resaults.OK;
                            client.ringPartner = this.Character;
                            client.netIO.SendPacket((Packet)new SSMG_RING_INVITE()
                            {
                                CharID = this.Character.CharID,
                                CharName = this.Character.Name,
                                RingName = this.Character.Ring.Name
                            });
                        }
                        else
                            ringInviteResult.Resault = SSMG_RING_INVITE_RESULT.Resaults.NO_RIGHT;
                    }
                    else
                        ringInviteResult.Resault = SSMG_RING_INVITE_RESULT.Resaults.TARGET_ALREADY_IN_RING;
                }
                else
                    ringInviteResult.Resault = SSMG_RING_INVITE_RESULT.Resaults.NO_RING;
            }
            else
                ringInviteResult.Resault = SSMG_RING_INVITE_RESULT.Resaults.CANNOT_FIND_TARGET;
            this.netIO.SendPacket((Packet)ringInviteResult);
        }

        /// <summary>
        /// The SendRingMember.
        /// </summary>
        private void SendRingMember()
        {
            if (this.Character.Ring == null)
                return;
            foreach (ActorPC pc in this.Character.Ring.Members.Values)
            {
                SSMG_RING_MEMBER_INFO ssmgRingMemberInfo = new SSMG_RING_MEMBER_INFO();
                ssmgRingMemberInfo.Member(pc, this.Character.Ring);
                this.netIO.SendPacket((Packet)ssmgRingMemberInfo);
                this.SendRingMemberInfo(pc);
            }
        }

        /// <summary>
        /// The SendRingInfo.
        /// </summary>
        /// <param name="reason">The reason<see cref="SSMG_RING_INFO.Reason"/>.</param>
        public void SendRingInfo(SSMG_RING_INFO.Reason reason)
        {
            if (this.Character.Ring == null)
                return;
            if (reason != SSMG_RING_INFO.Reason.UPDATED)
            {
                SSMG_RING_INFO ssmgRingInfo = new SSMG_RING_INFO();
                SSMG_RING_NAME ssmgRingName = new SSMG_RING_NAME();
                ssmgRingInfo.Ring(this.Character.Ring, reason);
                ssmgRingName.Player = this.Character;
                this.netIO.SendPacket((Packet)ssmgRingInfo);
                this.netIO.SendPacket((Packet)ssmgRingName);
                this.SendRingMember();
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_RING_INFO_UPDATE()
                {
                    RingID = this.Character.Ring.ID,
                    Fame = this.Character.Ring.Fame,
                    CurrentMember = (byte)this.Character.Ring.MemberCount,
                    MaxMember = (byte)this.Character.Ring.MaxMemberCount
                });
        }

        /// <summary>
        /// The SendRingMemberInfo.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendRingMemberInfo(ActorPC pc)
        {
            if (this.Character.Ring == null || !this.Character.Ring.IsMember(pc) || !pc.Online)
                return;
            uint num = (uint)this.Character.Ring.IndexOf(pc);
            this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_STATE()
            {
                PartyIndex = num,
                CharID = pc.CharID,
                Online = pc.Online
            });
            this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_DETAIL()
            {
                PartyIndex = num,
                CharID = pc.CharID,
                Job = pc.Job,
                Level = pc.Level,
                JobLevel = pc.CurrentJobLevel
            });
        }

        /// <summary>
        /// The SendRingMemberState.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendRingMemberState(ActorPC pc)
        {
            if (this.Character.Ring == null || !this.Character.Ring.IsMember(pc))
                return;
            int num = this.Character.Ring.IndexOf(pc);
            this.netIO.SendPacket((Packet)new SSMG_PARTY_MEMBER_STATE()
            {
                PartyIndex = (uint)num,
                CharID = pc.CharID,
                Online = pc.Online
            });
        }

        /// <summary>
        /// The SendChatRing.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="content">The content<see cref="string"/>.</param>
        public void SendChatRing(string name, string content)
        {
            this.netIO.SendPacket((Packet)new SSMG_CHAT_RING()
            {
                Sender = name,
                Content = content
            });
        }

        /// <summary>
        /// The SendRingMeDelete.
        /// </summary>
        /// <param name="reason">The reason<see cref="SSMG_RING_QUIT.Reasons"/>.</param>
        public void SendRingMeDelete(SSMG_RING_QUIT.Reasons reason)
        {
            this.netIO.SendPacket((Packet)new SSMG_RING_QUIT()
            {
                RingID = this.Character.Ring.ID,
                Reason = reason
            });
        }

        /// <summary>
        /// The SendRingMemberDelete.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendRingMemberDelete(ActorPC pc)
        {
            SSMG_RING_MEMBER_INFO ssmgRingMemberInfo = new SSMG_RING_MEMBER_INFO();
            ssmgRingMemberInfo.Member(pc, (SagaDB.Ring.Ring)null);
            this.netIO.SendPacket((Packet)ssmgRingMemberInfo);
        }

        /// <summary>
        /// The OnGolemWarehouse.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_WAREHOUSE"/>.</param>
        public void OnGolemWarehouse(CSMG_GOLEM_WAREHOUSE p)
        {
            this.netIO.SendPacket((Packet)new SSMG_GOLEM_WAREHOUSE()
            {
                ActorID = this.Character.ActorID,
                Title = this.Character.Golem.Title
            });
            foreach (SagaDB.Item.Item obj in this.Character.Inventory.GetContainer(ContainerType.GOLEMWAREHOUSE))
                this.netIO.SendPacket((Packet)new SSMG_GOLEM_WAREHOUSE_ITEM()
                {
                    InventorySlot = obj.Slot,
                    Container = ContainerType.GOLEMWAREHOUSE,
                    Item = obj
                });
            this.netIO.SendPacket((Packet)new SSMG_GOLEM_WAREHOUSE_ITEM_FOOTER());
        }

        /// <summary>
        /// The OnGolemWarehouseSet.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_WAREHOUSE_SET"/>.</param>
        public void OnGolemWarehouseSet(CSMG_GOLEM_WAREHOUSE_SET p)
        {
            if (this.Character.Golem == null)
                return;
            this.Character.Golem.Title = p.Title;
        }

        /// <summary>
        /// The OnGolemWarehouseGet.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_WAREHOUSE_GET"/>.</param>
        public void OnGolemWarehouseGet(CSMG_GOLEM_WAREHOUSE_GET p)
        {
            SagaDB.Item.Item obj1 = this.Character.Inventory.GetItem(p.InventoryID);
            if (obj1 == null)
                return;
            ushort count = p.Count;
            if ((int)obj1.Stack >= (int)count)
            {
                SagaDB.Item.Item obj2 = obj1.Clone();
                obj2.Stack = count;
                if (obj2.Stack > (ushort)0)
                    Logger.LogItemLost(Logger.EventType.ItemGolemLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj2.BaseData.name + "(" + (object)obj2.ItemID + ")", string.Format("GolemWarehouseGet Count:{0}", (object)count), false);
                int num = (int)this.Character.Inventory.DeleteItem(p.InventoryID, (int)count);
                Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj1.BaseData.name + "(" + (object)obj1.ItemID + ")", string.Format("GolemWarehouse Count:{0}", (object)obj1.Stack), false);
                this.AddItem(obj2, false);
                this.netIO.SendPacket((Packet)new SSMG_GOLEM_WAREHOUSE_GET());
            }
        }

        /// <summary>
        /// The OnGolemShopBuySell.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_SHOP_BUY_SELL"/>.</param>
        public void OnGolemShopBuySell(CSMG_GOLEM_SHOP_BUY_SELL p)
        {
            SagaDB.Actor.Actor actor = this.map.GetActor(p.ActorID);
            Dictionary<uint, ushort> items = p.Items;
            if (actor.type != ActorType.GOLEM)
                return;
            ActorGolem actorGolem = (ActorGolem)actor;
            uint num1 = 0;
            foreach (uint key1 in items.Keys)
            {
                SagaDB.Item.Item obj1 = this.Character.Inventory.GetItem(key1);
                if (obj1 != null && items[key1] != (ushort)0)
                {
                    SagaDB.Item.Item obj2 = obj1.Clone();
                    if ((int)obj1.Stack >= (int)items[key1])
                    {
                        uint index = 0;
                        foreach (uint key2 in actorGolem.BuyShop.Keys)
                        {
                            if ((int)actorGolem.BuyShop[key2].ItemID == (int)obj2.ItemID)
                            {
                                index = key2;
                                break;
                            }
                        }
                        num1 += actorGolem.BuyShop[index].Price * (uint)items[key1];
                        if (actorGolem.BuyLimit >= num1)
                        {
                            obj2.Stack = items[key1];
                            Logger.LogItemLost(Logger.EventType.ItemGolemLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj1.BaseData.name + "(" + (object)obj1.ItemID + ")", string.Format("GolemSell Count:{0}", (object)items[key1]), false);
                            this.DeleteItem(key1, items[key1], true);
                            actorGolem.BuyShop[index].Count -= items[key1];
                            if (actorGolem.BoughtItem.ContainsKey(obj1.ItemID))
                            {
                                actorGolem.BoughtItem[obj1.ItemID].Count += items[key1];
                            }
                            else
                            {
                                actorGolem.BoughtItem.Add(obj1.ItemID, new GolemShopItem());
                                actorGolem.BoughtItem[obj1.ItemID].Price = actorGolem.BuyShop[index].Price;
                                actorGolem.BoughtItem[obj1.ItemID].Count += items[key1];
                            }
                            if (obj2.Stack > (ushort)0)
                                Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj2.BaseData.name + "(" + (object)obj2.ItemID + ")", string.Format("GolemBuy Count:{0}", (object)obj2.Stack), false);
                            int num2 = (int)actorGolem.Owner.Inventory.AddItem(ContainerType.BODY, obj2);
                        }
                        else
                            break;
                    }
                }
            }
            actorGolem.Owner.Gold -= (int)num1;
            actorGolem.BuyLimit -= num1;
            this.Character.Gold += (int)num1;
        }

        /// <summary>
        /// The OnGolemShopSellBuy.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_SHOP_SELL_BUY"/>.</param>
        public void OnGolemShopSellBuy(CSMG_GOLEM_SHOP_SELL_BUY p)
        {
            SagaDB.Actor.Actor actor = this.map.GetActor(p.ActorID);
            Dictionary<uint, ushort> items = p.Items;
            SSMG_GOLEM_SHOP_SELL_ANSWER golemShopSellAnswer = new SSMG_GOLEM_SHOP_SELL_ANSWER();
            if (actor.type != ActorType.GOLEM)
                return;
            ActorGolem actorGolem = (ActorGolem)actor;
            uint num1 = 0;
            foreach (uint key in items.Keys)
            {
                SagaDB.Item.Item obj1 = actorGolem.Owner.Inventory.GetItem(key);
                if (obj1 == null)
                {
                    golemShopSellAnswer.Result = -4;
                    this.netIO.SendPacket((Packet)golemShopSellAnswer);
                    return;
                }
                if (items[key] == (ushort)0)
                {
                    golemShopSellAnswer.Result = -2;
                    this.netIO.SendPacket((Packet)golemShopSellAnswer);
                    return;
                }
                if ((int)obj1.Stack >= (int)items[key])
                {
                    num1 += actorGolem.SellShop[key].Price * (uint)items[key];
                    if ((long)this.Character.Gold < (long)num1)
                    {
                        golemShopSellAnswer.Result = -7;
                        this.netIO.SendPacket((Packet)golemShopSellAnswer);
                        return;
                    }
                    if ((long)num1 + (long)actorGolem.Owner.Gold >= 99999999L)
                    {
                        golemShopSellAnswer.Result = -9;
                        this.netIO.SendPacket((Packet)golemShopSellAnswer);
                        return;
                    }
                    SagaDB.Item.Item obj2 = obj1.Clone();
                    obj2.Stack = items[key];
                    if (obj2.Stack > (ushort)0)
                        Logger.LogItemLost(Logger.EventType.ItemGolemLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj2.BaseData.name + "(" + (object)obj2.ItemID + ")", string.Format("GolemSell Count:{0}", (object)items[key]), false);
                    int num2 = (int)actorGolem.Owner.Inventory.DeleteItem(key, (int)items[key]);
                    actorGolem.SellShop[key].Count -= items[key];
                    if (actorGolem.SoldItem.ContainsKey(obj1.ItemID))
                    {
                        actorGolem.SoldItem[obj1.ItemID].Count += items[key];
                    }
                    else
                    {
                        actorGolem.SoldItem.Add(obj1.ItemID, new GolemShopItem());
                        actorGolem.SoldItem[obj1.ItemID].Price = actorGolem.SellShop[key].Price;
                        actorGolem.SoldItem[obj1.ItemID].Count += items[key];
                    }
                    if (actorGolem.SellShop[key].Count == (ushort)0)
                        actorGolem.SellShop.Remove(key);
                    if (actorGolem.SellShop.Count == 0)
                    {
                        actorGolem.invisble = true;
                        this.map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorGolem);
                    }
                    Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj1.BaseData.name + "(" + (object)obj1.ItemID + ")", string.Format("GolemBuy Count:{0}", (object)obj1.Stack), false);
                    this.AddItem(obj2, true);
                }
                else
                {
                    golemShopSellAnswer.Result = -5;
                    this.netIO.SendPacket((Packet)golemShopSellAnswer);
                    return;
                }
            }
            actorGolem.Owner.Gold += (int)num1;
            this.Character.Gold -= (int)num1;
        }

        /// <summary>
        /// The OnGolemShopOpen.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_SHOP_OPEN"/>.</param>
        public void OnGolemShopOpen(CSMG_GOLEM_SHOP_OPEN p)
        {
            SagaDB.Actor.Actor actor = this.map.GetActor(p.ActorID);
            if (actor.type != ActorType.GOLEM)
                return;
            ActorGolem actorGolem = (ActorGolem)actor;
            if (actorGolem.GolemType == GolemType.Sell)
            {
                this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_OPEN_OK()
                {
                    ActorID = p.ActorID
                });
                this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_HEADER()
                {
                    ActorID = p.ActorID
                });
                foreach (uint key in actorGolem.SellShop.Keys)
                {
                    SagaDB.Item.Item obj = actorGolem.Owner.Inventory.GetItem(key);
                    if (obj != null)
                    {
                        SSMG_GOLEM_SHOP_ITEM ssmgGolemShopItem = new SSMG_GOLEM_SHOP_ITEM();
                        ssmgGolemShopItem.InventorySlot = key;
                        ssmgGolemShopItem.Container = ContainerType.BODY;
                        ssmgGolemShopItem.Price = actorGolem.SellShop[key].Price;
                        ssmgGolemShopItem.ShopCount = actorGolem.SellShop[key].Count;
                        ssmgGolemShopItem.Item = obj;
                        this.netIO.SendPacket((Packet)ssmgGolemShopItem);
                    }
                }
                this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_FOOTER());
            }
            if (actorGolem.GolemType == GolemType.Buy)
            {
                this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_BUY_HEADER()
                {
                    ActorID = p.ActorID
                });
                this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_BUY_ITEM()
                {
                    Items = actorGolem.BuyShop,
                    BuyLimit = actorGolem.BuyLimit
                });
            }
        }

        /// <summary>
        /// The OnGolemShopSellClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_SHOP_SELL_CLOSE"/>.</param>
        public void OnGolemShopSellClose(CSMG_GOLEM_SHOP_SELL_CLOSE p)
        {
            this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_SELL_SET());
        }

        /// <summary>
        /// The OnGolemShopSellSetup.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_SHOP_SELL_SETUP"/>.</param>
        public void OnGolemShopSellSetup(CSMG_GOLEM_SHOP_SELL_SETUP p)
        {
            uint[] inventoryIds = p.InventoryIDs;
            ushort[] counts = p.Counts;
            uint[] prices = p.Prices;
            if (inventoryIds.Length != 0)
            {
                for (int index = 0; index < inventoryIds.Length; ++index)
                {
                    if (!this.Character.Golem.SellShop.ContainsKey(inventoryIds[index]))
                        this.Character.Golem.SellShop.Add(inventoryIds[index], new GolemShopItem()
                        {
                            InventoryID = inventoryIds[index],
                            ItemID = this.Character.Inventory.GetItem(inventoryIds[index]).ItemID
                        });
                    if (counts[index] == (ushort)0)
                    {
                        this.Character.Golem.SellShop.Remove(inventoryIds[index]);
                    }
                    else
                    {
                        this.Character.Golem.SellShop[inventoryIds[index]].Count = counts[index];
                        this.Character.Golem.SellShop[inventoryIds[index]].Price = prices[index];
                    }
                }
            }
            this.Character.Golem.Title = p.Comment;
        }

        /// <summary>
        /// The OnGolemShopSell.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_SHOP_SELL"/>.</param>
        public void OnGolemShopSell(CSMG_GOLEM_SHOP_SELL p)
        {
            this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_SELL_SETUP()
            {
                Comment = this.Character.Golem.Title
            });
        }

        /// <summary>
        /// The OnGolemShopBuyClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_SHOP_BUY_CLOSE"/>.</param>
        public void OnGolemShopBuyClose(CSMG_GOLEM_SHOP_BUY_CLOSE p)
        {
            this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_BUY_SET());
        }

        /// <summary>
        /// The OnGolemShopBuySetup.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_SHOP_BUY_SETUP"/>.</param>
        public void OnGolemShopBuySetup(CSMG_GOLEM_SHOP_BUY_SETUP p)
        {
            uint[] inventoryIds = p.InventoryIDs;
            ushort[] counts = p.Counts;
            uint[] prices = p.Prices;
            if (inventoryIds.Length != 0)
            {
                for (int index = 0; index < inventoryIds.Length; ++index)
                {
                    if (!this.Character.Golem.BuyShop.ContainsKey(inventoryIds[index]))
                        this.Character.Golem.BuyShop.Add(inventoryIds[index], new GolemShopItem()
                        {
                            InventoryID = inventoryIds[index],
                            ItemID = this.Character.Inventory.GetItem(inventoryIds[index]).ItemID
                        });
                    if (counts[index] == (ushort)0)
                    {
                        this.Character.Golem.BuyShop.Remove(inventoryIds[index]);
                    }
                    else
                    {
                        this.Character.Golem.BuyShop[inventoryIds[index]].Count = counts[index];
                        this.Character.Golem.BuyShop[inventoryIds[index]].Price = prices[index];
                    }
                }
            }
            this.Character.Golem.BuyLimit = p.BuyLimit;
            this.Character.Golem.Title = p.Comment;
        }

        /// <summary>
        /// The OnGolemShopBuy.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_GOLEM_SHOP_BUY"/>.</param>
        public void OnGolemShopBuy(CSMG_GOLEM_SHOP_BUY p)
        {
            SSMG_GOLEM_SHOP_BUY_SETUP golemShopBuySetup = new SSMG_GOLEM_SHOP_BUY_SETUP();
            golemShopBuySetup.BuyLimit = this.Character.Golem.BuyLimit;
            golemShopBuySetup.Comment = this.Character.Golem.Title;
            this.Character.Golem.BuyShop.Clear();
            this.netIO.SendPacket((Packet)golemShopBuySetup);
        }

        /// <summary>
        /// The SendCL.
        /// </summary>
        public void SendCL()
        {
            if (this.chara.Race != PC_RACE.DEM || this.state == MapClient.SESSION_STATE.AUTHENTIFICATED)
                return;
            SSMG_DEM_COST_LIMIT_UPDATE demCostLimitUpdate = new SSMG_DEM_COST_LIMIT_UPDATE();
            demCostLimitUpdate.Result = SSMG_DEM_COST_LIMIT_UPDATE.Results.OK;
            if (this.map.Info.Flag.Test(MapFlags.Dominion))
            {
                demCostLimitUpdate.CurrentEP = this.Character.DominionEPUsed;
                demCostLimitUpdate.EPRequired = (short)((int)Singleton<ExperienceManager>.Instance.GetEPRequired(this.chara) - (int)this.chara.DominionEPUsed);
                demCostLimitUpdate.CL = this.chara.DominionCL;
            }
            else
            {
                demCostLimitUpdate.CurrentEP = this.Character.EPUsed;
                demCostLimitUpdate.EPRequired = (short)((int)Singleton<ExperienceManager>.Instance.GetEPRequired(this.chara) - (int)this.chara.EPUsed);
                demCostLimitUpdate.CL = this.chara.CL;
            }
            this.netIO.SendPacket((Packet)demCostLimitUpdate);
        }

        /// <summary>
        /// The OnDEMCostLimitBuy.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_COST_LIMIT_BUY"/>.</param>
        public void OnDEMCostLimitBuy(CSMG_DEM_COST_LIMIT_BUY p)
        {
            if (!this.demCLBuy)
                return;
            short ep = p.EP;
            SSMG_DEM_COST_LIMIT_UPDATE demCostLimitUpdate = new SSMG_DEM_COST_LIMIT_UPDATE();
            if ((long)this.Character.EP >= (long)ep)
            {
                this.Character.EP = (uint)((ulong)this.Character.EP - (ulong)ep);
                Singleton<ExperienceManager>.Instance.ApplyEP(this.Character, ep);
                Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                this.SendPlayerInfo();
                demCostLimitUpdate.Result = SSMG_DEM_COST_LIMIT_UPDATE.Results.OK;
            }
            else
                demCostLimitUpdate.Result = SSMG_DEM_COST_LIMIT_UPDATE.Results.NOT_ENOUGH_EP;
            if (this.map.Info.Flag.Test(MapFlags.Dominion))
            {
                demCostLimitUpdate.CurrentEP = this.Character.DominionEPUsed;
                demCostLimitUpdate.EPRequired = (short)((int)Singleton<ExperienceManager>.Instance.GetEPRequired(this.chara) - (int)this.chara.DominionEPUsed);
                demCostLimitUpdate.CL = this.chara.DominionCL;
            }
            else
            {
                demCostLimitUpdate.CurrentEP = this.Character.EPUsed;
                demCostLimitUpdate.EPRequired = (short)((int)Singleton<ExperienceManager>.Instance.GetEPRequired(this.chara) - (int)this.chara.EPUsed);
                demCostLimitUpdate.CL = this.chara.CL;
            }
            this.netIO.SendPacket((Packet)demCostLimitUpdate);
        }

        /// <summary>
        /// The OnDEMCostLimitClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_COST_LIMIT_CLOSE"/>.</param>
        public void OnDEMCostLimitClose(CSMG_DEM_COST_LIMIT_CLOSE p)
        {
            this.demCLBuy = false;
        }

        /// <summary>
        /// The OnDEMFormChange.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_FORM_CHANGE"/>.</param>
        public void OnDEMFormChange(CSMG_DEM_FORM_CHANGE p)
        {
            if (this.Character.Form == p.Form)
                return;
            this.Character.Form = p.Form;
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.chara);
            Singleton<StatusFactory>.Instance.CalcStatus(this.chara);
            this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.chara, true);
            this.SendPlayerInfo();
            this.SendAttackType();
            this.netIO.SendPacket((Packet)new SSMG_DEM_FORM_CHANGE()
            {
                Form = this.chara.Form
            });
        }

        /// <summary>
        /// The OnDEMPartsUnequip.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_PARTS_UNEQUIP"/>.</param>
        public void OnDEMPartsUnequip(CSMG_DEM_PARTS_UNEQUIP p)
        {
            if (this.chara.Race != PC_RACE.DEM || !this.demParts)
                return;
            SagaDB.Item.Item obj1 = this.Character.Inventory.GetItem(p.InventoryID);
            if (obj1 == null || !this.Character.Inventory.IsContainerParts(this.Character.Inventory.GetContainerType(obj1.Slot)))
                return;
            List<EnumEquipSlot> equipSlot = obj1.EquipSlot;
            if (equipSlot.Count > 1)
            {
                for (int index = 1; index < equipSlot.Count; ++index)
                    this.Character.Inventory.Parts.Remove(equipSlot[index]);
            }
            uint slot = obj1.Slot;
            if (this.Character.Inventory.MoveItem(this.Character.Inventory.GetContainerType(obj1.Slot), (int)obj1.Slot, ContainerType.BODY, 1))
            {
                if (obj1.Stack == (ushort)0)
                {
                    if ((int)slot == (int)this.Character.Inventory.LastItem.Slot)
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_CONTAINER_CHANGE()
                        {
                            InventorySlot = obj1.Slot,
                            Target = ContainerType.BODY
                        });
                        SSMG_ITEM_EQUIP ssmgItemEquip = new SSMG_ITEM_EQUIP();
                        ssmgItemEquip.InventorySlot = uint.MaxValue;
                        ssmgItemEquip.Target = ContainerType.NONE;
                        ssmgItemEquip.Result = 3;
                        Singleton<StatusFactory>.Instance.CalcRange(this.Character);
                        if (obj1.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                        {
                            this.SendAttackType();
                            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
                        }
                        ssmgItemEquip.Range = this.Character.Range;
                        this.netIO.SendPacket((Packet)ssmgItemEquip);
                        this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHANGE_EQUIP, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
                        Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                        this.SendPlayerInfo();
                    }
                    else
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                        {
                            InventorySlot = slot
                        });
                        if ((int)slot != (int)obj1.Slot)
                        {
                            SagaDB.Item.Item obj2 = this.Character.Inventory.GetItem(obj1.Slot);
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                            {
                                InventorySlot = obj2.Slot,
                                Stack = obj2.Stack
                            });
                            SagaDB.Item.Item lastItem = this.Character.Inventory.LastItem;
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_ADD()
                            {
                                Container = ContainerType.BODY,
                                InventorySlot = lastItem.Slot,
                                Item = lastItem
                            });
                        }
                        else
                        {
                            SagaDB.Item.Item lastItem = this.Character.Inventory.LastItem;
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                            {
                                InventorySlot = lastItem.Slot,
                                Stack = lastItem.Stack
                            });
                        }
                    }
                }
                else
                {
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                    {
                        InventorySlot = obj1.Slot,
                        Stack = obj1.Stack
                    });
                    if (this.Character.Inventory.LastItem.Stack == (ushort)1)
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_ADD()
                        {
                            Container = ContainerType.BODY,
                            InventorySlot = this.Character.Inventory.LastItem.Slot,
                            Item = this.Character.Inventory.LastItem
                        });
                    }
                    else
                    {
                        SagaDB.Item.Item lastItem = this.Character.Inventory.LastItem;
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                        {
                            InventorySlot = lastItem.Slot,
                            Stack = lastItem.Stack
                        });
                    }
                }
            }
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
        }

        /// <summary>
        /// The OnDEMPartsEquip.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_PARTS_EQUIP"/>.</param>
        public void OnDEMPartsEquip(CSMG_DEM_PARTS_EQUIP p)
        {
            if (this.chara.Race != PC_RACE.DEM || !this.demParts)
                return;
            SagaDB.Item.Item obj1 = this.Character.Inventory.GetItem(p.InventoryID);
            if (obj1 == null)
                return;
            int num1 = this.CheckEquipRequirement(obj1);
            if (num1 < 0)
            {
                this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                {
                    InventorySlot = uint.MaxValue,
                    Target = ContainerType.NONE,
                    Result = num1,
                    Range = this.Character.Range
                });
            }
            else
            {
                foreach (EnumEquipSlot key1 in obj1.EquipSlot)
                {
                    if (this.Character.Inventory.Parts.ContainsKey(key1))
                    {
                        foreach (EnumEquipSlot key2 in this.Character.Inventory.Parts[key1].EquipSlot)
                        {
                            if (this.Character.Inventory.Parts.ContainsKey(key2))
                            {
                                SagaDB.Item.Item part = this.Character.Inventory.Parts[key2];
                                if (part.Stack == (ushort)0)
                                    this.Character.Inventory.Parts.Remove(key2);
                                else if (this.Character.Inventory.MoveItem((ContainerType)Enum.Parse(typeof(ContainerType), key2.ToString()) + 200, (int)part.Slot, ContainerType.BODY, (int)part.Stack))
                                {
                                    this.netIO.SendPacket((Packet)new SSMG_ITEM_CONTAINER_CHANGE()
                                    {
                                        InventorySlot = part.Slot,
                                        Target = ContainerType.BODY
                                    });
                                    this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                                    {
                                        InventorySlot = uint.MaxValue,
                                        Target = ContainerType.NONE,
                                        Result = 1,
                                        Range = this.Character.Range
                                    });
                                    Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                                    this.SendPlayerInfo();
                                }
                            }
                        }
                    }
                }
                ushort stack = obj1.Stack;
                if (stack == (ushort)0)
                    return;
                ContainerType dst = (ContainerType)Enum.Parse(typeof(ContainerType), obj1.EquipSlot[0].ToString()) + 200;
                if (this.Character.Inventory.MoveItem(this.Character.Inventory.GetContainerType(obj1.Slot), (int)obj1.Slot, dst, (int)stack))
                {
                    if (obj1.Stack == (ushort)0)
                    {
                        SSMG_ITEM_EQUIP ssmgItemEquip = new SSMG_ITEM_EQUIP();
                        ssmgItemEquip.Target = (ContainerType)Enum.Parse(typeof(ContainerType), obj1.EquipSlot[0].ToString());
                        ssmgItemEquip.Result = 2;
                        ssmgItemEquip.InventorySlot = obj1.Slot;
                        Singleton<StatusFactory>.Instance.CalcRange(this.Character);
                        ssmgItemEquip.Range = this.Character.Range;
                        this.netIO.SendPacket((Packet)ssmgItemEquip);
                    }
                    else
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                        {
                            InventorySlot = obj1.Slot,
                            Stack = obj1.Stack
                        });
                }
                List<EnumEquipSlot> equipSlot = obj1.EquipSlot;
                if (equipSlot.Count > 1)
                {
                    for (int index = 1; index < equipSlot.Count; ++index)
                    {
                        SagaDB.Item.Item obj2 = obj1.Clone();
                        obj2.Stack = (ushort)0;
                        int num2 = (int)this.Character.Inventory.AddItem((ContainerType)Enum.Parse(typeof(ContainerType), equipSlot[index].ToString()) + 200, obj2);
                    }
                }
                if (obj1.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                {
                    this.SendAttackType();
                    Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
                }
                Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                this.SendPlayerInfo();
                this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHANGE_EQUIP, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
            }
        }

        /// <summary>
        /// The OnDEMPartsClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_PARTS_CLOSE"/>.</param>
        public void OnDEMPartsClose(CSMG_DEM_PARTS_CLOSE p)
        {
            this.demParts = false;
        }

        /// <summary>
        /// The OnDEMDemicInitialize.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_DEMIC_INITIALIZE"/>.</param>
        public void OnDEMDemicInitialize(CSMG_DEM_DEMIC_INITIALIZE p)
        {
            if (!this.demic)
                return;
            byte page = p.Page;
            DEMICPanel demicPanel = (DEMICPanel)null;
            bool[,] flagArray = (bool[,])null;
            if (this.map.Info.Flag.Test(MapFlags.Dominion))
            {
                if (this.chara.Inventory.DominionDemicChips.ContainsKey(page))
                {
                    demicPanel = this.chara.Inventory.DominionDemicChips[page];
                    flagArray = this.chara.Inventory.validTable(page, true);
                }
            }
            else if (this.chara.Inventory.DemicChips.ContainsKey(page))
            {
                demicPanel = this.chara.Inventory.DemicChips[page];
                flagArray = this.chara.Inventory.validTable(page, false);
            }
            SSMG_DEM_DEMIC_INITIALIZED demicInitialized = new SSMG_DEM_DEMIC_INITIALIZED();
            demicInitialized.Page = page;
            if (demicPanel != null)
            {
                if (this.chara.EP > 0U)
                {
                    --this.chara.EP;
                    foreach (Chip chip in demicPanel.Chips)
                    {
                        if (chip.Data.skill1 != 0U && this.chara.Skills.ContainsKey(chip.Data.skill1))
                        {
                            if (this.chara.Skills[chip.Data.skill1].Level > (byte)1)
                                --this.chara.Skills[chip.Data.skill1].Level;
                            else
                                this.chara.Skills.Remove(chip.Data.skill1);
                        }
                        if (chip.Data.skill2 != 0U && this.chara.Skills.ContainsKey(chip.Data.skill2))
                        {
                            if (this.chara.Skills[chip.Data.skill2].Level > (byte)1)
                                --this.chara.Skills[chip.Data.skill2].Level;
                            else
                                this.chara.Skills.Remove(chip.Data.skill2);
                        }
                        if (chip.Data.skill3 != 0U && this.chara.Skills.ContainsKey(chip.Data.skill3))
                        {
                            if (this.chara.Skills[chip.Data.skill3].Level > (byte)1)
                                --this.chara.Skills[chip.Data.skill3].Level;
                            else
                                this.chara.Skills.Remove(chip.Data.skill3);
                        }
                        this.AddItem(Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(chip.ItemID), true);
                    }
                    demicPanel.Chips.Clear();
                    int num1 = SagaLib.Global.Random.Next(0, 99);
                    int num2 = num1 > 10 ? (num1 > 40 ? 0 : 1) : 2;
                    demicPanel.EngageTask1 = byte.MaxValue;
                    demicPanel.EngageTask2 = byte.MaxValue;
                    for (int index1 = 0; index1 < num2; ++index1)
                    {
                        List<byte[]> numArrayList = new List<byte[]>();
                        for (int index2 = 0; index2 < 9; ++index2)
                        {
                            for (int index3 = 0; index3 < 9; ++index3)
                            {
                                if (flagArray[index3, index2])
                                    numArrayList.Add(new byte[2]
                                    {
                    (byte) index3,
                    (byte) index2
                                    });
                            }
                        }
                        byte[] numArray = numArrayList[SagaLib.Global.Random.Next(0, numArrayList.Count - 1)];
                        byte num3 = (byte)((uint)numArray[0] + (uint)numArray[1] * 9U);
                        if (index1 == 0)
                            demicPanel.EngageTask1 = num3;
                        else
                            demicPanel.EngageTask2 = num3;
                    }
                    this.SendActorHPMPSP((SagaDB.Actor.Actor)this.chara);
                    demicInitialized.Result = SSMG_DEM_DEMIC_INITIALIZED.Results.OK;
                    demicInitialized.EngageTask = demicPanel.EngageTask1;
                    demicInitialized.EngageTask2 = demicPanel.EngageTask2;
                    Singleton<StatusFactory>.Instance.CalcStatus(this.chara);
                    this.SendPlayerInfo();
                }
                else
                    demicInitialized.Result = SSMG_DEM_DEMIC_INITIALIZED.Results.NOT_ENOUGH_EP;
            }
            else
                demicInitialized.Result = SSMG_DEM_DEMIC_INITIALIZED.Results.FAILED;
            this.netIO.SendPacket((Packet)demicInitialized);
        }

        /// <summary>
        /// The OnDEMDemicConfirm.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_DEMIC_CONFIRM"/>.</param>
        public void OnDEMDemicConfirm(CSMG_DEM_DEMIC_CONFIRM p)
        {
            if (!this.demic)
                return;
            short[,] chips = p.Chips;
            byte page = p.Page;
            for (int index1 = 0; index1 < 9; ++index1)
            {
                for (int index2 = 0; index2 < 9; ++index2)
                {
                    short key = chips[index2, index1];
                    if (Factory<ChipFactory, Chip.BaseData>.Instance.ByChipID.ContainsKey(key))
                    {
                        Chip chip = new Chip(Factory<ChipFactory, Chip.BaseData>.Instance.ByChipID[key]);
                        if (this.CountItem(chip.ItemID) > 0)
                        {
                            chip.X = (byte)index2;
                            chip.Y = (byte)index1;
                            if (this.chara.Inventory.InsertChip(page, chip))
                                this.DeleteItemID(chip.ItemID, (ushort)1, true);
                        }
                    }
                }
            }
            this.netIO.SendPacket((Packet)new SSMG_DEM_DEMIC_CONFIRM_RESULT()
            {
                Page = page,
                Result = SSMG_DEM_DEMIC_CONFIRM_RESULT.Results.OK
            });
            Singleton<StatusFactory>.Instance.CalcStatus(this.chara);
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.chara);
            this.SendPlayerInfo();
        }

        /// <summary>
        /// The OnDEMDemicClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_DEMIC_CLOSE"/>.</param>
        public void OnDEMDemicClose(CSMG_DEM_DEMIC_CLOSE p)
        {
            this.demic = false;
        }

        /// <summary>
        /// The OnDEMStatsPreCalc.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_STATS_PRE_CALC"/>.</param>
        public void OnDEMStatsPreCalc(CSMG_DEM_STATS_PRE_CALC p)
        {
            ushort str = this.Character.Str;
            ushort dex = this.Character.Dex;
            ushort num1 = this.Character.Int;
            ushort agi = this.Character.Agi;
            ushort vit = this.Character.Vit;
            ushort mag = this.Character.Mag;
            this.Character.Str = p.Str;
            this.Character.Dex = p.Dex;
            this.Character.Int = p.Int;
            this.Character.Agi = p.Agi;
            this.Character.Vit = p.Vit;
            this.Character.Mag = p.Mag;
            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
            SSMG_PLAYER_STATS_PRE_CALC playerStatsPreCalc = new SSMG_PLAYER_STATS_PRE_CALC();
            playerStatsPreCalc.ASPD = this.Character.Status.aspd;
            playerStatsPreCalc.ATK1Max = this.Character.Status.max_atk1;
            playerStatsPreCalc.ATK1Min = this.Character.Status.min_atk1;
            playerStatsPreCalc.ATK2Max = this.Character.Status.max_atk2;
            playerStatsPreCalc.ATK2Min = this.Character.Status.min_atk2;
            playerStatsPreCalc.ATK3Max = this.Character.Status.max_atk3;
            playerStatsPreCalc.ATK3Min = this.Character.Status.min_atk3;
            playerStatsPreCalc.AvoidCritical = this.Character.Status.avoid_critical;
            playerStatsPreCalc.AvoidMagic = this.Character.Status.avoid_magic;
            playerStatsPreCalc.AvoidMelee = this.Character.Status.avoid_melee;
            playerStatsPreCalc.AvoidRanged = this.Character.Status.avoid_ranged;
            playerStatsPreCalc.CSPD = this.Character.Status.cspd;
            playerStatsPreCalc.DefAddition = (ushort)this.Character.Status.def_add;
            playerStatsPreCalc.DefBase = this.Character.Status.def;
            playerStatsPreCalc.HitCritical = this.Character.Status.hit_critical;
            playerStatsPreCalc.HitMagic = this.Character.Status.hit_magic;
            playerStatsPreCalc.HitMelee = this.Character.Status.hit_melee;
            playerStatsPreCalc.HitRanged = this.Character.Status.hit_ranged;
            playerStatsPreCalc.MATKMax = this.Character.Status.max_matk;
            playerStatsPreCalc.MATKMin = this.Character.Status.min_matk;
            playerStatsPreCalc.MDefAddition = (ushort)this.Character.Status.mdef_add;
            playerStatsPreCalc.MDefBase = this.Character.Status.mdef;
            playerStatsPreCalc.Speed = this.Character.Speed;
            playerStatsPreCalc.HP = (ushort)this.Character.MaxHP;
            playerStatsPreCalc.MP = (ushort)this.Character.MaxMP;
            playerStatsPreCalc.SP = (ushort)this.Character.MaxSP;
            uint num2 = 0;
            foreach (uint num3 in this.Character.Inventory.MaxVolume.Values)
                num2 += num3;
            playerStatsPreCalc.Capacity = (ushort)num2;
            uint num4 = 0;
            foreach (uint num3 in this.Character.Inventory.MaxPayload.Values)
                num4 += num3;
            playerStatsPreCalc.Payload = (ushort)num4;
            this.netIO.SendPacket((Packet)playerStatsPreCalc);
            SSMG_DEM_STATS_PRE_CALC ssmgDemStatsPreCalc = new SSMG_DEM_STATS_PRE_CALC();
            ssmgDemStatsPreCalc.ASPD = this.Character.Status.aspd;
            ssmgDemStatsPreCalc.ATK1Max = this.Character.Status.max_atk1;
            ssmgDemStatsPreCalc.ATK1Min = this.Character.Status.min_atk1;
            ssmgDemStatsPreCalc.ATK2Max = this.Character.Status.max_atk2;
            ssmgDemStatsPreCalc.ATK2Min = this.Character.Status.min_atk2;
            ssmgDemStatsPreCalc.ATK3Max = this.Character.Status.max_atk3;
            ssmgDemStatsPreCalc.ATK3Min = this.Character.Status.min_atk3;
            ssmgDemStatsPreCalc.AvoidCritical = this.Character.Status.avoid_critical;
            ssmgDemStatsPreCalc.AvoidMagic = this.Character.Status.avoid_magic;
            ssmgDemStatsPreCalc.AvoidMelee = this.Character.Status.avoid_melee;
            ssmgDemStatsPreCalc.AvoidRanged = this.Character.Status.avoid_ranged;
            ssmgDemStatsPreCalc.CSPD = this.Character.Status.cspd;
            ssmgDemStatsPreCalc.DefAddition = (ushort)this.Character.Status.def_add;
            ssmgDemStatsPreCalc.DefBase = this.Character.Status.def;
            ssmgDemStatsPreCalc.HitCritical = this.Character.Status.hit_critical;
            ssmgDemStatsPreCalc.HitMagic = this.Character.Status.hit_magic;
            ssmgDemStatsPreCalc.HitMelee = this.Character.Status.hit_melee;
            ssmgDemStatsPreCalc.HitRanged = this.Character.Status.hit_ranged;
            ssmgDemStatsPreCalc.MATKMax = this.Character.Status.max_matk;
            ssmgDemStatsPreCalc.MATKMin = this.Character.Status.min_matk;
            ssmgDemStatsPreCalc.MDefAddition = (ushort)this.Character.Status.mdef_add;
            ssmgDemStatsPreCalc.MDefBase = this.Character.Status.mdef;
            ssmgDemStatsPreCalc.Speed = this.Character.Speed;
            ssmgDemStatsPreCalc.HP = (ushort)this.Character.MaxHP;
            ssmgDemStatsPreCalc.MP = (ushort)this.Character.MaxMP;
            ssmgDemStatsPreCalc.SP = (ushort)this.Character.MaxSP;
            uint num5 = 0;
            foreach (uint num3 in this.Character.Inventory.MaxVolume.Values)
                num5 += num3;
            ssmgDemStatsPreCalc.Capacity = (ushort)num5;
            uint num6 = 0;
            foreach (uint num3 in this.Character.Inventory.MaxPayload.Values)
                num6 += num3;
            ssmgDemStatsPreCalc.Payload = (ushort)num6;
            this.netIO.SendPacket((Packet)ssmgDemStatsPreCalc);
            this.Character.Str = str;
            this.Character.Dex = dex;
            this.Character.Int = num1;
            this.Character.Agi = agi;
            this.Character.Vit = vit;
            this.Character.Mag = mag;
            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
        }

        /// <summary>
        /// The OnDEMChipCategory.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_CHIP_CATEGORY"/>.</param>
        public void OnDEMChipCategory(CSMG_DEM_CHIP_CATEGORY p)
        {
            if (!this.chipShop || !Factory<ChipShopFactory, ChipShopCategory>.Instance.Items.ContainsKey(p.Category))
                return;
            this.currentChipCategory = p.Category;
            ChipShopCategory chipShopCategory = Factory<ChipShopFactory, ChipShopCategory>.Instance.Items[p.Category];
            this.netIO.SendPacket((Packet)new SSMG_DEM_CHIP_SHOP_HEADER()
            {
                CategoryID = p.Category
            });
            foreach (ShopChip shopChip in chipShopCategory.Items.Values)
                this.netIO.SendPacket((Packet)new SSMG_DEM_CHIP_SHOP_DATA()
                {
                    EXP = shopChip.EXP,
                    JEXP = shopChip.JEXP,
                    ItemID = shopChip.ItemID,
                    Description = shopChip.Description
                });
            this.netIO.SendPacket((Packet)new SSMG_DEM_CHIP_SHOP_FOOTER());
        }

        /// <summary>
        /// The OnDEMChipClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_CHIP_CLOSE"/>.</param>
        public void OnDEMChipClose(CSMG_DEM_CHIP_CLOSE p)
        {
            this.chipShop = false;
        }

        /// <summary>
        /// The OnDEMChipBuy.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_DEM_CHIP_BUY"/>.</param>
        public void OnDEMChipBuy(CSMG_DEM_CHIP_BUY p)
        {
            if (!this.chipShop)
                return;
            uint[] items = p.ItemIDs;
            int[] counts = p.Counts;
            for (int i = 0; i < items.Length; ++i)
            {
                IEnumerable<ChipShopCategory> source = Factory<ChipShopFactory, ChipShopCategory>.Instance.Items.Values.Where<ChipShopCategory>((Func<ChipShopCategory, bool>)(item => item.Items.ContainsKey(items[i])));
                if (source.Count<ChipShopCategory>() > 0)
                {
                    ChipShopCategory chipShopCategory = source.First<ChipShopCategory>();
                    if (counts[i] > 0)
                    {
                        ShopChip shopChip = chipShopCategory.Items[items[i]];
                        if ((long)this.chara.CEXP > (long)shopChip.EXP * (long)counts[i] && (long)this.chara.JEXP > (long)shopChip.JEXP * (long)counts[i])
                        {
                            this.chara.CEXP -= (uint)((ulong)shopChip.EXP * (ulong)counts[i]);
                            this.chara.JEXP -= (uint)((ulong)shopChip.JEXP * (ulong)counts[i]);
                            SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(items[i]);
                            obj.Stack = (ushort)counts[i];
                            this.AddItem(obj, true);
                        }
                    }
                }
                this.SendEXP();
            }
        }

        /// <summary>
        /// The OnItemFusionCancel.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_FUSION_CANCEL"/>.</param>
        public void OnItemFusionCancel(CSMG_ITEM_FUSION_CANCEL p)
        {
            this.itemFusionEffect = 0U;
            this.itemFusionView = 0U;
            this.itemFusion = false;
        }

        /// <summary>
        /// The OnItemFusion.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_FUSION"/>.</param>
        public void OnItemFusion(CSMG_ITEM_FUSION p)
        {
            this.itemFusionEffect = p.EffectItem;
            this.itemFusionView = p.ViewItem;
            this.itemFusion = false;
        }

        /// <summary>
        /// The OnItemEnhanceClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_ENHANCE_CLOSE"/>.</param>
        public void OnItemEnhanceClose(CSMG_ITEM_ENHANCE_CLOSE p)
        {
            this.itemEnhance = false;
        }

        /// <summary>
        /// The OnItemEnhanceSelect.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_ENHANCE_SELECT"/>.</param>
        public void OnItemEnhanceSelect(CSMG_ITEM_ENHANCE_SELECT p)
        {
            SagaDB.Item.Item obj = this.chara.Inventory.GetItem(p.InventorySlot);
            if (obj == null)
                return;
            List<EnhanceDetail> enhanceDetailList = new List<EnhanceDetail>();
            if (obj.IsWeapon)
            {
                if (this.CountItem(90000044U) > 0)
                    enhanceDetailList.Add(new EnhanceDetail()
                    {
                        material = 90000044U,
                        type = EnhanceType.Atk,
                        value = this.FindEnhancementValue(obj, 90000044U)
                    });
                if (this.CountItem(90000045U) > 0)
                    enhanceDetailList.Add(new EnhanceDetail()
                    {
                        material = 90000045U,
                        type = EnhanceType.MAtk,
                        value = this.FindEnhancementValue(obj, 90000045U)
                    });
                if (this.CountItem(90000046U) > 0)
                    enhanceDetailList.Add(new EnhanceDetail()
                    {
                        material = 90000046U,
                        type = EnhanceType.Cri,
                        value = this.FindEnhancementValue(obj, 90000046U)
                    });
            }
            if (obj.IsArmor)
            {
                if (this.CountItem(90000043U) > 0)
                    enhanceDetailList.Add(new EnhanceDetail()
                    {
                        material = 90000043U,
                        type = EnhanceType.HP,
                        value = this.FindEnhancementValue(obj, 90000043U)
                    });
                if (this.CountItem(90000044U) > 0)
                    enhanceDetailList.Add(new EnhanceDetail()
                    {
                        material = 90000044U,
                        type = EnhanceType.Def,
                        value = this.FindEnhancementValue(obj, 90000044U)
                    });
                if (this.CountItem(90000045U) > 0)
                    enhanceDetailList.Add(new EnhanceDetail()
                    {
                        material = 90000045U,
                        type = EnhanceType.MDef,
                        value = this.FindEnhancementValue(obj, 90000045U)
                    });
                if (this.CountItem(90000046U) > 0)
                    enhanceDetailList.Add(new EnhanceDetail()
                    {
                        material = 90000046U,
                        type = EnhanceType.AvoidCri,
                        value = this.FindEnhancementValue(obj, 90000046U)
                    });
            }
            if (obj.BaseData.itemType == ItemType.SHIELD)
            {
                if (this.CountItem(90000044U) > 0)
                    enhanceDetailList.Add(new EnhanceDetail()
                    {
                        material = 90000044U,
                        type = EnhanceType.Def,
                        value = this.FindEnhancementValue(obj, 90000044U)
                    });
                if (this.CountItem(90000045U) > 0)
                    enhanceDetailList.Add(new EnhanceDetail()
                    {
                        material = 90000045U,
                        type = EnhanceType.MDef,
                        value = this.FindEnhancementValue(obj, 90000045U)
                    });
            }
            if (obj.BaseData.itemType == ItemType.ACCESORY_NECK && this.CountItem(90000045U) > 0)
                enhanceDetailList.Add(new EnhanceDetail()
                {
                    material = 90000045U,
                    type = EnhanceType.MDef,
                    value = this.FindEnhancementValue(obj, 90000045U)
                });
            this.netIO.SendPacket((Packet)new SSMG_ITEM_ENHANCE_DETAIL()
            {
                Items = enhanceDetailList
            });
        }

        /// <summary>
        /// The FindEnhancementValue.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        /// <param name="itemID">The itemID<see cref="uint"/>.</param>
        /// <returns>The <see cref="short"/>.</returns>
        private short FindEnhancementValue(SagaDB.Item.Item item, uint itemID)
        {
            short[] numArray1 = new short[10]
            {
        (short) 10,
        (short) 20,
        (short) 30,
        (short) 40,
        (short) 60,
        (short) 85,
        (short) 125,
        (short) 185,
        (short) 275,
        (short) 425
            };
            short[] numArray2 = new short[10]
            {
        (short) 2,
        (short) 4,
        (short) 6,
        (short) 9,
        (short) 13,
        (short) 18,
        (short) 24,
        (short) 31,
        (short) 40,
        (short) 52
            };
            short[] numArray3 = new short[10]
            {
        (short) 1,
        (short) 2,
        (short) 3,
        (short) 5,
        (short) 8,
        (short) 12,
        (short) 17,
        (short) 23,
        (short) 30,
        (short) 39
            };
            switch (itemID)
            {
                case 90000043:
                    if (item.IsArmor)
                    {
                        int index = 0;
                        while ((int)numArray1[index] <= (int)item.HP)
                            ++index;
                        return numArray1[index];
                    }
                    break;
                case 90000044:
                    if (item.IsWeapon)
                    {
                        int index = 0;
                        while ((int)numArray2[index] <= (int)item.Atk1)
                            ++index;
                        return numArray2[index];
                    }
                    if (item.IsArmor || item.BaseData.itemType == ItemType.SHIELD)
                    {
                        int index = 0;
                        while ((int)numArray3[index] <= (int)item.Def)
                            ++index;
                        return numArray3[index];
                    }
                    break;
                case 90000045:
                    if (item.IsWeapon)
                    {
                        int index = 0;
                        while ((int)numArray2[index] <= (int)item.MAtk)
                            ++index;
                        return numArray2[index];
                    }
                    int index1 = 0;
                    while ((int)numArray3[index1] <= (int)item.MDef)
                        ++index1;
                    return numArray3[index1];
                case 90000046:
                    if (item.IsWeapon)
                    {
                        int index2 = 0;
                        while ((int)numArray3[index2] <= (int)item.HitCritical)
                            ++index2;
                        return numArray3[index2];
                    }
                    if (item.IsArmor)
                    {
                        int index2 = 0;
                        while ((int)numArray3[index2] <= (int)item.AvoidCritical)
                            ++index2;
                        return numArray3[index2];
                    }
                    break;
            }
            return 0;
        }

        /// <summary>
        /// The OnItemEnhanceConfirm.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_ENHANCE_CONFIRM"/>.</param>
        public void OnItemEnhanceConfirm(CSMG_ITEM_ENHANCE_CONFIRM p)
        {
            SagaDB.Item.Item item = this.Character.Inventory.GetItem(p.InventorySlot);
            bool flag = false;
            SSMG_ITEM_ENHANCE_RESULT itemEnhanceResult = new SSMG_ITEM_ENHANCE_RESULT();
            itemEnhanceResult.Result = 0;
            int[] numArray = new int[10]
            {
        9999,
        9999,
        9999,
        8500,
        8200,
        5700,
        5000,
        5000,
        5000,
        2000
            };
            if (item != null)
            {
                if (this.CountItem(p.ItemID) > 0 && item.Refine < (ushort)10)
                {
                    if (this.Character.Gold >= 5000)
                    {
                        this.Character.Gold -= 5000;
                        if (SagaLib.Global.Random.Next(0, 9999) < numArray[(int)item.Refine])
                        {
                            if (item.IsArmor)
                            {
                                switch (p.ItemID)
                                {
                                    case 90000043:
                                        item.HP = this.FindEnhancementValue(item, 90000043U);
                                        this.DeleteItemID(90000043U, (ushort)1, true);
                                        break;
                                    case 90000044:
                                        item.Def = this.FindEnhancementValue(item, 90000044U);
                                        this.DeleteItemID(90000044U, (ushort)1, true);
                                        break;
                                    case 90000045:
                                        item.MDef = this.FindEnhancementValue(item, 90000045U);
                                        this.DeleteItemID(90000045U, (ushort)1, true);
                                        break;
                                    case 90000046:
                                        item.AvoidCritical = this.FindEnhancementValue(item, 90000046U);
                                        this.DeleteItemID(90000046U, (ushort)1, true);
                                        break;
                                }
                            }
                            if (item.IsWeapon)
                            {
                                switch (p.ItemID)
                                {
                                    case 90000044:
                                        item.Atk1 = this.FindEnhancementValue(item, 90000044U);
                                        item.Atk2 = item.Atk1;
                                        item.Atk3 = item.Atk1;
                                        this.DeleteItemID(90000044U, (ushort)1, true);
                                        break;
                                    case 90000045:
                                        item.MAtk = this.FindEnhancementValue(item, 90000045U);
                                        this.DeleteItemID(90000045U, (ushort)1, true);
                                        break;
                                    case 90000046:
                                        item.HitCritical = this.FindEnhancementValue(item, 90000046U);
                                        this.DeleteItemID(90000046U, (ushort)1, true);
                                        break;
                                }
                            }
                            if (item.BaseData.itemType == ItemType.SHIELD)
                            {
                                switch (p.ItemID)
                                {
                                    case 90000044:
                                        item.Def = this.FindEnhancementValue(item, 90000044U);
                                        this.DeleteItemID(90000044U, (ushort)1, true);
                                        break;
                                    case 90000045:
                                        item.MDef = this.FindEnhancementValue(item, 90000045U);
                                        this.DeleteItemID(90000045U, (ushort)1, true);
                                        break;
                                }
                            }
                            if (item.BaseData.itemType == ItemType.ACCESORY_NECK && p.ItemID == 90000045U)
                            {
                                item.MDef = this.FindEnhancementValue(item, 90000045U);
                                this.DeleteItemID(90000045U, (ushort)1, true);
                            }
                            this.SendEffect(5145U);
                            itemEnhanceResult.Result = 1;
                            ++item.Refine;
                            this.SendItemInfo(item);
                            Singleton<StatusFactory>.Instance.CalcStatus(this.chara);
                            this.SendPlayerInfo();
                        }
                        else
                        {
                            flag = true;
                            this.SendEffect(5146U);
                            itemEnhanceResult.Result = 0;
                            if (this.CountItem(16001300U) == 0)
                                this.DeleteItem(p.InventorySlot, (ushort)1, true);
                            else
                                this.DeleteItemID(16001300U, (ushort)1, true);
                            this.DeleteItemID(p.ItemID, (ushort)1, true);
                        }
                    }
                    else
                        itemEnhanceResult.Result = -1;
                }
            }
            else
                itemEnhanceResult.Result = -2;
            if ((item.IsArmor && (this.CountItem(90000044U) > 0 || this.CountItem(90000045U) > 0 || (this.CountItem(90000046U) > 0 || this.CountItem(90000043U) > 0)) || item.IsWeapon && (this.CountItem(90000044U) > 0 || this.CountItem(90000045U) > 0 || this.CountItem(90000046U) > 0) || (item.BaseData.itemType == ItemType.SHIELD && (this.CountItem(90000044U) > 0 || this.CountItem(90000045U) > 0) || item.BaseData.itemType == ItemType.ACCESORY_NECK && this.CountItem(90000044U) > 0)) && item.Refine < (ushort)10)
            {
                this.netIO.SendPacket((Packet)itemEnhanceResult);
                this.OnItemEnhanceSelect(new CSMG_ITEM_ENHANCE_SELECT()
                {
                    InventorySlot = p.InventorySlot
                });
            }
            else
            {
                itemEnhanceResult.Result = 252;
                this.netIO.SendPacket((Packet)itemEnhanceResult);
                if (flag)
                {
                    List<SagaDB.Item.Item> list = this.chara.Inventory.GetContainer(ContainerType.BODY).Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(item2 => (item2.IsArmor && (this.CountItem(90000044U) > 0 || this.CountItem(90000045U) > 0 || (this.CountItem(90000046U) > 0 || this.CountItem(90000043U) > 0)) || item2.IsWeapon && (this.CountItem(90000044U) > 0 || this.CountItem(90000045U) > 0 || this.CountItem(90000046U) > 0) || (item2.BaseData.itemType == ItemType.SHIELD && (this.CountItem(90000044U) > 0 || this.CountItem(90000045U) > 0) || item2.BaseData.itemType == ItemType.ACCESORY_NECK && this.CountItem(90000044U) > 0)) && item2.Refine < (ushort)10)).Select<SagaDB.Item.Item, SagaDB.Item.Item>((Func<SagaDB.Item.Item, SagaDB.Item.Item>)(item2 => item)).ToList<SagaDB.Item.Item>();
                    if (list.Count > 0)
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_ENHANCE_LIST()
                        {
                            Items = list
                        });
                    else
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_ENHANCE_RESULT()
                        {
                            Result = 252
                        });
                }
            }
        }

        /// <summary>
        /// The OnItemUse.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_USE"/>.</param>
        public void OnItemUse(CSMG_ITEM_USE p)
        {
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            SagaDB.Item.Item obj = this.Character.Inventory.GetItem(p.InventorySlot);
            SkillArg skill = new SkillArg();
            skill.sActor = this.Character.ActorID;
            skill.dActor = p.ActorID;
            skill.item = obj;
            skill.x = p.X;
            skill.y = p.Y;
            skill.argType = SkillArg.ArgType.Item_Cast;
            skill.inventorySlot = p.InventorySlot;
            if (obj == null)
                return;
            SagaDB.Actor.Actor actor1 = this.map.GetActor(p.ActorID);
            if (this.Character.PossessionTarget != 0U)
            {
                SagaDB.Actor.Actor actor2 = this.Map.GetActor(this.Character.PossessionTarget);
                if (actor2 != null)
                {
                    if (actor2.type == ActorType.PC)
                    {
                        if ((int)skill.dActor == (int)this.Character.ActorID)
                            skill.dActor = actor2.ActorID;
                    }
                    else
                        skill.result = (short)-21;
                }
            }
            if (obj.BaseData.itemType == ItemType.MARIONETTE && skill.result == (short)0)
            {
                if (this.Character.Marionette == null && DateTime.Now < this.Character.NextMarionetteTime)
                    skill.result = (short)-18;
                if (this.Character.Pet != null && this.Character.Pet.Ride)
                    skill.result = (short)-32;
                if (this.Character.PossessionTarget != 0U || this.Character.PossesionedActors.Count > 0)
                    skill.result = (short)-16;
                if (this.chara.Race == PC_RACE.DEM)
                    skill.result = (short)-33;
            }
            if (this.GetPossessionTarget() != null && skill.result == (short)0 && (this.GetPossessionTarget().Buff.Dead && (obj.ItemID != 10000604U && obj.ItemID != 10034104U)))
                skill.result = (short)-27;
            if (actor1 != null && skill.result == (short)0 && (!actor1.Buff.Dead && (obj.ItemID == 10000604U || obj.ItemID == 10034104U)))
                skill.result = (short)-23;
            if (this.scriptThread != null && skill.result == (short)0)
                skill.result = (short)-7;
            if (this.Character.Buff.Dead && skill.result == (short)0)
                skill.result = (short)-9;
            if (skill.result == (short)0 && this.Character.Tasks.ContainsKey("ItemCast"))
                skill.result = (short)-19;
            if (skill.result == (short)0)
            {
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SKILL, (MapEventArgs)skill, (SagaDB.Actor.Actor)this.Character, true);
                if (obj.BaseData.cast > 0U)
                {
                    SkillCast skillCast = new SkillCast(this, skill);
                    this.Character.Tasks.Add("ItemCast", (MultiRunTask)skillCast);
                    skillCast.Activate();
                }
                else
                    this.OnItemCastComplete(skill);
            }
            else
                this.Character.e.OnActorSkillUse((SagaDB.Actor.Actor)this.Character, (MapEventArgs)skill);
        }

        /// <summary>
        /// The OnItemCastComplete.
        /// </summary>
        /// <param name="skill">The skill<see cref="SkillArg"/>.</param>
        public void OnItemCastComplete(SkillArg skill)
        {
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            if (skill.dActor != uint.MaxValue)
            {
                SagaDB.Actor.Actor actor = this.Map.GetActor(skill.dActor);
                this.Character.Tasks.Remove("ItemCast");
                skill.argType = SkillArg.ArgType.Item_Active;
                Singleton<SkillHandler>.Instance.ItemUse((SagaDB.Actor.Actor)this.Character, actor, skill);
            }
            else
            {
                this.Character.Tasks.Remove("ItemCast");
                skill.argType = SkillArg.ArgType.Item_Active;
            }
            if (skill.item.BaseData.usable || skill.item.BaseData.itemType == ItemType.POTION || skill.item.BaseData.itemType == ItemType.SCROLL || skill.item.BaseData.itemType == ItemType.FREESCROLL)
            {
                if (skill.item.Durability > (ushort)0)
                    --skill.item.Durability;
                this.SendItemInfo(skill.item);
                if (skill.item.Durability == (ushort)0)
                {
                    Logger.LogItemLost(Logger.EventType.ItemUseLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", skill.item.BaseData.name + "(" + (object)skill.item.ItemID + ")", string.Format("ItemUse Count:{0}", (object)1), false);
                    this.DeleteItem(skill.inventorySlot, (ushort)1, true);
                }
            }
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SKILL, (MapEventArgs)skill, (SagaDB.Actor.Actor)this.Character, true);
            if (skill.item.BaseData.effectID != 0U)
                this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
                {
                    actorID = skill.dActor,
                    effectID = skill.item.BaseData.effectID
                }, (SagaDB.Actor.Actor)this.Character, true);
            if (skill.item.BaseData.activateSkill != (ushort)0)
                this.OnSkillCast(new CSMG_SKILL_CAST()
                {
                    ActorID = skill.dActor,
                    SkillID = skill.item.BaseData.activateSkill,
                    SkillLv = (byte)1,
                    X = skill.x,
                    Y = skill.y,
                    Random = (short)SagaLib.Global.Random.Next()
                }, true, true);
            if (skill.item.BaseData.itemType == ItemType.MARIONETTE)
            {
                if (this.Character.Marionette == null)
                {
                    this.MarionetteActivate(skill.item.BaseData.marionetteID);
                }
                else
                {
                    if (!this.Character.Status.Additions.ContainsKey("ChangeMarionette"))
                    {
                        this.MarionetteDeactivate();
                        return;
                    }
                    this.MarionetteActivate(skill.item.BaseData.marionetteID, false, false);
                    return;
                }
            }
            if (skill.item.BaseData.eventID != 0U)
                this.EventActivate(skill.item.BaseData.eventID);
            if (skill.item.BaseData.itemType != ItemType.GOLEM)
                return;
            if (this.Character.Golem == null)
                this.Character.Golem = new ActorGolem();
            this.Character.Golem.Item = skill.item;
            this.EventActivate(4294967091U);
        }

        /// <summary>
        /// The OnItemDrop.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_DROP"/>.</param>
        public void OnItemDrop(CSMG_ITEM_DROP p)
        {
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
                this.Character.Status.Additions["Hiding"].AdditionEnd();
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            SagaDB.Item.Item obj1 = this.Character.Inventory.GetItem(p.InventorySlot);
            ushort num = p.Count;
            if ((int)num > (int)obj1.Stack)
                num = obj1.Stack;
            SSMG_ITEM_PUT_ERROR ssmgItemPutError = new SSMG_ITEM_PUT_ERROR();
            if (obj1.BaseData.events == 1U)
            {
                ssmgItemPutError.ErrorID = -3;
                this.netIO.SendPacket((Packet)ssmgItemPutError);
            }
            else if (this.trading)
            {
                ssmgItemPutError.ErrorID = -8;
                this.netIO.SendPacket((Packet)ssmgItemPutError);
            }
            else if (obj1.BaseData.itemType == ItemType.DEMIC_CHIP)
            {
                ssmgItemPutError.ErrorID = -18;
                this.netIO.SendPacket((Packet)ssmgItemPutError);
            }
            else
            {
                if (obj1.Stack > (ushort)0)
                    Logger.LogItemLost(Logger.EventType.ItemDropLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj1.BaseData.name + "(" + (object)obj1.ItemID + ")", string.Format("Drop Count:{0}", (object)num), false);
                switch (this.Character.Inventory.DeleteItem(p.InventorySlot, (int)num))
                {
                    case InventoryDeleteResult.ALL_DELETED:
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                        {
                            InventorySlot = p.InventorySlot
                        });
                        break;
                    case InventoryDeleteResult.STACK_UPDATED:
                        SSMG_ITEM_COUNT_UPDATE ssmgItemCountUpdate = new SSMG_ITEM_COUNT_UPDATE();
                        SagaDB.Item.Item obj2 = this.Character.Inventory.GetItem(p.InventorySlot);
                        obj1 = obj2.Clone();
                        obj1.Stack = num;
                        ssmgItemCountUpdate.InventorySlot = p.InventorySlot;
                        ssmgItemCountUpdate.Stack = obj2.Stack;
                        this.netIO.SendPacket((Packet)ssmgItemCountUpdate);
                        break;
                }
                ActorItem actorItem = new ActorItem(obj1);
                actorItem.e = (ActorEventHandler)new ItemEventHandler((SagaDB.Actor.Actor)actorItem);
                actorItem.MapID = this.Character.MapID;
                actorItem.X = this.Character.X;
                actorItem.Y = this.Character.Y;
                if (obj1.BaseData.noTrade)
                {
                    actorItem.Owner = (SagaDB.Actor.Actor)this.chara;
                    actorItem.CreateTime = DateTime.Now + new TimeSpan(0, 3, 0);
                }
                this.map.RegisterActor((SagaDB.Actor.Actor)actorItem);
                actorItem.invisble = false;
                this.map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorItem);
                DeleteItem deleteItem = new DeleteItem(actorItem);
                deleteItem.Activate();
                actorItem.Tasks.Add("DeleteItem", (MultiRunTask)deleteItem);
                this.Character.Inventory.CalcPayloadVolume();
                this.SendCapacity();
                this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_DELETED, (object)obj1.BaseData.name, (object)obj1.Stack));
            }
        }

        /// <summary>
        /// The OnItemGet.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_GET"/>.</param>
        public void OnItemGet(CSMG_ITEM_GET p)
        {
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
                this.Character.Status.Additions["Hiding"].AdditionEnd();
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            ActorItem actor = (ActorItem)this.map.GetActor(p.ActorID);
            if (actor == null)
                return;
            if (actor.Owner != null && actor.Owner.type == ActorType.PC)
            {
                ActorPC owner = (ActorPC)actor.Owner;
                if (owner != this.Character)
                {
                    if (owner.Party != null && !actor.Party)
                    {
                        if (!owner.Party.IsMember(this.Character) && (DateTime.Now - actor.CreateTime).TotalMinutes < 1.0)
                        {
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_GET_ERROR()
                            {
                                ActorID = actor.ActorID,
                                ErrorID = -10
                            });
                            return;
                        }
                    }
                    else if ((DateTime.Now - actor.CreateTime).TotalMinutes < 1.0 || actor.Party)
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_GET_ERROR()
                        {
                            ActorID = actor.ActorID,
                            ErrorID = -10
                        });
                        return;
                    }
                }
            }
            if (!actor.PossessionItem)
            {
                actor.LootedBy = this.Character.ActorID;
                this.map.DeleteActor((SagaDB.Actor.Actor)actor);
                Logger.LogItemGet(Logger.EventType.ItemLootGet, this.Character.Name + "(" + (object)this.Character.CharID + ")", actor.Item.BaseData.name + "(" + (object)actor.Item.ItemID + ")", string.Format("ItemLoot Count:{0}", (object)actor.Item.Stack), false);
                this.AddItem(actor.Item, true);
                actor.Tasks["DeleteItem"].Deactivate();
                actor.Tasks.Remove("DeleteItem");
            }
            else
            {
                foreach (EnumEquipSlot key in actor.Item.EquipSlot)
                {
                    if (this.Character.Inventory.Equipments.ContainsKey(key))
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_GET_ERROR()
                        {
                            ActorID = actor.ActorID,
                            ErrorID = -5
                        });
                        return;
                    }
                }
                if (this.chara.Race == PC_RACE.DEM && this.chara.Form == DEM_FORM.MACHINA_FORM)
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_GET_ERROR()
                    {
                        ActorID = actor.ActorID,
                        ErrorID = -16
                    });
                else if (Math.Abs((int)this.Character.Level - (int)actor.Item.PossessionedActor.Level) > 30)
                {
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_GET_ERROR()
                    {
                        ActorID = actor.ActorID,
                        ErrorID = -4
                    });
                }
                else
                {
                    int num = this.CheckEquipRequirement(actor.Item);
                    if (num < 0)
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                        {
                            InventorySlot = uint.MaxValue,
                            Target = ContainerType.NONE,
                            Result = num,
                            Range = this.Character.Range
                        });
                    }
                    else
                    {
                        actor.LootedBy = this.Character.ActorID;
                        this.map.DeleteActor((SagaDB.Actor.Actor)actor);
                        SagaDB.Item.Item obj = actor.Item.Clone();
                        this.AddItem(obj, true);
                        this.OnItemEquipt(new CSMG_ITEM_EQUIPT()
                        {
                            InventoryID = obj.Slot
                        });
                    }
                }
            }
        }

        /// <summary>
        /// The CheckEquipRequirement.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int CheckEquipRequirement(SagaDB.Item.Item item)
        {
            if ((int)this.Character.Str < (int)item.BaseData.possibleStr)
                return -16;
            if ((int)this.Character.Dex < (int)item.BaseData.possibleDex)
                return -19;
            if ((int)this.Character.Agi < (int)item.BaseData.possibleAgi)
                return -20;
            if ((int)this.Character.Vit < (int)item.BaseData.possibleVit)
                return -18;
            if ((int)this.Character.Int < (int)item.BaseData.possibleInt)
                return -21;
            if ((int)this.Character.Mag < (int)item.BaseData.possibleMag)
                return -17;
            if (!item.BaseData.possibleRace[this.Character.Race])
                return -13;
            if (!item.IsParts && this.chara.Race != PC_RACE.DEM)
            {
                if (this.Character.JobJoint == PC_JOB.NONE)
                {
                    if (!item.BaseData.possibleJob[this.Character.Job])
                        return -2;
                }
                else if (!item.BaseData.possibleJob[this.Character.JobJoint])
                    return -2;
            }
            if (!item.BaseData.possibleGender[this.Character.Gender])
                return -14;
            if ((!this.map.Info.Flag.Test(MapFlags.Dominion) ? (int)this.Character.Level : (int)this.Character.DominionLevel) < (int)item.BaseData.possibleLv)
                return -15;
            return item.BaseData.itemType == ItemType.RIDE_PET && this.Character.Marionette != null ? -2 : 0;
        }

        /// <summary>
        /// The OnItemEquipt.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_EQUIPT"/>.</param>
        public void OnItemEquipt(CSMG_ITEM_EQUIPT p)
        {
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
                this.Character.Status.Additions["Hiding"].AdditionEnd();
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            if (this.Character.Tasks.ContainsKey("Regeneration"))
            {
                this.Character.Tasks["Regeneration"].Deactivate();
                this.Character.Tasks.Remove("Regeneration");
            }
            SagaDB.Item.Item pet = this.Character.Inventory.GetItem(p.InventoryID);
            if (pet == null)
                return;
            int num1 = this.CheckEquipRequirement(pet);
            if (num1 == 0 && (this.chara.Race == PC_RACE.DEM && this.chara.Form == DEM_FORM.MACHINA_FORM))
                num1 = -29;
            if (num1 < 0)
            {
                this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                {
                    InventorySlot = uint.MaxValue,
                    Target = ContainerType.NONE,
                    Result = num1,
                    Range = this.Character.Range
                });
            }
            else
            {
                uint num2 = 0;
                foreach (EnumEquipSlot key1 in pet.EquipSlot)
                {
                    if (pet.BaseData.itemType == ItemType.ARROW || pet.BaseData.itemType == ItemType.BULLET)
                    {
                        switch (pet.BaseData.itemType)
                        {
                            case ItemType.BULLET:
                                if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                                {
                                    if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.GUN && this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.RIFLE && this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.DUALGUN)
                                    {
                                        this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                                        {
                                            InventorySlot = uint.MaxValue,
                                            Target = ContainerType.NONE,
                                            Result = -7,
                                            Range = this.Character.Range
                                        });
                                        return;
                                    }
                                    break;
                                }
                                this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                                {
                                    InventorySlot = uint.MaxValue,
                                    Target = ContainerType.NONE,
                                    Result = -7,
                                    Range = this.Character.Range
                                });
                                return;
                            case ItemType.ARROW:
                                if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                                {
                                    if (this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType != ItemType.BOW)
                                    {
                                        this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                                        {
                                            InventorySlot = uint.MaxValue,
                                            Target = ContainerType.NONE,
                                            Result = -6,
                                            Range = this.Character.Range
                                        });
                                        return;
                                    }
                                    break;
                                }
                                this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                                {
                                    InventorySlot = uint.MaxValue,
                                    Target = ContainerType.NONE,
                                    Result = -6,
                                    Range = this.Character.Range
                                });
                                return;
                        }
                        if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND) && ((this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.ARROW || this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType == ItemType.BULLET) && (int)this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].ItemID != (int)pet.ItemID))
                        {
                            SagaDB.Item.Item equipment = this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND];
                            this.ItemMoveSub(equipment, ContainerType.BODY, equipment.Stack);
                        }
                    }
                    else if (this.Character.Inventory.Equipments.ContainsKey(key1))
                    {
                        SagaDB.Item.Item equipment1 = this.Character.Inventory.Equipments[key1];
                        if (equipment1.PossessionedActor != null)
                        {
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                            {
                                InventorySlot = uint.MaxValue,
                                Target = ContainerType.NONE,
                                Result = -10,
                                Range = this.Character.Range
                            });
                            return;
                        }
                        if (equipment1.BaseData.possibleSkill != (ushort)0 && Singleton<SkillFactory>.Instance.GetSkill((uint)equipment1.BaseData.possibleSkill, (byte)1) != null && this.Character.Skills.ContainsKey((uint)equipment1.BaseData.possibleSkill))
                            this.Character.Skills.Remove((uint)equipment1.BaseData.possibleSkill);
                        if (equipment1.BaseData.passiveSkill != (ushort)0 && Singleton<SkillFactory>.Instance.GetSkill((uint)equipment1.BaseData.passiveSkill, (byte)1) != null && this.Character.Skills.ContainsKey((uint)equipment1.BaseData.passiveSkill))
                        {
                            this.Character.Skills.Remove((uint)equipment1.BaseData.passiveSkill);
                            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
                        }
                        if (equipment1.BaseData.jointJob != PC_JOB.NONE)
                            this.Character.JobJoint = PC_JOB.NONE;
                        if (equipment1.BaseData.itemType == ItemType.BACK_DEMON)
                        {
                            SkillHandler.RemoveAddition((SagaDB.Actor.Actor)this.chara, "MoveUp2");
                            SkillHandler.RemoveAddition((SagaDB.Actor.Actor)this.chara, "MoveUp3");
                        }
                        foreach (EnumEquipSlot key2 in equipment1.EquipSlot)
                        {
                            if (this.Character.Inventory.Equipments.ContainsKey(key2))
                            {
                                SagaDB.Item.Item equipment2 = this.Character.Inventory.Equipments[key2];
                                if (equipment2.Stack == (ushort)0)
                                {
                                    this.Character.Inventory.Equipments.Remove(key2);
                                }
                                else
                                {
                                    if (key2 == EnumEquipSlot.PET && this.Character.Pet != null)
                                    {
                                        if (this.Character.Pet.Ride)
                                        {
                                            num2 = this.Character.Pet.HP;
                                            this.Character.HP = num2;
                                            this.Character.Speed = (ushort)420;
                                            this.Character.Pet = (ActorPet)null;
                                        }
                                        this.DeletePet();
                                    }
                                    if (this.Character.Inventory.MoveItem((ContainerType)Enum.Parse(typeof(ContainerType), key2.ToString()), (int)equipment2.Slot, ContainerType.BODY, (int)equipment2.Stack))
                                    {
                                        this.netIO.SendPacket((Packet)new SSMG_ITEM_CONTAINER_CHANGE()
                                        {
                                            InventorySlot = equipment2.Slot,
                                            Target = ContainerType.BODY
                                        });
                                        this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                                        {
                                            InventorySlot = uint.MaxValue,
                                            Target = ContainerType.NONE,
                                            Result = 1,
                                            Range = this.Character.Range
                                        });
                                        Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                                        this.SendPlayerInfo();
                                    }
                                }
                            }
                        }
                    }
                }
                ushort num3 = pet.Stack;
                if ((pet.BaseData.itemType == ItemType.ARROW || pet.BaseData.itemType == ItemType.BULLET) && this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND) && (int)this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack + (int)num3 > 999)
                {
                    ushort num4 = (ushort)((int)this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack + (int)num3 - 999);
                    num3 = (ushort)((uint)pet.Stack - (uint)num4);
                }
                if (num3 == (ushort)0)
                    return;
                if (this.Character.Inventory.MoveItem(this.Character.Inventory.GetContainerType(pet.Slot), (int)pet.Slot, (ContainerType)Enum.Parse(typeof(ContainerType), pet.EquipSlot[0].ToString()), (int)num3))
                {
                    if (pet.Stack == (ushort)0)
                    {
                        SSMG_ITEM_EQUIP ssmgItemEquip = new SSMG_ITEM_EQUIP();
                        ssmgItemEquip.Target = (ContainerType)Enum.Parse(typeof(ContainerType), pet.EquipSlot[0].ToString());
                        ssmgItemEquip.InventorySlot = pet.Slot;
                        Singleton<StatusFactory>.Instance.CalcRange(this.Character);
                        ssmgItemEquip.Range = this.Character.Range;
                        this.netIO.SendPacket((Packet)ssmgItemEquip);
                    }
                    else
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                        {
                            InventorySlot = pet.Slot,
                            Stack = pet.Stack
                        });
                    if (pet.BaseData.itemType == ItemType.ARROW || pet.BaseData.itemType == ItemType.BULLET)
                    {
                        if (pet.Stack == (ushort)0)
                            this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Slot = pet.Slot;
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                        {
                            InventorySlot = this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Slot,
                            Stack = this.Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].Stack
                        });
                    }
                }
                List<EnumEquipSlot> equipSlot = pet.EquipSlot;
                if (equipSlot.Count > 1 && pet.BaseData.itemType != ItemType.BULLET && pet.BaseData.itemType != ItemType.ARROW)
                {
                    for (int index = 1; index < equipSlot.Count; ++index)
                    {
                        SagaDB.Item.Item obj = pet.Clone();
                        obj.Stack = (ushort)0;
                        int num4 = (int)this.Character.Inventory.AddItem((ContainerType)Enum.Parse(typeof(ContainerType), equipSlot[index].ToString()), obj);
                    }
                }
                if (pet.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                {
                    this.SendAttackType();
                    Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
                }
                Singleton<SkillHandler>.Instance.CheckBuffValid(this.Character);
                if (pet.BaseData.itemType == ItemType.PET || pet.BaseData.itemType == ItemType.PET_NEKOMATA)
                    this.SendPet(pet);
                if (pet.BaseData.itemType == ItemType.RIDE_PET)
                {
                    ActorPet actorPet = new ActorPet(pet.BaseData.petID, pet);
                    actorPet.Owner = this.Character;
                    this.Character.Pet = actorPet;
                    actorPet.Ride = true;
                    if (num2 == 0U)
                        actorPet.HP = this.Character.HP;
                    else
                        actorPet.HP = num2;
                    this.Character.HP = actorPet.MaxHP;
                }
                if (pet.BaseData.possibleSkill != (ushort)0)
                {
                    SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill((uint)pet.BaseData.possibleSkill, (byte)1);
                    if (skill != null && !this.Character.Skills.ContainsKey((uint)pet.BaseData.possibleSkill))
                        this.Character.Skills.Add((uint)pet.BaseData.possibleSkill, skill);
                }
                if (pet.BaseData.passiveSkill != (ushort)0)
                {
                    SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill((uint)pet.BaseData.passiveSkill, (byte)1);
                    if (skill != null && !this.Character.Skills.ContainsKey((uint)pet.BaseData.passiveSkill))
                    {
                        this.Character.Skills.Add((uint)pet.BaseData.passiveSkill, skill);
                        if (!skill.BaseData.active)
                            Singleton<SkillHandler>.Instance.SkillCast((SagaDB.Actor.Actor)this.Character, (SagaDB.Actor.Actor)this.Character, new SkillArg()
                            {
                                skill = skill
                            });
                    }
                }
                if (pet.BaseData.jointJob != PC_JOB.NONE)
                    this.Character.JobJoint = pet.BaseData.jointJob;
                if (pet.PossessionedActor != null)
                {
                    PossessionArg possessionArg = new PossessionArg();
                    possessionArg.fromID = pet.PossessionedActor.ActorID;
                    possessionArg.toID = this.Character.ActorID;
                    possessionArg.result = (int)pet.PossessionedActor.PossessionPosition;
                    pet.PossessionedActor.PossessionTarget = this.Character.ActorID;
                    MapServer.charDB.SaveChar(pet.PossessionedActor, false, false);
                    MapServer.accountDB.WriteUser(pet.PossessionedActor.Account);
                    string str = "";
                    switch (pet.PossessionedActor.PossessionPosition)
                    {
                        case PossessionPosition.RIGHT_HAND:
                            str = Singleton<LocalManager>.Instance.Strings.POSSESSION_RIGHT;
                            break;
                        case PossessionPosition.LEFT_HAND:
                            str = Singleton<LocalManager>.Instance.Strings.POSSESSION_LEFT;
                            break;
                        case PossessionPosition.NECK:
                            str = Singleton<LocalManager>.Instance.Strings.POSSESSION_NECK;
                            break;
                        case PossessionPosition.CHEST:
                            str = Singleton<LocalManager>.Instance.Strings.POSSESSION_ARMOR;
                            break;
                    }
                    this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.POSSESSION_DONE, (object)str));
                    if (pet.PossessionedActor.Online)
                        MapClient.FromActorPC(pet.PossessionedActor).SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.POSSESSION_DONE, (object)str));
                    this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.POSSESSION, (MapEventArgs)possessionArg, (SagaDB.Actor.Actor)this.Character, true);
                }
                Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                this.SendPlayerInfo();
                this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHANGE_EQUIP, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
                List<SagaDB.Item.Item> objList = new List<SagaDB.Item.Item>();
                foreach (SagaDB.Item.Item obj in this.chara.Inventory.Equipments.Values)
                {
                    if (obj.Stack != (ushort)0 && this.CheckEquipRequirement(obj) != 0)
                        objList.Add(obj);
                }
                foreach (SagaDB.Item.Item obj in objList)
                {
                    CSMG_ITEM_MOVE p1 = new CSMG_ITEM_MOVE();
                    p1.data = new byte[9];
                    p1.Count = (ushort)1;
                    p1.InventoryID = obj.Slot;
                    p1.Target = ContainerType.BODY;
                    this.OnItemMove(p1);
                }
            }
        }

        /// <summary>
        /// The OnItemMove.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_MOVE"/>.</param>
        public void OnItemMove(CSMG_ITEM_MOVE p)
        {
            this.OnItemMove(p, false);
        }

        /// <summary>
        /// The OnItemMove.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_MOVE"/>.</param>
        /// <param name="possessionRemove">The possessionRemove<see cref="bool"/>.</param>
        public void OnItemMove(CSMG_ITEM_MOVE p, bool possessionRemove)
        {
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            SagaDB.Item.Item obj = this.Character.Inventory.GetItem(p.InventoryID);
            if (p.Target >= ContainerType.HEAD)
            {
                this.netIO.SendPacket((Packet)new SSMG_ITEM_CONTAINER_CHANGE()
                {
                    InventorySlot = obj.Slot,
                    Result = -3,
                    Target = ContainerType.NONE
                });
            }
            else
            {
                if (this.Character.Inventory.IsContainerEquip(this.Character.Inventory.GetContainerType(obj.Slot)))
                {
                    if (obj.PossessionedActor != null && !possessionRemove)
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_CONTAINER_CHANGE()
                        {
                            InventorySlot = obj.Slot,
                            Result = -4,
                            Target = ContainerType.NONE
                        });
                        return;
                    }
                    if (this.chara.Race == PC_RACE.DEM && this.chara.Form == DEM_FORM.MACHINA_FORM)
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_CONTAINER_CHANGE()
                        {
                            InventorySlot = obj.Slot,
                            Result = -10,
                            Target = ContainerType.NONE
                        });
                        return;
                    }
                    if (obj.BaseData.possibleSkill != (ushort)0 && Singleton<SkillFactory>.Instance.GetSkill((uint)obj.BaseData.possibleSkill, (byte)1) != null && this.Character.Skills.ContainsKey((uint)obj.BaseData.possibleSkill))
                        this.Character.Skills.Remove((uint)obj.BaseData.possibleSkill);
                    if (obj.BaseData.passiveSkill != (ushort)0 && Singleton<SkillFactory>.Instance.GetSkill((uint)obj.BaseData.passiveSkill, (byte)1) != null && this.Character.Skills.ContainsKey((uint)obj.BaseData.passiveSkill))
                    {
                        this.Character.Skills.Remove((uint)obj.BaseData.passiveSkill);
                        Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
                    }
                    if (obj.BaseData.jointJob != PC_JOB.NONE)
                        this.Character.JobJoint = PC_JOB.NONE;
                    if (obj.BaseData.itemType == ItemType.BACK_DEMON)
                    {
                        SkillHandler.RemoveAddition((SagaDB.Actor.Actor)this.chara, "MoveUp2");
                        SkillHandler.RemoveAddition((SagaDB.Actor.Actor)this.chara, "MoveUp3");
                    }
                    if (possessionRemove)
                        return;
                    List<EnumEquipSlot> equipSlot = obj.EquipSlot;
                    if (equipSlot.Count > 1)
                    {
                        for (int index = 1; index < equipSlot.Count; ++index)
                        {
                            if (this.Character.Inventory.Equipments[equipSlot[index]].BaseData.itemType == ItemType.ARROW || this.Character.Inventory.Equipments[equipSlot[index]].BaseData.itemType == ItemType.BULLET || (this.Character.Inventory.Equipments[equipSlot[index]].BaseData.itemType == ItemType.BOW || this.Character.Inventory.Equipments[equipSlot[index]].BaseData.itemType == ItemType.GUN) || this.Character.Inventory.Equipments[equipSlot[index]].BaseData.itemType == ItemType.RIFLE)
                            {
                                SagaDB.Item.Item equipment = this.Character.Inventory.Equipments[equipSlot[index]];
                                if (equipment.Stack != (ushort)0)
                                    this.ItemMoveSub(equipment, ContainerType.BODY, equipment.Stack);
                                else
                                    this.Character.Inventory.Equipments.Remove(equipSlot[index]);
                            }
                            else
                                this.Character.Inventory.Equipments.Remove(equipSlot[index]);
                        }
                    }
                    else if (equipSlot[0] == EnumEquipSlot.PET && this.Character.Pet != null)
                        this.DeletePet();
                }
                this.ItemMoveSub(obj, p.Target, p.Count);
            }
        }

        /// <summary>
        /// The ItemMoveSub.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        /// <param name="container">The container<see cref="ContainerType"/>.</param>
        /// <param name="count">The count<see cref="ushort"/>.</param>
        private void ItemMoveSub(SagaDB.Item.Item item, ContainerType container, ushort count)
        {
            bool flag = this.Character.Inventory.IsContainerEquip(this.Character.Inventory.GetContainerType(item.Slot));
            uint slot = item.Slot;
            if (this.Character.Inventory.MoveItem(this.Character.Inventory.GetContainerType(item.Slot), (int)item.Slot, container, (int)count))
            {
                if (item.Stack == (ushort)0)
                {
                    if ((int)slot == (int)this.Character.Inventory.LastItem.Slot)
                    {
                        if (!flag)
                        {
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                            {
                                InventorySlot = item.Slot
                            });
                            SSMG_ITEM_ADD ssmgItemAdd = new SSMG_ITEM_ADD();
                            ssmgItemAdd.Container = container;
                            ssmgItemAdd.InventorySlot = item.Slot;
                            item.Stack = count;
                            ssmgItemAdd.Item = item;
                            this.netIO.SendPacket((Packet)ssmgItemAdd);
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_CONTAINER_CHANGE()
                            {
                                InventorySlot = item.Slot,
                                Target = container
                            });
                        }
                        else
                        {
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_CONTAINER_CHANGE()
                            {
                                InventorySlot = item.Slot,
                                Target = container
                            });
                            SSMG_ITEM_EQUIP ssmgItemEquip = new SSMG_ITEM_EQUIP();
                            ssmgItemEquip.InventorySlot = uint.MaxValue;
                            ssmgItemEquip.Target = ContainerType.NONE;
                            ssmgItemEquip.Result = 1;
                            Singleton<StatusFactory>.Instance.CalcRange(this.Character);
                            if (item.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND)
                            {
                                this.SendAttackType();
                                Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
                            }
                            ssmgItemEquip.Range = this.Character.Range;
                            this.netIO.SendPacket((Packet)ssmgItemEquip);
                            this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHANGE_EQUIP, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
                            if (item.EquipSlot[0] == EnumEquipSlot.PET && this.Character.Pet != null && this.Character.Pet.Ride)
                            {
                                this.Character.Speed = (ushort)420;
                                this.Character.HP = this.Character.Pet.HP;
                                this.Character.Pet = (ActorPet)null;
                            }
                            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                            this.SendPlayerInfo();
                        }
                    }
                    else
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                        {
                            InventorySlot = slot
                        });
                        if ((int)slot != (int)item.Slot)
                        {
                            item = this.Character.Inventory.GetItem(item.Slot);
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                            {
                                InventorySlot = item.Slot,
                                Stack = item.Stack
                            });
                            item = this.Character.Inventory.LastItem;
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_ADD()
                            {
                                Container = container,
                                InventorySlot = item.Slot,
                                Item = item
                            });
                        }
                        else
                        {
                            item = this.Character.Inventory.LastItem;
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                            {
                                InventorySlot = item.Slot,
                                Stack = item.Stack
                            });
                        }
                    }
                }
                else
                {
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                    {
                        InventorySlot = item.Slot,
                        Stack = item.Stack
                    });
                    if ((int)this.Character.Inventory.LastItem.Stack == (int)count)
                    {
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_ADD()
                        {
                            Container = container,
                            InventorySlot = this.Character.Inventory.LastItem.Slot,
                            Item = this.Character.Inventory.LastItem
                        });
                    }
                    else
                    {
                        item = this.Character.Inventory.LastItem;
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                        {
                            InventorySlot = item.Slot,
                            Stack = item.Stack
                        });
                    }
                }
            }
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
        }

        /// <summary>
        /// The SendItemAdd.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        /// <param name="container">The container<see cref="ContainerType"/>.</param>
        /// <param name="result">The result<see cref="InventoryAddResult"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <param name="sendMessage">The sendMessage<see cref="bool"/>.</param>
        public void SendItemAdd(SagaDB.Item.Item item, ContainerType container, InventoryAddResult result, int count, bool sendMessage)
        {
            switch (result)
            {
                case InventoryAddResult.NEW_INDEX:
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_ADD()
                    {
                        Container = container,
                        Item = item,
                        InventorySlot = item.Slot
                    });
                    break;
                case InventoryAddResult.STACKED:
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                    {
                        InventorySlot = item.Slot,
                        Stack = item.Stack
                    });
                    break;
                case InventoryAddResult.MIXED:
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                    {
                        InventorySlot = item.Slot,
                        Stack = item.Stack
                    });
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_ADD()
                    {
                        Container = container,
                        Item = this.Character.Inventory.LastItem,
                        InventorySlot = this.Character.Inventory.LastItem.Slot
                    });
                    break;
            }
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
            if (!sendMessage)
                return;
            if (item.Identified)
                this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_ADDED, (object)item.BaseData.name, (object)count));
            else
                this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_ADDED, (object)Event.GetItemNameByType(item.BaseData.itemType), (object)count));
        }

        /// <summary>
        /// The SendItems.
        /// </summary>
        public void SendItems()
        {
            foreach (string name in Enum.GetNames(typeof(ContainerType)))
            {
                ContainerType container = (ContainerType)Enum.Parse(typeof(ContainerType), name);
                foreach (SagaDB.Item.Item obj in this.Character.Inventory.GetContainer(container))
                {
                    if (obj.Stack != (ushort)0)
                    {
                        if (obj.Refine == (ushort)0)
                            obj.Clear();
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_INFO()
                        {
                            Item = obj,
                            InventorySlot = obj.Slot,
                            Container = container
                        });
                    }
                }
            }
        }

        /// <summary>
        /// The SendItemInfo.
        /// </summary>
        /// <param name="slot">The slot<see cref="uint"/>.</param>
        public void SendItemInfo(uint slot)
        {
            SagaDB.Item.Item obj = this.Character.Inventory.GetItem(slot);
            if (obj == null)
                return;
            this.netIO.SendPacket((Packet)new SSMG_ITEM_INFO()
            {
                Item = obj,
                InventorySlot = obj.Slot,
                Container = this.Character.Inventory.GetContainerType(slot)
            });
        }

        /// <summary>
        /// The SendItemInfo.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        public void SendItemInfo(SagaDB.Item.Item item)
        {
            if (item == null)
                return;
            this.netIO.SendPacket((Packet)new SSMG_ITEM_INFO()
            {
                Item = item,
                InventorySlot = item.Slot,
                Container = this.Character.Inventory.GetContainerType(item.Slot)
            });
        }

        /// <summary>
        /// The SendItemIdentify.
        /// </summary>
        /// <param name="slot">The slot<see cref="uint"/>.</param>
        public void SendItemIdentify(uint slot)
        {
            SagaDB.Item.Item obj = this.Character.Inventory.GetItem(slot);
            if (obj == null)
                return;
            this.netIO.SendPacket((Packet)new SSMG_ITEM_IDENTIFY()
            {
                InventorySlot = obj.Slot,
                Identify = obj.Identified,
                Lock = obj.Locked
            });
        }

        /// <summary>
        /// The SendEquip.
        /// </summary>
        public void SendEquip()
        {
            this.netIO.SendPacket((Packet)new SSMG_ITEM_ACTOR_EQUIP_UPDATE()
            {
                Player = this.Character
            });
        }

        /// <summary>
        /// The AddItem.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        /// <param name="sendMessage">The sendMessage<see cref="bool"/>.</param>
        public void AddItem(SagaDB.Item.Item item, bool sendMessage)
        {
            ushort stack = item.Stack;
            InventoryAddResult result = this.Character.Inventory.AddItem(ContainerType.BODY, item);
            this.SendItemAdd(item, ContainerType.BODY, result, (int)stack, sendMessage);
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
        }

        /// <summary>
        /// The CountItem.
        /// </summary>
        /// <param name="itemID">The itemID<see cref="uint"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int CountItem(uint itemID)
        {
            SagaDB.Item.Item obj = this.chara.Inventory.GetItem(itemID, Inventory.SearchType.ITEM_ID);
            if (obj != null)
                return (int)obj.Stack;
            return 0;
        }

        /// <summary>
        /// The DeleteItemID.
        /// </summary>
        /// <param name="itemID">The itemID<see cref="uint"/>.</param>
        /// <param name="count">The count<see cref="ushort"/>.</param>
        /// <param name="message">The message<see cref="bool"/>.</param>
        /// <returns>The <see cref="SagaDB.Item.Item"/>.</returns>
        public SagaDB.Item.Item DeleteItemID(uint itemID, ushort count, bool message)
        {
            SagaDB.Item.Item obj = this.Character.Inventory.GetItem(itemID, Inventory.SearchType.ITEM_ID);
            if (obj == null)
                return (SagaDB.Item.Item)null;
            uint slot = obj.Slot;
            InventoryDeleteResult inventoryDeleteResult = this.Character.Inventory.DeleteItem(obj.Slot, (int)count);
            if (obj.IsEquipt)
            {
                this.SendEquip();
                Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                this.SendPlayerInfo();
            }
            switch (inventoryDeleteResult)
            {
                case InventoryDeleteResult.ALL_DELETED:
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                    {
                        InventorySlot = slot
                    });
                    if (obj.IsEquipt)
                    {
                        this.SendAttackType();
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                        {
                            InventorySlot = uint.MaxValue,
                            Target = ContainerType.NONE,
                            Result = 1,
                            Range = this.Character.Range
                        });
                        break;
                    }
                    break;
                case InventoryDeleteResult.STACK_UPDATED:
                    SSMG_ITEM_COUNT_UPDATE ssmgItemCountUpdate = new SSMG_ITEM_COUNT_UPDATE();
                    obj = this.Character.Inventory.GetItem(slot);
                    ssmgItemCountUpdate.InventorySlot = slot;
                    ssmgItemCountUpdate.Stack = obj.Stack;
                    this.netIO.SendPacket((Packet)ssmgItemCountUpdate);
                    break;
            }
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
            if (message)
                this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_DELETED, (object)obj.BaseData.name, (object)count));
            return obj;
        }

        /// <summary>
        /// The DeleteItem.
        /// </summary>
        /// <param name="slot">The slot<see cref="uint"/>.</param>
        /// <param name="count">The count<see cref="ushort"/>.</param>
        /// <param name="message">The message<see cref="bool"/>.</param>
        public void DeleteItem(uint slot, ushort count, bool message)
        {
            SagaDB.Item.Item obj1 = this.Character.Inventory.GetItem(slot);
            ContainerType containerType1 = this.Character.Inventory.GetContainerType(obj1.Slot);
            bool flag = false;
            if (containerType1 >= ContainerType.HEAD && containerType1 <= ContainerType.PET)
                flag = true;
            InventoryDeleteResult inventoryDeleteResult = this.Character.Inventory.DeleteItem(slot, (int)count);
            if (flag)
            {
                this.SendEquip();
                Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
                this.SendPlayerInfo();
            }
            switch (inventoryDeleteResult)
            {
                case InventoryDeleteResult.ALL_DELETED:
                    this.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                    {
                        InventorySlot = slot
                    });
                    int containerType2 = (int)this.Character.Inventory.GetContainerType(slot);
                    if (flag)
                    {
                        this.SendAttackType();
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                        {
                            InventorySlot = uint.MaxValue,
                            Target = ContainerType.NONE,
                            Result = 1,
                            Range = this.Character.Range
                        });
                        if (obj1.BaseData.itemType == ItemType.ARROW || obj1.BaseData.itemType == ItemType.BULLET)
                        {
                            SagaDB.Item.Item obj2 = this.Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Clone();
                            obj2.Stack = (ushort)0;
                            int num = (int)this.Character.Inventory.AddItem(ContainerType.LEFT_HAND, obj2);
                        }
                        break;
                    }
                    break;
                case InventoryDeleteResult.STACK_UPDATED:
                    SSMG_ITEM_COUNT_UPDATE ssmgItemCountUpdate = new SSMG_ITEM_COUNT_UPDATE();
                    obj1 = this.Character.Inventory.GetItem(slot);
                    ssmgItemCountUpdate.InventorySlot = slot;
                    ssmgItemCountUpdate.Stack = obj1.Stack;
                    this.netIO.SendPacket((Packet)ssmgItemCountUpdate);
                    break;
            }
            this.Character.Inventory.CalcPayloadVolume();
            this.SendCapacity();
            if (!message)
                return;
            this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_DELETED, (object)obj1.BaseData.name, (object)count));
        }

        /// <summary>
        /// The SendPet.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        public void SendPet(SagaDB.Item.Item item)
        {
            if (item.BaseData.itemType == ItemType.BACK_DEMON || item.BaseData.itemType == ItemType.RIDE_PET)
                return;
            ActorPet mob = new ActorPet(item.BaseData.petID, item);
            this.Character.Pet = mob;
            mob.MapID = this.Character.MapID;
            mob.X = this.Character.X;
            mob.Y = this.Character.Y;
            mob.Owner = this.Character;
            PetEventHandler petEventHandler = new PetEventHandler(mob);
            mob.e = (ActorEventHandler)petEventHandler;
            petEventHandler.AI.Mode = !Factory<MobAIFactory, AIMode>.Instance.Items.ContainsKey(item.BaseData.petID) ? new AIMode(0) : Factory<MobAIFactory, AIMode>.Instance.Items[item.BaseData.petID];
            petEventHandler.AI.Start();
            this.map.RegisterActor((SagaDB.Actor.Actor)mob);
            mob.invisble = false;
            this.map.OnActorVisibilityChange((SagaDB.Actor.Actor)mob);
            this.map.SendVisibleActorsToActor((SagaDB.Actor.Actor)mob);
        }

        /// <summary>
        /// The DeletePet.
        /// </summary>
        public void DeletePet()
        {
            if (this.Character.Pet == null || this.Character.Pet.Ride)
                return;
            PetEventHandler e = (PetEventHandler)this.Character.Pet.e;
            e.AI.Pause();
            e.AI.Activated = false;
            Singleton<MapManager>.Instance.GetMap(this.Character.Pet.MapID).DeleteActor((SagaDB.Actor.Actor)this.Character.Pet);
            this.Character.Pet = (ActorPet)null;
        }

        /// <summary>
        /// The OnBBSRequestPage.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_COMMUNITY_BBS_REQUEST_PAGE"/>.</param>
        public void OnBBSRequestPage(CSMG_COMMUNITY_BBS_REQUEST_PAGE p)
        {
            SSMG_COMMUNITY_BBS_PAGE_INFO communityBbsPageInfo = new SSMG_COMMUNITY_BBS_PAGE_INFO();
            this.bbsCurrentPage = p.Page;
            communityBbsPageInfo.Posts = MapServer.charDB.GetBBSPage(this.bbsID, this.bbsCurrentPage);
            this.netIO.SendPacket((Packet)communityBbsPageInfo);
        }

        /// <summary>
        /// The OnBBSPost.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_COMMUNITY_BBS_POST"/>.</param>
        public void OnBBSPost(CSMG_COMMUNITY_BBS_POST p)
        {
            SSMG_COMMUNITY_BBS_POST_RESULT communityBbsPostResult = new SSMG_COMMUNITY_BBS_POST_RESULT();
            if ((long)this.Character.Gold >= (long)this.bbsCost)
            {
                if (MapServer.charDB.BBSNewPost(this.Character, this.bbsID, p.Title, p.Content))
                {
                    communityBbsPostResult.Result = SSMG_COMMUNITY_BBS_POST_RESULT.Results.SUCCEED;
                    this.Character.Gold -= (int)this.bbsCost;
                }
                else
                    communityBbsPostResult.Result = SSMG_COMMUNITY_BBS_POST_RESULT.Results.FAILED;
            }
            else
                communityBbsPostResult.Result = SSMG_COMMUNITY_BBS_POST_RESULT.Results.NOT_ENOUGH_MONEY;
            this.netIO.SendPacket((Packet)communityBbsPostResult);
            this.netIO.SendPacket((Packet)new SSMG_COMMUNITY_BBS_PAGE_INFO()
            {
                Posts = MapServer.charDB.GetBBSPage(this.bbsID, this.bbsCurrentPage)
            });
        }

        /// <summary>
        /// The OnBBSClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_COMMUNITY_BBS_CLOSE"/>.</param>
        public void OnBBSClose(CSMG_COMMUNITY_BBS_CLOSE p)
        {
            this.bbsClose = true;
        }

        /// <summary>
        /// The OnRecruit.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_COMMUNITY_RECRUIT"/>.</param>
        public void OnRecruit(CSMG_COMMUNITY_RECRUIT p)
        {
            int page = p.Page;
            int maxPage;
            List<Recruitment> recruitments = Singleton<RecruitmentManager>.Instance.GetRecruitments(p.Type, page, out maxPage);
            this.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT()
            {
                Type = p.Type,
                Page = page,
                MaxPage = maxPage,
                Entries = recruitments
            });
        }

        /// <summary>
        /// The OnRecruitRequestAns.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_COMMUNITY_RECRUIT_REQUEST_ANS"/>.</param>
        public void OnRecruitRequestAns(CSMG_COMMUNITY_RECRUIT_REQUEST_ANS p)
        {
            MapClient client = MapClientManager.Instance.FindClient(p.CharID);
            if (client == null || this.partyPartner == null || (int)client.chara.CharID != (int)this.partyPartner.CharID)
                return;
            if (p.Accept)
            {
                client.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT_JOIN_RES()
                {
                    Result = JoinRes.OK,
                    CharID = this.chara.CharID
                });
                if (this.Character.Party != null)
                {
                    if (this.Character.Party.MemberCount >= 8)
                        return;
                    Singleton<PartyManager>.Instance.AddMember(this.Character.Party, this.partyPartner);
                }
                else
                    Singleton<PartyManager>.Instance.AddMember(Singleton<PartyManager>.Instance.CreateParty(this.chara), this.partyPartner);
            }
            else
                client.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT_JOIN_RES()
                {
                    Result = JoinRes.REJECTED,
                    CharID = this.chara.CharID
                });
        }

        /// <summary>
        /// The OnRecruitJoin.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_COMMUNITY_RECRUIT_JOIN"/>.</param>
        public void OnRecruitJoin(CSMG_COMMUNITY_RECRUIT_JOIN p)
        {
            MapClient client = MapClientManager.Instance.FindClient(p.CharID);
            if (this.Character.Party != null)
                this.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT_JOIN_RES()
                {
                    Result = JoinRes.ALREADY_IN_PARTY,
                    CharID = p.CharID
                });
            else if (client != null)
            {
                if ((int)client.Character.CharID == (int)this.Character.CharID)
                    this.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT_JOIN_RES()
                    {
                        Result = JoinRes.SELF,
                        CharID = p.CharID
                    });
                else if (client.Character.Party != null && client.Character.Party.MemberCount == 8)
                {
                    this.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT_JOIN_RES()
                    {
                        Result = JoinRes.PARTY_FULL,
                        CharID = p.CharID
                    });
                }
                else
                {
                    this.partyPartner = client.Character;
                    client.partyPartner = this.chara;
                    client.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT_REQUEST()
                    {
                        CharID = this.chara.CharID,
                        CharName = this.chara.Name
                    });
                }
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT_JOIN_RES()
                {
                    Result = JoinRes.TARGET_OFFLINE,
                    CharID = p.CharID
                });
        }

        /// <summary>
        /// The OnRecruitCreate.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_COMMUNITY_RECRUIT_CREATE"/>.</param>
        public void OnRecruitCreate(CSMG_COMMUNITY_RECRUIT_CREATE p)
        {
            Singleton<RecruitmentManager>.Instance.CreateRecruiment(new Recruitment()
            {
                Creator = this.Character,
                Type = p.Type,
                Title = p.Title,
                Content = p.Content
            });
            this.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT_CREATE());
        }

        /// <summary>
        /// The OnRecruitDelete.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_COMMUNITY_RECRUIT_DELETE"/>.</param>
        public void OnRecruitDelete(CSMG_COMMUNITY_RECRUIT_DELETE p)
        {
            Singleton<RecruitmentManager>.Instance.DeleteRecruitment(this.Character);
            this.netIO.SendPacket((Packet)new SSMG_COMMUNITY_RECRUIT_DELETE());
        }

        /// <summary>
        /// The SendEffect.
        /// </summary>
        /// <param name="effect">The effect<see cref="uint"/>.</param>
        private void SendEffect(uint effect)
        {
            this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
            {
                actorID = this.Character.ActorID,
                effectID = effect
            }, (SagaDB.Actor.Actor)this.chara, true);
        }

        /// <summary>
        /// The ResetStatusPoint.
        /// </summary>
        public void ResetStatusPoint()
        {
            StartupSetting startupSetting = Singleton<Configuration>.Instance.StartupSetting[this.Character.Race];
            this.Character.StatsPoint += Singleton<StatusFactory>.Instance.GetTotalBonusPointForStats(startupSetting.Str, this.Character.Str);
            this.Character.StatsPoint += Singleton<StatusFactory>.Instance.GetTotalBonusPointForStats(startupSetting.Dex, this.Character.Dex);
            this.Character.StatsPoint += Singleton<StatusFactory>.Instance.GetTotalBonusPointForStats(startupSetting.Int, this.Character.Int);
            this.Character.StatsPoint += Singleton<StatusFactory>.Instance.GetTotalBonusPointForStats(startupSetting.Vit, this.Character.Vit);
            this.Character.StatsPoint += Singleton<StatusFactory>.Instance.GetTotalBonusPointForStats(startupSetting.Agi, this.Character.Agi);
            this.Character.StatsPoint += Singleton<StatusFactory>.Instance.GetTotalBonusPointForStats(startupSetting.Mag, this.Character.Mag);
            this.Character.Str = startupSetting.Str;
            this.Character.Dex = startupSetting.Dex;
            this.Character.Int = startupSetting.Int;
            this.Character.Vit = startupSetting.Vit;
            this.Character.Agi = startupSetting.Agi;
            this.Character.Mag = startupSetting.Mag;
            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
            this.SendPlayerInfo();
        }

        /// <summary>
        /// The SendRange.
        /// </summary>
        public void SendRange()
        {
            this.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
            {
                InventorySlot = uint.MaxValue,
                Target = ContainerType.NONE,
                Result = 1,
                Range = this.chara.Range
            });
        }

        /// <summary>
        /// The SendActorID.
        /// </summary>
        public void SendActorID()
        {
            this.netIO.SendPacket((Packet)new SSMG_ACTOR_SPEED()
            {
                ActorID = this.Character.ActorID,
                Speed = (ushort)10
            });
        }

        /// <summary>
        /// The SendStamp.
        /// </summary>
        public void SendStamp()
        {
            this.netIO.SendPacket((Packet)new SSMG_STAMP_INFO()
            {
                Stamp = this.Character.Stamp
            });
        }

        /// <summary>
        /// The SendActorMode.
        /// </summary>
        public void SendActorMode()
        {
            this.Character.e.OnPlayerMode((SagaDB.Actor.Actor)this.Character);
        }

        /// <summary>
        /// The SendCharOption.
        /// </summary>
        public void SendCharOption()
        {
            this.netIO.SendPacket((Packet)new SSMG_ACTOR_OPTION()
            {
                Option = SSMG_ACTOR_OPTION.Options.NONE
            });
        }

        /// <summary>
        /// The SendCharInfo.
        /// </summary>
        public void SendCharInfo()
        {
            if (!this.Character.Online)
                return;
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
            this.SendAttackType();
            this.netIO.SendPacket((Packet)new SSMG_PLAYER_INFO()
            {
                Player = this.Character
            });
            this.SendPlayerInfo();
        }

        /// <summary>
        /// The SendPlayerInfo.
        /// </summary>
        public void SendPlayerInfo()
        {
            if (!this.Character.Online)
                return;
            this.SendGoldUpdate();
            this.SendActorHPMPSP((SagaDB.Actor.Actor)this.Character);
            this.SendStatus();
            this.SendRange();
            this.SendStatusExtend();
            this.SendCapacity();
            this.SendMaxCapacity();
            this.SendPlayerJob();
            this.SendSkillList();
            this.SendPlayerLevel();
            this.SendEXP();
            this.SendActorMode();
            this.SendCL();
        }

        /// <summary>
        /// The SendAttackType.
        /// </summary>
        public void SendAttackType()
        {
            if (!this.Character.Online)
                return;
            Dictionary<EnumEquipSlot, SagaDB.Item.Item> dictionary = this.chara.Form != DEM_FORM.NORMAL_FORM ? this.chara.Inventory.Parts : this.chara.Inventory.Equipments;
            if (dictionary.ContainsKey(EnumEquipSlot.RIGHT_HAND))
            {
                SagaDB.Item.Item obj = dictionary[EnumEquipSlot.RIGHT_HAND];
                this.Character.Status.attackType = obj.AttackType;
                switch (obj.AttackType)
                {
                    case ATTACK_TYPE.BLOW:
                        this.SendSystemMessage(SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTUREBLOW_TEXT);
                        break;
                    case ATTACK_TYPE.SLASH:
                        this.SendSystemMessage(SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTURESLASH_TEXT);
                        break;
                    case ATTACK_TYPE.STAB:
                        this.SendSystemMessage(SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTURESTAB_TEXT);
                        break;
                    default:
                        this.SendSystemMessage(SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTUREERROR_TEXT);
                        break;
                }
            }
            else
            {
                this.Character.Status.attackType = ATTACK_TYPE.BLOW;
                this.SendSystemMessage(SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTUREBLOW_TEXT);
            }
            this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.ATTACK_TYPE_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The SendStatus.
        /// </summary>
        public void SendStatus()
        {
            if (!this.Character.Online)
                return;
            SSMG_PLAYER_STATUS ssmgPlayerStatus = new SSMG_PLAYER_STATUS();
            if (this.chara.Form == DEM_FORM.MACHINA_FORM || this.chara.Race != PC_RACE.DEM)
            {
                ssmgPlayerStatus.AgiBase = (ushort)((uint)this.Character.Agi + (uint)this.chara.Status.m_agi_chip);
                ssmgPlayerStatus.AgiRevide = (short)((int)this.Character.Status.agi_rev + (int)this.Character.Status.agi_item + (int)this.Character.Status.agi_mario + (int)this.Character.Status.agi_skill);
                ssmgPlayerStatus.AgiBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Agi);
                ssmgPlayerStatus.DexBase = (ushort)((uint)this.Character.Dex + (uint)this.chara.Status.m_dex_chip);
                ssmgPlayerStatus.DexRevide = (short)((int)this.Character.Status.dex_rev + (int)this.Character.Status.dex_item + (int)this.Character.Status.dex_mario + (int)this.Character.Status.dex_skill);
                ssmgPlayerStatus.DexBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Dex);
                ssmgPlayerStatus.IntBase = (ushort)((uint)this.Character.Int + (uint)this.chara.Status.m_int_chip);
                ssmgPlayerStatus.IntRevide = (short)((int)this.Character.Status.int_rev + (int)this.Character.Status.int_item + (int)this.Character.Status.int_mario + (int)this.Character.Status.int_skill);
                ssmgPlayerStatus.IntBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Int);
                ssmgPlayerStatus.VitBase = (ushort)((uint)this.Character.Vit + (uint)this.chara.Status.m_vit_chip);
                ssmgPlayerStatus.VitRevide = (short)((int)this.Character.Status.vit_rev + (int)this.Character.Status.vit_item + (int)this.Character.Status.vit_mario + (int)this.Character.Status.vit_skill);
                ssmgPlayerStatus.VitBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Vit);
                ssmgPlayerStatus.StrBase = (ushort)((uint)this.Character.Str + (uint)this.chara.Status.m_str_chip);
                ssmgPlayerStatus.StrRevide = (short)((int)this.Character.Status.str_rev + (int)this.Character.Status.str_item + (int)this.Character.Status.str_mario + (int)this.Character.Status.str_skill);
                ssmgPlayerStatus.StrBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Str);
                ssmgPlayerStatus.MagBase = (ushort)((uint)this.Character.Mag + (uint)this.chara.Status.m_mag_chip);
                ssmgPlayerStatus.MagRevide = (short)((int)this.Character.Status.mag_rev + (int)this.Character.Status.mag_item + (int)this.Character.Status.mag_mario + (int)this.Character.Status.mag_skill);
                ssmgPlayerStatus.MagBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Mag);
                this.netIO.SendPacket((Packet)ssmgPlayerStatus);
            }
            else
            {
                ssmgPlayerStatus.AgiBase = (ushort)((uint)this.Character.Agi + (uint)this.chara.Status.m_agi_chip);
                ssmgPlayerStatus.AgiRevide = (short)((int)this.Character.Status.agi_rev - (int)this.chara.Status.m_agi_chip + (int)this.Character.Status.agi_item + (int)this.Character.Status.agi_mario + (int)this.Character.Status.agi_skill);
                ssmgPlayerStatus.AgiBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Agi);
                ssmgPlayerStatus.DexBase = (ushort)((uint)this.Character.Dex + (uint)this.chara.Status.m_dex_chip);
                ssmgPlayerStatus.DexRevide = (short)((int)this.Character.Status.dex_rev - (int)this.chara.Status.m_dex_chip + (int)this.Character.Status.dex_item + (int)this.Character.Status.dex_mario + (int)this.Character.Status.dex_skill);
                ssmgPlayerStatus.DexBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Dex);
                ssmgPlayerStatus.IntBase = (ushort)((uint)this.Character.Int + (uint)this.chara.Status.m_int_chip);
                ssmgPlayerStatus.IntRevide = (short)((int)this.Character.Status.int_rev - (int)this.chara.Status.m_int_chip + (int)this.Character.Status.int_item + (int)this.Character.Status.int_mario + (int)this.Character.Status.int_skill);
                ssmgPlayerStatus.IntBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Int);
                ssmgPlayerStatus.VitBase = (ushort)((uint)this.Character.Vit + (uint)this.chara.Status.m_vit_chip);
                ssmgPlayerStatus.VitRevide = (short)((int)this.Character.Status.vit_rev - (int)this.chara.Status.m_vit_chip + (int)this.Character.Status.vit_item + (int)this.Character.Status.vit_mario + (int)this.Character.Status.vit_skill);
                ssmgPlayerStatus.VitBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Vit);
                ssmgPlayerStatus.StrBase = (ushort)((uint)this.Character.Str + (uint)this.chara.Status.m_str_chip);
                ssmgPlayerStatus.StrRevide = (short)((int)this.Character.Status.str_rev - (int)this.chara.Status.m_str_chip + (int)this.Character.Status.str_item + (int)this.Character.Status.str_mario + (int)this.Character.Status.str_skill);
                ssmgPlayerStatus.StrBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Str);
                ssmgPlayerStatus.MagBase = (ushort)((uint)this.Character.Mag + (uint)this.chara.Status.m_mag_chip);
                ssmgPlayerStatus.MagRevide = (short)((int)this.Character.Status.mag_rev - (int)this.chara.Status.m_mag_chip + (int)this.Character.Status.mag_item + (int)this.Character.Status.mag_mario + (int)this.Character.Status.mag_skill);
                ssmgPlayerStatus.MagBonus = Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Mag);
                this.netIO.SendPacket((Packet)ssmgPlayerStatus);
            }
        }

        /// <summary>
        /// The SendStatusExtend.
        /// </summary>
        public void SendStatusExtend()
        {
            if (!this.Character.Online)
                return;
            SSMG_PLAYER_STATUS_EXTEND playerStatusExtend = new SSMG_PLAYER_STATUS_EXTEND()
            {
                ASPD = (short)((int)this.Character.Status.aspd + (int)this.Character.Status.aspd_skill),
                ATK1Max = this.Character.Status.max_atk1,
                ATK1Min = this.Character.Status.min_atk1
            };
            playerStatusExtend.ATK1Max = this.Character.Status.max_atk2;
            playerStatusExtend.ATK1Min = this.Character.Status.min_atk2;
            playerStatusExtend.ATK1Max = this.Character.Status.max_atk3;
            playerStatusExtend.ATK1Min = this.Character.Status.min_atk3;
            playerStatusExtend.ATK2Max = this.Character.Status.max_atk2;
            playerStatusExtend.ATK2Min = this.Character.Status.min_atk2;
            playerStatusExtend.ATK3Max = this.Character.Status.max_atk3;
            playerStatusExtend.ATK3Min = this.Character.Status.min_atk3;
            playerStatusExtend.AvoidCritical = this.Character.Status.avoid_critical;
            playerStatusExtend.AvoidMagic = this.Character.Status.avoid_magic;
            playerStatusExtend.AvoidMelee = this.Character.Status.avoid_melee;
            playerStatusExtend.AvoidRanged = this.Character.Status.avoid_ranged;
            playerStatusExtend.CSPD = (short)((int)this.Character.Status.cspd + (int)this.Character.Status.cspd_skill);
            playerStatusExtend.DefAddition = (ushort)this.Character.Status.def_add;
            playerStatusExtend.DefBase = this.Character.Status.def;
            playerStatusExtend.HitCritical = this.Character.Status.hit_critical;
            playerStatusExtend.HitMagic = this.Character.Status.hit_magic;
            playerStatusExtend.HitMelee = this.Character.Status.hit_melee;
            playerStatusExtend.HitRanged = this.Character.Status.hit_ranged;
            playerStatusExtend.MATKMax = this.Character.Status.max_matk;
            playerStatusExtend.MATKMin = this.Character.Status.min_matk;
            playerStatusExtend.MDefAddition = (ushort)this.Character.Status.mdef_add;
            playerStatusExtend.MDefBase = this.Character.Status.mdef;
            playerStatusExtend.Speed = this.Character.Speed;
            this.netIO.SendPacket((Packet)playerStatusExtend);
        }

        /// <summary>
        /// The SendCapacity.
        /// </summary>
        public void SendCapacity()
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_PLAYER_CAPACITY()
            {
                CapacityBack = this.Character.Inventory.Volume[ContainerType.BACK_BAG],
                CapacityBody = this.Character.Inventory.Volume[ContainerType.BODY],
                CapacityLeft = this.Character.Inventory.Volume[ContainerType.LEFT_BAG],
                CapacityRight = this.Character.Inventory.Volume[ContainerType.RIGHT_BAG],
                PayloadBack = this.Character.Inventory.Payload[ContainerType.BACK_BAG],
                PayloadBody = this.Character.Inventory.Payload[ContainerType.BODY],
                PayloadLeft = this.Character.Inventory.Payload[ContainerType.LEFT_BAG],
                PayloadRight = this.Character.Inventory.Payload[ContainerType.RIGHT_BAG]
            });
        }

        /// <summary>
        /// The SendMaxCapacity.
        /// </summary>
        public void SendMaxCapacity()
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_PLAYER_MAX_CAPACITY()
            {
                CapacityBack = this.Character.Inventory.MaxVolume[ContainerType.BACK_BAG],
                CapacityBody = this.Character.Inventory.MaxVolume[ContainerType.BODY],
                CapacityLeft = this.Character.Inventory.MaxVolume[ContainerType.LEFT_BAG],
                CapacityRight = this.Character.Inventory.MaxVolume[ContainerType.RIGHT_BAG],
                PayloadBack = this.Character.Inventory.MaxPayload[ContainerType.BACK_BAG],
                PayloadBody = this.Character.Inventory.MaxPayload[ContainerType.BODY],
                PayloadLeft = this.Character.Inventory.MaxPayload[ContainerType.LEFT_BAG],
                PayloadRight = this.Character.Inventory.MaxPayload[ContainerType.RIGHT_BAG]
            });
        }

        /// <summary>
        /// The SendChangeMap.
        /// </summary>
        public void SendChangeMap()
        {
            if (!this.Character.Online)
                return;
            SSMG_PLAYER_CHANGE_MAP ssmgPlayerChangeMap = new SSMG_PLAYER_CHANGE_MAP();
            ssmgPlayerChangeMap.MapID = this.Character.MapID;
            ssmgPlayerChangeMap.X = SagaLib.Global.PosX16to8(this.Character.X, this.map.Width);
            ssmgPlayerChangeMap.Y = SagaLib.Global.PosY16to8(this.Character.Y, this.map.Height);
            ssmgPlayerChangeMap.Dir = (byte)((uint)this.Character.Dir / 45U);
            if (this.map.IsDungeon)
            {
                ssmgPlayerChangeMap.DungeonDir = this.map.DungeonMap.Dir;
                ssmgPlayerChangeMap.DungeonX = this.map.DungeonMap.X;
                ssmgPlayerChangeMap.DungeonY = this.map.DungeonMap.Y;
            }
            if (this.fgTakeOff)
            {
                ssmgPlayerChangeMap.FGTakeOff = this.fgTakeOff;
                this.fgTakeOff = false;
            }
            this.netIO.SendPacket((Packet)ssmgPlayerChangeMap);
        }

        /// <summary>
        /// The SendGotoFG.
        /// </summary>
        public void SendGotoFG()
        {
            if (!this.Character.Online)
                return;
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(this.Character.MapID);
            if (!map.IsMapInstance)
                Logger.ShowDebug(string.Format("MapID:{0} isn't a valid flying garden!"), Logger.defaultlogger);
            ActorPC creator = map.Creator;
            this.netIO.SendPacket((Packet)new SSMG_PLAYER_GOTO_FG()
            {
                MapID = this.Character.MapID,
                X = SagaLib.Global.PosX16to8(this.Character.X, this.map.Width),
                Y = SagaLib.Global.PosY16to8(this.Character.Y, this.map.Height),
                Dir = (byte)((uint)this.Character.Dir / 45U),
                Equiptments = creator.FGarden.FGardenEquipments
            });
        }

        /// <summary>
        /// The SendDungeonEvent.
        /// </summary>
        public void SendDungeonEvent()
        {
            if (!this.Character.Online || (!this.map.IsMapInstance || !this.map.IsDungeon))
                return;
            foreach (GateType key in this.map.DungeonMap.Gates.Keys)
            {
                if (this.map.DungeonMap.Gates[key].NPCID != 0U)
                    this.netIO.SendPacket((Packet)new SSMG_NPC_SHOW()
                    {
                        NPCID = this.map.DungeonMap.Gates[key].NPCID
                    });
                if (this.map.DungeonMap.Gates[key].ConnectedMap != null)
                {
                    if (key != GateType.Central && key != GateType.Exit)
                    {
                        SSMG_NPC_SET_EVENT_AREA ssmgNpcSetEventArea = new SSMG_NPC_SET_EVENT_AREA();
                        ssmgNpcSetEventArea.StartX = (uint)this.map.DungeonMap.Gates[key].X;
                        ssmgNpcSetEventArea.EndX = (uint)this.map.DungeonMap.Gates[key].X;
                        ssmgNpcSetEventArea.StartY = (uint)this.map.DungeonMap.Gates[key].Y;
                        ssmgNpcSetEventArea.EndY = (uint)this.map.DungeonMap.Gates[key].Y;
                        switch (key)
                        {
                            case GateType.East:
                                ssmgNpcSetEventArea.EventID = 12001502U;
                                break;
                            case GateType.West:
                                ssmgNpcSetEventArea.EventID = 12001504U;
                                break;
                            case GateType.South:
                                ssmgNpcSetEventArea.EventID = 12001503U;
                                break;
                            case GateType.North:
                                ssmgNpcSetEventArea.EventID = 12001501U;
                                break;
                        }
                        switch (this.map.DungeonMap.Gates[key].Direction)
                        {
                            case Direction.In:
                                ssmgNpcSetEventArea.EffectID = 9002U;
                                break;
                            case Direction.Out:
                                ssmgNpcSetEventArea.EffectID = 9005U;
                                break;
                        }
                        this.netIO.SendPacket((Packet)ssmgNpcSetEventArea);
                    }
                    else
                        this.netIO.SendPacket((Packet)new SSMG_NPC_SET_EVENT_AREA()
                        {
                            StartX = (uint)this.map.DungeonMap.Gates[key].X,
                            EndX = (uint)this.map.DungeonMap.Gates[key].X,
                            StartY = (uint)this.map.DungeonMap.Gates[key].Y,
                            EndY = (uint)this.map.DungeonMap.Gates[key].Y,
                            EventID = 12001505U,
                            EffectID = 9005U
                        });
                    if (this.map.DungeonMap.Gates[key].NPCID != 0U)
                        this.netIO.SendPacket((Packet)new SSMG_CHAT_MOTION()
                        {
                            ActorID = this.map.DungeonMap.Gates[key].NPCID,
                            Motion = (MotionType)621
                        });
                }
                else if (key == GateType.Entrance)
                    this.netIO.SendPacket((Packet)new SSMG_NPC_SET_EVENT_AREA()
                    {
                        StartX = (uint)this.map.DungeonMap.Gates[key].X,
                        EndX = (uint)this.map.DungeonMap.Gates[key].X,
                        StartY = (uint)this.map.DungeonMap.Gates[key].Y,
                        EndY = (uint)this.map.DungeonMap.Gates[key].Y,
                        EventID = 12001505U,
                        EffectID = 9003U
                    });
            }
        }

        /// <summary>
        /// The SendFGEvent.
        /// </summary>
        public void SendFGEvent()
        {
            if (!this.Character.Online || !Singleton<MapManager>.Instance.GetMap(this.Character.MapID).IsMapInstance || (this.map.ID / 10U != 7000000U || this.map.Creator.FGarden.FGardenEquipments[FGardenSlot.GARDEN_MODELHOUSE] == 0U))
                return;
            this.netIO.SendPacket((Packet)new SSMG_NPC_SET_EVENT_AREA()
            {
                EventID = 10000315U,
                StartX = 6U,
                StartY = 7U,
                EndX = 6U,
                EndY = 7U
            });
        }

        /// <summary>
        /// The SendGoldUpdate.
        /// </summary>
        public void SendGoldUpdate()
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_PLAYER_GOLD_UPDATE()
            {
                Gold = (uint)this.Character.Gold
            });
        }

        /// <summary>
        /// The SendActorHPMPSP.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void SendActorHPMPSP(SagaDB.Actor.Actor actor)
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_PLAYER_MAX_HPMPSP()
            {
                ActorID = actor.ActorID,
                MaxHP = actor.MaxHP,
                MaxMP = actor.MaxMP,
                MaxSP = actor.MaxSP,
                MaxEP = actor.MaxEP
            });
            this.netIO.SendPacket((Packet)new SSMG_PLAYER_HPMPSP()
            {
                ActorID = actor.ActorID,
                HP = actor.HP,
                MP = actor.MP,
                SP = actor.SP,
                EP = actor.EP
            });
            if (actor == this.Character && (DateTime.Now - this.hpmpspStamp).TotalSeconds >= 2.0)
            {
                if (this.Character.Party != null)
                    Singleton<PartyManager>.Instance.UpdateMemberHPMPSP(this.Character.Party, this.Character);
                this.hpmpspStamp = DateTime.Now;
            }
        }

        /// <summary>
        /// The SendCharXY.
        /// </summary>
        public void SendCharXY()
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_ACTOR_MOVE()
            {
                ActorID = this.Character.ActorID,
                Dir = this.Character.Dir,
                X = this.Character.X,
                Y = this.Character.Y,
                MoveType = MoveType.WARP
            });
        }

        /// <summary>
        /// The SendPlayerLevel.
        /// </summary>
        public void SendPlayerLevel()
        {
            if (!this.Character.Online)
                return;
            SSMG_PLAYER_LEVEL ssmgPlayerLevel = new SSMG_PLAYER_LEVEL();
            if (this.map.Info.Flag.Test(MapFlags.Dominion))
            {
                ssmgPlayerLevel.Level = this.Character.DominionLevel;
                ssmgPlayerLevel.JobLevel = this.Character.DominionJobLevel;
                ssmgPlayerLevel.JobLevel2T = this.Character.DominionJobLevel;
                ssmgPlayerLevel.JobLevel2X = this.Character.DominionJobLevel;
                ssmgPlayerLevel.JobLevelJoint = this.Character.JointJobLevel;
                ssmgPlayerLevel.BonusPoint = this.Character.StatsPoint;
                ssmgPlayerLevel.SkillPoint = (ushort)0;
                ssmgPlayerLevel.Skill2XPoint = (ushort)0;
                ssmgPlayerLevel.Skill2TPoint = (ushort)0;
            }
            else
            {
                ssmgPlayerLevel.Level = this.Character.Level;
                ssmgPlayerLevel.JobLevel = this.Character.JobLevel1;
                ssmgPlayerLevel.JobLevel2T = this.Character.JobLevel2T;
                ssmgPlayerLevel.JobLevel2X = this.Character.JobLevel2X;
                ssmgPlayerLevel.JobLevelJoint = this.Character.JointJobLevel;
                ssmgPlayerLevel.BonusPoint = this.Character.StatsPoint;
                ssmgPlayerLevel.SkillPoint = this.Character.SkillPoint;
                ssmgPlayerLevel.Skill2XPoint = this.Character.SkillPoint2X;
                ssmgPlayerLevel.Skill2TPoint = this.Character.SkillPoint2T;
            }
            this.netIO.SendPacket((Packet)ssmgPlayerLevel);
        }

        /// <summary>
        /// The SendPlayerJob.
        /// </summary>
        public void SendPlayerJob()
        {
            if (!this.Character.Online)
                return;
            SSMG_PLAYER_JOB ssmgPlayerJob = new SSMG_PLAYER_JOB();
            ssmgPlayerJob.Job = this.Character.Job;
            if (this.Character.JobJoint != PC_JOB.NONE)
                ssmgPlayerJob.JointJob = this.Character.JobJoint;
            this.netIO.SendPacket((Packet)ssmgPlayerJob);
        }

        /// <summary>
        /// The SendCharInfoUpdate.
        /// </summary>
        public void SendCharInfoUpdate()
        {
            if (!this.Character.Online)
                return;
            this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The SendAnnounce.
        /// </summary>
        /// <param name="text">The text<see cref="string"/>.</param>
        public void SendAnnounce(string text)
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_CHAT_PUBLIC()
            {
                ActorID = 0U,
                Message = text
            });
        }

        /// <summary>
        /// The SendPkMode.
        /// </summary>
        public void SendPkMode()
        {
            if (!this.Character.Online)
                return;
            this.Character.Mode = PlayerMode.COLISEUM_MODE;
            this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_MODE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The SendNormalMode.
        /// </summary>
        public void SendNormalMode()
        {
            if (!this.Character.Online)
                return;
            this.Character.Mode = PlayerMode.NORMAL;
            this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_MODE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The SendPlayerSizeUpdate.
        /// </summary>
        public void SendPlayerSizeUpdate()
        {
            if (!this.Character.Online)
                return;
            this.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.PLAYER_SIZE_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
        }

        /// <summary>
        /// The OnMove.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PLAYER_MOVE"/>.</param>
        public void OnMove(CSMG_PLAYER_MOVE p)
        {
            if (!this.Character.Online || this.state != MapClient.SESSION_STATE.LOADED)
                return;
            switch (p.MoveType)
            {
                case MoveType.CHANGE_DIR:
                    this.Map.MoveActor(SagaMap.Map.MOVE_TYPE.STOP, (SagaDB.Actor.Actor)this.Character, new short[2]
                    {
            p.X,
            p.Y
                    }, p.Dir, this.Character.Speed);
                    break;
                case MoveType.RUN:
                    this.Map.MoveActor(SagaMap.Map.MOVE_TYPE.START, (SagaDB.Actor.Actor)this.Character, new short[2]
                    {
            p.X,
            p.Y
                    }, p.Dir, this.Character.Speed);
                    this.moveCheckStamp = DateTime.Now;
                    break;
            }
        }

        /// <summary>
        /// The SendActorSpeed.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="speed">The speed<see cref="ushort"/>.</param>
        public void SendActorSpeed(SagaDB.Actor.Actor actor, ushort speed)
        {
            if (!this.Character.Online)
                return;
            this.netIO.SendPacket((Packet)new SSMG_ACTOR_SPEED()
            {
                ActorID = actor.ActorID,
                Speed = speed
            });
        }

        /// <summary>
        /// The SendEXP.
        /// </summary>
        public void SendEXP()
        {
            if (!this.Character.Online)
                return;
            SSMG_PLAYER_EXP ssmgPlayerExp = new SSMG_PLAYER_EXP();
            uint num1;
            if (this.map.Info.Flag.Test(MapFlags.Dominion))
            {
                uint expForLevel1 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.DominionLevel, LevelType.CLEVEL);
                uint expForLevel2 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.DominionLevel + 1U, LevelType.CLEVEL);
                num1 = (uint)((double)(this.Character.DominionCEXP - expForLevel1) / (double)(expForLevel2 - expForLevel1) * 1000.0);
            }
            else
            {
                uint expForLevel1 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.Level, LevelType.CLEVEL);
                uint expForLevel2 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.Level + 1U, LevelType.CLEVEL);
                num1 = (uint)((double)(this.Character.CEXP - expForLevel1) / (double)(expForLevel2 - expForLevel1) * 1000.0);
            }
            uint num2;
            if (this.Character.JobJoint == PC_JOB.NONE)
            {
                if (this.map.Info.Flag.Test(MapFlags.Dominion))
                {
                    uint expForLevel1 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.DominionJobLevel, LevelType.JLEVEL2);
                    uint expForLevel2 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.DominionJobLevel + 1U, LevelType.JLEVEL2);
                    num2 = (uint)((double)(this.Character.DominionJEXP - expForLevel1) / (double)(expForLevel2 - expForLevel1) * 1000.0);
                }
                else
                {
                    uint expForLevel1;
                    uint expForLevel2;
                    if (this.Character.Job == this.Character.JobBasic)
                    {
                        expForLevel1 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel1, LevelType.JLEVEL);
                        expForLevel2 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel1 + 1U, LevelType.JLEVEL);
                    }
                    else if (this.Character.Job == this.Character.Job2X)
                    {
                        expForLevel1 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2X, LevelType.JLEVEL2);
                        expForLevel2 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2X + 1U, LevelType.JLEVEL2);
                    }
                    else
                    {
                        expForLevel1 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2T, LevelType.JLEVEL2);
                        expForLevel2 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2T + 1U, LevelType.JLEVEL2);
                    }
                    num2 = (uint)((double)(this.Character.JEXP - expForLevel1) / (double)(expForLevel2 - expForLevel1) * 1000.0);
                }
            }
            else
            {
                uint expForLevel1 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JointJobLevel, LevelType.JLEVEL2);
                uint expForLevel2 = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JointJobLevel + 1U, LevelType.JLEVEL2);
                num2 = (uint)((double)(this.Character.JointJEXP - expForLevel1) / (double)(expForLevel2 - expForLevel1) * 1000.0);
            }
            ssmgPlayerExp.EXPPercentage = num1;
            ssmgPlayerExp.JEXPPercentage = num2;
            ssmgPlayerExp.WRP = this.Character.WRP;
            ssmgPlayerExp.ECoin = this.Character.ECoin;
            if (this.map.Info.Flag.Test(MapFlags.Dominion))
            {
                ssmgPlayerExp.Exp = (long)this.Character.DominionCEXP;
                ssmgPlayerExp.JExp = (long)this.Character.DominionJEXP;
            }
            else
            {
                ssmgPlayerExp.Exp = (long)this.Character.CEXP;
                ssmgPlayerExp.JExp = (long)this.Character.JEXP;
            }
            this.netIO.SendPacket((Packet)ssmgPlayerExp);
        }

        /// <summary>
        /// The SendLvUP.
        /// </summary>
        /// <param name="pc">The pc<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="type">The type<see cref="byte"/>.</param>
        public void SendLvUP(SagaDB.Actor.Actor pc, byte type)
        {
            if (!this.Character.Online)
                return;
            SSMG_ACTOR_LEVEL_UP ssmgActorLevelUp = new SSMG_ACTOR_LEVEL_UP();
            ssmgActorLevelUp.ActorID = pc.ActorID;
            if (this.map.Info.Flag.Test(MapFlags.Dominion))
            {
                ssmgActorLevelUp.Level = ((ActorPC)pc).DominionLevel;
                ssmgActorLevelUp.JobLevel = ((ActorPC)pc).DominionJobLevel;
            }
            else
                ssmgActorLevelUp.Level = pc.Level;
            ssmgActorLevelUp.LvType = type;
            this.netIO.SendPacket((Packet)ssmgActorLevelUp);
        }

        /// <summary>
        /// The OnPlayerGreetings.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PLAYER_GREETINGS"/>.</param>
        public void OnPlayerGreetings(CSMG_PLAYER_GREETINGS p)
        {
            SagaDB.Actor.Actor actor = this.map.GetActor(p.ActorID);
            if (actor == null || actor.type != ActorType.PC)
                return;
            ActorPC pc1 = (ActorPC)actor;
            if (pc1.Online && this.chara.Online)
            {
                this.map.CalcDir(this.chara.X, this.chara.Y, pc1.X, pc1.Y);
                this.map.CalcDir(pc1.X, pc1.Y, this.chara.X, this.chara.Y);
                this.SendMotion((MotionType)163, (byte)0);
                MapClient.FromActorPC(pc1).SendMotion((MotionType)163, (byte)0);
                foreach (ActorPC pc2 in new List<ActorPC>()
        {
          this.chara,
          pc1
        })
                {
                    if (pc2.EPGreetingTime < DateTime.Now)
                    {
                        IEnumerable<ActorPC> source = MapServer.charDB.GetFriendList(pc2).Where<ActorPC>((Func<ActorPC, bool>)(friends => MapClientManager.Instance.FindClient(friends.CharID) != null));
                        pc2.EP += (uint)(1 + source.ToList<ActorPC>().Count / 10);
                        if (pc2.EP > pc2.MaxEP)
                            pc2.EP = pc2.MaxEP;
                        pc2.EPGreetingTime = DateTime.Today + new TimeSpan(1, 0, 0, 0);
                    }
                    else
                    {
                        TimeSpan timeSpan = pc2.EPGreetingTime - DateTime.Now;
                        MapClient.FromActorPC(pc2).SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.EP_INCREASE, (object)(timeSpan.Hours + 1)));
                    }
                }
            }
        }

        /// <summary>
        /// The OnPlayerElements.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PLAYER_ELEMENTS"/>.</param>
        public void OnPlayerElements(CSMG_PLAYER_ELEMENTS p)
        {
            SSMG_PLAYER_ELEMENTS ssmgPlayerElements = new SSMG_PLAYER_ELEMENTS();
            Dictionary<Elements, int> dictionary = new Dictionary<Elements, int>();
            foreach (Elements key in this.chara.AttackElements.Keys)
                dictionary.Add(key, this.chara.AttackElements[key] + this.chara.Status.attackElements_item[key]);
            ssmgPlayerElements.AttackElements = dictionary;
            dictionary.Clear();
            foreach (Elements key in this.chara.Elements.Keys)
                dictionary.Add(key, this.chara.Elements[key] + this.chara.Status.elements_item[key]);
            ssmgPlayerElements.DefenceElements = dictionary;
            this.netIO.SendPacket((Packet)ssmgPlayerElements);
        }

        /// <summary>
        /// The OnRequestPCInfo.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ACTOR_REQUEST_PC_INFO"/>.</param>
        public void OnRequestPCInfo(CSMG_ACTOR_REQUEST_PC_INFO p)
        {
            SSMG_ACTOR_PC_INFO ssmgActorPcInfo = new SSMG_ACTOR_PC_INFO();
            SagaDB.Actor.Actor actor = this.map.GetActor(p.ActorID);
            if (actor == null)
                return;
            if (actor.type == ActorType.PC)
            {
                ActorPC pc = (ActorPC)actor;
                pc.WRPRanking = Singleton<WRPRankingManager>.Instance.GetRanking(pc);
            }
            ssmgActorPcInfo.Actor = actor;
            this.netIO.SendPacket((Packet)ssmgActorPcInfo);
            if (actor.type != ActorType.PC)
                return;
            ActorPC aActor = (ActorPC)actor;
            if (aActor.Ring != null)
                this.Character.e.OnActorRingUpdate(aActor);
        }

        /// <summary>
        /// The OnStatsPreCalc.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PLAYER_STATS_PRE_CALC"/>.</param>
        public void OnStatsPreCalc(CSMG_PLAYER_STATS_PRE_CALC p)
        {
            SSMG_PLAYER_STATS_PRE_CALC playerStatsPreCalc = new SSMG_PLAYER_STATS_PRE_CALC();
            ushort str = this.Character.Str;
            ushort dex = this.Character.Dex;
            ushort num1 = this.Character.Int;
            ushort agi = this.Character.Agi;
            ushort vit = this.Character.Vit;
            ushort mag = this.Character.Mag;
            this.Character.Str = p.Str;
            this.Character.Dex = p.Dex;
            this.Character.Int = p.Int;
            this.Character.Agi = p.Agi;
            this.Character.Vit = p.Vit;
            this.Character.Mag = p.Mag;
            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
            playerStatsPreCalc.ASPD = this.Character.Status.aspd;
            playerStatsPreCalc.ATK1Max = this.Character.Status.max_atk1;
            playerStatsPreCalc.ATK1Min = this.Character.Status.min_atk1;
            playerStatsPreCalc.ATK2Max = this.Character.Status.max_atk2;
            playerStatsPreCalc.ATK2Min = this.Character.Status.min_atk2;
            playerStatsPreCalc.ATK3Max = this.Character.Status.max_atk3;
            playerStatsPreCalc.ATK3Min = this.Character.Status.min_atk3;
            playerStatsPreCalc.AvoidCritical = this.Character.Status.avoid_critical;
            playerStatsPreCalc.AvoidMagic = this.Character.Status.avoid_magic;
            playerStatsPreCalc.AvoidMelee = this.Character.Status.avoid_melee;
            playerStatsPreCalc.AvoidRanged = this.Character.Status.avoid_ranged;
            playerStatsPreCalc.CSPD = this.Character.Status.cspd;
            playerStatsPreCalc.DefAddition = (ushort)this.Character.Status.def_add;
            playerStatsPreCalc.DefBase = this.Character.Status.def;
            playerStatsPreCalc.HitCritical = this.Character.Status.hit_critical;
            playerStatsPreCalc.HitMagic = this.Character.Status.hit_magic;
            playerStatsPreCalc.HitMelee = this.Character.Status.hit_melee;
            playerStatsPreCalc.HitRanged = this.Character.Status.hit_ranged;
            playerStatsPreCalc.MATKMax = this.Character.Status.max_matk;
            playerStatsPreCalc.MATKMin = this.Character.Status.min_matk;
            playerStatsPreCalc.MDefAddition = (ushort)this.Character.Status.mdef_add;
            playerStatsPreCalc.MDefBase = this.Character.Status.mdef;
            playerStatsPreCalc.Speed = this.Character.Speed;
            playerStatsPreCalc.HP = (ushort)this.Character.MaxHP;
            playerStatsPreCalc.MP = (ushort)this.Character.MaxMP;
            playerStatsPreCalc.SP = (ushort)this.Character.MaxSP;
            uint num2 = 0;
            foreach (uint num3 in this.Character.Inventory.MaxVolume.Values)
                num2 += num3;
            playerStatsPreCalc.Capacity = (ushort)num2;
            uint num4 = 0;
            foreach (uint num3 in this.Character.Inventory.MaxPayload.Values)
                num4 += num3;
            playerStatsPreCalc.Payload = (ushort)num4;
            this.Character.Str = str;
            this.Character.Dex = dex;
            this.Character.Int = num1;
            this.Character.Agi = agi;
            this.Character.Vit = vit;
            this.Character.Mag = mag;
            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
            this.netIO.SendPacket((Packet)playerStatsPreCalc);
        }

        /// <summary>
        /// The OnStatsUp.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PLAYER_STATS_UP"/>.</param>
        public void OnStatsUp(CSMG_PLAYER_STATS_UP p)
        {
            switch (p.Type)
            {
                case 0:
                    if ((int)this.Character.StatsPoint >= (int)Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Str))
                    {
                        this.Character.StatsPoint -= Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Str);
                        ++this.Character.Str;
                        break;
                    }
                    break;
                case 1:
                    if ((int)this.Character.StatsPoint >= (int)Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Dex))
                    {
                        this.Character.StatsPoint -= Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Dex);
                        ++this.Character.Dex;
                        break;
                    }
                    break;
                case 2:
                    if ((int)this.Character.StatsPoint >= (int)Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Int))
                    {
                        this.Character.StatsPoint -= Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Int);
                        ++this.Character.Int;
                        break;
                    }
                    break;
                case 3:
                    if ((int)this.Character.StatsPoint >= (int)Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Vit))
                    {
                        this.Character.StatsPoint -= Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Vit);
                        ++this.Character.Vit;
                        break;
                    }
                    break;
                case 4:
                    if ((int)this.Character.StatsPoint >= (int)Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Agi))
                    {
                        this.Character.StatsPoint -= Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Agi);
                        ++this.Character.Agi;
                        break;
                    }
                    break;
                case 5:
                    if ((int)this.Character.StatsPoint >= (int)Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Mag))
                    {
                        this.Character.StatsPoint -= Singleton<StatusFactory>.Instance.RequiredBonusPoint(this.Character.Mag);
                        ++this.Character.Mag;
                        break;
                    }
                    break;
            }
            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
            this.SendActorHPMPSP((SagaDB.Actor.Actor)this.Character);
            this.SendStatus();
            this.SendStatusExtend();
            this.SendCapacity();
            this.SendMaxCapacity();
            this.SendPlayerLevel();
        }

        /// <summary>
        /// The SendWRPRanking.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SendWRPRanking(ActorPC pc)
        {
            this.netIO.SendPacket((Packet)new SSMG_ACTOR_WRP_RANKING()
            {
                ActorID = pc.ActorID,
                Ranking = pc.WRPRanking
            });
        }

        /// <summary>
        /// The OnPlayerReturnHome.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PLAYER_RETURN_HOME"/>.</param>
        public void OnPlayerReturnHome(CSMG_PLAYER_RETURN_HOME p)
        {
            if (this.Character.HP == 0U)
            {
                this.Character.HP = 1U;
                this.Character.MP = 1U;
                this.Character.SP = 1U;
            }
            if (this.Character.SaveMap == 0U)
            {
                this.Character.SaveMap = 10023100U;
                this.Character.SaveX = (byte)250;
                this.Character.SaveY = (byte)132;
            }
            this.Character.BattleStatus = (byte)0;
            this.SendChangeStatus();
            this.Character.Buff.Dead = false;
            this.Character.Motion = MotionType.STAND;
            this.Character.MotionLoop = false;
            this.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.Character, true);
            Singleton<SkillHandler>.Instance.CastPassiveSkills(this.Character);
            this.SendPlayerInfo();
            if ((int)this.map.ID == (int)this.Character.SaveMap)
            {
                if (this.Map.Info.Healing)
                {
                    if (!this.Character.Tasks.ContainsKey("CityRecover"))
                    {
                        CityRecover cityRecover = new CityRecover(this);
                        this.Character.Tasks.Add("CityRecover", (MultiRunTask)cityRecover);
                        cityRecover.Activate();
                    }
                }
                else if (this.Character.Tasks.ContainsKey("CityRecover"))
                {
                    this.Character.Tasks["CityRecover"].Deactivate();
                    this.Character.Tasks.Remove("CityRecover");
                }
                if (this.Map.Info.Cold || this.map.Info.Hot || this.map.Info.Wet)
                {
                    if (!this.Character.Tasks.ContainsKey("CityDown"))
                    {
                        CityDown cityDown = new CityDown(this);
                        this.Character.Tasks.Add("CityDown", (MultiRunTask)cityDown);
                        cityDown.Activate();
                    }
                }
                else if (this.Character.Tasks.ContainsKey("CityDown"))
                {
                    this.Character.Tasks["CityDown"].Deactivate();
                    this.Character.Tasks.Remove("CityDown");
                }
            }
            if (Singleton<Configuration>.Instance.HostedMaps.Contains(this.Character.SaveMap))
            {
                MapInfo mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[this.Character.SaveMap];
                this.Map.SendActorToMap((SagaDB.Actor.Actor)this.Character, this.Character.SaveMap, SagaLib.Global.PosX8to16(this.Character.SaveX, mapInfo.width), SagaLib.Global.PosY8to16(this.Character.SaveY, mapInfo.height));
            }
            Event @event = (Event)null;
            if (@event == null)
                return;
            @event.CurrentPC = (ActorPC)null;
            this.scriptThread = (Thread)null;
            this.currentEvent = (Event)null;
            ClientManager.RemoveThread(Thread.CurrentThread.Name);
            ClientManager.LeaveCriticalArea();
        }

        /// <summary>
        /// The OnIrisCardAssembleCancel.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_CARD_ASSEMBLE_CANCEL"/>.</param>
        public void OnIrisCardAssembleCancel(CSMG_IRIS_CARD_ASSEMBLE_CANCEL p)
        {
            this.irisCardAssemble = false;
            this.netIO.SendPacket(new Packet(2U)
            {
                ID = (ushort)1501
            });
        }

        /// <summary>
        /// The OnIrisCardAssemble.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_CARD_ASSEMBLE"/>.</param>
        public void OnIrisCardAssemble(CSMG_IRIS_CARD_ASSEMBLE p)
        {
            uint cardId = p.CardID;
            if (this.CountItem(cardId) > 0)
            {
                if (!Factory<IrisCardFactory, IrisCard>.Instance.Items.ContainsKey(cardId))
                    return;
                IrisCard irisCard = Factory<IrisCardFactory, IrisCard>.Instance.Items[cardId];
                if (irisCard.NextCard != 0U)
                {
                    int[] numArray1 = new int[4] { 90, 60, 30, 5 };
                    int[] numArray2 = new int[4] { 10, 2, 2, 2 };
                    int num1 = numArray1[irisCard.Rank];
                    int num2 = numArray2[irisCard.Rank];
                    if (this.CountItem(cardId) >= num2)
                    {
                        if (this.chara.Gold >= 5000)
                        {
                            this.chara.Gold -= 5000;
                            this.DeleteItemID(cardId, (ushort)num2, true);
                            if (SagaLib.Global.Random.Next(0, 99) < num1)
                            {
                                this.AddItem(Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(irisCard.NextCard), true);
                                this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_ASSEMBLE_RESULT()
                                {
                                    Result = SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.OK
                                });
                            }
                            else
                            {
                                this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_ASSEMBLE_RESULT()
                                {
                                    Result = SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.FAILED
                                });
                                this.irisCardAssemble = false;
                            }
                        }
                        else
                        {
                            this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_ASSEMBLE_RESULT()
                            {
                                Result = SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NOT_ENOUGH_GOLD
                            });
                            this.irisCardAssemble = false;
                        }
                    }
                    else
                    {
                        this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_ASSEMBLE_RESULT()
                        {
                            Result = SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NOT_ENOUGH_ITEM
                        });
                        this.irisCardAssemble = false;
                    }
                }
                else
                {
                    this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_ASSEMBLE_RESULT()
                    {
                        Result = SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NO_ITEM
                    });
                    this.irisCardAssemble = false;
                }
            }
            else
            {
                this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_ASSEMBLE_RESULT()
                {
                    Result = SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NO_ITEM
                });
                this.irisCardAssemble = false;
            }
        }

        /// <summary>
        /// The OnIrisCardClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_CARD_CLOSE"/>.</param>
        public void OnIrisCardClose(CSMG_IRIS_CARD_CLOSE p)
        {
            this.irisCardItem = 0U;
        }

        /// <summary>
        /// The OnIrisCardLock.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_CARD_LOCK"/>.</param>
        public void OnIrisCardLock(CSMG_IRIS_CARD_LOCK p)
        {
            SagaDB.Item.Item obj = this.chara.Inventory.GetItem(this.irisCardItem);
            if (obj == null)
                return;
            obj.Locked = true;
            this.SendItemIdentify(obj.Slot);
            this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_LOCK_RESULT());
        }

        /// <summary>
        /// The OnIrisCardRemove.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_CARD_REMOVE"/>.</param>
        public void OnIrisCardRemove(CSMG_IRIS_CARD_REMOVE p)
        {
            SagaDB.Item.Item obj = this.chara.Inventory.GetItem(this.irisCardItem);
            if (obj != null)
            {
                if (!obj.Locked)
                {
                    if ((int)p.CardSlot < obj.Cards.Count)
                    {
                        this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_REMOVE_RESULT()
                        {
                            Result = SSMG_IRIS_CARD_REMOVE_RESULT.Results.OK
                        });
                        this.AddItem(Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(obj.Cards[(int)p.CardSlot].ID), true);
                        obj.Cards.RemoveAt((int)p.CardSlot);
                        this.SendItemCardInfo(obj);
                        this.SendItemCardAbility(obj);
                    }
                    else
                        this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_REMOVE_RESULT()
                        {
                            Result = SSMG_IRIS_CARD_REMOVE_RESULT.Results.FAILED
                        });
                }
                else
                    this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_REMOVE_RESULT()
                    {
                        Result = SSMG_IRIS_CARD_REMOVE_RESULT.Results.FAILED
                    });
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_REMOVE_RESULT()
                {
                    Result = SSMG_IRIS_CARD_REMOVE_RESULT.Results.FAILED
                });
        }

        /// <summary>
        /// The OnIrisCardInsert.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_CARD_INSERT"/>.</param>
        public void OnIrisCardInsert(CSMG_IRIS_CARD_INSERT p)
        {
            SagaDB.Item.Item obj1 = this.chara.Inventory.GetItem(this.irisCardItem);
            if (obj1 == null)
                return;
            if (obj1.Cards.Count < (int)obj1.CurrentSlot)
            {
                SagaDB.Item.Item obj2 = this.chara.Inventory.GetItem(p.InventorySlot);
                if (obj2 != null && obj2.BaseData.itemType == ItemType.IRIS_CARD)
                {
                    if (Factory<IrisCardFactory, IrisCard>.Instance.Items.ContainsKey(obj2.BaseData.id))
                    {
                        this.DeleteItem(obj2.Slot, (ushort)1, true);
                        this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_INSERT_RESULT()
                        {
                            Result = SSMG_IRIS_CARD_INSERT_RESULT.Results.OK
                        });
                        IrisCard irisCard = Factory<IrisCardFactory, IrisCard>.Instance.Items[obj2.BaseData.id];
                        obj1.Cards.Add(irisCard);
                        this.SendItemCardInfo(obj1);
                        this.SendItemCardAbility(obj1);
                    }
                    else
                        this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_INSERT_RESULT()
                        {
                            Result = SSMG_IRIS_CARD_INSERT_RESULT.Results.CANNOT_SET
                        });
                }
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_INSERT_RESULT()
                {
                    Result = SSMG_IRIS_CARD_INSERT_RESULT.Results.SLOT_OVER
                });
        }

        /// <summary>
        /// The OnIrisCardOpen.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_CARD_OPEN"/>.</param>
        public void OnIrisCardOpen(CSMG_IRIS_CARD_OPEN p)
        {
            SagaDB.Item.Item obj = this.chara.Inventory.GetItem(p.InventorySlot);
            if (obj != null)
            {
                if (obj.CurrentSlot > (byte)0)
                {
                    this.irisCardItem = obj.Slot;
                    this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_OPEN_RESULT()
                    {
                        Result = SSMG_IRIS_CARD_OPEN_RESULT.Results.OK
                    });
                    this.SendItemCardAbility(obj);
                }
                else
                    this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_OPEN_RESULT()
                    {
                        Result = SSMG_IRIS_CARD_OPEN_RESULT.Results.NO_SLOT
                    });
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_OPEN_RESULT()
                {
                    Result = SSMG_IRIS_CARD_OPEN_RESULT.Results.NO_ITEM
                });
        }

        /// <summary>
        /// The OnIrisAddSlotConfirm.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_ADD_SLOT_CONFIRM"/>.</param>
        public void OnIrisAddSlotConfirm(CSMG_IRIS_ADD_SLOT_CONFIRM p)
        {
            if (!this.irisAddSlot)
                return;
            SagaDB.Item.Item obj = this.Character.Inventory.GetItem(this.irisAddSlotItem);
            if (obj != null)
            {
                int num1 = (int)obj.BaseData.possibleLv * 1000;
                uint irisAddSlotMaterial = this.irisAddSlotMaterial;
                if (this.CountItem(irisAddSlotMaterial) > 0)
                {
                    if (this.chara.Gold > num1)
                    {
                        if (obj.CurrentSlot < (byte)5)
                        {
                            this.chara.Gold -= num1;
                            this.DeleteItemID(irisAddSlotMaterial, (ushort)1, true);
                            int num2 = 90 - (int)obj.CurrentSlot * 20;
                            if (num2 < 0)
                                num2 = 5;
                            if (SagaLib.Global.Random.Next(0, 99) < num2)
                            {
                                this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                                {
                                    Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.OK
                                });
                                this.SendEffect(5145U);
                                ++obj.CurrentSlot;
                                this.SendItemInfo(obj);
                            }
                            else
                            {
                                this.DeleteItem(obj.Slot, (ushort)1, true);
                                this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                                {
                                    Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.Failed
                                });
                            }
                        }
                        else
                        {
                            this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                            {
                                Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.Failed
                            });
                            this.irisAddSlot = false;
                        }
                    }
                    else
                        this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                        {
                            Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.NOT_ENOUGH_GOLD
                        });
                }
                else
                    this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                    {
                        Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.NO_RIGHT_MATERIAL
                    });
            }
            else
            {
                this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                {
                    Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.NO_ITEM
                });
                this.irisAddSlot = false;
            }
        }

        /// <summary>
        /// The OnIrisAddSlotCancel.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_ADD_SLOT_CANCEL"/>.</param>
        public void OnIrisAddSlotCancel(CSMG_IRIS_ADD_SLOT_CANCEL p)
        {
            this.irisAddSlot = false;
        }

        /// <summary>
        /// The OnIrisAddSlotItemSelect.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_IRIS_ADD_SLOT_ITEM_SELECT"/>.</param>
        public void OnIrisAddSlotItemSelect(CSMG_IRIS_ADD_SLOT_ITEM_SELECT p)
        {
            if (!this.irisAddSlot)
                return;
            SagaDB.Item.Item obj = this.Character.Inventory.GetItem(p.InventorySlot);
            if (obj != null)
            {
                int num1 = (int)obj.BaseData.possibleLv * 1000;
                uint num2 = obj.BaseData.possibleLv > (byte)30 ? (obj.BaseData.possibleLv > (byte)70 ? 10073200U : 10073100U) : 10073000U;
                if (this.chara.Gold > num1)
                {
                    if (obj.CurrentSlot < (byte)5)
                    {
                        this.irisAddSlotMaterial = num2;
                        this.irisAddSlotItem = obj.Slot;
                        this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_MATERIAL()
                        {
                            Slot = (byte)1,
                            Material = num2,
                            Gold = num1
                        });
                    }
                    else
                        this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                        {
                            Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.Failed
                        });
                }
                else
                    this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                    {
                        Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.NOT_ENOUGH_GOLD
                    });
            }
            else
            {
                this.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                {
                    Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.NO_ITEM
                });
                this.irisAddSlot = false;
            }
        }

        /// <summary>
        /// The SendItemCardInfo.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        public void SendItemCardInfo(SagaDB.Item.Item item)
        {
            this.netIO.SendPacket((Packet)new SSMG_ITEM_IRIS_CARD_INFO()
            {
                Item = item
            });
        }

        /// <summary>
        /// The SendItemCardAbility.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        public void SendItemCardAbility(SagaDB.Item.Item item)
        {
            SSMG_IRIS_CARD_ITEM_ABILITY irisCardItemAbility1 = new SSMG_IRIS_CARD_ITEM_ABILITY();
            irisCardItemAbility1.Type = SSMG_IRIS_CARD_ITEM_ABILITY.Types.Deck;
            irisCardItemAbility1.AbilityVectors = item.AbilityVectors(true);
            irisCardItemAbility1.VectorValues = item.VectorValues(true, false).Values.ToList<int>();
            irisCardItemAbility1.VectorLevels = item.VectorValues(true, true).Values.ToList<int>();
            Dictionary<ReleaseAbility, int> dictionary1 = item.ReleaseAbilities(true);
            irisCardItemAbility1.ReleaseAbilities = dictionary1.Keys.ToList<ReleaseAbility>();
            irisCardItemAbility1.AbilityValues = dictionary1.Values.ToList<int>();
            irisCardItemAbility1.ElementsAttack = item.EquipSlot[0] != EnumEquipSlot.RIGHT_HAND ? new Dictionary<Elements, int>() : item.Elements(true);
            irisCardItemAbility1.ElementsDefence = item.EquipSlot[0] != EnumEquipSlot.UPPER_BODY ? new Dictionary<Elements, int>() : item.Elements(true);
            this.netIO.SendPacket((Packet)irisCardItemAbility1);
            SSMG_IRIS_CARD_ITEM_ABILITY irisCardItemAbility2 = new SSMG_IRIS_CARD_ITEM_ABILITY();
            irisCardItemAbility2.Type = SSMG_IRIS_CARD_ITEM_ABILITY.Types.Max;
            irisCardItemAbility2.AbilityVectors = item.AbilityVectors(false);
            irisCardItemAbility2.VectorValues = item.VectorValues(false, false).Values.ToList<int>();
            irisCardItemAbility2.VectorLevels = item.VectorValues(false, true).Values.ToList<int>();
            Dictionary<ReleaseAbility, int> dictionary2 = item.ReleaseAbilities(false);
            irisCardItemAbility2.ReleaseAbilities = dictionary2.Keys.ToList<ReleaseAbility>();
            irisCardItemAbility2.AbilityValues = dictionary2.Values.ToList<int>();
            irisCardItemAbility2.ElementsAttack = item.EquipSlot[0] != EnumEquipSlot.RIGHT_HAND ? new Dictionary<Elements, int>() : item.Elements(false);
            irisCardItemAbility2.ElementsDefence = item.EquipSlot[0] != EnumEquipSlot.UPPER_BODY ? new Dictionary<Elements, int>() : item.Elements(false);
            this.netIO.SendPacket((Packet)irisCardItemAbility2);
        }

        /// <summary>
        /// The OnPing.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PING"/>.</param>
        public void OnPing(CSMG_PING p)
        {
            if (this.chara != null && this.chara.Online)
            {
                this.ping = DateTime.Now;
                if (!this.chara.Tasks.ContainsKey("Ping"))
                {
                    Ping ping = new Ping(this);
                    this.chara.Tasks.Add("Ping", (MultiRunTask)ping);
                    ping.Activate();
                }
            }
            SSMG_PONG ssmgPong = new SSMG_PONG();
            this.netIO.SendPacket((Packet)p);
        }

        /// <summary>
        /// The OnSendVersion.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SEND_VERSION"/>.</param>
        public void OnSendVersion(CSMG_SEND_VERSION p)
        {
            if (Singleton<Configuration>.Instance.ClientVersion == null || Singleton<Configuration>.Instance.ClientVersion == p.GetVersion())
            {
                Logger.ShowInfo(string.Format(Singleton<LocalManager>.Instance.Strings.CLIENT_CONNECTING, (object)p.GetVersion()));
                this.client_Version = p.GetVersion();
                SSMG_VERSION_ACK ssmgVersionAck = new SSMG_VERSION_ACK();
                ssmgVersionAck.SetResult(SSMG_VERSION_ACK.Result.OK);
                ssmgVersionAck.SetVersion(this.client_Version);
                this.netIO.SendPacket((Packet)ssmgVersionAck);
                SSMG_LOGIN_ALLOWED ssmgLoginAllowed = new SSMG_LOGIN_ALLOWED();
                this.frontWord = (uint)SagaLib.Global.Random.Next();
                this.backWord = (uint)SagaLib.Global.Random.Next();
                ssmgLoginAllowed.FrontWord = this.frontWord;
                ssmgLoginAllowed.BackWord = this.backWord;
                this.netIO.SendPacket((Packet)ssmgLoginAllowed);
            }
            else
            {
                SSMG_VERSION_ACK ssmgVersionAck = new SSMG_VERSION_ACK();
                ssmgVersionAck.SetResult(SSMG_VERSION_ACK.Result.VERSION_MISSMATCH);
                this.netIO.SendPacket((Packet)ssmgVersionAck);
            }
        }

        /// <summary>
        /// The OnLogin.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_LOGIN"/>.</param>
        public void OnLogin(CSMG_LOGIN p)
        {
            p.GetContent();
            if (MapServer.shutingdown)
                this.netIO.SendPacket((Packet)new SSMG_LOGIN_ACK()
                {
                    LoginResult = SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_IPBLOCK
                });
            else if (MapServer.accountDB.CheckPassword(p.UserName, p.Password, this.frontWord, this.backWord))
            {
                this.netIO.SendPacket((Packet)new SSMG_LOGIN_ACK()
                {
                    LoginResult = SSMG_LOGIN_ACK.Result.OK,
                    Unknown1 = (ushort)256,
                    Unknown2 = 1215214624U
                });
                this.account = MapServer.accountDB.GetUser(p.UserName);
                foreach (SagaLib.Client client in MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(acc => acc.account.Name == this.account.Name)))
                    client.netIO.Disconnect();
                this.account.LastIP = this.netIO.sock.RemoteEndPoint.ToString().Split(':')[0];
                uint[] charIds = MapServer.charDB.GetCharIDs(this.account.AccountID);
                this.account.Characters = new List<ActorPC>();
                for (int index = 0; index < charIds.Length; ++index)
                    this.account.Characters.Add(MapServer.charDB.GetChar(charIds[index]));
                this.state = MapClient.SESSION_STATE.AUTHENTIFICATED;
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_LOGIN_ACK()
                {
                    LoginResult = SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_BADPASS
                });
        }

        /// <summary>
        /// The OnCharSlot.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAR_SLOT"/>.</param>
        public void OnCharSlot(CSMG_CHAR_SLOT p)
        {
            if (this.state != MapClient.SESSION_STATE.AUTHENTIFICATED)
                return;
            this.Character = this.account.Characters.Where<ActorPC>((Func<ActorPC, bool>)(c => (int)c.Slot == (int)p.Slot)).First<ActorPC>();
            if (this.Character.PossessionTarget != 0U)
            {
                if (this.Character.PossessionTarget < 10000U)
                {
                    MapClient client = (MapClient)MapClientManager.Instance.GetClient(this.Character.PossessionTarget);
                    if (client != null)
                    {
                        ActorPC character = client.Character;
                        ActorPC actorPc = (ActorPC)null;
                        foreach (ActorPC possesionedActor in character.PossesionedActors)
                        {
                            if ((int)possesionedActor.CharID == (int)this.Character.CharID)
                            {
                                actorPc = possesionedActor;
                                break;
                            }
                        }
                        if (actorPc != null)
                        {
                            actorPc.Inventory = this.Character.Inventory;
                            this.Character = actorPc;
                            this.Character.MapID = character.MapID;
                            this.Character.X = character.X;
                            this.Character.Y = character.Y;
                        }
                        else
                            this.Character.PossessionTarget = 0U;
                    }
                    else
                        this.Character.PossessionTarget = 0U;
                }
                else
                {
                    SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(this.Character.MapID);
                    if (map != null)
                    {
                        SagaDB.Actor.Actor actor = map.GetActor(this.Character.PossessionTarget);
                        if (actor == null)
                            this.Character.PossessionTarget = 0U;
                        else if (actor.type == ActorType.ITEM)
                        {
                            ActorItem actorItem = (ActorItem)actor;
                            if ((int)actorItem.Item.PossessionedActor.CharID == (int)this.Character.CharID)
                            {
                                ActorPC possessionedActor = actorItem.Item.PossessionedActor;
                                possessionedActor.Inventory = this.Character.Inventory;
                                this.Character = possessionedActor;
                                this.Character.MapID = actorItem.MapID;
                                this.Character.X = actorItem.X;
                                this.Character.Y = actorItem.Y;
                            }
                            else
                                this.Character.PossessionTarget = 0U;
                        }
                        else
                            this.Character.PossessionTarget = 0U;
                    }
                    else
                        this.Character.PossessionTarget = 0U;
                }
            }
            if (this.Character.Golem != null)
            {
                SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(this.Character.MapID);
                if (map != null)
                {
                    SagaDB.Actor.Actor actor = map.GetActor(this.Character.Golem.ActorID);
                    if (actor != null)
                    {
                        if (actor.type == ActorType.GOLEM)
                        {
                            ActorGolem actorGolem = (ActorGolem)actor;
                            if ((int)actorGolem.Owner.CharID == (int)this.Character.CharID)
                            {
                                actorGolem.Owner.Inventory.WareHouse = this.Character.Inventory.WareHouse;
                                this.Character.Inventory = actorGolem.Owner.Inventory;
                                this.Character.Gold = actorGolem.Owner.Gold;
                                this.Character.Golem = actorGolem;
                                actorGolem.ClearTaskAddition();
                                map.DeleteActor((SagaDB.Actor.Actor)actorGolem);
                            }
                            else
                                this.Character.Golem = (ActorGolem)null;
                        }
                    }
                    else
                        this.Character.Golem = (ActorGolem)null;
                }
                else
                    this.Character.Golem = (ActorGolem)null;
            }
            if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && this.Character.Inventory.Equipments[EnumEquipSlot.PET].BaseData.itemType == ItemType.RIDE_PET)
            {
                this.Character.Pet = new ActorPet(this.Character.Inventory.Equipments[EnumEquipSlot.PET].BaseData.petID, this.Character.Inventory.Equipments[EnumEquipSlot.PET]);
                this.Character.Pet.Ride = true;
                this.Character.Pet.Owner = this.Character;
            }
            this.Character.e = (ActorEventHandler)new PCEventHandler(this);
            if (this.Character.Account == null)
                this.Character.Account = this.account;
            this.Character.Online = true;
            this.Character.Party = Singleton<PartyManager>.Instance.GetParty(this.Character.Party);
            Singleton<PartyManager>.Instance.PlayerOnline(this.Character.Party, this.Character);
            this.Character.Ring = Singleton<RingManager>.Instance.GetRing(this.Character.Ring);
            Singleton<RingManager>.Instance.PlayerOnline(this.Character.Ring, this.Character);
            Logger.ShowInfo(string.Format(Singleton<LocalManager>.Instance.Strings.PLAYER_LOG_IN, (object)this.Character.Name));
            Logger.ShowInfo(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + (object)MapClientManager.Instance.OnlinePlayer.Count);
            MapServer.shouldRefreshStatistic = true;
            this.Map = Singleton<MapManager>.Instance.GetMap(this.Character.MapID);
            if (this.map == null)
            {
                if (this.Character.SaveMap == 0U)
                {
                    this.Character.SaveMap = 10023100U;
                    this.Character.SaveX = (byte)250;
                    this.Character.SaveY = (byte)132;
                }
                this.Character.MapID = this.Character.SaveMap;
                this.map = Singleton<MapManager>.Instance.GetMap(this.Character.SaveMap);
                this.Character.X = SagaLib.Global.PosX8to16(this.chara.SaveX, this.map.Width);
                this.Character.Y = SagaLib.Global.PosY8to16(this.chara.SaveY, this.map.Height);
            }
            if (this.map.IsMapInstance && this.chara.PossessionTarget == 0U)
            {
                SagaMap.Map map = this.map;
                this.Character.MapID = this.map.ClientExitMap;
                this.map = Singleton<MapManager>.Instance.GetMap(this.map.ClientExitMap);
                this.Character.X = SagaLib.Global.PosX8to16(map.ClientExitX, this.map.Width);
                this.Character.Y = SagaLib.Global.PosY8to16(map.ClientExitY, this.map.Height);
            }
            if (this.chara.Race != PC_RACE.DEM)
            {
                if (this.Character.CEXP < Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.Level, LevelType.CLEVEL))
                    this.Character.CEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.Level, LevelType.CLEVEL);
                if (this.Character.DominionCEXP < Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.DominionLevel, LevelType.CLEVEL))
                    this.Character.DominionCEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.DominionLevel, LevelType.CLEVEL);
                if (this.Character.DominionJEXP < Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.DominionJobLevel, LevelType.JLEVEL2))
                    this.Character.DominionJEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.DominionJobLevel, LevelType.JLEVEL2);
                if (this.Character.Job == this.Character.JobBasic)
                {
                    if (this.Character.JEXP < Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel1, LevelType.JLEVEL))
                        this.Character.JEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel1, LevelType.JLEVEL);
                }
                else if (this.Character.Job == this.Character.Job2X)
                {
                    if (this.Character.JEXP < Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2X, LevelType.JLEVEL2))
                        this.Character.JEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2X, LevelType.JLEVEL2);
                }
                else if (this.Character.JEXP < Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2T, LevelType.JLEVEL2))
                    this.Character.JEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)this.Character.JobLevel2T, LevelType.JLEVEL2);
            }
            if ((int)this.Character.DominionStr < (int)Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Str)
                this.Character.DominionStr = Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Str;
            if ((int)this.Character.DominionDex < (int)Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Dex)
                this.Character.DominionDex = Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Dex;
            if ((int)this.Character.DominionInt < (int)Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Int)
                this.Character.DominionInt = Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Int;
            if ((int)this.Character.DominionVit < (int)Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Vit)
                this.Character.DominionVit = Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Vit;
            if ((int)this.Character.DominionAgi < (int)Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Agi)
                this.Character.DominionAgi = Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Agi;
            if ((int)this.Character.DominionMag < (int)Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Mag)
                this.Character.DominionMag = Singleton<Configuration>.Instance.StartupSetting[this.Character.Race].Mag;
            this.Character.WRPRanking = Singleton<WRPRankingManager>.Instance.GetRanking(this.Character);
            foreach (SagaDB.Item.Item obj in this.Character.Inventory.Equipments.Values)
            {
                if (obj.BaseData.jointJob != PC_JOB.NONE)
                {
                    this.Character.JobJoint = obj.BaseData.jointJob;
                    break;
                }
            }
            this.Map.RegisterActor((SagaDB.Actor.Actor)this.Character);
            this.state = MapClient.SESSION_STATE.LOADING;
            if (this.Character.Golem != null)
                this.needSendGolem = true;
            this.SendSystemMessage(string.Format("SagaECO SVN:{0}({1})", (object)SagaMap.GlobalInfo.Version, (object)SagaMap.GlobalInfo.ModifyDate));
            this.SendSystemMessage("================================================");
            this.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.VISIT_OUR_HOMEPAGE);
            foreach (string content in Singleton<Configuration>.Instance.Motd)
                this.SendSystemMessage(content);
            this.SendSystemMessage("================================================");
            Singleton<StatusFactory>.Instance.CalcStatus(this.chara);
            if (this.chara.EPLoginTime < DateTime.Now)
            {
                this.chara.EP += 10U;
                if (this.chara.EP > this.chara.MaxEP)
                    this.chara.EP = this.chara.MaxEP;
                this.chara.EPLoginTime = DateTime.Now + new TimeSpan(1, 0, 0, 0);
            }
            else
                this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.EP_INCREASE, (object)((this.chara.EPLoginTime - DateTime.Now).Hours + 1)));
            foreach (string monitorAccount in Singleton<Configuration>.Instance.MonitorAccounts)
            {
                if (this.account.Name.StartsWith(monitorAccount))
                {
                    this.logger = new SagaMap.PacketLogger.PacketLogger(this);
                    break;
                }
            }
        }

        /// <summary>
        /// The OnMapLoaded.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PLAYER_MAP_LOADED"/>.</param>
        public void OnMapLoaded(CSMG_PLAYER_MAP_LOADED p)
        {
            this.Character.invisble = false;
            this.Character.VisibleActors.Clear();
            this.Map.OnActorVisibilityChange((SagaDB.Actor.Actor)this.Character);
            this.Map.SendVisibleActorsToActor((SagaDB.Actor.Actor)this.Character);
            if (this.needSendGolem)
            {
                ActorGolem golem = this.Character.Golem;
                if (golem.GolemType == GolemType.Sell)
                    this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_SELL_RESULT()
                    {
                        SoldItems = golem.SoldItem
                    });
                if (golem.GolemType == GolemType.Buy)
                    this.netIO.SendPacket((Packet)new SSMG_GOLEM_SHOP_BUY_RESULT()
                    {
                        BoughtItems = golem.BoughtItem
                    });
                if (golem.GolemType >= GolemType.Plant && golem.GolemType <= GolemType.Strange)
                    this.OnGolemWarehouse(new CSMG_GOLEM_WAREHOUSE());
                this.Character.Golem.SoldItem.Clear();
                this.Character.Golem.BoughtItem.Clear();
                this.Character.Golem.SellShop.Clear();
                this.Character.Golem.BuyShop.Clear();
                this.needSendGolem = false;
            }
            this.SendFGEvent();
            this.SendDungeonEvent();
            if (this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                this.SendPet(this.Character.Inventory.Equipments[EnumEquipSlot.PET]);
            Singleton<StatusFactory>.Instance.CalcStatus(this.Character);
            this.SendEquip();
            this.Character.e.OnActorChangeBuff((SagaDB.Actor.Actor)this.Character);
            if (this.Character.PossessionTarget != 0U)
            {
                SagaDB.Actor.Actor actor = this.Map.GetActor(this.Character.PossessionTarget);
                if (actor != null)
                    this.Character.e.OnActorAppears(actor);
            }
            foreach (SagaDB.Actor.Actor possesionedActor in this.Character.PossesionedActors)
            {
                if (possesionedActor != this.Character)
                    possesionedActor.e.OnActorAppears((SagaDB.Actor.Actor)this.Character);
            }
            this.SendQuestInfo();
            this.SendQuestPoints();
            this.SendQuestCount();
            this.SendQuestTime();
            this.SendQuestStatus();
            this.SendStamp();
            this.SendWRPRanking(this.Character);
            if (Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items.ContainsKey(this.map.ID))
            {
                if (!this.chara.Skills.ContainsKey(2457U))
                {
                    SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(2457U, (byte)1);
                    skill.NoSave = true;
                    this.chara.Skills.Add(2457U, skill);
                }
            }
            else if (this.chara.Skills.ContainsKey(2457U))
                this.chara.Skills.Remove(2457U);
            if (this.map.Info.Flag.Test(MapFlags.Dominion))
            {
                if (this.Character.WRPRanking <= 10U)
                {
                    if (!this.chara.Skills.ContainsKey(10500U))
                    {
                        SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(10500U, (byte)1);
                        skill.NoSave = true;
                        this.chara.Skills.Add(10500U, skill);
                    }
                }
                else if (this.chara.Skills.ContainsKey(10500U))
                    this.chara.Skills.Remove(10500U);
            }
            else if (this.chara.Skills.ContainsKey(10500U))
                this.chara.Skills.Remove(10500U);
            this.SendPlayerInfo();
            if (this.map.Info.Flag.Test(MapFlags.Wrp))
                this.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.WRP_ENTER);
            this.SendPartyInfo();
            this.SendRingInfo(SSMG_RING_INFO.Reason.NONE);
            Singleton<PartyManager>.Instance.UpdateMemberPosition(this.Character.Party, this.Character);
            if (this.map.IsDungeon && this.Character.Party != null)
                Singleton<PartyManager>.Instance.UpdateMemberDungeonPosition(this.Character.Party, this.Character);
            if (FactoryList<TheaterFactory, Movie>.Instance.Items.ContainsKey(this.map.ID))
            {
                Movie nextMovie = FactoryList<TheaterFactory, Movie>.Instance.GetNextMovie(this.map.ID);
                if (nextMovie != null)
                {
                    this.netIO.SendPacket((Packet)new SSMG_THEATER_INFO()
                    {
                        MessageType = SSMG_THEATER_INFO.Type.MESSAGE,
                        Message = Singleton<LocalManager>.Instance.Strings.THEATER_WELCOME
                    });
                    this.netIO.SendPacket((Packet)new SSMG_THEATER_INFO()
                    {
                        MessageType = SSMG_THEATER_INFO.Type.MOVIE_ADDRESS,
                        Message = nextMovie.URL
                    });
                }
            }
            if (!this.Character.Tasks.ContainsKey("QuestTime"))
            {
                QuestTime questTime = new QuestTime(this);
                this.Character.Tasks.Add("QuestTime", (MultiRunTask)questTime);
                questTime.Activate();
            }
            if (!this.Character.Tasks.ContainsKey("AutoSave"))
            {
                AutoSave autoSave = new AutoSave(this.Character);
                this.Character.Tasks.Add("AutoSave", (MultiRunTask)autoSave);
                autoSave.Activate();
            }
            if (this.Map.Info.Healing)
            {
                if (!this.Character.Tasks.ContainsKey("CityRecover"))
                {
                    CityRecover cityRecover = new CityRecover(this);
                    this.Character.Tasks.Add("CityRecover", (MultiRunTask)cityRecover);
                    cityRecover.Activate();
                }
            }
            else if (this.Character.Tasks.ContainsKey("CityRecover"))
            {
                this.Character.Tasks["CityRecover"].Deactivate();
                this.Character.Tasks.Remove("CityRecover");
            }
            if (this.Map.Info.Cold || this.map.Info.Hot || this.map.Info.Wet)
            {
                if (!this.Character.Tasks.ContainsKey("CityDown"))
                {
                    CityDown cityDown = new CityDown(this);
                    this.Character.Tasks.Add("CityDown", (MultiRunTask)cityDown);
                    cityDown.Activate();
                }
            }
            else if (this.Character.Tasks.ContainsKey("CityDown"))
            {
                this.Character.Tasks["CityDown"].Deactivate();
                this.Character.Tasks.Remove("CityDown");
            }
            if (this.Character.PossessionTarget != 0U && !this.Character.Tasks.ContainsKey("PossessionRecover"))
            {
                PossessionRecover possessionRecover = new PossessionRecover(this);
                this.Character.Tasks.Add("PossessionRecover", (MultiRunTask)possessionRecover);
                possessionRecover.Activate();
            }
            this.netIO.SendPacket((Packet)new SSMG_LOGIN_FINISHED());
            this.state = MapClient.SESSION_STATE.LOADED;
            this.SendNPCStates();
        }

        /// <summary>
        /// The OnLogout.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_LOGOUT"/>.</param>
        public void OnLogout(CSMG_LOGOUT p)
        {
            this.PossessionPrepareCancel();
            this.golemLogout = p.Result == (CSMG_LOGOUT.Results)1;
            this.netIO.SendPacket((Packet)new SSMG_LOGOUT()
            {
                Result = (SSMG_LOGOUT.Results)p.Result
            });
        }

        /// <summary>
        /// The OnItemWareClose.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_WARE_CLOSE"/>.</param>
        public void OnItemWareClose(CSMG_ITEM_WARE_CLOSE p)
        {
            this.currentWarehouse = WarehousePlace.Current;
        }

        /// <summary>
        /// The OnItemWareGet.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_WARE_GET"/>.</param>
        public void OnItemWareGet(CSMG_ITEM_WARE_GET p)
        {
            int num = 0;
            if (this.currentWarehouse == WarehousePlace.Current)
            {
                num = 1;
            }
            else
            {
                SagaDB.Item.Item obj1 = this.Character.Inventory.GetItem(this.currentWarehouse, p.InventoryID);
                if (obj1 == null)
                    num = -2;
                else if ((int)obj1.Stack < (int)p.Count)
                {
                    num = -3;
                }
                else
                {
                    switch (this.Character.Inventory.DeleteWareItem(this.currentWarehouse, obj1.Slot, (int)p.Count))
                    {
                        case InventoryDeleteResult.ALL_DELETED:
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                            {
                                InventorySlot = obj1.Slot
                            });
                            SagaDB.Item.Item obj2 = obj1.Clone();
                            obj2.Stack = p.Count;
                            Logger.LogItemGet(Logger.EventType.ItemWareGet, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj1.BaseData.name + "(" + (object)obj1.ItemID + ")", string.Format("WareGet Count:{0}", (object)obj1.Stack), false);
                            this.AddItem(obj2, false);
                            this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_WARE_GET, (object)obj1.BaseData.name, (object)p.Count));
                            break;
                        case InventoryDeleteResult.STACK_UPDATED:
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                            {
                                InventorySlot = obj1.Slot,
                                Stack = obj1.Stack
                            });
                            SagaDB.Item.Item obj3 = obj1.Clone();
                            obj3.Stack = p.Count;
                            Logger.LogItemGet(Logger.EventType.ItemWareGet, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj1.BaseData.name + "(" + (object)obj1.ItemID + ")", string.Format("WareGet Count:{0}", (object)obj1.Stack), false);
                            this.AddItem(obj3, false);
                            this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_WARE_GET, (object)obj1.BaseData.name, (object)p.Count));
                            break;
                        case InventoryDeleteResult.ERROR:
                            num = -99;
                            break;
                    }
                }
            }
            this.netIO.SendPacket((Packet)new SSMG_ITEM_WARE_GET_RESULT()
            {
                Result = num
            });
        }

        /// <summary>
        /// The OnItemWarePut.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_ITEM_WARE_PUT"/>.</param>
        public void OnItemWarePut(CSMG_ITEM_WARE_PUT p)
        {
            int num = 0;
            if (this.currentWarehouse == WarehousePlace.Current)
            {
                num = -1;
            }
            else
            {
                SagaDB.Item.Item obj1 = this.Character.Inventory.GetItem(p.InventoryID);
                if (obj1 == null)
                    num = -2;
                else if ((int)obj1.Stack < (int)p.Count)
                    num = -3;
                else if (this.Character.Inventory.WareTotalCount >= Singleton<Configuration>.Instance.WarehouseLimit)
                {
                    num = -4;
                }
                else
                {
                    Logger.LogItemLost(Logger.EventType.ItemWareLost, this.Character.Name + "(" + (object)this.Character.CharID + ")", obj1.BaseData.name + "(" + (object)obj1.ItemID + ")", string.Format("WarePut Count:{0}", (object)p.Count), false);
                    this.DeleteItem(p.InventoryID, p.Count, false);
                    SagaDB.Item.Item obj2 = obj1.Clone();
                    obj2.Stack = p.Count;
                    switch (this.Character.Inventory.AddWareItem(this.currentWarehouse, obj2))
                    {
                        case InventoryAddResult.NEW_INDEX:
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_WARE_ITEM()
                            {
                                Place = WarehousePlace.Current,
                                InventorySlot = obj2.Slot,
                                Item = obj2
                            });
                            break;
                        case InventoryAddResult.STACKED:
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                            {
                                InventorySlot = obj1.Slot,
                                Stack = obj1.Stack
                            });
                            break;
                        case InventoryAddResult.MIXED:
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_COUNT_UPDATE()
                            {
                                InventorySlot = obj1.Slot,
                                Stack = obj1.Stack
                            });
                            this.netIO.SendPacket((Packet)new SSMG_ITEM_WARE_ITEM()
                            {
                                InventorySlot = this.Character.Inventory.LastItem.Slot,
                                Item = this.Character.Inventory.LastItem,
                                Place = WarehousePlace.Current
                            });
                            break;
                    }
                    this.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_WARE_PUT, (object)obj1.BaseData.name, (object)p.Count));
                }
            }
            this.netIO.SendPacket((Packet)new SSMG_ITEM_WARE_PUT_RESULT()
            {
                Result = num
            });
        }

        /// <summary>
        /// The SendWareItems.
        /// </summary>
        /// <param name="place">The place<see cref="WarehousePlace"/>.</param>
        public void SendWareItems(WarehousePlace place)
        {
            this.netIO.SendPacket((Packet)new SSMG_ITEM_WARE_HEADER()
            {
                Place = place,
                CountCurrent = this.Character.Inventory.WareHouse[place].Count,
                CountAll = this.Character.Inventory.WareTotalCount,
                CountMax = Singleton<Configuration>.Instance.WarehouseLimit
            });
            foreach (WarehousePlace key in this.Character.Inventory.WareHouse.Keys)
            {
                if (key != WarehousePlace.Current)
                {
                    foreach (SagaDB.Item.Item obj in this.Character.Inventory.WareHouse[key])
                    {
                        if (obj.Refine == (ushort)0)
                            obj.Clear();
                        this.netIO.SendPacket((Packet)new SSMG_ITEM_WARE_ITEM()
                        {
                            Item = obj,
                            InventorySlot = obj.Slot,
                            Place = (key != place ? key : WarehousePlace.Current)
                        });
                    }
                }
            }
            this.netIO.SendPacket((Packet)new SSMG_ITEM_WARE_FOOTER());
        }

        /// <summary>
        /// Defines the SESSION_STATE.
        /// </summary>
        public enum SESSION_STATE
        {
            /// <summary>
            /// Defines the LOGIN.
            /// </summary>
            LOGIN,

            /// <summary>
            /// Defines the AUTHENTIFICATED.
            /// </summary>
            AUTHENTIFICATED,

            /// <summary>
            /// Defines the REDIRECTING.
            /// </summary>
            REDIRECTING,

            /// <summary>
            /// Defines the DISCONNECTED.
            /// </summary>
            DISCONNECTED,

            /// <summary>
            /// Defines the LOADING.
            /// </summary>
            LOADING,

            /// <summary>
            /// Defines the LOADED.
            /// </summary>
            LOADED,
        }
    }
}
