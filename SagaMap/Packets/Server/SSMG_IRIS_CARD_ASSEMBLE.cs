namespace SagaMap.Packets.Server
{
    using SagaDB.Iris;
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_IRIS_CARD_ASSEMBLE" />.
    /// </summary>
    public class SSMG_IRIS_CARD_ASSEMBLE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_IRIS_CARD_ASSEMBLE"/> class.
        /// </summary>
        public SSMG_IRIS_CARD_ASSEMBLE()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)5130;
        }

        /// <summary>
        /// Sets the CardAndPrice.
        /// </summary>
        public Dictionary<IrisCard, int> CardAndPrice
        {
            set
            {
                byte[] numArray = new byte[7 + 4 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count, (ushort)2);
                int s = 0;
                foreach (IrisCard key in value.Keys)
                {
                    s = value[key];
                    this.PutUInt(key.ID);
                }
                this.PutInt(s);
            }
        }
    }
}
