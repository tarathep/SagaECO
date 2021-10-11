namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_TRADE_PERFORM" />.
    /// </summary>
    public class CSMG_TRADE_PERFORM : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_TRADE_PERFORM"/> class.
        /// </summary>
        public CSMG_TRADE_PERFORM()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the State.
        /// </summary>
        public byte State
        {
            get
            {
                return this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_TRADE_PERFORM();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnTradePerform(this);
        }
    }
}
