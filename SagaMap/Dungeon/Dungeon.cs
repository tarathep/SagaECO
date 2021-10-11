namespace SagaMap.Dungeon
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Packets.Server;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Dungeon" />.
    /// </summary>
    public class Dungeon
    {
        /// <summary>
        /// Defines the maps.
        /// </summary>
        private List<DungeonMap> maps = new List<DungeonMap>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the dungeonID.
        /// </summary>
        private uint dungeonID;

        /// <summary>
        /// Defines the time.
        /// </summary>
        private int time;

        /// <summary>
        /// Defines the theme.
        /// </summary>
        private Theme theme;

        /// <summary>
        /// Defines the startMap.
        /// </summary>
        private uint startMap;

        /// <summary>
        /// Defines the endMap.
        /// </summary>
        private uint endMap;

        /// <summary>
        /// Defines the maxRoom.
        /// </summary>
        private int maxRoom;

        /// <summary>
        /// Defines the maxCross.
        /// </summary>
        private int maxCross;

        /// <summary>
        /// Defines the maxFloor.
        /// </summary>
        private int maxFloor;

        /// <summary>
        /// Defines the start.
        /// </summary>
        private DungeonMap start;

        /// <summary>
        /// Defines the end.
        /// </summary>
        private DungeonMap end;

        /// <summary>
        /// Defines the spawnFile.
        /// </summary>
        private string spawnFile;

        /// <summary>
        /// Defines the task.
        /// </summary>
        private SagaMap.Tasks.Dungeon.Dungeon task;

        /// <summary>
        /// Defines the creator.
        /// </summary>
        private ActorPC creator;

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public uint ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets the DungeonID.
        /// </summary>
        public uint DungeonID
        {
            get
            {
                return this.dungeonID;
            }
            set
            {
                this.dungeonID = value;
            }
        }

        /// <summary>
        /// Gets or sets the TimeLimit.
        /// </summary>
        public int TimeLimit
        {
            get
            {
                return this.time;
            }
            set
            {
                this.time = value;
            }
        }

        /// <summary>
        /// Gets or sets the Theme.
        /// </summary>
        public Theme Theme
        {
            get
            {
                return this.theme;
            }
            set
            {
                this.theme = value;
            }
        }

        /// <summary>
        /// Gets or sets the StartMap.
        /// </summary>
        public uint StartMap
        {
            get
            {
                return this.startMap;
            }
            set
            {
                this.startMap = value;
            }
        }

        /// <summary>
        /// Gets or sets the EndMap.
        /// </summary>
        public uint EndMap
        {
            get
            {
                return this.endMap;
            }
            set
            {
                this.endMap = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxRoomCount.
        /// </summary>
        public int MaxRoomCount
        {
            get
            {
                return this.maxRoom;
            }
            set
            {
                this.maxRoom = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxCrossCount.
        /// </summary>
        public int MaxCrossCount
        {
            get
            {
                return this.maxCross;
            }
            set
            {
                this.maxCross = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxFloorCount.
        /// </summary>
        public int MaxFloorCount
        {
            get
            {
                return this.maxFloor;
            }
            set
            {
                this.maxFloor = value;
            }
        }

        /// <summary>
        /// Gets or sets the SpawnFile.
        /// </summary>
        public string SpawnFile
        {
            get
            {
                return this.spawnFile;
            }
            set
            {
                this.spawnFile = value;
            }
        }

        /// <summary>
        /// Gets the DestroyTask.
        /// </summary>
        public SagaMap.Tasks.Dungeon.Dungeon DestroyTask
        {
            get
            {
                return this.task;
            }
        }

        /// <summary>
        /// Gets or sets the Creator.
        /// </summary>
        public ActorPC Creator
        {
            get
            {
                return this.creator;
            }
            set
            {
                this.creator = value;
            }
        }

        /// <summary>
        /// Gets the Maps.
        /// </summary>
        public List<DungeonMap> Maps
        {
            get
            {
                return this.maps;
            }
        }

        /// <summary>
        /// Gets or sets the Start.
        /// </summary>
        public DungeonMap Start
        {
            get
            {
                return this.start;
            }
            set
            {
                this.start = value;
            }
        }

        /// <summary>
        /// Gets or sets the End.
        /// </summary>
        public DungeonMap End
        {
            get
            {
                return this.end;
            }
            set
            {
                this.end = value;
            }
        }

        /// <summary>
        /// The Clone.
        /// </summary>
        /// <returns>The <see cref="SagaMap.Dungeon.Dungeon"/>.</returns>
        public SagaMap.Dungeon.Dungeon Clone()
        {
            SagaMap.Dungeon.Dungeon dungeon = new SagaMap.Dungeon.Dungeon();
            dungeon.id = this.id;
            dungeon.time = this.time;
            dungeon.theme = this.theme;
            dungeon.startMap = this.startMap;
            dungeon.endMap = this.endMap;
            dungeon.maxCross = this.maxCross;
            dungeon.maxFloor = this.maxFloor;
            dungeon.maxRoom = this.maxRoom;
            dungeon.spawnFile = this.spawnFile;
            dungeon.task = new SagaMap.Tasks.Dungeon.Dungeon(dungeon, dungeon.time);
            return dungeon;
        }

        /// <summary>
        /// The Destory.
        /// </summary>
        /// <param name="type">The type<see cref="DestroyType"/>.</param>
        public void Destory(DestroyType type)
        {
            switch (type)
            {
                case DestroyType.BossDown:
                    foreach (SagaDB.Actor.Actor actor in this.End.Map.Actors.Values)
                    {
                        if (actor.type == ActorType.PC && ((ActorPC)actor).Online)
                        {
                            PCEventHandler e = (PCEventHandler)actor.e;
                            SSMG_NPC_SET_EVENT_AREA ssmgNpcSetEventArea = new SSMG_NPC_SET_EVENT_AREA();
                            if (this.end.Gates.ContainsKey(GateType.Exit))
                            {
                                ssmgNpcSetEventArea.StartX = (uint)this.end.Gates[GateType.Exit].X;
                                ssmgNpcSetEventArea.EndX = (uint)this.end.Gates[GateType.Exit].X;
                                ssmgNpcSetEventArea.StartY = (uint)this.end.Gates[GateType.Exit].Y;
                                ssmgNpcSetEventArea.EndY = (uint)this.end.Gates[GateType.Exit].Y;
                            }
                            else
                            {
                                ssmgNpcSetEventArea.StartX = (uint)(byte)((uint)this.end.Map.Width / 2U);
                                ssmgNpcSetEventArea.EndX = (uint)(byte)((uint)this.end.Map.Width / 2U);
                                ssmgNpcSetEventArea.StartY = (uint)(byte)((uint)this.end.Map.Height / 2U);
                                ssmgNpcSetEventArea.EndY = (uint)(byte)((uint)this.end.Map.Height / 2U);
                            }
                            ssmgNpcSetEventArea.EventID = 12001505U;
                            ssmgNpcSetEventArea.EffectID = 9005U;
                            e.Client.netIO.SendPacket((Packet)ssmgNpcSetEventArea);
                        }
                    }
                    this.task.counter = this.task.lifeTime - 31;
                    break;
                case DestroyType.QuestCancel:
                    foreach (DungeonMap map in this.maps)
                    {
                        foreach (SagaDB.Actor.Actor actor in map.Map.Actors.Values)
                        {
                            if (actor.type == ActorType.PC && ((ActorPC)actor).Online)
                                ((PCEventHandler)actor.e).Client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ITD_QUEST_CANCEL);
                        }
                    }
                    this.task.counter = this.task.lifeTime - 31;
                    break;
                case DestroyType.PartyDismiss:
                    foreach (DungeonMap map in this.maps)
                    {
                        foreach (SagaDB.Actor.Actor actor in map.Map.Actors.Values)
                        {
                            if (actor.type == ActorType.PC && ((ActorPC)actor).Online)
                                ((PCEventHandler)actor.e).Client.SendSystemMessage(Singleton<LocalManager>.Instance.Strings.ITD_PARTY_DISMISSED);
                        }
                    }
                    this.task.counter = this.task.lifeTime - 31;
                    break;
                case DestroyType.TimeOver:
                    foreach (DungeonMap map in this.maps)
                    {
                        Singleton<MapManager>.Instance.DeleteMapInstance(map.Map.ID);
                        map.Map.DungeonMap = (DungeonMap)null;
                        map.Map = (Map)null;
                    }
                    this.maps.Clear();
                    this.Creator.DungeonID = 0U;
                    Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.RemoveDungeon(this.dungeonID);
                    break;
            }
        }
    }
}
