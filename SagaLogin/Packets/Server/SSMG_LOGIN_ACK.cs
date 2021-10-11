namespace SagaLogin.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_LOGIN_ACK" />.
    /// </summary>
    public class SSMG_LOGIN_ACK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_LOGIN_ACK"/> class.
        /// </summary>
        public SSMG_LOGIN_ACK()
        {
            this.data = new byte[18];
            this.offset = (ushort)14;
            this.ID = (ushort)32;
        }

        /// <summary>
        /// Sets the LoginResult.
        /// </summary>
        public SSMG_LOGIN_ACK.Result LoginResult
        {
            set
            {
                this.PutUInt((uint)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the AccountID.
        /// </summary>
        public uint AccountID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the RestTestTime.
        /// </summary>
        public uint RestTestTime
        {
            set
            {
                this.PutUInt(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the TestEndTime.
        /// </summary>
        public uint TestEndTime
        {
            set
            {
                this.PutUInt(value, (ushort)14);
            }
        }

        /// <summary>
        /// Defines the Result.
        /// </summary>
        public enum Result
        {
            /// <summary>
            /// Defines the GAME_SMSG_GHLOGIN_ERR_101.
            /// </summary>
            GAME_SMSG_GHLOGIN_ERR_101 = -11, // -0x0000000B

            /// <summary>
            /// Defines the GAME_SMSG_LOGIN_ERR_IPBLOCK.
            /// </summary>
            GAME_SMSG_LOGIN_ERR_IPBLOCK = -6,

            /// <summary>
            /// Defines the GAME_SMSG_LOGIN_ERR_ALREADY.
            /// </summary>
            GAME_SMSG_LOGIN_ERR_ALREADY = -5,

            /// <summary>
            /// Defines the GAME_SMSG_LOGIN_ERR_BFALOCK.
            /// </summary>
            GAME_SMSG_LOGIN_ERR_BFALOCK = -4,

            /// <summary>
            /// Defines the GAME_SMSG_LOGIN_ERR_BADPASS.
            /// </summary>
            GAME_SMSG_LOGIN_ERR_BADPASS = -3,

            /// <summary>
            /// Defines the GAME_SMSG_LOGIN_ERR_UNKNOWN_ACC.
            /// </summary>
            GAME_SMSG_LOGIN_ERR_UNKNOWN_ACC = -2,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
