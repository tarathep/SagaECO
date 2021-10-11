namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_RING_EMBLEM_UPLOAD" />.
    /// </summary>
    public class CSMG_RING_EMBLEM_UPLOAD : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_RING_EMBLEM_UPLOAD"/> class.
        /// </summary>
        public CSMG_RING_EMBLEM_UPLOAD()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Data.
        /// </summary>
        public byte[] Data
        {
            get
            {
                if (this.GetInt((ushort)3) == 253)
                    return this.GetBytes((ushort)this.GetInt((ushort)7), (ushort)11);
                return this.GetBytes((ushort)this.GetInt((ushort)3), (ushort)7);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_RING_EMBLEM_UPLOAD();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnRingEmblemUpload(this);
        }
    }
}
