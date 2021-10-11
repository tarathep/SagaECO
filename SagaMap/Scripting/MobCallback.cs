namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaMap.ActorEventHandlers;

    /// <summary>
    /// The MobCallback.
    /// </summary>
    /// <param name="eh">The eh<see cref="MobEventHandler"/>.</param>
    /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
    public delegate void MobCallback(MobEventHandler eh, ActorPC pc);
}
