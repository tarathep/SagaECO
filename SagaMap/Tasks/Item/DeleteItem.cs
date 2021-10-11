namespace SagaMap.Tasks.Item
{
    using global::System;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="DeleteItem" />.
    /// </summary>
    public class DeleteItem : MultiRunTask
    {
        /// <summary>
        /// Defines the npc.
        /// </summary>
        private ActorItem npc;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteItem"/> class.
        /// </summary>
        /// <param name="item">The item<see cref="ActorItem"/>.</param>
        public DeleteItem(ActorItem item)
        {
            this.dueTime = 180000;
            this.period = 180000;
            this.npc = item;
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
                this.npc.Tasks.Remove(nameof(DeleteItem));
                Singleton<MapManager>.Instance.GetMap(this.npc.MapID).DeleteActor((SagaDB.Actor.Actor)this.npc);
                this.Deactivate();
            }
            catch (Exception ex)
            {
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
