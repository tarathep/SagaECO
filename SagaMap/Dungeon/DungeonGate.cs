namespace SagaMap.Dungeon
{
    /// <summary>
    /// Defines the <see cref="DungeonGate" />.
    /// </summary>
    public class DungeonGate
    {
        /// <summary>
        /// Defines the type.
        /// </summary>
        private GateType type;

        /// <summary>
        /// Defines the x.
        /// </summary>
        private byte x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        private byte y;

        /// <summary>
        /// Defines the npcID.
        /// </summary>
        private uint npcID;

        /// <summary>
        /// Defines the map.
        /// </summary>
        private DungeonMap map;

        /// <summary>
        /// Defines the dir.
        /// </summary>
        private Direction dir;

        /// <summary>
        /// Gets or sets the GateType.
        /// </summary>
        public GateType GateType
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        public byte X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        public byte Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// Gets or sets the NPCID.
        /// </summary>
        public uint NPCID
        {
            get
            {
                return this.npcID;
            }
            set
            {
                this.npcID = value;
            }
        }

        /// <summary>
        /// Gets or sets the ConnectedMap.
        /// </summary>
        public DungeonMap ConnectedMap
        {
            get
            {
                return this.map;
            }
            set
            {
                this.map = value;
            }
        }

        /// <summary>
        /// Gets or sets the Direction.
        /// </summary>
        public Direction Direction
        {
            get
            {
                return this.dir;
            }
            set
            {
                this.dir = value;
            }
        }

        /// <summary>
        /// The Clone.
        /// </summary>
        /// <returns>The <see cref="DungeonGate"/>.</returns>
        public DungeonGate Clone()
        {
            return new DungeonGate()
            {
                type = this.type,
                x = this.x,
                y = this.y,
                npcID = this.npcID
            };
        }
    }
}
