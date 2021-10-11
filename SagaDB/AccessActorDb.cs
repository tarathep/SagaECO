namespace SagaDB
{
    using SagaDB.Actor;
    using SagaDB.BBS;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Quests;
    using SagaDB.Skill;
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.OleDb;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="AccessActorDb" />.
    /// </summary>
    public class AccessActorDb : AccessConnectivity, ActorDB
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
        /// Defines the Source.
        /// </summary>
        private string Source;

        /// <summary>
        /// Defines the isconnected.
        /// </summary>
        private bool isconnected;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessActorDb"/> class.
        /// </summary>
        /// <param name="Source">The Source<see cref="string"/>.</param>
        public AccessActorDb(string Source)
        {
            this.Source = Source;
            this.isconnected = false;
            try
            {
                this.db = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", (object)this.Source));
                this.dbinactive = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", (object)this.Source));
                this.db.Open();
            }
            catch (OleDbException ex)
            {
                Logger.ShowSQL((Exception)ex, (Logger)null);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, (Logger)null);
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
            if (this.db.State == ConnectionState.Open)
            {
                this.isconnected = true;
                return true;
            }
            this.isconnected = false;
            return false;
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
            string sqlstr = string.Format("INSERT INTO `char`(`account_id`,`name`,`race`,`gender`,`hairStyle`,`hairColor`,`wig`,`face`,`job`,`mapID`,`lv`,`jlv1`,`jlv2x`,`jlv2t`,`questRemaining`,`slot`,`x`,`y`,`dir`,`hp`,`max_hp`,`mp`,`max_mp`,`sp`,`max_sp`,`str`,`dex`,`intel`,`vit`,`agi`,`mag`,`statspoint`,`skillpoint`,`skillpoint2x`,`skillpoint2t`,`gold`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}')", (object)account_id, (object)name, (object)(byte)aChar.Race, (object)(byte)aChar.Gender, (object)aChar.HairStyle, (object)aChar.HairColor, (object)aChar.Wig, (object)aChar.Face, (object)(byte)aChar.Job, (object)aChar.MapID, (object)aChar.Level, (object)aChar.JobLevel1, (object)aChar.JobLevel2X, (object)aChar.JobLevel2T, (object)aChar.QuestRemaining, (object)aChar.Slot, (object)Global.PosX16to8(aChar.X, mapInfo.width), (object)Global.PosY16to8(aChar.Y, mapInfo.height), (object)(byte)((uint)aChar.Dir / 45U), (object)aChar.HP, (object)aChar.MaxHP, (object)aChar.MP, (object)aChar.MaxMP, (object)aChar.SP, (object)aChar.MaxSP, (object)aChar.Str, (object)aChar.Dex, (object)aChar.Int, (object)aChar.Vit, (object)aChar.Agi, (object)aChar.Mag, (object)aChar.StatsPoint, (object)aChar.SkillPoint, (object)aChar.SkillPoint2X, (object)aChar.SkillPoint2T, (object)aChar.Gold);
            try
            {
                this.SQLExecuteScalar(sqlstr, ref index);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            aChar.CharID = index;
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
            catch (OleDbException ex)
            {
                Logger.ShowSQL((Exception)ex, (Logger)null);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
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
            MapInfo mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[aChar.MapID];
            if (aChar == null || !this.isConnected())
                return;
            uint num1 = 0;
            uint num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            DateTime date = DateTime.Now;
            QuestStatus questStatus = QuestStatus.OPEN;
            if (aChar.Quest != null)
            {
                num1 = aChar.Quest.ID;
                num3 = aChar.Quest.CurrentCount1;
                num4 = aChar.Quest.CurrentCount2;
                num5 = aChar.Quest.CurrentCount3;
                date = aChar.Quest.EndTime;
                questStatus = aChar.Quest.Status;
            }
            if (aChar.Party != null)
                num2 = aChar.Party.ID;
            string sqlstr = string.Format("UPDATE `char` SET `name`='{0}',`race`='{1}',`gender`='{2}',`hairStyle`='{3}',`hairColor`='{4}',`wig`='{5}',`face`='{6}',`job`='{7}',`mapID`='{8}',`lv`='{9}',`jlv1`='{10}',`jlv2x`='{11}',`jlv2t`='{12}',`questRemaining`='{13}',`slot`='{14}',`x`='{16}',`y`='{17}',`dir`='{18}',`hp`='{19}',`max_hp`='{20}',`mp`='{21}',`max_mp`='{22}',`sp`='{23}',`max_sp`='{24}',`str`='{25}',`dex`='{26}',`intel`='{27}',`vit`='{28}',`agi`='{29}',`mag`='{30}',`statspoint`='{31}',`skillpoint`='{32}',`skillpoint2x`='{33}',`skillpoint2t`='{34}',`gold`='{35}',`cexp`='{36}',`jexp`='{37}',`save_map`='{38}',`save_x`='{39}',`save_y`='{40}',`possession_target`='{41}',`questid`='{42}',`questendtime`='{43}',`queststatus`='{44}',`questcurrentcount1`='{45}',`questcurrentcount2`='{46}',`questcurrentcount3`='{47}',`questresettime`='{48}',`fame`='{49}',`party`='{50}' WHERE char_id='{15}' LIMIT 1", (object)aChar.Name, (object)(byte)aChar.Race, (object)(byte)aChar.Gender, (object)aChar.HairStyle, (object)aChar.HairColor, (object)aChar.Wig, (object)aChar.Face, (object)(byte)aChar.Job, (object)aChar.MapID, (object)aChar.Level, (object)aChar.JobLevel1, (object)aChar.JobLevel2X, (object)aChar.JobLevel2T, (object)aChar.QuestRemaining, (object)aChar.Slot, (object)aChar.CharID, (object)Global.PosX16to8(aChar.X, mapInfo.width), (object)Global.PosY16to8(aChar.Y, mapInfo.height), (object)(byte)((uint)aChar.Dir / 45U), (object)aChar.HP, (object)aChar.MaxHP, (object)aChar.MP, (object)aChar.MaxMP, (object)aChar.SP, (object)aChar.MaxSP, (object)aChar.Str, (object)aChar.Dex, (object)aChar.Int, (object)aChar.Vit, (object)aChar.Agi, (object)aChar.Mag, (object)aChar.StatsPoint, (object)aChar.SkillPoint, (object)aChar.SkillPoint2X, (object)aChar.SkillPoint2T, (object)aChar.Gold, (object)aChar.CEXP, (object)aChar.JEXP, (object)aChar.SaveMap, (object)aChar.SaveX, (object)aChar.SaveY, (object)aChar.PossessionTarget, (object)num1, (object)this.ToSQLDateTime(date), (object)(byte)questStatus, (object)num3, (object)num4, (object)num5, (object)this.ToSQLDateTime(aChar.QuestNextResetTime), (object)aChar.Fame, (object)num2);
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            this.SaveItem(aChar);
            this.SaveSkill(aChar);
            this.SaveVar(aChar);
        }

        /// <summary>
        /// The DeleteChar.
        /// </summary>
        /// <param name="aChar">The aChar<see cref="ActorPC"/>.</param>
        public void DeleteChar(ActorPC aChar)
        {
            uint num = (uint)this.SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + (object)aChar.CharID + "' LIMIT 1")[0]["account_id"];
            string sqlstr = "DELETE FROM `char` WHERE char_id='" + (object)aChar.CharID + "';" + "DELETE FROM `inventory` WHERE char_id='" + (object)aChar.CharID + "';" + "DELETE FROM `skill` WHERE char_id='" + (object)aChar.CharID + "';" + "DELETE FROM `cVar` WHERE char_id='" + (object)aChar.CharID + "';" + "DELETE FROM `aVar` WHERE account_id='" + (object)num + "';" + "DELETE FROM `friend` WHERE `char_id`='" + (object)aChar.CharID + "' OR `friend_char_id`='" + (object)aChar.CharID + "';";
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
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
            string sqlstr = "SELECT * FROM `char` WHERE `char_id`='" + (object)charID + "' LIMIT 1";
            DataRow dataRow;
            try
            {
                dataRow = this.SQLExecuteQuery(sqlstr)[0];
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return (ActorPC)null;
            }
            ActorPC pc = new ActorPC();
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
            pc.QuestRemaining = (ushort)dataRow["questRemaining"];
            pc.QuestNextResetTime = (DateTime)dataRow["questresettime"];
            pc.Fame = (uint)dataRow["fame"];
            pc.Slot = (byte)dataRow["slot"];
            if (fullinfo)
            {
                MapInfo mapInfo = Singleton<MapInfoFactory>.Instance.MapInfo[pc.MapID];
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
            pc.Str = (ushort)dataRow["str"];
            pc.Dex = (ushort)dataRow["dex"];
            pc.Int = (ushort)dataRow["intel"];
            pc.Vit = (ushort)dataRow["vit"];
            pc.Agi = (ushort)dataRow["agi"];
            pc.Mag = (ushort)dataRow["mag"];
            pc.StatsPoint = (ushort)dataRow["statspoint"];
            pc.SkillPoint = (ushort)dataRow["skillpoint"];
            pc.SkillPoint2X = (ushort)dataRow["skillpoint2x"];
            pc.SkillPoint2T = (ushort)dataRow["skillpoint2t"];
            pc.Gold = (int)dataRow["gold"];
            pc.CEXP = (uint)dataRow["cexp"];
            pc.JEXP = (uint)dataRow["jexp"];
            pc.PossessionTarget = (uint)dataRow["possession_target"];
            SagaDB.Party.Party party = new SagaDB.Party.Party();
            party.ID = (uint)dataRow["party"];
            if (party.ID != 0U)
                pc.Party = party;
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
            }
            this.GetItem(pc);
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
        /// The SaveSkill.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void SaveSkill(ActorPC pc)
        {
            string sqlstr = "DELETE FROM `skill` WHERE char_id='" + (object)pc.CharID + "';";
            foreach (SagaDB.Skill.Skill skill in pc.Skills.Values)
                sqlstr += string.Format("INSERT INTO `skill`(`char_id`,`skill_id`,`lv`) VALUES ('{0}','{1}','{2}');", (object)pc.CharID, (object)skill.ID, (object)skill.Level);
            foreach (SagaDB.Skill.Skill skill in pc.Skills2.Values)
                sqlstr += string.Format("INSERT INTO `skill`(`char_id`,`skill_id`,`lv`) VALUES ('{0}','{1}','{2}');", (object)pc.CharID, (object)skill.ID, (object)skill.Level);
            foreach (SagaDB.Skill.Skill skill in pc.SkillsReserve.Values)
                sqlstr += string.Format("INSERT INTO `skill`(`char_id`,`skill_id`,`lv`) VALUES ('{0}','{1}','{2}');", (object)pc.CharID, (object)skill.ID, (object)skill.Level);
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The SaveVar.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void SaveVar(ActorPC pc)
        {
            string str = "DELETE FROM `cVar` WHERE char_id='" + (object)pc.CharID + "';";
            uint num = (uint)this.SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + (object)pc.CharID + "' LIMIT 1")[0]["account_id"];
            string sqlstr = str + "DELETE FROM `aVar` WHERE account_id='" + (object)num + "';";
            foreach (string key in (IEnumerable<string>)pc.AStr.Keys)
                sqlstr += string.Format("INSERT INTO `aVar`(`account_id`,`name`,`type`,`content`) VALUES ('{0}','{1}',0,'{2}');", (object)num, (object)key, (object)pc.AStr[key]);
            foreach (string key in (IEnumerable<string>)pc.AInt.Keys)
                sqlstr += string.Format("INSERT INTO `aVar`(`account_id`,`name`,`type`,`content`) VALUES ('{0}','{1}',1,'{2}');", (object)num, (object)key, (object)pc.AInt[key]);
            foreach (string key in (IEnumerable<string>)pc.AMask.Keys)
                sqlstr += string.Format("INSERT INTO `aVar`(`account_id`,`name`,`type`,`content`) VALUES ('{0}','{1}',2,'{2}');", (object)num, (object)key, (object)pc.AMask[key].Value);
            foreach (string key in (IEnumerable<string>)pc.CStr.Keys)
                sqlstr += string.Format("INSERT INTO `cVar`(`char_id`,`name`,`type`,`content`) VALUES ('{0}','{1}',0,'{2}');", (object)pc.CharID, (object)key, (object)pc.CStr[key]);
            foreach (string key in (IEnumerable<string>)pc.CInt.Keys)
                sqlstr += string.Format("INSERT INTO `cVar`(`char_id`,`name`,`type`,`content`) VALUES ('{0}','{1}',1,'{2}');", (object)pc.CharID, (object)key, (object)pc.CInt[key]);
            foreach (string key in (IEnumerable<string>)pc.CMask.Keys)
                sqlstr += string.Format("INSERT INTO `cVar`(`char_id`,`name`,`type`,`content`) VALUES ('{0}','{1}',2,'{2}');", (object)pc.CharID, (object)key, (object)pc.CMask[key].Value);
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The GetVar.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void GetVar(ActorPC pc)
        {
            string sqlstr = "SELECT * FROM `cVar` WHERE `char_id`='" + (object)pc.CharID + "';";
            uint num = (uint)this.SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + (object)pc.CharID + "' LIMIT 1")[0]["account_id"];
            foreach (DataRow dataRow in (InternalDataCollectionBase)this.SQLExecuteQuery(sqlstr))
            {
                switch ((byte)dataRow["type"])
                {
                    case 0:
                        pc.CStr[(string)dataRow["name"]] = (string)dataRow["content"];
                        continue;
                    case 1:
                        pc.CInt[(string)dataRow["name"]] = int.Parse((string)dataRow["content"]);
                        continue;
                    case 2:
                        pc.CMask[(string)dataRow["name"]] = new BitMask(int.Parse((string)dataRow["content"]));
                        continue;
                    default:
                        continue;
                }
            }
            foreach (DataRow dataRow in (InternalDataCollectionBase)this.SQLExecuteQuery("SELECT * FROM `aVar` WHERE `account_id`='" + (object)num + "';"))
            {
                switch ((byte)dataRow["type"])
                {
                    case 0:
                        pc.AStr[(string)dataRow["name"]] = (string)dataRow["content"];
                        continue;
                    case 1:
                        pc.AInt[(string)dataRow["name"]] = int.Parse((string)dataRow["content"]);
                        continue;
                    case 2:
                        pc.AMask[(string)dataRow["name"]] = new BitMask(int.Parse((string)dataRow["content"]));
                        continue;
                    default:
                        continue;
                }
            }
        }

        /// <summary>
        /// The NewItem.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        /// <param name="container">The container<see cref="ContainerType"/>.</param>
        public void NewItem(ActorPC pc, SagaDB.Item.Item item, ContainerType container)
        {
        }

        /// <summary>
        /// The DeleteItem.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        public void DeleteItem(ActorPC pc, SagaDB.Item.Item item)
        {
            this.SQLExecuteNonQuery(string.Format("DELETE FROM `inventory` WHERE `char_id`='{0}' AND `id`='{01}';", (object)pc.CharID, (object)item.DBID));
        }

        /// <summary>
        /// The NewWarehouseItem.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        /// <param name="container">The container<see cref="WarehousePlace"/>.</param>
        public void NewWarehouseItem(ActorPC pc, SagaDB.Item.Item item, WarehousePlace container)
        {
        }

        /// <summary>
        /// The DeleteWarehouseItem.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        public void DeleteWarehouseItem(ActorPC pc, SagaDB.Item.Item item)
        {
            this.SQLExecuteNonQuery(string.Format("DELETE FROM `inventory` WHERE `account_id`='{0}' AND `id`='{01}';", (object)this.GetAccountID(pc), (object)item.DBID));
        }

        /// <summary>
        /// The SaveItem.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void SaveItem(ActorPC pc)
        {
            string[] names = Enum.GetNames(typeof(ContainerType));
            string sqlstr = "DELETE FROM `inventory` WHERE char_id='" + (object)pc.CharID + "';";
            foreach (string str in names)
            {
                ContainerType container = (ContainerType)Enum.Parse(typeof(ContainerType), str);
                foreach (SagaDB.Item.Item obj in pc.Inventory.GetContainer(container))
                    sqlstr += string.Format("INSERT INTO `inventory`(`char_id`,`itemID`,`durability`,`identified`,`stack`,`container`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');", (object)pc.CharID, (object)obj.ItemID, (object)obj.Durability, (object)obj.identified, (object)obj.Stack, (object)(byte)container);
            }
            try
            {
                this.SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The GetSkill.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void GetSkill(ActorPC pc)
        {
            string sqlstr = "SELECT * FROM `skill` WHERE `char_id`='" + (object)pc.CharID + "';";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
            Dictionary<uint, byte> dictionary1 = Singleton<SkillFactory>.Instance.SkillList(pc.JobBasic);
            Dictionary<uint, byte> dictionary2 = Singleton<SkillFactory>.Instance.SkillList(pc.Job2X);
            Dictionary<uint, byte> dictionary3 = Singleton<SkillFactory>.Instance.SkillList(pc.Job2T);
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataRowCollection)
            {
                SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill((uint)dataRow["skill_id"], (byte)dataRow["lv"]);
                if (dictionary1.ContainsKey(skill.ID))
                    pc.Skills.Add(skill.ID, skill);
                else if (dictionary2.ContainsKey(skill.ID))
                {
                    if (pc.Job == pc.Job2X)
                        pc.Skills2.Add(skill.ID, skill);
                    else
                        pc.SkillsReserve.Add(skill.ID, skill);
                }
                else if (dictionary3.ContainsKey(skill.ID))
                {
                    if (pc.Job == pc.Job2T)
                        pc.Skills2.Add(skill.ID, skill);
                    else
                        pc.SkillsReserve.Add(skill.ID, skill);
                }
                else
                    pc.Skills.Add(skill.ID, skill);
            }
        }

        /// <summary>
        /// The GetItem.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void GetItem(ActorPC pc)
        {
            string sqlstr = "SELECT * FROM `inventory` WHERE `char_id`='" + (object)pc.CharID + "';";
            DataRowCollection dataRowCollection;
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
            foreach (DataRow dataRow in (InternalDataCollectionBase)dataRowCollection)
            {
                SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem((uint)dataRow["itemID"]);
                obj.Durability = (ushort)dataRow["durability"];
                obj.Stack = (ushort)dataRow["stack"];
                obj.identified = (byte)dataRow["identified"];
                ContainerType container = (ContainerType)(byte)dataRow["container"];
                int num = (int)pc.Inventory.AddItem(container, obj);
            }
        }

        /// <summary>
        /// The CharExists.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CharExists(string name)
        {
            DataRowCollection dataRowCollection = (DataRowCollection)null;
            string sqlstr = "SELECT count(*) FROM `char` WHERE name='" + name + "'";
            try
            {
                dataRowCollection = this.SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            return Convert.ToInt32(dataRowCollection[0][0]) > 0;
        }

        /// <summary>
        /// The GetAccountID.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetAccountID(ActorPC pc)
        {
            return this.GetAccountID(pc.CharID);
        }

        /// <summary>
        /// The GetAccountID.
        /// </summary>
        /// <param name="charID">The charID<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetAccountID(uint charID)
        {
            DataRowCollection dataRowCollection = this.SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='`" + (object)charID + "' LIMIT 1;");
            if (dataRowCollection.Count == 0)
                return 0;
            return (uint)dataRowCollection[0]["account_id"];
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
                Logger.ShowError(ex);
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
            return party;
        }

        /// <summary>
        /// The NewParty.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        public void NewParty(SagaDB.Party.Party party)
        {
            uint index = 0;
            this.SQLExecuteScalar(string.Format("INSERT INTO `party`(`leader`,`name`,`member1`,`member2`,`member3`,`member4`,`member5`,`member6`,`member7`,`member8`) VALUES ('0','{0}','0','0','0','0','0','0','0','0');", (object)party.Name), ref index);
            party.ID = index;
        }

        /// <summary>
        /// The SaveParty.
        /// </summary>
        /// <param name="party">The party<see cref="SagaDB.Party.Party"/>.</param>
        public void SaveParty(SagaDB.Party.Party party)
        {
            this.SQLExecuteNonQuery(string.Format("UPDATE `party` SET `leader`='{0}',`member1`='{1}',`member2`='{2}',`member3`='{3}',`member4`='{4}',`member5`='{5}',`member6`='{6}',`member7`='{7}',`member8`='{8}',`name`='{10}' WHERE `party_id`='{9}' LIMIT 1;", (object)party.Leader.CharID, (object)(!party.Members.ContainsKey((byte)0) ? 0U : party[(byte)0].CharID), (object)(!party.Members.ContainsKey((byte)1) ? 0U : party[(byte)1].CharID), (object)(!party.Members.ContainsKey((byte)2) ? 0U : party[(byte)2].CharID), (object)(!party.Members.ContainsKey((byte)3) ? 0U : party[(byte)3].CharID), (object)(!party.Members.ContainsKey((byte)4) ? 0U : party[(byte)4].CharID), (object)(!party.Members.ContainsKey((byte)5) ? 0U : party[(byte)5].CharID), (object)(!party.Members.ContainsKey((byte)6) ? 0U : party[(byte)6].CharID), (object)(!party.Members.ContainsKey((byte)7) ? 0U : party[(byte)7].CharID), (object)party.ID, (object)party.Name));
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
            return (SagaDB.Ring.Ring)null;
        }

        /// <summary>
        /// The NewRing.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        public void NewRing(SagaDB.Ring.Ring ring)
        {
        }

        /// <summary>
        /// The SaveRing.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="saveMembers">The saveMembers<see cref="bool"/>.</param>
        public void SaveRing(SagaDB.Ring.Ring ring, bool saveMembers)
        {
        }

        /// <summary>
        /// The DeleteRing.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        public void DeleteRing(SagaDB.Ring.Ring ring)
        {
        }

        /// <summary>
        /// The GetBBSPage.
        /// </summary>
        /// <param name="bbsID">The bbsID<see cref="uint"/>.</param>
        /// <param name="page">The page<see cref="int"/>.</param>
        /// <returns>The <see cref="List{Post}"/>.</returns>
        public List<Post> GetBBSPage(uint bbsID, int page)
        {
            return new List<Post>();
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
            return false;
        }

        /// <summary>
        /// The RingEmblemUpdate.
        /// </summary>
        /// <param name="ring">The ring<see cref="SagaDB.Ring.Ring"/>.</param>
        /// <param name="buf">The buf<see cref="byte[]"/>.</param>
        public void RingEmblemUpdate(SagaDB.Ring.Ring ring, byte[] buf)
        {
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
            needUpdate = false;
            newTime = DateTime.Now;
            return (byte[])null;
        }

        /// <summary>
        /// The LoadServerVar.
        /// </summary>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        public ActorPC LoadServerVar()
        {
            return (ActorPC)null;
        }

        /// <summary>
        /// The SaveServerVar.
        /// </summary>
        /// <param name="fakepc">The fakepc<see cref="ActorPC"/>.</param>
        public void SaveServerVar(ActorPC fakepc)
        {
        }

        /// <summary>
        /// The GetVShop.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void GetVShop(ActorPC pc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The SaveVShop.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SaveVShop(ActorPC pc)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The SaveWRP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void SaveWRP(ActorPC pc)
        {
        }

        /// <summary>
        /// The GetWRPRanking.
        /// </summary>
        /// <returns>The <see cref="List{ActorPC}"/>.</returns>
        public List<ActorPC> GetWRPRanking()
        {
            return (List<ActorPC>)null;
        }
    }
}
