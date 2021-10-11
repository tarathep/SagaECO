namespace SagaMap.Packets.Server
{
    using SagaDB.DEMIC;
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_CHIP_SHOP_CATEGORY" />.
    /// </summary>
    public class SSMG_DEM_CHIP_SHOP_CATEGORY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_CHIP_SHOP_CATEGORY"/> class.
        /// </summary>
        public SSMG_DEM_CHIP_SHOP_CATEGORY()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)1590;
        }

        /// <summary>
        /// Sets the Categories.
        /// </summary>
        public List<ChipShopCategory> Categories
        {
            set
            {
                byte[][] numArray1 = new byte[value.Count][];
                int index = 0;
                int num = 0;
                foreach (ChipShopCategory chipShopCategory in value)
                {
                    numArray1[index] = Global.Unicode.GetBytes(chipShopCategory.Name);
                    num += numArray1[index].Length + 1;
                    ++index;
                }
                byte[] numArray2 = new byte[4 + value.Count * 4 + num];
                this.data.CopyTo((Array)numArray2, 0);
                this.data = numArray2;
                this.offset = (ushort)2;
                this.PutByte((byte)value.Count);
                foreach (ChipShopCategory chipShopCategory in value)
                    this.PutUInt(chipShopCategory.ID);
                this.PutByte((byte)value.Count);
                foreach (byte[] bdata in numArray1)
                {
                    this.PutByte((byte)bdata.Length);
                    this.PutBytes(bdata);
                }
            }
        }
    }
}
