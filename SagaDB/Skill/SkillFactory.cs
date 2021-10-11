namespace SagaDB.Skill
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaLib.VirtualFileSystem;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="SkillFactory" />.
    /// </summary>
    public class SkillFactory : Singleton<SkillFactory>
    {
        /// <summary>
        /// Defines the items.
        /// </summary>
        private Dictionary<uint, Dictionary<byte, SkillData>> items = new Dictionary<uint, Dictionary<byte, SkillData>>();

        /// <summary>
        /// Defines the skills.
        /// </summary>
        private Dictionary<PC_JOB, Dictionary<uint, byte>> skills = new Dictionary<PC_JOB, Dictionary<uint, byte>>();

        /// <summary>
        /// The GetSkill.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        /// <returns>The <see cref="SagaDB.Skill.Skill"/>.</returns>
        public SagaDB.Skill.Skill GetSkill(uint id, byte level)
        {
            try
            {
                if (!this.items.ContainsKey(id))
                {
                    Logger.ShowDebug("Cannot find skill:" + id.ToString(), (Logger)null);
                    return (SagaDB.Skill.Skill)null;
                }
                if (level != (byte)0)
                {
                    SkillData skillData = this.items[id][level];
                    return new SagaDB.Skill.Skill()
                    {
                        BaseData = skillData,
                        Level = level
                    };
                }
                SkillData skillData1 = this.items[id][(byte)1];
                return new SagaDB.Skill.Skill()
                {
                    BaseData = skillData1,
                    Level = level
                };
            }
            catch
            {
                Logger.ShowDebug("Cannot find skill:" + id.ToString() + " with level:" + (object)level, (Logger)null);
                return (SagaDB.Skill.Skill)null;
            }
        }

        /// <summary>
        /// The SkillList.
        /// </summary>
        /// <param name="job">The job<see cref="PC_JOB"/>.</param>
        /// <returns>The <see cref="Dictionary{uint, byte}"/>.</returns>
        public Dictionary<uint, byte> SkillList(PC_JOB job)
        {
            if (this.skills.ContainsKey(job))
                return this.skills[job];
            return new Dictionary<uint, byte>();
        }

        /// <summary>
        /// The LoadSkillList.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void LoadSkillList(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path));
                foreach (object childNode in xmlDocument["SkillList"].ChildNodes)
                {
                    if (childNode.GetType() == typeof(XmlElement))
                    {
                        XmlElement xmlElement1 = (XmlElement)childNode;
                        switch (xmlElement1.Name.ToLower())
                        {
                            case "skills":
                                PC_JOB key = (PC_JOB)Enum.Parse(typeof(PC_JOB), xmlElement1.Attributes["Job"].Value, true);
                                Dictionary<uint, byte> dictionary;
                                if (!this.skills.ContainsKey(key))
                                {
                                    dictionary = new Dictionary<uint, byte>();
                                    this.skills.Add(key, dictionary);
                                }
                                else
                                    dictionary = this.skills[key];
                                IEnumerator enumerator = xmlElement1.ChildNodes.GetEnumerator();
                                try
                                {
                                    while (enumerator.MoveNext())
                                    {
                                        object current = enumerator.Current;
                                        if (current.GetType() == typeof(XmlElement))
                                        {
                                            XmlElement xmlElement2 = (XmlElement)current;
                                            switch (xmlElement2.Name.ToLower())
                                            {
                                                case "skillid":
                                                    dictionary.Add(uint.Parse(xmlElement2.InnerText), byte.Parse(xmlElement2.Attributes["JobLV"].InnerText));
                                                    continue;
                                                default:
                                                    continue;
                                            }
                                        }
                                    }
                                    continue;
                                }
                                finally
                                {
                                    (enumerator as IDisposable)?.Dispose();
                                }
                            default:
                                continue;
                        }
                    }
                }
                Logger.ShowInfo("Skill list for jobs loaded...");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The Convert.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        public void Convert(string path, Encoding encoding)
        {
            StreamReader streamReader = new StreamReader(path, encoding);
            StreamWriter streamWriter = new StreamWriter(path + ".csv", false, encoding);
            streamWriter.WriteLine("#ID,Name,主动,最大Lv,Lv,JobLv,MP,SP,吟唱时间,延迟,射程,目标,目标2,范围,技能释放射程");
            Logger.ShowInfo("Loading skill database...");
            Console.ForegroundColor = ConsoleColor.Green;
            int num = 0;
            bool flag = true;
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
                                if (strArray[index] == "")
                                    strArray[index] = "0";
                            }
                            SkillData skillData = new SkillData();
                            skillData.id = uint.Parse(strArray[0]);
                            skillData.name = strArray[1];
                            skillData.active = strArray[3] == "1";
                            skillData.maxLv = byte.Parse(strArray[4]);
                            skillData.lv = byte.Parse(strArray[5]);
                            skillData.mp = (ushort)byte.Parse(strArray[6]);
                            skillData.sp = (ushort)byte.Parse(strArray[7]);
                            skillData.range = byte.Parse(strArray[9]);
                            skillData.target = byte.Parse(strArray[10]);
                            skillData.target2 = byte.Parse(strArray[11]);
                            skillData.effectRange = byte.Parse(strArray[12]);
                            skillData.castRange = (byte)ushort.Parse(strArray[17]);
                            streamWriter.WriteLine(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14}", (object)skillData.id, (object)skillData.name, (object)strArray[3], (object)skillData.maxLv, (object)skillData.lv, (object)1, (object)skillData.mp, (object)skillData.sp, (object)10, (object)10, (object)skillData.range, (object)skillData.target, (object)skillData.target2, (object)skillData.effectRange, (object)skillData.castRange));
                            if ((int)((double)streamReader.BaseStream.Position / (double)streamReader.BaseStream.Length * 100.0) % 3 == 0)
                            {
                                if (flag)
                                {
                                    Console.Write("*");
                                    flag = false;
                                }
                            }
                            else
                                flag = true;
                            ++num;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError("Error on parsing skill db!\r\nat line:" + str);
                    Logger.ShowError(ex);
                }
            }
            Console.WriteLine();
            Console.ResetColor();
            Logger.ShowInfo(num.ToString() + " skills loaded.");
            streamWriter.Flush();
            streamWriter.Close();
            streamReader.Close();
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="encoding">The encoding<see cref="Encoding"/>.</param>
        public void Init(string path, Encoding encoding)
        {
            StreamReader streamReader = new StreamReader(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path), encoding);
            string label = "Loading skill database";
            Logger.ProgressBarShow(0U, (uint)streamReader.BaseStream.Length, label);
            DateTime now = DateTime.Now;
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
                                if (strArray[index] == "")
                                    strArray[index] = "0";
                            }
                            SkillData skillData = new SkillData();
                            skillData.id = uint.Parse(strArray[0]);
                            skillData.name = strArray[1];
                            skillData.active = strArray[2] == "1";
                            skillData.maxLv = byte.Parse(strArray[3]);
                            skillData.lv = byte.Parse(strArray[4]);
                            skillData.mp = (ushort)byte.Parse(strArray[6]);
                            skillData.sp = (ushort)byte.Parse(strArray[7]);
                            skillData.castTime = int.Parse(strArray[8]);
                            skillData.delay = int.Parse(strArray[9]);
                            skillData.range = byte.Parse(strArray[10]);
                            skillData.target = byte.Parse(strArray[11]);
                            skillData.target2 = byte.Parse(strArray[12]);
                            skillData.effectRange = byte.Parse(strArray[13]);
                            skillData.castRange = (byte)ushort.Parse(strArray[14]);
                            skillData.flag.Value = (int)Conversions.HexStr2uint(strArray[16].Replace("0x", ""))[0];
                            skillData.equipFlag.Value = (int)Conversions.HexStr2uint(strArray[15].Replace("0x", ""))[0];
                            if (!this.items.ContainsKey(skillData.id))
                                this.items.Add(skillData.id, new Dictionary<byte, SkillData>());
                            this.items[skillData.id].Add(skillData.lv, skillData);
                            if ((DateTime.Now - now).TotalMilliseconds > 10.0)
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
                    Logger.ShowError("Error on parsing skill db!\r\nat line:" + str);
                    Logger.ShowError(ex);
                }
            }
            Logger.ProgressBarHide(num.ToString() + " skills loaded.");
            streamReader.Close();
        }
    }
}
