namespace SagaMap.Tasks.Golem
{
    using global::System;
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaDB.Marionette;
    using SagaDB.Treasure;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="GolemTask" />.
    /// </summary>
    public class GolemTask : MultiRunTask
    {
        /// <summary>
        /// Defines the counter.
        /// </summary>
        private int counter = 0;

        /// <summary>
        /// Defines the nextGatherTime.
        /// </summary>
        private DateTime nextGatherTime = DateTime.Now + new TimeSpan(2, 0, 0, 0);

        /// <summary>
        /// Defines the golem.
        /// </summary>
        private ActorGolem golem;

        /// <summary>
        /// Defines the gatherSpan.
        /// </summary>
        private TimeSpan gatherSpan;

        /// <summary>
        /// Initializes a new instance of the <see cref="GolemTask"/> class.
        /// </summary>
        /// <param name="golem">The golem<see cref="ActorGolem"/>.</param>
        public GolemTask(ActorGolem golem)
        {
            this.dueTime = 60000;
            this.period = 60000;
            this.golem = golem;
            Map map = Singleton<MapManager>.Instance.GetMap(golem.MapID);
            switch (golem.GolemType)
            {
                case GolemType.Plant:
                    if (!map.Info.gatherInterval.ContainsKey(GatherType.Plant))
                        break;
                    this.gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Plant], 0);
                    this.nextGatherTime = DateTime.Now + this.gatherSpan;
                    break;
                case GolemType.Mineral:
                    if (!map.Info.gatherInterval.ContainsKey(GatherType.Mineral))
                        break;
                    this.gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Mineral], 0);
                    this.nextGatherTime = DateTime.Now + this.gatherSpan;
                    break;
                case GolemType.Food:
                    if (!map.Info.gatherInterval.ContainsKey(GatherType.Food))
                        break;
                    this.gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Food], 0);
                    this.nextGatherTime = DateTime.Now + this.gatherSpan;
                    break;
                case GolemType.Magic:
                    if (!map.Info.gatherInterval.ContainsKey(GatherType.Magic))
                        break;
                    this.gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Magic], 0);
                    this.nextGatherTime = DateTime.Now + this.gatherSpan;
                    break;
                case GolemType.TreasureBox:
                    if (!map.Info.gatherInterval.ContainsKey(GatherType.Treasurebox))
                        break;
                    this.gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Treasurebox], 0);
                    this.nextGatherTime = DateTime.Now + this.gatherSpan;
                    break;
                case GolemType.Excavation:
                    if (!map.Info.gatherInterval.ContainsKey(GatherType.Excavation))
                        break;
                    this.gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Excavation], 0);
                    this.nextGatherTime = DateTime.Now + this.gatherSpan;
                    break;
                case GolemType.Any:
                    if (!map.Info.gatherInterval.ContainsKey(GatherType.Any))
                        break;
                    this.gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Any], 0);
                    this.nextGatherTime = DateTime.Now + this.gatherSpan;
                    break;
                case GolemType.Strange:
                    if (!map.Info.gatherInterval.ContainsKey(GatherType.Strange))
                        break;
                    this.gatherSpan = new TimeSpan(0, map.Info.gatherInterval[GatherType.Strange], 0);
                    this.nextGatherTime = DateTime.Now + this.gatherSpan;
                    break;
            }
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            ++this.counter;
            try
            {
                if (this.counter == 1440)
                {
                    Map map = Singleton<MapManager>.Instance.GetMap(this.golem.MapID);
                    if (this.golem.GolemType >= GolemType.Plant && this.golem.GolemType <= GolemType.Strange)
                    {
                        MobEventHandler e = (MobEventHandler)this.golem.e;
                        this.golem.e = (ActorEventHandler)new NullEventHandler();
                        e.AI.Pause();
                    }
                    this.golem.invisble = true;
                    map.OnActorVisibilityChange((SagaDB.Actor.Actor)this.golem);
                    this.golem.Tasks.Remove(nameof(GolemTask));
                    this.Deactivate();
                }
                if (!(this.nextGatherTime <= DateTime.Now))
                    return;
                if (this.golem.GolemType >= GolemType.Plant && this.golem.GolemType <= GolemType.Strange)
                {
                    SagaDB.Marionette.Marionette marionette = Singleton<MarionetteFactory>.Instance[this.golem.Item.BaseData.marionetteID];
                    if (marionette != null)
                    {
                        TreasureItem treasureItem = (TreasureItem)null;
                        int num1 = 0;
                        switch (this.golem.GolemType)
                        {
                            case GolemType.Plant:
                                if (!marionette.gather[GatherType.Plant])
                                    num1 = Global.Random.Next(0, 99);
                                if (num1 <= 10)
                                {
                                    treasureItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem("Gather_Plant");
                                    break;
                                }
                                break;
                            case GolemType.Mineral:
                                if (!marionette.gather[GatherType.Mineral])
                                    num1 = Global.Random.Next(0, 99);
                                if (num1 <= 10)
                                {
                                    treasureItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem("Gather_Mineral");
                                    break;
                                }
                                break;
                            case GolemType.Food:
                                if (!marionette.gather[GatherType.Food])
                                    num1 = Global.Random.Next(0, 99);
                                if (num1 <= 10)
                                {
                                    treasureItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem("Gather_Food");
                                    break;
                                }
                                break;
                            case GolemType.Magic:
                                if (!marionette.gather[GatherType.Magic])
                                    num1 = Global.Random.Next(0, 99);
                                if (num1 <= 10)
                                {
                                    treasureItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem("Gather_Magic");
                                    break;
                                }
                                break;
                            case GolemType.TreasureBox:
                                if (!marionette.gather[GatherType.Treasurebox])
                                    num1 = Global.Random.Next(0, 99);
                                if (num1 <= 10)
                                {
                                    treasureItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem("Gather_Treasurebox");
                                    break;
                                }
                                break;
                            case GolemType.Excavation:
                                if (!marionette.gather[GatherType.Excavation])
                                    num1 = Global.Random.Next(0, 99);
                                if (num1 <= 10)
                                {
                                    treasureItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem("Gather_Excavation");
                                    break;
                                }
                                break;
                            case GolemType.Any:
                                if (!marionette.gather[GatherType.Any])
                                    num1 = Global.Random.Next(0, 99);
                                if (num1 <= 10)
                                {
                                    treasureItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem("Gather_Any");
                                    break;
                                }
                                break;
                            case GolemType.Strange:
                                if (!marionette.gather[GatherType.Strange])
                                    num1 = Global.Random.Next(0, 99);
                                if (num1 <= 10)
                                {
                                    treasureItem = FactoryString<TreasureFactory, TreasureList>.Instance.GetRandomItem("Gather_Strange");
                                    break;
                                }
                                break;
                        }
                        if (treasureItem != null && treasureItem.ID != 0U)
                        {
                            SagaDB.Item.Item obj = Factory<ItemFactory, SagaDB.Item.Item.ItemData>.Instance.GetItem(treasureItem.ID, false);
                            obj.Stack = (ushort)treasureItem.Count;
                            if (obj.Stack > (ushort)0)
                                Logger.LogItemGet(Logger.EventType.ItemGolemGet, this.golem.Owner.Name + "(" + (object)this.golem.Owner.CharID + ")", obj.BaseData.name + "(" + (object)obj.ItemID + ")", string.Format("GolemCollect Count:{0}", (object)obj.Stack), false);
                            int num2 = (int)this.golem.Owner.Inventory.AddItem(ContainerType.GOLEMWAREHOUSE, obj);
                        }
                    }
                }
                this.nextGatherTime += this.gatherSpan;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.golem.Tasks.Remove(nameof(GolemTask));
                this.Deactivate();
            }
        }
    }
}
