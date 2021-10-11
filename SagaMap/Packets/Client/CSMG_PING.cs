namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_PING" />.
    /// </summary>
    public class CSMG_PING : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_PING"/> class.
        /// </summary>
        public CSMG_PING()
        {
            this.size = 2U;
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_PING();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnPing(this);
        }
    }
}
