namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_ENHANCE_DETAIL" />.
    /// </summary>
    public class SSMG_ITEM_ENHANCE_DETAIL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_ENHANCE_DETAIL"/> class.
        /// </summary>
        public SSMG_ITEM_ENHANCE_DETAIL()
        {
            this.data = new byte[5];
            this.offset = (ushort)2;
            this.ID = (ushort)5062;
        }

        /// <summary>
        /// Sets the Items.
        /// </summary>
        public List<EnhanceDetail> Items
        {
            set
            {
                byte[] numArray = new byte[5 + 8 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count, (ushort)2);
                this.PutByte((byte)value.Count, (ushort)(3 + 4 * value.Count));
                this.PutByte((byte)value.Count, (ushort)(4 + 6 * value.Count));
                int num = 0;
                foreach (EnhanceDetail enhanceDetail in value)
                {
                    this.PutUInt(enhanceDetail.material, (ushort)(3 + 4 * num));
                    this.PutShort((short)enhanceDetail.type, (ushort)(4 + 4 * value.Count + 2 * num));
                    this.PutShort(enhanceDetail.value, (ushort)(5 + 6 * value.Count + 2 * num));
                    ++num;
                }
            }
        }
    }
}
