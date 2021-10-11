namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_GOLEM_SHOP_BUY_SETUP" />.
    /// </summary>
    public class CSMG_GOLEM_SHOP_BUY_SETUP : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_GOLEM_SHOP_BUY_SETUP"/> class.
        /// </summary>
        public CSMG_GOLEM_SHOP_BUY_SETUP()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the InventoryIDs.
        /// </summary>
        public uint[] InventoryIDs
        {
            get
            {
                uint[] numArray = new uint[(int)this.GetByte((ushort)2)];
                for (int index = 0; index < numArray.Length; ++index)
                    numArray[index] = this.GetUInt();
                return numArray;
            }
        }

        /// <summary>
        /// Gets the Counts.
        /// </summary>
        public ushort[] Counts
        {
            get
            {
                ushort[] numArray = new ushort[(int)this.GetByte((ushort)(3 + (int)this.GetByte((ushort)2) * 4))];
                for (int index = 0; index < numArray.Length; ++index)
                    numArray[index] = this.GetUShort();
                return numArray;
            }
        }

        /// <summary>
        /// Gets the Prices.
        /// </summary>
        public uint[] Prices
        {
            get
            {
                uint[] numArray = new uint[(int)this.GetByte((ushort)(4 + (int)this.GetByte((ushort)2) * 6))];
                for (int index = 0; index < numArray.Length; ++index)
                    numArray[index] = this.GetUInt();
                return numArray;
            }
        }

        /// <summary>
        /// Gets the BuyLimit.
        /// </summary>
        public uint BuyLimit
        {
            get
            {
                return this.GetUInt((ushort)(5 + (int)this.GetByte((ushort)2) * 10));
            }
        }

        /// <summary>
        /// Gets the Comment.
        /// </summary>
        public string Comment
        {
            get
            {
                byte num = this.GetByte((ushort)(9 + (int)this.GetByte((ushort)2) * 10));
                return Global.Unicode.GetString(this.GetBytes((ushort)num, (ushort)(10 + (int)this.GetByte((ushort)2) * 10))).Replace("\0", "");
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_GOLEM_SHOP_BUY_SETUP();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnGolemShopBuySetup(this);
        }
    }
}
