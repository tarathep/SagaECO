namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_QUIT" />.
    /// </summary>
    public class SSMG_RING_QUIT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_QUIT"/> class.
        /// </summary>
        public SSMG_RING_QUIT()
        {
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)6861;
            this.PutByte((byte)1, (ushort)6);
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
        /// Sets the Reason.
        /// </summary>
        public SSMG_RING_QUIT.Reasons Reason
        {
            set
            {
                this.PutInt((int)value, (ushort)8);
            }
        }

        /// <summary>
        /// Defines the Reasons.
        /// </summary>
        public enum Reasons
        {
            /// <summary>
            /// Defines the DISSOLVE.
            /// </summary>
            DISSOLVE = 1,

            /// <summary>
            /// Defines the LEAVE.
            /// </summary>
            LEAVE = 2,

            /// <summary>
            /// Defines the KICK.
            /// </summary>
            KICK = 3,
        }
    }
}
