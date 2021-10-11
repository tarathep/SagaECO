namespace SagaLogin.Network.Client
{
    using SagaDB;
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaLib;
    using SagaLogin.Configurations;
    using SagaLogin.Manager;
    using SagaLogin.Packets.Client;
    using SagaLogin.Packets.Map;
    using SagaLogin.Packets.Server;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;

    /// <summary>
    /// Defines the <see cref="LoginClient" />.
    /// </summary>
    public class LoginClient : SagaLib.Client
    {
        /// <summary>
        /// Defines the currentMap.
        /// </summary>
        public uint currentMap = 0;

        /// <summary>
        /// Defines the currentStatus.
        /// </summary>
        public CharStatus currentStatus = CharStatus.ONLINE;

        /// <summary>
        /// Defines the friendTarget.
        /// </summary>
        private LoginClient friendTarget = (LoginClient)null;

        /// <summary>
        /// Defines the IsMapServer.
        /// </summary>
        public bool IsMapServer = false;

        /// <summary>
        /// Defines the lv.
        /// </summary>
        public byte lv;

        /// <summary>
        /// Defines the joblv.
        /// </summary>
        public byte joblv;

        /// <summary>
        /// Defines the job.
        /// </summary>
        public PC_JOB job;

        /// <summary>
        /// Defines the selectedChar.
        /// </summary>
        public ActorPC selectedChar;

        /// <summary>
        /// Defines the server.
        /// </summary>
        private MapServer server;

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
        public Account account;

        /// <summary>
        /// Defines the state.
        /// </summary>
        public LoginClient.SESSION_STATE state;

        /// <summary>
        /// The OnFriendDelete.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FRIEND_DELETE"/>.</param>
        public void OnFriendDelete(CSMG_FRIEND_DELETE p)
        {
            if (this.selectedChar == null)
                return;
            LoginServer.charDB.DeleteFriend(this.selectedChar.CharID, p.CharID);
            LoginServer.charDB.DeleteFriend(p.CharID, this.selectedChar.CharID);
            this.netIO.SendPacket((Packet)new SSMG_FRIEND_DELETE()
            {
                CharID = p.CharID
            });
            LoginClient client = LoginClientManager.Instance.FindClient(p.CharID);
            if (client == null)
                return;
            client.netIO.SendPacket((Packet)new SSMG_FRIEND_DELETE()
            {
                CharID = this.selectedChar.CharID
            });
        }

        /// <summary>
        /// The OnFriendAdd.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FRIEND_ADD"/>.</param>
        public void OnFriendAdd(CSMG_FRIEND_ADD p)
        {
            LoginClient client = LoginClientManager.Instance.FindClient(p.CharID);
            if (client != null)
            {
                if (!LoginServer.charDB.IsFriend(this.selectedChar.CharID, client.selectedChar.CharID))
                {
                    this.friendTarget = client;
                    client.friendTarget = this;
                    client.netIO.SendPacket((Packet)new SSMG_FRIEND_ADD()
                    {
                        CharID = this.selectedChar.CharID,
                        Name = this.selectedChar.Name
                    });
                }
                else
                    this.netIO.SendPacket((Packet)new SSMG_FRIEND_ADD_FAILED()
                    {
                        AddResult = SSMG_FRIEND_ADD_FAILED.Result.TARGET_REFUSED
                    });
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_FRIEND_ADD_FAILED()
                {
                    AddResult = SSMG_FRIEND_ADD_FAILED.Result.CANNOT_FIND_TARGET
                });
        }

        /// <summary>
        /// The OnFriendAddReply.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FRIEND_ADD_REPLY"/>.</param>
        public void OnFriendAddReply(CSMG_FRIEND_ADD_REPLY p)
        {
            if (this.friendTarget == null)
            {
                this.netIO.SendPacket((Packet)new SSMG_FRIEND_ADD_FAILED()
                {
                    AddResult = SSMG_FRIEND_ADD_FAILED.Result.CANNOT_FIND_TARGET
                });
            }
            else
            {
                if (p.Reply == 1U)
                {
                    this.netIO.SendPacket((Packet)new SSMG_FRIEND_ADD_OK()
                    {
                        CharID = this.friendTarget.selectedChar.CharID
                    });
                    this.SendFriendAdd(this.friendTarget);
                    LoginServer.charDB.AddFriend(this.selectedChar, this.friendTarget.selectedChar.CharID);
                    this.friendTarget.netIO.SendPacket((Packet)new SSMG_FRIEND_ADD_OK()
                    {
                        CharID = this.selectedChar.CharID
                    });
                    this.friendTarget.SendFriendAdd(this);
                    LoginServer.charDB.AddFriend(this.friendTarget.selectedChar, this.selectedChar.CharID);
                }
                else
                {
                    this.netIO.SendPacket((Packet)new SSMG_FRIEND_ADD_FAILED()
                    {
                        AddResult = SSMG_FRIEND_ADD_FAILED.Result.TARGET_REFUSED
                    });
                    this.friendTarget.netIO.SendPacket((Packet)new SSMG_FRIEND_ADD_FAILED()
                    {
                        AddResult = SSMG_FRIEND_ADD_FAILED.Result.TARGET_REFUSED
                    });
                }
                this.friendTarget = (LoginClient)null;
            }
        }

        /// <summary>
        /// The OnFriendMapUpdate.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FRIEND_MAP_UPDATE"/>.</param>
        public void OnFriendMapUpdate(CSMG_FRIEND_MAP_UPDATE p)
        {
            if (this.selectedChar == null)
                return;
            List<ActorPC> friendList2 = LoginServer.charDB.GetFriendList2(this.selectedChar);
            this.currentMap = p.MapID;
            foreach (ActorPC pc in friendList2)
            {
                LoginClient client = LoginClientManager.Instance.FindClient(pc);
                SSMG_FRIEND_MAP_UPDATE ssmgFriendMapUpdate = new SSMG_FRIEND_MAP_UPDATE();
                ssmgFriendMapUpdate.CharID = this.selectedChar.CharID;
                if (client != null)
                {
                    ssmgFriendMapUpdate.MapID = this.currentMap;
                    client.netIO.SendPacket((Packet)ssmgFriendMapUpdate);
                }
            }
        }

        /// <summary>
        /// The OnFriendDetailUpdate.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_FRIEND_DETAIL_UPDATE"/>.</param>
        public void OnFriendDetailUpdate(CSMG_FRIEND_DETAIL_UPDATE p)
        {
            if (this.selectedChar == null)
                return;
            List<ActorPC> friendList2 = LoginServer.charDB.GetFriendList2(this.selectedChar);
            this.job = p.Job;
            this.lv = p.Level;
            this.joblv = p.Level;
            foreach (ActorPC pc in friendList2)
            {
                LoginClient client = LoginClientManager.Instance.FindClient(pc);
                SSMG_FRIEND_DETAIL_UPDATE friendDetailUpdate = new SSMG_FRIEND_DETAIL_UPDATE();
                friendDetailUpdate.CharID = this.selectedChar.CharID;
                if (client != null)
                {
                    friendDetailUpdate.Job = this.job;
                    friendDetailUpdate.Level = this.lv;
                    friendDetailUpdate.JobLevel = this.joblv;
                    client.netIO.SendPacket((Packet)friendDetailUpdate);
                }
            }
        }

        /// <summary>
        /// The SendFriendAdd.
        /// </summary>
        /// <param name="client">The client<see cref="LoginClient"/>.</param>
        public void SendFriendAdd(LoginClient client)
        {
            this.netIO.SendPacket((Packet)new SSMG_FRIEND_CHAR_INFO()
            {
                ActorPC = client.selectedChar,
                MapID = client.currentMap,
                Status = client.currentStatus,
                Comment = ""
            });
        }

        /// <summary>
        /// The SendFriendList.
        /// </summary>
        public void SendFriendList()
        {
            if (this.selectedChar == null)
                return;
            foreach (ActorPC friend in LoginServer.charDB.GetFriendList(this.selectedChar))
            {
                LoginClient client = LoginClientManager.Instance.FindClient(friend);
                SSMG_FRIEND_CHAR_INFO ssmgFriendCharInfo = new SSMG_FRIEND_CHAR_INFO();
                ssmgFriendCharInfo.ActorPC = friend;
                if (client != null)
                {
                    ssmgFriendCharInfo.MapID = client.currentMap;
                    ssmgFriendCharInfo.Status = client.currentStatus;
                }
                ssmgFriendCharInfo.Comment = "";
                this.netIO.SendPacket((Packet)ssmgFriendCharInfo);
            }
        }

        /// <summary>
        /// The SendStatusToFriends.
        /// </summary>
        public void SendStatusToFriends()
        {
            if (this.selectedChar == null)
                return;
            foreach (ActorPC pc in LoginServer.charDB.GetFriendList2(this.selectedChar))
            {
                LoginClient client = LoginClientManager.Instance.FindClient(pc);
                SSMG_FRIEND_STATUS_UPDATE friendStatusUpdate = new SSMG_FRIEND_STATUS_UPDATE();
                SSMG_FRIEND_DETAIL_UPDATE friendDetailUpdate = new SSMG_FRIEND_DETAIL_UPDATE();
                friendStatusUpdate.CharID = this.selectedChar.CharID;
                friendDetailUpdate.CharID = this.selectedChar.CharID;
                if (client != null)
                {
                    friendStatusUpdate.Status = this.currentStatus;
                    friendDetailUpdate.Job = this.selectedChar.Job;
                    friendDetailUpdate.Level = this.selectedChar.Level;
                    friendDetailUpdate.JobLevel = this.selectedChar.CurrentJobLevel;
                    client.netIO.SendPacket((Packet)friendStatusUpdate);
                    client.netIO.SendPacket((Packet)friendDetailUpdate);
                }
            }
        }

        /// <summary>
        /// The OnSendVersion.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SEND_VERSION"/>.</param>
        public void OnSendVersion(CSMG_SEND_VERSION p)
        {
            Logger.ShowInfo("Client(Version:" + p.GetVersion() + ") is trying to connect...");
            this.client_Version = p.GetVersion();
            SSMG_VERSION_ACK ssmgVersionAck = new SSMG_VERSION_ACK();
            ssmgVersionAck.SetResult(SSMG_VERSION_ACK.Result.OK);
            ssmgVersionAck.SetVersion(this.client_Version);
            this.netIO.SendPacket((Packet)ssmgVersionAck);
            SSMG_LOGIN_ALLOWED ssmgLoginAllowed = new SSMG_LOGIN_ALLOWED();
            this.frontWord = (uint)Global.Random.Next();
            this.backWord = (uint)Global.Random.Next();
            ssmgLoginAllowed.FrontWord = this.frontWord;
            ssmgLoginAllowed.BackWord = this.backWord;
            this.netIO.SendPacket((Packet)ssmgLoginAllowed);
        }

        /// <summary>
        /// The OnSendGUID.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_SEND_GUID"/>.</param>
        public void OnSendGUID(CSMG_SEND_GUID p)
        {
            this.netIO.SendPacket((Packet)new SSMG_LOGIN_ALLOWED());
        }

        /// <summary>
        /// The OnPing.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_PING"/>.</param>
        public void OnPing(CSMG_PING p)
        {
            this.netIO.SendPacket((Packet)new SSMG_PONG());
        }

        /// <summary>
        /// The OnLogin.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_LOGIN"/>.</param>
        public void OnLogin(CSMG_LOGIN p)
        {
            p.GetContent();
            if (Singleton<MapServerManager>.Instance.MapServers.Count == 0)
                this.netIO.SendPacket((Packet)new SSMG_LOGIN_ACK()
                {
                    LoginResult = SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_IPBLOCK
                });
            else if (LoginServer.accountDB.CheckPassword(p.UserName, p.Password, this.frontWord, this.backWord))
            {
                Account user = LoginServer.accountDB.GetUser(p.UserName);
                if (LoginClientManager.Instance.FindClientAccount(p.UserName) != null && user.GMLevel == (byte)0)
                {
                    LoginClientManager.Instance.FindClientAccount(p.UserName).netIO.Disconnect();
                    this.netIO.SendPacket((Packet)new SSMG_LOGIN_ACK()
                    {
                        LoginResult = SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_ALREADY
                    });
                }
                else
                {
                    this.account = user;
                    if (this.account.Banned)
                    {
                        this.netIO.SendPacket((Packet)new SSMG_LOGIN_ACK()
                        {
                            LoginResult = SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_BFALOCK
                        });
                    }
                    else
                    {
                        this.netIO.SendPacket((Packet)new SSMG_LOGIN_ACK()
                        {
                            LoginResult = SSMG_LOGIN_ACK.Result.OK
                        });
                        this.account.LastIP = this.netIO.sock.RemoteEndPoint.ToString().Split(':')[0];
                        uint[] charIds = LoginServer.charDB.GetCharIDs(this.account.AccountID);
                        this.account.Characters = new List<ActorPC>();
                        for (int index = 0; index < charIds.Length; ++index)
                        {
                            ActorPC actorPc = LoginServer.charDB.GetChar(charIds[index], false);
                            if (actorPc.QuestNextResetTime < DateTime.Now)
                            {
                                if ((DateTime.Now - actorPc.QuestNextResetTime).TotalDays > 1000.0)
                                {
                                    actorPc.QuestNextResetTime = DateTime.Now + new TimeSpan(1, 0, 0, 0);
                                }
                                else
                                {
                                    int totalDays = (int)(DateTime.Now - actorPc.QuestNextResetTime).TotalDays;
                                    actorPc.QuestRemaining += (ushort)((totalDays + 1) * 5);
                                    if (actorPc.QuestRemaining > (ushort)15)
                                        actorPc.QuestRemaining = (ushort)15;
                                }
                            }
                            this.account.Characters.Add(actorPc);
                        }
                        LoginServer.accountDB.WriteUser(this.account);
                        this.SendCharData();
                    }
                }
            }
            else
                this.netIO.SendPacket((Packet)new SSMG_LOGIN_ACK()
                {
                    LoginResult = SSMG_LOGIN_ACK.Result.GAME_SMSG_LOGIN_ERR_BADPASS
                });
        }

        /// <summary>
        /// The checkHairStyle.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAR_CREATE"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool checkHairStyle(CSMG_CHAR_CREATE p)
        {
            if (p.Gender == PC_GENDER.FEMALE)
                return p.HairStyle <= (byte)9 || p.HairStyle == (byte)14;
            return p.HairStyle <= (byte)9;
        }

        /// <summary>
        /// The checkHairColor.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAR_CREATE"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool checkHairColor(CSMG_CHAR_CREATE p)
        {
            if (p.Race == PC_RACE.DOMINION)
                return p.HairColor >= (byte)70 && p.HairColor <= (byte)72;
            if (p.Race == PC_RACE.EMIL)
                return p.HairColor >= (byte)50 && p.HairColor <= (byte)52;
            return p.Race != PC_RACE.TITANIA || (p.HairColor == (byte)7 || p.HairColor == (byte)60 || p.HairColor == (byte)61 || p.HairColor == (byte)62);
        }

        /// <summary>
        /// The OnCharCreate.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAR_CREATE"/>.</param>
        public void OnCharCreate(CSMG_CHAR_CREATE p)
        {
            SSMG_CHAR_CREATE_ACK ssmgCharCreateAck = new SSMG_CHAR_CREATE_ACK();
            if (p.Race != PC_RACE.DEM && (!this.checkHairColor(p) || !this.checkHairStyle(p)))
            {
                this.account.Banned = true;
                this.netIO.Disconnect();
                LoginServer.accountDB.WriteUser(this.account);
            }
            else
            {
                if (LoginServer.charDB.CharExists(p.Name))
                    ssmgCharCreateAck.CreateResult = SSMG_CHAR_CREATE_ACK.Result.GAME_SMSG_CHRCREATE_E_NAME_CONFLICT;
                else if (this.account.Characters.Where<ActorPC>((Func<ActorPC, bool>)(a => (int)a.Slot == (int)p.Slot)).Count<ActorPC>() != 0)
                {
                    ssmgCharCreateAck.CreateResult = SSMG_CHAR_CREATE_ACK.Result.GAME_SMSG_CHRCREATE_E_ALREADY_SLOT;
                }
                else
                {
                    ActorPC aChar = new ActorPC();
                    aChar.Name = p.Name;
                    aChar.Face = p.Face;
                    aChar.Gender = p.Gender;
                    aChar.HairColor = p.HairColor;
                    aChar.HairStyle = p.HairStyle;
                    aChar.Race = p.Race;
                    aChar.Slot = p.Slot;
                    aChar.Wig = byte.MaxValue;
                    aChar.Level = (byte)1;
                    aChar.JobLevel1 = (byte)1;
                    aChar.JobLevel2T = (byte)1;
                    aChar.JobLevel2X = (byte)1;
                    aChar.QuestRemaining = (ushort)3;
                    if (aChar.Race == PC_RACE.DEM)
                    {
                        aChar.EPLoginTime = DateTime.Now + new TimeSpan(1, 0, 0, 0);
                        aChar.EP = 0U;
                    }
                    else
                    {
                        aChar.EPLoginTime = new DateTime(2000, 1, 1);
                        aChar.EP = 30U;
                    }
                    aChar.MapID = Singleton<Configuration>.Instance.StartupSetting[aChar.Race].StartMap;
                    MapInfo mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[aChar.MapID];
                    aChar.X = Global.PosX8to16(Singleton<Configuration>.Instance.StartupSetting[aChar.Race].X, mapInfo.width);
                    aChar.Y = Global.PosY8to16(Singleton<Configuration>.Instance.StartupSetting[aChar.Race].Y, mapInfo.height);
                    aChar.Dir = (ushort)2;
                    aChar.HP = 900U;
                    aChar.MaxHP = 120U;
                    aChar.MP = 900U;
                    aChar.MaxMP = 220U;
                    aChar.SP = 900U;
                    aChar.MaxSP = 60U;
                    aChar.Str = Singleton<Configuration>.Instance.StartupSetting[aChar.Race].Str;
                    aChar.Dex = Singleton<Configuration>.Instance.StartupSetting[aChar.Race].Dex;
                    aChar.Int = Singleton<Configuration>.Instance.StartupSetting[aChar.Race].Int;
                    aChar.Vit = Singleton<Configuration>.Instance.StartupSetting[aChar.Race].Vit;
                    aChar.Agi = Singleton<Configuration>.Instance.StartupSetting[aChar.Race].Agi;
                    aChar.Mag = Singleton<Configuration>.Instance.StartupSetting[aChar.Race].Mag;
                    aChar.SkillPoint = (ushort)3;
                    aChar.StatsPoint = (ushort)2;
                    aChar.Gold = 0;
                    foreach (StartItem startItem in Singleton<Configuration>.Instance.StartItem[aChar.Race][aChar.Gender])
                    {
                        SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(startItem.ItemID);
                        obj.Stack = (ushort)startItem.Count;
                        int num = (int)aChar.Inventory.AddItem(startItem.Slot, obj);
                    }
                    LoginServer.charDB.CreateChar(aChar, this.account.AccountID);
                    this.account.Characters.Add(aChar);
                    ssmgCharCreateAck.CreateResult = SSMG_CHAR_CREATE_ACK.Result.OK;
                }
                this.netIO.SendPacket((Packet)ssmgCharCreateAck);
                this.SendCharData();
            }
        }

        /// <summary>
        /// The OnCharDelete.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAR_DELETE"/>.</param>
        public void OnCharDelete(CSMG_CHAR_DELETE p)
        {
            SSMG_CHAR_DELETE_ACK ssmgCharDeleteAck = new SSMG_CHAR_DELETE_ACK();
            ActorPC aChar = this.account.Characters.Where<ActorPC>((Func<ActorPC, bool>)(c => (int)c.Slot == (int)p.Slot)).First<ActorPC>();
            if (this.account.DeletePassword.ToLower() == p.DeletePassword.ToLower())
            {
                LoginServer.charDB.DeleteChar(aChar);
                this.account.Characters.Remove(aChar);
                ssmgCharDeleteAck.DeleteResult = SSMG_CHAR_DELETE_ACK.Result.OK;
            }
            else
                ssmgCharDeleteAck.DeleteResult = SSMG_CHAR_DELETE_ACK.Result.WRONG_DELETE_PASSWORD;
            this.netIO.SendPacket((Packet)ssmgCharDeleteAck);
            this.SendCharData();
        }

        /// <summary>
        /// The OnCharSelect.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAR_SELECT"/>.</param>
        public void OnCharSelect(CSMG_CHAR_SELECT p)
        {
            SSMG_CHAR_SELECT_ACK ssmgCharSelectAck = new SSMG_CHAR_SELECT_ACK();
            ActorPC actorPc = this.account.Characters.Where<ActorPC>((Func<ActorPC, bool>)(c => (int)c.Slot == (int)p.Slot)).First<ActorPC>();
            this.selectedChar = actorPc;
            ssmgCharSelectAck.MapID = actorPc.MapID;
            this.netIO.SendPacket((Packet)ssmgCharSelectAck);
        }

        /// <summary>
        /// The OnRequestMapServer.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_REQUEST_MAP_SERVER"/>.</param>
        public void OnRequestMapServer(CSMG_REQUEST_MAP_SERVER p)
        {
            SSMG_SEND_TO_MAP_SERVER ssmgSendToMapServer = new SSMG_SEND_TO_MAP_SERVER();
            if (Singleton<MapServerManager>.Instance.MapServers.ContainsKey(this.selectedChar.MapID))
            {
                MapServer mapServer = Singleton<MapServerManager>.Instance.MapServers[this.selectedChar.MapID];
                ssmgSendToMapServer.ServerID = (byte)1;
                ssmgSendToMapServer.IP = mapServer.IP;
                ssmgSendToMapServer.Port = mapServer.port;
            }
            else if (Singleton<MapServerManager>.Instance.MapServers.ContainsKey(this.selectedChar.MapID / 1000U * 1000U))
            {
                MapServer mapServer = Singleton<MapServerManager>.Instance.MapServers[this.selectedChar.MapID / 1000U * 1000U];
                ssmgSendToMapServer.ServerID = (byte)1;
                ssmgSendToMapServer.IP = mapServer.IP;
                ssmgSendToMapServer.Port = mapServer.port;
            }
            else
            {
                Logger.ShowWarning("No map server registered for mapID:" + (object)this.selectedChar.MapID);
                ssmgSendToMapServer.ServerID = byte.MaxValue;
                ssmgSendToMapServer.IP = "127.0.0.001";
                ssmgSendToMapServer.Port = 10000;
            }
            this.netIO.SendPacket((Packet)ssmgSendToMapServer);
        }

        /// <summary>
        /// The OnCharStatus.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAR_STATUS"/>.</param>
        public void OnCharStatus(CSMG_CHAR_STATUS p)
        {
            this.netIO.SendPacket((Packet)new SSMG_CHAR_STATUS());
            this.SendFriendList();
            this.SendStatusToFriends();
        }

        /// <summary>
        /// The SendCharData.
        /// </summary>
        private void SendCharData()
        {
            this.netIO.SendPacket((Packet)new SSMG_CHAR_DATA()
            {
                Chars = this.account.Characters
            });
            this.netIO.SendPacket((Packet)new SSMG_CHAR_EQUIP()
            {
                Characters = this.account.Characters
            });
        }

        /// <summary>
        /// The OnInternMapRequestConfig.
        /// </summary>
        /// <param name="p">The p<see cref="INTERN_LOGIN_REQUEST_CONFIG"/>.</param>
        public void OnInternMapRequestConfig(INTERN_LOGIN_REQUEST_CONFIG p)
        {
            Singleton<Configuration>.Instance.Version = p.Version;
            this.netIO.SendPacket((Packet)new INTERN_LOGIN_REQUEST_CONFIG_ANSWER()
            {
                AuthOK = (this.server.Password == Singleton<Configuration>.Instance.Password),
                StartupSetting = Singleton<Configuration>.Instance.StartupSetting
            });
            Logger.ShowInfo(string.Format("Mapserver:{0}:{1} is requesting configuration...", (object)this.server.IP, (object)this.server.port));
        }

        /// <summary>
        /// The OnInternMapRegister.
        /// </summary>
        /// <param name="p">The p<see cref="INTERN_LOGIN_REGISTER"/>.</param>
        public void OnInternMapRegister(INTERN_LOGIN_REGISTER p)
        {
            MapServer mapServer1 = p.MapServer;
            this.IsMapServer = true;
            if (this.server == null)
            {
                this.server = mapServer1;
                if (mapServer1.Password != Singleton<Configuration>.Instance.Password)
                {
                    Logger.ShowWarning(string.Format("Mapserver:{0}:{1} is trying to register maps with wrong password:{2}", (object)mapServer1.IP, (object)mapServer1.port, (object)mapServer1.Password));
                    return;
                }
            }
            else
            {
                if (mapServer1.Password != Singleton<Configuration>.Instance.Password)
                {
                    Logger.ShowWarning(string.Format("Mapserver:{0}:{1} is trying to register maps with wrong password:{2}", (object)mapServer1.IP, (object)mapServer1.port, (object)mapServer1.Password));
                    return;
                }
                foreach (uint hostedMap in mapServer1.HostedMaps)
                {
                    if (!this.server.HostedMaps.Contains(hostedMap))
                        this.server.HostedMaps.Add(hostedMap);
                }
            }
            int num = 0;
            foreach (uint hostedMap in mapServer1.HostedMaps)
            {
                if (!Singleton<MapServerManager>.Instance.MapServers.ContainsKey(hostedMap))
                {
                    Singleton<MapServerManager>.Instance.MapServers.Add(hostedMap, this.server);
                    ++num;
                }
                else
                {
                    MapServer mapServer2 = Singleton<MapServerManager>.Instance.MapServers[hostedMap];
                    Logger.ShowWarning(string.Format("MapID:{0} was already hosted by Mapserver:{1}:{2}, skiping...", (object)hostedMap, (object)mapServer2.IP, (object)mapServer2.port));
                }
            }
            Logger.ShowInfo(string.Format("{0} maps registered for MapServer:{1}:{2}...", (object)num, (object)mapServer1.IP, (object)mapServer1.port));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginClient"/> class.
        /// </summary>
        /// <param name="mSock">The mSock<see cref="Socket"/>.</param>
        /// <param name="mCommandTable">The mCommandTable<see cref="Dictionary{ushort, Packet}"/>.</param>
        public LoginClient(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, (SagaLib.Client)this);
            this.netIO.SetMode(NetIO.Mode.Server);
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
                if (this.netIO != null)
                    return this.netIO.sock.RemoteEndPoint.ToString();
                return nameof(LoginClient);
            }
            catch (Exception ex)
            {
                return nameof(LoginClient);
            }
        }

        /// <summary>
        /// The OnConnect.
        /// </summary>
        public override void OnConnect()
        {
        }

        /// <summary>
        /// The OnDisconnect.
        /// </summary>
        public override void OnDisconnect()
        {
            if (this.currentStatus != CharStatus.OFFLINE)
            {
                if (this.IsMapServer)
                {
                    Logger.ShowWarning("A map server has just disconnected...");
                    foreach (uint hostedMap in this.server.HostedMaps)
                    {
                        if (Singleton<MapServerManager>.Instance.MapServers.ContainsKey(hostedMap))
                            Singleton<MapServerManager>.Instance.MapServers.Remove(hostedMap);
                    }
                }
                else
                {
                    this.currentStatus = CharStatus.OFFLINE;
                    this.currentMap = 0U;
                    try
                    {
                        this.SendStatusToFriends();
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                    if (this.account != null)
                        Logger.ShowInfo(this.account.Name + " logged out.");
                }
            }
            if (!LoginClientManager.Instance.Clients.Contains(this))
                return;
            LoginClientManager.Instance.Clients.Remove(this);
        }

        /// <summary>
        /// The OnWRPRequest.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_WRP_REQUEST"/>.</param>
        public void OnWRPRequest(CSMG_WRP_REQUEST p)
        {
            this.netIO.SendPacket((Packet)new SSMG_WRP_LIST()
            {
                RankingList = LoginServer.charDB.GetWRPRanking()
            });
        }

        /// <summary>
        /// The OnChatWhisper.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_CHAT_WHISPER"/>.</param>
        public void OnChatWhisper(CSMG_CHAT_WHISPER p)
        {
            if (this.selectedChar == null)
                return;
            LoginClient client = LoginClientManager.Instance.FindClient(p.Receiver);
            if (client != null)
                client.netIO.SendPacket((Packet)new SSMG_CHAT_WHISPER()
                {
                    Sender = this.selectedChar.Name,
                    Content = p.Content
                });
            else
                this.netIO.SendPacket((Packet)new SSMG_CHAT_WHISPER_FAILED()
                {
                    Receiver = p.Receiver,
                    Result = uint.MaxValue
                });
        }

        /// <summary>
        /// The OnRingEmblemNew.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_RING_EMBLEM_NEW"/>.</param>
        public void OnRingEmblemNew(CSMG_RING_EMBLEM_NEW p)
        {
            bool needUpdate;
            DateTime newTime;
            byte[] ringEmblem = LoginServer.charDB.GetRingEmblem(p.RingID, new DateTime(1970, 1, 1), out needUpdate, out newTime);
            this.SendRingEmblem(p.RingID, ringEmblem, needUpdate, newTime);
        }

        /// <summary>
        /// The OnRingEmblem.
        /// </summary>
        /// <param name="p">The p<see cref="CSMG_RING_EMBLEM"/>.</param>
        public void OnRingEmblem(CSMG_RING_EMBLEM p)
        {
            bool needUpdate;
            DateTime newTime;
            byte[] ringEmblem = LoginServer.charDB.GetRingEmblem(p.RingID, p.UpdateTime, out needUpdate, out newTime);
            this.SendRingEmblem(p.RingID, ringEmblem, needUpdate, newTime);
        }

        /// <summary>
        /// The SendRingEmblem.
        /// </summary>
        /// <param name="ringid">The ringid<see cref="uint"/>.</param>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="needUpdate">The needUpdate<see cref="bool"/>.</param>
        /// <param name="newDate">The newDate<see cref="DateTime"/>.</param>
        private void SendRingEmblem(uint ringid, byte[] data, bool needUpdate, DateTime newDate)
        {
            SSMG_RING_EMBLEM ssmgRingEmblem = new SSMG_RING_EMBLEM();
            ssmgRingEmblem.Result = !needUpdate ? 1 : 0;
            ssmgRingEmblem.RingID = ringid;
            if (data != null)
            {
                ssmgRingEmblem.Result2 = (byte)0;
                if (needUpdate)
                    ssmgRingEmblem.Data = data;
                ssmgRingEmblem.UpdateTime = newDate;
            }
            else
                ssmgRingEmblem.Result2 = (byte)1;
            this.netIO.SendPacket((Packet)ssmgRingEmblem);
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
            /// Defines the MAP.
            /// </summary>
            MAP,

            /// <summary>
            /// Defines the REDIRECTING.
            /// </summary>
            REDIRECTING,

            /// <summary>
            /// Defines the DISCONNECTED.
            /// </summary>
            DISCONNECTED,
        }
    }
}
