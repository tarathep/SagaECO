namespace SagaMap.Scripting
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="Portal" />.
    /// </summary>
    public abstract class Portal : Event
    {
        /// <summary>
        /// Defines the mapID.
        /// </summary>
        public uint mapID;

        /// <summary>
        /// Defines the x.
        /// </summary>
        public byte x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        public byte y;

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="eventid">The eventid<see cref="uint"/>.</param>
        /// <param name="mapid">The mapid<see cref="uint"/>.</param>
        /// <param name="x">The x<see cref="byte"/>.</param>
        /// <param name="y">The y<see cref="byte"/>.</param>
        public void Init(uint eventid, uint mapid, byte x, byte y)
        {
            this.EventID = eventid;
            this.mapID = mapid;
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// The OnEvent.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public override void OnEvent(ActorPC pc)
        {
            this.Warp(pc, this.mapID, this.x, this.y);
        }
    }
}
