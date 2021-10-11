namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="RandomPortal" />.
    /// </summary>
    public abstract class RandomPortal : Event
    {
        /// <summary>
        /// Defines the mapID.
        /// </summary>
        public uint mapID;

        /// <summary>
        /// Defines the x1.
        /// </summary>
        public byte x1;

        /// <summary>
        /// Defines the y1.
        /// </summary>
        public byte y1;

        /// <summary>
        /// Defines the x2.
        /// </summary>
        public byte x2;

        /// <summary>
        /// Defines the y2.
        /// </summary>
        public byte y2;

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="eventID">The eventID<see cref="uint"/>.</param>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        /// <param name="x1">The x1<see cref="byte"/>.</param>
        /// <param name="y1">The y1<see cref="byte"/>.</param>
        /// <param name="x2">The x2<see cref="byte"/>.</param>
        /// <param name="y2">The y2<see cref="byte"/>.</param>
        public void Init(uint eventID, uint mapID, byte x1, byte y1, byte x2, byte y2)
        {
            this.EventID = eventID;
            this.mapID = mapID;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }

        /// <summary>
        /// The OnEvent.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public override void OnEvent(ActorPC pc)
        {
            byte x = (byte)Global.Random.Next((int)this.x1, (int)this.x2);
            byte y = (byte)Global.Random.Next((int)this.y1, (int)this.y2);
            this.Warp(pc, this.mapID, x, y);
        }
    }
}
