namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;
    using System;

    /// <summary>
    /// Defines the <see cref="CSMG_RING_EMBLEM" />.
    /// </summary>
    public class CSMG_RING_EMBLEM : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_RING_EMBLEM"/> class.
        /// </summary>
        public CSMG_RING_EMBLEM()
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
        /// Gets the UpdateTime.
        /// </summary>
        public DateTime UpdateTime
        {
            get
            {
                return new DateTime(1970, 1, 1) + TimeSpan.FromSeconds((double)this.GetUInt((ushort)6));
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_RING_EMBLEM();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnRingEmblem(this);
        }
    }
}
