namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_ENHANCE_LIST" />.
    /// </summary>
    public class SSMG_ITEM_ENHANCE_LIST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_ENHANCE_LIST"/> class.
        /// </summary>
        public SSMG_ITEM_ENHANCE_LIST()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)5060;
        }

        /// <summary>
        /// Sets the Items.
        /// </summary>
        public List<SagaDB.Item.Item> Items
        {
            set
            {
                byte[] numArray = new byte[4 + 4 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count, (ushort)2);
                int num = 0;
                foreach (SagaDB.Item.Item obj in value)
                {
                    this.PutUInt(obj.Slot, (ushort)(3 + 4 * num));
                    ++num;
                }
            }
        }
    }
}
