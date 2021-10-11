namespace SagaMap.Packets.Server
{
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_IRIS_ADD_SLOT_ITEM_LIST" />.
    /// </summary>
    public class SSMG_IRIS_ADD_SLOT_ITEM_LIST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_IRIS_ADD_SLOT_ITEM_LIST"/> class.
        /// </summary>
        public SSMG_IRIS_ADD_SLOT_ITEM_LIST()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)5090;
        }

        /// <summary>
        /// Sets the Items.
        /// </summary>
        public List<uint> Items
        {
            set
            {
                this.offset = (ushort)2;
                byte[] numArray = new byte[3 + 4 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count);
                foreach (uint s in value)
                    this.PutUInt(s);
            }
        }
    }
}
