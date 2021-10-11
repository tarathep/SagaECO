namespace SagaMap.Dungeon
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="DungeonFactory" />.
    /// </summary>
    public class DungeonFactory : Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>
    {
        /// <summary>
        /// Defines the dungeons.
        /// </summary>
        private Dictionary<uint, SagaMap.Dungeon.Dungeon> dungeons = new Dictionary<uint, SagaMap.Dungeon.Dungeon>();

        /// <summary>
        /// Defines the count.
        /// </summary>
        private int count = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonFactory"/> class.
        /// </summary>
        public DungeonFactory()
        {
            this.loadingTab = "Loading Dungeon database";
            this.loadedTab = " dungeons loaded.";
            this.databaseName = "dungeon";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetDungeon.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaMap.Dungeon.Dungeon"/>.</returns>
        public SagaMap.Dungeon.Dungeon GetDungeon(uint id)
        {
            if (this.dungeons.ContainsKey(id))
                return this.dungeons[id];
            return (SagaMap.Dungeon.Dungeon)null;
        }

        /// <summary>
        /// The RemoveDungeon.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        public void RemoveDungeon(uint id)
        {
            if (!this.dungeons.ContainsKey(id))
                return;
            this.dungeons.Remove(id);
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="SagaMap.Dungeon.Dungeon"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(SagaMap.Dungeon.Dungeon item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="SagaMap.Dungeon.Dungeon"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(SagaMap.Dungeon.Dungeon item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="SagaMap.Dungeon.Dungeon"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, SagaMap.Dungeon.Dungeon item)
        {
            switch (root.Name.ToLower())
            {
                case "dungeon":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
                            return;
                        case "timelimit":
                            item.TimeLimit = int.Parse(current.InnerText);
                            return;
                        case "theme":
                            item.Theme = (Theme)Enum.Parse(typeof(Theme), current.InnerText);
                            return;
                        case "startmap":
                            item.StartMap = uint.Parse(current.InnerText);
                            return;
                        case "endmap":
                            item.EndMap = uint.Parse(current.InnerText);
                            return;
                        case "maxroomcount":
                            item.MaxRoomCount = int.Parse(current.InnerText);
                            return;
                        case "maxcrosscount":
                            item.MaxCrossCount = int.Parse(current.InnerText);
                            return;
                        case "maxfloorcount":
                            item.MaxFloorCount = int.Parse(current.InnerText);
                            return;
                        case "spawnfile":
                            item.SpawnFile = current.InnerText;
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
            }
        }

        /// <summary>
        /// The CreateDungeon.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <param name="creator">The creator<see cref="ActorPC"/>.</param>
        /// <param name="exitMap">The exitMap<see cref="uint"/>.</param>
        /// <param name="exitX">The exitX<see cref="byte"/>.</param>
        /// <param name="exitY">The exitY<see cref="byte"/>.</param>
        /// <returns>The <see cref="SagaMap.Dungeon.Dungeon"/>.</returns>
        public SagaMap.Dungeon.Dungeon CreateDungeon(uint id, ActorPC creator, uint exitMap, byte exitX, byte exitY)
        {
        label_1:
            if (!this.items.ContainsKey(id))
                return (SagaMap.Dungeon.Dungeon)null;
            ++this.count;
            SagaMap.Dungeon.Dungeon dungeon = this.items[id].Clone();
            dungeon.Creator = creator;
            dungeon.DungeonID = (uint)this.count;
            DungeonMap[,] dungeonMapArray = new DungeonMap[20, 20];
            List<DungeonMap> dungeonMapList = new List<DungeonMap>();
            int maxRoomCount = dungeon.MaxRoomCount;
            int maxCrossCount = dungeon.MaxCrossCount;
            int maxFloorCount = dungeon.MaxFloorCount;
            int num1 = 1;
            List<DungeonMap> list1 = Factory<DungeonMapsFactory, DungeonMap>.Instance.Items.Values.Where<DungeonMap>((Func<DungeonMap, bool>)(m => m.Theme == dungeon.Theme && m.MapType == MapType.Room)).ToList<DungeonMap>();
            List<DungeonMap> list2 = Factory<DungeonMapsFactory, DungeonMap>.Instance.Items.Values.Where<DungeonMap>((Func<DungeonMap, bool>)(m => m.Theme == dungeon.Theme && m.MapType == MapType.Cross)).ToList<DungeonMap>();
            List<DungeonMap> list3 = Factory<DungeonMapsFactory, DungeonMap>.Instance.Items.Values.Where<DungeonMap>((Func<DungeonMap, bool>)(m => m.Theme == dungeon.Theme && m.MapType == MapType.Floor)).ToList<DungeonMap>();
            byte num2 = (byte)Global.Random.Next(0, 19);
            byte num3 = (byte)Global.Random.Next(0, 19);
            dungeon.Start = Factory<DungeonMapsFactory, DungeonMap>.Instance.Items[dungeon.StartMap].Clone();
            dungeon.Start.Map = Singleton<MapManager>.Instance.GetMap(Singleton<MapManager>.Instance.CreateMapInstance(creator, dungeon.StartMap, exitMap, exitX, exitY));
            dungeon.Start.Map.IsDungeon = true;
            dungeon.Start.Map.DungeonMap = dungeon.Start;
            dungeon.Start.X = num2;
            dungeon.Start.Y = num3;
            dungeon.Maps.Add(dungeon.Start);
            dungeonMapArray[(int)num2, (int)num3] = dungeon.Start;
            dungeonMapList.Add(dungeon.Start);
            while (maxRoomCount > 0 || maxFloorCount > 0 || maxCrossCount > 0 || num1 > 0)
            {
                if (dungeonMapList.Count == 0)
                {
                    Logger.ShowWarning("Dungeon(" + id.ToString() + "): All nodes closed, but still rooms remaining, Recreating....");
                    dungeon.Destory(DestroyType.QuestCancel);
                    goto label_1;
                }
                else
                {
                    DungeonMap dungeonMap1 = dungeonMapList[Global.Random.Next(0, dungeonMapList.Count - 1)];
                    List<GateType> gateTypeList = new List<GateType>();
                    if (dungeonMap1.Gates.ContainsKey(GateType.North) && dungeonMap1.Gates[GateType.North].ConnectedMap == null && (dungeonMap1.GetXForGate(GateType.North) < (byte)20 && dungeonMap1.GetYForGate(GateType.North) < (byte)20 && dungeonMapArray[(int)dungeonMap1.GetXForGate(GateType.North), (int)dungeonMap1.GetYForGate(GateType.North)] == null))
                        gateTypeList.Add(GateType.North);
                    if (dungeonMap1.Gates.ContainsKey(GateType.East) && dungeonMap1.Gates[GateType.East].ConnectedMap == null && (dungeonMap1.GetXForGate(GateType.East) < (byte)20 && dungeonMap1.GetYForGate(GateType.East) < (byte)20 && dungeonMapArray[(int)dungeonMap1.GetXForGate(GateType.East), (int)dungeonMap1.GetYForGate(GateType.East)] == null))
                        gateTypeList.Add(GateType.East);
                    if (dungeonMap1.Gates.ContainsKey(GateType.South) && dungeonMap1.Gates[GateType.South].ConnectedMap == null && (dungeonMap1.GetXForGate(GateType.South) < (byte)20 && dungeonMap1.GetYForGate(GateType.South) < (byte)20 && dungeonMapArray[(int)dungeonMap1.GetXForGate(GateType.South), (int)dungeonMap1.GetYForGate(GateType.South)] == null))
                        gateTypeList.Add(GateType.South);
                    if (dungeonMap1.Gates.ContainsKey(GateType.West) && dungeonMap1.Gates[GateType.West].ConnectedMap == null && (dungeonMap1.GetXForGate(GateType.West) < (byte)20 && dungeonMap1.GetYForGate(GateType.West) < (byte)20 && dungeonMapArray[(int)dungeonMap1.GetXForGate(GateType.West), (int)dungeonMap1.GetYForGate(GateType.West)] == null))
                        gateTypeList.Add(GateType.West);
                    if (gateTypeList.Count == 0)
                    {
                        dungeonMapList.Remove(dungeonMap1);
                    }
                    else
                    {
                        GateType type = gateTypeList.Count <= 1 ? gateTypeList[0] : gateTypeList[Global.Random.Next(0, gateTypeList.Count - 1)];
                        GateType key = GateType.North;
                        switch (type)
                        {
                            case GateType.East:
                                key = GateType.West;
                                break;
                            case GateType.West:
                                key = GateType.East;
                                break;
                            case GateType.South:
                                key = GateType.North;
                                break;
                            case GateType.North:
                                key = GateType.South;
                                break;
                        }
                        List<MapType> mapTypeList = new List<MapType>();
                        if (maxRoomCount > 0)
                            mapTypeList.Add(MapType.Room);
                        if (maxCrossCount > 0)
                            mapTypeList.Add(MapType.Cross);
                        if (maxFloorCount > 0)
                            mapTypeList.Add(MapType.Floor);
                        MapType mapType = mapTypeList.Count <= 0 ? MapType.End : (mapTypeList.Count != 1 ? mapTypeList[Global.Random.Next(0, mapTypeList.Count - 1)] : mapTypeList[0]);
                        DungeonMap dungeonMap2 = (DungeonMap)null;
                        bool flag = false;
                        switch (mapType)
                        {
                            case MapType.End:
                                dungeonMap2 = Factory<DungeonMapsFactory, DungeonMap>.Instance.Items[dungeon.EndMap].Clone();
                                dungeon.End = dungeonMap2;
                                flag = true;
                                --num1;
                                break;
                            case MapType.Room:
                                dungeonMap2 = list1[Global.Random.Next(0, list1.Count - 1)].Clone();
                                --maxRoomCount;
                                break;
                            case MapType.Cross:
                                dungeonMap2 = list2[Global.Random.Next(0, list2.Count - 1)].Clone();
                                --maxCrossCount;
                                break;
                            case MapType.Floor:
                                dungeonMap2 = list3[Global.Random.Next(0, list3.Count - 1)].Clone();
                                --maxFloorCount;
                                break;
                        }
                        dungeonMap2.X = dungeonMap1.GetXForGate(type);
                        dungeonMap2.Y = dungeonMap1.GetYForGate(type);
                        dungeonMap2.Map = Singleton<MapManager>.Instance.GetMap(Singleton<MapManager>.Instance.CreateMapInstance(creator, dungeonMap2.ID, exitMap, exitX, exitY));
                        dungeonMap2.Map.IsDungeon = true;
                        dungeonMap2.Map.DungeonMap = dungeonMap2;
                        if (flag)
                            Singleton<MobSpawnManager>.Instance.LoadOne(dungeon.SpawnFile, dungeonMap2.Map.ID, false, true);
                        else
                            Singleton<MobSpawnManager>.Instance.LoadOne(dungeon.SpawnFile, dungeonMap2.Map.ID, true, false);
                        dungeon.Maps.Add(dungeonMap2);
                        dungeonMapArray[(int)dungeonMap2.X, (int)dungeonMap2.Y] = dungeonMap2;
                        int num4 = Global.Random.Next(1, 3);
                        for (int index = 0; index < num4; ++index)
                            dungeonMap2.Rotate();
                        int num5 = 0;
                        while (!dungeonMap2.Gates.ContainsKey(key))
                        {
                            dungeonMap2.Rotate();
                            ++num5;
                            if (num5 > 3)
                                break;
                        }
                        if (dungeonMap2.Gates.ContainsKey(key))
                        {
                            dungeonMap1.Gates[type].ConnectedMap = dungeonMap2;
                            dungeonMap1.Gates[type].Direction = Direction.In;
                            dungeonMap2.Gates[key].ConnectedMap = dungeonMap1;
                            dungeonMap2.Gates[key].Direction = Direction.Out;
                            if (dungeonMap1.FreeGates == 0 || dungeonMap1 == dungeon.Start)
                                dungeonMapList.Remove(dungeonMap1);
                            if (dungeonMap2.FreeGates > 0)
                                dungeonMapList.Add(dungeonMap2);
                        }
                    }
                }
            }
            creator.DungeonID = dungeon.DungeonID;
            if (creator.Party != null)
            {
                foreach (ActorPC actorPc in creator.Party.Members.Values)
                {
                    if (actorPc.Online)
                        ((PCEventHandler)actorPc.e).Client.SendSystemMessage(creator.Name + Singleton<LocalManager>.Instance.Strings.ITD_CREATED);
                }
            }
            else
                ((PCEventHandler)creator.e).Client.SendSystemMessage(creator.Name + Singleton<LocalManager>.Instance.Strings.ITD_CREATED);
            dungeon.DestroyTask.Activate();
            this.dungeons.Add(dungeon.DungeonID, dungeon);
            return dungeon;
        }
    }
}
