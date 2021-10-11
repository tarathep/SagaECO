namespace SagaMap.Manager
{
    using SagaDB.Actor;
    using SagaDB.Map;
    using SagaLib;
    using SagaLib.VirtualFileSystem;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Network.Client;
    using SagaMap.PC;
    using SagaMap.Scripting;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="ExperienceManager" />.
    /// </summary>
    public sealed class ExperienceManager : Singleton<ExperienceManager>
    {
        /// <summary>
        /// Defines the clTable.
        /// </summary>
        private short[,] clTable = new short[16, 2]
    {
      {
        (short) 0,
        (short) 1
      },
      {
        (short) 82,
        (short) 3
      },
      {
        (short) 96,
        (short) 4
      },
      {
        (short) 124,
        (short) 5
      },
      {
        (short) 152,
        (short) 6
      },
      {
        (short) 163,
        (short) 11
      },
      {
        (short) 177,
        (short) 12
      },
      {
        (short) 192,
        (short) 13
      },
      {
        (short) 207,
        (short) 14
      },
      {
        (short) 223,
        (short) 15
      },
      {
        (short) 238,
        (short) 16
      },
      {
        (short) 243,
        (short) 23
      },
      {
        (short) 248,
        (short) 24
      },
      {
        (short) 258,
        (short) 25
      },
      {
        (short) 269,
        (short) 26
      },
      {
        (short) 30000,
        (short) 26
      }
    };

        /// <summary>
        /// Defines the clTableDom.
        /// </summary>
        private short[,] clTableDom = new short[6, 2]
    {
      {
        (short) 0,
        (short) 1
      },
      {
        (short) 82,
        (short) 2
      },
      {
        (short) 135,
        (short) 3
      },
      {
        (short) 163,
        (short) 5
      },
      {
        (short) 205,
        (short) 6
      },
      {
        (short) 30000,
        (short) 6
      }
    };

        /// <summary>
        /// Defines the Chart.
        /// </summary>
        private Dictionary<uint, ExperienceManager.Level> Chart = new Dictionary<uint, ExperienceManager.Level>();

        /// <summary>
        /// Defines the MaxCLevel.
        /// </summary>
        public readonly uint MaxCLevel = 113;

        /// <summary>
        /// Defines the MaxJLevel.
        /// </summary>
        public readonly uint MaxJLevel = 50;

        /// <summary>
        /// Defines the currentMax.
        /// </summary>
        private uint currentMax;

        /// <summary>
        /// The LoadTable.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void LoadTable(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(path));
                foreach (object childNode in xmlDocument["exp"].ChildNodes)
                {
                    if (childNode.GetType() == typeof(XmlElement))
                    {
                        XmlElement xmlElement1 = (XmlElement)childNode;
                        switch (xmlElement1.Name.ToLower())
                        {
                            case "level":
                                XmlNodeList childNodes = xmlElement1.ChildNodes;
                                uint cxp = 0;
                                uint jxp = 0;
                                uint jxp2 = 0;
                                byte num = 0;
                                foreach (object obj in childNodes)
                                {
                                    if (obj.GetType() == typeof(XmlElement))
                                    {
                                        XmlElement xmlElement2 = (XmlElement)obj;
                                        switch (xmlElement2.Name.ToLower())
                                        {
                                            case "lv":
                                                num = byte.Parse(xmlElement2.InnerText);
                                                break;
                                            case "c":
                                                cxp = uint.Parse(xmlElement2.InnerText);
                                                break;
                                            case "j1":
                                                jxp = uint.Parse(xmlElement2.InnerText);
                                                break;
                                            case "j2":
                                                jxp2 = uint.Parse(xmlElement2.InnerText);
                                                break;
                                        }
                                    }
                                }
                                ExperienceManager.Level level = new ExperienceManager.Level(cxp, jxp, jxp2);
                                this.Chart.Add((uint)num, level);
                                break;
                        }
                    }
                }
                Logger.ShowInfo("EXP table loaded");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The checkExpGap.
        /// </summary>
        /// <param name="ori">The ori<see cref="uint"/>.</param>
        /// <param name="add">The add<see cref="uint"/>.</param>
        /// <param name="maxLv">The maxLv<see cref="byte"/>.</param>
        /// <param name="type">The type<see cref="LevelType"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        private uint checkExpGap(uint ori, uint add, byte maxLv, LevelType type)
        {
            uint num = ori + add;
            if (num >= this.GetExpForLevel((uint)maxLv + 1U, type))
                num = this.GetExpForLevel((uint)maxLv + 1U, type) - 1U;
            return num;
        }

        /// <summary>
        /// The checkCL.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void checkCL(ActorPC pc)
        {
            if (pc.Race != PC_RACE.DEM)
                return;
            if (Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.Dominion))
            {
                byte num = (byte)(((int)pc.DominionCL - 9) / 4 + 1);
                if ((int)num > (int)pc.DominionLevel)
                    this.SendLevelUp(MapClient.FromActorPC(pc), LevelType.CLEVEL, (uint)num - (uint)pc.DominionLevel);
            }
            else
            {
                byte num = (byte)(((int)pc.CL - 9) / 4 + 1);
                if ((int)num > (int)pc.Level)
                    this.SendLevelUp(MapClient.FromActorPC(pc), LevelType.CLEVEL, (uint)num - (uint)pc.Level);
            }
        }

        /// <summary>
        /// The GetEPRequired.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="short"/>.</returns>
        public short GetEPRequired(ActorPC pc)
        {
            if (pc.Race != PC_RACE.DEM)
                return 0;
            if (Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.Dominion))
                return this.GetEPRequired(pc.DominionCL, true);
            return this.GetEPRequired(pc.CL, false);
        }

        /// <summary>
        /// The GetEPRequired.
        /// </summary>
        /// <param name="cl">The cl<see cref="short"/>.</param>
        /// <param name="dominion">The dominion<see cref="bool"/>.</param>
        /// <returns>The <see cref="short"/>.</returns>
        public short GetEPRequired(short cl, bool dominion)
        {
            if (dominion)
            {
                for (int index = 0; index < this.clTableDom.Length; ++index)
                {
                    if ((int)cl < (int)this.clTableDom[index, 0])
                        return this.clTableDom[index - 1, 1];
                }
            }
            else
            {
                for (int index = 0; index < this.clTableDom.Length; ++index)
                {
                    if ((int)cl < (int)this.clTable[index, 0])
                        return this.clTable[index - 1, 1];
                }
            }
            return 0;
        }

        /// <summary>
        /// The ApplyEP.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="count">The count<see cref="short"/>.</param>
        public void ApplyEP(ActorPC pc, short count)
        {
            bool dominion = Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.Dominion);
            short count1 = 0;
            while (count > (short)0)
            {
                if (dominion)
                {
                    short epRequired = this.GetEPRequired((short)((int)pc.DominionCL + (int)count1), dominion);
                    if ((int)pc.DominionEPUsed + (int)count >= (int)epRequired)
                    {
                        ++count1;
                        count -= (short)((int)epRequired - (int)pc.DominionEPUsed);
                        pc.DominionEPUsed = (short)0;
                    }
                    else
                    {
                        pc.DominionEPUsed += count;
                        count = (short)0;
                    }
                }
                else
                {
                    short epRequired = this.GetEPRequired((short)((int)pc.CL + (int)count1), dominion);
                    if ((int)pc.EPUsed + (int)count >= (int)epRequired)
                    {
                        ++count1;
                        count -= (short)((int)epRequired - (int)pc.EPUsed);
                        pc.EPUsed = (short)0;
                    }
                    else
                    {
                        pc.EPUsed += count;
                        count = (short)0;
                    }
                }
            }
            if (count1 <= (short)0)
                return;
            this.ApplyCL(pc, count1);
        }

        /// <summary>
        /// The ApplyCL.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="count">The count<see cref="short"/>.</param>
        public void ApplyCL(ActorPC pc, short count)
        {
            if (pc.Race != PC_RACE.DEM)
                return;
            if (Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.Dominion))
                pc.DominionCL += count;
            else
                pc.CL += count;
            this.checkCL(pc);
        }

        /// <summary>
        /// Apply input percentage of experience from input targetNPC to input targetPC.
        /// The percentage gets capped at 1f and are multiplied by global EXP rate(s).
        /// </summary>
        /// <param name="targetPC">The target PC (the player).</param>
        /// <param name="exp">The exp<see cref="uint"/>.</param>
        /// <param name="jexp">The jexp<see cref="uint"/>.</param>
        /// <param name="percentage">The percentage of the NPC's exp to gain (for instance: the percentage of HP deducted by input player).</param>
        public void ApplyExp(ActorPC targetPC, uint exp, uint jexp, float percentage)
        {
            percentage *= (float)Singleton<Configuration>.Instance.QuestRate / 100f;
            ((PCEventHandler)targetPC.e).Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.GET_EXP, (object)(uint)((double)exp * (double)percentage), (object)(uint)((double)jexp * (double)percentage)));
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(targetPC.MapID);
            if (targetPC.JobJoint == PC_JOB.NONE)
            {
                if (map.Info.Flag.Test(MapFlags.Dominion))
                {
                    targetPC.DominionCEXP += (uint)((double)exp * (double)percentage);
                    if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        if (targetPC.DominionCEXP >= this.GetExpForLevel(51U, LevelType.CLEVEL))
                            targetPC.DominionCEXP = this.GetExpForLevel(51U, LevelType.CLEVEL) - 1U;
                        targetPC.DominionJEXP += (uint)((double)jexp * (double)percentage);
                        if (targetPC.DominionJEXP >= this.GetExpForLevel(51U, LevelType.JLEVEL2))
                            targetPC.DominionJEXP = this.GetExpForLevel(51U, LevelType.JLEVEL2) - 1U;
                    }
                    else
                    {
                        if (targetPC.DominionCEXP >= this.GetExpForLevel(31U, LevelType.CLEVEL))
                            targetPC.DominionCEXP = this.GetExpForLevel(31U, LevelType.CLEVEL) - 1U;
                        targetPC.DominionJEXP += (uint)((double)jexp * (double)percentage);
                        if (targetPC.DominionJEXP >= this.GetExpForLevel(31U, LevelType.JLEVEL2))
                            targetPC.DominionJEXP = this.GetExpForLevel(31U, LevelType.JLEVEL2) - 1U;
                    }
                }
                else
                {
                    if (targetPC.CEXP < this.GetExpForLevel(this.MaxCLevel, LevelType.CLEVEL))
                        targetPC.CEXP += (uint)((double)exp * (double)percentage);
                    if (targetPC.Job == targetPC.JobBasic)
                    {
                        if (targetPC.JEXP < this.GetExpForLevel(this.MaxJLevel, LevelType.JLEVEL))
                            targetPC.JEXP += (uint)((double)jexp * (double)percentage);
                    }
                    else if (targetPC.JEXP < this.GetExpForLevel(this.MaxJLevel, LevelType.JLEVEL2))
                        targetPC.JEXP += (uint)((double)jexp * (double)percentage);
                }
            }
            else
            {
                if (map.Info.Flag.Test(MapFlags.Dominion))
                {
                    targetPC.DominionCEXP += (uint)((double)exp * (double)percentage);
                    if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        if (targetPC.DominionCEXP >= this.GetExpForLevel(51U, LevelType.CLEVEL))
                            targetPC.DominionCEXP = this.GetExpForLevel(51U, LevelType.CLEVEL) - 1U;
                    }
                    else if (targetPC.DominionCEXP >= this.GetExpForLevel(31U, LevelType.CLEVEL))
                        targetPC.DominionCEXP = this.GetExpForLevel(31U, LevelType.CLEVEL) - 1U;
                }
                else if (targetPC.CEXP < this.GetExpForLevel(this.MaxCLevel, LevelType.CLEVEL))
                    targetPC.CEXP += (uint)((double)exp * (double)percentage);
                targetPC.JointJEXP += (uint)((double)jexp * (double)percentage);
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                {
                    if (targetPC.JointJEXP >= this.GetExpForLevel(51U, LevelType.JLEVEL2))
                        targetPC.JointJEXP = this.GetExpForLevel(51U, LevelType.JLEVEL2) - 1U;
                }
                else if (targetPC.JointJEXP >= this.GetExpForLevel(31U, LevelType.JLEVEL2))
                    targetPC.JointJEXP = this.GetExpForLevel(31U, LevelType.JLEVEL2) - 1U;
            }
            if (targetPC.Race == PC_RACE.DEM)
                return;
            PCEventHandler e = (PCEventHandler)targetPC.e;
            this.CheckExp(e.Client, LevelType.CLEVEL);
            if (targetPC.JobJoint == PC_JOB.NONE)
            {
                if (map.Info.Flag.Test(MapFlags.Dominion))
                    this.CheckExp(e.Client, LevelType.JLEVEL2);
                else if (targetPC.Job == targetPC.JobBasic)
                    this.CheckExp(e.Client, LevelType.JLEVEL);
                else
                    this.CheckExp(e.Client, LevelType.JLEVEL2);
            }
            else
                this.CheckExp(e.Client, LevelType.JLEVEL2);
        }

        /// <summary>
        /// Apply input percentage of experience from input targetNPC to input targetPC.
        /// The percentage gets capped at 1f and are multiplied by global EXP rate(s).
        /// </summary>
        /// <param name="targetPC">The target PC (the player).</param>
        /// <param name="targetNPC">The target NPC (the "mob").</param>
        /// <param name="percentage">The percentage of the NPC's exp to gain (for instance: the percentage of HP deducted by input player).</param>
        public void ApplyExp(ActorPC targetPC, ActorMob targetNPC, float percentage)
        {
            percentage *= (float)Singleton<Configuration>.Instance.EXPRate / 100f;
            PCEventHandler e1 = (PCEventHandler)targetPC.e;
            uint num1 = (uint)((double)targetNPC.BaseData.baseExp * (double)percentage);
            uint num2 = (uint)((double)targetNPC.BaseData.jobExp * (double)percentage);
            if (num1 != 0U || num2 != 0U)
                e1.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.GET_EXP, (object)num1, (object)num2));
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(targetPC.MapID);
            foreach (ActorPC possesionedActor in targetPC.PossesionedActors)
            {
                PCEventHandler e2 = (PCEventHandler)possesionedActor.e;
                if (possesionedActor != targetPC)
                {
                    if (possesionedActor.JobJoint == PC_JOB.NONE)
                    {
                        if (map.Info.Flag.Test(MapFlags.Dominion))
                        {
                            possesionedActor.DominionCEXP += (uint)((double)targetNPC.BaseData.baseExp * (double)percentage * 0.100000001490116);
                            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                            {
                                if (possesionedActor.DominionCEXP >= this.GetExpForLevel(51U, LevelType.CLEVEL))
                                    possesionedActor.DominionCEXP = this.GetExpForLevel(51U, LevelType.CLEVEL) - 1U;
                                targetPC.DominionJEXP += (uint)((double)targetNPC.BaseData.jobExp * (double)percentage * 0.100000001490116);
                                if (possesionedActor.DominionJEXP >= this.GetExpForLevel(50U, LevelType.JLEVEL2))
                                    possesionedActor.DominionJEXP = this.GetExpForLevel(50U, LevelType.JLEVEL2) - 1U;
                            }
                            else
                            {
                                if (possesionedActor.DominionCEXP >= this.GetExpForLevel(31U, LevelType.CLEVEL))
                                    possesionedActor.DominionCEXP = this.GetExpForLevel(31U, LevelType.CLEVEL) - 1U;
                                targetPC.DominionJEXP += (uint)((double)targetNPC.BaseData.jobExp * (double)percentage * 0.100000001490116);
                                if (possesionedActor.DominionJEXP >= this.GetExpForLevel(31U, LevelType.JLEVEL2))
                                    possesionedActor.DominionJEXP = this.GetExpForLevel(31U, LevelType.JLEVEL2) - 1U;
                            }
                        }
                        else
                        {
                            if (possesionedActor.CEXP < this.GetExpForLevel(this.MaxCLevel, LevelType.CLEVEL))
                                possesionedActor.CEXP += (uint)((double)targetNPC.BaseData.baseExp * (double)percentage * 0.100000001490116);
                            if (possesionedActor.Job == possesionedActor.JobBasic)
                            {
                                if (possesionedActor.JEXP < this.GetExpForLevel(this.MaxJLevel, LevelType.JLEVEL))
                                    possesionedActor.JEXP += (uint)((double)targetNPC.BaseData.jobExp * (double)percentage * 0.100000001490116);
                            }
                            else if (possesionedActor.JEXP < this.GetExpForLevel(this.MaxJLevel, LevelType.JLEVEL2))
                                possesionedActor.JEXP += (uint)((double)targetNPC.BaseData.jobExp * (double)percentage * 0.100000001490116);
                        }
                    }
                    else
                    {
                        if (map.Info.Flag.Test(MapFlags.Dominion))
                        {
                            possesionedActor.DominionCEXP += (uint)((double)targetNPC.BaseData.baseExp * (double)percentage * 0.100000001490116);
                            if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                            {
                                if (possesionedActor.DominionCEXP >= this.GetExpForLevel(51U, LevelType.CLEVEL))
                                    possesionedActor.DominionCEXP = this.GetExpForLevel(51U, LevelType.CLEVEL) - 1U;
                            }
                            else if (possesionedActor.DominionCEXP >= this.GetExpForLevel(31U, LevelType.CLEVEL))
                                possesionedActor.DominionCEXP = this.GetExpForLevel(31U, LevelType.CLEVEL) - 1U;
                        }
                        else if (possesionedActor.CEXP < this.GetExpForLevel(this.MaxCLevel, LevelType.CLEVEL))
                            possesionedActor.CEXP += (uint)((double)targetNPC.BaseData.baseExp * (double)percentage * 0.100000001490116);
                        possesionedActor.JointJEXP += (uint)((double)targetNPC.BaseData.jobExp * (double)percentage * 0.100000001490116);
                        if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                        {
                            if (possesionedActor.JointJEXP >= this.GetExpForLevel(51U, LevelType.JLEVEL2))
                                possesionedActor.JointJEXP = this.GetExpForLevel(51U, LevelType.JLEVEL2) - 1U;
                        }
                        else if (possesionedActor.JointJEXP >= this.GetExpForLevel(31U, LevelType.JLEVEL2))
                            possesionedActor.JointJEXP = this.GetExpForLevel(31U, LevelType.JLEVEL2) - 1U;
                    }
                    if (targetPC.Race != PC_RACE.DEM)
                    {
                        this.CheckExp(e2.Client, LevelType.CLEVEL);
                        if (possesionedActor.JobJoint == PC_JOB.NONE)
                        {
                            if (map.Info.Flag.Test(MapFlags.Dominion))
                                this.CheckExp(e2.Client, LevelType.JLEVEL2);
                            else if (possesionedActor.Job == possesionedActor.JobBasic)
                                this.CheckExp(e2.Client, LevelType.JLEVEL);
                            else
                                this.CheckExp(e2.Client, LevelType.JLEVEL2);
                        }
                        else
                            this.CheckExp(e2.Client, LevelType.JLEVEL2);
                    }
                    if (e2.Client.state != MapClient.SESSION_STATE.DISCONNECTED)
                    {
                        e2.Client.SendEXP();
                        if (targetNPC.BaseData.baseExp != 0U || targetNPC.BaseData.jobExp != 0U)
                            e2.Client.SendSystemMessage(string.Format(Singleton<LocalManager>.Instance.Strings.POSSESSION_EXP, (object)(uint)((double)targetNPC.BaseData.baseExp * (double)percentage * 0.100000001490116), (object)(uint)((double)targetNPC.BaseData.jobExp * (double)percentage * 0.100000001490116)));
                    }
                }
            }
            if (targetPC.JobJoint == PC_JOB.NONE)
            {
                if (map.Info.Flag.Test(MapFlags.Dominion))
                {
                    targetPC.DominionCEXP += num1;
                    if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        if (targetPC.DominionCEXP >= this.GetExpForLevel(51U, LevelType.CLEVEL))
                            targetPC.DominionCEXP = this.GetExpForLevel(51U, LevelType.CLEVEL) - 1U;
                        targetPC.DominionJEXP += num2;
                        if (targetPC.DominionJEXP >= this.GetExpForLevel(50U, LevelType.JLEVEL2))
                            targetPC.DominionJEXP = this.GetExpForLevel(50U, LevelType.JLEVEL2) - 1U;
                    }
                    else
                    {
                        if (targetPC.DominionCEXP >= this.GetExpForLevel(31U, LevelType.CLEVEL))
                            targetPC.DominionCEXP = this.GetExpForLevel(31U, LevelType.CLEVEL) - 1U;
                        targetPC.DominionJEXP += num2;
                        if (targetPC.DominionJEXP >= this.GetExpForLevel(31U, LevelType.JLEVEL2))
                            targetPC.DominionJEXP = this.GetExpForLevel(31U, LevelType.JLEVEL2) - 1U;
                    }
                }
                else
                {
                    if (targetPC.CEXP < this.GetExpForLevel(this.MaxCLevel, LevelType.CLEVEL))
                        targetPC.CEXP += num1;
                    if (targetPC.Job == targetPC.JobBasic)
                    {
                        if (targetPC.JEXP < this.GetExpForLevel(this.MaxJLevel, LevelType.JLEVEL))
                            targetPC.JEXP += num2;
                    }
                    else if (targetPC.JEXP < this.GetExpForLevel(this.MaxJLevel, LevelType.JLEVEL2))
                        targetPC.JEXP += num2;
                }
            }
            else
            {
                if (map.Info.Flag.Test(MapFlags.Dominion))
                {
                    targetPC.DominionCEXP += num1;
                    if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                    {
                        if (targetPC.DominionCEXP >= this.GetExpForLevel(51U, LevelType.CLEVEL))
                            targetPC.DominionCEXP = this.GetExpForLevel(51U, LevelType.CLEVEL) - 1U;
                    }
                    else if (targetPC.DominionCEXP >= this.GetExpForLevel(31U, LevelType.CLEVEL))
                        targetPC.DominionCEXP = this.GetExpForLevel(31U, LevelType.CLEVEL) - 1U;
                }
                else if (targetPC.CEXP < this.GetExpForLevel(this.MaxCLevel, LevelType.CLEVEL))
                    targetPC.CEXP += num1;
                targetPC.JointJEXP += num2;
                if (Singleton<Configuration>.Instance.Version >= SagaLib.Version.Saga10)
                {
                    if (targetPC.JointJEXP >= this.GetExpForLevel(51U, LevelType.JLEVEL2))
                        targetPC.JointJEXP = this.GetExpForLevel(51U, LevelType.JLEVEL2) - 1U;
                }
                else if (targetPC.JointJEXP >= this.GetExpForLevel(31U, LevelType.JLEVEL2))
                    targetPC.JointJEXP = this.GetExpForLevel(31U, LevelType.JLEVEL2) - 1U;
            }
            PCEventHandler e3 = (PCEventHandler)targetPC.e;
            if (e3.Client.state != MapClient.SESSION_STATE.DISCONNECTED)
                e3.Client.SendEXP();
            if (targetPC.Race == PC_RACE.DEM)
                return;
            this.CheckExp(e3.Client, LevelType.CLEVEL);
            if (targetPC.JobJoint == PC_JOB.NONE)
            {
                if (map.Info.Flag.Test(MapFlags.Dominion))
                    this.CheckExp(e3.Client, LevelType.JLEVEL2);
                else if (targetPC.Job == targetPC.JobBasic)
                    this.CheckExp(e3.Client, LevelType.JLEVEL);
                else
                    this.CheckExp(e3.Client, LevelType.JLEVEL2);
            }
            else
                this.CheckExp(e3.Client, LevelType.JLEVEL2);
        }

        /// <summary>
        /// Check whether input clients experience at the input level type has reached beyond it's current level or not.
        /// If it has, process the new level (update database and inform client), if not, proceed as nothing happened.
        /// </summary>
        /// <param name="client">.</param>
        /// <param name="type">.</param>
        public void CheckExp(MapClient client, LevelType type)
        {
            switch (type)
            {
                case LevelType.CLEVEL:
                    uint numLevels = !client.map.Info.Flag.Test(MapFlags.Dominion) ? this.GetLevelDelta((uint)client.Character.Level, client.Character.CEXP, LevelType.CLEVEL, true) : this.GetLevelDelta((uint)client.Character.DominionLevel, client.Character.DominionCEXP, LevelType.CLEVEL, true);
                    if (numLevels <= 0U)
                        break;
                    this.SendLevelUp(client, type, numLevels);
                    break;
                case LevelType.JLEVEL:
                    uint levelDelta1 = this.GetLevelDelta((uint)client.Character.JobLevel1, client.Character.JEXP, LevelType.JLEVEL, true);
                    if (levelDelta1 <= 0U)
                        break;
                    this.SendLevelUp(client, type, levelDelta1);
                    break;
                case LevelType.JLEVEL2:
                    if (client.map.Info.Flag.Test(MapFlags.Dominion))
                    {
                        uint levelDelta2 = this.GetLevelDelta((uint)client.Character.DominionJobLevel, client.Character.DominionJEXP, LevelType.JLEVEL2, true);
                        if (levelDelta2 <= 0U)
                            break;
                        this.SendLevelUp(client, LevelType.JLEVEL2, levelDelta2);
                        break;
                    }
                    if (client.Character.JobJoint == PC_JOB.NONE)
                    {
                        if (client.Character.Job == client.Character.Job2X)
                        {
                            uint levelDelta2 = this.GetLevelDelta((uint)client.Character.JobLevel2X, client.Character.JEXP, LevelType.JLEVEL2, true);
                            if (levelDelta2 > 0U)
                                this.SendLevelUp(client, type, levelDelta2);
                        }
                        else
                        {
                            uint levelDelta2 = this.GetLevelDelta((uint)client.Character.JobLevel2T, client.Character.JEXP, LevelType.JLEVEL2, true);
                            if (levelDelta2 > 0U)
                                this.SendLevelUp(client, LevelType.JLEVEL2T, levelDelta2);
                        }
                    }
                    else
                    {
                        uint levelDelta2 = this.GetLevelDelta((uint)client.Character.JointJobLevel, client.Character.JointJEXP, LevelType.JLEVEL2, true);
                        if (levelDelta2 > 0U)
                            this.SendLevelUp(client, type, levelDelta2);
                    }
                    break;
            }
        }

        /// <summary>
        /// The GetExpForLevel.
        /// </summary>
        /// <param name="level">The level to get the experience for.</param>
        /// <param name="type">The level type to get the experience for.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetExpForLevel(uint level, LevelType type)
        {
            if (!this.Chart.ContainsKey(level))
                return uint.MaxValue;
            ExperienceManager.Level level1 = this.Chart[level];
            switch (type)
            {
                case LevelType.CLEVEL:
                    return level1.cxp;
                case LevelType.JLEVEL:
                    return level1.jxp;
                case LevelType.JLEVEL2:
                case LevelType.JLEVEL2T:
                    return level1.jxp2;
                default:
                    return uint.MaxValue;
            }
        }

        /// <summary>
        /// The CalcLevelDiffReduction.
        /// </summary>
        /// <param name="lvDelta">The lvDelta<see cref="int"/>.</param>
        /// <returns>The <see cref="float"/>.</returns>
        private float CalcLevelDiffReduction(int lvDelta)
        {
            if (lvDelta >= 0 && lvDelta < 26)
                return 1f;
            if (lvDelta >= 26 && lvDelta < 31)
                return 0.75f;
            if (lvDelta >= 31 && lvDelta < 36)
                return 0.5f;
            if (lvDelta >= 26 && lvDelta < 41)
                return 0.25f;
            return lvDelta >= 41 ? 0.1f : 1f;
        }

        /// <summary>
        /// The ProcessMobExp.
        /// </summary>
        /// <param name="mob">怪物.</param>
        public void ProcessMobExp(ActorMob mob)
        {
            MobEventHandler e = (MobEventHandler)mob.e;
            Dictionary<uint, int> dictionary1 = new Dictionary<uint, int>();
            uint maxHp = mob.MaxHP;
            foreach (uint key in e.AI.DamageTable.Keys)
            {
                SagaDB.Actor.Actor actor = e.AI.map.GetActor(key);
                if (actor != null && actor.type == ActorType.PC)
                {
                    ActorPC targetPC = (ActorPC)actor;
                    if (targetPC.Party == null)
                    {
                        int lvDelta = Math.Abs((int)targetPC.Level - (int)mob.Level);
                        if (!targetPC.Buff.Dead)
                            this.ApplyExp(targetPC, mob, (float)e.AI.DamageTable[key] / (float)maxHp * this.CalcLevelDiffReduction(lvDelta));
                    }
                    else
                    {
                        if (dictionary1.ContainsKey(targetPC.Party.ID))
                        {
                            Dictionary<uint, int> dictionary2;
                            uint id;
                            (dictionary2 = dictionary1)[id = targetPC.Party.ID] = dictionary2[id] + e.AI.DamageTable[key];
                        }
                        else
                            dictionary1.Add(targetPC.Party.ID, e.AI.DamageTable[key]);
                        if ((long)dictionary1[targetPC.Party.ID] > (long)mob.MaxHP)
                            dictionary1[targetPC.Party.ID] = (int)mob.MaxHP;
                    }
                }
            }
            foreach (uint key in dictionary1.Keys)
            {
                SagaDB.Party.Party party = Singleton<PartyManager>.Instance.GetParty(key);
                List<ActorPC> actorPcList = new List<ActorPC>();
                foreach (ActorPC actorPc in party.Members.Values)
                {
                    if (actorPc.Online && !actorPc.Buff.Dead && (int)actorPc.MapID == (int)mob.MapID && (Math.Abs((int)mob.X - (int)actorPc.X) <= 1200 && Math.Abs((int)mob.Y - (int)actorPc.Y) <= 1200))
                        actorPcList.Add(actorPc);
                }
                if (actorPcList.Count != 0)
                {
                    float num1 = (actorPcList.Count <= 1 ? 1f : (float)(0.899999976158142 + 0.200000002980232 * (double)actorPcList.Count)) * ((float)dictionary1[key] / (float)maxHp);
                    int num2 = 0;
                    foreach (ActorPC actorPc in actorPcList)
                    {
                        if (e.AI.map.Info.Flag.Test(MapFlags.Dominion))
                            num2 += (int)actorPc.DominionLevel;
                        else
                            num2 += (int)actorPc.Level;
                    }
                    int num3 = num2 / actorPcList.Count;
                    foreach (ActorPC targetPC in actorPcList)
                    {
                        float num4 = this.CalcLevelDiffReduction(Math.Abs(num3 - (int)mob.Level));
                        if (e.AI.map.Info.Flag.Test(MapFlags.Dominion))
                            this.ApplyExp(targetPC, mob, (float)((double)num4 * (double)num1 * ((double)targetPC.DominionLevel / (double)num2)));
                        else
                            this.ApplyExp(targetPC, mob, (float)((double)num4 * (double)num1 * ((double)targetPC.Level / (double)num2)));
                    }
                }
            }
        }

        /// <summary>
        /// The DeathPenalty.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public void DeathPenalty(ActorPC pc)
        {
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(pc.MapID);
            MapClient mapClient = MapClient.FromActorPC(pc);
            if (map.Info.Flag.Test(MapFlags.Dominion))
            {
                if (pc.Race == PC_RACE.DEM)
                {
                    pc.DominionCEXP -= (uint)((double)pc.DominionCEXP * (double)Singleton<Configuration>.Instance.DeathPenaltyBaseDominion);
                    pc.DominionJEXP -= (uint)((double)pc.DominionJEXP * (double)Singleton<Configuration>.Instance.DeathPenaltyJobDominion);
                    return;
                }
                uint num1 = (uint)((double)(this.GetExpForLevel((uint)pc.DominionLevel + 1U, LevelType.CLEVEL) - this.GetExpForLevel((uint)pc.DominionLevel, LevelType.CLEVEL)) * (double)Singleton<Configuration>.Instance.DeathPenaltyBaseDominion);
                uint num2 = (uint)((double)(this.GetExpForLevel((uint)pc.DominionJobLevel + 1U, LevelType.JLEVEL2) - this.GetExpForLevel((uint)pc.DominionJobLevel, LevelType.JLEVEL2)) * (double)Singleton<Configuration>.Instance.DeathPenaltyJobDominion);
                if (pc.DominionCEXP > num1)
                    pc.DominionCEXP -= num1;
                else
                    pc.DominionCEXP = 0U;
                if (pc.DominionJEXP > num2)
                    pc.DominionJEXP -= num2;
                else
                    pc.DominionJEXP = 0U;
                if (pc.DominionCEXP < this.GetExpForLevel((uint)pc.DominionLevel, LevelType.CLEVEL))
                {
                    int num3 = (int)pc.DominionLevel / 3 + 3;
                    while ((int)pc.DominionStatsPoint < num3)
                    {
                        List<int> intList = new List<int>();
                        if ((int)pc.Str > (int)Singleton<Configuration>.Instance.StartupSetting[pc.Race].Str)
                            intList.Add(0);
                        if ((int)pc.Dex > (int)Singleton<Configuration>.Instance.StartupSetting[pc.Race].Dex)
                            intList.Add(1);
                        if ((int)pc.Int > (int)Singleton<Configuration>.Instance.StartupSetting[pc.Race].Int)
                            intList.Add(2);
                        if ((int)pc.Vit > (int)Singleton<Configuration>.Instance.StartupSetting[pc.Race].Vit)
                            intList.Add(3);
                        if ((int)pc.Agi > (int)Singleton<Configuration>.Instance.StartupSetting[pc.Race].Agi)
                            intList.Add(4);
                        if ((int)pc.Mag > (int)Singleton<Configuration>.Instance.StartupSetting[pc.Race].Mag)
                            intList.Add(5);
                        if (intList.Count == 0)
                        {
                            num3 = 0;
                            break;
                        }
                        switch (intList[Global.Random.Next(0, intList.Count - 1)])
                        {
                            case 0:
                                --pc.Str;
                                pc.DominionStatsPoint += Singleton<StatusFactory>.Instance.RequiredBonusPoint(pc.Str);
                                break;
                            case 1:
                                --pc.Dex;
                                pc.DominionStatsPoint += Singleton<StatusFactory>.Instance.RequiredBonusPoint(pc.Dex);
                                break;
                            case 2:
                                --pc.Int;
                                pc.DominionStatsPoint += Singleton<StatusFactory>.Instance.RequiredBonusPoint(pc.Int);
                                break;
                            case 3:
                                --pc.Vit;
                                pc.DominionStatsPoint += Singleton<StatusFactory>.Instance.RequiredBonusPoint(pc.Vit);
                                break;
                            case 4:
                                --pc.Agi;
                                pc.DominionStatsPoint += Singleton<StatusFactory>.Instance.RequiredBonusPoint(pc.Agi);
                                break;
                            case 5:
                                --pc.Mag;
                                pc.DominionStatsPoint += Singleton<StatusFactory>.Instance.RequiredBonusPoint(pc.Mag);
                                break;
                        }
                    }
                    pc.DominionStatsPoint -= (ushort)num3;
                    --pc.DominionLevel;
                }
                if (pc.DominionJEXP < this.GetExpForLevel((uint)pc.DominionJobLevel, LevelType.JLEVEL2))
                    --pc.DominionJobLevel;
                mapClient.SendEXP();
                mapClient.SendPlayerLevel();
            }
            else
            {
                if (pc.Race == PC_RACE.DEM)
                {
                    pc.CEXP -= (uint)((double)pc.CEXP * (double)Singleton<Configuration>.Instance.DeathPenaltyBaseEmil);
                    pc.JEXP -= (uint)((double)pc.JEXP * (double)Singleton<Configuration>.Instance.DeathPenaltyJobEmil);
                    return;
                }
                uint num1 = (uint)((double)(this.GetExpForLevel((uint)pc.Level + 1U, LevelType.CLEVEL) - this.GetExpForLevel((uint)pc.Level, LevelType.CLEVEL)) * (double)Singleton<Configuration>.Instance.DeathPenaltyBaseEmil);
                uint num2 = pc.Job != pc.JobBasic ? (pc.Job != pc.Job2X ? (uint)((double)(this.GetExpForLevel((uint)pc.JobLevel2T + 1U, LevelType.JLEVEL2) - this.GetExpForLevel((uint)pc.JobLevel2T, LevelType.JLEVEL2)) * (double)Singleton<Configuration>.Instance.DeathPenaltyJobEmil) : (uint)((double)(this.GetExpForLevel((uint)pc.JobLevel2X + 1U, LevelType.JLEVEL2) - this.GetExpForLevel((uint)pc.JobLevel2X, LevelType.JLEVEL2)) * (double)Singleton<Configuration>.Instance.DeathPenaltyJobEmil)) : (uint)((double)(this.GetExpForLevel((uint)pc.JobLevel1 + 1U, LevelType.JLEVEL) - this.GetExpForLevel((uint)pc.JobLevel1, LevelType.JLEVEL)) * (double)Singleton<Configuration>.Instance.DeathPenaltyJobEmil);
                if (pc.CEXP > num1)
                    pc.CEXP -= num1;
                else
                    pc.CEXP = 0U;
                if (pc.JEXP > num2)
                    pc.JEXP -= num2;
                else
                    pc.JEXP = 0U;
                if (pc.CEXP < this.GetExpForLevel((uint)pc.Level, LevelType.CLEVEL))
                    pc.CEXP = this.GetExpForLevel((uint)pc.Level, LevelType.CLEVEL);
                if (pc.Job == pc.JobBasic)
                {
                    if (pc.JEXP < this.GetExpForLevel((uint)pc.JobLevel1, LevelType.JLEVEL))
                        pc.JEXP = this.GetExpForLevel((uint)pc.JobLevel1, LevelType.JLEVEL);
                }
                else if (pc.Job == pc.Job2X)
                {
                    if (pc.JEXP < this.GetExpForLevel((uint)pc.JobLevel2X, LevelType.JLEVEL2))
                        pc.JEXP = this.GetExpForLevel((uint)pc.JobLevel2X, LevelType.JLEVEL2);
                }
                else if (pc.JEXP < this.GetExpForLevel((uint)pc.JobLevel2T, LevelType.JLEVEL2))
                    pc.JEXP = this.GetExpForLevel((uint)pc.JobLevel2T, LevelType.JLEVEL2);
                mapClient.SendEXP();
                mapClient.SendPlayerLevel();
            }
            mapClient.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.DEATH_PENALTY);
        }

        /// <summary>
        /// The ProcessWrp.
        /// </summary>
        /// <param name="src">The src<see cref="ActorPC"/>.</param>
        /// <param name="dst">The dst<see cref="ActorPC"/>.</param>
        public void ProcessWrp(ActorPC src, ActorPC dst)
        {
            int num1;
            int num2;
            if (Singleton<MapManager>.Instance.GetMap(dst.MapID).Info.Flag.Test(MapFlags.Dominion))
            {
                num1 = (int)src.DominionLevel;
                num2 = (int)dst.DominionLevel;
            }
            else
            {
                num1 = (int)src.Level;
                num2 = (int)src.Level;
            }
            int num3 = num2 <= num1 ? 5 : num2 - num1 + 10;
            if (src.Party == null)
            {
                src.WRP += num3;
            }
            else
            {
                List<ActorPC> actorPcList = new List<ActorPC>();
                foreach (ActorPC actorPc in src.Party.Members.Values)
                {
                    if (actorPc.Online && !actorPc.Buff.Dead && (int)actorPc.MapID == (int)dst.MapID && (Math.Abs((int)dst.X - (int)actorPc.X) <= 1200 && Math.Abs((int)dst.Y - (int)actorPc.Y) <= 1200))
                        actorPcList.Add(actorPc);
                }
                int num4 = num3 / actorPcList.Count;
                if (num4 == 0)
                    num4 = 1;
                foreach (ActorPC actorPc in actorPcList)
                    actorPc.WRP += num4;
            }
            int num5 = num3 / 2;
            if (dst.WRP > num5)
                dst.WRP -= num5;
            else
                dst.WRP = 0;
            Singleton<WRPRankingManager>.Instance.UpdateRanking();
        }

        /// <summary>
        /// The SendLevelUp.
        /// </summary>
        /// <param name="client">The MapClient.</param>
        /// <param name="type">The LevelType that gained level(s).</param>
        /// <param name="numLevels">The number of levels gained.</param>
        private void SendLevelUp(MapClient client, LevelType type, uint numLevels)
        {
            byte num1;
            if (type == LevelType.CLEVEL)
            {
                ushort num2 = 0;
                if (client.map.Info.Flag.Test(MapFlags.Dominion))
                {
                    if (client.Character.Race != PC_RACE.DEM)
                    {
                        for (int index = 0; (long)index < (long)numLevels; ++index)
                            num2 += (ushort)(((int)client.Character.DominionLevel + index) / 3 + 3);
                        client.Character.DominionStatsPoint += num2;
                    }
                    client.Character.DominionLevel += (byte)numLevels;
                }
                else
                {
                    if (client.Character.Race != PC_RACE.DEM)
                    {
                        for (int index = 0; (long)index < (long)numLevels; ++index)
                            num2 += (ushort)(((int)client.Character.Level + index) / 3 + 3);
                        client.Character.StatsPoint += num2;
                    }
                    client.Character.Level += (byte)(uint)(byte)numLevels;
                }
                num1 = (byte)1;
            }
            else
            {
                if (client.Character.Race != PC_RACE.DEM)
                {
                    if (client.map.Info.Flag.Test(MapFlags.Dominion))
                        client.Character.DominionJobLevel += (byte)numLevels;
                    else if (client.Character.JobJoint == PC_JOB.NONE)
                    {
                        switch (type)
                        {
                            case LevelType.JLEVEL:
                                client.Character.JobLevel1 += (byte)numLevels;
                                client.Character.SkillPoint += (ushort)(byte)numLevels;
                                break;
                            case LevelType.JLEVEL2:
                                client.Character.JobLevel2X += (byte)numLevels;
                                client.Character.SkillPoint2X += (ushort)(byte)numLevels;
                                break;
                            default:
                                client.Character.JobLevel2T += (byte)numLevels;
                                client.Character.SkillPoint2T += (ushort)(byte)numLevels;
                                break;
                        }
                    }
                    else
                        client.Character.JointJobLevel += (byte)numLevels;
                }
                num1 = (byte)2;
            }
            client.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.LEVEL_UP, (MapEventArgs)new SkillArg()
            {
                x = num1
            }, (SagaDB.Actor.Actor)client.Character, true);
            Singleton<StatusFactory>.Instance.CalcStatus(client.Character);
            client.Character.HP = client.Character.MaxHP;
            client.Character.MP = client.Character.MaxMP;
            client.Character.SP = client.Character.MaxSP;
            client.Map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)client.Character, false);
            client.SendPlayerInfo();
            Logger.ShowInfo(client.Character.Name + " gained " + (object)numLevels + "x" + type.ToString(), (Logger)null);
        }

        /// <summary>
        /// Get the number of levels the overflow of exp represents compared to the current level.
        /// </summary>
        /// <param name="level">.</param>
        /// <param name="exp">.</param>
        /// <param name="type">.</param>
        /// <param name="allowMultilevel">.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        private uint GetLevelDelta(uint level, uint exp, LevelType type, bool allowMultilevel)
        {
            this.currentMax = level > this.GetTypeSpecificMaxLevel(type) ? 0U : this.GetTypeSpecificMaxLevel(type) - level;
            uint num = 0;
            while ((allowMultilevel ? 1 : (num < 2U ? 1 : 0)) != 0 && num < this.currentMax && exp > this.GetExpForLevel((uint)((int)level + (int)num + 1), type))
                ++num;
            return num;
        }

        /// <summary>
        /// The GetTypeSpecificMaxLevel.
        /// </summary>
        /// <param name="type">The LevelType to get the max level for.</param>
        /// <returns>The max level for the input LevelType.</returns>
        private uint GetTypeSpecificMaxLevel(LevelType type)
        {
            switch (type)
            {
                case LevelType.CLEVEL:
                    return this.MaxCLevel;
                case LevelType.JLEVEL:
                    return this.MaxJLevel;
                case LevelType.JLEVEL2:
                    return this.MaxJLevel;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Defines the <see cref="Level" />.
        /// </summary>
        public struct Level
        {
            /// <summary>
            /// Defines the cxp.
            /// </summary>
            public readonly uint cxp;

            /// <summary>
            /// Defines the jxp.
            /// </summary>
            public readonly uint jxp;

            /// <summary>
            /// Defines the jxp2.
            /// </summary>
            public readonly uint jxp2;

            /// <summary>
            /// Initializes a new instance of the <see cref=""/> class.
            /// </summary>
            /// <param name="cxp">The cxp<see cref="uint"/>.</param>
            /// <param name="jxp">The jxp<see cref="uint"/>.</param>
            /// <param name="jxp2">The jxp2<see cref="uint"/>.</param>
            public Level(uint cxp, uint jxp, uint jxp2)
            {
                this.cxp = cxp;
                this.jxp = jxp;
                this.jxp2 = jxp2;
            }
        }
    }
}
