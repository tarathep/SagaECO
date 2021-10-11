namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_COMMUNITY_RECRUIT_DELETE" />.
    /// </summary>
    public class CSMG_COMMUNITY_RECRUIT_DELETE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_COMMUNITY_RECRUIT_DELETE"/> class.
        /// </summary>
        public CSMG_COMMUNITY_RECRUIT_DELETE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_COMMUNITY_RECRUIT_DELETE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnRecruitDelete(this);
        }
    }
}
