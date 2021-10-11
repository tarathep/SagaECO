namespace SagaDB.DEMIC
{
    /// <summary>
    /// Defines the <see cref="ShopChip" />.
    /// </summary>
    public class ShopChip
    {
        /// <summary>
        /// Defines the itemID.
        /// </summary>
        private uint itemID;

        /// <summary>
        /// Defines the exp.
        /// </summary>
        private uint exp;

        /// <summary>
        /// Defines the jexp.
        /// </summary>
        private uint jexp;

        /// <summary>
        /// Defines the desc.
        /// </summary>
        private string desc;

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
        /// Gets or sets the EXP.
        /// </summary>
        public uint EXP
        {
            get
            {
                return this.exp;
            }
            set
            {
                this.exp = value;
            }
        }

        /// <summary>
        /// Gets or sets the JEXP.
        /// </summary>
        public uint JEXP
        {
            get
            {
                return this.jexp;
            }
            set
            {
                this.jexp = value;
            }
        }

        /// <summary>
        /// Gets or sets the Description.
        /// </summary>
        public string Description
        {
            get
            {
                return this.desc;
            }
            set
            {
                this.desc = value;
            }
        }
    }
}
