namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_FUSION_RESULT" />.
    /// </summary>
    public class SSMG_ITEM_FUSION_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_FUSION_RESULT"/> class.
        /// </summary>
        public SSMG_ITEM_FUSION_RESULT()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)5082;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_ITEM_FUSION_RESULT.FusionResult Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// Defines the FusionResult.
        /// </summary>
        public enum FusionResult
        {
            /// <summary>
            /// Defines the UNKNOWN_ERROR.
            /// </summary>
            UNKNOWN_ERROR = -30, // -0x0000001E

            /// <summary>
            /// Defines the LV_TOO_LOW.
            /// </summary>
            LV_TOO_LOW = -10, // -0x0000000A

            /// <summary>
            /// Defines the EVENT_ITEM.
            /// </summary>
            EVENT_ITEM = -9,

            /// <summary>
            /// Defines the KNIGHT_NOT_FIT.
            /// </summary>
            KNIGHT_NOT_FIT = -8,

            /// <summary>
            /// Defines the JOB_NOT_FIT.
            /// </summary>
            JOB_NOT_FIT = -7,

            /// <summary>
            /// Defines the GENDER_NOT_FIT.
            /// </summary>
            GENDER_NOT_FIT = -6,

            /// <summary>
            /// Defines the TYPE_NOT_FIT.
            /// </summary>
            TYPE_NOT_FIT = -5,

            /// <summary>
            /// Defines the NOT_FIT.
            /// </summary>
            NOT_FIT = -4,

            /// <summary>
            /// Defines the NOT_ENOUGH_GOLD.
            /// </summary>
            NOT_ENOUGH_GOLD = -3,

            /// <summary>
            /// Defines the CANCELED.
            /// </summary>
            CANCELED = -2,

            /// <summary>
            /// Defines the FAILED.
            /// </summary>
            FAILED = -1,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
