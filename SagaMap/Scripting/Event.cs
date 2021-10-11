namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaDB.DEMIC;
    using SagaDB.ECOShop;
    using SagaDB.FGarden;
    using SagaDB.Iris;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Npc;
    using SagaDB.Quests;
    using SagaDB.Skill;
    using SagaDB.Synthese;
    using SagaDB.Theater;
    using SagaDB.Treasure;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Dungeon;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Client;
    using SagaMap.Packets.Server;
    using SagaMap.PC;
    using SagaMap.Skill;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// Defines the <see cref="Event" />.
    /// </summary>
    public abstract class Event
    {
        /// <summary>
        /// Defines the buyLimit.
        /// </summary>
        private uint buyLimit = 2000;

        /// <summary>
        /// Defines the alreadyHasQuest.
        /// </summary>
        protected string alreadyHasQuest = "";

        /// <summary>
        /// Defines the gotNormalQuest.
        /// </summary>
        protected string gotNormalQuest = "";

        /// <summary>
        /// Defines the gotTransportQuest.
        /// </summary>
        protected string gotTransportQuest = "";

        /// <summary>
        /// Defines the questCompleted.
        /// </summary>
        protected string questCompleted = "";

        /// <summary>
        /// Defines the transport.
        /// </summary>
        protected string transport = "";

        /// <summary>
        /// Defines the questCanceled.
        /// </summary>
        protected string questCanceled = "";

        /// <summary>
        /// Defines the questFailed.
        /// </summary>
        protected string questFailed = "";

        /// <summary>
        /// Defines the leastQuestPoint.
        /// </summary>
        protected int leastQuestPoint = 1;

        /// <summary>
        /// Defines the notEnoughQuestPoint.
        /// </summary>
        protected string notEnoughQuestPoint = "";

        /// <summary>
        /// Defines the questTooEasy.
        /// </summary>
        protected string questTooEasy = "";

        /// <summary>
        /// Defines the questTooHard.
        /// </summary>
        protected string questTooHard = "";

        /// <summary>
        /// Defines the questTransportSource.
        /// </summary>
        protected string questTransportSource = "";

        /// <summary>
        /// Defines the questTransportCompleteSrc.
        /// </summary>
        protected string questTransportCompleteSrc = "";

        /// <summary>
        /// Defines the questTransportDest.
        /// </summary>
        protected string questTransportDest = "";

        /// <summary>
        /// Defines the questTransportCompleteDest.
        /// </summary>
        protected string questTransportCompleteDest = "";

        /// <summary>
        /// Defines the goods.
        /// </summary>
        private List<uint> goods = new List<uint>();

        /// <summary>
        /// Defines the eventID.
        /// </summary>
        private uint eventID;

        /// <summary>
        /// Defines the currentPC.
        /// </summary>
        private ActorPC currentPC;

        /// <summary>
        /// Gets or sets the CurrentPC.
        /// </summary>
        public ActorPC CurrentPC
        {
            get
            {
                return this.currentPC;
            }
            set
            {
                this.currentPC = value;
            }
        }

        /// <summary>
        /// Gets the Goods.
        /// </summary>
        public List<uint> Goods
        {
            get
            {
                return this.goods;
            }
        }

        /// <summary>
        /// Gets or sets the BuyLimit
        /// NPC所收购的物品价值的上限
        /// <remarks>默认为2000</remarks>.
        /// </summary>
        protected uint BuyLimit
        {
            get
            {
                return this.buyLimit;
            }
            set
            {
                this.buyLimit = value;
            }
        }

        /// <summary>
        /// Gets the SStr.
        /// </summary>
        protected VariableHolder<string, string> SStr
        {
            get
            {
                return Singleton<ScriptManager>.Instance.VariableHolder.AStr;
            }
        }

        /// <summary>
        /// Gets the SInt.
        /// </summary>
        protected VariableHolder<string, int> SInt
        {
            get
            {
                return Singleton<ScriptManager>.Instance.VariableHolder.AInt;
            }
        }

        /// <summary>
        /// Gets the SMask.
        /// </summary>
        protected VariableHolderA<string, BitMask> SMask
        {
            get
            {
                return Singleton<ScriptManager>.Instance.VariableHolder.AMask;
            }
        }

        /// <summary>
        /// Gets or sets the EventID.
        /// </summary>
        public uint EventID
        {
            get
            {
                return this.eventID;
            }
            set
            {
                this.eventID = value;
            }
        }

        /// <summary>
        /// The GetMapClient.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="MapClient"/>.</returns>
        private MapClient GetMapClient(ActorPC pc)
        {
            return ((PCEventHandler)pc.e).Client;
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            if (Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(this.eventID))
                return Factory<NPCFactory, NPC>.Instance.Items[this.eventID].Name + "(" + this.eventID.ToString() + ")";
            return base.ToString();
        }

        /// <summary>
        /// The HandleQuest.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="groupID">NPC的任务组ID.</param>
        protected void HandleQuest(ActorPC pc, uint groupID)
        {
            MapClient mapClient = this.GetMapClient(pc);
            if (pc.Quest != null)
            {
                if (pc.Quest.Status == QuestStatus.OPEN)
                {
                    if (pc.Quest.QuestType == QuestType.GATHER && this.CountItem(pc, pc.Quest.Detail.ObjectID1) > 0)
                    {
                        if (pc.Quest.QuestType == QuestType.GATHER)
                            this.Say(pc, (ushort)131, this.transport);
                        foreach (SagaDB.Item.Item obj in this.NPCTrade(pc))
                        {
                            if ((int)obj.ItemID == (int)pc.Quest.Detail.ObjectID1)
                                pc.Quest.CurrentCount1 += (int)obj.Stack;
                            if (pc.Quest.CurrentCount1 > pc.Quest.Detail.Count1)
                                pc.Quest.CurrentCount1 = pc.Quest.Detail.Count1;
                            if ((int)obj.ItemID == (int)pc.Quest.Detail.ObjectID2)
                                pc.Quest.CurrentCount2 += (int)obj.Stack;
                            if (pc.Quest.CurrentCount2 > pc.Quest.Detail.Count2)
                                pc.Quest.CurrentCount2 = pc.Quest.Detail.Count2;
                            if ((int)obj.ItemID == (int)pc.Quest.Detail.ObjectID3)
                                pc.Quest.CurrentCount3 += (int)obj.Stack;
                            if (pc.Quest.CurrentCount3 > pc.Quest.Detail.Count3)
                                pc.Quest.CurrentCount3 = pc.Quest.Detail.Count3;
                        }
                        mapClient.SendQuestCount();
                    }
                    else
                    {
                        this.Say(pc, (ushort)131, this.alreadyHasQuest);
                        if (this.Select(pc, Singleton<LocalManager>.Instance.Strings.QUEST_HOW_TO_DO, "", new string[2]
                        {
              Singleton<LocalManager>.Instance.Strings.QUEST_NOT_CANCEL,
              Singleton<LocalManager>.Instance.Strings.QUEST_CANCEL
                        }) == 2)
                        {
                            if (pc.Quest.Detail.DungeonID != 0U && pc.DungeonID != 0U)
                                Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.GetDungeon(pc.DungeonID).Destory(DestroyType.QuestCancel);
                            this.Say(pc, (ushort)131, this.questCanceled);
                            this.Say(pc, (ushort)131, Singleton<LocalManager>.Instance.Strings.QUEST_CANCELED);
                            this.PlaySound(pc, 4007U, false, 100U, (byte)50);
                            if (pc.Fame > 0U)
                                --pc.Fame;
                            pc.Quest.Status = QuestStatus.FAILED;
                            this.OnQuestUpdate(pc, pc.Quest);
                            pc.Quest = (Quest)null;
                            mapClient.SendQuestDelete();
                            return;
                        }
                    }
                }
                if (pc.Quest.Status == QuestStatus.COMPLETED)
                {
                    if (pc.Quest.Detail.DungeonID != 0U && pc.DungeonID != 0U)
                        Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.GetDungeon(pc.DungeonID).Destory(DestroyType.QuestCancel);
                    this.Say(pc, (ushort)131, this.questCompleted);
                    this.Say(pc, (ushort)131, Singleton<LocalManager>.Instance.Strings.QUEST_REWARDED);
                    this.PlaySound(pc, 4006U, false, 100U, (byte)50);
                    Singleton<ExperienceManager>.Instance.ApplyExp(pc, pc.Quest.Detail.EXP, pc.Quest.Detail.JEXP, 1f);
                    pc.Gold += (int)((double)pc.Quest.Detail.Gold * (double)Singleton<Configuration>.Instance.QuestGoldRate / 100.0);
                    pc.CP += pc.Quest.Detail.CP;
                    ++pc.EP;
                    if (pc.EP > pc.MaxEP)
                        pc.EP = pc.MaxEP;
                    if (pc.Quest.Difficulty(pc) != QuestDifficulty.TOO_EASY)
                        pc.Fame += pc.Quest.Detail.Fame;
                    if (pc.Ring != null)
                    {
                        pc.Ring.Fame += pc.Quest.Detail.Fame;
                        Singleton<RingManager>.Instance.UpdateRingInfo(pc.Ring, SSMG_RING_INFO.Reason.UPDATED);
                    }
                    if (pc.Quest.Detail.RewardItem != 0U)
                        this.GiveItem(pc, pc.Quest.Detail.RewardItem, (ushort)pc.Quest.Detail.RewardCount);
                    this.OnQuestUpdate(pc, pc.Quest);
                    pc.Quest = (Quest)null;
                    mapClient.SendQuestDelete();
                    mapClient.SendPlayerInfo();
                }
                else
                {
                    if (pc.Quest.Status != QuestStatus.FAILED)
                        return;
                    if (pc.Quest.Detail.DungeonID != 0U && pc.DungeonID != 0U)
                        Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.GetDungeon(pc.DungeonID).Destory(DestroyType.QuestCancel);
                    this.Say(pc, (ushort)131, this.questFailed);
                    this.Say(pc, (ushort)131, Singleton<LocalManager>.Instance.Strings.QUEST_FAILED);
                    this.PlaySound(pc, 4007U, false, 100U, (byte)50);
                    if (pc.Fame > 0U)
                        --pc.Fame;
                    this.OnQuestUpdate(pc, pc.Quest);
                    pc.Quest = (Quest)null;
                    mapClient.SendQuestDelete();
                }
            }
            else if ((int)pc.QuestRemaining < this.leastQuestPoint)
            {
                this.Say(pc, (ushort)131, this.notEnoughQuestPoint);
            }
            else
            {
                Quest quest = this.SendQuestList(pc, groupID);
                if (quest != null)
                {
                    if ((int)pc.QuestRemaining < (int)quest.Detail.RequiredQuestPoint)
                    {
                        this.Say(pc, (ushort)131, this.notEnoughQuestPoint);
                    }
                    else
                    {
                        QuestDifficulty questDifficulty = quest.Difficulty(pc);
                        if (questDifficulty == QuestDifficulty.TOO_EASY || questDifficulty == QuestDifficulty.TOO_HARD)
                        {
                            if (questDifficulty == QuestDifficulty.TOO_EASY)
                                this.Say(pc, (ushort)131, this.questTooEasy);
                            else
                                this.Say(pc, (ushort)131, this.questTooHard);
                            if (this.Select(pc, Singleton<LocalManager>.Instance.Strings.QUEST_IF_TAKE_QUEST, "", new string[2]
                            {
                Singleton<LocalManager>.Instance.Strings.QUEST_TAKE,
                Singleton<LocalManager>.Instance.Strings.QUEST_NOT_TAKE
                            }) == 1)
                            {
                                if (quest.QuestType != QuestType.GATHER)
                                    this.Say(pc, (ushort)131, this.gotNormalQuest);
                                else
                                    this.Say(pc, (ushort)131, this.gotTransportQuest);
                                this.QuestActivate(pc, quest);
                                pc.QuestRemaining -= (ushort)quest.Detail.RequiredQuestPoint;
                                mapClient.SendQuestPoints();
                            }
                        }
                        else if (this.CanTakeQuest(pc, quest))
                        {
                            if (quest.QuestType != QuestType.GATHER)
                                this.Say(pc, (ushort)131, this.gotNormalQuest);
                            else
                                this.Say(pc, (ushort)131, this.gotTransportQuest);
                            this.QuestActivate(pc, quest);
                            pc.QuestRemaining -= (ushort)quest.Detail.RequiredQuestPoint;
                            mapClient.SendQuestPoints();
                            this.OnQuestUpdate(pc, quest);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The CanTakeQuest.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="quest">任务.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public virtual bool CanTakeQuest(ActorPC pc, Quest quest)
        {
            return true;
        }

        /// <summary>
        /// The OnQuestUpdate.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="quest">任务.</param>
        public virtual void OnQuestUpdate(ActorPC pc, Quest quest)
        {
        }

        /// <summary>
        /// The SendQuestList.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="groupID">任务组ID.</param>
        /// <returns>玩家选择的任务.</returns>
        private Quest SendQuestList(ActorPC pc, uint groupID)
        {
            byte lv = 1;
            lv = !Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.Dominion) ? pc.Level : pc.DominionLevel;
            List<QuestInfo> list = Factory<QuestFactory, QuestInfo>.Instance.Items.Values.Where<QuestInfo>((Func<QuestInfo, bool>)(q => (int)q.GroupID == (int)groupID && ((int)lv >= (int)q.MinLevel && (int)lv <= (int)q.MaxLevel || q.MinLevel == byte.MaxValue) && ((pc.Job == q.Job || q.Job == PC_JOB.NONE) && (pc.JobType == q.JobType || q.JobType == JobType.NOVICE) && (pc.Race == q.Race || q.Race == PC_RACE.NONE)) && (pc.Gender == q.Gender || q.Gender == PC_GENDER.NONE))).ToList<QuestInfo>();
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.questID = 0U;
            mapClient.SendQuestList(list);
            ClientManager.LeaveCriticalArea();
            while (mapClient.questID == 0U)
                Thread.Sleep(500);
            ClientManager.EnterCriticalArea();
            if (Factory<QuestFactory, QuestInfo>.Instance.Items.ContainsKey(mapClient.questID) && list.Contains(Factory<QuestFactory, QuestInfo>.Instance.Items[mapClient.questID]))
                return new Quest(mapClient.questID);
            return (Quest)null;
        }

        /// <summary>
        /// The QuestActivate.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="quest">任务.</param>
        private void QuestActivate(ActorPC pc, Quest quest)
        {
            quest.Status = QuestStatus.OPEN;
            quest.EndTime = quest.Detail.TimeLimit < 0 ? new DateTime(9999, 1, 1) : DateTime.Now + new TimeSpan(0, quest.Detail.TimeLimit, 0);
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.Character.Quest = quest;
            mapClient.SendQuestInfo();
            mapClient.SendQuestWindow();
        }

        /// <summary>
        /// The Warp.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="mapID">地图ID.</param>
        /// <param name="x">X坐标.</param>
        /// <param name="y">Y坐标.</param>
        protected void Warp(ActorPC pc, uint mapID, byte x, byte y)
        {
            MapClient mapClient = this.GetMapClient(pc);
            if (!Singleton<Configuration>.Instance.HostedMaps.Contains(mapID))
                return;
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            if (mapClient.Character.Marionette != null)
                mapClient.MarionetteDeactivate();
            mapClient.Map.SendActorToMap((SagaDB.Actor.Actor)pc, mapID, Global.PosX8to16(x, map.Width), Global.PosY8to16(y, map.Height));
        }

        /// <summary>
        /// The Say.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="motion">NPC动作.</param>
        /// <param name="message">消息内容.</param>
        protected void Say(ActorPC pc, ushort motion, string message)
        {
            if (Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(this.eventID))
                this.Say(pc, motion, message, Factory<NPCFactory, NPC>.Instance.Items[this.eventID].Name);
            else
                this.Say(pc, motion, message, "");
        }

        /// <summary>
        /// The Say.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="motion">NPC动作.</param>
        /// <param name="message">消息内容.</param>
        /// <param name="title">消息标题.</param>
        protected void Say(ActorPC pc, ushort motion, string message, string title)
        {
            this.Say(pc, this.EventID, motion, message, title);
        }

        /// <summary>
        /// The Say.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="npcID">NPC ID.</param>
        /// <param name="motion">动作.</param>
        /// <param name="message">信息.</param>
        protected void Say(ActorPC pc, uint npcID, ushort motion, string message)
        {
            if (Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(npcID))
                this.Say(pc, npcID, motion, message, Factory<NPCFactory, NPC>.Instance.Items[npcID].Name);
            else
                this.Say(pc, npcID, motion, message, "");
        }

        /// <summary>
        /// The Say.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="npcID">NPCID.</param>
        /// <param name="motion">NPC动作.</param>
        /// <param name="message">消息内容.</param>
        /// <param name="title">消息标题.</param>
        protected void Say(ActorPC pc, uint npcID, ushort motion, string message, string title)
        {
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.SendNPCMessageStart();
            string str = message;
            char[] chArray = new char[1] { ';' };
            foreach (string message1 in str.Split(chArray))
            {
                if (!(message1 == ""))
                    mapClient.SendNPCMessage(npcID, message1, motion, title);
            }
            mapClient.SendNPCMessageEnd();
        }

        /// <summary>
        /// The Wait.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="wait">延迟事件，单位为毫秒.</param>
        protected void Wait(ActorPC pc, uint wait)
        {
            this.GetMapClient(pc).SendNPCWait(wait);
        }

        /// <summary>
        /// The Select.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="title">标题.</param>
        /// <param name="confirm">确认信息，可以为空字符串.</param>
        /// <param name="options">选项.</param>
        /// <returns>玩家选择的结果，第一个选项为1.</returns>
        protected int Select(ActorPC pc, string title, string confirm, params string[] options)
        {
            return this.Select(pc, title, confirm, false, options);
        }

        /// <summary>
        /// The Select.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="title">标题.</param>
        /// <param name="confirm">确认信息，可以为空字符串.</param>
        /// <param name="canCancel">是否可取消.</param>
        /// <param name="options">选项.</param>
        /// <returns>玩家选择的结果，第一个选项为1.</returns>
        protected int Select(ActorPC pc, string title, string confirm, bool canCancel, params string[] options)
        {
            SSMG_NPC_SELECT ssmgNpcSelect = new SSMG_NPC_SELECT();
            ssmgNpcSelect.SetSelect(title, confirm, options, canCancel);
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.npcSelectResult = -1;
            mapClient.netIO.SendPacket((Packet)ssmgNpcSelect);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.npcSelectResult == -1)
                Thread.Sleep(500);
            if (blocked)
                ClientManager.EnterCriticalArea();
            return mapClient.npcSelectResult;
        }

        /// <summary>
        /// The AddGoods.
        /// </summary>
        /// <param name="goods">道具ID.</param>
        protected void AddGoods(params uint[] goods)
        {
            foreach (uint good in goods)
            {
                if (this.goods.Count == 12)
                    Logger.ShowWarning(this.ToString() + ":Maximal shop items(12) reached, skiping");
                else
                    this.goods.Add(good);
            }
        }

        /// <summary>
        /// The OpenShopBuy.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="shopID">商店ID.</param>
        protected void OpenShopBuy(ActorPC pc, uint shopID)
        {
            MapClient mapClient = this.GetMapClient(pc);
            Shop shop = Factory<ShopFactory, Shop>.Instance.Items[shopID];
            SSMG_NPC_SHOP_BUY ssmgNpcShopBuy = new SSMG_NPC_SHOP_BUY(shop.Goods.Count);
            ssmgNpcShopBuy.Rate = shop.SellRate;
            ssmgNpcShopBuy.Goods = shop.Goods;
            switch (shop.ShopType)
            {
                case ShopType.None:
                    ssmgNpcShopBuy.Gold = (uint)pc.Gold;
                    ssmgNpcShopBuy.Bank = pc.Account.Bank;
                    break;
                case ShopType.CP:
                    ssmgNpcShopBuy.Gold = pc.CP;
                    break;
                case ShopType.ECoin:
                    ssmgNpcShopBuy.Gold = pc.ECoin;
                    break;
            }
            ssmgNpcShopBuy.Type = shop.ShopType;
            mapClient.netIO.SendPacket((Packet)ssmgNpcShopBuy);
            mapClient.npcShopClosed = false;
            mapClient.currentShop = shop;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!mapClient.npcShopClosed)
                Thread.Sleep(500);
            if (blocked)
                ClientManager.EnterCriticalArea();
            mapClient.currentShop = (Shop)null;
        }

        /// <summary>
        /// The OpenShopBuy.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void OpenShopBuy(ActorPC pc)
        {
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.netIO.SendPacket((Packet)new SSMG_NPC_SHOP_BUY(this.goods.Count)
            {
                Rate = (100U + (uint)pc.Status.buy_rate),
                Goods = this.goods,
                Gold = (uint)pc.Gold,
                Bank = pc.Account.Bank
            });
            mapClient.npcShopClosed = false;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!mapClient.npcShopClosed)
                Thread.Sleep(500);
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The OpenShopSell.
        /// </summary>
        /// <param name="pc">.</param>
        /// <param name="shopID">.</param>
        protected void OpenShopSell(ActorPC pc, uint shopID)
        {
            SSMG_NPC_SHOP_SELL ssmgNpcShopSell = new SSMG_NPC_SHOP_SELL();
            Shop shop = Factory<ShopFactory, Shop>.Instance.Items[shopID];
            MapClient mapClient = this.GetMapClient(pc);
            ssmgNpcShopSell.Rate = shop.BuyRate;
            ssmgNpcShopSell.ShopLimit = shop.BuyLimit;
            ssmgNpcShopSell.Bank = 0U;
            mapClient.netIO.SendPacket((Packet)ssmgNpcShopSell);
            mapClient.npcShopClosed = false;
            mapClient.currentShop = shop;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!mapClient.npcShopClosed)
                Thread.Sleep(500);
            if (blocked)
                ClientManager.EnterCriticalArea();
            mapClient.currentShop = (Shop)null;
        }

        /// <summary>
        /// The OpenShopSell.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void OpenShopSell(ActorPC pc)
        {
            SSMG_NPC_SHOP_SELL ssmgNpcShopSell = new SSMG_NPC_SHOP_SELL();
            MapClient mapClient = this.GetMapClient(pc);
            ssmgNpcShopSell.Rate = 10U + (uint)pc.Status.sell_rate;
            ssmgNpcShopSell.ShopLimit = this.buyLimit;
            ssmgNpcShopSell.Bank = 0U;
            mapClient.netIO.SendPacket((Packet)ssmgNpcShopSell);
            mapClient.npcShopClosed = false;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!mapClient.npcShopClosed)
                Thread.Sleep(500);
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The PlaySound.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="soundID">音效ID.</param>
        /// <param name="loop">是否循环播放.</param>
        /// <param name="volume">音量，100为满.</param>
        /// <param name="balance">声道平衡，0为左，50为中间，100为右.</param>
        protected void PlaySound(ActorPC pc, uint soundID, bool loop, uint volume, byte balance)
        {
            MapClient mapClient = this.GetMapClient(pc);
            byte loop1 = !loop ? (byte)0 : (byte)1;
            mapClient.SendNPCPlaySound(soundID, loop1, volume, balance);
        }

        /// <summary>
        /// The ShowEffect.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="effectID">特效ID.</param>
        protected void ShowEffect(ActorPC pc, uint effectID)
        {
            this.ShowEffect(pc, (SagaDB.Actor.Actor)pc, effectID);
        }

        /// <summary>
        /// The ShowEffect.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="target">NPCID.</param>
        /// <param name="effectID">特效ID.</param>
        protected void ShowEffect(ActorPC pc, uint target, uint effectID)
        {
            this.GetMapClient(pc).map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
            {
                effectID = effectID,
                actorID = target
            }, (SagaDB.Actor.Actor)pc, true);
        }

        /// <summary>
        /// The ShowEffect.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="x">X坐标.</param>
        /// <param name="y">Y坐标.</param>
        /// <param name="effectID">特效ID.</param>
        protected void ShowEffect(ActorPC pc, byte x, byte y, uint effectID)
        {
            this.GetMapClient(pc).map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
            {
                effectID = effectID,
                actorID = uint.MaxValue,
                x = x,
                y = y
            }, (SagaDB.Actor.Actor)pc, true);
        }

        /// <summary>
        /// The ShowEffect.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="target">对象.</param>
        /// <param name="effectID">特效ID.</param>
        protected void ShowEffect(ActorPC pc, SagaDB.Actor.Actor target, uint effectID)
        {
            this.ShowEffect(pc, target.ActorID, effectID);
        }

        /// <summary>
        /// The StartEvent.
        /// </summary>
        /// <param name="pc">.</param>
        /// <param name="eventID">.</param>
        protected void StartEvent(ActorPC pc, uint eventID)
        {
            this.GetMapClient(pc).SendCurrentEvent(eventID);
        }

        /// <summary>
        /// The GetMapName.
        /// </summary>
        /// <param name="mapID">地图ID.</param>
        /// <returns>.</returns>
        protected string GetMapName(uint mapID)
        {
            if (Singleton<Configuration>.Instance.HostedMaps.Contains(mapID))
                return Singleton<MapManager>.Instance.GetMap(mapID).Name;
            return "";
        }

        /// <summary>
        /// The SetHomePoint.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="mapID">地图ID.</param>
        /// <param name="x">X坐标.</param>
        /// <param name="y">Y坐标.</param>
        protected void SetHomePoint(ActorPC pc, uint mapID, byte x, byte y)
        {
            pc.SaveMap = mapID;
            pc.SaveX = x;
            pc.SaveY = y;
        }

        /// <summary>
        /// The CountItem.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="itemID">道具ID.</param>
        /// <returns>.</returns>
        protected int CountItem(ActorPC pc, uint itemID)
        {
            SagaDB.Item.Item obj = pc.Inventory.GetItem(itemID, Inventory.SearchType.ITEM_ID);
            if (obj != null)
                return (int)obj.Stack;
            return 0;
        }

        /// <summary>
        /// The GetItem.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="ID">道具ID.</param>
        /// <returns>道具清单.</returns>
        protected List<SagaDB.Item.Item> GetItem(ActorPC pc, uint ID)
        {
            List<SagaDB.Item.Item> objList = new List<SagaDB.Item.Item>();
            for (int index = 2; index < 6; ++index)
            {
                IEnumerable<SagaDB.Item.Item> collection = pc.Inventory.Items[(ContainerType)index].Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(it => (int)it.ItemID == (int)ID));
                objList.AddRange(collection);
            }
            return objList;
        }

        /// <summary>
        /// The GiveItem.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="item">道具.</param>
        protected void GiveItem(ActorPC pc, SagaDB.Item.Item item)
        {
            MapClient mapClient = this.GetMapClient(pc);
            Logger.LogItemGet(Logger.EventType.ItemNPCGet, pc.Name + "(" + (object)pc.CharID + ")", item.BaseData.name + "(" + (object)item.ItemID + ")", string.Format("ScriptGive Count:{0}", (object)item.Stack), true);
            mapClient.AddItem(item, true);
        }

        /// <summary>
        /// The GiveItem.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="itemID">道具ID.</param>
        /// <param name="count">个数.</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count)
        {
            this.GiveItem(pc, itemID, count, true, 0U);
        }

        /// <summary>
        /// The GiveItem.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="itemID">道具ID.</param>
        /// <param name="count">数量.</param>
        /// <param name="rentalMinutes">租凭分钟.</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count, int rentalMinutes)
        {
            this.GiveItem(pc, itemID, count, true, 0U, rentalMinutes);
        }

        /// <summary>
        /// The GiveItem.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="itemID">道具ID.</param>
        /// <param name="count">个数.</param>
        /// <param name="identified">是否鉴定.</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count, bool identified)
        {
            this.GiveItem(pc, itemID, count, identified, 0U);
        }

        /// <summary>
        /// The GiveItem.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="itemID">道具ID.</param>
        /// <param name="count">个数.</param>
        /// <param name="identified">是否鉴定.</param>
        /// <param name="pictID">外观ID或者画板包含的怪物ID.</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count, bool identified, uint pictID)
        {
            this.GiveItem(pc, itemID, count, identified, pictID, 0);
        }

        /// <summary>
        /// The GiveItem.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="itemID">道具ID.</param>
        /// <param name="count">个数.</param>
        /// <param name="identified">是否鉴定.</param>
        /// <param name="pictID">外观ID或者画板包含的怪物ID.</param>
        /// <param name="rentalMinutes">The rentalMinutes<see cref="int"/>.</param>
        protected void GiveItem(ActorPC pc, uint itemID, ushort count, bool identified, uint pictID, int rentalMinutes)
        {
            MapClient mapClient = this.GetMapClient(pc);
            SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(itemID);
            obj.PictID = pictID;
            if (rentalMinutes > 0)
            {
                obj.Rental = true;
                obj.RentalTime = DateTime.Now + new TimeSpan(0, rentalMinutes, 0);
            }
            if (obj.Stackable)
            {
                obj.Stack = count;
                obj.Identified = identified;
                mapClient.AddItem(obj, true);
            }
            else
            {
                for (int index = 0; index < (int)count; ++index)
                {
                    obj.Stack = (ushort)1;
                    obj.Identified = identified;
                    mapClient.AddItem(obj, true);
                }
            }
            Logger.LogItemGet(Logger.EventType.ItemNPCGet, pc.Name + "(" + (object)pc.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("ScriptGive Count:{0}", (object)count), true);
        }

        /// <summary>
        /// The ItemFreeSlotCount.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>.</returns>
        protected int ItemFreeSlotCount(ActorPC pc)
        {
            int num = 0 + this.GetItemCount(pc, ContainerType.BODY) + this.GetItemCount(pc, ContainerType.BACK_BAG) + this.GetItemCount(pc, ContainerType.RIGHT_BAG) + this.GetItemCount(pc, ContainerType.LEFT_BAG);
            if (num >= 100)
                return 0;
            return 100 - num;
        }

        /// <summary>
        /// The GetItemCount.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="container">哪一個道具欄.</param>
        /// <returns>.</returns>
        protected int GetItemCount(ActorPC pc, ContainerType container)
        {
            try
            {
                return pc.Inventory.Items[container].Count<SagaDB.Item.Item>();
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// The TakeItem.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="itemID">道具ID.</param>
        /// <param name="count">个数.</param>
        /// <returns>The <see cref="SagaDB.Item.Item"/>.</returns>
        protected SagaDB.Item.Item TakeItem(ActorPC pc, uint itemID, ushort count)
        {
            MapClient mapClient = this.GetMapClient(pc);
            Logger.LogItemLost(Logger.EventType.ItemNPCLost, pc.Name + "(" + (object)pc.CharID + ")", "(" + (object)itemID + ")", string.Format("ScriptTake Count:{0}", (object)count), true);
            return mapClient.DeleteItemID(itemID, count, true);
        }

        /// <summary>
        /// The TakeItemBySlot.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="slotID">道具SlotID.</param>
        /// <param name="count">个数.</param>
        protected void TakeItemBySlot(ActorPC pc, uint slotID, ushort count)
        {
            this.GetMapClient(pc).DeleteItem(slotID, count, true);
        }

        /// <summary>
        /// The TakeEquipment.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="eqSlot">装备部位.</param>
        /// <returns>是否成功拿走道具.</returns>
        protected bool TakeEquipment(ActorPC pc, EnumEquipSlot eqSlot)
        {
            MapClient mapClient = this.GetMapClient(pc);
            try
            {
                SagaDB.Item.Item equipment = pc.Inventory.Equipments[eqSlot];
                uint slot = equipment.Slot;
                pc.Inventory.DeleteItem(slot, 1);
                mapClient.SendEquip();
                Singleton<StatusFactory>.Instance.CalcStatus(pc);
                mapClient.SendPlayerInfo();
                mapClient.netIO.SendPacket((Packet)new SSMG_ITEM_DELETE()
                {
                    InventorySlot = slot
                });
                mapClient.SendAttackType();
                mapClient.netIO.SendPacket((Packet)new SSMG_ITEM_EQUIP()
                {
                    InventorySlot = uint.MaxValue,
                    Target = ContainerType.NONE,
                    Result = 1,
                    Range = pc.Range
                });
                mapClient.Character.Inventory.CalcPayloadVolume();
                mapClient.SendCapacity();
                mapClient.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_DELETED, (object)equipment.BaseData.name, (object)1));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// The RemoveEquipment.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="eqSlot">装备部位.</param>
        protected void RemoveEquipment(ActorPC pc, EnumEquipSlot eqSlot)
        {
            if (!pc.Inventory.Equipments.ContainsKey(eqSlot))
                return;
            MapClient mapClient = this.GetMapClient(pc);
            SagaDB.Item.Item equipment1 = pc.Inventory.Equipments[eqSlot];
            SagaDB.Item.Item equipment2 = pc.Inventory.Equipments[equipment1.EquipSlot[0]];
            CSMG_ITEM_MOVE p = new CSMG_ITEM_MOVE();
            p.data = new byte[20];
            p.Target = ContainerType.BODY;
            p.InventoryID = equipment2.Slot;
            p.Count = equipment2.Stack;
            mapClient.OnItemMove(p);
        }

        /// <summary>
        /// The ActivateMarionette.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="id">活动木偶ID.</param>
        protected void ActivateMarionette(ActorPC pc, uint id)
        {
            this.GetMapClient(pc).MarionetteActivate(id, false, true);
        }

        /// <summary>
        /// The Heal.
        /// </summary>
        /// <param name="actor">对象.</param>
        protected void Heal(SagaDB.Actor.Actor actor)
        {
            MapClient mapClient = this.GetMapClient(this.currentPC);
            actor.HP = actor.MaxHP;
            actor.MP = actor.MaxMP;
            actor.SP = actor.MaxSP;
            mapClient.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The CheckMapInstance.
        /// </summary>
        /// <param name="creator">创造者.</param>
        /// <param name="mapID">地图ID.</param>
        /// <returns>.</returns>
        protected bool CheckMapInstance(ActorPC creator, uint mapID)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            return map != null && map.IsMapInstance && (int)map.Creator.CharID == (int)creator.CharID;
        }

        /// <summary>
        /// The CreateMapInstance.
        /// </summary>
        /// <param name="template">模板地图.</param>
        /// <param name="exitMap">玩家退出时返回的地图.</param>
        /// <param name="exitX">玩家退出时返回的X坐标.</param>
        /// <param name="exitY">玩家退出时返回的Y坐标.</param>
        /// <returns>新建地图副本的ID.</returns>
        protected int CreateMapInstance(int template, uint exitMap, byte exitX, byte exitY)
        {
            return (int)Singleton<MapManager>.Instance.CreateMapInstance(this.currentPC, (uint)template, exitMap, exitX, exitY);
        }

        /// <summary>
        /// The DeleteMapInstance.
        /// </summary>
        /// <param name="id">地图ID.</param>
        /// <returns>.</returns>
        protected bool DeleteMapInstance(int id)
        {
            return Singleton<MapManager>.Instance.DeleteMapInstance((uint)id);
        }

        /// <summary>
        /// The LoadSpawnFile.
        /// </summary>
        /// <param name="mapID">地图ID.</param>
        /// <param name="spawnFile">刷怪文件.</param>
        protected void LoadSpawnFile(int mapID, string spawnFile)
        {
            Singleton<MobSpawnManager>.Instance.LoadOne(spawnFile, (uint)mapID);
        }

        /// <summary>
        /// The ChangePlayerJob.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="job">职业.</param>
        protected void ChangePlayerJob(ActorPC pc, PC_JOB job)
        {
            pc.Job = job;
            Singleton<StatusFactory>.Instance.CalcStatus(pc);
            pc.HP = pc.MaxHP;
            pc.MP = pc.MaxMP;
            pc.SP = pc.MaxSP;
            this.GetMapClient(pc).SendPlayerInfo();
        }

        /// <summary>
        /// The LearnSkill.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="skillID">技能ID.</param>
        protected void LearnSkill(ActorPC pc, uint skillID)
        {
            if (!Singleton<SkillFactory>.Instance.SkillList(pc.Job).ContainsKey(skillID))
                return;
            SagaDB.Skill.Skill skill1 = Singleton<SkillFactory>.Instance.GetSkill(skillID, (byte)1);
            byte skill2 = Singleton<SkillFactory>.Instance.SkillList(pc.Job)[skillID];
            if (skill1 == null)
                return;
            if (pc.Job == pc.JobBasic)
            {
                if ((int)pc.JobLevel1 < (int)skill2)
                    return;
                if (!pc.Skills.ContainsKey(skillID))
                    pc.Skills.Add(skillID, skill1);
            }
            if (pc.Job == pc.Job2X)
            {
                if ((int)pc.JobLevel2X < (int)skill2)
                    return;
                if (!pc.Skills2.ContainsKey(skillID))
                    pc.Skills2.Add(skillID, skill1);
            }
            if (pc.Job == pc.Job2T)
            {
                if ((int)pc.JobLevel2T < (int)skill2)
                    return;
                if (!pc.Skills2.ContainsKey(skillID))
                    pc.Skills2.Add(skillID, skill1);
            }
            Singleton<StatusFactory>.Instance.CalcStatus(pc);
            this.GetMapClient(pc).SendPlayerInfo();
        }

        /// <summary>
        /// The NPCTrade.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>玩家交易的道具.</returns>
        protected List<SagaDB.Item.Item> NPCTrade(ActorPC pc)
        {
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.npcTrade = true;
            string name = "";
            if (Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(this.eventID))
                name = Factory<NPCFactory, NPC>.Instance.Items[this.eventID].Name;
            mapClient.SendTradeStartNPC(name);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.npcTrade)
                Thread.Sleep(500);
            if (blocked)
                ClientManager.EnterCriticalArea();
            List<SagaDB.Item.Item> npcTradeItem = mapClient.npcTradeItem;
            mapClient.npcTradeItem = (List<SagaDB.Item.Item>)null;
            return npcTradeItem;
        }

        /// <summary>
        /// The CheckInventory.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="itemID">道具ID.</param>
        /// <param name="amount">个数.</param>
        /// <returns>.</returns>
        protected bool CheckInventory(ActorPC pc, uint itemID, int amount)
        {
            SagaDB.Item.Item.ItemData itemData = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.Items[itemID];
            int num1 = (int)itemData.volume * amount;
            int num2 = (int)itemData.weight * amount;
            return (long)pc.Inventory.Payload[ContainerType.BODY] + (long)num2 < (long)pc.Inventory.MaxPayload[ContainerType.BODY] && (long)pc.Inventory.Volume[ContainerType.BODY] + (long)num1 < (long)pc.Inventory.MaxVolume[ContainerType.BODY] && pc.Inventory.Items[ContainerType.BODY].Count < 100;
        }

        /// <summary>
        /// The SkillPointBonus.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="val">值.</param>
        protected void SkillPointBonus(ActorPC pc, byte val)
        {
            pc.SkillPoint += (ushort)val;
            this.GetMapClient(pc).SendPlayerLevel();
        }

        /// <summary>
        /// The SkillPointBonus2T.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="val">值.</param>
        protected void SkillPointBonus2T(ActorPC pc, byte val)
        {
            pc.SkillPoint2T += (ushort)val;
            this.GetMapClient(pc).SendPlayerLevel();
        }

        /// <summary>
        /// The SkillPointBonus2X.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="val">值.</param>
        protected void SkillPointBonus2X(ActorPC pc, byte val)
        {
            pc.SkillPoint2X += (ushort)val;
            this.GetMapClient(pc).SendPlayerLevel();
        }

        /// <summary>
        /// The Revive.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="level">复活术等级.</param>
        protected void Revive(ActorPC pc, byte level)
        {
            if (level <= (byte)0)
                return;
            this.GetMapClient(pc).SendRevive(level);
        }

        /// <summary>
        /// Gets the OnlinePlayers.
        /// </summary>
        protected List<ActorPC> OnlinePlayers
        {
            get
            {
                List<ActorPC> actorPcList = new List<ActorPC>();
                foreach (MapClient client in MapClientManager.Instance.Clients)
                {
                    if (client.Character.Online)
                        actorPcList.Add(client.Character);
                }
                return actorPcList;
            }
        }

        /// <summary>
        /// The CreateItem.
        /// </summary>
        /// <param name="itemID">道具ID.</param>
        /// <param name="count">数量.</param>
        /// <param name="identified">是否鉴定.</param>
        /// <returns>道具.</returns>
        protected SagaDB.Item.Item CreateItem(uint itemID, ushort count, bool identified)
        {
            SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(itemID, identified);
            obj.Stack = count;
            return obj;
        }

        /// <summary>
        /// The CreateItemList.
        /// </summary>
        /// <param name="itemID">道具ID.</param>
        /// <returns>道具列表.</returns>
        protected List<SagaDB.Item.Item> CreateItemList(params uint[] itemID)
        {
            List<SagaDB.Item.Item> objList = new List<SagaDB.Item.Item>();
            foreach (uint num in itemID)
                objList.Add(this.CreateItem(itemID[(uint)num], (ushort)1, true));
            return objList;
        }

        /// <summary>
        /// The Navigate.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="x">X坐标.</param>
        /// <param name="y">Y坐标.</param>
        protected void Navigate(ActorPC pc, byte x, byte y)
        {
            this.GetMapClient(pc).netIO.SendPacket((Packet)new SSMG_NPC_NAVIGATION()
            {
                X = x,
                Y = y
            });
        }

        /// <summary>
        /// The NavigateCancel.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void NavigateCancel(ActorPC pc)
        {
            SSMG_NPC_NAVIGATION_CANCEL navigationCancel = new SSMG_NPC_NAVIGATION_CANCEL();
            this.GetMapClient(pc).netIO.SendPacket((Packet)navigationCancel);
        }

        /// <summary>
        /// The Synthese.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="skillID">精炼技能.</param>
        /// <param name="skillLv">技能等级.</param>
        public void Synthese(ActorPC pc, ushort skillID, byte skillLv)
        {
            List<SyntheseInfo> list = Factory<SyntheseFactory, SyntheseInfo>.Instance.Items.Values.Where<SyntheseInfo>((Func<SyntheseInfo, bool>)(c => (int)c.SkillID == (int)skillID && (int)c.SkillLv <= (int)skillLv && this.HasMaterial(pc, c))).ToList<SyntheseInfo>();
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.netIO.SendPacket((Packet)new SSMG_NPC_SYNTHESE_HEADER()
            {
                Count = (byte)list.Count
            });
            foreach (SyntheseInfo syntheseInfo in list)
                mapClient.netIO.SendPacket((Packet)new SSMG_NPC_SYNTHESE_INFO()
                {
                    Synthese = syntheseInfo
                });
            mapClient.syntheseItem = new Dictionary<uint, uint>();
            mapClient.syntheseFinished = false;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!mapClient.syntheseFinished)
                Thread.Sleep(500);
            if (blocked)
                ClientManager.EnterCriticalArea();
            bool flag1 = true;
            bool flag2 = false;
            uint pictID = 0;
            foreach (uint key in mapClient.syntheseItem.Keys)
            {
                if (Factory<SyntheseFactory, SyntheseInfo>.Instance.Items.ContainsKey(key))
                {
                    SyntheseInfo syntheseInfo = Factory<SyntheseFactory, SyntheseInfo>.Instance.Items[key];
                    foreach (ItemElement material in syntheseInfo.Materials)
                    {
                        int num1 = 0;
                        while (this.CountItem(pc, material.ID) > 0 && (long)num1 < (long)((uint)material.Count * mapClient.syntheseItem[key]))
                        {
                            int num2 = this.CountItem(pc, material.ID);
                            if (!flag2)
                                flag2 = material.ID == 10020758U;
                            if ((long)(num1 + num2) > (long)((uint)material.Count * mapClient.syntheseItem[key]))
                                num2 = (int)((long)((uint)material.Count * mapClient.syntheseItem[key]) - (long)num1);
                            SagaDB.Item.Item obj = this.TakeItem(pc, material.ID, (ushort)num2);
                            if (obj != null && flag2 && pictID == 0U)
                                pictID = obj.PictID;
                            num1 += num2;
                        }
                        if ((long)num1 < (long)((uint)material.Count * mapClient.syntheseItem[key]))
                            flag1 = false;
                    }
                    if (flag1 && (long)pc.Gold > (long)syntheseInfo.Gold)
                    {
                        for (uint index = 0; index < mapClient.syntheseItem[key]; ++index)
                        {
                            int num1 = Global.Random.Next(0, 99);
                            int num2 = 0;
                            foreach (ItemElement product in syntheseInfo.Products)
                            {
                                int num3 = num2 + product.Rate;
                                if (num1 >= num2 && num1 < num3)
                                {
                                    if (flag2 && pictID != 0U)
                                        this.GiveItem(pc, product.ID, product.Count, true, pictID);
                                    else
                                        this.GiveItem(pc, product.ID, product.Count);
                                }
                                num2 = num3;
                            }
                        }
                        pc.Gold -= (int)syntheseInfo.Gold;
                    }
                }
            }
            mapClient.SendSkillDummy((uint)skillID, skillLv);
            mapClient.netIO.SendPacket((Packet)new SSMG_NPC_SYNTHESE_RESULT()
            {
                Result = (byte)1
            });
        }

        /// <summary>
        /// The HasMaterial.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="info">The info<see cref="SyntheseInfo"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool HasMaterial(ActorPC pc, SyntheseInfo info)
        {
            foreach (ItemElement material in info.Materials)
            {
                if (this.CountItem(pc, material.ID) > 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The BankDeposit.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void BankDeposit(ActorPC pc)
        {
            MapClient mapClient = this.GetMapClient(pc);
            string s = this.InputBox(pc, string.Format(Singleton<LocalManager>.Instance.Strings.NPC_INPUT_BANK, (object)mapClient.Character.Account.Bank), InputType.Bank);
            if (s == "")
                return;
            uint num = uint.Parse(s);
            if ((long)pc.Gold < (long)num)
            {
                this.Say(pc, (ushort)131, Singleton<LocalManager>.Instance.Strings.NPC_BANK_NOT_ENOUGH_GOLD);
            }
            else
            {
                pc.Gold -= (int)num;
                pc.Account.Bank += num;
            }
        }

        /// <summary>
        /// The BankWithdraw.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void BankWithdraw(ActorPC pc)
        {
            MapClient mapClient = this.GetMapClient(pc);
            string s = this.InputBox(pc, string.Format(Singleton<LocalManager>.Instance.Strings.NPC_INPUT_BANK, (object)mapClient.Character.Account.Bank), InputType.Bank);
            if (s == "")
                return;
            uint num = uint.Parse(s);
            if (pc.Account.Bank < num)
            {
                this.Say(pc, (ushort)131, Singleton<LocalManager>.Instance.Strings.NPC_BANK_NOT_ENOUGH_GOLD);
            }
            else
            {
                pc.Account.Bank -= num;
                pc.Gold += (int)num;
            }
        }

        /// <summary>
        /// The InputBox.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="type">类型.</param>
        /// <returns>输入的内容，取消则为"".</returns>
        protected string InputBox(ActorPC pc, string title, InputType type)
        {
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.inputContent = "\0";
            mapClient.netIO.SendPacket((Packet)new SSMG_NPC_INPUTBOX()
            {
                Title = title,
                Type = type
            });
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.inputContent == "\0")
                Thread.Sleep(500);
            if (blocked)
                ClientManager.EnterCriticalArea();
            return mapClient.inputContent;
        }

        /// <summary>
        /// The OpenWareHouse.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="place">地点.</param>
        protected void OpenWareHouse(ActorPC pc, WarehousePlace place)
        {
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.currentWarehouse = place;
            this.PlaySound(pc, 2060U, false, 100U, (byte)50);
            mapClient.SendWareItems(place);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.currentWarehouse != WarehousePlace.Current)
                Thread.Sleep(500);
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The GiveRandomTreasure.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="treasureGroup">宝物组.</param>
        protected void GiveRandomTreasure(ActorPC pc, string treasureGroup)
        {
            TreasureItem randomItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem(treasureGroup);
            bool identified = Global.Random.Next(0, 99) <= 5;
            this.GiveItem(pc, randomItem.ID, (ushort)randomItem.Count, identified);
        }

        /// <summary>
        /// The GetRandomTreasure.
        /// </summary>
        /// <param name="treasureGroup">宝物组.</param>
        /// <returns>道具.</returns>
        protected SagaDB.Item.Item GetRandomTreasure(string treasureGroup)
        {
            TreasureItem randomItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem(treasureGroup);
            bool identified = Global.Random.Next(0, 99) <= 5;
            SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(randomItem.ID, identified);
            obj.Stack = (ushort)randomItem.Count;
            return obj;
        }

        /// <summary>
        /// The Identify.
        /// </summary>
        /// <param name="pc">玩家.</param>
        public void Identify(ActorPC pc)
        {
            List<SagaDB.Item.Item> list = pc.Inventory.GetContainer(ContainerType.BODY).Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(c => !c.Identified)).ToList<SagaDB.Item.Item>();
            if (list.Count > 0)
            {
                string[] strArray = new string[list.Count];
                int index = 0;
                foreach (SagaDB.Item.Item obj in list)
                {
                    strArray[index] = Event.GetItemNameByType(obj.BaseData.itemType);
                    ++index;
                }
                int num = this.Select(pc, Singleton<LocalManager>.Instance.Strings.ITEM_IDENTIFY, "", true, strArray);
                if (num == (int)byte.MaxValue)
                    return;
                MapClient mapClient = this.GetMapClient(pc);
                SagaDB.Item.Item obj1 = list[num - 1];
                obj1.Identified = true;
                mapClient.SendItemIdentify(obj1.Slot);
                mapClient.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ITEM_IDENTIFY_RESULT, (object)strArray[num - 1].Replace("\0", ""), (object)obj1.BaseData.name));
                mapClient.SendSkillDummy(903U, (byte)1);
            }
            else
            {
                MapClient mapClient = this.GetMapClient(pc);
                mapClient.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ITEM_IDENTIFY_NO_NEED);
                mapClient.SendSkillDummy(903U, (byte)1);
            }
        }

        /// <summary>
        /// The OpenTreasureBox.
        /// </summary>
        /// <param name="pc">玩家.</param>
        public void OpenTreasureBox(ActorPC pc)
        {
            this.OpenTreasureBox(pc, true, true, true);
        }

        /// <summary>
        /// The OpenTreasureBox.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="timber">The timber<see cref="bool"/>.</param>
        /// <param name="treasure">The treasure<see cref="bool"/>.</param>
        /// <param name="container">The container<see cref="bool"/>.</param>
        public void OpenTreasureBox(ActorPC pc, bool timber, bool treasure, bool container)
        {
            List<SagaDB.Item.Item> list = pc.Inventory.GetContainer(ContainerType.BODY).Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(c => c.BaseData.itemType == ItemType.TIMBER_BOX && timber || c.BaseData.itemType == ItemType.TREASURE_BOX && treasure || c.BaseData.itemType == ItemType.CONTAINER && container)).ToList<SagaDB.Item.Item>();
            if (list.Count > 0)
            {
                string[] strArray = new string[list.Count];
                int index = 0;
                foreach (SagaDB.Item.Item obj in list)
                {
                    strArray[index] = obj.BaseData.name;
                    ++index;
                }
                int num1 = this.Select(pc, Singleton<LocalManager>.Instance.Strings.ITEM_TREASURE_OPEN, "", true, strArray);
                if (num1 == (int)byte.MaxValue)
                    return;
                SagaDB.Item.Item obj1 = list[num1 - 1];
                uint num2 = obj1.BaseData.id - obj1.BaseData.iconID;
                TreasureItem randomItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem(obj1.BaseData.itemType.ToString() + num2.ToString());
                this.TakeItem(pc, obj1.ItemID, (ushort)1);
                bool identified = Global.Random.Next(0, 99) <= 5;
                this.GiveItem(pc, randomItem.ID, (ushort)randomItem.Count, identified);
            }
            else
                this.GetMapClient(pc).SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ITEM_TREASURE_NO_NEED);
        }

        /// <summary>
        /// The Fade.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="type">特效类型，Out=渐出，In=渐进.</param>
        /// <param name="effect">特效效果，黑或者白.</param>
        protected void Fade(ActorPC pc, FadeType type, FadeEffect effect)
        {
            this.GetMapClient(pc).netIO.SendPacket((Packet)new SSMG_NPC_FADE()
            {
                FadeEffect = effect,
                FadeType = type
            });
        }

        /// <summary>
        /// The JobSwitch.
        /// </summary>
        /// <param name="pc">.</param>
        /// <returns>转职结果.</returns>
        protected bool JobSwitch(ActorPC pc)
        {
            MapClient mapClient = this.GetMapClient(pc);
            SSMG_NPC_JOB_SWITCH ssmgNpcJobSwitch = new SSMG_NPC_JOB_SWITCH();
            ssmgNpcJobSwitch.Job = pc.Job;
            int num = 0;
            if (pc.Job == pc.Job2X)
            {
                ssmgNpcJobSwitch.Level = pc.JobLevel2T;
                ssmgNpcJobSwitch.Job = pc.Job2T;
                ssmgNpcJobSwitch.LevelReduced = (byte)((uint)pc.JobLevel2T - (uint)pc.JobLevel2T / 5U);
                ssmgNpcJobSwitch.PossibleReserveSkills = (ushort)((uint)pc.JobLevel2X / 10U);
                num = (int)pc.JobLevel2T / 5;
            }
            else if (pc.Job == pc.Job2T)
            {
                ssmgNpcJobSwitch.Level = pc.JobLevel2X;
                ssmgNpcJobSwitch.Job = pc.Job2X;
                ssmgNpcJobSwitch.LevelReduced = (byte)((uint)pc.JobLevel2X - (uint)pc.JobLevel2X / 5U);
                ssmgNpcJobSwitch.PossibleReserveSkills = (ushort)((uint)pc.JobLevel2T / 10U);
                num = (int)pc.JobLevel2X / 5;
            }
            ssmgNpcJobSwitch.LevelItem = Singleton<Configuration>.Instance.JobSwitchReduceItem;
            SagaDB.Item.Item obj = pc.Inventory.GetItem(Singleton<Configuration>.Instance.JobSwitchReduceItem, Inventory.SearchType.ITEM_ID);
            if (obj != null)
                ssmgNpcJobSwitch.ItemCount = (int)obj.Stack >= num ? (uint)num : (uint)obj.Stack;
            ssmgNpcJobSwitch.PossibleSkills = pc.Skills2.Values.ToList<SagaDB.Skill.Skill>();
            mapClient.netIO.SendPacket((Packet)ssmgNpcJobSwitch);
            mapClient.npcJobSwitch = true;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.npcJobSwitch)
                Thread.Sleep(500);
            if (blocked)
                ClientManager.EnterCriticalArea();
            return mapClient.npcJobSwitchRes;
        }

        /// <summary>
        /// The ResetSkill.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="job">1为1转，2为2转.</param>
        protected void ResetSkill(ActorPC pc, byte job)
        {
            this.GetMapClient(pc).ResetSkill(job);
            this.GetMapClient(pc).SendPlayerInfo();
        }

        /// <summary>
        /// The ResetStatusPoint.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void ResetStatusPoint(ActorPC pc)
        {
            this.GetMapClient(pc).ResetStatusPoint();
        }

        /// <summary>
        /// The OpenBBS.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="bbsID">揭示版ID.</param>
        /// <param name="cost">发贴费用.</param>
        protected void OpenBBS(ActorPC pc, uint bbsID, uint cost)
        {
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.bbsClose = false;
            mapClient.bbsCost = cost;
            mapClient.bbsID = bbsID;
            mapClient.netIO.SendPacket((Packet)new SSMG_COMMUNITY_BBS_OPEN()
            {
                Gold = cost
            });
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (!mapClient.bbsClose)
                Thread.Sleep(500);
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The CreateRing.
        /// </summary>
        /// <param name="pc">创建者.</param>
        /// <param name="name">军团名.</param>
        /// <returns>创建成功返回true,如果名字已存在返回false.</returns>
        protected bool CreateRing(ActorPC pc, string name)
        {
            MapClient.FromActorPC(pc);
            return Singleton<RingManager>.Instance.CreateRing(pc, name) != null;
        }

        /// <summary>
        /// The SendFGardenCreateMaterial.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="parts">.</param>
        protected void SendFGardenCreateMaterial(ActorPC pc, BitMask<FGardenParts> parts)
        {
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_FG_CREATE_MATERIAL()
            {
                Parts = parts
            });
        }

        /// <summary>
        /// The EnterFGarden.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="rope">绳子的Actor.</param>
        protected void EnterFGarden(ActorPC pc, ActorEvent rope)
        {
            if (rope.Caster.FGarden == null)
                return;
            Packet p = new Packet(10U);
            p.ID = (ushort)6371;
            p.PutUInt(pc.ActorID, (ushort)2);
            p.PutUInt(pc.MapID, (ushort)6);
            MapClient.FromActorPC(pc).netIO.SendPacket(p);
            if (rope.Caster.FGarden.MapID == 0U)
            {
                SagaMap.Map map1 = Singleton<MapManager>.Instance.GetMap(pc.MapID);
                rope.Caster.FGarden.MapID = Singleton<MapManager>.Instance.CreateMapInstance(rope.Caster, 70000000U, pc.MapID, Global.PosX16to8(pc.X, map1.Width), Global.PosY16to8(pc.Y, map1.Height));
                SagaMap.Map map2 = Singleton<MapManager>.Instance.GetMap(rope.Caster.FGarden.MapID);
                foreach (ActorFurniture actorFurniture in rope.Caster.FGarden.Furnitures[FurniturePlace.GARDEN])
                {
                    actorFurniture.e = (ActorEventHandler)new NullEventHandler();
                    map2.RegisterActor((SagaDB.Actor.Actor)actorFurniture);
                    actorFurniture.invisble = false;
                }
            }
            pc.BattleStatus = (byte)0;
            pc.Speed = (ushort)200;
            MapClient.FromActorPC(pc).SendChangeStatus();
            this.Warp(pc, rope.Caster.FGarden.MapID, (byte)6, (byte)11);
        }

        /// <summary>
        /// The ExitFGarden.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void ExitFGarden(ActorPC pc)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(pc.MapID);
            pc.Speed = (ushort)350;
            if (!map.IsMapInstance || (int)(map.ID / 10U) * 10 != 70000000)
                return;
            this.Warp(pc, map.ClientExitMap, map.ClientExitX, map.ClientExitY);
        }

        /// <summary>
        /// The EnterFGRoom.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void EnterFGRoom(ActorPC pc)
        {
            ActorPC fgardenOwner = this.GetFGardenOwner(pc);
            if (fgardenOwner == null)
                return;
            if (fgardenOwner.FGarden.RoomMapID == 0U)
            {
                fgardenOwner.FGarden.RoomMapID = Singleton<MapManager>.Instance.CreateMapInstance(fgardenOwner, 75000000U, fgardenOwner.FGarden.MapID, (byte)6, (byte)7);
                SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(fgardenOwner.FGarden.RoomMapID);
                foreach (ActorFurniture actorFurniture in fgardenOwner.FGarden.Furnitures[FurniturePlace.ROOM])
                {
                    actorFurniture.e = (ActorEventHandler)new NullEventHandler();
                    map.RegisterActor((SagaDB.Actor.Actor)actorFurniture);
                    actorFurniture.invisble = false;
                }
            }
            this.Warp(pc, fgardenOwner.FGarden.RoomMapID, (byte)5, (byte)11);
        }

        /// <summary>
        /// The ExitFGRoom.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void ExitFGRoom(ActorPC pc)
        {
            ActorPC fgardenOwner = this.GetFGardenOwner(pc);
            if (fgardenOwner == null)
                return;
            this.Warp(pc, fgardenOwner.FGarden.MapID, (byte)5, (byte)8);
        }

        /// <summary>
        /// The GetFGardenOwner.
        /// </summary>
        /// <param name="pc">.</param>
        /// <returns>.</returns>
        protected ActorPC GetFGardenOwner(ActorPC pc)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(pc.MapID);
            if (map.IsMapInstance && (int)(map.ID / 10U) * 10 == 70000000 || map.IsMapInstance && (int)(map.ID / 10U) * 10 == 75000000)
                return map.Creator;
            return (ActorPC)null;
        }

        /// <summary>
        /// The GetRopeActor.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>如果没有飞空庭或者没有召唤过飞空庭，则返回null.</returns>
        protected ActorEvent GetRopeActor(ActorPC pc)
        {
            if (pc.FGarden != null && pc.FGarden.RopeActor != null)
                return pc.FGarden.RopeActor;
            return (ActorEvent)null;
        }

        /// <summary>
        /// The ReturnRope.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void ReturnRope(ActorPC pc)
        {
            if (pc.FGarden == null)
                return;
            if (pc.FGarden.RopeActor != null)
            {
                Singleton<MapManager>.Instance.GetMap(pc.FGarden.RopeActor.MapID).DeleteActor((SagaDB.Actor.Actor)pc.FGarden.RopeActor);
                if (Singleton<ScriptManager>.Instance.Events.ContainsKey(pc.FGarden.RopeActor.EventID))
                    Singleton<ScriptManager>.Instance.Events.Remove(pc.FGarden.RopeActor.EventID);
                pc.FGarden.RopeActor = (ActorEvent)null;
            }
            if (pc.FGarden.RoomMapID != 0U)
            {
                SagaMap.Map map1 = Singleton<MapManager>.Instance.GetMap(pc.FGarden.RoomMapID);
                SagaMap.Map map2 = Singleton<MapManager>.Instance.GetMap(pc.FGarden.MapID);
                map1.ClientExitMap = map2.ClientExitMap;
                map1.ClientExitX = map2.ClientExitX;
                map1.ClientExitY = map2.ClientExitY;
                Singleton<MapManager>.Instance.DeleteMapInstance(map1.ID);
                pc.FGarden.RoomMapID = 0U;
            }
            if (pc.FGarden.MapID != 0U)
            {
                Singleton<MapManager>.Instance.DeleteMapInstance(pc.FGarden.MapID);
                pc.FGarden.MapID = 0U;
            }
        }

        /// <summary>
        /// The FGTakeOff.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="mapID">目的地ID.</param>
        /// <param name="x">X坐标.</param>
        /// <param name="y">Y坐标.</param>
        public void FGTakeOff(ActorPC pc, uint mapID, byte x, byte y)
        {
            ActorPC fgardenOwner = this.GetFGardenOwner(pc);
            if (fgardenOwner != pc)
                return;
            if (fgardenOwner.FGarden.MapID != 0U)
            {
                SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(fgardenOwner.FGarden.MapID);
                List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
                foreach (SagaDB.Actor.Actor actor in map.Actors.Values)
                {
                    if (actor.type == ActorType.PC)
                    {
                        ActorPC pc1 = (ActorPC)actor;
                        if (pc1.Online)
                        {
                            MapClient.FromActorPC(pc1).netIO.SendPacket((Packet)new SSMG_FG_TAKEOFF()
                            {
                                ActorID = pc1.ActorID,
                                MapID = fgardenOwner.FGarden.MapID
                            });
                            actorList.Add((SagaDB.Actor.Actor)pc1);
                        }
                    }
                }
                foreach (SagaDB.Actor.Actor actor in actorList)
                {
                    actor.Speed = (ushort)350;
                    MapClient.FromActorPC((ActorPC)actor).fgTakeOff = true;
                    this.Warp((ActorPC)actor, mapID, x, y);
                }
            }
            if (fgardenOwner.FGarden.RoomMapID != 0U)
            {
                SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(fgardenOwner.FGarden.RoomMapID);
                List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
                foreach (SagaDB.Actor.Actor actor in map.Actors.Values)
                {
                    if (actor.type == ActorType.PC)
                    {
                        ActorPC pc1 = (ActorPC)actor;
                        if (pc1.Online)
                        {
                            MapClient.FromActorPC(pc1).netIO.SendPacket((Packet)new SSMG_FG_TAKEOFF()
                            {
                                ActorID = pc1.ActorID,
                                MapID = fgardenOwner.FGarden.MapID
                            });
                            actorList.Add((SagaDB.Actor.Actor)pc1);
                        }
                    }
                }
                foreach (SagaDB.Actor.Actor actor in actorList)
                {
                    actor.Speed = (ushort)350;
                    MapClient.FromActorPC((ActorPC)actor).fgTakeOff = true;
                    this.Warp((ActorPC)actor, mapID, x, y);
                }
            }
            this.ReturnRope(pc);
        }

        /// <summary>
        /// The ShowUI.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="type">界面种类.</param>
        protected void ShowUI(ActorPC pc, UIType type)
        {
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_NPC_SHOW_UI()
            {
                UIType = type
            });
        }

        /// <summary>
        /// The NPCMotion.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="npcID">NPC ID.</param>
        /// <param name="motion">动作.</param>
        protected void NPCMotion(ActorPC pc, uint npcID, ushort motion)
        {
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_CHAT_MOTION()
            {
                ActorID = npcID,
                Motion = (MotionType)motion
            });
        }

        /// <summary>
        /// The NPCMotion.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="npcID">NPC ID.</param>
        /// <param name="motion">动作.</param>
        /// <param name="loop">是否重复.</param>
        /// <param name="special">未知，猜测为NPC专用.</param>
        protected void NPCMotion(ActorPC pc, uint npcID, ushort motion, bool loop, bool special)
        {
            SSMG_CHAT_MOTION ssmgChatMotion = new SSMG_CHAT_MOTION();
            ssmgChatMotion.ActorID = npcID;
            ssmgChatMotion.Motion = (MotionType)motion;
            if (loop)
                ssmgChatMotion.Loop = (byte)1;
            if (special)
                ssmgChatMotion.Special = (byte)1;
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)ssmgChatMotion);
        }

        /// <summary>
        /// The VShopOpen.
        /// </summary>
        /// <param name="pc">.</param>
        public void VShopOpen(ActorPC pc)
        {
            MapClient mapClient = this.GetMapClient(pc);
            mapClient.netIO.SendPacket((Packet)new SSMG_VSHOP_CATEGORY()
            {
                CurrentPoint = pc.VShopPoints,
                Categories = Factory<ECOShopFactory, ShopCategory>.Instance.Items
            });
            mapClient.vshopClosed = false;
        }

        /// <summary>
        /// The SetGolemType.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="type">石像类型.</param>
        public void SetGolemType(ActorPC pc, GolemType type)
        {
            if (pc.Golem != null)
                pc.Golem.GolemType = type;
            this.GetMapClient(pc).netIO.SendPacket((Packet)new SSMG_GOLEM_SET_TYPE()
            {
                GolemType = type
            });
        }

        /// <summary>
        /// The Recall.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="name">指定玩家的名稱.</param>
        public void Recall(ActorPC pc, string name)
        {
            try
            {
                MapClient mapClient1 = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => c.Character.Name == name)).First<MapClient>();
                MapClient mapClient2 = MapClient.FromActorPC(pc);
                short x = mapClient1.Character.X;
                short y = mapClient1.Character.Y;
                uint mapId = mapClient1.Character.MapID;
                mapClient2.Map.SendActorToMap((SagaDB.Actor.Actor)mapClient2.Character, mapId, x, y);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The Recall.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="charID">指定玩家的CharID.</param>
        public void Recall(ActorPC pc, uint charID)
        {
            try
            {
                MapClient mapClient1 = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => (int)c.Character.CharID == (int)charID)).First<MapClient>();
                MapClient mapClient2 = MapClient.FromActorPC(pc);
                short x = mapClient1.Character.X;
                short y = mapClient1.Character.Y;
                uint mapId = mapClient1.Character.MapID;
                mapClient2.Map.SendActorToMap((SagaDB.Actor.Actor)mapClient2.Character, mapId, x, y);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The Jump.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="charID">玩家的CharID.</param>
        public void Jump(ActorPC pc, uint charID)
        {
            try
            {
                MapClient mapClient1 = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => (int)c.Character.CharID == (int)charID)).First<MapClient>();
                MapClient mapClient2 = MapClient.FromActorPC(pc);
                short x = mapClient1.Character.X;
                short y = mapClient1.Character.Y;
                uint mapId = mapClient1.Character.MapID;
                mapClient2.Map.SendActorToMap((SagaDB.Actor.Actor)mapClient2.Character, mapId, x, y);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The Jump.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="name">指定玩家的名字.</param>
        public void Jump(ActorPC pc, string name)
        {
            try
            {
                MapClient mapClient1 = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => c.Character.Name == name)).First<MapClient>();
                MapClient mapClient2 = MapClient.FromActorPC(pc);
                short x = mapClient1.Character.X;
                short y = mapClient1.Character.Y;
                uint mapId = mapClient1.Character.MapID;
                mapClient2.Map.SendActorToMap((SagaDB.Actor.Actor)mapClient2.Character, mapId, x, y);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The Who2.
        /// </summary>
        /// <param name="pc">玩家.</param>
        public void Who2(ActorPC pc)
        {
            try
            {
                MapClient mapClient1 = MapClient.FromActorPC(pc);
                foreach (MapClient mapClient2 in MapClientManager.Instance.OnlinePlayer)
                {
                    byte num1 = Global.PosX16to8(mapClient2.Character.X, mapClient2.map.Width);
                    byte num2 = Global.PosY16to8(mapClient2.Character.Y, mapClient2.map.Height);
                    mapClient1.SendSystemMessage(mapClient2.Character.Name + "(CharID:" + (object)mapClient2.Character.CharID + ")[" + mapClient2.Map.Name + " " + num1.ToString() + "," + num2.ToString() + "]");
                }
                mapClient1.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The Hide.
        /// </summary>
        /// <param name="pc">玩家.</param>
        public void Hide(ActorPC pc)
        {
            pc.Buff.Transparent = false;
            Singleton<MapManager>.Instance.GetMap(pc.MapID).SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)pc, true);
        }

        /// <summary>
        /// The Show.
        /// </summary>
        /// <param name="pc">玩家.</param>
        public void Show(ActorPC pc)
        {
            pc.Buff.Transparent = true;
            Singleton<MapManager>.Instance.GetMap(pc.MapID).SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, (SagaDB.Actor.Actor)pc, true);
        }

        /// <summary>
        /// The GetItemTypeList.
        /// </summary>
        /// <param name="types">類型.</param>
        /// <returns>道具列表.</returns>
        public List<SagaDB.Item.Item.ItemData> GetItemTypeList(params ItemType[] types)
        {
            return Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.Items.Cast<KeyValuePair<uint, SagaDB.Item.Item.ItemData>>().Where<KeyValuePair<uint, SagaDB.Item.Item.ItemData>>((Func<KeyValuePair<uint, SagaDB.Item.Item.ItemData>, bool>)(i => ((IEnumerable<ItemType>)types).Contains<ItemType>(i.Value.itemType))).Select<KeyValuePair<uint, SagaDB.Item.Item.ItemData>, SagaDB.Item.Item.ItemData>((Func<KeyValuePair<uint, SagaDB.Item.Item.ItemData>, SagaDB.Item.Item.ItemData>)(i => i.Value)).ToList<SagaDB.Item.Item.ItemData>();
        }

        /// <summary>
        /// The GetItemTypeList.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="types">類型.</param>
        /// <returns>道具列表.</returns>
        public List<SagaDB.Item.Item> GetItemTypeList(ActorPC pc, params ItemType[] types)
        {
            return pc.Inventory.Items[ContainerType.BODY].Cast<SagaDB.Item.Item>().Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(i => ((IEnumerable<ItemType>)types).Contains<ItemType>(i.BaseData.itemType))).ToList<SagaDB.Item.Item>();
        }

        /// <summary>
        /// The CreateDungeon.
        /// </summary>
        /// <param name="pc">创建者.</param>
        /// <param name="id">遗迹ID.</param>
        /// <param name="exitMap">退出MapID.</param>
        /// <param name="exitX">退出X.</param>
        /// <param name="exitY">退出Y.</param>
        /// <returns>遗迹的ID.</returns>
        protected uint CreateDungeon(ActorPC pc, uint id, uint exitMap, byte exitX, byte exitY)
        {
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            SagaMap.Dungeon.Dungeon dungeon = Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.CreateDungeon(id, pc, exitMap, exitX, exitY);
            if (blocked)
                ClientManager.EnterCriticalArea();
            return dungeon.DungeonID;
        }

        /// <summary>
        /// The WarpToDungeon.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void WarpToDungeon(ActorPC pc)
        {
            SagaMap.Dungeon.Dungeon dungeon = (SagaMap.Dungeon.Dungeon)null;
            List<uint> possibleDungeons = this.GetPossibleDungeons(pc);
            if (possibleDungeons.Count > 0)
            {
                if (possibleDungeons.Count == 1)
                {
                    dungeon = Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.GetDungeon(possibleDungeons[0]);
                }
                else
                {
                    string[] strArray = new string[possibleDungeons.Count];
                    for (int index = 0; index < possibleDungeons.Count; ++index)
                        strArray[index] = Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.GetDungeon(possibleDungeons[index]).Creator.Name + Singleton<LocalManager>.Instance.Strings.ITD_DUNGEON_NAME;
                    dungeon = Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.GetDungeon(possibleDungeons[this.Select(pc, Singleton<LocalManager>.Instance.Strings.ITD_SELECT_DUUNGEON, "", strArray) - 1]);
                }
            }
            if (dungeon == null)
                return;
            this.Warp(pc, dungeon.Start.Map.ID, dungeon.Start.Gates[GateType.Entrance].X, dungeon.Start.Gates[GateType.Entrance].Y);
        }

        /// <summary>
        /// The GetPossibleDungeons.
        /// </summary>
        /// <param name="pc">.</param>
        /// <returns>.</returns>
        protected List<uint> GetPossibleDungeons(ActorPC pc)
        {
            List<uint> uintList = new List<uint>();
            if (pc.DungeonID != 0U)
                uintList.Add(pc.DungeonID);
            if (pc.Party != null)
            {
                foreach (ActorPC actorPc in pc.Party.Members.Values)
                {
                    if (actorPc != pc && actorPc.DungeonID != 0U)
                        uintList.Add(actorPc.DungeonID);
                }
            }
            return uintList;
        }

        /// <summary>
        /// The NPCHide.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="npcID">NPCID.</param>
        protected void NPCHide(ActorPC pc, uint npcID)
        {
            uint key = !Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(npcID) ? pc.MapID : Factory<NPCFactory, NPC>.Instance.Items[npcID].MapID;
            if (!pc.NPCStates.ContainsKey(key))
                pc.NPCStates.Add(key, new Dictionary<uint, bool>());
            if (!pc.NPCStates[key].ContainsKey(npcID))
                pc.NPCStates[key].Add(npcID, false);
            else
                pc.NPCStates[key][npcID] = false;
            if ((int)pc.MapID != (int)key)
                return;
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_NPC_HIDE()
            {
                NPCID = npcID
            });
        }

        /// <summary>
        /// The NPCShow.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="npcID">NPCID.</param>
        protected void NPCShow(ActorPC pc, uint npcID)
        {
            uint key = !Factory<NPCFactory, NPC>.Instance.Items.ContainsKey(npcID) ? pc.MapID : Factory<NPCFactory, NPC>.Instance.Items[npcID].MapID;
            if (!pc.NPCStates.ContainsKey(key))
                pc.NPCStates.Add(key, new Dictionary<uint, bool>());
            if (!pc.NPCStates[key].ContainsKey(npcID))
                pc.NPCStates[key].Add(npcID, true);
            else
                pc.NPCStates[key][npcID] = true;
            if ((int)pc.MapID != (int)key)
                return;
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_NPC_SHOW()
            {
                NPCID = npcID
            });
        }

        /// <summary>
        /// The TranceMob.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="MobID">怪物ID(設定為0時表示變回玩家的樣子).</param>
        protected void TranceMob(ActorPC pc, uint MobID)
        {
            Singleton<SkillHandler>.Instance.TranceMob((SagaDB.Actor.Actor)pc, MobID);
        }

        /// <summary>
        /// The ShowTheaterSchedule.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="mapID">影院地图ID.</param>
        protected void ShowTheaterSchedule(ActorPC pc, uint mapID)
        {
            if (!pc.Online)
                return;
            SSMG_THEATER_SCHEDULE_HEADER theaterScheduleHeader = new SSMG_THEATER_SCHEDULE_HEADER();
            theaterScheduleHeader.MapID = mapID;
            if (FactoryList<TheaterFactory, Movie>.Instance.Items.ContainsKey(mapID))
            {
                IOrderedEnumerable<Movie> source = FactoryList<TheaterFactory, Movie>.Instance.Items[mapID].OrderBy<Movie, DateTime>((Func<Movie, DateTime>)(movie => movie.StartTime));
                if (source.Count<Movie>() > 0)
                {
                    theaterScheduleHeader.Count = source.Count<Movie>();
                    MapClient.FromActorPC(pc).netIO.SendPacket((Packet)theaterScheduleHeader);
                    int num = 0;
                    foreach (Movie movie in source.ToList<Movie>())
                    {
                        MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_THEATER_SCHEDULE()
                        {
                            Index = num,
                            TicketItem = movie.Ticket,
                            Time = string.Format("{0:00}:{1:00}", (object)movie.StartTime.Hour, (object)movie.StartTime.Minute),
                            Title = movie.Name
                        });
                        ++num;
                    }
                }
                else
                    MapClient.FromActorPC(pc).netIO.SendPacket((Packet)theaterScheduleHeader);
            }
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_THEATER_SCHEDULE_FOOTER()
            {
                MapID = mapID
            });
        }

        /// <summary>
        /// The GetNextMovie.
        /// </summary>
        /// <param name="mapID">影院MapID.</param>
        /// <returns>电影.</returns>
        protected Movie GetNextMovie(uint mapID)
        {
            return FactoryList<TheaterFactory, Movie>.Instance.GetNextMovie(mapID);
        }

        /// <summary>
        /// The GetCurrentMovie.
        /// </summary>
        /// <param name="mapID">影院MapID.</param>
        /// <returns>电影.</returns>
        protected Movie GetCurrentMovie(uint mapID)
        {
            return FactoryList<TheaterFactory, Movie>.Instance.GetCurrentMovie(mapID);
        }

        /// <summary>
        /// The UseStamp.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="genre">印章系列.</param>
        /// <param name="slot">印章槽.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected bool UseStamp(ActorPC pc, StampGenre genre, StampSlot slot)
        {
            if (pc.Stamp[genre].Test(slot))
                return false;
            pc.Stamp[genre].SetValue(slot, true);
            SSMG_STAMP_USE ssmgStampUse = new SSMG_STAMP_USE();
            ssmgStampUse.Genre = genre;
            switch (slot)
            {
                case StampSlot.One:
                    ssmgStampUse.Slot = (byte)0;
                    break;
                case StampSlot.Two:
                    ssmgStampUse.Slot = (byte)1;
                    break;
                case StampSlot.Three:
                    ssmgStampUse.Slot = (byte)2;
                    break;
                case StampSlot.Four:
                    ssmgStampUse.Slot = (byte)3;
                    break;
                case StampSlot.Five:
                    ssmgStampUse.Slot = (byte)4;
                    break;
                case StampSlot.Six:
                    ssmgStampUse.Slot = (byte)5;
                    break;
                case StampSlot.Seven:
                    ssmgStampUse.Slot = (byte)6;
                    break;
                case StampSlot.Eight:
                    ssmgStampUse.Slot = (byte)7;
                    break;
                case StampSlot.Nine:
                    ssmgStampUse.Slot = (byte)8;
                    break;
                case StampSlot.Ten:
                    ssmgStampUse.Slot = (byte)9;
                    break;
            }
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)ssmgStampUse);
            return true;
        }

        /// <summary>
        /// The CheckStampGenre.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="genre">系列.</param>
        /// <returns>.</returns>
        protected bool CheckStampGenre(ActorPC pc, StampGenre genre)
        {
            if (genre != StampGenre.Special)
                return pc.Stamp[genre].Test(StampSlot.One) && pc.Stamp[genre].Test(StampSlot.Two) && (pc.Stamp[genre].Test(StampSlot.Three) && pc.Stamp[genre].Test(StampSlot.Four)) && (pc.Stamp[genre].Test(StampSlot.Five) && pc.Stamp[genre].Test(StampSlot.Six) && (pc.Stamp[genre].Test(StampSlot.Seven) && pc.Stamp[genre].Test(StampSlot.Eight))) && pc.Stamp[genre].Test(StampSlot.Nine) && pc.Stamp[genre].Test(StampSlot.Ten);
            return pc.Stamp[genre].Test(StampSlot.One) && pc.Stamp[genre].Test(StampSlot.Two) && (pc.Stamp[genre].Test(StampSlot.Three) && pc.Stamp[genre].Test(StampSlot.Four)) && (pc.Stamp[genre].Test(StampSlot.Five) && pc.Stamp[genre].Test(StampSlot.Six) && pc.Stamp[genre].Test(StampSlot.Seven)) && pc.Stamp[genre].Test(StampSlot.Eight);
        }

        /// <summary>
        /// The ClearStampGenre.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="genre">印章系列.</param>
        protected void ClearStampGenre(ActorPC pc, StampGenre genre)
        {
            pc.Stamp[genre].Value = 0;
            MapClient.FromActorPC(pc).SendStamp();
        }

        /// <summary>
        /// The GetExpForLevel.
        /// </summary>
        /// <param name="level">等级.</param>
        /// <param name="type">等级类别.</param>
        /// <returns>所需经验.</returns>
        protected uint GetExpForLevel(uint level, LevelType type)
        {
            return Singleton<ExperienceManager>.Instance.GetExpForLevel(level, type);
        }

        /// <summary>
        /// The ChangePlayerSize.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="size">高度.</param>
        protected void ChangePlayerSize(ActorPC pc, uint size)
        {
            pc.Size = size;
            MapClient.FromActorPC(pc).SendPlayerSizeUpdate();
        }

        /// <summary>
        /// The ItemEnhance.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected bool ItemEnhance(ActorPC pc)
        {
            List<SagaDB.Item.Item> list = pc.Inventory.GetContainer(ContainerType.BODY).Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(item => (item.IsArmor && (this.CountItem(pc, 90000044U) > 0 || this.CountItem(pc, 90000045U) > 0 || (this.CountItem(pc, 90000046U) > 0 || this.CountItem(pc, 90000043U) > 0)) || item.IsWeapon && (this.CountItem(pc, 90000044U) > 0 || this.CountItem(pc, 90000045U) > 0 || this.CountItem(pc, 90000046U) > 0) || (item.BaseData.itemType == ItemType.SHIELD && (this.CountItem(pc, 90000044U) > 0 || this.CountItem(pc, 90000045U) > 0) || item.BaseData.itemType == ItemType.ACCESORY_NECK && this.CountItem(pc, 90000044U) > 0)) && item.Refine < (ushort)10)).ToList<SagaDB.Item.Item>();
            if (list.Count <= 0)
                return false;
            SSMG_ITEM_ENHANCE_LIST ssmgItemEnhanceList = new SSMG_ITEM_ENHANCE_LIST();
            ssmgItemEnhanceList.Items = list;
            MapClient mapClient = MapClient.FromActorPC(pc);
            mapClient.netIO.SendPacket((Packet)ssmgItemEnhanceList);
            mapClient.itemEnhance = true;
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.itemEnhance)
                Thread.Sleep(500);
            if (blocked)
                ClientManager.EnterCriticalArea();
            return true;
        }

        /// <summary>
        /// The ItemFusion.
        /// </summary>
        /// <param name="pc">.</param>
        /// <param name="confirm">.</param>
        protected void ItemFusion(ActorPC pc, string confirm)
        {
            int num1 = 0;
            MapClient mapClient = MapClient.FromActorPC(pc);
            do
            {
                SSMG_ITEM_FUSION ssmgItemFusion = new SSMG_ITEM_FUSION();
                mapClient.itemFusion = true;
                mapClient.itemFusionView = 0U;
                mapClient.itemFusionEffect = 0U;
                mapClient.netIO.SendPacket((Packet)ssmgItemFusion);
                bool blocked = ClientManager.Blocked;
                if (blocked)
                    ClientManager.LeaveCriticalArea();
                while (mapClient.itemFusion)
                    Thread.Sleep(500);
                if (blocked)
                    ClientManager.EnterCriticalArea();
                if (mapClient.itemFusionEffect != 0U && mapClient.itemFusionView != 0U)
                {
                    SagaDB.Item.Item effect = pc.Inventory.GetItem(mapClient.itemFusionEffect);
                    SagaDB.Item.Item view = pc.Inventory.GetItem(mapClient.itemFusionView);
                    SSMG_ITEM_FUSION_RESULT itemFusionResult = new SSMG_ITEM_FUSION_RESULT();
                    if (effect != null && view != null)
                    {
                        int num2 = (int)effect.BaseData.possibleLv * 1000;
                        num1 = this.Select(pc, confirm, "", Singleton<LocalManager>.Instance.Strings.NPC_ITEM_FUSION_RECHOOSE, Singleton<LocalManager>.Instance.Strings.NPC_ITEM_FUSION_CANCEL, string.Format(Singleton<LocalManager>.Instance.Strings.NPC_ITEM_FUSION_CONFIRM, (object)num2, (object)80));
                        switch (num1 - 1)
                        {
                            case 0:
                            case 1:
                                itemFusionResult.Result = SSMG_ITEM_FUSION_RESULT.FusionResult.CANCELED;
                                break;
                            case 2:
                                if (pc.Gold >= num2)
                                {
                                    SSMG_ITEM_FUSION_RESULT.FusionResult fusionResult = this.checkFusionItem(effect, view);
                                    itemFusionResult.Result = fusionResult;
                                    if (fusionResult == SSMG_ITEM_FUSION_RESULT.FusionResult.OK)
                                    {
                                        pc.Gold -= num2;
                                        if (Global.Random.Next(0, 99) < 80)
                                        {
                                            effect.PictID = view.ItemID;
                                            this.TakeItemBySlot(pc, view.Slot, (ushort)1);
                                            mapClient.SendItemInfo(effect);
                                            this.ShowEffect(pc, 5191U);
                                        }
                                        break;
                                    }
                                    break;
                                }
                                itemFusionResult.Result = SSMG_ITEM_FUSION_RESULT.FusionResult.NOT_ENOUGH_GOLD;
                                break;
                        }
                    }
                    else
                        itemFusionResult.Result = SSMG_ITEM_FUSION_RESULT.FusionResult.FAILED;
                    if (num1 != 1)
                        mapClient.netIO.SendPacket((Packet)itemFusionResult);
                }
            }
            while (num1 == 1 && (mapClient.itemFusionEffect != 0U && mapClient.itemFusionView != 0U));
        }

        /// <summary>
        /// The checkFusionItem.
        /// </summary>
        /// <param name="effect">The effect<see cref="SagaDB.Item.Item"/>.</param>
        /// <param name="view">The view<see cref="SagaDB.Item.Item"/>.</param>
        /// <returns>The <see cref="SSMG_ITEM_FUSION_RESULT.FusionResult"/>.</returns>
        private SSMG_ITEM_FUSION_RESULT.FusionResult checkFusionItem(SagaDB.Item.Item effect, SagaDB.Item.Item view)
        {
            if ((int)effect.BaseData.possibleLv < (int)view.BaseData.possibleLv)
                return SSMG_ITEM_FUSION_RESULT.FusionResult.LV_TOO_LOW;
            foreach (PC_JOB key in effect.BaseData.possibleJob.Keys)
            {
                if (effect.BaseData.possibleJob[key] && !view.BaseData.possibleJob[key])
                    return SSMG_ITEM_FUSION_RESULT.FusionResult.JOB_NOT_FIT;
            }
            foreach (PC_GENDER key in effect.BaseData.possibleGender.Keys)
            {
                if (effect.BaseData.possibleGender[key] && !view.BaseData.possibleGender[key])
                    return SSMG_ITEM_FUSION_RESULT.FusionResult.GENDER_NOT_FIT;
            }
            foreach (PC_RACE key in effect.BaseData.possibleRace.Keys)
            {
                if (effect.BaseData.possibleRace[key] && !view.BaseData.possibleRace[key])
                    return SSMG_ITEM_FUSION_RESULT.FusionResult.NOT_FIT;
            }
            return SSMG_ITEM_FUSION_RESULT.FusionResult.OK;
        }

        /// <summary>
        /// The CheckMapFlag.
        /// </summary>
        /// <param name="pc">pc.</param>
        /// <param name="flag">flag.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        protected bool CheckMapFlag(ActorPC pc, MapFlags flag)
        {
            return Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(flag);
        }

        /// <summary>
        /// The NPCChangeView.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="npcID">NPCID.</param>
        /// <param name="mobID">怪物ID.</param>
        protected void NPCChangeView(ActorPC pc, uint npcID, uint mobID)
        {
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_NPC_CHANGE_VIEW()
            {
                NPCID = npcID,
                MobID = mobID
            });
        }

        /// <summary>
        /// The ODWarCanApply.
        /// </summary>
        /// <param name="mapID">战场MapID.</param>
        /// <returns>.</returns>
        protected bool ODWarCanApply(uint mapID)
        {
            return Singleton<ODWarManager>.Instance.CanApply(mapID);
        }

        /// <summary>
        /// The ODWarReviveSymbol.
        /// </summary>
        /// <param name="mapID">战场MapID.</param>
        /// <param name="number">编号.</param>
        /// <returns>.</returns>
        protected SymbolReviveResult ODWarReviveSymbol(uint mapID, int number)
        {
            return Singleton<ODWarManager>.Instance.ReviveSymbol(mapID, number);
        }

        /// <summary>
        /// The ODWarIsDefence.
        /// </summary>
        /// <param name="mapID">战场MapID.</param>
        /// <returns>.</returns>
        protected bool ODWarIsDefence(uint mapID)
        {
            return Singleton<ODWarManager>.Instance.IsDefence(mapID);
        }

        /// <summary>
        /// The ItemAddSlot.
        /// </summary>
        /// <param name="pc">.</param>
        protected void ItemAddSlot(ActorPC pc)
        {
            List<uint> uintList = new List<uint>();
            foreach (SagaDB.Item.Item obj in pc.Inventory.GetContainer(ContainerType.BODY))
            {
                if (obj.IsEquipt && obj.CurrentSlot < (byte)5 && (obj.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE || obj.EquipSlot[0] == EnumEquipSlot.UPPER_BODY || obj.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND))
                    uintList.Add(obj.Slot);
            }
            foreach (SagaDB.Item.Item obj in pc.Inventory.GetContainer(ContainerType.BACK_BAG))
            {
                if (obj.IsEquipt && obj.CurrentSlot < (byte)5 && (obj.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE || obj.EquipSlot[0] == EnumEquipSlot.UPPER_BODY || obj.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND))
                    uintList.Add(obj.Slot);
            }
            foreach (SagaDB.Item.Item obj in pc.Inventory.GetContainer(ContainerType.LEFT_BAG))
            {
                if (obj.IsEquipt && obj.CurrentSlot < (byte)5 && (obj.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE || obj.EquipSlot[0] == EnumEquipSlot.UPPER_BODY || obj.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND))
                    uintList.Add(obj.Slot);
            }
            foreach (SagaDB.Item.Item obj in pc.Inventory.GetContainer(ContainerType.RIGHT_BAG))
            {
                if (obj.IsEquipt && obj.CurrentSlot < (byte)5 && (obj.EquipSlot[0] == EnumEquipSlot.CHEST_ACCE || obj.EquipSlot[0] == EnumEquipSlot.UPPER_BODY || obj.EquipSlot[0] == EnumEquipSlot.RIGHT_HAND))
                    uintList.Add(obj.Slot);
            }
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) && pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].CurrentSlot < (byte)5)
                uintList.Add(pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].Slot);
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY) && pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].CurrentSlot < (byte)5)
                uintList.Add(pc.Inventory.Equipments[EnumEquipSlot.UPPER_BODY].Slot);
            if (pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE) && pc.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE].CurrentSlot < (byte)5)
                uintList.Add(pc.Inventory.Equipments[EnumEquipSlot.CHEST_ACCE].Slot);
            if (uintList.Count > 0)
            {
                MapClient mapClient = MapClient.FromActorPC(pc);
                mapClient.irisAddSlot = true;
                mapClient.SendSkillDummy(2090U, (byte)1);
                mapClient.netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_ITEM_LIST()
                {
                    Items = uintList
                });
                bool blocked = ClientManager.Blocked;
                if (blocked)
                    ClientManager.LeaveCriticalArea();
                while (mapClient.irisAddSlot)
                    Thread.Sleep(500);
                if (!blocked)
                    return;
                ClientManager.EnterCriticalArea();
            }
            else
                MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_IRIS_ADD_SLOT_RESULT()
                {
                    Result = SSMG_IRIS_ADD_SLOT_RESULT.Results.NO_ITEM
                });
        }

        /// <summary>
        /// The IrisCardAssemble.
        /// </summary>
        /// <param name="pc">.</param>
        protected void IrisCardAssemble(ActorPC pc)
        {
            Dictionary<IrisCard, int> dictionary = new Dictionary<IrisCard, int>();
            foreach (SagaDB.Item.Item obj in pc.Inventory.Items[ContainerType.BODY])
            {
                if (obj.BaseData.itemType == ItemType.IRIS_CARD && Factory<IrisCardFactory, IrisCard>.Instance.Items.ContainsKey(obj.BaseData.id))
                {
                    IrisCard key = Factory<IrisCardFactory, IrisCard>.Instance.Items[obj.BaseData.id];
                    if (key.NextCard != 0U)
                        dictionary.Add(key, 5000);
                }
            }
            MapClient mapClient = MapClient.FromActorPC(pc);
            if (dictionary.Count > 0)
            {
                mapClient.irisCardAssemble = true;
                mapClient.SendSkillDummy(2091U, (byte)1);
                mapClient.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_ASSEMBLE()
                {
                    CardAndPrice = dictionary
                });
                bool blocked = ClientManager.Blocked;
                if (blocked)
                    ClientManager.LeaveCriticalArea();
                while (mapClient.irisCardAssemble)
                    Thread.Sleep(500);
                if (!blocked)
                    return;
                ClientManager.EnterCriticalArea();
            }
            else
                mapClient.netIO.SendPacket((Packet)new SSMG_IRIS_CARD_ASSEMBLE_RESULT()
                {
                    Result = SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results.NO_ITEM
                });
        }

        /// <summary>
        /// The CheckMapFlag.
        /// </summary>
        /// <param name="mapID">地图ID.</param>
        /// <param name="flag">要检查的标识.</param>
        /// <returns>结果.</returns>
        protected bool CheckMapFlag(uint mapID, MapFlags flag)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            if (map == null)
                return false;
            return map.Info.Flag.Test(flag);
        }

        /// <summary>
        /// The AddTimer.
        /// </summary>
        /// <param name="name">名称.</param>
        /// <param name="period">再启动间隔.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>添加的计时器.</returns>
        protected Timer AddTimer(string name, int period, ActorPC pc)
        {
            return this.AddTimer(name, period, 500, pc, true);
        }

        /// <summary>
        /// The AddTimer.
        /// </summary>
        /// <param name="name">名字.</param>
        /// <param name="period">重复间隔，单位为毫秒.</param>
        /// <param name="dueTime">启动延迟.</param>
        /// <param name="pc">挂钩的玩家.</param>
        /// <param name="needScript">是否需要用脚本.</param>
        /// <returns>添加的计时器.</returns>
        protected Timer AddTimer(string name, int period, int dueTime, ActorPC pc, bool needScript)
        {
            Timer timer = new Timer(name, period, dueTime);
            timer.AttachedPC = pc;
            timer.NeedScript = needScript;
            string key = string.Format("{0}:{1}({2})", (object)name, (object)pc.Name, (object)pc.CharID);
            if (Singleton<ScriptManager>.Instance.Timers.ContainsKey(key))
            {
                Singleton<ScriptManager>.Instance.Timers[key].Deactivate();
                Singleton<ScriptManager>.Instance.Timers[key] = (MultiRunTask)null;
                Singleton<ScriptManager>.Instance.Timers[key] = (MultiRunTask)timer;
            }
            else
                Singleton<ScriptManager>.Instance.Timers.Add(key, (MultiRunTask)timer);
            return timer;
        }

        /// <summary>
        /// The DeleteTimer.
        /// </summary>
        /// <param name="name">名字.</param>
        /// <param name="pc">绑定的玩家.</param>
        protected void DeleteTimer(string name, ActorPC pc)
        {
            string key = string.Format("{0}:{1}({2})", (object)name, (object)pc.Name, (object)pc.CharID);
            if (!Singleton<ScriptManager>.Instance.Timers.ContainsKey(key))
                return;
            Singleton<ScriptManager>.Instance.Timers[key].Deactivate();
            Singleton<ScriptManager>.Instance.Timers.Remove(key);
        }

        /// <summary>
        /// The GetTimer.
        /// </summary>
        /// <param name="name">名字.</param>
        /// <param name="pc">绑定的玩家.</param>
        /// <returns>The <see cref="Timer"/>.</returns>
        protected Timer GetTimer(string name, ActorPC pc)
        {
            string key = string.Format("{0}:{1}({2})", (object)name, (object)pc.Name, (object)pc.CharID);
            if (Singleton<ScriptManager>.Instance.Timers.ContainsKey(key))
                return (Timer)Singleton<ScriptManager>.Instance.Timers[key];
            return (Timer)null;
        }

        /// <summary>
        /// The ShowPicture.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="path">图片路径.</param>
        /// <param name="title">标题.</param>
        protected void ShowPicture(ActorPC pc, string path, string title)
        {
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_NPC_SHOW_PIC()
            {
                Path = path,
                Title = title
            });
        }

        /// <summary>
        /// The FGardenChangeWeather.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="weather">天气.</param>
        protected void FGardenChangeWeather(ActorPC pc, FG_Weather weather)
        {
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_FG_CHANGE_WEATHER()
            {
                Weather = weather
            });
        }

        /// <summary>
        /// The FGardenChangeSky.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="sky">天空.</param>
        protected void FGardenChangeSky(ActorPC pc, FG_Sky sky)
        {
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_FG_CHANGE_SKY()
            {
                Sky = sky
            });
        }

        /// <summary>
        /// The Announce.
        /// </summary>
        /// <param name="text">文字.</param>
        protected void Announce(string text)
        {
            MapClientManager.Instance.Announce(text);
        }

        /// <summary>
        /// The Announce.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="text">文字.</param>
        protected void Announce(ActorPC pc, string text)
        {
            MapClient.FromActorPC(pc).SendAnnounce(text);
        }

        /// <summary>
        /// The Announce.
        /// </summary>
        /// <param name="mapID">地图ID.</param>
        /// <param name="text">文字.</param>
        protected void Announce(uint mapID, string text)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            if (map == null)
                return;
            foreach (SagaDB.Actor.Actor actor in map.Actors.Values.ToArray<SagaDB.Actor.Actor>())
            {
                if (actor.type == ActorType.PC)
                    MapClient.FromActorPC((ActorPC)actor).SendAnnounce(text);
            }
        }

        /// <summary>
        /// The SpawnMob.
        /// </summary>
        /// <param name="mapID">地图ID.</param>
        /// <param name="x">.</param>
        /// <param name="y">.</param>
        /// <param name="mobID">怪物ID.</param>
        /// <param name="count">数量.</param>
        /// <returns>The <see cref="List{ActorMob}"/>.</returns>
        protected List<ActorMob> SpawnMob(uint mapID, byte x, byte y, uint mobID, int count)
        {
            return this.SpawnMob(mapID, x, y, mobID, count, (MobCallback)null);
        }

        /// <summary>
        /// The SpawnMob.
        /// </summary>
        /// <param name="mapID">地图ID.</param>
        /// <param name="x">.</param>
        /// <param name="y">.</param>
        /// <param name="mobID">怪物ID.</param>
        /// <param name="count">数量.</param>
        /// <param name="dieEvent">死亡事件处理器.</param>
        /// <returns>The <see cref="List{ActorMob}"/>.</returns>
        protected List<ActorMob> SpawnMob(uint mapID, byte x, byte y, uint mobID, int count, MobCallback dieEvent)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(mapID);
            List<ActorMob> actorMobList = new List<ActorMob>();
            for (int index = 0; index < count; ++index)
            {
                ActorMob actorMob = map.SpawnMob(mobID, Global.PosX8to16(x, map.Width), Global.PosY8to16(y, map.Height), (short)1000, (SagaDB.Actor.Actor)null);
                MobEventHandler e = (MobEventHandler)actorMob.e;
                if (dieEvent != null)
                    e.Dying += dieEvent;
                actorMobList.Add(actorMob);
            }
            return actorMobList;
        }

        /// <summary>
        /// The PetRecover.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="amount">恢复数量.</param>
        protected void PetRecover(ActorPC pc, int amount)
        {
            IEnumerable<SagaDB.Item.Item> source = pc.Inventory.GetContainer(ContainerType.BODY).Where<SagaDB.Item.Item>((Func<SagaDB.Item.Item, bool>)(c => (c.BaseData.itemType == ItemType.PET || c.BaseData.itemType == ItemType.PET_NEKOMATA || c.BaseData.itemType == ItemType.RIDE_PET) && (int)c.Durability < (int)c.BaseData.durability));
            MapClient mapClient = MapClient.FromActorPC(pc);
            mapClient.selectedPet = 0U;
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_NPC_PET_SELECT()
            {
                Type = SSMG_NPC_PET_SELECT.SelType.Recover,
                Pets = source.ToList<SagaDB.Item.Item>()
            });
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.selectedPet == 0U)
                Thread.Sleep(100);
            if (blocked)
                ClientManager.EnterCriticalArea();
            if (mapClient.selectedPet == uint.MaxValue)
                return;
            SagaDB.Item.Item obj = pc.Inventory.GetItem(mapClient.selectedPet);
            if (obj != null && (obj.BaseData.itemType == ItemType.PET || obj.BaseData.itemType == ItemType.PET_NEKOMATA || obj.BaseData.itemType == ItemType.RIDE_PET))
            {
                obj.Durability += (ushort)amount;
                if ((int)obj.Durability > (int)obj.BaseData.durability)
                    obj.Durability = obj.BaseData.durability;
                mapClient.SendItemInfo(obj);
            }
        }

        /// <summary>
        /// The DEMCL.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void DEMCL(ActorPC pc)
        {
            if (pc.Race != PC_RACE.DEM)
                return;
            MapClient mapClient = MapClient.FromActorPC(pc);
            bool inDominionWorld = pc.InDominionWorld;
            SSMG_DEM_COST_LIMIT ssmgDemCostLimit = new SSMG_DEM_COST_LIMIT();
            if (inDominionWorld)
            {
                ssmgDemCostLimit.CurrentEP = pc.DominionEPUsed;
                ssmgDemCostLimit.EPRequired = (short)((int)Singleton<ExperienceManager>.Instance.GetEPRequired(pc) - (int)pc.DominionEPUsed);
            }
            else
            {
                ssmgDemCostLimit.CurrentEP = pc.EPUsed;
                ssmgDemCostLimit.EPRequired = (short)((int)Singleton<ExperienceManager>.Instance.GetEPRequired(pc) - (int)pc.EPUsed);
            }
            mapClient.demCLBuy = true;
            mapClient.netIO.SendPacket((Packet)ssmgDemCostLimit);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.demCLBuy)
                Thread.Sleep(100);
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The DEMParts.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void DEMParts(ActorPC pc)
        {
            if (pc.Race != PC_RACE.DEM)
                return;
            MapClient mapClient = MapClient.FromActorPC(pc);
            SSMG_DEM_PARTS ssmgDemParts = new SSMG_DEM_PARTS();
            mapClient.demParts = true;
            mapClient.netIO.SendPacket((Packet)ssmgDemParts);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.demParts)
                Thread.Sleep(100);
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The DEMIC.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void DEMIC(ActorPC pc)
        {
            if (pc.Race != PC_RACE.DEM)
                return;
            MapClient mapClient = MapClient.FromActorPC(pc);
            mapClient.demic = true;
            SSMG_DEM_DEMIC_HEADER ssmgDemDemicHeader = new SSMG_DEM_DEMIC_HEADER();
            if (mapClient.map.Info.Flag.Test(MapFlags.Dominion))
            {
                ssmgDemDemicHeader.CL = pc.DominionCL;
                mapClient.netIO.SendPacket((Packet)ssmgDemDemicHeader);
                foreach (byte key in pc.Inventory.DominionDemicChips.Keys)
                    mapClient.netIO.SendPacket((Packet)new SSMG_DEM_DEMIC_DATA()
                    {
                        Page = key,
                        Chips = pc.Inventory.GetChipList(key, true)
                    });
            }
            else
            {
                ssmgDemDemicHeader.CL = pc.CL;
                mapClient.netIO.SendPacket((Packet)ssmgDemDemicHeader);
                foreach (byte key in pc.Inventory.DemicChips.Keys)
                    mapClient.netIO.SendPacket((Packet)new SSMG_DEM_DEMIC_DATA()
                    {
                        Page = key,
                        Chips = pc.Inventory.GetChipList(key, false)
                    });
            }
            SSMG_DEM_DEMIC_FOOTER ssmgDemDemicFooter = new SSMG_DEM_DEMIC_FOOTER();
            mapClient.netIO.SendPacket((Packet)ssmgDemDemicFooter);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.demic)
                Thread.Sleep(100);
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The NPCMove.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="npcID">NPCID.</param>
        /// <param name="x">.</param>
        /// <param name="y">.</param>
        /// <param name="dir">方向.</param>
        /// <param name="type">移动方式.</param>
        protected void NPCMove(ActorPC pc, uint npcID, short x, short y, short dir, MoveType type)
        {
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_ACTOR_MOVE()
            {
                ActorID = npcID,
                X = x,
                Y = y,
                Dir = (ushort)dir,
                MoveType = type
            });
        }

        /// <summary>
        /// The NPCMove.
        /// </summary>
        /// <param name="pc">玩家.</param>
        /// <param name="npcID">NPCID.</param>
        /// <param name="x">.</param>
        /// <param name="y">.</param>
        /// <param name="speed">速度.</param>
        /// <param name="type">类型，7为水平移动，0xb为跳跃.</param>
        /// <param name="motion">表情.</param>
        /// <param name="motionSpeed">表情速度.</param>
        protected void NPCMove(ActorPC pc, uint npcID, byte x, byte y, ushort speed, byte type, ushort motion, ushort motionSpeed)
        {
            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_NPC_MOVE()
            {
                NPCID = npcID,
                X = x,
                Y = y,
                Speed = speed,
                Type = type,
                Motion = motion,
                MotionSpeed = motionSpeed
            });
        }

        /// <summary>
        /// The DEMChipShop.
        /// </summary>
        /// <param name="pc">玩家.</param>
        protected void DEMChipShop(ActorPC pc)
        {
            MapClient mapClient = MapClient.FromActorPC(pc);
            SSMG_DEM_CHIP_SHOP_CATEGORY chipShopCategory = new SSMG_DEM_CHIP_SHOP_CATEGORY();
            chipShopCategory.Categories = !pc.InDominionWorld ? Factory<ChipShopFactory, ChipShopCategory>.Instance.GetCategoryFromLv(pc.Level) : Factory<ChipShopFactory, ChipShopCategory>.Instance.GetCategoryFromLv(pc.DominionLevel);
            mapClient.chipShop = true;
            mapClient.netIO.SendPacket((Packet)chipShopCategory);
            bool blocked = ClientManager.Blocked;
            if (blocked)
                ClientManager.LeaveCriticalArea();
            while (mapClient.chipShop)
                Thread.Sleep(100);
            if (!blocked)
                return;
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The ShowPortal.
        /// </summary>
        /// <param name="pc">玩家，用于确定地图.</param>
        /// <param name="x">.</param>
        /// <param name="y">.</param>
        /// <param name="eventID">传送门触发的EventID.</param>
        protected void ShowPortal(ActorPC pc, byte x, byte y, uint eventID)
        {
            foreach (SagaDB.Actor.Actor actor in Singleton<MapManager>.Instance.GetMap(pc.MapID).Actors.Values.ToArray<SagaDB.Actor.Actor>())
            {
                if (actor.type == ActorType.PC)
                {
                    ActorPC pc1 = (ActorPC)actor;
                    if (pc1.Online)
                        MapClient.FromActorPC(pc1).netIO.SendPacket((Packet)new SSMG_NPC_SET_EVENT_AREA()
                        {
                            StartX = (uint)x,
                            StartY = (uint)y,
                            EndX = (uint)x,
                            EndY = (uint)y,
                            EventID = eventID,
                            EffectID = 9005U
                        });
                }
            }
        }

        /// <summary>
        /// The GetItemNameByType.
        /// </summary>
        /// <param name="type">The type<see cref="ItemType"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string GetItemNameByType(ItemType type)
        {
            switch (type)
            {
                case ItemType.NONE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_NONE;
                case ItemType.POTION:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_POTION;
                case ItemType.FOOD:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_FOOD;
                case ItemType.USE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_USE;
                case ItemType.PETFOOD:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_PETFOOD;
                case ItemType.SEED:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_SEED;
                case ItemType.SCROLL:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_SCROLL;
                case ItemType.ACCESORY_HEAD:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ACCE_HEAD;
                case ItemType.ACCESORY_FACE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ACCE_FACE0;
                case ItemType.HELM:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_HELM;
                case ItemType.BOOTS:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_BOOTS;
                case ItemType.CLAW:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_CLAW;
                case ItemType.HAMMER:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_HAMMER;
                case ItemType.ARMOR_UPPER:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ARMOR_UPPER;
                case ItemType.FULLFACE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_FULLFACE;
                case ItemType.LONGBOOTS:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_LONGBOOTS;
                case ItemType.SHIELD:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_SHIELD;
                case ItemType.ONEPIECE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ONEPIECE;
                case ItemType.COSTUME:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_COSTUME;
                case ItemType.BODYSUIT:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_BODYSUIT;
                case ItemType.STAFF:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_STAFF;
                case ItemType.SWORD:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_SWORD;
                case ItemType.AXE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_AXE;
                case ItemType.SPEAR:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_SPEAR;
                case ItemType.BOW:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_BOW;
                case ItemType.HANDBAG:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_HANDBAG;
                case ItemType.GUN:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_GUN;
                case ItemType.ARMOR_LOWER:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ARMOR_LOWER;
                case ItemType.WEDDING:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_BODYSUIT;
                case ItemType.OVERALLS:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_OVERALLS;
                case ItemType.FACEBODYSUIT:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_FACEBODYSUIT;
                case ItemType.SLACKS:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_SLACKS;
                case ItemType.SHOES:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_SHOES;
                case ItemType.HALFBOOTS:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_HALFBOOTS;
                case ItemType.ACCESORY_NECK:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ACCE_NECK;
                case ItemType.BACKPACK:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_BACKPACK;
                case ItemType.LEFT_HANDBAG:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_LEFT_HANDBAG;
                case ItemType.ETC_WEAPON:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ETC_WEAPON;
                case ItemType.ACCESORY_FINGER:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ACCE_FINGER;
                case ItemType.SHORT_SWORD:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_SHORT_SWORD;
                case ItemType.RAPIER:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_RAPIER;
                case ItemType.STRINGS:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_STAFF;
                case ItemType.BOOK:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_BOOK;
                case ItemType.DUALGUN:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_GUN;
                case ItemType.RIFLE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_GUN;
                case ItemType.THROW:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_THROW;
                case ItemType.ROPE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ROPE;
                case ItemType.SOCKS:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_SOCKS;
                case ItemType.BULLET:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_BULLET;
                case ItemType.ARROW:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_ARROW;
                case ItemType.BACK_DEMON:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_BACK_DEMON;
                case ItemType.PET:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_PET;
                case ItemType.RIDE_PET:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_RIDE_PET;
                case ItemType.PET_NEKOMATA:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_PET_NEKOMATA;
                case ItemType.FURNITURE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_FG_FURNITURE;
                case ItemType.CARD:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_CARD;
                case ItemType.FG_GARDEN_MODELHOUSE:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_FG_BASEBUILD;
                case ItemType.FG_ROOM_WALL:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_FG_ROOM_WALL;
                case ItemType.FG_ROOM_FLOOR:
                    return Singleton<LocalManager>.Instance.Strings.ITEM_UNIDENTIFIED_FG_ROOM_FLOOR;
                default:
                    return type.ToString();
            }
        }

        /// <summary>
        /// The OnEvent.
        /// </summary>
        /// <param name="pc">触发该事件的玩家类.</param>
        public abstract void OnEvent(ActorPC pc);

        /// <summary>
        /// The OnTransportSource.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public virtual void OnTransportSource(ActorPC pc)
        {
            this.Say(pc, (ushort)131, this.questTransportSource, "");
            if (pc.Quest.Detail.ObjectID1 != 0U)
                this.GiveItem(pc, pc.Quest.Detail.ObjectID1, (ushort)pc.Quest.Detail.Count1);
            if (pc.Quest.Detail.ObjectID2 != 0U)
                this.GiveItem(pc, pc.Quest.Detail.ObjectID2, (ushort)pc.Quest.Detail.Count2);
            if (pc.Quest.Detail.ObjectID3 != 0U)
                this.GiveItem(pc, pc.Quest.Detail.ObjectID3, (ushort)pc.Quest.Detail.Count3);
            this.Say(pc, (ushort)131, Singleton<LocalManager>.Instance.Strings.QUEST_TRANSPORT_GET, " ");
            this.PlaySound(pc, 2030U, false, 100U, (byte)50);
        }

        /// <summary>
        /// The OnTransportDest.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public virtual void OnTransportDest(ActorPC pc)
        {
            if (pc.Quest.Detail.ObjectID1 != 0U && this.CountItem(pc, pc.Quest.Detail.ObjectID1) < pc.Quest.Detail.Count1)
                pc.Quest.CurrentCount3 = 1;
            if (pc.Quest.Detail.ObjectID2 != 0U && this.CountItem(pc, pc.Quest.Detail.ObjectID2) < pc.Quest.Detail.Count2)
                pc.Quest.CurrentCount3 = 1;
            if (pc.Quest.Detail.ObjectID3 != 0U && this.CountItem(pc, pc.Quest.Detail.ObjectID3) < pc.Quest.Detail.Count3)
                pc.Quest.CurrentCount3 = 1;
            if (pc.Quest.CurrentCount3 != 0)
                return;
            this.Say(pc, (ushort)131, this.questTransportDest, "");
            if (pc.Quest.Detail.ObjectID1 != 0U)
                this.TakeItem(pc, pc.Quest.Detail.ObjectID1, (ushort)pc.Quest.Detail.Count1);
            if (pc.Quest.Detail.ObjectID2 != 0U)
                this.TakeItem(pc, pc.Quest.Detail.ObjectID2, (ushort)pc.Quest.Detail.Count2);
            if (pc.Quest.Detail.ObjectID3 != 0U)
                this.TakeItem(pc, pc.Quest.Detail.ObjectID3, (ushort)pc.Quest.Detail.Count3);
            this.Say(pc, (ushort)131, Singleton<LocalManager>.Instance.Strings.QUEST_TRANSPORT_GIVE, " ");
            this.PlaySound(pc, 2040U, false, 100U, (byte)50);
        }

        /// <summary>
        /// The OnTransportCompleteSrc.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public virtual void OnTransportCompleteSrc(ActorPC pc)
        {
            this.Say(pc, (ushort)131, this.questTransportCompleteSrc, "");
        }

        /// <summary>
        /// The OnTransportCompleteDest.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public virtual void OnTransportCompleteDest(ActorPC pc)
        {
            this.Say(pc, (ushort)131, this.questTransportCompleteDest, "");
        }
    }
}
