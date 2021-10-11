namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_COMMUNITY_BBS_POST_RESULT" />.
    /// </summary>
    public class SSMG_COMMUNITY_BBS_POST_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_COMMUNITY_BBS_POST_RESULT"/> class.
        /// </summary>
        public SSMG_COMMUNITY_BBS_POST_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6911;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_COMMUNITY_BBS_POST_RESULT.Results Result
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
            /// Defines the NOT_ENOUGH_MONEY.
            /// </summary>
            NOT_ENOUGH_MONEY = -2,

            /// <summary>
            /// Defines the FAILED.
            /// </summary>
            FAILED = -1,

            /// <summary>
            /// Defines the SUCCEED.
            /// </summary>
            SUCCEED = 0,
        }
    }
}
