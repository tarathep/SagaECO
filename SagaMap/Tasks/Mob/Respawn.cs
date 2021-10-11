namespace SagaMap.Tasks.Mob
{
    using global::System;
    using global::System.Threading;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="Respawn" />.
    /// </summary>
    public class Respawn : MultiRunTask
    {
        /// <summary>
        /// Defines the mob.
        /// </summary>
        private ActorMob mob;

        /// <summary>
        /// Initializes a new instance of the <see cref="Respawn"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
        /// <param name="delay">The delay<see cref="int"/>.</param>
        public Respawn(ActorMob mob, int delay)
        {
            this.dueTime = delay;
            this.period = delay;
            this.mob = mob;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            try
            {
                Map map = Singleton<MapManager>.Instance.GetMap(this.mob.MapID);
                MobEventHandler e = (MobEventHandler)this.mob.e;
                int num1 = (int)Global.PosX16to8(e.AI.X_Ori, map.Width);
                int num2 = (int)Global.PosY16to8(e.AI.Y_Ori, map.Height);
                int min1 = num1 - (int)e.AI.MoveRange / 100;
                int max1 = num1 + (int)e.AI.MoveRange / 100;
                int min2 = num2 - (int)e.AI.MoveRange / 100;
                int max2 = num2 + (int)e.AI.MoveRange / 100;
                if (min1 < 0)
                    min1 = 0;
                if (max1 >= (int)map.Width)
                    max1 = (int)map.Width - 1;
                if (min2 < 0)
                    min2 = 0;
                if (max2 >= (int)map.Height)
                    max2 = (int)map.Height - 1;
                int index1 = (int)(byte)Global.Random.Next(min1, max1);
                while (index1 < min1 || index1 > max1)
                {
                    index1 = (int)(byte)Global.Random.Next(min1, max1);
                    Thread.Sleep(1);
                }
                int index2 = (int)(byte)Global.Random.Next(min2, max2);
                while (index2 < min2 || index2 > max2)
                {
                    index2 = (int)(byte)Global.Random.Next(min2, max2);
                    Thread.Sleep(1);
                }
                int num3 = 0;
                while (map.Info.walkable[index1, index2] != (byte)2)
                {
                    if (num3 > 1000)
                    {
                        ClientManager.LeaveCriticalArea();
                        return;
                    }
                    index1 = (int)(byte)Global.Random.Next(min1, max1);
                    index2 = (int)(byte)Global.Random.Next(min2, max2);
                    ++num3;
                }
                this.mob.Buff.Clear();
                this.mob.X = Global.PosX8to16((byte)index1, map.Width);
                this.mob.Y = Global.PosY8to16((byte)index2, map.Height);
                e.AI.X_Spawn = this.mob.X;
                e.AI.Y_Spawn = this.mob.Y;
                map.RegisterActor((SagaDB.Actor.Actor)this.mob);
                this.mob.HP = this.mob.MaxHP;
                this.mob.MP = this.mob.MaxMP;
                this.mob.SP = this.mob.MaxSP;
                this.mob.invisble = false;
                map.OnActorVisibilityChange((SagaDB.Actor.Actor)this.mob);
                map.SendVisibleActorsToActor((SagaDB.Actor.Actor)this.mob);
                this.mob.Tasks.Remove(nameof(Respawn));
                ((MobEventHandler)this.mob.e).AI.Start();
                this.Deactivate();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
