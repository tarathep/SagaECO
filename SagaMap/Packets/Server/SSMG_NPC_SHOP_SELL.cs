namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SHOP_SELL" />.
    /// </summary>
    public class SSMG_NPC_SHOP_SELL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SHOP_SELL"/> class.
        /// </summary>
        public SSMG_NPC_SHOP_SELL()
        {
            this.data = new byte[15];
            this.offset = (ushort)2;
            this.ID = (ushort)1557;
        }

        /// <summary>
        /// Sets the Rate.
        /// </summary>
        public uint Rate
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the ShopLimit.
        /// </summary>
        public uint ShopLimit
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Bank.
        /// </summary>
        public uint Bank
        {
            set
            {
                this.PutUInt(value, (ushort)10);
            }
        }
    }
}
