namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_COMMUNITY_BBS_REQUEST_PAGE" />.
    /// </summary>
    public class CSMG_COMMUNITY_BBS_REQUEST_PAGE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_COMMUNITY_BBS_REQUEST_PAGE"/> class.
        /// </summary>
        public CSMG_COMMUNITY_BBS_REQUEST_PAGE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Page.
        /// </summary>
        public int Page
        {
            get
            {
                return this.GetInt((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_COMMUNITY_BBS_REQUEST_PAGE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnBBSRequestPage(this);
        }
    }
}
