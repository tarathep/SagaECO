namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SHOP_OPEN_FAILED" />.
    /// </summary>
    public class SSMG_GOLEM_SHOP_OPEN_FAILED : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SHOP_OPEN_FAILED"/> class.
        /// </summary>
        public SSMG_GOLEM_SHOP_OPEN_FAILED()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)6141;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public int Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)3);
            }
        }
    }
}
