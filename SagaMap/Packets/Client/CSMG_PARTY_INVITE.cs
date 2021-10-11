namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_PARTY_INVITE" />.
    /// </summary>
    public class CSMG_PARTY_INVITE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_PARTY_INVITE"/> class.
        /// </summary>
        public CSMG_PARTY_INVITE()
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
            return (Packet)new CSMG_PARTY_INVITE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnPartyInvite(this);
        }
    }
}
