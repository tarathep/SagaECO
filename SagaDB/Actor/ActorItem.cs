namespace SagaDB.Actor
{
    using System;

    /// <summary>
    /// Defines the <see cref="ActorItem" />.
    /// </summary>
    public class ActorItem : SagaDB.Actor.Actor
    {
        /// <summary>
        /// Defines the lootedBy.
        /// </summary>
        private uint lootedBy = uint.MaxValue;

        /// <summary>
        /// Defines the createTime.
        /// </summary>
        private DateTime createTime = DateTime.Now;

        /// <summary>
        /// Defines the item.
        /// </summary>
        private SagaDB.Item.Item item;

        /// <summary>
        /// Defines the comment.
        /// </summary>
        private string comment;

        /// <summary>
        /// Defines the owner.
        /// </summary>
        private SagaDB.Actor.Actor owner;

        /// <summary>
        /// Defines the party.
        /// </summary>
        private bool party;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorItem"/> class.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.Item.Item"/>.</param>
        public ActorItem(SagaDB.Item.Item item)
        {
            this.item = item;
            this.Name = item.BaseData.name;
            this.type = ActorType.ITEM;
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
        /// Gets a value indicating whether PossessionItem.
        /// </summary>
        public bool PossessionItem
        {
            get
            {
                return this.item.PossessionedActor != null;
            }
        }

        /// <summary>
        /// Gets or sets the Comment.
        /// </summary>
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.comment = value;
            }
        }

        /// <summary>
        /// Gets or sets the LootedBy.
        /// </summary>
        public uint LootedBy
        {
            get
            {
                return this.lootedBy;
            }
            set
            {
                this.lootedBy = value;
            }
        }

        /// <summary>
        /// Gets or sets the Owner.
        /// </summary>
        public SagaDB.Actor.Actor Owner
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
        /// Gets or sets the CreateTime.
        /// </summary>
        public DateTime CreateTime
        {
            get
            {
                return this.createTime;
            }
            set
            {
                this.createTime = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Party.
        /// </summary>
        public bool Party
        {
            get
            {
                return this.party;
            }
            set
            {
                this.party = value;
            }
        }
    }
}
