namespace SagaLogin.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_FRIEND_ADD_FAILED" />.
    /// </summary>
    public class SSMG_FRIEND_ADD_FAILED : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FRIEND_ADD_FAILED"/> class.
        /// </summary>
        public SSMG_FRIEND_ADD_FAILED()
        {
            this.data = new byte[6];
            this.ID = (ushort)214;
        }

        /// <summary>
        /// Sets the AddResult.
        /// </summary>
        public SSMG_FRIEND_ADD_FAILED.Result AddResult
        {
            set
            {
                this.PutUInt((uint)value, (ushort)2);
            }
        }

        /// <summary>
        /// Defines the Result.
        /// </summary>
        public enum Result
        {
            /// <summary>
            /// Defines the CANNOT_FIND_TARGET.
            /// </summary>
            CANNOT_FIND_TARGET = -5,

            /// <summary>
            /// Defines the TARGET_REFUSED.
            /// </summary>
            TARGET_REFUSED = -3,

            /// <summary>
            /// Defines the NO_FREE_SPACE.
            /// </summary>
            NO_FREE_SPACE = -2,

            /// <summary>
            /// Defines the TARGET_NO_FREE_SPACE.
            /// </summary>
            TARGET_NO_FREE_SPACE = -1,
        }
    }
}
