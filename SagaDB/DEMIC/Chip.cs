namespace SagaDB.DEMIC
{
    /// <summary>
    /// Defines the <see cref="Chip" />.
    /// </summary>
    public class Chip
    {
        /// <summary>
        /// Defines the data.
        /// </summary>
        private Chip.BaseData data;

        /// <summary>
        /// Defines the x.
        /// </summary>
        private byte x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        private byte y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Chip"/> class.
        /// </summary>
        /// <param name="baseData">The baseData<see cref="Chip.BaseData"/>.</param>
        public Chip(Chip.BaseData baseData)
        {
            this.data = baseData;
        }

        /// <summary>
        /// Gets the ChipID.
        /// </summary>
        public short ChipID
        {
            get
            {
                return this.data.chipID;
            }
        }

        /// <summary>
        /// Gets the ItemID.
        /// </summary>
        public uint ItemID
        {
            get
            {
                return this.data.itemID;
            }
        }

        /// <summary>
        /// Gets the Data.
        /// </summary>
        public Chip.BaseData Data
        {
            get
            {
                return this.data;
            }
        }

        /// <summary>
        /// Gets the Model.
        /// </summary>
        public Model Model
        {
            get
            {
                return this.data.model;
            }
        }

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        public byte X
        {
            get
            {
                return this.x;
            }
            set
            {
                this.x = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        public byte Y
        {
            get
            {
                return this.y;
            }
            set
            {
                this.y = value;
            }
        }

        /// <summary>
        /// The IsNear.
        /// </summary>
        /// <param name="x">The x<see cref="byte"/>.</param>
        /// <param name="y">The y<see cref="byte"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsNear(byte x, byte y)
        {
            foreach (byte[] cell in this.Model.Cells)
            {
                int num1 = (int)this.x + (int)cell[0] - (int)this.Model.CenterX;
                int num2 = (int)this.y + (int)cell[1] - (int)this.Model.CenterY;
                if (num1 == (int)x + 1 && num2 == (int)y || num1 == (int)x - 1 && num2 == (int)y || (num1 == (int)x && num2 == (int)y + 1 || num1 == (int)x && num2 == (int)y - 1))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.data.name;
        }

        /// <summary>
        /// Defines the <see cref="BaseData" />.
        /// </summary>
        public class BaseData
        {
            /// <summary>
            /// Defines the chipID.
            /// </summary>
            public short chipID;

            /// <summary>
            /// Defines the name.
            /// </summary>
            public string name;

            /// <summary>
            /// Defines the itemID.
            /// </summary>
            public uint itemID;

            /// <summary>
            /// Defines the type.
            /// </summary>
            public byte type;

            /// <summary>
            /// Defines the model.
            /// </summary>
            public Model model;

            /// <summary>
            /// Defines the possibleLv.
            /// </summary>
            public byte possibleLv;

            /// <summary>
            /// Defines the engageTaskChip.
            /// </summary>
            public short engageTaskChip;

            /// <summary>
            /// Defines the hp.
            /// </summary>
            public short hp;

            /// <summary>
            /// Defines the mp.
            /// </summary>
            public short mp;

            /// <summary>
            /// Defines the sp.
            /// </summary>
            public short sp;

            /// <summary>
            /// Defines the str.
            /// </summary>
            public short str;

            /// <summary>
            /// Defines the mag.
            /// </summary>
            public short mag;

            /// <summary>
            /// Defines the vit.
            /// </summary>
            public short vit;

            /// <summary>
            /// Defines the dex.
            /// </summary>
            public short dex;

            /// <summary>
            /// Defines the agi.
            /// </summary>
            public short agi;

            /// <summary>
            /// Defines the intel.
            /// </summary>
            public short intel;

            /// <summary>
            /// Defines the skill1.
            /// </summary>
            public uint skill1;

            /// <summary>
            /// Defines the skill2.
            /// </summary>
            public uint skill2;

            /// <summary>
            /// Defines the skill3.
            /// </summary>
            public uint skill3;

            /// <summary>
            /// The ToString.
            /// </summary>
            /// <returns>The <see cref="string"/>.</returns>
            public override string ToString()
            {
                return this.name;
            }
        }
    }
}
