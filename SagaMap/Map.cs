namespace SagaMap
{
    using SagaDB;
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaDB.Map;
    using SagaDB.Treasure;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Dungeon;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Client;
    using SagaMap.Tasks.Item;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="Map" />.
    /// </summary>
    public class Map
    {
        /// <summary>
        /// Defines the ID_BORDER_MOB.
        /// </summary>
        private const uint ID_BORDER_MOB = 10000;

        /// <summary>
        /// Defines the ID_BORDER_PET.
        /// </summary>
        private const uint ID_BORDER_PET = 20000;

        /// <summary>
        /// Defines the ID_BORDER_GOLEM.
        /// </summary>
        private const uint ID_BORDER_GOLEM = 40000;

        /// <summary>
        /// Defines the ID_BORDER_ITEM.
        /// </summary>
        private const uint ID_BORDER_ITEM = 50000;

        /// <summary>
        /// Defines the ID_BORDER_EVENT.
        /// </summary>
        private const uint ID_BORDER_EVENT = 60000;

        /// <summary>
        /// Defines the ID_BORDER2.
        /// </summary>
        private const uint ID_BORDER2 = 1000000000;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the width.
        /// </summary>
        private ushort width;

        /// <summary>
        /// Defines the height.
        /// </summary>
        private ushort height;

        /// <summary>
        /// Defines the actorsByID.
        /// </summary>
        private Dictionary<uint, SagaDB.Actor.Actor> actorsByID;

        /// <summary>
        /// Defines the actorsByRegion.
        /// </summary>
        private Dictionary<uint, List<SagaDB.Actor.Actor>> actorsByRegion;

        /// <summary>
        /// Defines the pcByName.
        /// </summary>
        private Dictionary<string, ActorPC> pcByName;

        /// <summary>
        /// Defines the nextPcId.
        /// </summary>
        private static uint nextPcId;

        /// <summary>
        /// Defines the nextMobId.
        /// </summary>
        private uint nextMobId;

        /// <summary>
        /// Defines the nextItemId.
        /// </summary>
        private uint nextItemId;

        /// <summary>
        /// Defines the nextPetId.
        /// </summary>
        private uint nextPetId;

        /// <summary>
        /// Defines the nextEventId.
        /// </summary>
        private uint nextEventId;

        /// <summary>
        /// Defines the nextGolemId.
        /// </summary>
        private uint nextGolemId;

        /// <summary>
        /// Defines the Info.
        /// </summary>
        public MapInfo Info;

        /// <summary>
        /// Defines the instance.
        /// </summary>
        private bool instance;

        /// <summary>
        /// Defines the dungeon.
        /// </summary>
        private bool dungeon;

        /// <summary>
        /// Defines the clientExitMap.
        /// </summary>
        private uint clientExitMap;

        /// <summary>
        /// Defines the clientExitX.
        /// </summary>
        private byte clientExitX;

        /// <summary>
        /// Defines the clientExitY.
        /// </summary>
        private byte clientExitY;

        /// <summary>
        /// Defines the creator.
        /// </summary>
        private ActorPC creator;

        /// <summary>
        /// Defines the dungeonMap.
        /// </summary>
        private DungeonMap dungeonMap;

        /// <summary>
        /// The Distance.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="short"/>.</returns>
        public static short Distance(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor)
        {
            return (short)Math.Sqrt((double)(((int)dActor.X - (int)sActor.X) * ((int)dActor.X - (int)sActor.X) + ((int)dActor.Y - (int)sActor.Y) * ((int)dActor.Y - (int)sActor.Y)));
        }

        /// <summary>
        /// The Announce.
        /// </summary>
        /// <param name="text">The text<see cref="string"/>.</param>
        public void Announce(string text)
        {
            foreach (SagaDB.Actor.Actor actor in this.actorsByID.Values.ToList<SagaDB.Actor.Actor>())
            {
                if (actor.type == ActorType.PC)
                    MapClient.FromActorPC((ActorPC)actor).SendAnnounce(text);
            }
        }

        /// <summary>
        /// The FindFreeCoord.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <param name="x2">The x2<see cref="short"/>.</param>
        /// <param name="y2">The y2<see cref="short"/>.</param>
        /// <param name="excludes">The excludes<see cref="SagaDB.Actor.Actor[]"/>.</param>
        public void FindFreeCoord(short x, short y, out short x2, out short y2, params SagaDB.Actor.Actor[] excludes)
        {
            if (this.GetActorsArea(x, y, (short)50, excludes).Count == 0)
            {
                x2 = x;
                y2 = y;
            }
            else
            {
                short num1 = -100;
                while (num1 < (short)200)
                {
                    short num2 = -100;
                    while (num2 < (short)200)
                    {
                        if (this.GetActorsArea((short)((int)x + (int)num1), (short)((int)y + (int)num2), (short)50, excludes).Count == 0)
                        {
                            x2 = (short)((int)x + (int)num1);
                            y2 = (short)((int)y + (int)num2);
                            return;
                        }
                        num2 += (short)100;
                    }
                    num1 += (short)100;
                }
                x2 = x;
                y2 = y;
            }
        }

        /// <summary>
        /// The CalcDir.
        /// </summary>
        /// <param name="x">原点X.</param>
        /// <param name="y">原点Y.</param>
        /// <param name="x2">目标点X.</param>
        /// <param name="y2">目标点Y.</param>
        /// <returns>.</returns>
        public ushort CalcDir(short x, short y, short x2, short y2)
        {
            short num1 = (short)((int)x2 - (int)x);
            short num2 = (short)((int)y2 - (int)y);
            if (num1 < (short)0)
                return (ushort)(Math.Acos((double)num2 / Math.Sqrt((double)((int)num1 * (int)num1 + (int)num2 * (int)num2))) / Math.PI * 180.0);
            return (ushort)(360.0 - Math.Acos((double)num2 / Math.Sqrt((double)((int)num1 * (int)num1 + (int)num2 * (int)num2))) / Math.PI * 180.0);
        }

        /// <summary>
        /// The GetActorsArea.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="range">The range<see cref="short"/>.</param>
        /// <param name="includeSourceActor">The includeSourceActor<see cref="bool"/>.</param>
        /// <returns>The <see cref="List{SagaDB.Actor.Actor}"/>.</returns>
        public List<SagaDB.Actor.Actor> GetActorsArea(SagaDB.Actor.Actor sActor, short range, bool includeSourceActor)
        {
            return this.GetActorsArea(sActor, range, includeSourceActor, true);
        }

        /// <summary>
        /// The GetActorsArea.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="range">The range<see cref="short"/>.</param>
        /// <param name="includeSourceActor">The includeSourceActor<see cref="bool"/>.</param>
        /// <param name="includeInvisibleActor">The includeInvisibleActor<see cref="bool"/>.</param>
        /// <returns>The <see cref="List{SagaDB.Actor.Actor}"/>.</returns>
        public List<SagaDB.Actor.Actor> GetActorsArea(SagaDB.Actor.Actor sActor, short range, bool includeSourceActor, bool includeInvisibleActor)
        {
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            for (short index1 = -1; index1 <= (short)1; ++index1)
            {
                for (short index2 = -1; index2 <= (short)1; ++index2)
                {
                    uint key = (uint)((ulong)this.GetRegion((float)sActor.X, (float)sActor.Y) + (ulong)((int)index2 * 1000000) + (ulong)index1);
                    if (this.actorsByRegion.ContainsKey(key))
                    {
                        foreach (SagaDB.Actor.Actor A in this.actorsByRegion[key].ToArray())
                        {
                            if ((includeSourceActor || (int)A.ActorID != (int)sActor.ActorID) && ((includeInvisibleActor || !A.Buff.Transparent) && this.ACanSeeB(A, sActor, (float)range)))
                                actorList.Add(A);
                        }
                    }
                }
            }
            return actorList;
        }

        /// <summary>
        /// The GetActorsArea.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <param name="range">The range<see cref="short"/>.</param>
        /// <param name="excludes">The excludes<see cref="SagaDB.Actor.Actor[]"/>.</param>
        /// <returns>The <see cref="List{SagaDB.Actor.Actor}"/>.</returns>
        public List<SagaDB.Actor.Actor> GetActorsArea(short x, short y, short range, params SagaDB.Actor.Actor[] excludes)
        {
            return this.GetActorsArea(x, y, range, true, excludes);
        }

        /// <summary>
        /// The GetActorsArea.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <param name="range">The range<see cref="short"/>.</param>
        /// <param name="includeInvisibleActor">The includeInvisibleActor<see cref="bool"/>.</param>
        /// <param name="excludes">The excludes<see cref="SagaDB.Actor.Actor[]"/>.</param>
        /// <returns>The <see cref="List{SagaDB.Actor.Actor}"/>.</returns>
        public List<SagaDB.Actor.Actor> GetActorsArea(short x, short y, short range, bool includeInvisibleActor, params SagaDB.Actor.Actor[] excludes)
        {
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            for (short index1 = -1; index1 <= (short)1; ++index1)
            {
                for (short index2 = -1; index2 <= (short)1; ++index2)
                {
                    uint key = (uint)((ulong)this.GetRegion((float)x, (float)y) + (ulong)((int)index2 * 1000000) + (ulong)index1);
                    if (this.actorsByRegion.ContainsKey(key))
                    {
                        foreach (SagaDB.Actor.Actor actor in this.actorsByRegion[key].ToArray())
                        {
                            bool flag = false;
                            if (excludes != null)
                            {
                                foreach (SagaDB.Actor.Actor exclude in excludes)
                                {
                                    if (actor == exclude)
                                        flag = true;
                                }
                            }
                            if (actor != null && !flag && (includeInvisibleActor || !actor.Buff.Transparent) && ((int)actor.X >= (int)x - (int)range && (int)actor.X <= (int)x + (int)range && (int)actor.Y >= (int)y - (int)range && (int)actor.Y <= (int)y + (int)range))
                                actorList.Add(actor);
                        }
                    }
                }
            }
            return actorList;
        }

        /// <summary>
        /// The SpawnMob.
        /// </summary>
        /// <param name="mobID">The mobID<see cref="uint"/>.</param>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <param name="moveRange">The moveRange<see cref="short"/>.</param>
        /// <param name="master">The master<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="ActorMob"/>.</returns>
        public ActorMob SpawnMob(uint mobID, short x, short y, short moveRange, SagaDB.Actor.Actor master)
        {
            ActorMob mob = new ActorMob(mobID);
            mob.MapID = this.ID;
            mob.X = x;
            mob.Y = y;
            MobEventHandler mobEventHandler = new MobEventHandler(mob);
            mob.e = (ActorEventHandler)mobEventHandler;
            mobEventHandler.AI.MoveRange = moveRange;
            mobEventHandler.AI.Mode = !Factory<MobAIFactory, AIMode>.Instance.Items.ContainsKey(mob.MobID) ? new AIMode(0) : Factory<MobAIFactory, AIMode>.Instance.Items[mob.MobID];
            mobEventHandler.AI.Master = master;
            mobEventHandler.AI.X_Ori = x;
            mobEventHandler.AI.Y_Ori = y;
            mobEventHandler.AI.X_Spawn = x;
            mobEventHandler.AI.Y_Spawn = y;
            if (mobEventHandler.AI.Master != null)
                mobEventHandler.AI.OnAttacked(master, 1);
            this.RegisterActor((SagaDB.Actor.Actor)mob);
            mob.invisble = false;
            mob.sightRange = 1500U;
            this.SendVisibleActorsToActor((SagaDB.Actor.Actor)mob);
            this.OnActorVisibilityChange((SagaDB.Actor.Actor)mob);
            mobEventHandler.AI.Start();
            return mob;
        }

        /// <summary>
        /// The CheckActorSkillInRange.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <param name="range">The range<see cref="short"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool CheckActorSkillInRange(short x, short y, short range)
        {
            foreach (SagaDB.Actor.Actor actor in this.GetActorsArea(x, y, range, new SagaDB.Actor.Actor[0]))
            {
                if (actor.type == ActorType.SKILL && !((ActorSkill)actor).Stackable)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The CountActorType.
        /// </summary>
        /// <param name="type">The type<see cref="ActorType"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CountActorType(ActorType type)
        {
            List<SagaDB.Actor.Actor> list = this.actorsByID.Values.ToList<SagaDB.Actor.Actor>();
            int num = 0;
            foreach (SagaDB.Actor.Actor actor in list)
            {
                if (actor.type == type)
                    ++num;
            }
            return num;
        }

        /// <summary>
        /// The SendEffect.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="effect">The effect<see cref="uint"/>.</param>
        public void SendEffect(SagaDB.Actor.Actor actor, uint effect)
        {
            this.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
            {
                actorID = actor.ActorID,
                effectID = effect
            }, actor, true);
        }

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
        /// Gets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the Width.
        /// </summary>
        public ushort Width
        {
            get
            {
                return this.width;
            }
        }

        /// <summary>
        /// Gets the Height.
        /// </summary>
        public ushort Height
        {
            get
            {
                return this.height;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="MapInfo"/>.</param>
        public Map(MapInfo info)
        {
            this.id = info.id;
            this.name = info.name;
            this.width = info.width;
            this.height = info.height;
            this.Info = info;
            this.actorsByID = new Dictionary<uint, SagaDB.Actor.Actor>();
            this.actorsByRegion = new Dictionary<uint, List<SagaDB.Actor.Actor>>();
            this.pcByName = new Dictionary<string, ActorPC>();
            if (SagaMap.Map.nextPcId == 0U)
                SagaMap.Map.nextPcId = 16U;
            this.nextMobId = 10001U;
            this.nextItemId = 50001U;
            this.nextPetId = 20001U;
            this.nextEventId = 60001U;
            this.nextGolemId = 40001U;
        }

        /// <summary>
        /// The GetRandomPos.
        /// </summary>
        /// <returns>The <see cref="short[]"/>.</returns>
        public short[] GetRandomPos()
        {
            return new short[2]
            {
        (short) Global.Random.Next(-12700, 12700),
        (short) Global.Random.Next(-12700, 12700)
            };
        }

        /// <summary>
        /// The GetRandomPosAroundActor.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="short[]"/>.</returns>
        public short[] GetRandomPosAroundActor(SagaDB.Actor.Actor actor)
        {
            return new short[2]
            {
        (short) Global.Random.Next((int) actor.X - 50, (int) actor.X + 50),
        (short) Global.Random.Next((int) actor.Y - 50, (int) actor.Y + 50)
            };
        }

        /// <summary>
        /// The GetRandomPosAroundPos.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <param name="range">The range<see cref="int"/>.</param>
        /// <returns>The <see cref="short[]"/>.</returns>
        public short[] GetRandomPosAroundPos(short x, short y, int range)
        {
            short[] numArray = new short[2];
            int num1 = 0;
            while (num1 < 1000)
            {
                numArray[0] = (short)Global.Random.Next((int)x - range, (int)x + range);
                numArray[1] = (short)Global.Random.Next((int)y - range, (int)y + range);
                byte num2 = Global.PosX16to8(numArray[0], this.width);
                byte num3 = Global.PosY16to8(numArray[1], this.height);
                ++num1;
                if ((int)num2 >= (int)this.width)
                    num2 = (byte)((uint)this.width - 1U);
                if ((int)num3 >= (int)this.height)
                    num3 = (byte)((uint)this.height - 1U);
                if (this.Info.walkable[(int)num2, (int)num3] == (byte)2)
                    return numArray;
            }
            numArray[0] = x;
            numArray[(int)y] = y;
            return numArray;
        }

        /// <summary>
        /// Gets the Actors.
        /// </summary>
        public Dictionary<uint, SagaDB.Actor.Actor> Actors
        {
            get
            {
                return this.actorsByID;
            }
        }

        /// <summary>
        /// The GetActor.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaDB.Actor.Actor"/>.</returns>
        public SagaDB.Actor.Actor GetActor(uint id)
        {
            try
            {
                return this.actorsByID[id];
            }
            catch (Exception ex)
            {
                return (SagaDB.Actor.Actor)null;
            }
        }

        /// <summary>
        /// The GetPC.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="ActorPC"/>.</returns>
        public ActorPC GetPC(string name)
        {
            try
            {
                return this.pcByName[name];
            }
            catch (Exception ex)
            {
                return (ActorPC)null;
            }
        }

        /// <summary>
        /// The GetNewActorID.
        /// </summary>
        /// <param name="type">The type<see cref="ActorType"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        private uint GetNewActorID(ActorType type)
        {
            uint key;
            uint num1;
            int num2;
            switch (type)
            {
                case ActorType.PC:
                    key = SagaMap.Map.nextPcId;
                    num1 = SagaMap.Map.nextPcId;
                    goto label_13;
                case ActorType.MOB:
                    key = this.nextMobId;
                    num1 = this.nextMobId;
                    goto label_12;
                case ActorType.PET:
                    num2 = 0;
                    break;
                default:
                    num2 = type != ActorType.SHADOW ? 1 : 0;
                    break;
            }
            if (num2 == 0)
            {
                key = this.nextPetId;
                num1 = this.nextPetId;
            }
            else if (type == ActorType.EVENT || type == ActorType.FURNITURE)
            {
                key = this.nextEventId;
                num1 = this.nextEventId;
            }
            else if (type == ActorType.GOLEM)
            {
                key = this.nextGolemId;
                num1 = this.nextGolemId;
            }
            else
            {
                key = this.nextItemId;
                num1 = this.nextItemId;
            }
        label_12:
        label_13:
            if (key >= 10000U && type == ActorType.PC)
                key = 16U;
            if (key >= 20000U && type == ActorType.MOB)
                key = 10001U;
            if (key >= 30000U && type == ActorType.PET)
                key = 20001U;
            if (key >= 50000U && type == ActorType.GOLEM)
                key = 40001U;
            if (key >= 60000U && type == ActorType.ITEM)
                key = 50001U;
            if (key >= 70000U && type == ActorType.EVENT)
                key = 60001U;
            if (key >= uint.MaxValue)
                key = 1U;
            while (this.actorsByID.ContainsKey(key))
            {
                ++key;
                if (key >= 10000U && type == ActorType.PC)
                    key = 16U;
                if (key >= 20000U && type == ActorType.MOB)
                    key = 10001U;
                if (key >= 30000U && type == ActorType.PET)
                    key = 20001U;
                if (key >= 50000U && type == ActorType.GOLEM)
                    key = 40001U;
                if (key >= 60000U && type == ActorType.ITEM)
                    key = 50001U;
                if (key >= 70000U && type == ActorType.EVENT)
                    key = 60001U;
                if (key >= uint.MaxValue)
                    key = 1U;
                if ((int)key == (int)num1)
                    return 0;
            }
            int num3;
            switch (type)
            {
                case ActorType.PC:
                    SagaMap.Map.nextPcId = key + 1U;
                    goto label_55;
                case ActorType.MOB:
                    this.nextMobId = key + 1U;
                    goto label_55;
                case ActorType.PET:
                    this.nextPetId = key + 1U;
                    goto label_55;
                case ActorType.FURNITURE:
                    num3 = 0;
                    break;
                default:
                    num3 = type != ActorType.EVENT ? 1 : 0;
                    break;
            }
            if (num3 == 0)
                this.nextEventId = key + 1U;
            else if (type == ActorType.GOLEM)
                this.nextGolemId = key + 1U;
            else
                this.nextItemId = key + 1U;
            label_55:
            return key;
        }

        /// <summary>
        /// The RegisterActor.
        /// </summary>
        /// <param name="nActor">The nActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool RegisterActor(SagaDB.Actor.Actor nActor)
        {
            bool success = false;
            uint newActorId = this.GetNewActorID(nActor.type);
            if (nActor.type == ActorType.ITEM && ((ActorItem)nActor).PossessionItem)
                newActorId += 1000000000U;
            if (newActorId != 0U)
            {
                nActor.ActorID = newActorId;
                nActor.region = this.GetRegion((float)nActor.X, (float)nActor.Y);
                if (this.GetRegionPlayerCount(nActor.region) == 0 && nActor.type == ActorType.PC)
                    this.MobAIToggle(nActor.region, true);
                nActor.invisble = true;
                this.actorsByID.Add(nActor.ActorID, nActor);
                if (nActor.type == ActorType.PC && !this.pcByName.ContainsKey(nActor.Name))
                    this.pcByName.Add(nActor.Name, (ActorPC)nActor);
                if (!this.actorsByRegion.ContainsKey(nActor.region))
                    this.actorsByRegion.Add(nActor.region, new List<SagaDB.Actor.Actor>());
                this.actorsByRegion[nActor.region].Add(nActor);
                success = true;
            }
            nActor.MapID = this.ID;
            if (nActor.type == ActorType.PC)
                ((ActorPC)nActor).Mode = !this.Info.Flag.Test(MapFlags.Wrp) ? PlayerMode.NORMAL : PlayerMode.WRP;
            nActor.e.OnCreate(success);
            return success;
        }

        /// <summary>
        /// The RegisterActor.
        /// </summary>
        /// <param name="nActor">The nActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="SessionID">The SessionID<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool RegisterActor(SagaDB.Actor.Actor nActor, uint SessionID)
        {
            bool success = false;
            uint num = SessionID;
            if (num != 0U)
            {
                nActor.ActorID = num;
                nActor.region = this.GetRegion((float)nActor.X, (float)nActor.Y);
                if (this.GetRegionPlayerCount(nActor.region) == 0 && nActor.type == ActorType.PC)
                    this.MobAIToggle(nActor.region, true);
                nActor.invisble = true;
                if (!this.actorsByID.ContainsKey(nActor.ActorID))
                    this.actorsByID.Add(nActor.ActorID, nActor);
                if (nActor.type == ActorType.PC && !this.pcByName.ContainsKey(nActor.Name))
                    this.pcByName.Add(nActor.Name, (ActorPC)nActor);
                if (!this.actorsByRegion.ContainsKey(nActor.region))
                    this.actorsByRegion.Add(nActor.region, new List<SagaDB.Actor.Actor>());
                this.actorsByRegion[nActor.region].Add(nActor);
                success = true;
            }
            if (nActor.type == ActorType.PC)
            {
                PCEventHandler e = (PCEventHandler)nActor.e;
                if (e.Client.state != MapClient.SESSION_STATE.DISCONNECTED)
                {
                    e.Client.state = MapClient.SESSION_STATE.LOADING;
                }
                else
                {
                    MapServer.charDB.SaveChar((ActorPC)nActor, false, false);
                    MapServer.accountDB.WriteUser(((ActorPC)nActor).Account);
                }
            }
            nActor.MapID = this.ID;
            if (nActor.type == ActorType.PC)
                ((ActorPC)nActor).Mode = !this.Info.Flag.Test(MapFlags.Wrp) ? PlayerMode.NORMAL : PlayerMode.WRP;
            nActor.e.OnCreate(success);
            return success;
        }

        /// <summary>
        /// The OnActorVisibilityChange.
        /// </summary>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorVisibilityChange(SagaDB.Actor.Actor dActor)
        {
            if (dActor.invisble)
            {
                dActor.invisble = false;
                this.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.DISAPPEAR, (MapEventArgs)null, dActor, false);
                dActor.invisble = true;
            }
            else
                this.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.APPEAR, (MapEventArgs)null, dActor, false);
        }

        /// <summary>
        /// The DeleteActor.
        /// </summary>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void DeleteActor(SagaDB.Actor.Actor dActor)
        {
            this.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.DISAPPEAR, (MapEventArgs)null, dActor, false);
            if (dActor.type == ActorType.PC && this.pcByName.ContainsKey(dActor.Name))
                this.pcByName.Remove(dActor.Name);
            this.actorsByID.Remove(dActor.ActorID);
            if (this.actorsByRegion.ContainsKey(dActor.region))
            {
                this.actorsByRegion[dActor.region].Remove(dActor);
                if (this.GetRegionPlayerCount(dActor.region) == 0)
                    this.MobAIToggle(dActor.region, false);
            }
            dActor.e.OnDelete();
            if (!this.IsDungeon || this.DungeonMap.MapType != MapType.End)
                return;
            int num = 0;
            foreach (SagaDB.Actor.Actor actor in this.actorsByID.Values)
            {
                if (actor.type == ActorType.MOB)
                    ++num;
            }
            if (num == 0)
                Factory<DungeonFactory, SagaMap.Dungeon.Dungeon>.Instance.GetDungeon(this.creator.DungeonID).Destory(DestroyType.BossDown);
        }

        /// <summary>
        /// The MoveActor.
        /// </summary>
        /// <param name="mType">The mType<see cref="SagaMap.Map.MOVE_TYPE"/>.</param>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="pos">The pos<see cref="short[]"/>.</param>
        /// <param name="dir">The dir<see cref="ushort"/>.</param>
        /// <param name="speed">The speed<see cref="ushort"/>.</param>
        public void MoveActor(SagaMap.Map.MOVE_TYPE mType, SagaDB.Actor.Actor mActor, short[] pos, ushort dir, ushort speed)
        {
            this.MoveActor(mType, mActor, pos, dir, speed, false);
        }

        /// <summary>
        /// The MoveActor.
        /// </summary>
        /// <param name="mType">The mType<see cref="SagaMap.Map.MOVE_TYPE"/>.</param>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="pos">The pos<see cref="short[]"/>.</param>
        /// <param name="dir">The dir<see cref="ushort"/>.</param>
        /// <param name="speed">The speed<see cref="ushort"/>.</param>
        /// <param name="sendToSelf">The sendToSelf<see cref="bool"/>.</param>
        public void MoveActor(SagaMap.Map.MOVE_TYPE mType, SagaDB.Actor.Actor mActor, short[] pos, ushort dir, ushort speed, bool sendToSelf)
        {
            bool blocked = ClientManager.Blocked;
            if (!blocked)
                ClientManager.EnterCriticalArea();
            try
            {
                bool flag = false;
                if (mActor.Status != null)
                {
                    if (mActor.Status.Additions.ContainsKey("Hiding"))
                    {
                        mActor.Status.Additions["Hiding"].AdditionEnd();
                        mActor.Status.Additions.Remove("Hiding");
                    }
                    if (mActor.Status.Additions.ContainsKey("IAmTree"))
                    {
                        mActor.Status.Additions["IAmTree"].AdditionEnd();
                        mActor.Status.Additions.Remove("IAmTree");
                    }
                }
                if (!this.MoveStepIsInRange(mActor, pos))
                {
                    pos = new short[2] { mActor.X, mActor.Y };
                    dir = (ushort)600;
                    flag = true;
                    sendToSelf = true;
                }
                if (mActor.type == ActorType.PC && !flag)
                {
                    ActorPC pc = (ActorPC)mActor;
                    foreach (ActorPC possesionedActor in pc.PossesionedActors)
                    {
                        if (possesionedActor != pc && (int)possesionedActor.MapID == (int)mActor.MapID)
                            this.MoveActor(mType, (SagaDB.Actor.Actor)possesionedActor, pos, dir, speed);
                    }
                    if (pc.Online)
                    {
                        MapClient mapClient = MapClient.FromActorPC(pc);
                        if ((DateTime.Now - mapClient.moveStamp).TotalSeconds >= 2.0)
                        {
                            if (mapClient.Character.Party != null)
                                Singleton<PartyManager>.Instance.UpdateMemberPosition(mapClient.Character.Party, mapClient.Character);
                            mapClient.moveStamp = DateTime.Now;
                        }
                    }
                }
                if (!flag)
                {
                    for (short index1 = -1; index1 <= (short)1; ++index1)
                    {
                        for (short index2 = -1; index2 <= (short)1; ++index2)
                        {
                            uint key = (uint)((ulong)mActor.region + (ulong)((int)index2 * 10000) + (ulong)index1);
                            if (this.actorsByRegion.ContainsKey(key))
                            {
                                foreach (SagaDB.Actor.Actor actor in this.actorsByRegion[key].ToArray())
                                {
                                    if (((int)actor.ActorID != (int)mActor.ActorID || sendToSelf) && this.actorsByRegion[key].Contains(actor))
                                    {
                                        if (actor.Status == null)
                                        {
                                            this.DeleteActor(actor);
                                        }
                                        else
                                        {
                                            if (this.ACanSeeB(actor, mActor))
                                            {
                                                if (this.ACanSeeB(actor, mActor, (float)pos[0], (float)pos[1]))
                                                {
                                                    if (mType == SagaMap.Map.MOVE_TYPE.START)
                                                        actor.e.OnActorStartsMoving(mActor, pos, dir, speed);
                                                    else
                                                        actor.e.OnActorStopsMoving(mActor, pos, dir, speed);
                                                }
                                                else
                                                    actor.e.OnActorDisappears(mActor);
                                            }
                                            else if (this.ACanSeeB(actor, mActor, (float)pos[0], (float)pos[1]))
                                            {
                                                actor.e.OnActorAppears(mActor);
                                                if (mType == SagaMap.Map.MOVE_TYPE.START)
                                                    actor.e.OnActorStartsMoving(mActor, pos, dir, speed);
                                                else
                                                    actor.e.OnActorStopsMoving(mActor, pos, dir, speed);
                                            }
                                            if (this.ACanSeeB(mActor, actor))
                                            {
                                                if (!this.ACanSeeB(mActor, (float)pos[0], (float)pos[1], actor))
                                                    mActor.e.OnActorDisappears(actor);
                                            }
                                            else if (this.ACanSeeB(mActor, (float)pos[0], (float)pos[1], actor))
                                                mActor.e.OnActorAppears(actor);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    mActor.e.OnActorStopsMoving(mActor, pos, dir, speed);
                mActor.LastX = mActor.X;
                mActor.LastY = mActor.Y;
                mActor.X = pos[0];
                mActor.Y = pos[1];
                if (mActor.type == ActorType.FURNITURE)
                    ((ActorFurniture)mActor).Z = pos[2];
                if (dir <= (ushort)360)
                    mActor.Dir = dir;
                uint region = this.GetRegion((float)pos[0], (float)pos[1]);
                if ((int)mActor.region != (int)region)
                {
                    this.actorsByRegion[mActor.region].Remove(mActor);
                    if (this.GetRegionPlayerCount(mActor.region) == 0)
                        this.MobAIToggle(mActor.region, false);
                    mActor.region = region;
                    if (this.GetRegionPlayerCount(mActor.region) == 0 && mActor.type == ActorType.PC)
                        this.MobAIToggle(mActor.region, true);
                    if (!this.actorsByRegion.ContainsKey(region))
                        this.actorsByRegion.Add(region, new List<SagaDB.Actor.Actor>());
                    this.actorsByRegion[region].Add(mActor);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            if (blocked)
                return;
            ClientManager.LeaveCriticalArea();
        }

        /// <summary>
        /// The GetRegionPlayerCount.
        /// </summary>
        /// <param name="region">The region<see cref="uint"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetRegionPlayerCount(uint region)
        {
            int num = 0;
            if (!this.actorsByRegion.ContainsKey(region))
                return 0;
            List<SagaDB.Actor.Actor> actorList = this.actorsByRegion[region];
            List<int> intList = new List<int>();
            for (int index = 0; index < actorList.Count; ++index)
            {
                if (actorList[index] == null)
                    intList.Add(index);
                else if (actorList[index].type == ActorType.PC)
                    ++num;
            }
            foreach (int index in intList)
                actorList.RemoveAt(index);
            return num;
        }

        /// <summary>
        /// The MobAIToggle.
        /// </summary>
        /// <param name="region">The region<see cref="uint"/>.</param>
        /// <param name="toggle">The toggle<see cref="bool"/>.</param>
        public void MobAIToggle(uint region, bool toggle)
        {
        }

        /// <summary>
        /// The MoveStepIsInRange.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="to">The to<see cref="short[]"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool MoveStepIsInRange(SagaDB.Actor.Actor mActor, short[] to)
        {
            if (mActor.type == ActorType.PC)
            {
                MapClient mapClient = MapClient.FromActorPC((ActorPC)mActor);
                if (mapClient.AI != null && mapClient.AI.Activated)
                    return true;
                TimeSpan timeSpan = DateTime.Now - mapClient.moveCheckStamp;
                double num = timeSpan.TotalMilliseconds <= 1000.0 ? (double)mActor.Speed * (timeSpan.TotalMilliseconds / 1000.0) * 1.5 : (double)mActor.Speed * 1.5;
                if ((double)Math.Abs((int)mActor.X - (int)to[0]) > num || (double)Math.Abs((int)mActor.Y - (int)to[1]) > num)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// The GetRegion.
        /// </summary>
        /// <param name="x">The x<see cref="float"/>.</param>
        /// <param name="y">The y<see cref="float"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetRegion(float x, float y)
        {
            uint num1 = Global.MAX_SIGHT_RANGE * 2U;
            bool flag1 = false;
            bool flag2 = false;
            if ((double)x < 0.0)
            {
                x -= 2f * x;
                flag1 = true;
            }
            if ((double)y < 0.0)
            {
                y -= 2f * y;
                flag2 = true;
            }
            uint num2 = (uint)x;
            uint num3 = (uint)y;
            uint num4 = num2 / num1;
            uint num5 = num3 / num1;
            if (num4 > 4999U)
                num4 = 4999U;
            uint num6 = flag1 ? 5000U - num4 : num4 + 5000U;
            if (num5 > 4999U)
                num5 = 4999U;
            uint num7 = flag2 ? 5000U - num5 : num5 + 5000U;
            return num6 * 10000U + num7;
        }

        /// <summary>
        /// The ACanSeeB.
        /// </summary>
        /// <param name="A">The A<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="B">The B<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ACanSeeB(SagaDB.Actor.Actor A, SagaDB.Actor.Actor B)
        {
            return A != null && B != null && !B.invisble && ((long)Math.Abs((int)A.X - (int)B.X) <= (long)A.sightRange && (long)Math.Abs((int)A.Y - (int)B.Y) <= (long)A.sightRange);
        }

        /// <summary>
        /// The ACanSeeB.
        /// </summary>
        /// <param name="A">The A<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="B">The B<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="bx">The bx<see cref="float"/>.</param>
        /// <param name="by">The by<see cref="float"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ACanSeeB(SagaDB.Actor.Actor A, SagaDB.Actor.Actor B, float bx, float by)
        {
            return A != null && B != null && !B.invisble && ((double)Math.Abs((float)A.X - bx) <= (double)A.sightRange && (double)Math.Abs((float)A.Y - by) <= (double)A.sightRange);
        }

        /// <summary>
        /// The ACanSeeB.
        /// </summary>
        /// <param name="A">The A<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="ax">The ax<see cref="float"/>.</param>
        /// <param name="ay">The ay<see cref="float"/>.</param>
        /// <param name="B">The B<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ACanSeeB(SagaDB.Actor.Actor A, float ax, float ay, SagaDB.Actor.Actor B)
        {
            return A != null && B != null && !B.invisble && ((double)Math.Abs(ax - (float)B.X) <= (double)A.sightRange && (double)Math.Abs(ay - (float)B.Y) <= (double)A.sightRange);
        }

        /// <summary>
        /// The ACanSeeB.
        /// </summary>
        /// <param name="A">The A<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="B">The B<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="sightrange">The sightrange<see cref="float"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ACanSeeB(SagaDB.Actor.Actor A, SagaDB.Actor.Actor B, float sightrange)
        {
            return A != null && B != null && !B.invisble && ((double)Math.Abs((int)A.X - (int)B.X) <= (double)sightrange && (double)Math.Abs((int)A.Y - (int)B.Y) <= (double)sightrange);
        }

        /// <summary>
        /// The SendVisibleActorsToActor.
        /// </summary>
        /// <param name="jActor">The jActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void SendVisibleActorsToActor(SagaDB.Actor.Actor jActor)
        {
            for (short index1 = -1; index1 <= (short)1; ++index1)
            {
                for (short index2 = -1; index2 <= (short)1; ++index2)
                {
                    uint key = (uint)((ulong)jActor.region + (ulong)((int)index2 * 10000) + (ulong)index1);
                    if (this.actorsByRegion.ContainsKey(key))
                    {
                        foreach (SagaDB.Actor.Actor actor in this.actorsByRegion[key].ToArray())
                        {
                            try
                            {
                                if ((int)actor.ActorID != (int)jActor.ActorID)
                                {
                                    if (actor.Status == null)
                                        this.DeleteActor(actor);
                                    else if (this.ACanSeeB(jActor, actor))
                                        jActor.e.OnActorAppears(actor);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowError(ex);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// The TeleportActor.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        public void TeleportActor(SagaDB.Actor.Actor sActor, short x, short y)
        {
            if (sActor.Status.Additions.ContainsKey("Hiding"))
            {
                sActor.Status.Additions["Hiding"].AdditionEnd();
                sActor.Status.Additions.Remove("Hiding");
            }
            if (sActor.Status.Additions.ContainsKey("Cloaking"))
            {
                sActor.Status.Additions["Cloaking"].AdditionEnd();
                sActor.Status.Additions.Remove("Cloaking");
            }
            if (sActor.Status.Additions.ContainsKey("IAmTree"))
            {
                sActor.Status.Additions["IAmTree"].AdditionEnd();
                sActor.Status.Additions.Remove("IAmTree");
            }
            if (sActor.Status.Additions.ContainsKey("Invisible"))
            {
                sActor.Status.Additions["Invisible"].AdditionEnd();
                sActor.Status.Additions.Remove("Invisible");
            }
            this.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.DISAPPEAR, (MapEventArgs)null, sActor, false);
            this.actorsByRegion[sActor.region].Remove(sActor);
            if (this.GetRegionPlayerCount(sActor.region) == 0)
                this.MobAIToggle(sActor.region, false);
            sActor.X = x;
            sActor.Y = y;
            sActor.region = this.GetRegion((float)x, (float)y);
            if (this.GetRegionPlayerCount(sActor.region) == 0 && sActor.type == ActorType.PC)
                this.MobAIToggle(sActor.region, true);
            if (!this.actorsByRegion.ContainsKey(sActor.region))
                this.actorsByRegion.Add(sActor.region, new List<SagaDB.Actor.Actor>());
            this.actorsByRegion[sActor.region].Add(sActor);
            sActor.e.OnTeleport(x, y);
            this.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.APPEAR, (MapEventArgs)null, sActor, false);
            this.SendVisibleActorsToActor(sActor);
        }

        /// <summary>
        /// The SendEventToAllActorsWhoCanSeeActor.
        /// </summary>
        /// <param name="etype">The etype<see cref="SagaMap.Map.EVENT_TYPE"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="sendToSourceActor">The sendToSourceActor<see cref="bool"/>.</param>
        public void SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE etype, MapEventArgs args, SagaDB.Actor.Actor sActor, bool sendToSourceActor)
        {
            try
            {
                for (short index1 = -1; index1 <= (short)1; ++index1)
                {
                    for (short index2 = -1; index2 <= (short)1; ++index2)
                    {
                        uint key = (uint)((ulong)sActor.region + (ulong)((int)index2 * 10000) + (ulong)index1);
                        if (this.actorsByRegion.ContainsKey(key))
                        {
                            foreach (SagaDB.Actor.Actor actor in this.actorsByRegion[key].ToArray())
                            {
                                try
                                {
                                    if (sendToSourceActor || (int)actor.ActorID != (int)sActor.ActorID)
                                    {
                                        if (actor.Status == null)
                                        {
                                            if (etype != SagaMap.Map.EVENT_TYPE.DISAPPEAR)
                                                this.DeleteActor(actor);
                                        }
                                        else if (this.ACanSeeB(actor, sActor))
                                        {
                                            switch (etype)
                                            {
                                                case SagaMap.Map.EVENT_TYPE.APPEAR:
                                                    actor.e.OnActorAppears(sActor);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.DISAPPEAR:
                                                    actor.e.OnActorDisappears(sActor);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.MOTION:
                                                    actor.e.OnActorChangeMotion(sActor, args);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.EMOTION:
                                                    actor.e.OnActorChangeEmotion(sActor, args);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.CHAT:
                                                    actor.e.OnActorChat(sActor, args);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.SKILL:
                                                    actor.e.OnActorSkillUse(sActor, args);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.CHANGE_EQUIP:
                                                    actor.e.OnActorChangeEquip(sActor, args);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.CHANGE_STATUS:
                                                    if (sActor.type == ActorType.PC)
                                                    {
                                                        actor.e.OnPlayerChangeStatus((ActorPC)sActor);
                                                        break;
                                                    }
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.BUFF_CHANGE:
                                                    actor.e.OnActorChangeBuff(sActor);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.CHAR_INFO_UPDATE:
                                                    actor.e.OnCharInfoUpdate(sActor);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.PLAYER_SIZE_UPDATE:
                                                    actor.e.OnPlayerSizeChange(sActor);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.ATTACK:
                                                    actor.e.OnAttack(sActor, args);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.HPMPSP_UPDATE:
                                                    actor.e.OnHPMPSPUpdate(sActor);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.LEVEL_UP:
                                                    actor.e.OnLevelUp(sActor, args);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.PLAYER_MODE:
                                                    actor.e.OnPlayerMode(sActor);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.SHOW_EFFECT:
                                                    actor.e.OnShowEffect(sActor, args);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.POSSESSION:
                                                    actor.e.OnActorPossession(sActor, args);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.PARTY_NAME_UPDATE:
                                                    if (sActor.type == ActorType.PC)
                                                    {
                                                        actor.e.OnActorPartyUpdate((ActorPC)sActor);
                                                        break;
                                                    }
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.SPEED_UPDATE:
                                                    actor.e.OnActorSpeedChange(sActor);
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.SIGN_UPDATE:
                                                    if (sActor.type == ActorType.PC || sActor.type == ActorType.EVENT)
                                                    {
                                                        actor.e.OnSignUpdate(sActor);
                                                        break;
                                                    }
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.RING_NAME_UPDATE:
                                                    if (sActor.type == ActorType.PC)
                                                    {
                                                        actor.e.OnActorRingUpdate((ActorPC)sActor);
                                                        break;
                                                    }
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.WRP_RANKING_UPDATE:
                                                    if (sActor.type == ActorType.PC)
                                                    {
                                                        actor.e.OnActorWRPRankingUpdate((ActorPC)sActor);
                                                        break;
                                                    }
                                                    break;
                                                case SagaMap.Map.EVENT_TYPE.ATTACK_TYPE_CHANGE:
                                                    if (sActor.type == ActorType.PC)
                                                    {
                                                        actor.e.OnActorChangeAttackType((ActorPC)sActor);
                                                        break;
                                                    }
                                                    break;
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.ShowError(ex);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        /// <summary>
        /// The SendEventToAllActors.
        /// </summary>
        /// <param name="etype">The etype<see cref="SagaMap.Map.TOALL_EVENT_TYPE"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="sendToSourceActor">The sendToSourceActor<see cref="bool"/>.</param>
        public void SendEventToAllActors(SagaMap.Map.TOALL_EVENT_TYPE etype, MapEventArgs args, SagaDB.Actor.Actor sActor, bool sendToSourceActor)
        {
            foreach (SagaDB.Actor.Actor actor in this.actorsByID.Values)
            {
                if ((sActor == null || (sendToSourceActor || (int)actor.ActorID != (int)sActor.ActorID)) && etype == SagaMap.Map.TOALL_EVENT_TYPE.CHAT)
                    actor.e.OnActorChat(sActor, args);
            }
        }

        /// <summary>
        /// The SendActorToMap.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="newMap">The newMap<see cref="SagaMap.Map"/>.</param>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        public void SendActorToMap(SagaDB.Actor.Actor mActor, SagaMap.Map newMap, short x, short y)
        {
            this.SendActorToMap(mActor, newMap, x, y, false);
        }

        /// <summary>
        /// The SendActorToMap.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="newMap">The newMap<see cref="SagaMap.Map"/>.</param>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <param name="possession">The possession<see cref="bool"/>.</param>
        public void SendActorToMap(SagaDB.Actor.Actor mActor, SagaMap.Map newMap, short x, short y, bool possession)
        {
            if (mActor.Status.Additions.ContainsKey("Hiding"))
            {
                mActor.Status.Additions["Hiding"].AdditionEnd();
                mActor.Status.Additions.Remove("Hiding");
            }
            if (mActor.Status.Additions.ContainsKey("Cloaking"))
            {
                mActor.Status.Additions["Cloaking"].AdditionEnd();
                mActor.Status.Additions.Remove("Cloaking");
            }
            if (mActor.Status.Additions.ContainsKey("IAmTree"))
            {
                mActor.Status.Additions["IAmTree"].AdditionEnd();
                mActor.Status.Additions.Remove("IAmTree");
            }
            if (mActor.Status.Additions.ContainsKey("Invisible"))
            {
                mActor.Status.Additions["Invisible"].AdditionEnd();
                mActor.Status.Additions.Remove("Invisible");
            }
            if (mActor.type == ActorType.PC)
            {
                ActorPC actorPc = (ActorPC)mActor;
                if (actorPc.PossessionTarget != 0U && !possession)
                    return;
                foreach (ActorPC possesionedActor in actorPc.PossesionedActors)
                {
                    if (possesionedActor != actorPc)
                        this.SendActorToMap((SagaDB.Actor.Actor)possesionedActor, newMap, x, y);
                }
            }
            byte id = (byte)newMap.id;
            if ((int)id == (int)mActor.MapID)
            {
                this.TeleportActor(mActor, x, y);
            }
            else
            {
                this.DeleteActor(mActor);
                mActor.MapID = (uint)id;
                mActor.X = x;
                mActor.Y = y;
                mActor.Buff.Dead = false;
                if (mActor.type != ActorType.PC)
                {
                    newMap.RegisterActor(mActor);
                }
                else
                {
                    ((ActorPC)mActor).Motion = MotionType.STAND;
                    newMap.RegisterActor(mActor, mActor.ActorID);
                    MapClient mapClient = MapClient.FromActorPC((ActorPC)mActor);
                    if (mapClient.AI != null)
                        mapClient.AI.map = newMap;
                }
            }
        }

        /// <summary>
        /// The SendActorToMap.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="mapid">The mapid<see cref="uint"/>.</param>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        public void SendActorToMap(SagaDB.Actor.Actor mActor, uint mapid, short x, short y)
        {
            this.SendActorToMap(mActor, mapid, x, y, false);
        }

        /// <summary>
        /// The SendActorToMap.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="mapid">The mapid<see cref="uint"/>.</param>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <param name="possession">The possession<see cref="bool"/>.</param>
        public void SendActorToMap(SagaDB.Actor.Actor mActor, uint mapid, short x, short y, bool possession)
        {
            if (mActor.Status.Additions.ContainsKey("Hiding"))
            {
                mActor.Status.Additions["Hiding"].AdditionEnd();
                mActor.Status.Additions.Remove("Hiding");
            }
            if (mActor.Status.Additions.ContainsKey("Cloaking"))
            {
                mActor.Status.Additions["Cloaking"].AdditionEnd();
                mActor.Status.Additions.Remove("Cloaking");
            }
            if (mActor.Status.Additions.ContainsKey("IAmTree"))
            {
                mActor.Status.Additions["IAmTree"].AdditionEnd();
                mActor.Status.Additions.Remove("IAmTree");
            }
            if (mActor.Status.Additions.ContainsKey("Invisible"))
            {
                mActor.Status.Additions["Invisible"].AdditionEnd();
                mActor.Status.Additions.Remove("Invisible");
            }
            if (mActor.type == ActorType.PC)
            {
                ActorPC actorPc = (ActorPC)mActor;
                if (actorPc.PossessionTarget != 0U && !possession)
                    return;
                foreach (ActorPC possesionedActor in actorPc.PossesionedActors)
                {
                    if (possesionedActor != actorPc)
                        this.SendActorToMap((SagaDB.Actor.Actor)possesionedActor, mapid, x, y, true);
                }
            }
            if ((int)mapid == (int)mActor.MapID)
            {
                this.TeleportActor(mActor, x, y);
            }
            else
            {
                SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(mapid);
                if (map == null)
                    return;
                this.DeleteActor(mActor);
                if ((double)x == 0.0 && (double)y == 0.0)
                {
                    short[] randomPos = map.GetRandomPos();
                    x = randomPos[0];
                    y = randomPos[1];
                }
                mActor.MapID = mapid;
                mActor.X = x;
                mActor.Y = y;
                mActor.Buff.Dead = false;
                if (mActor.type != ActorType.PC)
                {
                    map.RegisterActor(mActor);
                }
                else
                {
                    ((ActorPC)mActor).Motion = MotionType.STAND;
                    map.RegisterActor(mActor, mActor.ActorID);
                    MapClient mapClient = MapClient.FromActorPC((ActorPC)mActor);
                    if (mapClient.AI != null)
                        mapClient.AI.map = map;
                }
            }
        }

        /// <summary>
        /// The SendActorToActor.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="tActor">The tActor<see cref="SagaDB.Actor.Actor"/>.</param>
        private void SendActorToActor(SagaDB.Actor.Actor mActor, SagaDB.Actor.Actor tActor)
        {
            if ((int)mActor.MapID == (int)tActor.MapID)
                this.TeleportActor(mActor, tActor.X, tActor.Y);
            else
                this.SendActorToMap(mActor, tActor.MapID, tActor.X, tActor.Y);
        }

        /// <summary>
        /// The AddItemDrop.
        /// </summary>
        /// <param name="itemID">The itemID<see cref="uint"/>.</param>
        /// <param name="treasureGroup">The treasureGroup<see cref="string"/>.</param>
        /// <param name="ori">The ori<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="party">The party<see cref="bool"/>.</param>
        public void AddItemDrop(uint itemID, string treasureGroup, SagaDB.Actor.Actor ori, bool party)
        {
            SagaDB.Actor.Actor actor1 = (SagaDB.Actor.Actor)null;
            if (ori.type == ActorType.MOB)
            {
                MobEventHandler e = (MobEventHandler)ori.e;
                if (e.AI.firstAttacker != null)
                    actor1 = e.AI.firstAttacker;
            }
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            if (actor1 == null || actor1.type != ActorType.PC)
                return;
            ActorPC actorPc1 = (ActorPC)actor1;
            if (actorPc1.Party != null && party)
            {
                foreach (ActorPC actorPc2 in actorPc1.Party.Members.Values)
                {
                    if (actorPc2.Online && (int)actorPc2.MapID == (int)ori.MapID)
                        actorList.Add((SagaDB.Actor.Actor)actorPc2);
                }
            }
            else
                actorList.Add(actor1);
            foreach (SagaDB.Actor.Actor actor2 in actorList)
            {
                SagaDB.Item.Item obj = (SagaDB.Item.Item)null;
                if (itemID != 0U)
                    obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(itemID, true);
                if (treasureGroup != null)
                {
                    if (FactoryString<TreasureFactory, TreasureList>.Instance.Items.ContainsKey(treasureGroup))
                    {
                        TreasureItem randomItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem(treasureGroup);
                        obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(randomItem.ID, true);
                        obj.Stack = (ushort)randomItem.Count;
                    }
                    else
                        obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(itemID, true);
                }
                ActorItem actorItem = new ActorItem(obj);
                actorItem.e = (ActorEventHandler)new ItemEventHandler((SagaDB.Actor.Actor)actorItem);
                actorItem.Owner = actor2;
                actorItem.Party = party;
                actorItem.MapID = this.ID;
                short[] numArray;
                if (party)
                    numArray = this.GetRandomPosAroundActor(ori);
                else
                    numArray = new short[2] { ori.X, ori.Y };
                actorItem.X = numArray[0];
                actorItem.Y = numArray[1];
                this.RegisterActor((SagaDB.Actor.Actor)actorItem);
                actorItem.invisble = false;
                this.OnActorVisibilityChange((SagaDB.Actor.Actor)actorItem);
                if (party)
                    this.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
                    {
                        actorID = uint.MaxValue,
                        effectID = 7116U,
                        x = Global.PosX16to8(numArray[0], this.width),
                        y = Global.PosY16to8(numArray[1], this.height),
                        oneTime = false
                    }, ori, false);
                DeleteItem deleteItem = new DeleteItem(actorItem);
                deleteItem.Activate();
                actorItem.Tasks.Add("DeleteItem", (MultiRunTask)deleteItem);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsMapInstance.
        /// </summary>
        public bool IsMapInstance
        {
            get
            {
                return this.instance;
            }
            set
            {
                this.instance = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsDungeon.
        /// </summary>
        public bool IsDungeon
        {
            get
            {
                return this.dungeon;
            }
            set
            {
                this.dungeon = value;
            }
        }

        /// <summary>
        /// Gets or sets the DungeonMap.
        /// </summary>
        public DungeonMap DungeonMap
        {
            get
            {
                return this.dungeonMap;
            }
            set
            {
                this.dungeonMap = value;
            }
        }

        /// <summary>
        /// Gets or sets the ClientExitMap.
        /// </summary>
        public uint ClientExitMap
        {
            get
            {
                return this.clientExitMap;
            }
            set
            {
                this.clientExitMap = value;
            }
        }

        /// <summary>
        /// Gets or sets the ClientExitX.
        /// </summary>
        public byte ClientExitX
        {
            get
            {
                return this.clientExitX;
            }
            set
            {
                this.clientExitX = value;
            }
        }

        /// <summary>
        /// Gets or sets the ClientExitY.
        /// </summary>
        public byte ClientExitY
        {
            get
            {
                return this.clientExitY;
            }
            set
            {
                this.clientExitY = value;
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
        /// The OnDestrory.
        /// </summary>
        public void OnDestrory()
        {
            List<SagaDB.Actor.Actor> actorList1 = new List<SagaDB.Actor.Actor>();
            List<SagaDB.Actor.Actor> actorList2 = new List<SagaDB.Actor.Actor>();
            List<SagaDB.Actor.Actor> actorList3 = new List<SagaDB.Actor.Actor>();
            if (Singleton<MobSpawnManager>.Instance.Spawns.ContainsKey(this.id))
            {
                foreach (ActorMob actorMob in Singleton<MobSpawnManager>.Instance.Spawns[this.id])
                {
                    Singleton<AIThread>.Instance.RemoveAI(((MobEventHandler)actorMob.e).AI);
                    foreach (MultiRunTask multiRunTask in actorMob.Tasks.Values)
                        multiRunTask.Deactivate();
                    actorMob.Tasks.Clear();
                }
                Singleton<MobSpawnManager>.Instance.Spawns[this.id].Clear();
                Singleton<MobSpawnManager>.Instance.Spawns.Remove(this.id);
            }
            foreach (SagaDB.Actor.Actor actor in this.actorsByID.Values)
            {
                if (actor.type == ActorType.PC)
                    actorList1.Add(actor);
                else if (actor.type == ActorType.ITEM)
                    actorList2.Add(actor);
                else if (actor.type == ActorType.GOLEM)
                {
                    try
                    {
                        ActorGolem actorGolem = (ActorGolem)actor;
                        MapServer.charDB.SaveChar(actorGolem.Owner, false);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                }
                else
                    actorList3.Add(actor);
            }
            foreach (SagaDB.Actor.Actor dActor in actorList3)
            {
                dActor.ClearTaskAddition();
                this.DeleteActor(dActor);
            }
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(this.clientExitMap);
            foreach (SagaDB.Actor.Actor dActor in actorList2)
            {
                ActorItem actorItem = (ActorItem)dActor;
                if (actorItem.Item.PossessionedActor != null)
                {
                    CSMG_POSSESSION_CANCEL p = new CSMG_POSSESSION_CANCEL();
                    p.PossessionPosition = PossessionPosition.NONE;
                    PCEventHandler e = (PCEventHandler)actorItem.Item.PossessionedActor.e;
                    ActorPC possessionedActor = actorItem.Item.PossessionedActor;
                    if (possessionedActor.Online)
                        e.Client.OnPossessionCancel(p);
                    else
                        possessionedActor.PossessionTarget = 0U;
                    e.Client.map.SendActorToMap((SagaDB.Actor.Actor)possessionedActor, this.clientExitMap, Global.PosX8to16(this.clientExitX, map.Width), Global.PosY8to16(this.clientExitY, map.Height));
                    if (!possessionedActor.Online)
                    {
                        MapServer.charDB.SaveChar(possessionedActor, false, false);
                        MapServer.accountDB.WriteUser(possessionedActor.Account);
                        Singleton<MapManager>.Instance.GetMap(possessionedActor.MapID).DeleteActor((SagaDB.Actor.Actor)possessionedActor);
                        MapClient.FromActorPC(possessionedActor).DisposeActor();
                        possessionedActor.Account = (Account)null;
                    }
                    if (actorList1.Contains((SagaDB.Actor.Actor)possessionedActor))
                        actorList1.Remove((SagaDB.Actor.Actor)possessionedActor);
                }
                dActor.Speed = (ushort)420;
                dActor.ClearTaskAddition();
                this.DeleteActor(dActor);
            }
            foreach (SagaDB.Actor.Actor mActor in actorList1)
            {
                mActor.Speed = (ushort)420;
                try
                {
                    this.SendActorToMap(mActor, this.clientExitMap, Global.PosX8to16(this.clientExitX, map.Width), Global.PosY8to16(this.clientExitY, map.Height));
                }
                catch
                {
                }
            }
        }

        /// <summary>
        /// Defines the MOVE_TYPE.
        /// </summary>
        public enum MOVE_TYPE
        {
            /// <summary>
            /// Defines the START.
            /// </summary>
            START,

            /// <summary>
            /// Defines the STOP.
            /// </summary>
            STOP,
        }

        /// <summary>
        /// Defines the EVENT_TYPE.
        /// </summary>
        public enum EVENT_TYPE
        {
            /// <summary>
            /// Defines the APPEAR.
            /// </summary>
            APPEAR,

            /// <summary>
            /// Defines the DISAPPEAR.
            /// </summary>
            DISAPPEAR,

            /// <summary>
            /// Defines the MOTION.
            /// </summary>
            MOTION,

            /// <summary>
            /// Defines the EMOTION.
            /// </summary>
            EMOTION,

            /// <summary>
            /// Defines the CHAT.
            /// </summary>
            CHAT,

            /// <summary>
            /// Defines the SKILL.
            /// </summary>
            SKILL,

            /// <summary>
            /// Defines the CHANGE_EQUIP.
            /// </summary>
            CHANGE_EQUIP,

            /// <summary>
            /// Defines the CHANGE_STATUS.
            /// </summary>
            CHANGE_STATUS,

            /// <summary>
            /// Defines the BUFF_CHANGE.
            /// </summary>
            BUFF_CHANGE,

            /// <summary>
            /// Defines the ACTOR_SELECTION.
            /// </summary>
            ACTOR_SELECTION,

            /// <summary>
            /// Defines the YAW_UPDATE.
            /// </summary>
            YAW_UPDATE,

            /// <summary>
            /// Defines the CHAR_INFO_UPDATE.
            /// </summary>
            CHAR_INFO_UPDATE,

            /// <summary>
            /// Defines the PLAYER_SIZE_UPDATE.
            /// </summary>
            PLAYER_SIZE_UPDATE,

            /// <summary>
            /// Defines the ATTACK.
            /// </summary>
            ATTACK,

            /// <summary>
            /// Defines the HPMPSP_UPDATE.
            /// </summary>
            HPMPSP_UPDATE,

            /// <summary>
            /// Defines the LEVEL_UP.
            /// </summary>
            LEVEL_UP,

            /// <summary>
            /// Defines the PLAYER_MODE.
            /// </summary>
            PLAYER_MODE,

            /// <summary>
            /// Defines the SHOW_EFFECT.
            /// </summary>
            SHOW_EFFECT,

            /// <summary>
            /// Defines the POSSESSION.
            /// </summary>
            POSSESSION,

            /// <summary>
            /// Defines the PARTY_NAME_UPDATE.
            /// </summary>
            PARTY_NAME_UPDATE,

            /// <summary>
            /// Defines the SPEED_UPDATE.
            /// </summary>
            SPEED_UPDATE,

            /// <summary>
            /// Defines the SIGN_UPDATE.
            /// </summary>
            SIGN_UPDATE,

            /// <summary>
            /// Defines the RING_NAME_UPDATE.
            /// </summary>
            RING_NAME_UPDATE,

            /// <summary>
            /// Defines the WRP_RANKING_UPDATE.
            /// </summary>
            WRP_RANKING_UPDATE,

            /// <summary>
            /// Defines the ATTACK_TYPE_CHANGE.
            /// </summary>
            ATTACK_TYPE_CHANGE,
        }

        /// <summary>
        /// Defines the TOALL_EVENT_TYPE.
        /// </summary>
        public enum TOALL_EVENT_TYPE
        {
            /// <summary>
            /// Defines the CHAT.
            /// </summary>
            CHAT,
        }
    }
}
