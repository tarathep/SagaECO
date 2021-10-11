namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_IRIS_ADD_SLOT_RESULT" />.
    /// </summary>
    public class SSMG_IRIS_ADD_SLOT_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_IRIS_ADD_SLOT_RESULT"/> class.
        /// </summary>
        public SSMG_IRIS_ADD_SLOT_RESULT()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)5094;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public SSMG_IRIS_ADD_SLOT_RESULT.Results Result
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
            /// Defines the NO_RIGHT_MATERIAL.
            /// </summary>
            NO_RIGHT_MATERIAL = -4,

            /// <summary>
            /// Defines the NO_MATERIAL.
            /// </summary>
            NO_MATERIAL = -3,

            /// <summary>
            /// Defines the NO_ITEM.
            /// </summary>
            NO_ITEM = -2,

            /// <summary>
            /// Defines the NOT_ENOUGH_GOLD.
            /// </summary>
            NOT_ENOUGH_GOLD = -1,

            /// <summary>
            /// Defines the Failed.
            /// </summary>
            Failed = 0,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 1,
        }
    }
}
