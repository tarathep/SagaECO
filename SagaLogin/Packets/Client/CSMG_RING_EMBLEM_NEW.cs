namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_RING_EMBLEM_NEW" />.
    /// </summary>
    public class CSMG_RING_EMBLEM_NEW : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_RING_EMBLEM_NEW"/> class.
        /// </summary>
        public CSMG_RING_EMBLEM_NEW()
        {
            this.size = 6U;
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the RingID.
        /// </summary>
        public uint RingID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_RING_EMBLEM_NEW();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnRingEmblemNew(this);
        }
    }
}
