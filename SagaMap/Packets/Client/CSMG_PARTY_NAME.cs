namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_PARTY_NAME" />.
    /// </summary>
    public class CSMG_PARTY_NAME : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_PARTY_NAME"/> class.
        /// </summary>
        public CSMG_PARTY_NAME()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return Global.Unicode.GetString(this.GetBytes((ushort)((uint)this.GetByte((ushort)2) - 1U), (ushort)3));
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_PARTY_NAME();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnPartyName(this);
        }
    }
}
