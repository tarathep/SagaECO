namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_NPC_SYNTHESE" />.
    /// </summary>
    public class CSMG_NPC_SYNTHESE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_NPC_SYNTHESE"/> class.
        /// </summary>
        public CSMG_NPC_SYNTHESE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the SynID.
        /// </summary>
        public uint SynID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Count.
        /// </summary>
        public uint Count
        {
            get
            {
                return this.GetUInt((ushort)8);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_NPC_SYNTHESE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnNPCSynthese(this);
        }
    }
}
