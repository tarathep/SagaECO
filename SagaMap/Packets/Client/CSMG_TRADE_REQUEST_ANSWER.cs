namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_TRADE_REQUEST_ANSWER" />.
    /// </summary>
    public class CSMG_TRADE_REQUEST_ANSWER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_TRADE_REQUEST_ANSWER"/> class.
        /// </summary>
        public CSMG_TRADE_REQUEST_ANSWER()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Answer.
        /// </summary>
        public byte Answer
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
            return (Packet)new CSMG_TRADE_REQUEST_ANSWER();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnTradeRequestAnswer(this);
        }
    }
}
