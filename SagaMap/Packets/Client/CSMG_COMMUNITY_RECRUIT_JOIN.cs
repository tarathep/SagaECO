namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_COMMUNITY_RECRUIT_JOIN" />.
    /// </summary>
    public class CSMG_COMMUNITY_RECRUIT_JOIN : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_COMMUNITY_RECRUIT_JOIN"/> class.
        /// </summary>
        public CSMG_COMMUNITY_RECRUIT_JOIN()
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
            return (Packet)new CSMG_COMMUNITY_RECRUIT_JOIN();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnRecruitJoin(this);
        }
    }
}
