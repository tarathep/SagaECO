namespace SagaDB.Synthese
{
    /// <summary>
    /// Defines the <see cref="ItemElement" />.
    /// </summary>
    public class ItemElement
    {
        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the count.
        /// </summary>
        private ushort count;

        /// <summary>
        /// Defines the rate.
        /// </summary>
        private int rate;

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
        /// Gets or sets the Count.
        /// </summary>
        public ushort Count
        {
            get
            {
                return this.count;
            }
            set
            {
                this.count = value;
            }
        }

        /// <summary>
        /// Gets or sets the Rate.
        /// </summary>
        public int Rate
        {
            get
            {
                return this.rate;
            }
            set
            {
                this.rate = value;
            }
        }
    }
}
