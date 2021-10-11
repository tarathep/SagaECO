namespace SagaMap
{
    using SagaDB.Actor;
    using SagaDB.ECOShop;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Mob;
    using SagaDB.Npc;
    using SagaDB.Quests;
    using SagaDB.Skill;
    using SagaDB.Synthese;
    using SagaDB.Theater;
    using SagaDB.Treasure;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Server;
    using SagaMap.PC;
    using SagaMap.Scripting;
    using SagaMap.Tasks.System;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="AtCommand" />.
    /// </summary>
    public class AtCommand : Singleton<AtCommand>
    {
        /// <summary>
        /// Defines the _MasterName.
        /// </summary>
        private static string _MasterName = "Saga";

        /// <summary>
        /// Defines the commandTable.
        /// </summary>
        private Dictionary<string, AtCommand.CommandInfo> commandTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="AtCommand"/> class.
        /// </summary>
        public AtCommand()
        {
            this.commandTable = new Dictionary<string, AtCommand.CommandInfo>();
            string str1 = "/";
            string str2 = "!";
            this.commandTable.Add(str1 + "motion", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessMotion), 0U));
            this.commandTable.Add(str1 + "dustbox", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessDustbox), 0U));
            this.commandTable.Add(str1 + "vcashshop", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessVcashsop), 0U));
            this.commandTable.Add(str1 + "user", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessUser), 0U));
            this.commandTable.Add(str1 + "commandlist", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessCommandList), 0U));
            this.commandTable.Add(str2 + "warp", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessWarp), 20U));
            this.commandTable.Add(str2 + "announce", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessAnnounce), 100U));
            this.commandTable.Add(str2 + "heal", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessHeal), 20U));
            this.commandTable.Add(str2 + "level", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessLevel), 60U));
            this.commandTable.Add(str2 + "joblv", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessJobLevel), 60U));
            this.commandTable.Add(str2 + "gold", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessGold), 60U));
            this.commandTable.Add(str2 + "shoppoint", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessShoppoint), 60U));
            this.commandTable.Add(str2 + "hair", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessHair), 20U));
            this.commandTable.Add(str2 + "hairstyle", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessHairstyle), 20U));
            this.commandTable.Add(str2 + "haircolor", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessHaircolor), 20U));
            this.commandTable.Add(str2 + "job", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessJob), 60U));
            this.commandTable.Add(str2 + "statpoints", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessStatPoints), 60U));
            this.commandTable.Add(str2 + "skillpoints", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessSkillPoints), 60U));
            this.commandTable.Add(str2 + "hide", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessHide), 60U));
            this.commandTable.Add(str2 + "ban", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessBan), 80U));
            this.commandTable.Add(str2 + "event", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessEvent), 20U));
            this.commandTable.Add(str2 + "hairext", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessHairext), 20U));
            this.commandTable.Add(str2 + "playersize", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessPlayersize), 20U));
            this.commandTable.Add(str2 + "item", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessItem), 60U));
            this.commandTable.Add(str2 + "speed", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessSpeed), 20U));
            this.commandTable.Add(str2 + "revive", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessRevive), 20U));
            this.commandTable.Add(str2 + "kick", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessKick), 100U));
            this.commandTable.Add(str2 + "kickall", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessKickAll), 100U));
            this.commandTable.Add(str2 + "recall", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessJump), 60U));
            this.commandTable.Add(str2 + "recall2", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessJump2), 60U));
            this.commandTable.Add(str2 + "jump", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessJumpTo), 60U));
            this.commandTable.Add(str2 + "jump2", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessJumpTo2), 60U));
            this.commandTable.Add(str2 + "mob", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessMob), 60U));
            this.commandTable.Add(str2 + "summon", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessSummon), 60U));
            this.commandTable.Add(str2 + "summonme", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessSummonMe), 60U));
            this.commandTable.Add(str2 + "spawn", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessSpawn), 60U));
            this.commandTable.Add(str2 + "effect", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessEffect), 60U));
            this.commandTable.Add(str2 + "kickgolem", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessKickGolem), 60U));
            this.commandTable.Add(str2 + "killallmob", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessKillAllMob), 60U));
            this.commandTable.Add(str2 + "odwarstart", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessODWarStart), 60U));
            this.commandTable.Add(str2 + "skill", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessSkill), 60U));
            this.commandTable.Add(str2 + "skillclear", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessSkillClear), 60U));
            this.commandTable.Add(str2 + "gmob", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessGridMob), 60U));
            this.commandTable.Add(str2 + "showstatus", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessShowStatus), 60U));
            this.commandTable.Add(str2 + "who", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessWho), 1U));
            this.commandTable.Add(str2 + "who2", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessWho2), 20U));
            this.commandTable.Add(str2 + "who3", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessWho3), 60U));
            this.commandTable.Add(str2 + "mode", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessMode), 100U));
            this.commandTable.Add(str2 + "robot", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessRobot), 100U));
            this.commandTable.Add(str2 + "go", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessGo), 20U));
            this.commandTable.Add(str2 + "ch", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessCh), 20U));
            this.commandTable.Add(str2 + "info", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessInfo), 20U));
            this.commandTable.Add(str2 + "reloadscript", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessReloadScript), 99U));
            this.commandTable.Add(str2 + "reloadconfig", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessReloadConfig), 99U));
            this.commandTable.Add(str2 + "raw", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessRaw), 100U));
            this.commandTable.Add(str2 + "test", new AtCommand.CommandInfo(new AtCommand.ProcessCommandFunc(this.ProcessTest), 100U));
        }

        /// <summary>
        /// The LoadCommandLevelSetting.
        /// </summary>
        /// <param name="path">.</param>
        public void LoadCommandLevelSetting(string path)
        {
            try
            {
                StreamReader streamReader = new StreamReader(path);
                int num = 0;
                while (!streamReader.EndOfStream)
                {
                    string str = streamReader.ReadLine();
                    if (str[0] != '#')
                    {
                        string[] strArray = str.Split(',');
                        AtCommand.CommandInfo commandInfo = this.commandTable[strArray[0]];
                        if (commandInfo != null)
                        {
                            commandInfo.level = uint.Parse(strArray[1]);
                            ++num;
                        }
                    }
                }
                Logger.ShowInfo(string.Format("{0} GMCommand Setting Loaded.", (object)num));
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex.ToString());
            }
        }

        /// <summary>
        /// The ProcessCommand.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="command">The command<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ProcessCommand(MapClient client, string command)
        {
            try
            {
                string[] strArray = command.Split(" ".ToCharArray(), 2);
                strArray[0] = strArray[0].ToLower();
                if (this.commandTable.ContainsKey(strArray[0]))
                {
                    AtCommand.CommandInfo commandInfo = this.commandTable[strArray[0]];
                    if ((uint)client.Character.Account.GMLevel >= commandInfo.level)
                    {
                        Logger.LogGMCommand(client.Character.Name + "(" + (object)client.Character.CharID + ")", "", string.Format("Account:{0}({1}) GMLv:{2} Command:{3}", (object)client.Character.Account.Name, (object)client.Character.Account.AccountID, (object)client.Character.Account.GMLevel, (object)command));
                        if (strArray.Length == 2)
                            commandInfo.func(client, strArray[1]);
                        else
                            commandInfo.func(client, "");
                    }
                    else
                        client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_NO_ACCESS);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, (Logger)null);
            }
            return false;
        }

        /// <summary>
        /// The ProcessMotion.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessMotion(MapClient client, string args)
        {
            client.SendMotion((MotionType)int.Parse(args), (byte)1);
        }

        /// <summary>
        /// The ProcessWhere.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessWhere(MapClient client, string args)
        {
        }

        /// <summary>
        /// The ProcessDustbox.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessDustbox(MapClient client, string args)
        {
        }

        /// <summary>
        /// The ProcessVcashsop.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessVcashsop(MapClient client, string args)
        {
            SkillEvent.Instance.VShopOpen(client.Character);
        }

        /// <summary>
        /// The ProcessUser.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessUser(MapClient client, string args)
        {
            foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                client.SendSystemMessage(mapClient.Character.Name + " [" + mapClient.Map.Name + "]");
            client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
        }

        /// <summary>
        /// The ProcessGetHeight.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessGetHeight(MapClient client, string args)
        {
        }

        /// <summary>
        /// The ProcessCommandList.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        public void ProcessCommandList(MapClient client, string args)
        {
            client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_COMMANDLIST);
            foreach (string key in this.commandTable.Keys)
            {
                if ((uint)client.Character.Account.GMLevel >= this.commandTable[key].level)
                {
                    string str = "";
                    if (Singleton<LocalManager>.Instance.Strings.ATCOMMAND_DESC.ContainsKey(key))
                        str = Singleton<LocalManager>.Instance.Strings.ATCOMMAND_DESC[key];
                    client.SendSystemMessage(key + " " + str);
                }
            }
        }

        /// <summary>
        /// The ProcessRevive.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessRevive(MapClient client, string args)
        {
            if (!client.Character.Buff.Dead)
                return;
            client.Character.BattleStatus = (byte)0;
            client.SendChangeStatus();
            client.Character.TInt["Revive"] = 5;
            client.EventActivate(4043309056U);
        }

        /// <summary>
        /// The ProcessSpeed.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessSpeed(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_SPEED_PARA);
            }
            else
            {
                try
                {
                    client.Character.Speed = ushort.Parse(args);
                    client.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SPEED_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)client.Character, true);
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessStatPoints.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessStatPoints(MapClient client, string args)
        {
            try
            {
                ushort num = ushort.Parse(args);
                client.Character.StatsPoint = num;
            }
            catch
            {
            }
        }

        /// <summary>
        /// The ProcessSkillPoints.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="command">The command<see cref="string"/>.</param>
        private void ProcessSkillPoints(MapClient client, string command)
        {
            try
            {
                string[] strArray = command.Split(' ');
                if (strArray.Length <= 1)
                    return;
                ushort num = ushort.Parse(strArray[1]);
                switch (strArray[0])
                {
                    case "1":
                        client.Character.SkillPoint = num;
                        break;
                    case "2-1":
                        client.Character.SkillPoint2X = num;
                        break;
                    case "2-2":
                        client.Character.SkillPoint2T = num;
                        break;
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// The ProcessJob.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessJob(MapClient client, string args)
        {
            try
            {
                int num = int.Parse(args);
                client.Character.Job = (PC_JOB)num;
                Singleton<StatusFactory>.Instance.CalcStatus(client.Character);
                client.SendPlayerInfo();
                client.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
                {
                    effectID = 4131U,
                    actorID = client.Character.ActorID
                }, (SagaDB.Actor.Actor)client.Character, true);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The ProcessEvent.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessEvent(MapClient client, string args)
        {
            try
            {
                uint EventID = uint.Parse(args);
                client.EventActivate(EventID);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The ProcessReloadConfig.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessReloadConfig(MapClient client, string args)
        {
            try
            {
                switch (args.ToLower())
                {
                    case "ecoshop":
                        this.ProcessAnnounce(client, "Reloading ECOShop");
                        Factory<ECOShopFactory, ShopCategory>.Instance.Reload();
                        this.ProcessAnnounce(client, "Reloaded ECOShop");
                        break;
                    case "shopdb":
                        this.ProcessAnnounce(client, "Reloading ShopDB");
                        Factory<ShopFactory, Shop>.Instance.Reload();
                        this.ProcessAnnounce(client, "Reloaded ShopDB");
                        break;
                    case "monster":
                        this.ProcessAnnounce(client, "Reloading monster");
                        Singleton<MobFactory>.Instance.Mobs.Clear();
                        Singleton<MobFactory>.Instance.Init("./DB/monster.csv", Encoding.GetEncoding(Singleton<Configuration>.Instance.DBEncoding));
                        this.ProcessAnnounce(client, "Reloaded monster");
                        break;
                    case "quests":
                        this.ProcessAnnounce(client, "Reloading Quests");
                        Factory<QuestFactory, QuestInfo>.Instance.Reload();
                        this.ProcessAnnounce(client, "Reloaded Quests");
                        break;
                    case "treasure":
                        this.ProcessAnnounce(client, "Reloading Treasure");
                        FactoryString<TreasureFactory, TreasureList>.Instance.Reload();
                        this.ProcessAnnounce(client, "Reloaded Treasure");
                        break;
                    case "spawns":
                        this.ProcessAnnounce(client, "Reloading Spawns");
                        Singleton<MobSpawnManager>.Instance.Spawns.Clear();
                        Singleton<MobSpawnManager>.Instance.LoadSpawn("./DB/Spawns");
                        this.ProcessAnnounce(client, "Reloaded Spawns");
                        break;
                    case "theater":
                        this.ProcessAnnounce(client, "Reloading Theater");
                        FactoryList<TheaterFactory, Movie>.Instance.Reload();
                        this.ProcessAnnounce(client, "Reloaded Theater");
                        break;
                    case "synthese":
                        this.ProcessAnnounce(client, "Reloading SyntheseDB");
                        Factory<SyntheseFactory, SyntheseInfo>.Instance.Reload();
                        this.ProcessAnnounce(client, "Reloaded SyntheseDB");
                        break;
                    default:
                        this.ProcessAnnounce(client, "Reloading Configs");
                        Singleton<Configuration>.Instance.Initialization("./Config/SagaMap.xml");
                        this.ProcessAnnounce(client, "Reloaded Configs");
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The ProcessReloadScript.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessReloadScript(MapClient client, string args)
        {
            this.ProcessAnnounce(client, "Reloading Scripts");
            Singleton<ScriptManager>.Instance.ReloadScript();
            this.ProcessAnnounce(client, "Reloaded Scripts");
        }

        /// <summary>
        /// The ProcessEffect.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessEffect(MapClient client, string args)
        {
            client.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
            {
                effectID = uint.Parse(args),
                actorID = client.Character.ActorID
            }, (SagaDB.Actor.Actor)client.Character, true);
        }

        /// <summary>
        /// The ProcessRobot.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessRobot(MapClient client, string args)
        {
            if (client.AI == null)
            {
                client.AI = new MobAI((SagaDB.Actor.Actor)client.Character);
                client.AI.Mode = new AIMode();
                client.AI.Mode.mask.SetValue((object)AIFlag.Active, true);
            }
            if (client.AI.Activated)
                client.AI.Pause();
            else
                client.AI.Start();
        }

        /// <summary>
        /// The ProcessMode.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessMode(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_MODE_PARA);
            }
            else
            {
                try
                {
                    switch (args)
                    {
                        case "1":
                            foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                                mapClient.SendPkMode();
                            this.ProcessAnnounce(client, Singleton<LocalManager>.Instance.Strings.ATCOMMAND_PK_MODE_INFO);
                            break;
                        case "2":
                            foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                                mapClient.SendNormalMode();
                            this.ProcessAnnounce(client, Singleton<LocalManager>.Instance.Strings.ATCOMMAND_NORMAL_MODE_INFO);
                            break;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessInfo.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessInfo(MapClient client, string args)
        {
            byte num1 = Global.PosX16to8(client.Character.X, client.map.Width);
            byte num2 = Global.PosY16to8(client.Character.Y, client.map.Height);
            client.SendSystemMessage(client.Map.Name + " [" + num1.ToString() + "," + num2.ToString() + "]");
            client.SendSystemMessage("Fire:" + client.map.Info.fire[(int)num1, (int)num2].ToString());
            client.SendSystemMessage("Wind:" + client.map.Info.wind[(int)num1, (int)num2].ToString());
            client.SendSystemMessage("Water:" + client.map.Info.water[(int)num1, (int)num2].ToString());
            client.SendSystemMessage("Earth:" + client.map.Info.earth[(int)num1, (int)num2].ToString());
            client.SendSystemMessage("Holy:" + client.map.Info.holy[(int)num1, (int)num2].ToString());
            client.SendSystemMessage("Dark:" + client.map.Info.dark[(int)num1, (int)num2].ToString());
        }

        /// <summary>
        /// The ProcessJump.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessJump(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_JUMP_PARA);
            }
            else
            {
                try
                {
                    short x = client.Character.X;
                    short y = client.Character.Y;
                    uint mapId = client.Character.MapID;
                    client = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => c.Character.Name == args)).First<MapClient>();
                    client.Map.SendActorToMap((SagaDB.Actor.Actor)client.Character, mapId, x, y);
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessJump2.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessJump2(MapClient client, string args)
        {
            if (args == "")
                return;
            try
            {
                short x = client.Character.X;
                short y = client.Character.Y;
                uint mapId = client.Character.MapID;
                client = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => (int)c.Character.CharID == (int)uint.Parse(args))).First<MapClient>();
                client.Map.SendActorToMap((SagaDB.Actor.Actor)client.Character, mapId, x, y);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The ProcessBan.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessBan(MapClient client, string args)
        {
            if (!(args != ""))
                return;
            try
            {
                MapClient mapClient = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => c.Character.Name == args)).First<MapClient>();
                mapClient.Character.Account.Banned = true;
                mapClient.netIO.Disconnect();
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The ProcessJumpTo.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessJumpTo(MapClient client, string args)
        {
            if (args == "")
                return;
            try
            {
                MapClient mapClient = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => c.Character.Name == args)).First<MapClient>();
                short x = mapClient.Character.X;
                short y = mapClient.Character.Y;
                uint mapId = mapClient.Character.MapID;
                client.Map.SendActorToMap((SagaDB.Actor.Actor)client.Character, mapId, x, y);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The ProcessJumpTo2.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessJumpTo2(MapClient client, string args)
        {
            if (args == "")
                return;
            try
            {
                MapClient mapClient = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => (int)c.Character.CharID == (int)uint.Parse(args))).First<MapClient>();
                short x = mapClient.Character.X;
                short y = mapClient.Character.Y;
                uint mapId = mapClient.Character.MapID;
                client.Map.SendActorToMap((SagaDB.Actor.Actor)client.Character, mapId, x, y);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The ProcessSummon.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessSummon(MapClient client, string args)
        {
            uint mobID = 0;
            if (!(args != ""))
                return;
            int num1;
            switch (args.Split(' ').Length)
            {
                case 1:
                    num1 = 1;
                    mobID = uint.Parse(args);
                    break;
                case 2:
                    mobID = uint.Parse(args.Split(' ')[0]);
                    num1 = int.Parse(args.Split(' ')[1]);
                    break;
                default:
                    num1 = 1;
                    int num2 = (int)uint.Parse(args);
                    break;
            }
            try
            {
                for (int index = 1; index <= num1; ++index)
                    client.map.SpawnMob(mobID, (short)((int)client.Character.X + new Random().Next(1, 10)), (short)((int)client.Character.Y + new Random().Next(1, 10)), (short)2500, (SagaDB.Actor.Actor)client.Character);
            }
            catch
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_MOB_ERROR);
            }
        }

        /// <summary>
        /// The ProcessSummonMe.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessSummonMe(MapClient client, string args)
        {
            ActorPC character = client.Character;
            ActorShadow actorShadow = new ActorShadow(character);
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(character.MapID);
            actorShadow.Name = Singleton<LocalManager>.Instance.Strings.SKILL_DECOY + character.Name;
            actorShadow.MapID = character.MapID;
            actorShadow.X = character.X;
            actorShadow.Y = character.Y;
            actorShadow.MaxHP = character.MaxHP;
            actorShadow.HP = character.HP;
            actorShadow.Speed = character.Speed;
            actorShadow.BaseData.mobSize = 1f;
            PetEventHandler petEventHandler = new PetEventHandler((ActorPet)actorShadow);
            actorShadow.e = (ActorEventHandler)petEventHandler;
            petEventHandler.AI.Mode = new AIMode(1);
            petEventHandler.AI.Master = (SagaDB.Actor.Actor)character;
            map.RegisterActor((SagaDB.Actor.Actor)actorShadow);
            actorShadow.invisble = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorShadow);
            map.SendVisibleActorsToActor((SagaDB.Actor.Actor)actorShadow);
            petEventHandler.AI.Start();
        }

        /// <summary>
        /// The ProcessGridMob.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessGridMob(MapClient client, string args)
        {
            uint mobID = 0;
            if (!(args != ""))
                return;
            int num1;
            switch (args.Split(' ').Length)
            {
                case 1:
                    num1 = 1;
                    mobID = uint.Parse(args);
                    break;
                case 2:
                    mobID = uint.Parse(args.Split(' ')[0]);
                    num1 = int.Parse(args.Split(' ')[1]);
                    break;
                default:
                    num1 = 1;
                    int num2 = (int)uint.Parse(args);
                    break;
            }
            try
            {
                short x = client.Character.X;
                short y = client.Character.Y;
                int num3 = (int)x - num1 * 100;
                while (num3 <= (int)x + num1 * 100)
                {
                    int num4 = (int)y - num1 * 100;
                    while (num4 <= (int)y + num1 * 100)
                    {
                        if ((int)x != num3 || (int)y != num4)
                            ((MobEventHandler)client.map.SpawnMob(mobID, (short)num3, (short)num4, (short)50, (SagaDB.Actor.Actor)null).e).AI.Mode = new AIMode(4);
                        num4 += 100;
                    }
                    num3 += 100;
                }
            }
            catch
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_MOB_ERROR);
            }
        }

        /// <summary>
        /// The ProcessMob.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessMob(MapClient client, string args)
        {
            uint mobID = 0;
            if (!(args != ""))
                return;
            int num1;
            switch (args.Split(' ').Length)
            {
                case 1:
                    num1 = 1;
                    mobID = uint.Parse(args);
                    break;
                case 2:
                    mobID = uint.Parse(args.Split(' ')[0]);
                    num1 = int.Parse(args.Split(' ')[1]);
                    break;
                default:
                    num1 = 1;
                    int num2 = (int)uint.Parse(args);
                    break;
            }
            try
            {
                for (int index = 1; index <= num1; ++index)
                    client.map.SpawnMob(mobID, (short)((int)client.Character.X + new Random().Next(1, 10)), (short)((int)client.Character.Y + new Random().Next(1, 10)), (short)2500, (SagaDB.Actor.Actor)null);
            }
            catch
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_MOB_ERROR);
            }
        }

        /// <summary>
        /// The ProcessItem.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessItem(MapClient client, string args)
        {
            uint id = 0;
            uint num1 = 0;
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ITEM_PARA);
            }
            else
            {
                try
                {
                    int num2;
                    switch (args.Split(' ').Length)
                    {
                        case 1:
                            num2 = 1;
                            id = uint.Parse(args);
                            break;
                        case 2:
                            id = uint.Parse(args.Split(' ')[0]);
                            num2 = int.Parse(args.Split(' ')[1]);
                            break;
                        case 3:
                            id = uint.Parse(args.Split(' ')[0]);
                            num2 = int.Parse(args.Split(' ')[1]);
                            num1 = uint.Parse(args.Split(' ')[2]);
                            break;
                        default:
                            num2 = 1;
                            int num3 = (int)uint.Parse(args);
                            break;
                    }
                    SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(id);
                    if (obj != null)
                    {
                        obj.Stack = (ushort)num2;
                        if (num1 != 0U)
                            obj.PictID = num1;
                        client.AddItem(obj, true);
                    }
                    else
                        client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ITEM_NO_SUCH_ITEM);
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessWho.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessWho(MapClient client, string args)
        {
            client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
        }

        /// <summary>
        /// The ProcessWho2.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessWho2(MapClient client, string args)
        {
            foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
            {
                byte num1 = Global.PosX16to8(mapClient.Character.X, mapClient.map.Width);
                byte num2 = Global.PosY16to8(mapClient.Character.Y, mapClient.map.Height);
                client.SendSystemMessage(mapClient.Character.Name + "(CharID:" + (object)mapClient.Character.CharID + ")[" + mapClient.Map.Name + " " + num1.ToString() + "," + num2.ToString() + "]");
            }
            client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ONLINE_PLAYER_INFO + MapClientManager.Instance.OnlinePlayer.Count.ToString());
        }

        /// <summary>
        /// The ProcessWho3.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessWho3(MapClient client, string args)
        {
            int num1 = -1;
            if (args != "")
            {
                try
                {
                    num1 = int.Parse(args);
                }
                catch (Exception ex)
                {
                }
            }
            try
            {
                List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
                if (num1 == -1)
                {
                    foreach (KeyValuePair<uint, SagaDB.Actor.Actor> actor in client.map.Actors)
                        actorList.Add(actor.Value);
                }
                else
                    actorList = client.map.GetActorsArea((SagaDB.Actor.Actor)client.Character, (short)(num1 * 100), true);
                foreach (SagaDB.Actor.Actor actor in actorList)
                {
                    byte num2 = Global.PosX16to8(actor.X, client.map.Width);
                    byte num3 = Global.PosY16to8(actor.Y, client.map.Height);
                    switch (actor.type)
                    {
                        case ActorType.PC:
                            ActorPC actorPc = (ActorPC)actor;
                            client.SendSystemMessage(actorPc.Name + "(CharID:" + (object)actorPc.CharID + ")[" + num2.ToString() + "," + num3.ToString() + "]");
                            break;
                        case ActorType.MOB:
                            ActorMob actorMob = (ActorMob)actor;
                            client.SendSystemMessage(actorMob.BaseData.name + "(CharID:" + (object)actorMob.ActorID + ")[" + num2.ToString() + "," + num3.ToString() + "]");
                            break;
                        case ActorType.ITEM:
                            ActorItem actorItem = (ActorItem)actor;
                            client.SendSystemMessage(actorItem.Name + "(CharID:" + (object)actorItem.ActorID + ")[" + num2.ToString() + "," + num3.ToString() + "]");
                            break;
                        case ActorType.PET:
                            ActorPet actorPet = (ActorPet)actor;
                            client.SendSystemMessage(actorPet.BaseData.name + "(CharID:" + (object)actorPet.ActorID + ")[" + num2.ToString() + "," + num3.ToString() + "]");
                            break;
                        case ActorType.SHADOW:
                            ActorShadow actorShadow = (ActorShadow)actor;
                            client.SendSystemMessage(actorShadow.Name + "(CharID:" + (object)actorShadow.ActorID + ")[" + num2.ToString() + "," + num3.ToString() + "]");
                            break;
                    }
                }
                client.SendSystemMessage(string.Format("共：{0} 個Actors", (object)actorList.Count));
            }
            catch
            {
            }
        }

        /// <summary>
        /// The ProcessGo.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessGo(MapClient client, string args)
        {
            uint num;
            byte pos1;
            byte pos2;
            switch (uint.Parse(args))
            {
                case 1:
                    num = 10024000U;
                    pos1 = (byte)127;
                    pos2 = (byte)141;
                    break;
                case 2:
                    num = 10023000U;
                    pos1 = (byte)127;
                    pos2 = (byte)144;
                    break;
                default:
                    return;
            }
            try
            {
                if (Singleton<Configuration>.Instance.HostedMaps.Contains(num))
                {
                    SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(num);
                    client.Map.SendActorToMap((SagaDB.Actor.Actor)client.Character, num, Global.PosX8to16(pos1, map.Width), Global.PosY8to16(pos2, map.Height));
                }
            }
            catch (Exception ex)
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_WARP_ERROR);
            }
        }

        /// <summary>
        /// The ProcessWarp.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessWarp(MapClient client, string args)
        {
            string[] strArray = args.Split(' ');
            if (strArray[0] == "" || strArray[1] == "" || strArray[2] == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_WARP_PARA);
            }
            else
            {
                try
                {
                    uint num = uint.Parse(strArray[0]);
                    byte pos1 = byte.Parse(strArray[1]);
                    byte pos2 = byte.Parse(strArray[2]);
                    if (Singleton<Configuration>.Instance.HostedMaps.Contains(num))
                    {
                        SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(num);
                        client.Map.SendActorToMap((SagaDB.Actor.Actor)client.Character, num, Global.PosX8to16(pos1, map.Width), Global.PosY8to16(pos2, map.Height));
                    }
                }
                catch (Exception ex)
                {
                    client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_WARP_ERROR);
                }
            }
        }

        /// <summary>
        /// The ProcessPCall.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessPCall(MapClient client, string args)
        {
        }

        /// <summary>
        /// The ProcessHair.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessHair(MapClient client, string args)
        {
            string[] strArray = args.Split(' ');
            if (strArray[0] == "" || strArray[1] == "" || strArray[2] == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_HAIR_PAEA);
            }
            else
            {
                try
                {
                    byte num1 = byte.Parse(strArray[0]);
                    byte num2 = byte.Parse(strArray[1]);
                    byte num3 = byte.Parse(strArray[2]);
                    client.Character.HairStyle = num1;
                    client.Character.Wig = num2;
                    client.Character.HairColor = num3;
                    client.SendCharInfoUpdate();
                }
                catch (Exception ex)
                {
                    client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_HAIR_ERROR);
                }
            }
        }

        /// <summary>
        /// The ProcessHairext.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessHairext(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_HAIREXT_PARA);
            }
            else
            {
                try
                {
                    byte num = byte.Parse(args);
                    if (num >= (byte)1 && num <= (byte)52)
                    {
                        client.Character.Wig = (byte)((uint)num - 1U);
                        client.SendCharInfoUpdate();
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessLevel.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessLevel(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_LEVEL_PARA);
            }
            else
            {
                try
                {
                    byte num = byte.Parse(args);
                    if (client.map.Info.Flag.Test(MapFlags.Dominion))
                    {
                        client.Character.DominionCEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)num, LevelType.CLEVEL) + 1U;
                        Singleton<ExperienceManager>.Instance.CheckExp(client, LevelType.CLEVEL);
                        client.Character.DominionLevel = num;
                    }
                    else
                    {
                        client.Character.CEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)num, LevelType.CLEVEL) + 1U;
                        Singleton<ExperienceManager>.Instance.CheckExp(client, LevelType.CLEVEL);
                        client.Character.Level = num;
                    }
                    client.SendEXP();
                    client.SendPlayerLevel();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessJobLevel.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessJobLevel(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_LEVEL_PARA);
            }
            else
            {
                try
                {
                    byte num = byte.Parse(args);
                    if (client.map.Info.Flag.Test(MapFlags.Dominion))
                    {
                        client.Character.DominionJEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)num, LevelType.JLEVEL2) + 1U;
                        Singleton<ExperienceManager>.Instance.CheckExp(client, LevelType.JLEVEL2);
                        client.Character.DominionJobLevel = num;
                    }
                    else if (client.Character.Job == client.Character.JobBasic)
                    {
                        client.Character.JEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)num, LevelType.JLEVEL) + 1U;
                        Singleton<ExperienceManager>.Instance.CheckExp(client, LevelType.JLEVEL);
                        client.Character.JobLevel1 = num;
                    }
                    else
                    {
                        client.Character.JEXP = Singleton<ExperienceManager>.Instance.GetExpForLevel((uint)num, LevelType.JLEVEL2) + 1U;
                        Singleton<ExperienceManager>.Instance.CheckExp(client, LevelType.JLEVEL2);
                        if (client.Character.Job == client.Character.Job2X)
                            client.Character.JobLevel2X = num;
                        else
                            client.Character.JobLevel2T = num;
                    }
                    client.SendEXP();
                    client.SendPlayerLevel();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessHaircolor.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessHaircolor(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_HAIRCOLOR_PARA);
            }
            else
            {
                try
                {
                    uint.Parse(args);
                    if (client.Character.HairStyle == (byte)90 || client.Character.HairStyle == (byte)91 || client.Character.HairStyle == (byte)92)
                    {
                        client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_HAIRCOLOR_ERROR);
                    }
                    else
                    {
                        switch (args)
                        {
                            case "1":
                                client.Character.HairColor = (byte)0;
                                client.SendCharInfoUpdate();
                                break;
                            case "2":
                                client.Character.HairColor = (byte)1;
                                client.SendCharInfoUpdate();
                                break;
                            case "3":
                                client.Character.HairColor = (byte)2;
                                client.SendCharInfoUpdate();
                                break;
                            case "4":
                                client.Character.HairColor = (byte)3;
                                client.SendCharInfoUpdate();
                                break;
                            case "5":
                                client.Character.HairColor = (byte)4;
                                client.SendCharInfoUpdate();
                                break;
                            case "6":
                                client.Character.HairColor = (byte)5;
                                client.SendCharInfoUpdate();
                                break;
                            case "7":
                                client.Character.HairColor = (byte)6;
                                client.SendCharInfoUpdate();
                                break;
                            case "8":
                                client.Character.HairColor = (byte)7;
                                client.SendCharInfoUpdate();
                                break;
                            case "9":
                                client.Character.HairColor = (byte)8;
                                client.SendCharInfoUpdate();
                                break;
                            case "10":
                                client.Character.HairColor = (byte)9;
                                client.SendCharInfoUpdate();
                                break;
                            case "11":
                                client.Character.HairColor = (byte)10;
                                client.SendCharInfoUpdate();
                                break;
                            case "12":
                                client.Character.HairColor = (byte)11;
                                client.SendCharInfoUpdate();
                                break;
                            case "13":
                                client.Character.HairColor = (byte)12;
                                client.SendCharInfoUpdate();
                                break;
                            case "14":
                                client.Character.HairColor = (byte)50;
                                client.SendCharInfoUpdate();
                                break;
                            case "15":
                                client.Character.HairColor = (byte)51;
                                client.SendCharInfoUpdate();
                                break;
                            case "16":
                                client.Character.HairColor = (byte)52;
                                client.SendCharInfoUpdate();
                                break;
                            case "17":
                                client.Character.HairColor = (byte)60;
                                client.SendCharInfoUpdate();
                                break;
                            case "18":
                                client.Character.HairColor = (byte)61;
                                client.SendCharInfoUpdate();
                                break;
                            case "19":
                                client.Character.HairColor = (byte)62;
                                client.SendCharInfoUpdate();
                                break;
                            case "20":
                                client.Character.HairColor = (byte)70;
                                client.SendCharInfoUpdate();
                                break;
                            case "21":
                                client.Character.HairColor = (byte)71;
                                client.SendCharInfoUpdate();
                                break;
                            case "22":
                                client.Character.HairColor = (byte)72;
                                client.SendCharInfoUpdate();
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessHairstyle.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessHairstyle(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_HAIRSTYLE_PARA);
            }
            else
            {
                try
                {
                    uint.Parse(args);
                    switch (args)
                    {
                        case "1":
                            client.Character.HairStyle = (byte)90;
                            client.Character.HairColor = (byte)0;
                            client.SendCharInfoUpdate();
                            break;
                        case "2":
                            client.Character.HairStyle = (byte)91;
                            client.Character.HairColor = (byte)0;
                            client.SendCharInfoUpdate();
                            break;
                        case "3":
                            client.Character.HairStyle = (byte)92;
                            client.Character.HairColor = (byte)0;
                            client.SendCharInfoUpdate();
                            break;
                        case "4":
                            client.Character.HairStyle = (byte)2;
                            client.SendCharInfoUpdate();
                            break;
                        case "5":
                            client.Character.HairStyle = (byte)6;
                            client.SendCharInfoUpdate();
                            break;
                        case "6":
                            client.Character.HairStyle = (byte)11;
                            client.SendCharInfoUpdate();
                            break;
                        case "7":
                            client.Character.HairStyle = (byte)12;
                            client.SendCharInfoUpdate();
                            break;
                        case "8":
                            client.Character.HairStyle = (byte)13;
                            client.SendCharInfoUpdate();
                            break;
                        case "9":
                            client.Character.HairStyle = (byte)14;
                            client.SendCharInfoUpdate();
                            break;
                        case "10":
                            client.Character.HairStyle = (byte)15;
                            client.SendCharInfoUpdate();
                            break;
                        case "11":
                            client.Character.HairStyle = (byte)16;
                            client.SendCharInfoUpdate();
                            break;
                        case "12":
                            client.Character.HairStyle = (byte)17;
                            client.SendCharInfoUpdate();
                            break;
                        case "13":
                            client.Character.HairStyle = (byte)18;
                            client.SendCharInfoUpdate();
                            break;
                        case "14":
                            client.Character.HairStyle = (byte)19;
                            client.SendCharInfoUpdate();
                            break;
                        case "15":
                            client.Character.HairStyle = (byte)20;
                            client.SendCharInfoUpdate();
                            break;
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessPlayersize.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessPlayersize(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_PLAYERSIZE_PARA);
            }
            else
            {
                try
                {
                    uint num = uint.Parse(args);
                    client.Character.Size = num;
                    client.SendPlayerSizeUpdate();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessShowStatus.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessShowStatus(MapClient client, string args)
        {
            try
            {
                client.SendSystemMessage("------------------Status----------------");
                Status status = client.Character.Status;
                client.SendSystemMessage(string.Format("mp_recover_skill:{0}", (object)status.mp_recover_skill));
                client.SendSystemMessage("----------------------------------------");
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// The ProcessGold.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessGold(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_GOLD_PARA);
            }
            else
            {
                try
                {
                    uint num = uint.Parse(args);
                    client.Character.Gold = (int)num;
                    client.SendGoldUpdate();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessHide.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessHide(MapClient client, string args)
        {
            SagaDB.Actor.Actor character = (SagaDB.Actor.Actor)client.Character;
            character.Buff.Transparent = !client.Character.Buff.Transparent;
            Singleton<MapManager>.Instance.GetMap(character.MapID).SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, character, true);
        }

        /// <summary>
        /// The ProcessShoppoint.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessShoppoint(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_SHOPPOINT_PARA);
            }
            else
            {
                try
                {
                    uint num = uint.Parse(args);
                    client.Character.VShopPoints = num;
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessHeal.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessHeal(MapClient client, string args)
        {
            client.Character.HP = client.Character.MaxHP;
            client.Character.MP = client.Character.MaxMP;
            client.Character.SP = client.Character.MaxSP;
            client.Character.EP = client.Character.MaxEP;
            client.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)client.Character, true);
            client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_HEAL_MESSAGE);
        }

        /// <summary>
        /// The ProcessSkill.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessSkill(MapClient client, string args)
        {
            byte level = 0;
            uint num1 = 0;
            if (!(args != ""))
                return;
            switch (args.Split(' ').Length)
            {
                case 1:
                    num1 = uint.Parse(args);
                    break;
                case 2:
                    num1 = uint.Parse(args.Split(' ')[0]);
                    level = byte.Parse(args.Split(' ')[1]);
                    break;
                default:
                    int num2 = (int)uint.Parse(args);
                    break;
            }
            try
            {
                SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(num1, level);
                if (skill == null)
                    return;
                if (level == (byte)0)
                    skill.Level = skill.MaxLevel;
                client.Character.Skills.Add(num1, skill);
                Singleton<StatusFactory>.Instance.CalcStatus(client.Character);
                client.SendPlayerInfo();
            }
            catch
            {
            }
        }

        /// <summary>
        /// The ProcessSkillClear.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessSkillClear(MapClient client, string args)
        {
            int num = 0;
            if (args != "")
                num = int.Parse(args);
            switch (num)
            {
                case 0:
                    client.Character.Skills.Clear();
                    client.Character.Skills2.Clear();
                    client.Character.SkillsReserve.Clear();
                    break;
                case 1:
                    client.Character.Skills.Clear();
                    break;
                case 2:
                    client.Character.Skills2.Clear();
                    break;
                case 3:
                    client.Character.SkillsReserve.Clear();
                    break;
            }
            Singleton<StatusFactory>.Instance.CalcStatus(client.Character);
            client.SendPlayerInfo();
        }

        /// <summary>
        /// The ProcessAnnounce.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessAnnounce(MapClient client, string args)
        {
            if (args == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_ANNOUNCE_PARA);
            }
            else
            {
                try
                {
                    foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                        mapClient.SendAnnounce(args);
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessTweet.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessTweet(MapClient client, string args)
        {
            string twitterId = Singleton<Configuration>.Instance.TwitterID;
            string twitterPasswd = Singleton<Configuration>.Instance.TwitterPasswd;
            string name = client.Character.Name;
            int num = name.Length + args.Length;
            if (twitterId != null && twitterPasswd != null)
                ;
            if (args == "")
                client.SendSystemMessage("Error: NoTweetComment");
            else if (num >= 140)
            {
                client.SendSystemMessage("Error: TweetSizeOver");
            }
            else
            {
                try
                {
                    Encoding.GetEncoding("UTF-8");
                    string base64String = Convert.ToBase64String(Encoding.UTF8.GetBytes(twitterId + ":" + twitterPasswd));
                    byte[] bytes = Encoding.UTF8.GetBytes("status=" + name + ":" + args);
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://twitter.com/statuses/update.xml");
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ServicePoint.Expect100Continue = false;
                    httpWebRequest.UserAgent = "SagaECOJP";
                    httpWebRequest.Headers.Add("Authorization", "Basic " + base64String);
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    httpWebRequest.ContentLength = (long)bytes.Length;
                    Stream requestStream = httpWebRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                    try
                    {
                        foreach (MapClient mapClient in MapClientManager.Instance.OnlinePlayer)
                            mapClient.netIO.SendPacket((Packet)new SSMG_CHAT_WHOLE()
                            {
                                Sender = "Tweet",
                                Content = (name + ":" + args)
                            });
                    }
                    catch (Exception ex)
                    {
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessCh.
        /// </summary>
        /// <param name="client">.</param>
        /// <param name="args">.</param>
        private void ProcessCh(MapClient client, string args)
        {
        }

        /// <summary>
        /// The ProcessKick.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="playername">The playername<see cref="string"/>.</param>
        private void ProcessKick(MapClient client, string playername)
        {
            if (playername == "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_KICK_PARA);
            }
            else
            {
                try
                {
                    client = MapClientManager.Instance.OnlinePlayer.Where<MapClient>((Func<MapClient, bool>)(c => c.Character.Name == playername)).First<MapClient>();
                    client.netIO.Disconnect();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessODWarStart.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="arg">The arg<see cref="string"/>.</param>
        private void ProcessODWarStart(MapClient client, string arg)
        {
            ODWar.Instance.StartODWar(uint.Parse(arg));
        }

        /// <summary>
        /// The ProcessKillAllMob.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="arg">The arg<see cref="string"/>.</param>
        private void ProcessKillAllMob(MapClient client, string arg)
        {
            bool loot = false;
            if (arg == "1")
                loot = true;
            List<SagaDB.Actor.Actor> list = client.map.Actors.Values.ToList<SagaDB.Actor.Actor>();
            int num = 0;
            foreach (SagaDB.Actor.Actor actor in list)
            {
                if (actor.type == ActorType.MOB)
                {
                    MobEventHandler e = (MobEventHandler)actor.e;
                    actor.Buff.死んだふり = true;
                    e.OnDie(loot);
                    ++num;
                }
            }
            client.SendSystemMessage(num.ToString() + " mobs killed");
        }

        /// <summary>
        /// The ProcessKickGolem.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="arg">The arg<see cref="string"/>.</param>
        private void ProcessKickGolem(MapClient client, string arg)
        {
            ClientManager.LeaveCriticalArea();
            try
            {
                foreach (SagaDB.Actor.Actor actor in client.map.Actors.Values)
                {
                    if (actor.type == ActorType.GOLEM)
                    {
                        try
                        {
                            ActorGolem actorGolem = (ActorGolem)actor;
                            if (actorGolem.GolemType >= GolemType.Plant && actorGolem.GolemType <= GolemType.Strange)
                            {
                                MobEventHandler e = (MobEventHandler)actorGolem.e;
                                actorGolem.e = (ActorEventHandler)new NullEventHandler();
                                e.AI.Pause();
                            }
                            actorGolem.invisble = true;
                            client.map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorGolem);
                            actorGolem.ClearTaskAddition();
                            MapServer.charDB.SaveChar(actorGolem.Owner, false);
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            ClientManager.EnterCriticalArea();
        }

        /// <summary>
        /// The ProcessKickAll.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessKickAll(MapClient client, string args)
        {
            if (args != "")
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_KICKALL_PARA);
            }
            else
            {
                try
                {
                    foreach (SagaLib.Client client1 in MapClientManager.Instance.OnlinePlayer)
                        client1.netIO.Disconnect();
                }
                catch (Exception ex)
                {
                }
            }
        }

        /// <summary>
        /// The ProcessSpawn.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessSpawn(MapClient client, string args)
        {
            string[] strArray = args.Split(' ');
            if (strArray.Length < 4)
            {
                client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_SPAWN_PARA);
            }
            else
            {
                FileStream fileStream = new FileStream("autospawn.xml", FileMode.Append);
                StreamWriter streamWriter = new StreamWriter((Stream)fileStream);
                SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(client.Character.MapID);
                streamWriter.WriteLine("  <spawn>");
                streamWriter.WriteLine(string.Format("    <id>{0}</id>", (object)strArray[0]));
                streamWriter.WriteLine(string.Format("    <map>{0}</map>", (object)client.Character.MapID));
                streamWriter.WriteLine(string.Format("    <x>{0}</x>", (object)Global.PosX16to8(client.Character.X, map.Width)));
                streamWriter.WriteLine(string.Format("    <y>{0}</y>", (object)Global.PosY16to8(client.Character.Y, map.Height)));
                streamWriter.WriteLine(string.Format("    <amount>{0}</amount>", (object)strArray[1]));
                streamWriter.WriteLine(string.Format("    <range>{0}</range>", (object)strArray[2]));
                streamWriter.WriteLine(string.Format("    <delay>{0}</delay>", (object)strArray[3]));
                streamWriter.WriteLine("  </spawn>");
                streamWriter.Flush();
                fileStream.Flush();
                fileStream.Close();
                client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.ATCOMMAND_SPAWN_SUCCESS, (object)strArray[0], (object)strArray[1], (object)strArray[2], (object)strArray[3]));
            }
        }

        /// <summary>
        /// The ProcessCallMap.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessCallMap(MapClient client, string args)
        {
        }

        /// <summary>
        /// The ProcessCallAll.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessCallAll(MapClient client, string args)
        {
        }

        /// <summary>
        /// The ProcessRaw.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessRaw(MapClient client, string args)
        {
            byte[] numArray = Conversions.HexStr2Bytes(args.Replace(" ", ""));
            client.netIO.SendPacket(new Packet()
            {
                data = numArray
            });
        }

        /// <summary>
        /// The ProcessTest.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private void ProcessTest(MapClient client, string args)
        {
            Packet p1 = new Packet(24U);
            p1.ID = (ushort)7621;
            p1.offset += (ushort)6;
            p1.PutByte((byte)7);
            client.netIO.SendPacket(p1);
            Packet p2 = new Packet(38U);
            p2.ID = (ushort)7621;
            p2.PutByte((byte)1);
            p2.offset += (ushort)5;
            p2.PutByte((byte)7);
            p2.PutUShort((ushort)1);
            p2.PutUShort((ushort)2);
            p2.PutUShort((ushort)3);
            p2.PutUShort((ushort)4);
            p2.PutUShort((ushort)5);
            p2.PutUShort((ushort)6);
            p2.PutUShort((ushort)7);
            p2.PutByte((byte)7);
            p2.PutUShort((ushort)1);
            p2.PutUShort((ushort)2);
            p2.PutUShort((ushort)3);
            p2.PutUShort((ushort)4);
            p2.PutUShort((ushort)5);
            p2.PutUShort((ushort)6);
            p2.PutUShort((ushort)7);
            client.netIO.SendPacket(p2);
            Packet p3 = new Packet(59U);
            p3.ID = (ushort)7621;
            p3.PutByte((byte)2);
            p3.PutByte((byte)1);
            p3.PutUInt(1U);
            p3.PutByte((byte)1);
            p3.PutUShort((ushort)1);
            p3.PutByte((byte)1);
            p3.PutByte((byte)1);
            p3.PutByte((byte)1);
            p3.PutUInt(1U);
            p3.PutByte((byte)1);
            p3.PutUInt(1U);
            p3.PutByte((byte)7);
            p3.PutUShort((ushort)1);
            p3.PutUShort((ushort)2);
            p3.PutUShort((ushort)9);
            p3.PutUShort((ushort)4);
            p3.PutUShort((ushort)5);
            p3.PutUShort((ushort)6);
            p3.PutUShort((ushort)7);
            p3.PutByte((byte)7);
            p3.PutUShort((ushort)1);
            p3.PutUShort((ushort)2);
            p3.PutUShort((ushort)3);
            p3.PutUShort((ushort)4);
            p3.PutUShort((ushort)5);
            p3.PutUShort((ushort)6);
            p3.PutUShort((ushort)7);
            client.netIO.SendPacket(p3);
        }

        /// <summary>
        /// The ProcessCommandFunc.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="args">The args<see cref="string"/>.</param>
        private delegate void ProcessCommandFunc(MapClient client, string args);

        /// <summary>
        /// Defines the <see cref="CommandInfo" />.
        /// </summary>
        private class CommandInfo
        {
            /// <summary>
            /// Defines the func.
            /// </summary>
            public AtCommand.ProcessCommandFunc func;

            /// <summary>
            /// Defines the level.
            /// </summary>
            public uint level;

            /// <summary>
            /// Initializes a new instance of the <see cref="CommandInfo"/> class.
            /// </summary>
            /// <param name="func">The func<see cref="AtCommand.ProcessCommandFunc"/>.</param>
            /// <param name="lvl">The lvl<see cref="uint"/>.</param>
            public CommandInfo(AtCommand.ProcessCommandFunc func, uint lvl)
            {
                this.func = func;
                this.level = lvl;
            }
        }
    }
}
