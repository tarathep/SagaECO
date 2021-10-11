namespace SagaMap.Packets.Server
{
    using SagaDB.ECOShop;
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_VSHOP_CATEGORY" />.
    /// </summary>
    public class SSMG_VSHOP_CATEGORY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_VSHOP_CATEGORY"/> class.
        /// </summary>
        public SSMG_VSHOP_CATEGORY()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)1600;
        }

        /// <summary>
        /// Sets the CurrentPoint.
        /// </summary>
        public uint CurrentPoint
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Categories.
        /// </summary>
        public Dictionary<uint, ShopCategory> Categories
        {
            set
            {
                int num = 0;
                int index = 0;
                byte[][] numArray1 = new byte[value.Count][];
                foreach (ShopCategory shopCategory in value.Values)
                {
                    numArray1[index] = Global.Unicode.GetBytes(shopCategory.Name);
                    num += numArray1[index].Length + 1;
                    ++index;
                }
                byte[] numArray2 = new byte[7 + num];
                this.data.CopyTo((Array)numArray2, 0);
                this.data = numArray2;
                this.offset = (ushort)6;
                this.PutByte((byte)numArray1.Length);
                foreach (byte[] bdata in numArray1)
                {
                    this.PutByte((byte)bdata.Length);
                    this.PutBytes(bdata);
                }
            }
        }
    }
}
