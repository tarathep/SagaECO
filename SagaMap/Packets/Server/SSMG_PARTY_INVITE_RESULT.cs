namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PARTY_INVITE_RESULT" />.
    /// </summary>
    public class SSMG_PARTY_INVITE_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PARTY_INVITE_RESULT"/> class.
        /// </summary>
        public SSMG_PARTY_INVITE_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6604;
        }

        /// <summary>
        /// Sets the InviteResult.
        /// </summary>
        public SSMG_PARTY_INVITE_RESULT.Result InviteResult
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
            /// Defines the PARTY_MEMBER_EXCEED.
            /// </summary>
            PARTY_MEMBER_EXCEED = -12, // -0x0000000C

            /// <summary>
            /// Defines the PLAYER_ALREADY_IN_PARTY.
            /// </summary>
            PLAYER_ALREADY_IN_PARTY = -10, // -0x0000000A

            /// <summary>
            /// Defines the PLAYER_NOT_EXIST.
            /// </summary>
            PLAYER_NOT_EXIST = -2,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
