namespace SagaMap.Mob.AICommands
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Chase" />.
    /// </summary>
    public class Chase : AICommand
    {
        /// <summary>
        /// Defines the index.
        /// </summary>
        private int index = 0;

        /// <summary>
        /// Defines the x.
        /// </summary>
        public short x = 0;

        /// <summary>
        /// Defines the y.
        /// </summary>
        public short y = 0;

        /// <summary>
        /// Defines the status.
        /// </summary>
        private CommandStatus status;

        /// <summary>
        /// Defines the dest.
        /// </summary>
        private SagaDB.Actor.Actor dest;

        /// <summary>
        /// Defines the mob.
        /// </summary>
        private MobAI mob;

        /// <summary>
        /// Defines the path.
        /// </summary>
        private List<MapNode> path;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chase"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="MobAI"/>.</param>
        /// <param name="dest">The dest<see cref="SagaDB.Actor.Actor"/>.</param>
        public Chase(MobAI mob, SagaDB.Actor.Actor dest)
        {
            this.mob = mob;
            this.dest = dest;
            this.x = dest.X;
            this.y = dest.Y;
            if ((int)mob.Mob.MapID != (int)dest.MapID || !mob.CanMove)
            {
                this.Status = CommandStatus.FINISHED;
            }
            else
            {
                if (mob.Mode.RunAway)
                {
                    if (MobAI.GetLengthD(mob.Mob.X, mob.Mob.Y, dest.X, dest.Y) < 2000.0)
                    {
                        if (Global.Random.Next(0, 99) < 20)
                        {
                            int num = Global.Random.Next(100, 1000);
                            this.x = (short)((int)mob.Mob.X - (int)dest.X);
                            this.y = (short)((int)mob.Mob.Y - (int)dest.Y);
                            this.x = (short)((double)mob.Mob.X + (double)this.x / MobAI.GetLengthD((short)0, (short)0, this.x, this.y) * (double)num);
                            this.y = (short)((double)mob.Mob.Y + (double)this.y / MobAI.GetLengthD((short)0, (short)0, this.x, this.y) * (double)num);
                        }
                        else
                        {
                            this.status = CommandStatus.FINISHED;
                            return;
                        }
                    }
                    else
                    {
                        this.Status = CommandStatus.FINISHED;
                        return;
                    }
                }
                this.path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(this.x, mob.map.Width), Global.PosY16to8(this.y, mob.map.Height));
                this.Status = CommandStatus.INIT;
            }
        }

        /// <summary>
        /// The GetName.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetName()
        {
            return nameof(Chase);
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="para">The para<see cref="object"/>.</param>
        public void Update(object para)
        {
            try
            {
                if (this.mob.Mode.NoMove || !this.mob.CanMove)
                    return;
                if (this.dest.Status == null)
                {
                    this.status = CommandStatus.FINISHED;
                }
                else
                {
                    if (this.dest.Status.Additions.ContainsKey("Hiding"))
                        this.Status = CommandStatus.FINISHED;
                    if (this.Status == CommandStatus.FINISHED)
                        return;
                    float num = this.mob.Mob.type == ActorType.PC ? 1f : ((ActorMob)this.mob.Mob).BaseData.mobSize;
                    bool flag = false;
                    if (this.mob.Mob.type == ActorType.PET && ((ActorMob)this.mob.Mob).BaseData.mobType == MobType.MAGIC_CREATURE)
                        flag = true;
                    if (MobAI.GetLengthD(this.mob.Mob.X, this.mob.Mob.Y, this.dest.X, this.dest.Y) <= (double)num * 150.0 && !flag && (!this.mob.Mode.RunAway || Global.Random.Next(0, 99) < 70))
                    {
                        this.mob.map.FindFreeCoord(this.mob.Mob.X, this.mob.Mob.Y, out this.x, out this.y, this.mob.Mob);
                        if ((int)this.mob.Mob.X == (int)this.x && (int)this.mob.Mob.Y == (int)this.y || this.mob.Mode.RunAway)
                        {
                            this.Status = CommandStatus.FINISHED;
                        }
                        else
                        {
                            short[] pos = new short[2] { this.x, this.y };
                            this.mob.map.MoveActor(Map.MOVE_TYPE.START, this.mob.Mob, pos, MobAI.GetDir((short)((int)pos[0] - (int)this.x), (short)((int)pos[1] - (int)this.y)), (ushort)((uint)this.mob.Mob.Speed / 20U), true);
                        }
                    }
                    else
                    {
                        if (this.index + 1 < this.path.Count)
                        {
                            MapNode mapNode = this.path[this.index];
                            short[] pos = new short[2]
                            {
                Global.PosX8to16(mapNode.x, this.mob.map.Width),
                Global.PosY8to16(mapNode.y, this.mob.map.Height)
                            };
                            this.mob.map.MoveActor(Map.MOVE_TYPE.START, this.mob.Mob, pos, MobAI.GetDir((short)((int)pos[0] - (int)this.x), (short)((int)pos[1] - (int)this.y)), (ushort)((uint)this.mob.Mob.Speed / 20U), true);
                        }
                        else
                        {
                            if (this.path.Count == 0)
                            {
                                this.Status = CommandStatus.FINISHED;
                                return;
                            }
                            MapNode mapNode = this.path[this.path.Count - 1];
                            short[] pos = new short[2]
                            {
                Global.PosX8to16(mapNode.x, this.mob.map.Width),
                Global.PosY8to16(mapNode.y, this.mob.map.Height)
                            };
                            if (this.mob.map.GetActorsArea(pos[0], pos[1], (short)50, new SagaDB.Actor.Actor[0]).Count > 0 && !flag)
                            {
                                this.mob.map.FindFreeCoord(this.dest.X, this.dest.Y, out this.x, out this.y, this.mob.Mob);
                                this.path = this.mob.FindPath(Global.PosX16to8(this.mob.Mob.X, this.mob.map.Width), Global.PosY16to8(this.mob.Mob.Y, this.mob.map.Height), Global.PosX16to8(this.x, this.mob.map.Width), Global.PosY16to8(this.y, this.mob.map.Height));
                                this.index = 0;
                                return;
                            }
                            this.mob.map.MoveActor(Map.MOVE_TYPE.START, this.mob.Mob, pos, MobAI.GetDir((short)((int)pos[0] - (int)this.x), (short)((int)pos[1] - (int)this.y)), (ushort)((uint)this.mob.Mob.Speed / 20U), true);
                            if (MobAI.GetLengthD(this.mob.Mob.X, this.mob.Mob.Y, this.dest.X, this.dest.Y) > 50.0 + (double)num * 100.0 && !this.mob.Mode.RunAway)
                            {
                                if ((int)this.mob.Mob.MapID != (int)this.dest.MapID)
                                {
                                    this.Status = CommandStatus.FINISHED;
                                    return;
                                }
                                this.path = this.mob.FindPath(Global.PosX16to8(this.mob.Mob.X, this.mob.map.Width), Global.PosY16to8(this.mob.Mob.Y, this.mob.map.Height), Global.PosX16to8(this.dest.X, this.mob.map.Width), Global.PosY16to8(this.dest.Y, this.mob.map.Height));
                                this.index = -1;
                            }
                            else
                            {
                                this.Status = CommandStatus.FINISHED;
                                return;
                            }
                        }
                        ++this.index;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, (Logger)null);
                this.Status = CommandStatus.FINISHED;
            }
        }

        /// <summary>
        /// The FindPath.
        /// </summary>
        public void FindPath()
        {
            this.path = this.mob.FindPath(Global.PosX16to8(this.mob.Mob.X, this.mob.map.Width), Global.PosY16to8(this.mob.Mob.Y, this.mob.map.Height), Global.PosX16to8(this.x, this.mob.map.Width), Global.PosY16to8(this.y, this.mob.map.Height));
            this.index = 0;
        }

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        public CommandStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        /// <summary>
        /// The GetPath.
        /// </summary>
        /// <returns>The <see cref="List{MapNode}"/>.</returns>
        public List<MapNode> GetPath()
        {
            return this.path;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.status = CommandStatus.FINISHED;
        }
    }
}
