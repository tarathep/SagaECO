namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_IRIS_CARD_LOCK" />.
    /// </summary>
    public class CSMG_IRIS_CARD_LOCK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_IRIS_CARD_LOCK"/> class.
        /// </summary>
        public CSMG_IRIS_CARD_LOCK()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_IRIS_CARD_LOCK();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnIrisCardLock(this);
        }
    }
}
