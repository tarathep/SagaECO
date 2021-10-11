namespace SagaMap.Scripting
{
    using SagaDB.Actor;

    /// <summary>
    /// The TimerCallback.
    /// </summary>
    /// <param name="timer">The timer<see cref="Timer"/>.</param>
    /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
    public delegate void TimerCallback(Timer timer, ActorPC pc);
}
