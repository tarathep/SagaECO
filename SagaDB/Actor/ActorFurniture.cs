namespace SagaDB.Actor
{
    /// <summary>
    /// Defines the <see cref="ActorFurniture" />.
    /// </summary>
    public class ActorFurniture : SagaDB.Actor.Actor
    {
        /// <summary>
        /// Defines the motion.
        /// </summary>
        private ushort motion = 111;

        /// <summary>
        /// Defines the itemID.
        /// </summary>
        private uint itemID;

        /// <summary>
        /// Defines the pictID.
        /// </summary>
        private uint pictID;

        /// <summary>
        /// Defines the z.
        /// </summary>
        private short z;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorFurniture"/> class.
        /// </summary>
        public ActorFurniture()
        {
            this.type = ActorType.FURNITURE;
        }

        /// <summary>
        /// Gets or sets the ItemID.
        /// </summary>
        public uint ItemID
        {
            get
            {
                return this.itemID;
            }
            set
            {
                this.itemID = value;
            }
        }

        /// <summary>
        /// Gets or sets the PictID.
        /// </summary>
        public uint PictID
        {
            get
            {
                return this.pictID;
            }
            set
            {
                this.pictID = value;
            }
        }

        /// <summary>
        /// Gets or sets the Z.
        /// </summary>
        public short Z
        {
            get
            {
                return this.z;
            }
            set
            {
                this.z = value;
            }
        }

        /// <summary>
        /// Gets or sets the Motion.
        /// </summary>
        public ushort Motion
        {
            get
            {
                return this.motion;
            }
            set
            {
                this.motion = value;
            }
        }
    }
}
