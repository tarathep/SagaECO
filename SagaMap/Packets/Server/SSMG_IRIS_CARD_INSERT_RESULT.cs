namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_IRIS_CARD_INSERT_RESULT" />.
    /// </summary>
    public class SSMG_IRIS_CARD_INSERT_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_IRIS_CARD_INSERT_RESULT"/> class.
        /// </summary>
        public SSMG_IRIS_CARD_INSERT_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)7607;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_IRIS_CARD_INSERT_RESULT.Results Result
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
            /// Defines the SLOT_OVER.
            /// </summary>
            SLOT_OVER = -3,

            /// <summary>
            /// Defines the CANNOT_SET.
            /// </summary>
            CANNOT_SET = -2,

            /// <summary>
            /// Defines the CANNOT_SET_NOW.
            /// </summary>
            CANNOT_SET_NOW = -1,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
