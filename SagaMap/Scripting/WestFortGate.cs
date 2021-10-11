namespace SagaMap.Scripting
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="WestFortGate" />.
    /// </summary>
    public class WestFortGate : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WestFortGate"/> class.
        /// </summary>
        public WestFortGate()
        {
            this.EventID = 4043309056U;
        }

        /// <summary>
        /// The OnEvent.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public override void OnEvent(ActorPC pc)
        {
            this.Warp(pc, 32003001U, (byte)20, (byte)81);
        }
    }
}
