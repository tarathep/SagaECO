namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_SKILL_CHANGE_BATTLE_STATUS" />.
    /// </summary>
    public class SSMG_SKILL_CHANGE_BATTLE_STATUS : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_SKILL_CHANGE_BATTLE_STATUS"/> class.
        /// </summary>
        public SSMG_SKILL_CHANGE_BATTLE_STATUS()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)4006;
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Status.
        /// </summary>
        public byte Status
        {
            set
            {
                this.PutByte(value, (ushort)6);
            }
        }
    }
}
