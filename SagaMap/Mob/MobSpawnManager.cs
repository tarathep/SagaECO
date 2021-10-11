namespace SagaMap.Mob
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaLib;
    using SagaLib.VirtualFileSystem;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="MobSpawnManager" />.
    /// </summary>
    public class MobSpawnManager : Singleton<MobSpawnManager>
    {
        /// <summary>
        /// Defines the mobs.
        /// </summary>
        private Dictionary<uint, List<ActorMob>> mobs = new Dictionary<uint, List<ActorMob>>();

        /// <summary>
        /// Gets the Spawns.
        /// </summary>
        public Dictionary<uint, List<ActorMob>> Spawns
        {
            get
            {
                return this.mobs;
            }
        }

        /// <summary>
        /// The LoadOne.
        /// </summary>
        /// <param name="f">The f<see cref="string"/>.</param>
        /// <param name="setMap">The setMap<see cref="uint"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int LoadOne(string f, uint setMap)
        {
            return this.LoadOne(f, setMap, true, true);
        }

        /// <summary>
        /// The LoadOne.
        /// </summary>
        /// <param name="f">The f<see cref="string"/>.</param>
        /// <param name="setMap">The setMap<see cref="uint"/>.</param>
        /// <param name="loadDelay">The loadDelay<see cref="bool"/>.</param>
        /// <param name="loadNoDelay">The loadNoDelay<see cref="bool"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int LoadOne(string f, uint setMap, bool loadDelay, bool loadNoDelay)
        {
            int num1 = 0;
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                Stream inStream = Singleton<VirtualFileSystemManager>.Instance.FileSystem.OpenFile(f);
                xmlDocument.Load(inStream);
                foreach (object childNode in xmlDocument["Spawns"].ChildNodes)
                {
                    if (childNode.GetType() == typeof(XmlElement))
                    {
                        XmlElement xmlElement1 = (XmlElement)childNode;
                        switch (xmlElement1.Name.ToLower())
                        {
                            case "spawn":
                                XmlNodeList childNodes = xmlElement1.ChildNodes;
                                uint index1 = 0;
                                uint num2 = 0;
                                byte pos1 = 0;
                                byte pos2 = 0;
                                int num3 = 0;
                                int num4 = 0;
                                int num5 = 30;
                                int num6 = 100;
                                string attribute = xmlElement1.GetAttribute("rate");
                                if (attribute != "")
                                    num6 = int.Parse(attribute);
                                foreach (object obj in childNodes)
                                {
                                    if (obj.GetType() == typeof(XmlElement))
                                    {
                                        XmlElement xmlElement2 = (XmlElement)obj;
                                        switch (xmlElement2.Name.ToLower())
                                        {
                                            case "id":
                                                num2 = uint.Parse(xmlElement2.InnerText);
                                                break;
                                            case "map":
                                                index1 = setMap == 0U ? uint.Parse(xmlElement2.InnerText) : setMap;
                                                break;
                                            case "x":
                                                pos1 = byte.Parse(xmlElement2.InnerText);
                                                break;
                                            case "y":
                                                pos2 = byte.Parse(xmlElement2.InnerText);
                                                break;
                                            case "amount":
                                                num3 = int.Parse(xmlElement2.InnerText);
                                                break;
                                            case "range":
                                                num4 = int.Parse(xmlElement2.InnerText);
                                                break;
                                            case "delay":
                                                num5 = int.Parse(xmlElement2.InnerText);
                                                break;
                                        }
                                    }
                                }
                                if (index1 == 0U)
                                    index1 = setMap;
                                if (index1 != 0U && (loadDelay || num5 == 0) && (loadNoDelay || num5 != 0))
                                {
                                    if (num5 == 0)
                                        num6 = 100;
                                    if (num6 > Global.Random.Next(0, 99) && Singleton<Configuration>.Instance.HostedMaps.Contains(index1))
                                    {
                                        for (int index2 = 0; index2 < num3 && Singleton<MobFactory>.Instance.Mobs.ContainsKey(num2); ++index2)
                                        {
                                            ActorMob mob = new ActorMob(num2);
                                            mob.MapID = index1;
                                            Map map = Singleton<MapManager>.Instance.GetMap(index1);
                                            if (map != null)
                                            {
                                                if (pos1 != (byte)0 || pos2 != (byte)0 || num4 != 0)
                                                {
                                                    int min1 = (int)pos1 - num4;
                                                    int max1 = (int)pos1 + num4;
                                                    int min2 = (int)pos2 - num4;
                                                    int max2 = (int)pos2 + num4;
                                                    if (min1 < 0)
                                                        min1 = 0;
                                                    if (max1 >= (int)map.Width)
                                                        max1 = (int)map.Width - 1;
                                                    if (min2 < 0)
                                                        min2 = 0;
                                                    if (max2 >= (int)map.Height)
                                                        max2 = (int)map.Height - 1;
                                                    int index3 = (int)(byte)Global.Random.Next(min1, max1);
                                                    int index4 = (int)(byte)Global.Random.Next(min2, max2);
                                                    int num7 = 0;
                                                    try
                                                    {
                                                        while (map.Info.walkable[index3, index4] != (byte)2)
                                                        {
                                                            if (num7 > 1000 || num4 == 0)
                                                            {
                                                                Logger.ShowWarning(string.Format("Cannot find free place for mob:{0} map:{1}[{2},{3}]", (object)num2, (object)index1, (object)pos1, (object)pos2), Logger.defaultlogger);
                                                                break;
                                                            }
                                                            index3 = (int)(byte)Global.Random.Next(min1, max1);
                                                            index4 = (int)(byte)Global.Random.Next(min2, max2);
                                                            ++num7;
                                                        }
                                                    }
                                                    catch
                                                    {
                                                    }
                                                    if (num7 <= 1000)
                                                    {
                                                        mob.X = Global.PosX8to16((byte)index3, map.Width);
                                                        mob.Y = Global.PosY8to16((byte)index4, map.Height);
                                                        MobEventHandler mobEventHandler = new MobEventHandler(mob);
                                                        mob.e = (ActorEventHandler)mobEventHandler;
                                                        mobEventHandler.AI.Mode = !Factory<MobAIFactory, AIMode>.Instance.Items.ContainsKey(mob.MobID) ? new AIMode(0) : Factory<MobAIFactory, AIMode>.Instance.Items[mob.MobID];
                                                        mobEventHandler.AI.X_Ori = Global.PosX8to16(pos1, map.Width);
                                                        mobEventHandler.AI.Y_Ori = Global.PosY8to16(pos2, map.Height);
                                                        mobEventHandler.AI.X_Spawn = mob.X;
                                                        mobEventHandler.AI.Y_Spawn = mob.Y;
                                                        mobEventHandler.AI.MoveRange = (short)(num4 * 100);
                                                        mobEventHandler.AI.SpawnDelay = num5 * 1000;
                                                        map.RegisterActor((SagaDB.Actor.Actor)mob);
                                                        mob.invisble = false;
                                                        mob.sightRange = 1500U;
                                                        map.OnActorVisibilityChange((SagaDB.Actor.Actor)mob);
                                                        List<ActorMob> actorMobList;
                                                        if (this.mobs.ContainsKey(index1))
                                                        {
                                                            actorMobList = this.mobs[index1];
                                                        }
                                                        else
                                                        {
                                                            actorMobList = new List<ActorMob>();
                                                            this.mobs.Add(index1, actorMobList);
                                                        }
                                                        actorMobList.Add(mob);
                                                        ++num1;
                                                    }
                                                }
                                                else
                                                    break;
                                            }
                                        }
                                        break;
                                    }
                                    continue;
                                }
                                continue;
                        }
                    }
                }
                inStream.Close();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            return num1;
        }

        /// <summary>
        /// The LoadSpawn.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        public void LoadSpawn(string path)
        {
            string[] strArray = Singleton<VirtualFileSystemManager>.Instance.FileSystem.SearchFile(path, "*.xml");
            int num = 0;
            foreach (string f in strArray)
                num += this.LoadOne(f, 0U);
            Logger.ShowInfo(num.ToString() + " mobs spawned...");
        }
    }
}
