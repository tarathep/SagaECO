namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_SKILL_CHANGE_BATTLE_STATUS" />.
    /// </summary>
    public class CSMG_SKILL_CHANGE_BATTLE_STATUS : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_SKILL_CHANGE_BATTLE_STATUS"/> class.
        /// </summary>
        public CSMG_SKILL_CHANGE_BATTLE_STATUS()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Status.
        /// </summary>
        public byte Status
        {
            get
            {
                return this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_SKILL_CHANGE_BATTLE_STATUS();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnSkillChangeBattleStatus(this);
        }
    }
}
