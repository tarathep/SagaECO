namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_INVITE_RESULT" />.
    /// </summary>
    public class SSMG_RING_INVITE_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_INVITE_RESULT"/> class.
        /// </summary>
        public SSMG_RING_INVITE_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6835;
        }

        /// <summary>
        /// Sets the Resault.
        /// </summary>
        public SSMG_RING_INVITE_RESULT.Resaults Resault
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
            /// Defines the NO_RIGHT.
            /// </summary>
            NO_RIGHT = -6,

            /// <summary>
            /// Defines the NO_RING.
            /// </summary>
            NO_RING = -5,

            /// <summary>
            /// Defines the TARGET_ALREADY_IN_RING.
            /// </summary>
            TARGET_ALREADY_IN_RING = -4,

            /// <summary>
            /// Defines the TARGET_NO_RING_INVITE.
            /// </summary>
            TARGET_NO_RING_INVITE = -3,

            /// <summary>
            /// Defines the SERVER_ERROR.
            /// </summary>
            SERVER_ERROR = -2,

            /// <summary>
            /// Defines the CANNOT_FIND_TARGET.
            /// </summary>
            CANNOT_FIND_TARGET = -1,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
