namespace SagaMap.Tasks.Mob
{
    using global::System;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="DeleteCorpse" />.
    /// </summary>
    public class DeleteCorpse : MultiRunTask
    {
        /// <summary>
        /// Defines the npc.
        /// </summary>
        private ActorMob npc;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteCorpse"/> class.
        /// </summary>
        /// <param name="mob">The mob<see cref="ActorMob"/>.</param>
        public DeleteCorpse(ActorMob mob)
        {
            this.dueTime = 5000;
            this.period = 5000;
            this.npc = mob;
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
                this.npc.Tasks.Remove(nameof(DeleteCorpse));
                Singleton<MapManager>.Instance.GetMap(this.npc.MapID).DeleteActor((SagaDB.Actor.Actor)this.npc);
                this.Deactivate();
            }
            catch (Exception ex)
            {
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
