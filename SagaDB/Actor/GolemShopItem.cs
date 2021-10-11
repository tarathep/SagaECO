namespace SagaDB.Actor
{
    /// <summary>
    /// Defines the <see cref="GolemShopItem" />.
    /// </summary>
    public class GolemShopItem
    {
        /// <summary>
        /// Defines the inventoryID.
        /// </summary>
        private uint inventoryID;

        /// <summary>
        /// Defines the itemID.
        /// </summary>
        private uint itemID;

        /// <summary>
        /// Defines the count.
        /// </summary>
        private ushort count;

        /// <summary>
        /// Defines the price.
        /// </summary>
        private uint price;

        /// <summary>
        /// Gets or sets the InventoryID.
        /// </summary>
        public uint InventoryID
        {
            get
            {
                return this.inventoryID;
            }
            set
            {
                this.inventoryID = value;
            }
        }

        /// <summary>
        /// Gets or sets the ItemID.
        /// </summary>
        public uint ItemID
        {
            get
            {
                return this.itemID;
            }
            set
            {
                this.itemID = value;
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
        /// Gets or sets the Price.
        /// </summary>
        public uint Price
        {
            get
            {
                return this.price;
            }
            set
            {
                this.price = value;
            }
        }
    }
}
