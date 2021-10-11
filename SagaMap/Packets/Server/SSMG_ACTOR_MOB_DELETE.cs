namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_MOB_DELETE" />.
    /// </summary>
    public class SSMG_ACTOR_MOB_DELETE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_MOB_DELETE"/> class.
        /// </summary>
        public SSMG_ACTOR_MOB_DELETE()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)4645;
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
