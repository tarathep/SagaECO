namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_GOLEM_SHOP_BUY" />.
    /// </summary>
    public class CSMG_GOLEM_SHOP_BUY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_GOLEM_SHOP_BUY"/> class.
        /// </summary>
        public CSMG_GOLEM_SHOP_BUY()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_GOLEM_SHOP_BUY();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnGolemShopBuy(this);
        }
    }
}
