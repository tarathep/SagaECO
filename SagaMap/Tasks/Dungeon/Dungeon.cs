namespace SagaMap.Tasks.Dungeon
{
    using global::System;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Dungeon;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="Dungeon" />.
    /// </summary>
    public class Dungeon : MultiRunTask
    {
        /// <summary>
        /// Defines the counter.
        /// </summary>
        public int counter = 0;

        /// <summary>
        /// Defines the dungeon.
        /// </summary>
        private SagaMap.Dungeon.Dungeon dungeon;

        /// <summary>
        /// Defines the lifeTime.
        /// </summary>
        public int lifeTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="Dungeon"/> class.
        /// </summary>
        /// <param name="dungeon">The dungeon<see cref="SagaMap.Dungeon.Dungeon"/>.</param>
        /// <param name="lifeTime">The lifeTime<see cref="int"/>.</param>
        public Dungeon(SagaMap.Dungeon.Dungeon dungeon, int lifeTime)
        {
            this.period = 1000;
            this.dueTime = 1000;
            this.dungeon = dungeon;
            this.lifeTime = lifeTime;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            try
            {
                ++this.counter;
                int num1 = this.lifeTime - this.counter;
                int num2;
                switch (num1)
                {
                    case 0:
                        this.Deactivate();
                        this.dungeon.Destory(DestroyType.TimeOver);
                        return;
                    case 20:
                    case 30:
                    case 40:
                    case 50:
                    case 60:
                    case 120:
                    case 180:
                    case 240:
                    case 300:
                    case 600:
                    case 900:
                    case 1800:
                    case 3600:
                    case 7200:
                        num2 = 0;
                        break;
                    default:
                        num2 = num1 > 15 ? 1 : 0;
                        break;
                }
                if (num2 == 0)
                {
                    string str = "";
                    if (num1 >= 3600)
                        str = (num1 / 3600).ToString() + Singleton<LocalManager>.Instance.Strings.ITD_HOUR;
                    if (num1 < 3600 && num1 >= 60)
                        str = (num1 / 60).ToString() + Singleton<LocalManager>.Instance.Strings.ITD_MINUTE;
                    if (num1 < 60)
                        str = num1.ToString() + Singleton<LocalManager>.Instance.Strings.ITD_SECOND;
                    string text = string.Format(Singleton<LocalManager>.Instance.Strings.ITD_CRASHING, (object)str);
                    foreach (DungeonMap map in this.dungeon.Maps)
                    {
                        foreach (SagaDB.Actor.Actor actor in map.Map.Actors.Values)
                        {
                            if (actor.type == ActorType.PC && ((ActorPC)actor).Online)
                                ((PCEventHandler)actor.e).Client.SendAnnounce(text);
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
