namespace SagaDB.Marionette
{
    using SagaDB.Mob;
    using SagaLib;
    using SagaLib.VirtualFileSystem;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="MarionetteFactory" />.
    /// </summary>
    public class MarionetteFactory : Singleton<MarionetteFactory>
    {
        /// <summary>
        /// Defines the data.
        /// </summary>
        private Dictionary<uint, SagaDB.Marionette.Marionette> data = new Dictionary<uint, SagaDB.Marionette.Marionette>();


        public SagaDB.Marionette.Marionette this[uint index]
        {
            get
            {
                if (this.data.ContainsKey(index))
                    return this.data[index];
                return (SagaDB.Marionette.Marionette)null;
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
            Logger.ShowInfo("Loading marionette database...");
            Console.ForegroundColor = ConsoleColor.Green;
            int num = 0;
            while (!streamReader.EndOfStream)
            {
                string str = streamReader.ReadLine();
                try
                {
                    if (!(str == ""))
                    {
                        if (!(str.Substring(0, 1) == "#"))
                        {
                            string[] strArray = str.Split(',');
                            for (int index = 0; index < strArray.Length; ++index)
                            {
                                if (strArray[index] == "" || strArray[index].ToLower() == "null")
                                    strArray[index] = "0";
                            }
                            SagaDB.Marionette.Marionette marionette = new SagaDB.Marionette.Marionette();
                            marionette.ID = uint.Parse(strArray[0]);
                            marionette.Name = strArray[1];
                            marionette.PictID = uint.Parse(strArray[2]);
                            marionette.MobType = (MobType)Enum.Parse(typeof(MobType), strArray[3]);
                            marionette.Duration = int.Parse(strArray[4]);
                            marionette.Delay = int.Parse(strArray[5]);
                            marionette.str = short.Parse(strArray[24]);
                            marionette.mag = short.Parse(strArray[25]);
                            marionette.vit = short.Parse(strArray[26]);
                            marionette.dex = short.Parse(strArray[27]);
                            marionette.agi = short.Parse(strArray[28]);
                            marionette.intel = short.Parse(strArray[29]);
                            marionette.hp = short.Parse(strArray[32]);
                            marionette.sp = short.Parse(strArray[33]);
                            marionette.mp = short.Parse(strArray[34]);
                            marionette.move_speed = short.Parse(strArray[35]);
                            marionette.min_atk1 = short.Parse(strArray[36]);
                            marionette.max_atk1 = short.Parse(strArray[37]);
                            marionette.min_atk2 = short.Parse(strArray[38]);
                            marionette.max_atk2 = short.Parse(strArray[39]);
                            marionette.min_atk3 = short.Parse(strArray[40]);
                            marionette.max_atk3 = short.Parse(strArray[41]);
                            marionette.min_matk = short.Parse(strArray[42]);
                            marionette.max_matk = short.Parse(strArray[43]);
                            marionette.def_add = short.Parse(strArray[44]);
                            marionette.def = short.Parse(strArray[45]);
                            marionette.mdef_add = short.Parse(strArray[46]);
                            marionette.mdef = short.Parse(strArray[47]);
                            marionette.hit_melee = short.Parse(strArray[48]);
                            marionette.hit_ranged = short.Parse(strArray[49]);
                            marionette.hit_magic = short.Parse(strArray[50]);
                            marionette.avoid_melee = short.Parse(strArray[51]);
                            marionette.avoid_ranged = short.Parse(strArray[52]);
                            marionette.avoid_magic = short.Parse(strArray[53]);
                            marionette.hit_cri = short.Parse(strArray[54]);
                            marionette.avoid_cri = short.Parse(strArray[55]);
                            marionette.hp_recover = short.Parse(strArray[56]);
                            marionette.mp_recover = short.Parse(strArray[57]);
                            marionette.aspd = short.Parse(strArray[58]);
                            marionette.cspd = short.Parse(strArray[59]);
                            for (int index = 0; index < 6; ++index)
                            {
                                if (strArray[77 + index] != "0")
                                    marionette.skills.Add(ushort.Parse(strArray[77 + index]));
                            }
                            for (int index = 0; index < 8; ++index)
                            {
                                if (strArray[83 + index] == "1")
                                    marionette.gather.Add((GatherType)index, true);
                                else
                                    marionette.gather.Add((GatherType)index, false);
                            }
                            this.data.Add(marionette.ID, marionette);
                            ++num;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing marionette db!\r\nat line:" + str);
                    Logger.ShowError(ex);
                }
            }
            Console.ResetColor();
            Logger.ShowInfo(num.ToString() + " marionette loaded.");
            streamReader.Close();
        }
    }
}
