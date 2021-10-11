namespace SagaLogin.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_CHAR_CREATE_ACK" />.
    /// </summary>
    public class SSMG_CHAR_CREATE_ACK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_CHAR_CREATE_ACK"/> class.
        /// </summary>
        public SSMG_CHAR_CREATE_ACK()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)161;
        }

        /// <summary>
        /// Sets the CreateResult.
        /// </summary>
        public SSMG_CHAR_CREATE_ACK.Result CreateResult
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
            /// Defines the GAME_SMSG_CHRCREATE_E_ALREADY_SLOT.
            /// </summary>
            GAME_SMSG_CHRCREATE_E_ALREADY_SLOT = -99, // -0x00000063

            /// <summary>
            /// Defines the GAME_SMSG_CHRCREATE_E_NAME_CONFLICT.
            /// </summary>
            GAME_SMSG_CHRCREATE_E_NAME_CONFLICT = -98, // -0x00000062

            /// <summary>
            /// Defines the GAME_SMSG_CHRCREATE_E_NAME_BADCHAR.
            /// </summary>
            GAME_SMSG_CHRCREATE_E_NAME_BADCHAR = -96, // -0x00000060

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
