namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_SKILL_MOVE" />.
    /// </summary>
    public class SSMG_ACTOR_SKILL_MOVE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_SKILL_MOVE"/> class.
        /// </summary>
        public SSMG_ACTOR_SKILL_MOVE()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)5035;
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
        /// Sets the X.
        /// </summary>
        public short X
        {
            set
            {
                this.PutShort(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public short Y
        {
            set
            {
                this.PutShort(value, (ushort)8);
            }
        }
    }
}
