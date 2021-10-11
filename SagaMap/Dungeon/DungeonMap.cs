namespace SagaMap.Dungeon
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DungeonMap" />.
    /// </summary>
    public class DungeonMap
    {
        /// <summary>
        /// Defines the gates.
        /// </summary>
        private Dictionary<GateType, DungeonGate> gates = new Dictionary<GateType, DungeonGate>();

        /// <summary>
        /// Defines the dir.
        /// </summary>
        private byte dir = 4;

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the type.
        /// </summary>
        private MapType type;

        /// <summary>
        /// Defines the theme.
        /// </summary>
        private Theme theme;

        /// <summary>
        /// Defines the x.
        /// </summary>
        private byte x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        private byte y;

        /// <summary>
        /// Defines the map.
        /// </summary>
        private Map map;

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public uint ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets the MapType.
        /// </summary>
        public MapType MapType
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
        /// Gets or sets the Theme.
        /// </summary>
        public Theme Theme
        {
            get
            {
                return this.theme;
            }
            set
            {
                this.theme = value;
            }
        }

        /// <summary>
        /// Gets the Gates.
        /// </summary>
        public Dictionary<GateType, DungeonGate> Gates
        {
            get
            {
                return this.gates;
            }
        }

        /// <summary>
        /// Gets or sets the Map.
        /// </summary>
        public Map Map
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
        /// Gets the Dir.
        /// </summary>
        public byte Dir
        {
            get
            {
                return this.dir;
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
        /// Gets the FreeGates.
        /// </summary>
        public int FreeGates
        {
            get
            {
                int num = 0;
                foreach (DungeonGate dungeonGate in this.gates.Values)
                {
                    if (dungeonGate.GateType != GateType.Entrance && dungeonGate.GateType != GateType.Central && dungeonGate.GateType != GateType.Exit && dungeonGate.ConnectedMap == null)
                        ++num;
                }
                return num;
            }
        }

        /// <summary>
        /// The Clone.
        /// </summary>
        /// <returns>The <see cref="DungeonMap"/>.</returns>
        public DungeonMap Clone()
        {
            DungeonMap dungeonMap = new DungeonMap();
            dungeonMap.id = this.id;
            dungeonMap.type = this.type;
            dungeonMap.theme = this.theme;
            foreach (GateType key in this.gates.Keys)
                dungeonMap.gates.Add(key, this.gates[key].Clone());
            return dungeonMap;
        }

        /// <summary>
        /// The GetXForGate.
        /// </summary>
        /// <param name="type">The type<see cref="GateType"/>.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        public byte GetXForGate(GateType type)
        {
            if (!this.gates.ContainsKey(type))
                return byte.MaxValue;
            switch (type)
            {
                case GateType.East:
                    return (byte)((uint)this.x + 1U);
                case GateType.West:
                    return (byte)((uint)this.x - 1U);
                case GateType.South:
                    return this.x;
                case GateType.North:
                    return this.x;
                default:
                    return byte.MaxValue;
            }
        }

        /// <summary>
        /// The GetYForGate.
        /// </summary>
        /// <param name="type">The type<see cref="GateType"/>.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        public byte GetYForGate(GateType type)
        {
            if (!this.gates.ContainsKey(type))
                return byte.MaxValue;
            switch (type)
            {
                case GateType.East:
                    return this.y;
                case GateType.West:
                    return this.y;
                case GateType.South:
                    return (byte)((uint)this.y + 1U);
                case GateType.North:
                    return (byte)((uint)this.y - 1U);
                default:
                    return byte.MaxValue;
            }
        }

        /// <summary>
        /// The Rotate.
        /// </summary>
        public void Rotate()
        {
            this.dir = (byte)(((int)this.dir + 2) % 8);
            DungeonGate dungeonGate1 = (DungeonGate)null;
            DungeonGate dungeonGate2 = (DungeonGate)null;
            DungeonGate dungeonGate3 = (DungeonGate)null;
            DungeonGate dungeonGate4 = (DungeonGate)null;
            if (this.gates.ContainsKey(GateType.North))
                dungeonGate4 = this.gates[GateType.North];
            if (this.gates.ContainsKey(GateType.East))
                dungeonGate1 = this.gates[GateType.East];
            if (this.gates.ContainsKey(GateType.South))
                dungeonGate2 = this.gates[GateType.South];
            if (this.gates.ContainsKey(GateType.West))
                dungeonGate3 = this.gates[GateType.West];
            this.gates.Clear();
            if (dungeonGate4 != null)
            {
                dungeonGate4.GateType = GateType.West;
                this.gates.Add(GateType.West, dungeonGate4);
            }
            if (dungeonGate1 != null)
            {
                dungeonGate1.GateType = GateType.North;
                this.gates.Add(GateType.North, dungeonGate1);
            }
            if (dungeonGate2 != null)
            {
                dungeonGate2.GateType = GateType.East;
                this.gates.Add(GateType.East, dungeonGate2);
            }
            if (dungeonGate3 == null)
                return;
            dungeonGate3.GateType = GateType.South;
            this.gates.Add(GateType.South, dungeonGate3);
        }
    }
}
