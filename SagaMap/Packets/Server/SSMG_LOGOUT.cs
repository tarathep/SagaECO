namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_LOGOUT" />.
    /// </summary>
    public class SSMG_LOGOUT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_LOGOUT"/> class.
        /// </summary>
        public SSMG_LOGOUT()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)32;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_LOGOUT.Results Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// Defines the Results.
        /// </summary>
        public enum Results
        {
            /// <summary>
            /// Defines the START.
            /// </summary>
            START = 0,

            /// <summary>
            /// Defines the CANCEL.
            /// </summary>
            CANCEL = 249, // 0x000000F9
        }
    }
}
