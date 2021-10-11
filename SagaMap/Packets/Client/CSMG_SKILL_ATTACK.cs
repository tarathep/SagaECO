namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_SKILL_ATTACK" />.
    /// </summary>
    public class CSMG_SKILL_ATTACK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_SKILL_ATTACK"/> class.
        /// </summary>
        public CSMG_SKILL_ATTACK()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the ActorID.
        /// </summary>
        public uint ActorID
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Random.
        /// </summary>
        public short Random
        {
            get
            {
                return this.GetShort((ushort)6);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_SKILL_ATTACK();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnSkillAttack(this);
        }
    }
}
