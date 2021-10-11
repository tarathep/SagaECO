namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_INFO_UPDATE" />.
    /// </summary>
    public class SSMG_RING_INFO_UPDATE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_INFO_UPDATE"/> class.
        /// </summary>
        public SSMG_RING_INFO_UPDATE()
        {
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)6872;
        }

        /// <summary>
        /// Sets the RingID.
        /// </summary>
        public uint RingID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Fame.
        /// </summary>
        public uint Fame
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the CurrentMember.
        /// </summary>
        public byte CurrentMember
        {
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the MaxMember.
        /// </summary>
        public byte MaxMember
        {
            set
            {
                this.PutByte(value, (ushort)11);
            }
        }
    }
}
