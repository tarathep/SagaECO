namespace SagaDB.KnightWar
{
    using System;

    /// <summary>
    /// Defines the <see cref="KnightWar" />.
    /// </summary>
    public class KnightWar
    {
        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the mapID.
        /// </summary>
        private uint mapID;

        /// <summary>
        /// Defines the maxLV.
        /// </summary>
        private byte maxLV;

        /// <summary>
        /// Defines the startTime.
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// Defines the duration.
        /// </summary>
        private int duration;

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
        /// Gets or sets the MapID.
        /// </summary>
        public uint MapID
        {
            get
            {
                return this.mapID;
            }
            set
            {
                this.mapID = value;
            }
        }

        /// <summary>
        /// Gets or sets the MaxLV.
        /// </summary>
        public byte MaxLV
        {
            get
            {
                return this.maxLV;
            }
            set
            {
                this.maxLV = value;
            }
        }

        /// <summary>
        /// Gets or sets the StartTime.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the Duration.
        /// </summary>
        public int Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }
    }
}
