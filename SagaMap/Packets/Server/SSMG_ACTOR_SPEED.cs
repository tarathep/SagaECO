namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_SPEED" />.
    /// </summary>
    public class SSMG_ACTOR_SPEED : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_SPEED"/> class.
        /// </summary>
        public SSMG_ACTOR_SPEED()
        {
            this.data = new byte[8];
            this.offset = (ushort)2;
            this.ID = (ushort)4665;
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
        /// Sets the Speed.
        /// </summary>
        public ushort Speed
        {
            set
            {
                this.PutUShort(value, (ushort)6);
            }
        }
    }
}
