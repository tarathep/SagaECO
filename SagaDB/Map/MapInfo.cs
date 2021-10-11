namespace SagaDB.Map
{
    using SagaDB.Marionette;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MapInfo" />.
    /// </summary>
    public class MapInfo
    {
        /// <summary>
        /// Defines the gatherInterval.
        /// </summary>
        public Dictionary<GatherType, int> gatherInterval = new Dictionary<GatherType, int>();

        /// <summary>
        /// Defines the events.
        /// </summary>
        public Dictionary<uint, byte[]> events = new Dictionary<uint, byte[]>();

        /// <summary>
        /// Defines the Flag.
        /// </summary>
        public BitMask<MapFlags> Flag = new BitMask<MapFlags>(new BitMask());

        /// <summary>
        /// Defines the id.
        /// </summary>
        public uint id;

        /// <summary>
        /// Defines the name.
        /// </summary>
        public string name;

        /// <summary>
        /// Defines the width.
        /// </summary>
        public ushort width;

        /// <summary>
        /// Defines the height.
        /// </summary>
        public ushort height;

        /// <summary>
        /// Defines the walkable.
        /// </summary>
        public byte[,] walkable;

        /// <summary>
        /// Defines the holy.
        /// </summary>
        public byte[,] holy;

        /// <summary>
        /// Defines the dark.
        /// </summary>
        public byte[,] dark;

        /// <summary>
        /// Defines the neutral.
        /// </summary>
        public byte[,] neutral;

        /// <summary>
        /// Defines the fire.
        /// </summary>
        public byte[,] fire;

        /// <summary>
        /// Defines the wind.
        /// </summary>
        public byte[,] wind;

        /// <summary>
        /// Defines the water.
        /// </summary>
        public byte[,] water;

        /// <summary>
        /// Defines the earth.
        /// </summary>
        public byte[,] earth;

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.name;
        }

        /// <summary>
        /// Gets a value indicating whether Healing.
        /// </summary>
        public bool Healing
        {
            get
            {
                return this.Flag.Test(MapFlags.Healing);
            }
        }

        /// <summary>
        /// Gets a value indicating whether Cold.
        /// </summary>
        public bool Cold
        {
            get
            {
                return this.Flag.Test(MapFlags.Cold);
            }
        }

        /// <summary>
        /// Gets a value indicating whether Hot.
        /// </summary>
        public bool Hot
        {
            get
            {
                return this.Flag.Test(MapFlags.Hot);
            }
        }

        /// <summary>
        /// Gets a value indicating whether Wet.
        /// </summary>
        public bool Wet
        {
            get
            {
                return this.Flag.Test(MapFlags.Wet);
            }
        }
    }
}
