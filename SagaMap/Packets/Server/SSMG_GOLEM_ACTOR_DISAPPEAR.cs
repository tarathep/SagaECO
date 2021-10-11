namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_ACTOR_DISAPPEAR" />.
    /// </summary>
    public class SSMG_GOLEM_ACTOR_DISAPPEAR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_ACTOR_DISAPPEAR"/> class.
        /// </summary>
        public SSMG_GOLEM_ACTOR_DISAPPEAR()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6101;
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
    }
}
