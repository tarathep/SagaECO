namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_COMMUNITY_RECRUIT" />.
    /// </summary>
    public class CSMG_COMMUNITY_RECRUIT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_COMMUNITY_RECRUIT"/> class.
        /// </summary>
        public CSMG_COMMUNITY_RECRUIT()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Type.
        /// </summary>
        public RecruitmentType Type
        {
            get
            {
                return (RecruitmentType)this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Page.
        /// </summary>
        public int Page
        {
            get
            {
                return this.GetInt((ushort)3);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_COMMUNITY_RECRUIT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnRecruit(this);
        }
    }
}
