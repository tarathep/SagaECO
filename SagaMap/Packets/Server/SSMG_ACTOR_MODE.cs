namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_MODE" />.
    /// </summary>
    public class SSMG_ACTOR_MODE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_MODE"/> class.
        /// </summary>
        public SSMG_ACTOR_MODE()
        {
            this.data = new byte[14];
            this.offset = (ushort)2;
            this.ID = (ushort)4007;
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
        /// Sets the Mode1.
        /// </summary>
        public int Mode1
        {
            set
            {
                this.PutInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Mode2.
        /// </summary>
        public int Mode2
        {
            set
            {
                this.PutInt(value, (ushort)10);
            }
        }
    }
}
