namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_ATTACK_TYPE" />.
    /// </summary>
    public class SSMG_ACTOR_ATTACK_TYPE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_ATTACK_TYPE"/> class.
        /// </summary>
        public SSMG_ACTOR_ATTACK_TYPE()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)4031;
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
        /// Sets the AttackType.
        /// </summary>
        public ATTACK_TYPE AttackType
        {
            set
            {
                this.PutByte((byte)value, (ushort)6);
            }
        }
    }
}
