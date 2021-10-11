namespace SagaMap.Scripting
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="WestFortField" />.
    /// </summary>
    public class WestFortField : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WestFortField"/> class.
        /// </summary>
        public WestFortField()
        {
            this.EventID = 4043309057U;
        }

        /// <summary>
        /// The OnEvent.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public override void OnEvent(ActorPC pc)
        {
            this.Warp(pc, 12019000U, (byte)5, (byte)80);
        }
    }
}
