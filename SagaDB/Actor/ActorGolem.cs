namespace SagaDB.Actor
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ActorGolem" />.
    /// </summary>
    public class ActorGolem : ActorMob
    {
        /// <summary>
        /// Defines the sellShop.
        /// </summary>
        private Dictionary<uint, GolemShopItem> sellShop = new Dictionary<uint, GolemShopItem>();

        /// <summary>
        /// Defines the soldItem.
        /// </summary>
        private Dictionary<uint, GolemShopItem> soldItem = new Dictionary<uint, GolemShopItem>();

        /// <summary>
        /// Defines the buyShop.
        /// </summary>
        private Dictionary<uint, GolemShopItem> buyShop = new Dictionary<uint, GolemShopItem>();

        /// <summary>
        /// Defines the boughtItem.
        /// </summary>
        private Dictionary<uint, GolemShopItem> boughtItem = new Dictionary<uint, GolemShopItem>();

        /// <summary>
        /// Defines the item.
        /// </summary>
        private SagaDB.Item.Item item;

        /// <summary>
        /// Defines the title.
        /// </summary>
        private string title;

        /// <summary>
        /// Defines the owner.
        /// </summary>
        private ActorPC owner;

        /// <summary>
        /// Defines the golemType.
        /// </summary>
        private GolemType golemType;

        /// <summary>
        /// Defines the buyLimit.
        /// </summary>
        private uint buyLimit;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorGolem"/> class.
        /// </summary>
        public ActorGolem()
        {
            this.type = ActorType.GOLEM;
            this.Speed = (ushort)410;
            this.sightRange = 1500U;
            this.golemType = GolemType.None;
        }

        /// <summary>
        /// Gets or sets the Item.
        /// </summary>
        public SagaDB.Item.Item Item
        {
            get
            {
                return this.item;
            }
            set
            {
                this.item = value;
            }
        }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
            }
        }

        /// <summary>
        /// Gets or sets the Owner.
        /// </summary>
        public ActorPC Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }

        /// <summary>
        /// Gets or sets the GolemType.
        /// </summary>
        public GolemType GolemType
        {
            get
            {
                return this.golemType;
            }
            set
            {
                this.golemType = value;
            }
        }

        /// <summary>
        /// Gets or sets the BuyLimit.
        /// </summary>
        public uint BuyLimit
        {
            get
            {
                return this.buyLimit;
            }
            set
            {
                this.buyLimit = value;
            }
        }

        /// <summary>
        /// Gets the SellShop.
        /// </summary>
        public Dictionary<uint, GolemShopItem> SellShop
        {
            get
            {
                return this.sellShop;
            }
        }

        /// <summary>
        /// Gets the BuyShop.
        /// </summary>
        public Dictionary<uint, GolemShopItem> BuyShop
        {
            get
            {
                return this.buyShop;
            }
        }

        /// <summary>
        /// Gets the BoughtItem.
        /// </summary>
        public Dictionary<uint, GolemShopItem> BoughtItem
        {
            get
            {
                return this.boughtItem;
            }
        }

        /// <summary>
        /// Gets the SoldItem.
        /// </summary>
        public Dictionary<uint, GolemShopItem> SoldItem
        {
            get
            {
                return this.soldItem;
            }
        }
    }
}
