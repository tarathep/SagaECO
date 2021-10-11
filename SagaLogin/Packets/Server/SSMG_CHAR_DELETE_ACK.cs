namespace SagaLogin.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAR_DELETE_ACK" />.
    /// </summary>
    public class SSMG_CHAR_DELETE_ACK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAR_DELETE_ACK"/> class.
        /// </summary>
        public SSMG_CHAR_DELETE_ACK()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)166;
        }

        /// <summary>
        /// Sets the DeleteResult.
        /// </summary>
        public SSMG_CHAR_DELETE_ACK.Result DeleteResult
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// Defines the Result.
        /// </summary>
        public enum Result
        {
            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,

            /// <summary>
            /// Defines the WRONG_DELETE_PASSWORD.
            /// </summary>
            WRONG_DELETE_PASSWORD = 156, // 0x0000009C
        }
    }
}
