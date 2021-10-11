namespace SagaDB.ECOShop
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ShopCategory" />.
    /// </summary>
    public class ShopCategory
    {
        /// <summary>
        /// Defines the items.
        /// </summary>
        private Dictionary<uint, ShopItem> items = new Dictionary<uint, ShopItem>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

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
        /// Gets the Items.
        /// </summary>
        public Dictionary<uint, ShopItem> Items
        {
            get
            {
                return this.items;
            }
        }
    }
}
