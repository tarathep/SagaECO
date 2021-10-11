namespace SagaMap.Packets.Client
{
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Network.Client;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="CSMG_GOLEM_SHOP_SELL_BUY" />.
    /// </summary>
    public class CSMG_GOLEM_SHOP_SELL_BUY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_GOLEM_SHOP_SELL_BUY"/> class.
        /// </summary>
        public CSMG_GOLEM_SHOP_SELL_BUY()
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
                    uint key = this.GetUInt((ushort)(7 + index * 4));
                    ushort num2 = this.GetUShort((ushort)(8 + (int)num1 * 4 + index * 2));
                    dictionary.Add(key, num2);
                }
                return dictionary;
            }
        }

        /// <summary>
        /// Gets the Container.
        /// </summary>
        public ContainerType Container
        {
            get
            {
                return (ContainerType)this.GetByte((ushort)(8 + (int)this.GetByte((ushort)6) * 6));
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_GOLEM_SHOP_SELL_BUY();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnGolemShopSellBuy(this);
        }
    }
}
