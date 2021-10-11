namespace SagaDB
{
    using MySql.Data.MySqlClient;
    using SagaDB.Actor;
    using SagaDB.BBS;
    using SagaDB.FGarden;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Quests;
    using SagaDB.Skill;
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="MySQLActorDB" />.
    /// </summary>
    public class MySQLActorDB : MySQLConnectivity, ActorDB
    {
        /// <summary>
        /// Defines the encoder.
        /// </summary>
        private Encoding encoder = Encoding.UTF8;

        /// <summary>
        /// Defines the tick.
        /// </summary>
        private DateTime tick = DateTime.Now;

        /// <summary>
        /// Defines the host.
        /// </summary>
        private string host;

        /// <summary>
        /// Defines the port.
        /// </summary>
        private string port;

        /// <summary>
        /// Defines the database.
        /// </summary>
        private string database;

        /// <summary>
        /// Defines the dbuser.
        /// </summary>
        private string dbuser;

        /// <summary>
        /// Defines the dbpass.
        /// </summary>
        private string dbpass;

        /// <summary>
        /// Defines the isconnected.
        /// </summary>
        private bool isconnected;

        /// <summary>
        /// Initializes a new instance of the <see cref="MySQLActorDB"/> class.
        /// </summary>
        /// <param name="host">The host<see cref="string"/>.</param>
        /// <param name="port">The port<see cref="int"/>.</param>
        /// <param name="database">The database<see cref="string"/>.</param>
        /// <param name="user">The user<see cref="string"/>.</param>
        /// <param name="pass">The pass<see cref="string"/>.</param>
        public MySQLActorDB(string host, int port, string database, string user, string pass)
        {
            this.host = host;
            this.port = port.ToString();
            this.dbuser = user;
            this.dbpass = pass;
            this.database = database;
            this.isconnected = false;
            try
            {
                this.db = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;SslMode=none;", (object)database, (object)host, (object)port, (object)user, (object)pass));
                this.dbinactive = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;SslMode=none;", (object)database, (object)host, (object)port, (object)user, (object)pass));
                this.db.Open();
            }
            catch (MySqlException ex)
            {
                SagaLib.Logger.ShowSQL((Exception)ex, (SagaLib.Logger)null);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex, (SagaLib.Logger)null);
            }
            if (this.db == null)
                return;
            if (this.db.State != ConnectionState.Closed)
                this.isconnected = true;
            else
                Console.WriteLine("SQL Connection error");
        }

        /// <summary>
        /// The Connect.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Connect()
        {
            if (!this.isconnected)
            {
                if (this.db.State == ConnectionState.Open)
                {
                    this.isconnected = true;
                    return true;
                }
                try
                {
                    this.db.Open();
                }
                catch (Exception ex)
                {
                }
                if (this.db != null && this.db.State == ConnectionState.Closed)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// The isConnected.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool isConnected()
        {
            if (this.isconnected)
            {
                if ((DateTime.Now - this.tick).TotalMinutes > 5.0)
                {
                    SagaLib.Logger.ShowSQL("ActorDB:Pinging SQL Server to keep the connection alive", (SagaLib.Logger)null);
                    bool blocked = ClientManager.Blocked;
                    if (blocked)
                        ClientManager.LeaveCriticalArea();
                    DatabaseWaitress.EnterCriticalArea();
                    MySqlConnection mySqlConnection = this.dbinactive;
                    if (mySqlConnection.State == ConnectionState.Open)
                        mySqlConnection.Close();
                    try
                    {
                        mySqlConnection.Open();
                    }
                    catch (Exception ex)
                    {
                        mySqlConnection = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;", (object)this.database, (object)this.host, (object)this.port, (object)this.dbuser, (object)this.dbpass));
                        mySqlConnection.Open();
                    }
                    this.dbinactive = this.db;
                    this.db = mySqlConnection;
                    this.tick = DateTime.Now;
                    DatabaseWaitress.LeaveCriticalArea();
                    if (blocked)
                        ClientManager.EnterCriticalArea();
                }
                if (this.db.State == ConnectionState.Broken || this.db.State == ConnectionState.Closed)
                    this.isconnected = false;
            }
            return this.isconnected;
        }

        /// <summary>
        /// The CreateChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        /// <param name="account_id">The account_id<see cref="int"/>.</param>
        public void CreateChar(ActorPC aChar, int account_id)
        {
            uint index = 0;
            if (aChar == null || !this.isConnected())
                return;
            string name = aChar.Name;
            this.CheckSQLString(ref name);
            MapInfo mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[aChar.MapID];
            string sqlstr1 = string.Format("INSERT INTO `char`(`account_id`,`name`,`race`,`gender`,`hairStyle`,`hairColor`,`wig`,`face`,`job`,`mapID`,`lv`,`jlv1`,`jlv2x`,`jlv2t`,`questRemaining`,`slot`,`x`,`y`,`dir`,`hp`,`max_hp`,`mp`,`max_mp`,`sp`,`max_sp`,`str`,`dex`,`intel`,`vit`,`agi`,`mag`,`statspoint`,`skillpoint`,`skillpoint2x`,`skillpoint2t`,`gold`,`ep`,`eplogindate`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}');", (object)account_id, (object)name, (object)(byte)aChar.Race, (object)(byte)aChar.Gender, (object)aChar.HairStyle, (object)aChar.HairColor, (object)aChar.Wig, (object)aChar.Face, (object)(byte)aChar.Job, (object)aChar.MapID, (object)aChar.Level, (object)aChar.JobLevel1, (object)aChar.JobLevel2X, (object)aChar.JobLevel2T, (object)aChar.QuestRemaining, (object)aChar.Slot, (object)Global.PosX16to8(aChar.X, mapInfo.width), (object)Global.PosY16to8(aChar.Y, mapInfo.height), (object)(byte)((uint)aChar.Dir / 45U), (object)aChar.HP, (object)aChar.MaxHP, (object)aChar.MP, (object)aChar.MaxMP, (object)aChar.SP, (object)aChar.MaxSP, (object)aChar.Str, (object)aChar.Dex, (object)aChar.Int, (object)aChar.Vit, (object)aChar.Agi, (object)aChar.Mag, (object)aChar.StatsPoint, (object)aChar.SkillPoint, (object)aChar.SkillPoint2X, (object)aChar.SkillPoint2T, (object)aChar.Gold, (object)aChar.EP, (object)this.ToSQLDateTime(aChar.EPLoginTime));
            try
            {
                this.SQLExecuteScalar(sqlstr1, out index);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            aChar.CharID = index;
            MySqlCommand cmd1 = new MySqlCommand(string.Format("INSERT INTO `inventory`(`char_id`,`data`) VALUES ('{0}',?data);\r\n", (object)aChar.CharID));
            cmd1.Parameters.Add("?data", MySqlDbType.Blob).Value = (object)aChar.Inventory.ToBytes();
            try
            {
                this.SQLExecuteNonQuery(cmd1);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            if (aChar.Inventory.WareHouse != null)
            {
                DataRowCollection dataRowCollection = (DataRowCollection)null;
                string sqlstr2 = "SELECT count(*) FROM `warehouse` WHERE `account_id`='" + (object)account_id + "' LIMIT 1;";
                try
                {
                    dataRowCollection = this.SQLExecuteQuery(sqlstr2);
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
                if (Convert.ToInt32(dataRowCollection[0][0]) == 0)
                {
                    MySqlCommand cmd2 = new MySqlCommand(string.Format("INSERT INTO `warehouse`(`account_id`,`data`) VALUES ('{0}',?data);\r\n", (object)account_id));
                    cmd2.Parameters.Add("?data", MySqlDbType.Blob).Value = (object)aChar.Inventory.WareToBytes();
                    try
                    {
                        this.SQLExecuteNonQuery(cmd2);
                    }
                    catch (Exception ex)
                    {
                        SagaLib.Logger.ShowError(ex);
                    }
                }
            }
            aChar.Inventory.WareHouse = (Dictionary<WarehousePlace, List<SagaDB.Item.Item>>)null;
            this.SaveItem(aChar);
        }

        /// <summary>
        /// The getCharID.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        private uint getCharID(string name)
        {
            DataRowCollection dataRowCollection = (DataRowCollection)null;
            string sqlstr = "SELECT `char_id` FROM `char` WHERE name='" + name + "' LIMIT 1";
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (MySqlException ex)
            {
                SagaLib.Logger.ShowSQL((Exception)ex, (SagaLib.Logger)null);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            return (uint)dataRowCollection[0]["charID"];
        }

        /// <summary>
        /// The SaveChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        public void SaveChar(ActorPC aChar)
        {
            this.SaveChar(aChar, true);
        }

        /// <summary>
        /// The SaveChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        /// <param name="fullinfo">The fullinfo<see cref="bool"/>.</param>
        public void SaveChar(ActorPC aChar, bool fullinfo)
        {
            this.SaveChar(aChar, true, fullinfo);
        }

        /// <summary>
        /// The SaveChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        /// <param name="itemInfo">The itemInfo<see cref="bool"/>.</param>
        /// <param name="fullinfo">The fullinfo<see cref="bool"/>.</param>
        public void SaveChar(ActorPC aChar, bool itemInfo, bool fullinfo)
        {
            MapInfo mapInfo = (MapInfo)null;
            if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(aChar.MapID))
                mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[aChar.MapID];
            else if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(aChar.MapID / 1000U * 1000U))
                mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[aChar.MapID / 1000U * 1000U];
            if (aChar == null)
                return;
            uint num1 = 0;
            uint num2 = 0;
            uint num3 = 0;
            uint num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            DateTime date = DateTime.Now;
            QuestStatus questStatus = QuestStatus.OPEN;
            if (aChar.Quest != null)
            {
                num1 = aChar.Quest.ID;
                num5 = aChar.Quest.CurrentCount1;
                num6 = aChar.Quest.CurrentCount2;
                num7 = aChar.Quest.CurrentCount3;
                date = aChar.Quest.EndTime;
                questStatus = aChar.Quest.Status;
            }
            if (aChar.Party != null)
                num2 = aChar.Party.ID;
            if (aChar.Ring != null)
                num3 = aChar.Ring.ID;
            if (aChar.Golem != null)
                num4 = aChar.Golem.ActorID;
            uint num8;
            byte num9;
            byte num10;
            if (mapInfo != null)
            {
                num8 = aChar.MapID;
                num9 = Global.PosX16to8(aChar.X, mapInfo.width);
                num10 = Global.PosY16to8(aChar.Y, mapInfo.height);
            }
            else
            {
                num8 = aChar.SaveMap;
                num9 = aChar.SaveX;
                num10 = aChar.SaveY;
            }
            bool online = aChar.Online;
            byte num11 = 0;
            if (aChar.DominionReserveSkill)
                num11 = (byte)1;
            aChar.Online = false;
            string sqlstr = string.Format("UPDATE `char` SET `name`='{0}',`race`='{1}',`gender`='{2}',`hairStyle`='{3}',`hairColor`='{4}',`wig`='{5}',`face`='{6}',`job`='{7}',`mapID`='{8}',`lv`='{9}',`jlv1`='{10}',`jlv2x`='{11}',`jlv2t`='{12}',`questRemaining`='{13}',`slot`='{14}',`x`='{16}',`y`='{17}',`dir`='{18}',`hp`='{19}',`max_hp`='{20}',`mp`='{21}',`max_mp`='{22}',`sp`='{23}',`max_sp`='{24}',`str`='{25}',`dex`='{26}',`intel`='{27}',`vit`='{28}',`agi`='{29}',`mag`='{30}',`statspoint`='{31}',`skillpoint`='{32}',`skillpoint2x`='{33}',`skillpoint2t`='{34}',`gold`='{35}',`cexp`='{36}',`jexp`='{37}',`save_map`='{38}',`save_x`='{39}',`save_y`='{40}',`possession_target`='{41}',`questid`='{42}',`questendtime`='{43}',`queststatus`='{44}',`questcurrentcount1`='{45}',`questcurrentcount2`='{46}',`questcurrentcount3`='{47}',`questresettime`='{48}',`fame`='{49}',`party`='{50}',`ring`='{51}',`golem`='{52}',`stamp1`='{53}',`stamp2`='{54}',`stamp3`='{55}',`stamp4`='{56}',`stamp5`='{57}',`stamp6`='{58}',`stamp7`='{59}',`stamp8`='{60}',`stamp9`='{61}',`stamp10`='{62}',`stamp11`='{63}',`cp`='{64}',`ecoin`='{65}',`dominionlv`='{66}',`dominionjlv`='{67}',`jointjlv`='{68}',`dcexp`='{69}',`djexp`='{70}',`jjexp`='{71}',`wrp`='{72}',`dstr`='{73}',`ddex`='{74}',`dintel`='{75}',`dvit`='{76}',`dagi`='{77}',`dmag`='{78}',`dstatpoint`='{79}',`dreserve`='{80}',`ep`='{81}',`eplogindate`='{82}',`epgreetingdate`='{83}',`cl`='{84}',`dcl`='{85}',`epused`='{86}',`depused`='{87}' WHERE char_id='{15}' LIMIT 1", (object)this.CheckSQLString(aChar.Name), (object)(byte)aChar.Race, (object)(byte)aChar.Gender, (object)aChar.HairStyle, (object)aChar.HairColor, (object)aChar.Wig, (object)aChar.Face, (object)(byte)aChar.Job, (object)num8, (object)aChar.Level, (object)aChar.JobLevel1, (object)aChar.JobLevel2X, (object)aChar.JobLevel2T, (object)aChar.QuestRemaining, (object)aChar.Slot, (object)aChar.CharID, (object)num9, (object)num10, (object)(byte)((uint)aChar.Dir / 45U), (object)aChar.HP, (object)aChar.MaxHP, (object)aChar.MP, (object)aChar.MaxMP, (object)aChar.SP, (object)aChar.MaxSP, (object)aChar.Str, (object)aChar.Dex, (object)aChar.Int, (object)aChar.Vit, (object)aChar.Agi, (object)aChar.Mag, (object)aChar.StatsPoint, (object)aChar.SkillPoint, (object)aChar.SkillPoint2X, (object)aChar.SkillPoint2T, (object)aChar.Gold, (object)aChar.CEXP, (object)aChar.JEXP, (object)aChar.SaveMap, (object)aChar.SaveX, (object)aChar.SaveY, (object)aChar.PossessionTarget, (object)num1, (object)this.ToSQLDateTime(date), (object)(byte)questStatus, (object)num5, (object)num6, (object)num7, (object)this.ToSQLDateTime(aChar.QuestNextResetTime), (object)aChar.Fame, (object)num2, (object)num3, (object)num4, (object)aChar.Stamp[StampGenre.Special].Value, (object)aChar.Stamp[StampGenre.Pururu].Value, (object)aChar.Stamp[StampGenre.Field].Value, (object)aChar.Stamp[StampGenre.Coast].Value, (object)aChar.Stamp[StampGenre.Wild].Value, (object)aChar.Stamp[StampGenre.Cave].Value, (object)aChar.Stamp[StampGenre.Snow].Value, (object)aChar.Stamp[StampGenre.Colliery].Value, (object)aChar.Stamp[StampGenre.Northan].Value, (object)aChar.Stamp[StampGenre.IronSouth].Value, (object)aChar.Stamp[StampGenre.SouthDungeon].Value, (object)aChar.CP, (object)aChar.ECoin, (object)aChar.DominionLevel, (object)aChar.DominionJobLevel, (object)aChar.JointJobLevel, (object)aChar.DominionCEXP, (object)aChar.DominionJEXP, (object)aChar.JointJEXP, (object)aChar.WRP, (object)aChar.DominionStr, (object)aChar.DominionDex, (object)aChar.DominionInt, (object)aChar.DominionVit, (object)aChar.DominionAgi, (object)aChar.DominionMag, (object)aChar.DominionStatsPoint, (object)num11, (object)aChar.EP, (object)this.ToSQLDateTime(aChar.EPLoginTime), (object)this.ToSQLDateTime(aChar.EPGreetingTime), (object)aChar.CL, (object)aChar.DominionCL, (object)aChar.EPUsed, (object)aChar.DominionEPUsed);
            aChar.Online = online;
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            this.SaveFGarden(aChar);
            if (itemInfo)
                this.SaveItem(aChar);
            if (!fullinfo)
                return;
            this.SaveSkill(aChar);
            this.SaveVar(aChar);
            this.SaveNPCStates(aChar);
        }

        /// <summary>
        /// The SaveWRP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SaveWRP(ActorPC pc)
        {
            string sqlstr = string.Format("UPDATE `char` SET `wrp`='{0}' WHERE char_id='{1}' LIMIT 1", (object)pc.WRP, (object)pc.CharID);
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The DeleteChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        public void DeleteChar(ActorPC aChar)
        {
            int num = (int)(uint)this.SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + (object)aChar.CharID + "' LIMIT 1")[0]["account_id"];
            string sqlstr = "DELETE FROM `char` WHERE char_id='" + (object)aChar.CharID + "';" + "DELETE FROM `inventory` WHERE char_id='" + (object)aChar.CharID + "';" + "DELETE FROM `skill` WHERE char_id='" + (object)aChar.CharID + "';" + "DELETE FROM `cVar` WHERE char_id='" + (object)aChar.CharID + "';" + "DELETE FROM `friend` WHERE `char_id`='" + (object)aChar.CharID + "' OR `friend_char_id`='" + (object)aChar.CharID + "';";
            if (aChar.Party != null && (int)aChar.Party.Leader.CharID == (int)aChar.CharID)
                this.DeleteParty(aChar.Party);
            if (aChar.Ring != null)
            {
                if ((int)aChar.Ring.Leader.CharID == (int)aChar.CharID)
                    this.DeleteRing(aChar.Ring);
            }
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The GetChar.
        /// </summary>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        /// <param name="fullinfo">The fullinfo<see cref="bool"/>.</param>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        public ActorPC GetChar(uint charID, bool fullinfo)
        {
            ActorPC pc = (ActorPC)null;
            try
            {
                int accountId = (int)this.GetAccountID(charID);
                string sqlstr = "SELECT * FROM `char` WHERE `char_id`='" + (object)charID + "' LIMIT 1";
                DataRow dataRow;
                try
                {
                    dataRow = this.SQLExecuteQuery(sqlstr)[0];
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                    return (ActorPC)null;
                }
                pc = new ActorPC();
                pc.CharID = charID;
                pc.Account = (Account)null;
                pc.Name = (string)dataRow["name"];
                pc.Race = (PC_RACE)(byte)dataRow["race"];
                pc.Gender = (PC_GENDER)(byte)dataRow["gender"];
                pc.HairStyle = (byte)dataRow["hairStyle"];
                pc.HairColor = (byte)dataRow["hairColor"];
                pc.Wig = (byte)dataRow["wig"];
                pc.Face = (byte)dataRow["face"];
                pc.Job = (PC_JOB)(byte)dataRow["job"];
                pc.MapID = (uint)dataRow["mapID"];
                pc.Level = (byte)dataRow["lv"];
                pc.JobLevel1 = (byte)dataRow["jlv1"];
                pc.JobLevel2X = (byte)dataRow["jlv2x"];
                pc.JobLevel2T = (byte)dataRow["jlv2t"];
                pc.DominionLevel = (byte)dataRow["dominionlv"];
                pc.DominionJobLevel = (byte)dataRow["dominionjlv"];
                pc.JointJobLevel = (byte)dataRow["jointjlv"];
                pc.DominionReserveSkill = (byte)dataRow["dreserve"] == (byte)1;
                pc.QuestRemaining = (ushort)dataRow["questRemaining"];
                pc.QuestNextResetTime = (DateTime)dataRow["questresettime"];
                pc.Fame = (uint)dataRow["fame"];
                pc.Slot = (byte)dataRow["slot"];
                if (fullinfo)
                {
                    MapInfo mapInfo = (MapInfo)null;
                    if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(pc.MapID))
                        mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[pc.MapID];
                    else if (Singleton<MapInfoFactory>.Instance.MapInfo.ContainsKey(pc.MapID / 1000U * 1000U))
                        mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[pc.MapID / 1000U * 1000U];
                    pc.X = Global.PosX8to16((byte)dataRow["x"], mapInfo.width);
                    pc.Y = Global.PosY8to16((byte)dataRow["y"], mapInfo.height);
                }
                pc.Dir = (ushort)((uint)(byte)dataRow["dir"] * 45U);
                pc.SaveMap = (uint)dataRow["save_map"];
                pc.SaveX = (byte)dataRow["save_x"];
                pc.SaveY = (byte)dataRow["save_y"];
                pc.HP = (uint)dataRow["hp"];
                pc.MP = (uint)dataRow["mp"];
                pc.SP = (uint)dataRow["sp"];
                pc.MaxHP = (uint)dataRow["max_hp"];
                pc.MaxMP = (uint)dataRow["max_mp"];
                pc.MaxSP = (uint)dataRow["max_sp"];
                pc.EP = (uint)dataRow["ep"];
                pc.EPLoginTime = (DateTime)dataRow["eplogindate"];
                pc.EPGreetingTime = (DateTime)dataRow["epgreetingdate"];
                pc.EPUsed = (short)dataRow["epused"];
                pc.DominionEPUsed = (short)dataRow["depused"];
                pc.CL = (short)dataRow["cl"];
                pc.DominionCL = (short)dataRow["dcl"];
                pc.Str = (ushort)dataRow["str"];
                pc.Dex = (ushort)dataRow["dex"];
                pc.Int = (ushort)dataRow["intel"];
                pc.Vit = (ushort)dataRow["vit"];
                pc.Agi = (ushort)dataRow["agi"];
                pc.Mag = (ushort)dataRow["mag"];
                pc.DominionStr = (ushort)dataRow["dstr"];
                pc.DominionDex = (ushort)dataRow["ddex"];
                pc.DominionInt = (ushort)dataRow["dintel"];
                pc.DominionVit = (ushort)dataRow["dvit"];
                pc.DominionAgi = (ushort)dataRow["dagi"];
                pc.DominionMag = (ushort)dataRow["dmag"];
                pc.StatsPoint = (ushort)dataRow["statspoint"];
                pc.DominionStatsPoint = (ushort)dataRow["dstatpoint"];
                pc.SkillPoint = (ushort)dataRow["skillpoint"];
                pc.SkillPoint2X = (ushort)dataRow["skillpoint2x"];
                pc.SkillPoint2T = (ushort)dataRow["skillpoint2t"];
                lock (this)
                {
                    int num = SagaLib.Logger.SQLLogLevel.Value;
                    SagaLib.Logger.SQLLogLevel.Value = 0;
                    pc.Gold = (int)dataRow["gold"];
                    SagaLib.Logger.SQLLogLevel.Value = num;
                }
                pc.CP = (uint)dataRow["cp"];
                pc.ECoin = (uint)dataRow["ecoin"];
                pc.CEXP = (uint)dataRow["cexp"];
                pc.JEXP = (uint)dataRow["jexp"];
                pc.DominionCEXP = (uint)dataRow["dcexp"];
                pc.DominionJEXP = (uint)dataRow["djexp"];
                pc.JointJEXP = (uint)dataRow["jjexp"];
                pc.WRP = (int)dataRow["wrp"];
                pc.PossessionTarget = (uint)dataRow["possession_target"];
                SagaDB.Party.Party party = new SagaDB.Party.Party();
                party.ID = (uint)dataRow["party"];
                SagaDB.Ring.Ring ring = new SagaDB.Ring.Ring();
                ring.ID = (uint)dataRow["ring"];
                if (party.ID != 0U)
                    pc.Party = party;
                if (ring.ID != 0U)
                    pc.Ring = ring;
                uint num1 = (uint)dataRow["golem"];
                if (num1 != 0U)
                {
                    pc.Golem = new ActorGolem();
                    pc.Golem.ActorID = num1;
                }
                pc.Stamp[StampGenre.Special].Value = (int)(short)dataRow["stamp1"];
                pc.Stamp[StampGenre.Pururu].Value = (int)(short)dataRow["stamp2"];
                pc.Stamp[StampGenre.Field].Value = (int)(short)dataRow["stamp3"];
                pc.Stamp[StampGenre.Coast].Value = (int)(short)dataRow["stamp4"];
                pc.Stamp[StampGenre.Wild].Value = (int)(short)dataRow["stamp5"];
                pc.Stamp[StampGenre.Cave].Value = (int)(short)dataRow["stamp6"];
                pc.Stamp[StampGenre.Snow].Value = (int)(short)dataRow["stamp7"];
                pc.Stamp[StampGenre.Colliery].Value = (int)(short)dataRow["stamp8"];
                pc.Stamp[StampGenre.Northan].Value = (int)(short)dataRow["stamp9"];
                pc.Stamp[StampGenre.IronSouth].Value = (int)(short)dataRow["stamp10"];
                pc.Stamp[StampGenre.SouthDungeon].Value = (int)(short)dataRow["stamp11"];
                if (fullinfo)
                {
                    uint id = (uint)dataRow["questid"];
                    if (id != 0U)
                        pc.Quest = new Quest(id)
                        {
                            EndTime = (DateTime)dataRow["questendtime"],
                            Status = (QuestStatus)(byte)dataRow["queststatus"],
                            CurrentCount1 = (int)dataRow["questcurrentcount1"],
                            CurrentCount2 = (int)dataRow["questcurrentcount2"],
                            CurrentCount3 = (int)dataRow["questcurrentcount3"]
                        };
                    this.GetSkill(pc);
                    this.GetVar(pc);
                    this.GetNPCStates(pc);
                    this.GetFGarden(pc);
                }
                this.GetItem(pc);
                this.GetVShop(pc);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            return pc;
        }

        /// <summary>
        /// The GetChar.
        /// </summary>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        public ActorPC GetChar(uint charID)
        {
            return this.GetChar(charID, true);
        }

        /// <summary>
        /// The GetVShop.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void GetVShop(ActorPC pc)
        {
            DataRow dataRow = this.SQLExecuteQuery("SELECT `vshop_points`,`used_vshop_points` FROM `login` WHERE account_id='" + (object)this.GetAccountID(pc) + "' LIMIT 1")[0];
            ActorEventHandler e = pc.e;
            pc.e = (ActorEventHandler)null;
            pc.VShopPoints = (uint)dataRow["vshop_points"];
            pc.e = e;
            pc.UsedVShopPoints = (uint)dataRow["used_vshop_points"];
        }

        /// <summary>
        /// The SaveSkill.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void SaveSkill(ActorPC pc)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter((Stream)memoryStream);
            int num1 = pc.Skills.Count + pc.Skills2.Count + pc.SkillsReserve.Count;
            int num2 = 0;
            foreach (SagaDB.Skill.Skill skill in pc.Skills.Values)
            {
                if (skill.NoSave)
                    ++num2;
            }
            foreach (SagaDB.Skill.Skill skill in pc.Skills2.Values)
            {
                if (skill.NoSave)
                    ++num2;
            }
            foreach (SagaDB.Skill.Skill skill in pc.SkillsReserve.Values)
            {
                if (skill.NoSave)
                    ++num2;
            }
            int num3 = num1 - num2;
            binaryWriter.Write(num3);
            foreach (uint key in pc.Skills.Keys)
            {
                if (!pc.Skills[key].NoSave)
                {
                    binaryWriter.Write(key);
                    binaryWriter.Write(pc.Skills[key].Level);
                }
            }
            foreach (uint key in pc.Skills2.Keys)
            {
                if (!pc.Skills2[key].NoSave)
                {
                    binaryWriter.Write(key);
                    binaryWriter.Write(pc.Skills2[key].Level);
                }
            }
            foreach (uint key in pc.SkillsReserve.Keys)
            {
                if (!pc.SkillsReserve[key].NoSave)
                {
                    binaryWriter.Write(key);
                    binaryWriter.Write(pc.SkillsReserve[key].Level);
                }
            }
            memoryStream.Flush();
            MySqlCommand cmd = new MySqlCommand(string.Format("REPLACE INTO `skill`(`char_id`,`skills`) VALUES ('{0}',?data);", (object)pc.CharID));
            cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = (object)memoryStream.ToArray();
            memoryStream.Close();
            try
            {
                this.SQLExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The SaveVShop.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SaveVShop(ActorPC pc)
        {
            ActorEventHandler e = pc.e;
            pc.e = (ActorEventHandler)null;
            string sqlstr = string.Format("UPDATE `login` SET `vshop_points`='{0}',`used_vshop_points`='{1}' WHERE account_id='{2}' LIMIT 1", (object)pc.VShopPoints, (object)pc.UsedVShopPoints, (object)pc.Account.AccountID);
            pc.e = e;
            this.SQLExecuteNonQuery(sqlstr);
        }

        /// <summary>
        /// The SaveServerVar.
        /// </summary>
        /// <param name="fakepc">The fakepc<see cref="ActorPC"/>.</param>
        public void SaveServerVar(ActorPC fakepc)
        {
            string sqlstr = "TRUNCATE TABLE `sVar`;";
            foreach (string key in (IEnumerable<string>)fakepc.AStr.Keys)
                sqlstr += string.Format("INSERT INTO `sVar`(`name`,`type`,`content`) VALUES ('{0}',0,'{1}');", (object)key, (object)fakepc.AStr[key]);
            foreach (string key in (IEnumerable<string>)fakepc.AInt.Keys)
                sqlstr += string.Format("INSERT INTO `sVar`(`name`,`type`,`content`) VALUES ('{0}',1,'{1}');", (object)key, (object)fakepc.AInt[key]);
            foreach (string key in (IEnumerable<string>)fakepc.AMask.Keys)
                sqlstr += string.Format("INSERT INTO `sVar`(`name`,`type`,`content`) VALUES ('{0}',2,'{1}');", (object)key, (object)fakepc.AMask[key].Value);
            this.SQLExecuteNonQuery(sqlstr);
        }

        /// <summary>
        /// The LoadServerVar.
        /// </summary>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        public ActorPC LoadServerVar()
        {
            ActorPC actorPc = new ActorPC();
            foreach (DataRow dataRow in (InternalDataCollectionBase)this.SQLExecuteQuery("SELECT * FROM `sVar`;"))
            {
                switch ((byte)dataRow["type"])
                {
                    case 0:
                        actorPc.AStr[(string)dataRow["name"]] = (string)dataRow["content"];
                        continue;
                    case 1:
                        actorPc.AInt[(string)dataRow["name"]] = int.Parse((string)dataRow["content"]);
                        continue;
                    case 2:
                        actorPc.AMask[(string)dataRow["name"]] = new BitMask(int.Parse((string)dataRow["content"]));
                        continue;
                    default:
                        continue;
                }
            }
            return actorPc;
        }

        /// <summary>
        /// The SaveVar.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void SaveVar(ActorPC pc)
        {
            uint num = (uint)this.SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + (object)pc.CharID + "' LIMIT 1")[0]["account_id"];
            Encoding utF8 = Encoding.UTF8;
            MemoryStream memoryStream1 = new MemoryStream();
            BinaryWriter binaryWriter1 = new BinaryWriter((Stream)memoryStream1);
            binaryWriter1.Write(pc.CInt.Count);
            foreach (string key in (IEnumerable<string>)pc.CInt.Keys)
            {
                byte[] bytes = utF8.GetBytes(key);
                binaryWriter1.Write(bytes.Length);
                binaryWriter1.Write(bytes);
                binaryWriter1.Write(pc.CInt[key]);
            }
            binaryWriter1.Write(pc.CMask.Count);
            foreach (string key in (IEnumerable<string>)pc.CMask.Keys)
            {
                byte[] bytes = utF8.GetBytes(key);
                binaryWriter1.Write(bytes.Length);
                binaryWriter1.Write(bytes);
                binaryWriter1.Write(pc.CMask[key].Value);
            }
            binaryWriter1.Write(pc.CStr.Count);
            foreach (string key in (IEnumerable<string>)pc.CStr.Keys)
            {
                byte[] bytes1 = utF8.GetBytes(key);
                binaryWriter1.Write(bytes1.Length);
                binaryWriter1.Write(bytes1);
                byte[] bytes2 = utF8.GetBytes(pc.CStr[key]);
                binaryWriter1.Write(bytes2.Length);
                binaryWriter1.Write(bytes2);
            }
            memoryStream1.Flush();
            MySqlCommand cmd1 = new MySqlCommand(string.Format("REPLACE `cVar`(`char_id`,`values`) VALUES ('{0}',?data);", (object)pc.CharID));
            cmd1.Parameters.Add("?data", MySqlDbType.Blob).Value = (object)memoryStream1.ToArray();
            memoryStream1.Close();
            try
            {
                this.SQLExecuteNonQuery(cmd1);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            MemoryStream memoryStream2 = new MemoryStream();
            BinaryWriter binaryWriter2 = new BinaryWriter((Stream)memoryStream2);
            binaryWriter2.Write(pc.AInt.Count);
            foreach (string key in (IEnumerable<string>)pc.AInt.Keys)
            {
                byte[] bytes = utF8.GetBytes(key);
                binaryWriter2.Write(bytes.Length);
                binaryWriter2.Write(bytes);
                binaryWriter2.Write(pc.AInt[key]);
            }
            binaryWriter2.Write(pc.AMask.Count);
            foreach (string key in (IEnumerable<string>)pc.AMask.Keys)
            {
                byte[] bytes = utF8.GetBytes(key);
                binaryWriter2.Write(bytes.Length);
                binaryWriter2.Write(bytes);
                binaryWriter2.Write(pc.AMask[key].Value);
            }
            binaryWriter2.Write(pc.AStr.Count);
            foreach (string key in (IEnumerable<string>)pc.AStr.Keys)
            {
                byte[] bytes1 = utF8.GetBytes(key);
                binaryWriter2.Write(bytes1.Length);
                binaryWriter2.Write(bytes1);
                byte[] bytes2 = utF8.GetBytes(pc.AStr[key]);
                binaryWriter2.Write(bytes2.Length);
                binaryWriter2.Write(bytes2);
            }
            memoryStream2.Flush();
            MySqlCommand cmd2 = new MySqlCommand(string.Format("REPLACE INTO `aVar`(`account_id`,`values`) VALUES ('{0}',?data); ", (object)num));
            cmd2.Parameters.Add("?data", MySqlDbType.Blob).Value = (object)memoryStream2.ToArray();
            memoryStream2.Close();
            try
            {
                this.SQLExecuteNonQuery(cmd2);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The GetVar.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void GetVar(ActorPC pc)
        {
            string sqlstr = "SELECT * FROM `cVar` WHERE `char_id`='" + (object)pc.CharID + "' LIMIT 1;";
            uint num1 = (uint)this.SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + (object)pc.CharID + "' LIMIT 1")[0]["account_id"];
            Encoding utF8 = Encoding.UTF8;
            DataRowCollection dataRowCollection1 = this.SQLExecuteQuery(sqlstr);
            if (dataRowCollection1.Count > 0)
            {
                BinaryReader binaryReader = new BinaryReader((Stream)new MemoryStream((byte[])dataRowCollection1[0]["values"]));
                int num2 = binaryReader.ReadInt32();
                for (int index1 = 0; index1 < num2; ++index1)
                {
                    string index2 = utF8.GetString(binaryReader.ReadBytes(binaryReader.ReadInt32()));
                    pc.CInt[index2] = binaryReader.ReadInt32();
                }
                int num3 = binaryReader.ReadInt32();
                for (int index1 = 0; index1 < num3; ++index1)
                {
                    string index2 = utF8.GetString(binaryReader.ReadBytes(binaryReader.ReadInt32()));
                    pc.CMask[index2] = new BitMask(binaryReader.ReadInt32());
                }
                int num4 = binaryReader.ReadInt32();
                for (int index1 = 0; index1 < num4; ++index1)
                {
                    string index2 = utF8.GetString(binaryReader.ReadBytes(binaryReader.ReadInt32()));
                    pc.CStr[index2] = utF8.GetString(binaryReader.ReadBytes(binaryReader.ReadInt32()));
                }
            }
            DataRowCollection dataRowCollection2 = this.SQLExecuteQuery("SELECT * FROM `aVar` WHERE `account_id`='" + (object)num1 + "' LIMIT 1;");
            if (dataRowCollection2.Count <= 0)
                return;
            BinaryReader binaryReader1 = new BinaryReader((Stream)new MemoryStream((byte[])dataRowCollection2[0]["values"]));
            int num5 = binaryReader1.ReadInt32();
            for (int index1 = 0; index1 < num5; ++index1)
            {
                string index2 = utF8.GetString(binaryReader1.ReadBytes(binaryReader1.ReadInt32()));
                pc.AInt[index2] = binaryReader1.ReadInt32();
            }
            int num6 = binaryReader1.ReadInt32();
            for (int index1 = 0; index1 < num6; ++index1)
            {
                string index2 = utF8.GetString(binaryReader1.ReadBytes(binaryReader1.ReadInt32()));
                pc.AMask[index2] = new BitMask(binaryReader1.ReadInt32());
            }
            int num7 = binaryReader1.ReadInt32();
            for (int index1 = 0; index1 < num7; ++index1)
            {
                string index2 = utF8.GetString(binaryReader1.ReadBytes(binaryReader1.ReadInt32()));
                pc.AStr[index2] = utF8.GetString(binaryReader1.ReadBytes(binaryReader1.ReadInt32()));
            }
        }

        /// <summary>
        /// The SaveItem.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SaveItem(ActorPC pc)
        {
            uint accountId = this.GetAccountID(pc);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            if (!pc.Inventory.IsEmpty || pc.Inventory.NeedSave)
            {
                MySqlCommand cmd = new MySqlCommand(string.Format("UPDATE `inventory` SET `data`=?data WHERE `char_id`='{0}' LIMIT 1;\r\n", (object)pc.CharID));
                cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = (object)pc.Inventory.ToBytes();
                try
                {
                    this.SQLExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
            }
            if (pc.Inventory.WareHouse == null || pc.Inventory.IsWarehouseEmpty && !pc.Inventory.NeedSaveWare)
                return;
            MySqlCommand cmd1 = new MySqlCommand(string.Format("UPDATE `warehouse` SET `data`=?data WHERE `account_id`='{0}' LIMIT 1;\r\n", (object)accountId));
            cmd1.Parameters.Add("?data", MySqlDbType.Blob).Value = (object)pc.Inventory.WareToBytes();
            try
            {
                this.SQLExecuteNonQuery(cmd1);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The GetSkill.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void GetSkill(ActorPC pc)
        {
            string sqlstr = "SELECT * FROM `skill` WHERE `char_id`='" + (object)pc.CharID + "' LIMIT 1;";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return;
            }
            Dictionary<uint, byte> dictionary1 = Singleton<SkillFactory>.Instance.SkillList(pc.JobBasic);
            Dictionary<uint, byte> dictionary2 = Singleton<SkillFactory>.Instance.SkillList(pc.Job2X);
            Dictionary<uint, byte> dictionary3 = Singleton<SkillFactory>.Instance.SkillList(pc.Job2T);
            if (dataRowCollection.Count <= 0)
                return;
            BinaryReader binaryReader = new BinaryReader((Stream)new MemoryStream((byte[])dataRowCollection[0]["skills"]));
            int num = binaryReader.ReadInt32();
            for (int index = 0; index < num; ++index)
            {
                SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(binaryReader.ReadUInt32(), binaryReader.ReadByte());
                if (dictionary1.ContainsKey(skill.ID))
                {
                    if (!pc.Skills.ContainsKey(skill.ID))
                        pc.Skills.Add(skill.ID, skill);
                }
                else if (dictionary2.ContainsKey(skill.ID))
                {
                    if (pc.Job == pc.Job2X)
                    {
                        if (!pc.Skills2.ContainsKey(skill.ID))
                            pc.Skills2.Add(skill.ID, skill);
                    }
                    else if (!pc.SkillsReserve.ContainsKey(skill.ID))
                        pc.SkillsReserve.Add(skill.ID, skill);
                }
                else if (dictionary3.ContainsKey(skill.ID))
                {
                    if (pc.Job == pc.Job2T)
                    {
                        if (!pc.Skills2.ContainsKey(skill.ID))
                            pc.Skills2.Add(skill.ID, skill);
                    }
                    else if (!pc.SkillsReserve.ContainsKey(skill.ID))
                        pc.SkillsReserve.Add(skill.ID, skill);
                }
                else if (!pc.Skills.ContainsKey(skill.ID))
                    pc.Skills.Add(skill.ID, skill);
            }
        }

        /// <summary>
        /// The SaveNPCStates.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void SaveNPCStates(ActorPC pc)
        {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter((Stream)memoryStream);
            binaryWriter.Write(pc.NPCStates.Count);
            foreach (uint key1 in pc.NPCStates.Keys)
            {
                binaryWriter.Write(key1);
                binaryWriter.Write(pc.NPCStates[key1].Count);
                foreach (uint key2 in pc.NPCStates[key1].Keys)
                {
                    binaryWriter.Write(key2);
                    binaryWriter.Write(pc.NPCStates[key1][key2]);
                }
            }
            memoryStream.Flush();
            MySqlCommand cmd = new MySqlCommand(string.Format("REPLACE INTO `npcstates`(`char_id`,`data`) VALUES ('{0}',?data);\r\n", (object)pc.CharID));
            cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = (object)memoryStream.ToArray();
            try
            {
                this.SQLExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            memoryStream.Close();
        }

        /// <summary>
        /// The GetNPCStates.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void GetNPCStates(ActorPC pc)
        {
            int accountId = (int)this.GetAccountID(pc);
            string sqlstr = "SELECT * FROM `npcstates` WHERE `char_id`='" + (object)pc.CharID + "' LIMIT 1;";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return;
            }
            if (dataRowCollection.Count <= 0)
                return;
            MemoryStream memoryStream = new MemoryStream((byte[])dataRowCollection[0]["data"]);
            BinaryReader binaryReader = new BinaryReader((Stream)memoryStream);
            int num1 = binaryReader.ReadInt32();
            for (int index1 = 0; index1 < num1; ++index1)
            {
                uint key1 = binaryReader.ReadUInt32();
                pc.NPCStates.Add(key1, new Dictionary<uint, bool>());
                int num2 = binaryReader.ReadInt32();
                for (int index2 = 0; index2 < num2; ++index2)
                {
                    uint key2 = binaryReader.ReadUInt32();
                    bool flag = binaryReader.ReadBoolean();
                    pc.NPCStates[key1].Add(key2, flag);
                }
            }
            memoryStream.Close();
        }

        /// <summary>
        /// The GetItem.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void GetItem(ActorPC pc)
        {
            uint accountId = this.GetAccountID(pc);
            string sqlstr1 = "SELECT * FROM `inventory` WHERE `char_id`='" + (object)pc.CharID + "' LIMIT 1;";
            DataRowCollection dataRowCollection1;
            try
            {
                dataRowCollection1 = this.SQLExecuteQuery(sqlstr1);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return;
            }
            if (dataRowCollection1.Count > 0)
            {
                try
                {
                    byte[] buffer = (byte[])dataRowCollection1[0]["data"];
                    MemoryStream memoryStream1 = new MemoryStream(buffer);
                    if (buffer[0] == (byte)66 && buffer[1] == (byte)90)
                    {
                        MemoryStream memoryStream2 = new MemoryStream();
                        ICSharpCode.SharpZipLib.BZip2.BZip2.Decompress((Stream)memoryStream1, (Stream)memoryStream2);
                        Inventory inventory = (Inventory)new BinaryFormatter().Deserialize((Stream)new MemoryStream(memoryStream2.ToArray()));
                        if (inventory != null)
                        {
                            pc.Inventory = inventory;
                            pc.Inventory.Owner = pc;
                        }
                    }
                    else
                    {
                        Inventory inventory = new Inventory(pc);
                        inventory.FromStream((Stream)memoryStream1);
                        pc.Inventory = inventory;
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
            }
            string sqlstr2 = "SELECT * FROM `warehouse` WHERE `account_id`='" + (object)accountId + "';";
            DataRowCollection dataRowCollection2;
            try
            {
                dataRowCollection2 = this.SQLExecuteQuery(sqlstr2);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return;
            }
            if (dataRowCollection2.Count > 0)
            {
                try
                {
                    byte[] buffer = (byte[])dataRowCollection2[0]["data"];
                    MemoryStream memoryStream1 = new MemoryStream(buffer);
                    if (buffer[0] == (byte)66 && buffer[1] == (byte)90)
                    {
                        pc.Inventory.WareHouse = new Dictionary<WarehousePlace, List<SagaDB.Item.Item>>();
                        MemoryStream memoryStream2 = new MemoryStream();
                        ICSharpCode.SharpZipLib.BZip2.BZip2.Decompress((Stream)memoryStream1, (Stream)memoryStream2);
                        Dictionary<WarehousePlace, List<SagaDB.Item.Item>> dictionary = (Dictionary<WarehousePlace, List<SagaDB.Item.Item>>)new BinaryFormatter().Deserialize((Stream)new MemoryStream(memoryStream2.ToArray()));
                        if (dictionary != null)
                        {
                            pc.Inventory.wareIndex = 200000001U;
                            foreach (WarehousePlace key in dictionary.Keys)
                            {
                                pc.Inventory.WareHouse.Add(key, new List<SagaDB.Item.Item>());
                                foreach (SagaDB.Item.Item obj in dictionary[key])
                                {
                                    int num = (int)pc.Inventory.AddWareItem(key, obj);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (pc.Inventory.WareHouse == null)
                            pc.Inventory.WareHouse = new Inventory(pc).WareHouse;
                        pc.Inventory.WareFromSteam((Stream)memoryStream1);
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
            }
            if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.Acropolis))
                pc.Inventory.WareHouse.Add(WarehousePlace.Acropolis, new List<SagaDB.Item.Item>());
            if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.FederalOfIronSouth))
                pc.Inventory.WareHouse.Add(WarehousePlace.FederalOfIronSouth, new List<SagaDB.Item.Item>());
            if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.FarEast))
                pc.Inventory.WareHouse.Add(WarehousePlace.FarEast, new List<SagaDB.Item.Item>());
            if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.IronSouth))
                pc.Inventory.WareHouse.Add(WarehousePlace.IronSouth, new List<SagaDB.Item.Item>());
            if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.KingdomOfNorthan))
                pc.Inventory.WareHouse.Add(WarehousePlace.KingdomOfNorthan, new List<SagaDB.Item.Item>());
            if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.MiningCamp))
                pc.Inventory.WareHouse.Add(WarehousePlace.MiningCamp, new List<SagaDB.Item.Item>());
            if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.Morg))
                pc.Inventory.WareHouse.Add(WarehousePlace.Morg, new List<SagaDB.Item.Item>());
            if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.Northan))
                pc.Inventory.WareHouse.Add(WarehousePlace.Northan, new List<SagaDB.Item.Item>());
            if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.RepublicOfFarEast))
                pc.Inventory.WareHouse.Add(WarehousePlace.RepublicOfFarEast, new List<SagaDB.Item.Item>());
            if (pc.Inventory.WareHouse.ContainsKey(WarehousePlace.Tonka))
                return;
            pc.Inventory.WareHouse.Add(WarehousePlace.Tonka, new List<SagaDB.Item.Item>());
        }

        /// <summary>
        /// The CharExists.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CharExists(string name)
        {
            DataRowCollection dataRowCollection = (DataRowCollection)null;
            string sqlstr = "SELECT count(*) FROM `char` WHERE name='" + this.CheckSQLString(name) + "'";
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
            return Convert.ToInt32(dataRowCollection[0][0]) > 0;
        }

        /// <summary>
        /// The GetAccountID.
        /// </summary>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetAccountID(uint charID)
        {
            DataRowCollection dataRowCollection = this.SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + (object)charID + "' LIMIT 1;");
            if (dataRowCollection.Count == 0)
                return 0;
            return (uint)dataRowCollection[0]["account_id"];
        }

        /// <summary>
        /// The GetAccountID.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetAccountID(ActorPC pc)
        {
            if (pc.Account != null)
                return (uint)pc.Account.AccountID;
            return this.GetAccountID(pc.CharID);
        }

        /// <summary>
        /// The GetCharIDs.
        /// </summary>
        /// <param name="account_id">The account_id<see cref="int"/>.</param>
        /// <returns>The <see cref="uint[]"/>.</returns>
        public uint[] GetCharIDs(int account_id)
        {
            string sqlstr = "SELECT `char_id` FROM `char` WHERE account_id='" + (object)account_id + "'";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
                return new uint[0];
            }
            if (dataRowCollection.Count == 0)
                return new uint[0];
            uint[] numArray = new uint[dataRowCollection.Count];
            for (int index = 0; index < numArray.Length; ++index)
                numArray[index] = (uint)dataRowCollection[index]["char_id"];
            return numArray;
        }

        /// <summary>
        /// The GetCharName.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetCharName(uint id)
        {
            DataRowCollection dataRowCollection = this.SQLExecuteQuery("SELECT `name` FROM `char` WHERE `char_id`='" + id.ToString() + "' LIMIT 1;");
            if (dataRowCollection.Count == 0)
                return (string)null;
            return (string)dataRowCollection[0]["name"];
        }

        /// <summary>
        /// The GetFriendList.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="List{ActorPC}"/>.</returns>
        public List<ActorPC> GetFriendList(ActorPC pc)
        {
            DataRowCollection dataRowCollection1 = this.SQLExecuteQuery("SELECT `friend_char_id` FROM `friend` WHERE `char_id`='" + (object)pc.CharID + "';");
            List<ActorPC> actorPcList = new List<ActorPC>();
            for (int index = 0; index < dataRowCollection1.Count; ++index)
            {
                uint num = (uint)dataRowCollection1[index]["friend_char_id"];
                ActorPC actorPc = new ActorPC();
                actorPc.CharID = num;
                DataRowCollection dataRowCollection2 = this.SQLExecuteQuery("SELECT `name`,`job`,`lv`,`jlv1`,`jlv2x`,`jlv2t` FROM `char` WHERE `char_id`='" + (object)num + "' LIMIT 1;");
                if (dataRowCollection2.Count != 0)
                {
                    DataRow dataRow = dataRowCollection2[0];
                    actorPc.Name = (string)dataRow["name"];
                    actorPc.Job = (PC_JOB)(byte)dataRow["job"];
                    actorPc.Level = (byte)dataRow["lv"];
                    actorPc.JobLevel1 = (byte)dataRow["jlv1"];
                    actorPc.JobLevel2X = (byte)dataRow["jlv2x"];
                    actorPc.JobLevel2T = (byte)dataRow["jlv2t"];
                    actorPcList.Add(actorPc);
                }
            }
            return actorPcList;
        }

        /// <summary>
        /// The GetFriendList2.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="List{ActorPC}"/>.</returns>
        public List<ActorPC> GetFriendList2(ActorPC pc)
        {
            DataRowCollection dataRowCollection1 = this.SQLExecuteQuery("SELECT `char_id` FROM `friend` WHERE `friend_char_id`='" + (object)pc.CharID + "';");
            List<ActorPC> actorPcList = new List<ActorPC>();
            for (int index = 0; index < dataRowCollection1.Count; ++index)
            {
                uint num = (uint)dataRowCollection1[index]["char_id"];
                ActorPC actorPc = new ActorPC();
                actorPc.CharID = num;
                DataRowCollection dataRowCollection2 = this.SQLExecuteQuery("SELECT `name`,`job`,`lv`,`jlv1`,`jlv2x`,`jlv2t` FROM `char` WHERE `char_id`='" + (object)num + "' LIMIT 1;");
                if (dataRowCollection2.Count != 0)
                {
                    DataRow dataRow = dataRowCollection2[0];
                    actorPc.Name = (string)dataRow["name"];
                    actorPc.Job = (PC_JOB)(byte)dataRow["job"];
                    actorPc.Level = (byte)dataRow["lv"];
                    actorPc.JobLevel1 = (byte)dataRow["jlv1"];
                    actorPc.JobLevel2X = (byte)dataRow["jlv2x"];
                    actorPc.JobLevel2T = (byte)dataRow["jlv2t"];
                    actorPcList.Add(actorPc);
                }
            }
            return actorPcList;
        }

        /// <summary>
        /// The AddFriend.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        public void AddFriend(ActorPC pc, uint charID)
        {
            this.SQLExecuteNonQuery(string.Format("INSERT INTO `friend`(`char_id`,`friend_char_id`) VALUES ('{0}','{1}');", (object)pc.CharID, (object)charID));
        }

        /// <summary>
        /// The IsFriend.
        /// </summary>
        /// <param name="char1">The char1<see cref="uint"/>.</param>
        /// <param name="char2">The char2<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsFriend(uint char1, uint char2)
        {
            return this.SQLExecuteQuery("SELECT `char_id` FROM `friend` WHERE `friend_char_id`='" + (object)char2 + "' AND `char_id`='" + (object)char1 + "';").Count > 0;
        }

        /// <summary>
        /// The DeleteFriend.
        /// </summary>
        /// <param name="char1">The char1<see cref="uint"/>.</param>
        /// <param name="char2">The char2<see cref="uint"/>.</param>
        public void DeleteFriend(uint char1, uint char2)
        {
            this.SQLExecuteNonQuery("DELETE FROM `friend` WHERE `friend_char_id`='" + (object)char2 + "' AND `char_id`='" + (object)char1 + "';");
        }

        /// <summary>
        /// The GetParty.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Party.Party"/>.</returns>
        public SagaDB.Party.Party GetParty(uint id)
        {
            DataRowCollection dataRowCollection1 = this.SQLExecuteQuery("SELECT * FROM `party` WHERE `party_id`='" + (object)id + "' LIMIT 1;");
            SagaDB.Party.Party party = new SagaDB.Party.Party();
            if (dataRowCollection1.Count == 0)
                return (SagaDB.Party.Party)null;
            party.ID = id;
            uint num1 = (uint)dataRowCollection1[0]["leader"];
            party.Name = (string)dataRowCollection1[0]["name"];
            for (byte index = 1; index <= (byte)8; ++index)
            {
                uint num2 = (uint)dataRowCollection1[0]["member" + index.ToString()];
                if (num2 != 0U)
                {
                    ActorPC actorPc = new ActorPC();
                    actorPc.CharID = num2;
                    DataRowCollection dataRowCollection2 = this.SQLExecuteQuery("SELECT `name`,`job` FROM `char` WHERE `char_id`='" + (object)num2 + "' LIMIT 1;");
                    if (dataRowCollection2.Count > 0)
                    {
                        DataRow dataRow = dataRowCollection2[0];
                        actorPc.Name = (string)dataRow["name"];
                        actorPc.Job = (PC_JOB)(byte)dataRow["job"];
                        party.Members.Add((byte)((uint)index - 1U), actorPc);
                        if ((int)num1 == (int)num2)
                            party.Leader = actorPc;
                    }
                }
            }
            if (party.Leader == null)
                return (SagaDB.Party.Party)null;
            return party;
        }

        /// <summary>
        /// The NewParty.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        public void NewParty(SagaDB.Party.Party party)
        {
            uint index = 0;
            string name = party.Name;
            this.CheckSQLString(ref name);
            this.SQLExecuteScalar(string.Format("INSERT INTO `party`(`leader`,`name`,`member1`,`member2`,`member3`,`member4`,`member5`,`member6`,`member7`,`member8`) VALUES ('0','{0}','0','0','0','0','0','0','0','0');", (object)name), out index);
            party.ID = index;
        }

        /// <summary>
        /// The SaveParty.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        public void SaveParty(SagaDB.Party.Party party)
        {
            uint charId = party.Leader.CharID;
            string name = party.Name;
            this.CheckSQLString(ref name);
            uint num1 = !party.Members.ContainsKey((byte)0) ? 0U : party[(byte)0].CharID;
            uint num2 = !party.Members.ContainsKey((byte)1) ? 0U : party[(byte)1].CharID;
            uint num3 = !party.Members.ContainsKey((byte)2) ? 0U : party[(byte)2].CharID;
            uint num4 = !party.Members.ContainsKey((byte)3) ? 0U : party[(byte)3].CharID;
            uint num5 = !party.Members.ContainsKey((byte)4) ? 0U : party[(byte)4].CharID;
            uint num6 = !party.Members.ContainsKey((byte)5) ? 0U : party[(byte)5].CharID;
            uint num7 = !party.Members.ContainsKey((byte)6) ? 0U : party[(byte)6].CharID;
            uint num8 = !party.Members.ContainsKey((byte)7) ? 0U : party[(byte)7].CharID;
            this.SQLExecuteNonQuery(string.Format("UPDATE `party` SET `leader`='{0}',`member1`='{1}',`member2`='{2}',`member3`='{3}',`member4`='{4}',`member5`='{5}',`member6`='{6}',`member7`='{7}',`member8`='{8}',`name`='{10}' WHERE `party_id`='{9}' LIMIT 1;", (object)charId, (object)num1, (object)num2, (object)num3, (object)num4, (object)num5, (object)num6, (object)num7, (object)num8, (object)party.ID, (object)name));
        }

        /// <summary>
        /// The DeleteParty.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        public void DeleteParty(SagaDB.Party.Party party)
        {
            this.SQLExecuteNonQuery(string.Format("DELETE FROM `party` WHERE `party_id`='{0}';", (object)party.ID));
        }

        /// <summary>
        /// The GetRing.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Ring.Ring"/>.</returns>
        public SagaDB.Ring.Ring GetRing(uint id)
        {
            DataRowCollection dataRowCollection1 = this.SQLExecuteQuery("SELECT `leader`,`name`,`fame` FROM `ring` WHERE `ring_id`='" + (object)id + "' LIMIT 1;");
            SagaDB.Ring.Ring ring = new SagaDB.Ring.Ring();
            if (dataRowCollection1.Count == 0)
                return (SagaDB.Ring.Ring)null;
            ring.ID = id;
            uint num = (uint)dataRowCollection1[0]["leader"];
            ring.Name = (string)dataRowCollection1[0]["name"];
            ring.Fame = (uint)dataRowCollection1[0]["fame"];
            DataRowCollection dataRowCollection2 = this.SQLExecuteQuery("SELECT * FROM `ringmember` WHERE `ring_id`='" + (object)id + "';");
            for (int index1 = 0; index1 < dataRowCollection2.Count; ++index1)
            {
                ActorPC pc = new ActorPC();
                pc.CharID = (uint)dataRowCollection2[index1]["char_id"];
                DataRowCollection dataRowCollection3 = this.SQLExecuteQuery("SELECT `name`,`job` FROM `char` WHERE `char_id`='" + (object)pc.CharID + "' LIMIT 1;");
                if (dataRowCollection3.Count > 0)
                {
                    DataRow dataRow = dataRowCollection3[0];
                    pc.Name = (string)dataRow["name"];
                    pc.Job = (PC_JOB)(byte)dataRow["job"];
                    int index2 = ring.NewMember(pc);
                    if (index2 >= 0)
                        ring.Rights[index2].Value = (int)(uint)dataRowCollection2[index1]["right"];
                    if ((int)num == (int)pc.CharID)
                        ring.Leader = pc;
                }
            }
            if (ring.Leader == null)
                return (SagaDB.Ring.Ring)null;
            return ring;
        }

        /// <summary>
        /// The NewRing.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        public void NewRing(SagaDB.Ring.Ring ring)
        {
            uint index = 0;
            string name = ring.Name;
            this.CheckSQLString(ref name);
            if (this.SQLExecuteQuery(string.Format("SELECT `name` FROM `ring` WHERE `name`='{0}' LIMIT 1", (object)ring.Name)).Count > 0)
            {
                ring.ID = uint.MaxValue;
            }
            else
            {
                this.SQLExecuteScalar(string.Format("INSERT INTO `ring`(`leader`,`name`) VALUES ('0','{0}');", (object)name), out index);
                ring.ID = index;
            }
        }

        /// <summary>
        /// The SaveRing.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="saveMembers">The saveMembers<see cref="bool"/>.</param>
        public void SaveRing(SagaDB.Ring.Ring ring, bool saveMembers)
        {
            string sqlstr = string.Format("UPDATE `ring` SET `leader`='{0}',`name`='{1}',`fame`='{2}' WHERE `ring_id`='{3}' LIMIT 1;\r\n", (object)ring.Leader.CharID, (object)ring.Name, (object)ring.Fame, (object)ring.ID);
            if (saveMembers)
            {
                sqlstr += string.Format("DELETE FROM `ringmember` WHERE `ring_id`='{0}';\r\n", (object)ring.ID);
                foreach (int key in ring.Members.Keys)
                    sqlstr += string.Format("INSERT INTO `ringmember`(`ring_id`,`char_id`,`right`) VALUES ('{0}','{1}','{2}');\r\n", (object)ring.ID, (object)ring.Members[key].CharID, (object)ring.Rights[key].Value);
            }
            this.SQLExecuteNonQuery(sqlstr);
        }

        /// <summary>
        /// The DeleteRing.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        public void DeleteRing(SagaDB.Ring.Ring ring)
        {
            this.SQLExecuteNonQuery(string.Format("DELETE FROM `ring` WHERE `ring_id`='{0}';", (object)ring.ID) + string.Format("DELETE FROM `ringmember` WHERE `ring_id`='{0}';", (object)ring.ID));
        }

        /// <summary>
        /// The RingEmblemUpdate.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="buf">The buf<see cref="byte[]"/>.</param>
        public void RingEmblemUpdate(SagaDB.Ring.Ring ring, byte[] buf)
        {
            this.SQLExecuteNonQuery(string.Format("UPDATE `ring` SET `emblem`=0x{0},`emblem_date`='{1}' WHERE `ring_id`='{2}' LIMIT 1;", (object)Conversions.bytes2HexString(buf), (object)this.ToSQLDateTime(DateTime.Now.ToUniversalTime()), (object)ring.ID));
        }

        /// <summary>
        /// The GetRingEmblem.
        /// </summary>
        /// <param name="ring_id">The ring_id<see cref="uint"/>.</param>
        /// <param name="date">The date<see cref="DateTime"/>.</param>
        /// <param name="needUpdate">The needUpdate<see cref="bool"/>.</param>
        /// <param name="newTime">The newTime<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetRingEmblem(uint ring_id, DateTime date, out bool needUpdate, out DateTime newTime)
        {
            DataRowCollection dataRowCollection = this.SQLExecuteQuery(string.Format("SELECT `emblem`,`emblem_date` FROM `ring` WHERE `ring_id`='{0}' LIMIT 1", (object)ring_id));
            if (dataRowCollection.Count != 0)
            {
                if (dataRowCollection[0]["emblem"].GetType() == typeof(DBNull))
                {
                    needUpdate = false;
                    newTime = DateTime.Now;
                    return (byte[])null;
                }
                newTime = (DateTime)dataRowCollection[0]["emblem_date"];
                if (date < newTime)
                {
                    needUpdate = true;
                    return (byte[])dataRowCollection[0]["emblem"];
                }
                needUpdate = false;
                return new byte[0];
            }
            needUpdate = false;
            newTime = DateTime.Now;
            return (byte[])null;
        }

        /// <summary>
        /// The GetBBSPage.
        /// </summary>
        /// <param name="bbsID">The bbsID<see cref="uint"/>.</param>
        /// <param name="page">The page<see cref="int"/>.</param>
        /// <returns>The <see cref="List{Post}"/>.</returns>
        public List<Post> GetBBSPage(uint bbsID, int page)
        {
            string sqlstr = string.Format("SELECT * FROM `bbs` WHERE `bbsid`='{0}' ORDER BY `postdate` DESC LIMIT {1},5;", (object)bbsID, (object)((page - 1) * 5));
            List<Post> postList = new List<Post>();
            foreach (DataRow dataRow in (InternalDataCollectionBase)this.SQLExecuteQuery(sqlstr))
                postList.Add(new Post()
                {
                    Name = (string)dataRow["name"],
                    Title = (string)dataRow["title"],
                    Content = (string)dataRow["content"],
                    Date = (DateTime)dataRow["postdate"]
                });
            return postList;
        }

        /// <summary>
        /// The BBSNewPost.
        /// </summary>
        /// <param name="poster">The poster<see cref="ActorPC"/>.</param>
        /// <param name="bbsID">The bbsID<see cref="uint"/>.</param>
        /// <param name="title">The title<see cref="string"/>.</param>
        /// <param name="content">The content<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool BBSNewPost(ActorPC poster, uint bbsID, string title, string content)
        {
            this.CheckSQLString(ref title);
            this.CheckSQLString(ref content);
            return this.SQLExecuteNonQuery(string.Format("INSERT INTO `bbs`(`bbsid`,`postdate`,`charid`,`name`,`title`,`content`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');", (object)bbsID, (object)this.ToSQLDateTime(DateTime.Now.ToUniversalTime()), (object)poster.CharID, (object)poster.Name, (object)title, (object)content));
        }

        /// <summary>
        /// The GetFGarden.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void GetFGarden(ActorPC pc)
        {
            DataRowCollection dataRowCollection = this.SQLExecuteQuery(string.Format("SELECT * FROM `fgarden` WHERE `account_id`='{0}' LIMIT 1;", (object)this.GetAccountID(pc)));
            if (dataRowCollection.Count > 0)
                pc.FGarden = new SagaDB.FGarden.FGarden(pc)
                {
                    ID = (uint)dataRowCollection[0]["fgarden_id"],
                    FGardenEquipments = {
            [FGardenSlot.FLYING_BASE] = (uint) dataRowCollection[0]["part1"],
            [FGardenSlot.FLYING_SAIL] = (uint) dataRowCollection[0]["part2"],
            [FGardenSlot.GARDEN_FLOOR] = (uint) dataRowCollection[0]["part3"],
            [FGardenSlot.GARDEN_MODELHOUSE] = (uint) dataRowCollection[0]["part4"],
            [FGardenSlot.HouseOutSideWall] = (uint) dataRowCollection[0]["part5"],
            [FGardenSlot.HouseRoof] = (uint) dataRowCollection[0]["part6"],
            [FGardenSlot.ROOM_FLOOR] = (uint) dataRowCollection[0]["part7"],
            [FGardenSlot.ROOM_WALL] = (uint) dataRowCollection[0]["part8"]
          }
                };
            if (pc.FGarden == null)
                return;
            foreach (DataRow dataRow in (InternalDataCollectionBase)this.SQLExecuteQuery(string.Format("SELECT * FROM `fgarden_furniture` WHERE `fgarden_id`='{0}';", (object)pc.FGarden.ID)))
            {
                FurniturePlace index = (FurniturePlace)(byte)dataRow["place"];
                ActorFurniture actorFurniture = new ActorFurniture();
                actorFurniture.ItemID = (uint)dataRow["item_id"];
                actorFurniture.PictID = (uint)dataRow["pict_id"];
                actorFurniture.X = (short)dataRow["x"];
                actorFurniture.Y = (short)dataRow["y"];
                actorFurniture.Z = (short)dataRow["z"];
                actorFurniture.Dir = (ushort)dataRow["dir"];
                actorFurniture.Motion = (ushort)dataRow["motion"];
                actorFurniture.Name = (string)dataRow["name"];
                pc.FGarden.Furnitures[index].Add(actorFurniture);
            }
        }

        /// <summary>
        /// The SaveFGarden.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void SaveFGarden(ActorPC pc)
        {
            if (pc.FGarden == null)
                return;
            uint accountId = this.GetAccountID(pc);
            if (pc.FGarden.ID > 0U)
            {
                this.SQLExecuteNonQuery(string.Format("UPDATE `fgarden` SET `part1`='{0}',`part2`='{1}',`part3`='{2}',`part4`='{3}',`part5`='{4}',`part6`='{5}',`part7`='{6}',`part8`='{7}' WHERE `fgarden_id`='{8}';", (object)pc.FGarden.FGardenEquipments[FGardenSlot.FLYING_BASE], (object)pc.FGarden.FGardenEquipments[FGardenSlot.FLYING_SAIL], (object)pc.FGarden.FGardenEquipments[FGardenSlot.GARDEN_FLOOR], (object)pc.FGarden.FGardenEquipments[FGardenSlot.GARDEN_MODELHOUSE], (object)pc.FGarden.FGardenEquipments[FGardenSlot.HouseOutSideWall], (object)pc.FGarden.FGardenEquipments[FGardenSlot.HouseRoof], (object)pc.FGarden.FGardenEquipments[FGardenSlot.ROOM_FLOOR], (object)pc.FGarden.FGardenEquipments[FGardenSlot.ROOM_WALL], (object)pc.FGarden.ID));
            }
            else
            {
                string sqlstr = string.Format("INSERT INTO `fgarden`(`account_id`,`part1`,`part2`,`part3`,`part4`,`part5`,`part6`,`part7`,`part8`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');", (object)accountId, (object)pc.FGarden.FGardenEquipments[FGardenSlot.FLYING_BASE], (object)pc.FGarden.FGardenEquipments[FGardenSlot.FLYING_SAIL], (object)pc.FGarden.FGardenEquipments[FGardenSlot.GARDEN_FLOOR], (object)pc.FGarden.FGardenEquipments[FGardenSlot.GARDEN_MODELHOUSE], (object)pc.FGarden.FGardenEquipments[FGardenSlot.HouseOutSideWall], (object)pc.FGarden.FGardenEquipments[FGardenSlot.HouseRoof], (object)pc.FGarden.FGardenEquipments[FGardenSlot.ROOM_FLOOR], (object)pc.FGarden.FGardenEquipments[FGardenSlot.ROOM_WALL]);
                uint index = 0;
                this.SQLExecuteScalar(sqlstr, out index);
                pc.FGarden.ID = index;
            }
            string sqlstr1 = string.Format("DELETE FROM `fgarden_furniture` WHERE `fgarden_id`='{0}';", (object)pc.FGarden.ID);
            foreach (ActorFurniture actorFurniture in pc.FGarden.Furnitures[FurniturePlace.GARDEN])
                sqlstr1 += string.Format("INSERT INTO `fgarden_furniture`(`fgarden_id`,`place`,`item_id`,`pict_id`,`x`,`y`,`z`,`dir`,`motion`,`name`) VALUES ('{0}','0','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');", (object)pc.FGarden.ID, (object)actorFurniture.ItemID, (object)actorFurniture.PictID, (object)actorFurniture.X, (object)actorFurniture.Y, (object)actorFurniture.Z, (object)actorFurniture.Dir, (object)actorFurniture.Motion, (object)actorFurniture.Name);
            foreach (ActorFurniture actorFurniture in pc.FGarden.Furnitures[FurniturePlace.ROOM])
                sqlstr1 += string.Format("INSERT INTO `fgarden_furniture`(`fgarden_id`,`place`,`item_id`,`pict_id`,`x`,`y`,`z`,`dir`,`motion`,`name`) VALUES ('{0}','1','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');", (object)pc.FGarden.ID, (object)actorFurniture.ItemID, (object)actorFurniture.PictID, (object)actorFurniture.X, (object)actorFurniture.Y, (object)actorFurniture.Z, (object)actorFurniture.Dir, (object)actorFurniture.Motion, (object)actorFurniture.Name);
            this.SQLExecuteNonQuery(sqlstr1);
        }

        /// <summary>
        /// The GetWRPRanking.
        /// </summary>
        /// <returns>The <see cref="List{ActorPC}"/>.</returns>
        public List<ActorPC> GetWRPRanking()
        {
            DataRowCollection dataRowCollection = this.SQLExecuteQuery("SELECT `char_id`,`name`,`dominionlv`,`dominionjlv`,`job`,`wrp` FROM `char` ORDER BY `wrp` DESC LIMIT 100;");
            List<ActorPC> actorPcList = new List<ActorPC>();
            uint num = 1;
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataRowCollection)
            {
                ActorPC actorPc = new ActorPC();
                actorPc.CharID = (uint)dataRow["char_id"];
                actorPc.Name = (string)dataRow["name"];
                actorPc.DominionLevel = (byte)dataRow["dominionlv"];
                actorPc.DominionJobLevel = (byte)dataRow["dominionjlv"];
                actorPc.Job = (PC_JOB)(byte)dataRow["job"];
                actorPc.WRP = (int)dataRow["wrp"];
                actorPc.WRPRanking = num;
                actorPcList.Add(actorPc);
                ++num;
            }
            return actorPcList;
        }
    }
}
