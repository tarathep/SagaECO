namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_RIGHT_UPDATE" />.
    /// </summary>
    public class SSMG_RING_RIGHT_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_RIGHT_UPDATE"/> class.
        /// </summary>
        public SSMG_RING_RIGHT_UPDATE()
        {
            this.data = new byte[14];
            this.offset = (ushort)2;
            this.ID = (ushort)6871;
        }

        /// <summary>
        /// Sets the Unknown.
        /// </summary>
        public uint Unknown
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the CharID.
        /// </summary>
        public uint CharID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Right.
        /// </summary>
        public int Right
        {
            set
            {
                this.PutInt(value, (ushort)10);
            }
        }
    }
}
