namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_RING_EMBLEM_UPLOAD_RESULT" />.
    /// </summary>
    public class SSMG_RING_EMBLEM_UPLOAD_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_RING_EMBLEM_UPLOAD_RESULT"/> class.
        /// </summary>
        public SSMG_RING_EMBLEM_UPLOAD_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6876;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_RING_EMBLEM_UPLOAD_RESULT.Results Result
        {
            set
            {
                this.PutInt((int)value, (ushort)2);
            }
        }

        /// <summary>
        /// Defines the Results.
        /// </summary>
        public enum Results
        {
            /// <summary>
            /// Defines the FAME_NOT_ENOUGH.
            /// </summary>
            FAME_NOT_ENOUGH = -3,

            /// <summary>
            /// Defines the WRONG_FORMAT.
            /// </summary>
            WRONG_FORMAT = -2,

            /// <summary>
            /// Defines the SERVER_ERROR.
            /// </summary>
            SERVER_ERROR = -1,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
