namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="CSMG_GOLEM_SHOP_BUY_SELL" />.
    /// </summary>
    public class CSMG_GOLEM_SHOP_BUY_SELL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_GOLEM_SHOP_BUY_SELL"/> class.
        /// </summary>
        public CSMG_GOLEM_SHOP_BUY_SELL()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the ActorID.
        /// </summary>
        public uint ActorID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Items.
        /// </summary>
        public Dictionary<uint, ushort> Items
        {
            get
            {
                byte num1 = this.GetByte((ushort)6);
                Dictionary<uint, ushort> dictionary = new Dictionary<uint, ushort>();
                for (int index = 0; index < (int)num1; ++index)
                {
                    uint key = this.GetUInt((ushort)(8 + (int)num1 * 4 + index * 4));
                    ushort num2 = this.GetUShort((ushort)(9 + (int)num1 * 8 + index * 2));
                    dictionary.Add(key, num2);
                }
                return dictionary;
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_GOLEM_SHOP_BUY_SELL();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnGolemShopBuySell(this);
        }
    }
}
