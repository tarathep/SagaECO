namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_VSHOP_BUY" />.
    /// </summary>
    public class CSMG_VSHOP_BUY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_VSHOP_BUY"/> class.
        /// </summary>
        public CSMG_VSHOP_BUY()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Items.
        /// </summary>
        public uint[] Items
        {
            get
            {
                byte num = this.GetByte((ushort)2);
                uint[] numArray = new uint[(int)num];
                for (int index = 0; index < (int)num; ++index)
                    numArray[index] = this.GetUInt((ushort)(3 + index * 4));
                return numArray;
            }
        }

        /// <summary>
        /// Gets the Counts.
        /// </summary>
        public uint[] Counts
        {
            get
            {
                byte num = this.GetByte((ushort)2);
                uint[] numArray = new uint[(int)num];
                for (int index = 0; index < (int)num; ++index)
                    numArray[index] = this.GetUInt((ushort)(4 + (int)num * 4 + index * 4));
                return numArray;
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_VSHOP_BUY();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnVShopBuy(this);
        }
    }
}
