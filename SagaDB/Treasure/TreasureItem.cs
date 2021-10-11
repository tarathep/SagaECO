namespace SagaDB.Treasure
{
    /// <summary>
    /// Defines the <see cref="TreasureItem" />.
    /// </summary>
    public class TreasureItem
    {
        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the rate.
        /// </summary>
        private int rate;

        /// <summary>
        /// Defines the count.
        /// </summary>
        private int count;

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

        /// <summary>
        /// Gets or sets the Count.
        /// </summary>
        public int Count
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
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return string.Format("ItemID:{0}, Rate:{1},Count:{2}", (object)this.id, (object)this.rate, (object)this.count);
        }
    }
}
