namespace SagaDB.Synthese
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SyntheseInfo" />.
    /// </summary>
    public class SyntheseInfo
    {
        /// <summary>
        /// Defines the material.
        /// </summary>
        private List<ItemElement> material = new List<ItemElement>();

        /// <summary>
        /// Defines the product.
        /// </summary>
        private List<ItemElement> product = new List<ItemElement>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the skillid.
        /// </summary>
        private ushort skillid;

        /// <summary>
        /// Defines the skilllv.
        /// </summary>
        private byte skilllv;

        /// <summary>
        /// Defines the gold.
        /// </summary>
        private uint gold;

        /// <summary>
        /// Defines the requiredTool.
        /// </summary>
        private uint requiredTool;

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
        /// Gets or sets the SkillID.
        /// </summary>
        public ushort SkillID
        {
            get
            {
                return this.skillid;
            }
            set
            {
                this.skillid = value;
            }
        }

        /// <summary>
        /// Gets or sets the SkillLv.
        /// </summary>
        public byte SkillLv
        {
            get
            {
                return this.skilllv;
            }
            set
            {
                this.skilllv = value;
            }
        }

        /// <summary>
        /// Gets or sets the Gold.
        /// </summary>
        public uint Gold
        {
            get
            {
                return this.gold;
            }
            set
            {
                this.gold = value;
            }
        }

        /// <summary>
        /// Gets or sets the RequiredTool.
        /// </summary>
        public uint RequiredTool
        {
            get
            {
                return this.requiredTool;
            }
            set
            {
                this.requiredTool = value;
            }
        }

        /// <summary>
        /// Gets the Materials.
        /// </summary>
        public List<ItemElement> Materials
        {
            get
            {
                return this.material;
            }
        }

        /// <summary>
        /// Gets the Products.
        /// </summary>
        public List<ItemElement> Products
        {
            get
            {
                return this.product;
            }
        }
    }
}
