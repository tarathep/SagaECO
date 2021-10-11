namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_RING_RIGHT_SET" />.
    /// </summary>
    public class CSMG_RING_RIGHT_SET : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_RING_RIGHT_SET"/> class.
        /// </summary>
        public CSMG_RING_RIGHT_SET()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the CharID.
        /// </summary>
        public uint CharID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Right.
        /// </summary>
        public int Right
        {
            get
            {
                return this.GetInt((ushort)6);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_RING_RIGHT_SET();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnRingRightSet(this);
        }
    }
}
