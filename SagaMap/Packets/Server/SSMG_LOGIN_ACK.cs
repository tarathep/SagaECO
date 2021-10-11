namespace SagaMap.Packets.Server
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
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)17;
            this.Unknown1 = (ushort)256;
            this.Unknown2 = 305419896U;
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
        /// Sets the Unknown1.
        /// </summary>
        public ushort Unknown1
        {
            set
            {
                this.PutUShort(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Unknown2.
        /// </summary>
        public uint Unknown2
        {
            set
            {
                this.PutUInt(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the TestEndTime
        /// ゲストID期限　(1970年1月1日0時0分0秒からの秒数）08/01/11より
        /// End time of trial(second count since 1st Jan. 1970).
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
