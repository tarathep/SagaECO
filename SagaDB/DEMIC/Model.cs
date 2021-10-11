namespace SagaDB.DEMIC
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Model" />.
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Defines the cells.
        /// </summary>
        private List<byte[]> cells = new List<byte[]>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the centerX.
        /// </summary>
        private byte centerX;

        /// <summary>
        /// Defines the centerY.
        /// </summary>
        private byte centerY;

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
        /// Gets or sets the CenterX.
        /// </summary>
        public byte CenterX
        {
            get
            {
                return this.centerX;
            }
            set
            {
                this.centerX = value;
            }
        }

        /// <summary>
        /// Gets or sets the CenterY.
        /// </summary>
        public byte CenterY
        {
            get
            {
                return this.centerY;
            }
            set
            {
                this.centerY = value;
            }
        }

        /// <summary>
        /// Gets the Cells.
        /// </summary>
        public List<byte[]> Cells
        {
            get
            {
                return this.cells;
            }
        }
    }
}
