namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_IRIS_CARD_REMOVE_RESULT" />.
    /// </summary>
    public class SSMG_IRIS_CARD_REMOVE_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_IRIS_CARD_REMOVE_RESULT"/> class.
        /// </summary>
        public SSMG_IRIS_CARD_REMOVE_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)7612;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_IRIS_CARD_REMOVE_RESULT.Results Result
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
            /// Defines the FAILED.
            /// </summary>
            FAILED = -2,

            /// <summary>
            /// Defines the CANNOT_REMOVE_NOW.
            /// </summary>
            CANNOT_REMOVE_NOW = -1,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
