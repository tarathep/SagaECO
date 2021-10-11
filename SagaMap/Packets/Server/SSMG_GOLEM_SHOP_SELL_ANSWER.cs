namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SHOP_SELL_ANSWER" />.
    /// </summary>
    public class SSMG_GOLEM_SHOP_SELL_ANSWER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SHOP_SELL_ANSWER"/> class.
        /// </summary>
        public SSMG_GOLEM_SHOP_SELL_ANSWER()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6148;
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
    }
}
