namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_INVITE_ANSWER_RESULT" />.
    /// </summary>
    public class SSMG_RING_INVITE_ANSWER_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_INVITE_ANSWER_RESULT"/> class.
        /// </summary>
        public SSMG_RING_INVITE_ANSWER_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6835;
        }

        /// <summary>
        /// Sets the Resault.
        /// </summary>
        public SSMG_RING_INVITE_ANSWER_RESULT.Resaults Resault
        {
            set
            {
                this.PutInt((int)value, (ushort)2);
            }
        }

        /// <summary>
        /// Defines the Resaults.
        /// </summary>
        public enum Resaults
        {
            /// <summary>
            /// Defines the MEMBER_EXCEED.
            /// </summary>
            MEMBER_EXCEED = -12, // -0x0000000C

            /// <summary>
            /// Defines the ALREADY_IN_RING.
            /// </summary>
            ALREADY_IN_RING = -11, // -0x0000000B

            /// <summary>
            /// Defines the CANNOT_FIND_TARGET.
            /// </summary>
            CANNOT_FIND_TARGET = -2,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 2,
        }
    }
}
