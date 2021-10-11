namespace SagaDB.DEMIC
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ChipShopCategory" />.
    /// </summary>
    public class ChipShopCategory
    {
        /// <summary>
        /// Defines the items.
        /// </summary>
        private Dictionary<uint, ShopChip> items = new Dictionary<uint, ShopChip>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the possibleLv.
        /// </summary>
        private byte possibleLv;

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
        /// Gets or sets the PossibleLv.
        /// </summary>
        public byte PossibleLv
        {
            get
            {
                return this.possibleLv;
            }
            set
            {
                this.possibleLv = value;
            }
        }

        /// <summary>
        /// Gets the Items.
        /// </summary>
        public Dictionary<uint, ShopChip> Items
        {
            get
            {
                return this.items;
            }
        }
    }
}
