namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_NPC_SHOP_CLOSE" />.
    /// </summary>
    public class CSMG_NPC_SHOP_CLOSE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_NPC_SHOP_CLOSE"/> class.
        /// </summary>
        public CSMG_NPC_SHOP_CLOSE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_NPC_SHOP_CLOSE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnNPCShopClose(this);
        }
    }
}
