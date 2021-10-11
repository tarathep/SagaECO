namespace SagaMap.Mob
{
    using SagaDB.Actor;
    using SagaDB.Skill;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Mob.AICommands;
    using SagaMap.Skill;
    using SagaMap.Tasks.Mob;
    using SagaMap.Tasks.Skill;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MobAI" />.
    /// </summary>
    public class MobAI
    {
        /// <summary>
        /// Defines the aiActivity.
        /// </summary>
        private Activity aiActivity = Activity.LAZY;

        /// <summary>
        /// Defines the Activated.
        /// </summary>
        public bool Activated = false;

        /// <summary>
        /// Defines the commands.
        /// </summary>
        public Dictionary<string, AICommand> commands = new Dictionary<string, AICommand>();

        /// <summary>
        /// Defines the openedNode.
        /// </summary>
        private Dictionary<int, MapNode> openedNode = new Dictionary<int, MapNode>();

        /// <summary>
        /// Defines the Hate.
        /// </summary>
        public Dictionary<uint, uint> Hate = new Dictionary<uint, uint>();

        /// <summary>
        /// Defines the NextUpdateTime.
        /// </summary>
        public DateTime NextUpdateTime = DateTime.Now;

        /// <summary>
        /// Defines the DamageTable.
        /// </summary>
        public Dictionary<uint, int> DamageTable = new Dictionary<uint, int>();

        /// <summary>
        /// Defines the attackStamp.
        /// </summary>
        public DateTime attackStamp = DateTime.Now;

        /// <summary>
        /// Defines the lastSkillCast.
        /// </summary>
        private DateTime lastSkillCast = DateTime.Now;

        /// <summary>
        /// Defines the actor.
        /// </summary>
        private SagaDB.Actor.Actor actor;

        /// <summary>
        /// Defines the map.
        /// </summary>
        public Map map;

        /// <summary>
        /// Defines the MoveRange.
        /// </summary>
        public short MoveRange;

        /// <summary>
        /// Defines the X_Ori.
        /// </summary>
        public short X_Ori;

        /// <summary>
        /// Defines the Y_Ori.
        /// </summary>
        public short Y_Ori;

        /// <summary>
        /// Defines the X_Spawn.
        /// </summary>
        public short X_Spawn;

        /// <summary>
        /// Defines the Y_Spawn.
        /// </summary>
        public short Y_Spawn;

        /// <summary>
        /// Defines the SpawnDelay.
        /// </summary>
        public int SpawnDelay;

        /// <summary>
        /// Defines the mode.
        /// </summary>
        private AIMode mode;

        /// <summary>
        /// Defines the period.
        /// </summary>
        public int period;

        /// <summary>
        /// Defines the firstAttacker.
        /// </summary>
        public SagaDB.Actor.Actor firstAttacker;

        /// <summary>
        /// Defines the master.
        /// </summary>
        private SagaDB.Actor.Actor master;

        /// <summary>
        /// Defines the lastAttacker.
        /// </summary>
        public SagaDB.Actor.Actor lastAttacker;

        /// <summary>
        /// Gets or sets the Mode.
        /// </summary>
        public AIMode Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
            }
        }

        /// <summary>
        /// Gets or sets the Master.
        /// </summary>
        public SagaDB.Actor.Actor Master
        {
            get
            {
                return this.master;
            }
            set
            {
                this.master = value;
            }
        }

        /// <summary>
        /// Gets or sets the AIActivity.
        /// </summary>
        public Activity AIActivity
        {
            get
            {
                return this.aiActivity;
            }
            set
            {
                this.aiActivity = value;
                if (this.Mob.Speed == (ushort)0)
                    return;
                switch (value)
                {
                    case Activity.IDLE:
                        this.period = 1000;
                        break;
                    case Activity.LAZY:
                        this.period = 200000 / (int)this.Mob.Speed;
                        break;
                    case Activity.BUSY:
                        this.period = 100000 / (int)this.Mob.Speed;
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the Mob.
        /// </summary>
        public SagaDB.Actor.Actor Mob
        {
            get
            {
                return this.actor;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanMove.
        /// </summary>
        public bool CanMove
        {
            get
            {
                return !this.Mode.NoMove && !this.Mob.Buff.CannotMove && (!this.Mob.Buff.Stun && !this.Mob.Buff.Stone) && (!this.Mob.Buff.Frosen && !this.Mob.Buff.硬直) && !this.Mob.Tasks.ContainsKey("SkillCast");
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanAttack.
        /// </summary>
        public bool CanAttack
        {
            get
            {
                return !this.Mode.NoAttack && !this.Mob.Buff.Stone && !this.Mob.Buff.Stun && !this.Mob.Tasks.ContainsKey("SkillCast");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobAI"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="idle">The idle<see cref="bool"/>.</param>
        public MobAI(SagaDB.Actor.Actor mob, bool idle)
        {
            this.period = 1000;
            this.actor = mob;
            this.map = Singleton<MapManager>.Instance.GetMap(mob.MapID);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobAI"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="SagaDB.Actor.Actor"/>.</param>
        public MobAI(SagaDB.Actor.Actor mob)
        {
            this.period = 1000;
            this.actor = mob;
            this.map = Singleton<MapManager>.Instance.GetMap(mob.MapID);
            this.commands.Add("Attack", (AICommand)new Attack(this));
        }

        /// <summary>
        /// The Start.
        /// </summary>
        public void Start()
        {
            Singleton<AIThread>.Instance.RegisterAI(this);
            this.Hate.Clear();
            this.commands = new Dictionary<string, AICommand>();
            this.commands.Add("Attack", (AICommand)new Attack(this));
            this.AIActivity = Activity.LAZY;
            this.Activated = true;
        }

        /// <summary>
        /// The Pause.
        /// </summary>
        public void Pause()
        {
            try
            {
                foreach (string key in this.commands.Keys)
                    this.commands[key].Dispose();
                this.commands.Clear();
                this.Mob.VisibleActors.Clear();
                this.Mob.Status.attackingActors.Clear();
                this.lastAttacker = (SagaDB.Actor.Actor)null;
                Singleton<AIThread>.Instance.RemoveAI(this);
                this.Activated = false;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, (Logger)null);
            }
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public void CallBack(object o)
        {
            List<string> stringList = new List<string>();
            try
            {
                if (this.actor.Buff.Dead)
                    return;
                if (this.master != null)
                {
                    if ((int)this.master.MapID != (int)this.Mob.MapID)
                    {
                        this.Mob.e.OnDie();
                        return;
                    }
                    if (this.master.type == ActorType.PC && !((ActorPC)this.master).Online)
                    {
                        this.Mob.e.OnDie();
                        return;
                    }
                }
                if (this.commands.Count == 1 && this.commands.ContainsKey("Attack") && this.Hate.Count == 0)
                {
                    this.AIActivity = Activity.IDLE;
                    if (Global.Random.Next(0, 99) < 10)
                    {
                        this.AIActivity = Activity.LAZY;
                        if ((Math.Abs((int)this.Mob.X - (int)this.X_Spawn) > 1000 || Math.Abs((int)this.Mob.Y - (int)this.Y_Spawn) > 1000) && this.MoveRange != (short)0)
                        {
                            double lengthD = MobAI.GetLengthD(this.X_Spawn, this.Y_Spawn, this.Mob.X, this.Mob.Y);
                            this.commands.Add("Move", (AICommand)new Move(this, (short)((double)this.Mob.X + (double)((int)this.X_Spawn - (int)this.Mob.X) / lengthD * (double)this.Mob.Speed), (short)((double)this.Mob.Y + (double)((int)this.Y_Spawn - (int)this.Mob.Y) / lengthD * (double)this.Mob.Speed)));
                        }
                        else
                        {
                            int num1 = 0;
                            double num2;
                            double num3;
                            byte num4;
                            byte num5;
                            do
                            {
                                double num6 = (double)Global.Random.Next(-100, 100);
                                double num7 = (double)Global.Random.Next(-100, 100);
                                double lengthD = MobAI.GetLengthD((short)0, (short)0, (short)num6, (short)num7);
                                double num8 = num6 / lengthD * 500.0;
                                double num9 = num7 / lengthD * 500.0;
                                num2 = num8 + (double)this.Mob.X;
                                num3 = num9 + (double)this.Mob.Y;
                                num4 = Global.PosX16to8((short)num2, this.map.Width);
                                num5 = Global.PosY16to8((short)num3, this.map.Height);
                                if ((int)num4 >= (int)this.map.Width)
                                    num4 = (byte)((uint)this.map.Width - 1U);
                                if ((int)num5 >= (int)this.map.Height)
                                    num5 = (byte)((uint)this.map.Height - 1U);
                                ++num1;
                            }
                            while (this.map.Info.walkable[(int)num4, (int)num5] != (byte)2 && num1 < 1000);
                            this.commands.Add("Move", (AICommand)new Move(this, (short)num2, (short)num3));
                        }
                    }
                }
                string[] array = new string[this.commands.Count];
                this.commands.Keys.CopyTo(array, 0);
                int count = this.commands.Count;
                for (int index = 0; index < count; ++index)
                {
                    try
                    {
                        string key = array[index];
                        AICommand aiCommand;
                        this.commands.TryGetValue(key, out aiCommand);
                        if (aiCommand != null)
                        {
                            if (aiCommand.Status != CommandStatus.FINISHED && aiCommand.Status != CommandStatus.DELETING && aiCommand.Status != CommandStatus.PAUSED)
                                aiCommand.Update((object)null);
                            if (aiCommand.Status == CommandStatus.FINISHED)
                            {
                                stringList.Add(key);
                                aiCommand.Status = CommandStatus.DELETING;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
                foreach (string key in stringList)
                    this.commands.Remove(key);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, (Logger)null);
                Logger.ShowError(ex.StackTrace, (Logger)null);
            }
        }

        /// <summary>
        /// The FindPath.
        /// </summary>
        /// <param name="x">The x<see cref="byte"/>.</param>
        /// <param name="y">The y<see cref="byte"/>.</param>
        /// <param name="x2">The x2<see cref="byte"/>.</param>
        /// <param name="y2">The y2<see cref="byte"/>.</param>
        /// <returns>The <see cref="List{MapNode}"/>.</returns>
        public List<MapNode> FindPath(byte x, byte y, byte x2, byte y2)
        {
            MapNode node1 = new MapNode();
            DateTime now = DateTime.Now;
            int num = 0;
            node1.x = x;
            node1.y = y;
            node1.F = 0;
            node1.G = 0;
            node1.H = 0;
            List<MapNode> mapNodeList = new List<MapNode>();
            MapNode mapNode1 = node1;
            if ((int)x2 > (int)this.map.Info.width - 1 || (int)y2 > (int)this.map.Info.height - 1)
            {
                mapNodeList.Add(mapNode1);
                return mapNodeList;
            }
            if (this.map.Info.walkable[(int)x2, (int)y2] != (byte)2)
            {
                mapNodeList.Add(mapNode1);
                return mapNodeList;
            }
            if ((int)x == (int)x2 && (int)y == (int)y2)
            {
                mapNodeList.Add(mapNode1);
                return mapNodeList;
            }
            this.openedNode = new Dictionary<int, MapNode>();
            this.GetNeighbor(node1, x2, y2);
            while (this.openedNode.Count != 0)
            {
                MapNode node2 = new MapNode();
                node2.F = int.MaxValue;
                if (num <= 1000)
                {
                    foreach (MapNode mapNode2 in this.openedNode.Values)
                    {
                        if ((int)mapNode2.x == (int)x2 && (int)mapNode2.y == (int)y2)
                        {
                            this.openedNode.Clear();
                            node2 = mapNode2;
                            break;
                        }
                        if (mapNode2.F < node2.F)
                            node2 = mapNode2;
                    }
                    mapNode1 = node2;
                    if (this.openedNode.Count != 0)
                    {
                        this.openedNode.Remove((int)node2.x * 1000 + (int)node2.y);
                        mapNode1 = this.GetNeighbor(node2, x2, y2);
                        ++num;
                    }
                    else
                        break;
                }
                else
                    break;
            }
            for (; mapNode1.Previous != null; mapNode1 = mapNode1.Previous)
                mapNodeList.Add(mapNode1);
            mapNodeList.Reverse();
            return mapNodeList;
        }

        /// <summary>
        /// The GetPathLength.
        /// </summary>
        /// <param name="node">The node<see cref="MapNode"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int GetPathLength(MapNode node)
        {
            int num = 0;
            for (MapNode mapNode = node; mapNode.Previous != null; mapNode = mapNode.Previous)
                ++num;
            return num;
        }

        /// <summary>
        /// The GetLength.
        /// </summary>
        /// <param name="x">The x<see cref="byte"/>.</param>
        /// <param name="y">The y<see cref="byte"/>.</param>
        /// <param name="x2">The x2<see cref="byte"/>.</param>
        /// <param name="y2">The y2<see cref="byte"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetLength(byte x, byte y, byte x2, byte y2)
        {
            return (int)Math.Sqrt((double)(((int)x2 - (int)x) * ((int)x2 - (int)x) + ((int)y2 - (int)y) * ((int)y2 - (int)y)));
        }

        /// <summary>
        /// The GetLengthD.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <param name="x2">The x2<see cref="short"/>.</param>
        /// <param name="y2">The y2<see cref="short"/>.</param>
        /// <returns>The <see cref="double"/>.</returns>
        public static double GetLengthD(short x, short y, short x2, short y2)
        {
            return Math.Sqrt((double)(((int)x2 - (int)x) * ((int)x2 - (int)x) + ((int)y2 - (int)y) * ((int)y2 - (int)y)));
        }

        /// <summary>
        /// The GetDir.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        /// <returns>The <see cref="ushort"/>.</returns>
        public static ushort GetDir(short x, short y)
        {
            double lengthD = MobAI.GetLengthD((short)0, (short)0, x, y);
            int num = (int)(Math.Acos((double)y / lengthD) / Math.PI * 180.0);
            if (x < (short)0)
                return (ushort)num;
            return (ushort)(360 - num);
        }

        /// <summary>
        /// The GetNeighbor.
        /// </summary>
        /// <param name="node">The node<see cref="MapNode"/>.</param>
        /// <param name="x">The x<see cref="byte"/>.</param>
        /// <param name="y">The y<see cref="byte"/>.</param>
        /// <returns>The <see cref="MapNode"/>.</returns>
        private MapNode GetNeighbor(MapNode node, byte x, byte y)
        {
            MapNode mapNode1 = node;
            for (int index1 = (int)node.x - 1; index1 <= (int)node.x + 1; ++index1)
            {
                for (int index2 = (int)node.y - 1; index2 <= (int)node.y + 1; ++index2)
                {
                    if (index2 != -1 && index1 != -1 && (index2 != (int)node.y || index1 != (int)node.x) && (index1 < (int)this.map.Info.width && index2 < (int)this.map.Info.height && this.map.Info.walkable[index1, index2] == (byte)2))
                    {
                        if (!this.openedNode.ContainsKey(index1 * 1000 + index2))
                        {
                            MapNode mapNode2 = new MapNode();
                            mapNode2.x = (byte)index1;
                            mapNode2.y = (byte)index2;
                            mapNode2.Previous = node;
                            int num = index1 == (int)node.x ? 0 : (index2 != (int)node.y ? 1 : 0);
                            mapNode2.G = num != 0 ? node.G + 14 : node.G + 10;
                            mapNode2.H = Math.Abs((int)x - (int)mapNode2.x) * 10 + Math.Abs((int)y - (int)mapNode2.y) * 10;
                            mapNode2.F = mapNode2.G + mapNode2.H;
                            this.openedNode.Add(index1 * 1000 + index2, mapNode2);
                        }
                        else
                        {
                            MapNode mapNode2 = this.openedNode[index1 * 1000 + index2];
                            int num = index1 != (int)node.x && index2 != (int)node.y ? 14 : 10;
                            if (node.G + num > mapNode2.G)
                                mapNode1 = mapNode2;
                        }
                    }
                }
            }
            return mapNode1;
        }

        /// <summary>
        /// Gets or sets the LastSkillCast.
        /// </summary>
        public DateTime LastSkillCast
        {
            get
            {
                return this.lastSkillCast;
            }
            set
            {
                this.lastSkillCast = value;
            }
        }

        /// <summary>
        /// The OnShouldCastSkill.
        /// </summary>
        /// <param name="skillList">The skillList<see cref="Dictionary{uint, int}"/>.</param>
        /// <param name="currentTarget">The currentTarget<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnShouldCastSkill(Dictionary<uint, int> skillList, SagaDB.Actor.Actor currentTarget)
        {
            if (this.Mob.Tasks.ContainsKey("SkillCast") || skillList.Count <= 0)
                return;
            uint skillID = 0;
            int max = 0;
            foreach (int num in skillList.Values)
                max += num;
            int num1 = Global.Random.Next(0, max);
            int num2 = 0;
            foreach (uint key in skillList.Keys)
            {
                num2 += skillList[key];
                if (num1 <= num2)
                {
                    skillID = key;
                    break;
                }
            }
            if (skillID != 0U)
                this.CastSkill(skillID, (byte)1, currentTarget);
        }

        /// <summary>
        /// The CastSkill.
        /// </summary>
        /// <param name="skillID">The skillID<see cref="uint"/>.</param>
        /// <param name="lv">The lv<see cref="byte"/>.</param>
        /// <param name="target">The target<see cref="uint"/>.</param>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        public void CastSkill(uint skillID, byte lv, uint target, short x, short y)
        {
            SagaDB.Skill.Skill skill1 = Singleton<SkillFactory>.Instance.GetSkill(skillID, lv);
            if (skill1 == null)
                return;
            SkillArg skill2 = new SkillArg();
            skill2.sActor = this.Mob.ActorID;
            if (target != uint.MaxValue)
            {
                SagaDB.Actor.Actor actor1 = this.map.GetActor(target);
                if (actor1 == null)
                {
                    if (!this.Mob.Tasks.ContainsKey("AutoCast"))
                        return;
                    this.Mob.Tasks.Remove("AutoCast");
                    this.Mob.Buff.CannotMove = false;
                    return;
                }
                if (MobAI.GetLengthD(this.Mob.X, this.Mob.Y, actor1.X, actor1.Y) <= (double)((int)skill1.CastRange * 145))
                {
                    if (skill1.Target == (byte)2)
                    {
                        if (skill1.Support)
                        {
                            if (this.Mob.type == ActorType.PET)
                            {
                                ActorPet mob = (ActorPet)this.Mob;
                                skill2.dActor = mob.Owner == null ? this.Mob.ActorID : mob.Owner.ActorID;
                            }
                            else
                                skill2.dActor = this.master != null ? this.master.ActorID : this.Mob.ActorID;
                        }
                        else
                            skill2.dActor = target;
                    }
                    else if (skill1.Target == (byte)1)
                    {
                        if (this.Mob.type == ActorType.PET)
                        {
                            ActorPet mob = (ActorPet)this.Mob;
                            skill2.dActor = mob.Owner == null ? this.Mob.ActorID : mob.Owner.ActorID;
                        }
                        else
                            skill2.dActor = this.Mob.ActorID;
                    }
                    else
                        skill2.dActor = uint.MaxValue;
                    if (skill2.dActor != uint.MaxValue)
                    {
                        SagaDB.Actor.Actor actor2 = this.map.GetActor(skill2.dActor);
                        if (actor2 != null)
                        {
                            if (actor2.Buff.Dead != skill1.DeadOnly)
                            {
                                if (!this.Mob.Tasks.ContainsKey("AutoCast"))
                                    return;
                                this.Mob.Tasks.Remove("AutoCast");
                                this.Mob.Buff.CannotMove = false;
                                return;
                            }
                        }
                        else
                        {
                            if (!this.Mob.Tasks.ContainsKey("AutoCast"))
                                return;
                            this.Mob.Tasks.Remove("AutoCast");
                            this.Mob.Buff.CannotMove = false;
                            return;
                        }
                    }
                    if (this.master != null && ((int)this.master.ActorID == (int)target && !skill1.Support))
                    {
                        if (!this.Mob.Tasks.ContainsKey("AutoCast"))
                            return;
                        this.Mob.Tasks.Remove("AutoCast");
                        this.Mob.Buff.CannotMove = false;
                        return;
                    }
                    skill2.skill = skill1;
                    skill2.x = Global.PosX16to8(x, this.map.Width);
                    skill2.y = Global.PosY16to8(y, this.map.Height);
                    skill2.argType = SkillArg.ArgType.Cast;
                    skill2.delay = (uint)((double)skill1.CastTime * (1.0 - (double)this.Mob.Status.cspd / 1000.0));
                }
                else
                {
                    if (!this.Mob.Tasks.ContainsKey("AutoCast"))
                        return;
                    this.Mob.Tasks.Remove("AutoCast");
                    this.Mob.Buff.CannotMove = false;
                    return;
                }
            }
            else
            {
                skill2.dActor = uint.MaxValue;
                if (MobAI.GetLengthD(this.Mob.X, this.Mob.Y, x, y) <= (double)((int)skill1.CastRange * 145))
                {
                    skill2.skill = skill1;
                    skill2.x = Global.PosX16to8(x, this.map.Width);
                    skill2.y = Global.PosY16to8(x, this.map.Height);
                    skill2.argType = SkillArg.ArgType.Cast;
                    skill2.delay = (uint)((double)skill1.CastTime * (1.0 - (double)this.Mob.Status.cspd / 1000.0));
                }
                else
                {
                    if (!this.Mob.Tasks.ContainsKey("AutoCast"))
                        return;
                    this.Mob.Tasks.Remove("AutoCast");
                    this.Mob.Buff.CannotMove = false;
                    return;
                }
            }
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)skill2, this.Mob, false);
            if (skill1.CastTime > 0)
            {
                SkillCast skillCast = new SkillCast(this, skill2);
                this.Mob.Tasks.Add("SkillCast", (MultiRunTask)skillCast);
                skillCast.Activate();
            }
            else
                this.OnSkillCastComplete(skill2);
        }

        /// <summary>
        /// The CastSkill.
        /// </summary>
        /// <param name="skillID">The skillID<see cref="uint"/>.</param>
        /// <param name="lv">The lv<see cref="byte"/>.</param>
        /// <param name="currentTarget">The currentTarget<see cref="SagaDB.Actor.Actor"/>.</param>
        public void CastSkill(uint skillID, byte lv, SagaDB.Actor.Actor currentTarget)
        {
            this.CastSkill(skillID, lv, currentTarget.ActorID, currentTarget.X, currentTarget.Y);
        }

        /// <summary>
        /// The CastSkill.
        /// </summary>
        /// <param name="skillID">The skillID<see cref="uint"/>.</param>
        /// <param name="lv">The lv<see cref="byte"/>.</param>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        public void CastSkill(uint skillID, byte lv, short x, short y)
        {
            this.CastSkill(skillID, lv, uint.MaxValue, x, y);
        }

        /// <summary>
        /// The AttackActor.
        /// </summary>
        /// <param name="actorID">The actorID<see cref="uint"/>.</param>
        public void AttackActor(uint actorID)
        {
            if (this.Hate.ContainsKey(actorID))
                this.Hate[actorID] = this.Mob.MaxHP;
            else
                this.Hate.Add(actorID, this.Mob.MaxHP);
        }

        /// <summary>
        /// The StopAttacking.
        /// </summary>
        public void StopAttacking()
        {
            this.Hate.Clear();
        }

        /// <summary>
        /// The OnSkillCastComplete.
        /// </summary>
        /// <param name="skill">The skill<see cref="SkillArg"/>.</param>
        public void OnSkillCastComplete(SkillArg skill)
        {
            if (skill.dActor != uint.MaxValue)
            {
                SagaDB.Actor.Actor actor = this.map.GetActor(skill.dActor);
                if (actor != null)
                {
                    skill.argType = SkillArg.ArgType.Active;
                    Singleton<SkillHandler>.Instance.SkillCast(this.Mob, actor, skill);
                }
            }
            else
            {
                skill.argType = SkillArg.ArgType.Active;
                Singleton<SkillHandler>.Instance.SkillCast(this.Mob, this.Mob, skill);
            }
            if (this.Mob.type == ActorType.PET)
                Singleton<SkillHandler>.Instance.ProcessPetGrowth(this.Mob, PetGrowthReason.UseSkill);
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)skill, this.Mob, false);
            if (skill.skill.Effect != 0U)
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
                {
                    actorID = skill.dActor,
                    effectID = skill.skill.Effect,
                    x = skill.x,
                    y = skill.y
                }, this.Mob, false);
            if (this.Mob.Tasks.ContainsKey("AutoCast"))
                this.Mob.Tasks["AutoCast"].Activate();
            else if (skill.autoCast.Count != 0)
            {
                this.Mob.Buff.CannotMove = true;
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, this.Mob, true);
                AutoCast autoCast = new AutoCast(this.Mob, skill);
                this.Mob.Tasks.Add("AutoCast", (MultiRunTask)autoCast);
                autoCast.Activate();
            }
        }

        /// <summary>
        /// The OnPathInterupt.
        /// </summary>
        public void OnPathInterupt()
        {
            if (this.commands.ContainsKey("Move"))
                ((Move)this.commands["Move"]).FindPath();
            if (!this.commands.ContainsKey("Chase"))
                return;
            ((Chase)this.commands["Chase"]).FindPath();
        }

        /// <summary>
        /// The OnAttacked.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="damage">The damage<see cref="int"/>.</param>
        public void OnAttacked(SagaDB.Actor.Actor sActor, int damage)
        {
            if (this.actor.Buff.Dead)
                return;
            if (!this.Activated)
                this.Start();
            if ((int)sActor.ActorID == (int)this.actor.ActorID)
                return;
            this.lastAttacker = sActor;
            if (this.Hate.ContainsKey(sActor.ActorID))
            {
                uint num = (uint)Math.Abs(damage);
                if (num == 0U)
                    num = 1U;
                Dictionary<uint, uint> hate;
                uint actorId;
                (hate = this.Hate)[actorId = sActor.ActorID] = hate[actorId] + num;
            }
            else
            {
                uint num = (uint)Math.Abs(damage);
                if (num == 0U)
                    num = 1U;
                this.Hate.Add(sActor.ActorID, num);
            }
            if (damage > 0)
            {
                if (this.DamageTable.ContainsKey(sActor.ActorID))
                {
                    Dictionary<uint, int> damageTable;
                    uint actorId;
                    (damageTable = this.DamageTable)[actorId = sActor.ActorID] = damageTable[actorId] + damage;
                }
                else
                    this.DamageTable.Add(sActor.ActorID, damage);
                if ((long)this.DamageTable[sActor.ActorID] > (long)this.Mob.MaxHP)
                    this.DamageTable[sActor.ActorID] = (int)this.Mob.MaxHP;
            }
            if (this.firstAttacker != null)
            {
                if (this.firstAttacker == sActor)
                    this.attackStamp = DateTime.Now;
                else if ((DateTime.Now - this.attackStamp).TotalMinutes > 15.0)
                {
                    this.firstAttacker = sActor;
                    this.attackStamp = DateTime.Now;
                }
            }
            else
            {
                this.firstAttacker = sActor;
                this.attackStamp = DateTime.Now;
            }
        }

        /// <summary>
        /// The OnSeenSkillUse.
        /// </summary>
        /// <param name="arg">The arg<see cref="SkillArg"/>.</param>
        public void OnSeenSkillUse(SkillArg arg)
        {
            if (this.master != null)
            {
                for (int index = 0; index < arg.affectedActors.Count; ++index)
                {
                    if ((int)arg.affectedActors[index].ActorID == (int)this.master.ActorID)
                    {
                        SagaDB.Actor.Actor actor = this.map.GetActor(arg.sActor);
                        if (actor != null)
                        {
                            this.OnAttacked(actor, arg.hp[index]);
                            if (this.Hate.Count == 1)
                                this.SendAggroEffect();
                        }
                    }
                }
            }
            if (this.Mode.HelpSameType && this.actor.type == ActorType.MOB)
            {
                ActorMob mob = (ActorMob)this.Mob;
                for (int index = 0; index < arg.affectedActors.Count; ++index)
                {
                    SagaDB.Actor.Actor affectedActor = arg.affectedActors[index];
                    if (affectedActor.type == ActorType.MOB && ((ActorMob)affectedActor).BaseData.mobType == mob.BaseData.mobType)
                    {
                        SagaDB.Actor.Actor actor = this.map.GetActor(arg.sActor);
                        if (actor != null && actor.type == ActorType.PC)
                        {
                            if (this.Hate.Count == 0)
                                this.SendAggroEffect();
                            this.OnAttacked(actor, arg.hp[index]);
                        }
                    }
                }
                SagaDB.Actor.Actor actor1 = this.map.GetActor(arg.sActor);
                if (actor1 != null && actor1.type == ActorType.MOB && ((ActorMob)actor1).BaseData.mobType == mob.BaseData.mobType)
                {
                    foreach (SagaDB.Actor.Actor affectedActor in arg.affectedActors)
                    {
                        if (affectedActor.type == ActorType.PC)
                        {
                            if (this.Hate.Count == 0)
                                this.SendAggroEffect();
                            this.OnAttacked(affectedActor, 10);
                        }
                    }
                }
            }
            SagaDB.Actor.Actor actor2 = this.map.GetActor(arg.sActor);
            if (actor2 != null && arg.skill != null && this.Hate.Count > 0 && (arg.skill.Support && actor2.type == ActorType.PC))
            {
                int damage = 0;
                foreach (int num in arg.hp)
                    damage += -num;
                if (damage > 0)
                {
                    if (this.Hate.Count == 0)
                        this.SendAggroEffect();
                    this.OnAttacked(actor2, damage);
                }
            }
            if (!this.Mode.HateMagic)
                return;
            SagaDB.Actor.Actor actor3 = this.map.GetActor(arg.sActor);
            if (actor3 != null && arg.skill != null && arg.skill.Magical && actor3.type == ActorType.PC)
            {
                if (this.Hate.Count == 0)
                    this.SendAggroEffect();
                this.OnAttacked(actor3, (int)(this.Mob.MaxHP / 10U));
            }
        }

        /// <summary>
        /// The SendAggroEffect.
        /// </summary>
        private void SendAggroEffect()
        {
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, (MapEventArgs)new EffectArg()
            {
                actorID = this.Mob.ActorID,
                effectID = 4539U
            }, this.Mob, false);
        }
    }
}
