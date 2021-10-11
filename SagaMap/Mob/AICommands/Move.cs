namespace SagaMap.Mob.AICommands
{
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Move" />.
    /// </summary>
    public class Move : AICommand
    {
        /// <summary>
        /// Defines the index.
        /// </summary>
        private int index = 0;

        /// <summary>
        /// Defines the status.
        /// </summary>
        private CommandStatus status;

        /// <summary>
        /// Defines the x.
        /// </summary>
        private short x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        private short y;

        /// <summary>
        /// Defines the mob.
        /// </summary>
        private MobAI mob;

        /// <summary>
        /// Defines the path.
        /// </summary>
        private List<MapNode> path;

        /// <summary>
        /// Initializes a new instance of the <see cref="Move"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="MobAI"/>.</param>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        public Move(MobAI mob, short x, short y)
        {
            this.mob = mob;
            this.mob.map.FindFreeCoord(x, y, out x, out y);
            this.x = x;
            this.y = y;
            if (mob.Mode.NoMove)
            {
                this.status = CommandStatus.FINISHED;
            }
            else
            {
                this.path = mob.FindPath(Global.PosX16to8(mob.Mob.X, mob.map.Width), Global.PosY16to8(mob.Mob.Y, mob.map.Height), Global.PosX16to8(x, mob.map.Width), Global.PosY16to8(y, mob.map.Height));
                this.status = CommandStatus.INIT;
            }
        }

        /// <summary>
        /// The GetName.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetName()
        {
            return nameof(Move);
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="para">The para<see cref="object"/>.</param>
        public void Update(object para)
        {
            try
            {
                if (this.status == CommandStatus.FINISHED)
                    return;
                if (this.path.Count == 0 || !this.mob.CanMove)
                {
                    this.status = CommandStatus.FINISHED;
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
                        this.mob.map.MoveActor(Map.MOVE_TYPE.START, this.mob.Mob, pos, MobAI.GetDir((short)((int)pos[0] - (int)this.x), (short)((int)pos[1] - (int)this.y)), (ushort)0, true);
                    }
                    else
                    {
                        MapNode mapNode = this.path[this.path.Count - 1];
                        short[] pos = new short[2]
                        {
              Global.PosX8to16(mapNode.x, this.mob.map.Width),
              Global.PosY8to16(mapNode.y, this.mob.map.Height)
                        };
                        this.mob.map.MoveActor(Map.MOVE_TYPE.START, this.mob.Mob, pos, MobAI.GetDir((short)((int)pos[0] - (int)this.x), (short)((int)pos[1] - (int)this.y)), (ushort)((uint)this.mob.Mob.Speed / 10U), true);
                        this.Status = CommandStatus.FINISHED;
                    }
                    ++this.index;
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, (Logger)null);
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
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.status = CommandStatus.FINISHED;
        }
    }
}
