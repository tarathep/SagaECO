namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_PARTY_QUIT" />.
    /// </summary>
    public class CSMG_PARTY_QUIT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_PARTY_QUIT"/> class.
        /// </summary>
        public CSMG_PARTY_QUIT()
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
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_PARTY_QUIT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnPartyQuit(this);
        }
    }
}
