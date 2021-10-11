namespace SagaDB.DEMIC
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DEMICPanel" />.
    /// </summary>
    public class DEMICPanel
    {
        /// <summary>
        /// Defines the chips.
        /// </summary>
        private List<Chip> chips = new List<Chip>();

        /// <summary>
        /// Defines the engageTask1.
        /// </summary>
        private byte engageTask1 = byte.MaxValue;

        /// <summary>
        /// Defines the engageTask2.
        /// </summary>
        private byte engageTask2 = byte.MaxValue;

        /// <summary>
        /// Gets or sets the Chips.
        /// </summary>
        public List<Chip> Chips
        {
            get
            {
                return this.chips;
            }
            set
            {
                this.chips = value;
            }
        }

        /// <summary>
        /// Gets or sets the EngageTask1.
        /// </summary>
        public byte EngageTask1
        {
            get
            {
                return this.engageTask1;
            }
            set
            {
                this.engageTask1 = value;
            }
        }

        /// <summary>
        /// Gets or sets the EngageTask2.
        /// </summary>
        public byte EngageTask2
        {
            get
            {
                return this.engageTask2;
            }
            set
            {
                this.engageTask2 = value;
            }
        }
    }
}
