namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_DEM_COST_LIMIT_BUY" />.
    /// </summary>
    public class CSMG_DEM_COST_LIMIT_BUY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_DEM_COST_LIMIT_BUY"/> class.
        /// </summary>
        public CSMG_DEM_COST_LIMIT_BUY()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the EP.
        /// </summary>
        public short EP
        {
            get
            {
                return this.GetShort((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_DEM_COST_LIMIT_BUY();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnDEMCostLimitBuy(this);
        }
    }
}
