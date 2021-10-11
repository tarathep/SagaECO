namespace SagaDB.Mob
{
    using SagaDB.Actor;
    using SagaDB.Treasure;
    using SagaLib;
    using SagaLib.VirtualFileSystem;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="MobFactory" />.
    /// </summary>
    public class MobFactory : Singleton<MobFactory>
    {
        /// <summary>
        /// Defines the items.
        /// </summary>
        private Dictionary<uint, MobData> items = new Dictionary<uint, MobData>();

        /// <summary>
        /// Defines the pets.
        /// </summary>
        private Dictionary<uint, MobData> pets = new Dictionary<uint, MobData>();

        /// <summary>
        /// Defines the petLimits.
        /// </summary>
        private Dictionary<uint, MobData> petLimits = new Dictionary<uint, MobData>();

        /// <summary>
        /// The GetMobData.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="MobData"/>.</returns>
        public MobData GetMobData(uint id)
        {
            return this.items[id];
        }

        /// <summary>
        /// The GetPetData.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="MobData"/>.</returns>
        public MobData GetPetData(uint id)
        {
            return this.pets[id];
        }

        /// <summary>
        /// The GetPetLimit.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="MobData"/>.</returns>
        public MobData GetPetLimit(uint id)
        {
            if (this.petLimits.ContainsKey(id))
                return this.petLimits[id];
            return this.petLimits[10010003U];
        }

        /// <summary>
        /// Gets the Mobs.
        /// </summary>
        public Dictionary<uint, MobData> Mobs
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        public void Init(string path, Encoding encoding)
        {
            StreamReader streamReader = new StreamReader(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path), encoding);
            int num = 0;
            string label = "Loading mob database";
            Logger.ProgressBarShow(0U, (uint)streamReader.BaseStream.Length, label);
            DateTime now = DateTime.Now;
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                try
                {
                    if (!(str == ""))
                    {
                        if (!(str.Substring(0, 1) == "#"))
                        {
                            string[] strArray1 = str.Split(',');
                            for (int index = 0; index < strArray1.Length; ++index)
                            {
                                if (strArray1[index] == "" || strArray1[index].ToLower() == "null")
                                    strArray1[index] = "0";
                            }
                            MobData mobData = new MobData();
                            mobData.id = uint.Parse(strArray1[0]);
                            mobData.name = strArray1[1];
                            mobData.pictid = uint.Parse(strArray1[2]);
                            mobData.mobType = (MobType)Enum.Parse(typeof(MobType), strArray1[3].Replace(" ", "_"));
                            mobData.mobSize = float.Parse(strArray1[4]);
                            if (strArray1[7] == "1")
                                mobData.fly = true;
                            if (strArray1[8] == "1")
                                mobData.undead = true;
                            mobData.level = byte.Parse(strArray1[10]);
                            mobData.str = ushort.Parse(strArray1[11]);
                            mobData.mag = ushort.Parse(strArray1[12]);
                            mobData.vit = ushort.Parse(strArray1[13]);
                            mobData.dex = ushort.Parse(strArray1[14]);
                            mobData.agi = ushort.Parse(strArray1[15]);
                            mobData.intel = ushort.Parse(strArray1[16]);
                            mobData.hp = uint.Parse(strArray1[19]);
                            mobData.mp = uint.Parse(strArray1[20]);
                            mobData.sp = uint.Parse(strArray1[21]);
                            mobData.speed = ushort.Parse(strArray1[22]);
                            mobData.atk_min = ushort.Parse(strArray1[23]);
                            mobData.atk_max = ushort.Parse(strArray1[24]);
                            mobData.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), strArray1[25]);
                            mobData.matk_min = ushort.Parse(strArray1[26]);
                            mobData.matk_max = ushort.Parse(strArray1[27]);
                            mobData.def_add = ushort.Parse(strArray1[28]);
                            mobData.def = ushort.Parse(strArray1[29]);
                            mobData.mdef_add = ushort.Parse(strArray1[30]);
                            mobData.mdef = ushort.Parse(strArray1[31]);
                            mobData.hit_melee = ushort.Parse(strArray1[32]);
                            mobData.hit_ranged = ushort.Parse(strArray1[33]);
                            mobData.avoid_melee = ushort.Parse(strArray1[35]);
                            mobData.avoid_ranged = ushort.Parse(strArray1[36]);
                            mobData.cri = ushort.Parse(strArray1[38]);
                            mobData.aspd = short.Parse(strArray1[42]);
                            mobData.cspd = short.Parse(strArray1[43]);
                            for (int index = 0; index < 7; ++index)
                                mobData.elements.Add((Elements)index, int.Parse(strArray1[45 + index]));
                            mobData.baseExp = (uint)Math.Round((double)float.Parse(strArray1[97]), MidpointRounding.AwayFromZero);
                            mobData.jobExp = (uint)Math.Round((double)float.Parse(strArray1[98]), MidpointRounding.AwayFromZero);
                            for (int index = 0; index < 7; ++index)
                            {
                                if (strArray1[100 + index] != "0")
                                {
                                    try
                                    {
                                        MobData.DropData dropData = new MobData.DropData();
                                        string[] strArray2 = strArray1[100 + index].Split('|');
                                        dropData.Party = strArray2[0].StartsWith("P_");
                                        strArray2[0] = strArray2[0].Replace("P_", "");
                                        if (!uint.TryParse(strArray2[0], out dropData.ItemID))
                                        {
                                            dropData.TreasureGroup = strArray2[0];
                                            if (!FactoryString<TreasureFactory, TreasureList>.Instance.Items.ContainsKey(dropData.TreasureGroup))
                                                Logger.ShowWarning("Cannot find treasure group:" + dropData.TreasureGroup + "\r\n           Droped by:" + mobData.name + "(" + (object)mobData.id + ")");
                                        }
                                        dropData.Rate = strArray2.Length != 2 ? 100 : int.Parse(strArray2[1]);
                                        if (index < 5)
                                            mobData.dropItems.Add(dropData);
                                        else
                                            mobData.dropItemsSpecial.Add(dropData);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }
                            if (strArray1[107] != "0")
                                mobData.stampDrop = new MobData.DropData()
                                {
                                    ItemID = uint.Parse(strArray1[107]),
                                    Rate = int.Parse(strArray1[108])
                                };
                            this.items.Add(mobData.id, mobData);
                            if ((DateTime.Now - now).TotalMilliseconds > 40.0)
                            {
                                now = DateTime.Now;
                                Logger.ProgressBarShow((uint)streamReader.BaseStream.Position, (uint)streamReader.BaseStream.Length, label);
                            }
                            ++num;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing mob db!\r\nat line:" + str);
                    Logger.ShowError(ex);
                }
            }
            Logger.ProgressBarHide(num.ToString() + " mobs loaded.");
            streamReader.Close();
        }

        /// <summary>
        /// The InitPet.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        public void InitPet(string path, Encoding encoding)
        {
            StreamReader streamReader = new StreamReader(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path), encoding);
            int num = 0;
            string label = "Loading pet database";
            Logger.ProgressBarShow(0U, (uint)streamReader.BaseStream.Length, label);
            DateTime now = DateTime.Now;
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                try
                {
                    if (!(str == ""))
                    {
                        if (!(str.Substring(0, 1) == "#"))
                        {
                            string[] strArray1 = str.Split(',');
                            for (int index = 0; index < strArray1.Length; ++index)
                            {
                                if (strArray1[index] == "" || strArray1[index].ToLower() == "null")
                                    strArray1[index] = "0";
                            }
                            MobData mobData = new MobData();
                            mobData.id = uint.Parse(strArray1[0]);
                            mobData.name = strArray1[1];
                            mobData.pictid = uint.Parse(strArray1[2]);
                            try
                            {
                                mobData.mobType = (MobType)Enum.Parse(typeof(MobType), strArray1[3]);
                            }
                            catch (Exception ex)
                            {
                                while (strArray1[3].Substring(strArray1[3].Length - 1) != "_")
                                    strArray1[3] = strArray1[3].Substring(0, strArray1[3].Length - 1);
                                strArray1[3] = strArray1[3].Substring(0, strArray1[3].Length - 1);
                                mobData.mobType = (MobType)Enum.Parse(typeof(MobType), strArray1[3]);
                            }
                            mobData.mobSize = float.Parse(strArray1[4]);
                            mobData.level = byte.Parse(strArray1[9]);
                            mobData.str = ushort.Parse(strArray1[10]);
                            mobData.mag = ushort.Parse(strArray1[11]);
                            mobData.vit = ushort.Parse(strArray1[12]);
                            mobData.dex = ushort.Parse(strArray1[13]);
                            mobData.agi = ushort.Parse(strArray1[14]);
                            mobData.intel = ushort.Parse(strArray1[15]);
                            mobData.hp = uint.Parse(strArray1[18]);
                            mobData.mp = uint.Parse(strArray1[19]);
                            mobData.sp = uint.Parse(strArray1[20]);
                            mobData.speed = ushort.Parse(strArray1[21]);
                            mobData.atk_min = ushort.Parse(strArray1[22]);
                            mobData.atk_max = ushort.Parse(strArray1[23]);
                            mobData.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), strArray1[24]);
                            mobData.matk_min = ushort.Parse(strArray1[25]);
                            mobData.matk_max = ushort.Parse(strArray1[26]);
                            mobData.def_add = ushort.Parse(strArray1[27]);
                            mobData.def = ushort.Parse(strArray1[28]);
                            mobData.mdef_add = ushort.Parse(strArray1[29]);
                            mobData.mdef = ushort.Parse(strArray1[30]);
                            mobData.hit_melee = ushort.Parse(strArray1[31]);
                            mobData.hit_ranged = ushort.Parse(strArray1[32]);
                            mobData.avoid_melee = ushort.Parse(strArray1[34]);
                            mobData.avoid_ranged = ushort.Parse(strArray1[35]);
                            mobData.cri = ushort.Parse(strArray1[37]);
                            mobData.aspd = short.Parse(strArray1[41]);
                            mobData.cspd = short.Parse(strArray1[42]);
                            mobData.baseExp = uint.Parse(strArray1[96]);
                            mobData.jobExp = uint.Parse(strArray1[97]);
                            mobData.aiMode = int.Parse(strArray1[98]);
                            for (int index = 0; index < 7; ++index)
                            {
                                if (strArray1[99 + index] != "0")
                                {
                                    try
                                    {
                                        MobData.DropData dropData = new MobData.DropData();
                                        string[] strArray2 = strArray1[99 + index].Split('|');
                                        strArray2[0] = strArray2[0].Replace("P_", "");
                                        dropData.ItemID = uint.Parse(strArray2[0]);
                                        dropData.Rate = strArray2.Length != 2 ? 100 : int.Parse(strArray2[1]);
                                        mobData.dropItems.Add(dropData);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }
                            if (strArray1[106] != "0")
                                mobData.stampDrop = new MobData.DropData()
                                {
                                    ItemID = uint.Parse(strArray1[106]),
                                    Rate = int.Parse(strArray1[107])
                                };
                            this.pets.Add(mobData.id, mobData);
                            if ((DateTime.Now - now).TotalMilliseconds > 40.0)
                            {
                                now = DateTime.Now;
                                Logger.ProgressBarShow((uint)streamReader.BaseStream.Position, (uint)streamReader.BaseStream.Length, label);
                            }
                            ++num;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing pet db!\r\nat line:" + str);
                    Logger.ShowError(ex);
                }
            }
            Logger.ProgressBarHide(num.ToString() + " pets loaded.");
            streamReader.Close();
        }

        /// <summary>
        /// The InitPetLimit.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        public void InitPetLimit(string path, Encoding encoding)
        {
            StreamReader streamReader = new StreamReader(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path), encoding);
            int num = 0;
            string label = "Loading pet limit database";
            Logger.ProgressBarShow(0U, (uint)streamReader.BaseStream.Length, label);
            DateTime now = DateTime.Now;
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                try
                {
                    if (!(str == ""))
                    {
                        if (!(str.Substring(0, 1) == "#"))
                        {
                            string[] strArray1 = str.Split(',');
                            for (int index = 0; index < strArray1.Length; ++index)
                            {
                                if (strArray1[index] == "" || strArray1[index].ToLower() == "null")
                                    strArray1[index] = "0";
                            }
                            MobData mobData = new MobData();
                            mobData.id = uint.Parse(strArray1[0]);
                            mobData.name = strArray1[1];
                            mobData.str = ushort.Parse(strArray1[10]);
                            mobData.mag = ushort.Parse(strArray1[11]);
                            mobData.vit = ushort.Parse(strArray1[12]);
                            mobData.dex = ushort.Parse(strArray1[13]);
                            mobData.agi = ushort.Parse(strArray1[14]);
                            mobData.intel = ushort.Parse(strArray1[15]);
                            mobData.hp = uint.Parse(strArray1[18]);
                            mobData.mp = uint.Parse(strArray1[19]);
                            mobData.sp = uint.Parse(strArray1[20]);
                            mobData.speed = ushort.Parse(strArray1[21]);
                            mobData.atk_min = ushort.Parse(strArray1[22]);
                            mobData.atk_max = ushort.Parse(strArray1[23]);
                            mobData.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), strArray1[24]);
                            mobData.matk_min = ushort.Parse(strArray1[25]);
                            mobData.matk_max = ushort.Parse(strArray1[26]);
                            mobData.def_add = ushort.Parse(strArray1[27]);
                            mobData.def = ushort.Parse(strArray1[28]);
                            mobData.mdef_add = ushort.Parse(strArray1[29]);
                            mobData.mdef = ushort.Parse(strArray1[30]);
                            mobData.hit_melee = ushort.Parse(strArray1[31]);
                            mobData.hit_ranged = ushort.Parse(strArray1[32]);
                            mobData.avoid_melee = ushort.Parse(strArray1[34]);
                            mobData.avoid_ranged = ushort.Parse(strArray1[35]);
                            mobData.cri = ushort.Parse(strArray1[37]);
                            mobData.aspd = short.Parse(strArray1[41]);
                            mobData.cspd = short.Parse(strArray1[42]);
                            mobData.baseExp = uint.Parse(strArray1[96]);
                            mobData.jobExp = uint.Parse(strArray1[97]);
                            mobData.aiMode = int.Parse(strArray1[98]);
                            for (int index = 0; index < 7; ++index)
                            {
                                if (strArray1[99 + index] != "0")
                                {
                                    try
                                    {
                                        MobData.DropData dropData = new MobData.DropData();
                                        string[] strArray2 = strArray1[99 + index].Split('|');
                                        strArray2[0] = strArray2[0].Replace("P_", "");
                                        dropData.ItemID = uint.Parse(strArray2[0]);
                                        dropData.Rate = strArray2.Length != 2 ? 100 : int.Parse(strArray2[1]);
                                        mobData.dropItems.Add(dropData);
                                    }
                                    catch (Exception ex)
                                    {
                                    }
                                }
                            }
                            if (strArray1[106] != "0")
                                mobData.stampDrop = new MobData.DropData()
                                {
                                    ItemID = uint.Parse(strArray1[106]),
                                    Rate = int.Parse(strArray1[107])
                                };
                            this.petLimits.Add(mobData.id, mobData);
                            if ((DateTime.Now - now).TotalMilliseconds > 40.0)
                            {
                                now = DateTime.Now;
                                Logger.ProgressBarShow((uint)streamReader.BaseStream.Position, (uint)streamReader.BaseStream.Length, label);
                            }
                            ++num;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing pet limit db!\r\nat line:" + str);
                    Logger.ShowError(ex);
                }
            }
            Logger.ProgressBarHide(num.ToString() + " pet limits loaded.");
            streamReader.Close();
        }
    }
}
