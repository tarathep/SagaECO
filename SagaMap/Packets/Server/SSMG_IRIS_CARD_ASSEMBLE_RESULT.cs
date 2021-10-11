namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_IRIS_CARD_ASSEMBLE_RESULT" />.
    /// </summary>
    public class SSMG_IRIS_CARD_ASSEMBLE_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_IRIS_CARD_ASSEMBLE_RESULT"/> class.
        /// </summary>
        public SSMG_IRIS_CARD_ASSEMBLE_RESULT()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)5132;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_IRIS_CARD_ASSEMBLE_RESULT.Results Result
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
            /// Defines the NOT_ENOUGH_ITEM.
            /// </summary>
            NOT_ENOUGH_ITEM = -4,

            /// <summary>
            /// Defines the NO_ITEM.
            /// </summary>
            NO_ITEM = -3,

            /// <summary>
            /// Defines the ITEM_FULL.
            /// </summary>
            ITEM_FULL = -2,

            /// <summary>
            /// Defines the NOT_ENOUGH_GOLD.
            /// </summary>
            NOT_ENOUGH_GOLD = -1,

            /// <summary>
            /// Defines the FAILED.
            /// </summary>
            FAILED = 0,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 1,
        }
    }
}
