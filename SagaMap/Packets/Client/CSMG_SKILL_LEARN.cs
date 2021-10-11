namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_SKILL_LEARN" />.
    /// </summary>
    public class CSMG_SKILL_LEARN : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_SKILL_LEARN"/> class.
        /// </summary>
        public CSMG_SKILL_LEARN()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the SkillID.
        /// </summary>
        public ushort SkillID
        {
            get
            {
                return this.GetUShort((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_SKILL_LEARN();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnSkillLearn(this);
        }
    }
}
