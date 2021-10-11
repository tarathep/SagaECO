namespace SagaDB.Treasure
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="TreasureList" />.
    /// </summary>
    public class TreasureList
    {
        /// <summary>
        /// Defines the items.
        /// </summary>
        private List<TreasureItem> items = new List<TreasureItem>();

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the totalRate.
        /// </summary>
        private int totalRate;

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
        /// Gets the Items.
        /// </summary>
        public List<TreasureItem> Items
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// Gets or sets the TotalRate.
        /// </summary>
        public int TotalRate
        {
            get
            {
                return this.totalRate;
            }
            set
            {
                this.totalRate = value;
            }
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0},Items:{1},TotalRate:{2}", (object)this.name, (object)this.items.Count, (object)this.totalRate);
        }
    }
}
