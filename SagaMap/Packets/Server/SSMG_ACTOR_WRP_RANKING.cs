namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_WRP_RANKING" />.
    /// </summary>
    public class SSMG_ACTOR_WRP_RANKING : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_WRP_RANKING"/> class.
        /// </summary>
        public SSMG_ACTOR_WRP_RANKING()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)566;
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
        /// Sets the Ranking.
        /// </summary>
        public uint Ranking
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }
    }
}
