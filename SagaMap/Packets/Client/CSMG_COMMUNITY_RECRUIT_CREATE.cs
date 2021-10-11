namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_COMMUNITY_RECRUIT_CREATE" />.
    /// </summary>
    public class CSMG_COMMUNITY_RECRUIT_CREATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_COMMUNITY_RECRUIT_CREATE"/> class.
        /// </summary>
        public CSMG_COMMUNITY_RECRUIT_CREATE()
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
        /// Gets the Title.
        /// </summary>
        public string Title
        {
            get
            {
                return Global.Unicode.GetString(this.GetBytes((ushort)this.GetByte((ushort)3), (ushort)4)).Replace("\0", "");
            }
        }

        /// <summary>
        /// Gets the Content.
        /// </summary>
        public string Content
        {
            get
            {
                byte num = this.GetByte((ushort)3);
                return Global.Unicode.GetString(this.GetBytes((ushort)this.GetByte((ushort)(4U + (uint)num)), (ushort)(5U + (uint)num))).Replace("\0", "");
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_COMMUNITY_RECRUIT_CREATE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnRecruitCreate(this);
        }
    }
}
