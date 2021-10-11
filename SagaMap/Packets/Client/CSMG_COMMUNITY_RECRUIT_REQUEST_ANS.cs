namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_COMMUNITY_RECRUIT_REQUEST_ANS" />.
    /// </summary>
    public class CSMG_COMMUNITY_RECRUIT_REQUEST_ANS : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_COMMUNITY_RECRUIT_REQUEST_ANS"/> class.
        /// </summary>
        public CSMG_COMMUNITY_RECRUIT_REQUEST_ANS()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets a value indicating whether Accept.
        /// </summary>
        public bool Accept
        {
            get
            {
                return this.GetUInt((ushort)2) == 1U;
            }
        }

        /// <summary>
        /// Gets the CharID.
        /// </summary>
        public uint CharID
        {
            get
            {
                return this.GetUInt((ushort)6);
            }
        }

        /// <summary>
        /// Gets the CharName.
        /// </summary>
        public string CharName
        {
            get
            {
                return Global.Unicode.GetString(this.GetBytes((ushort)this.GetByte((ushort)10), (ushort)11));
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_COMMUNITY_RECRUIT_REQUEST_ANS();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnRecruitRequestAns(this);
        }
    }
}
