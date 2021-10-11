namespace SagaDB.Npc
{
    /// <summary>
    /// Defines the <see cref="NPC" />.
    /// </summary>
    public class NPC
    {
        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the mapid.
        /// </summary>
        private uint mapid;

        /// <summary>
        /// Defines the x.
        /// </summary>
        private byte x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        private byte y;

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.name;
        }

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
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the MapID.
        /// </summary>
        public uint MapID
        {
            get
            {
                return this.mapid;
            }
            set
            {
                this.mapid = value;
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
    }
}
