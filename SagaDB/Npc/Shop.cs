namespace SagaDB.Npc
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Shop" />.
    /// </summary>
    public class Shop
    {
        /// <summary>
        /// Defines the goods.
        /// </summary>
        private List<uint> goods = new List<uint>();

        /// <summary>
        /// Defines the npcs.
        /// </summary>
        private List<uint> npcs = new List<uint>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the sellrate.
        /// </summary>
        private uint sellrate;

        /// <summary>
        /// Defines the buyrate.
        /// </summary>
        private uint buyrate;

        /// <summary>
        /// Defines the buylimit.
        /// </summary>
        private uint buylimit;

        /// <summary>
        /// Defines the type.
        /// </summary>
        private ShopType type;

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
        /// Gets the RelatedNPC.
        /// </summary>
        public List<uint> RelatedNPC
        {
            get
            {
                return this.npcs;
            }
        }

        /// <summary>
        /// Gets or sets the SellRate.
        /// </summary>
        public uint SellRate
        {
            get
            {
                return this.sellrate;
            }
            set
            {
                this.sellrate = value;
            }
        }

        /// <summary>
        /// Gets or sets the BuyRate.
        /// </summary>
        public uint BuyRate
        {
            get
            {
                return this.buyrate;
            }
            set
            {
                this.buyrate = value;
            }
        }

        /// <summary>
        /// Gets or sets the BuyLimit.
        /// </summary>
        public uint BuyLimit
        {
            get
            {
                return this.buylimit;
            }
            set
            {
                this.buylimit = value;
            }
        }

        /// <summary>
        /// Gets the Goods.
        /// </summary>
        public List<uint> Goods
        {
            get
            {
                return this.goods;
            }
        }

        /// <summary>
        /// Gets or sets the ShopType.
        /// </summary>
        public ShopType ShopType
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}
