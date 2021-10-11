namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_VERSION_ACK" />.
    /// </summary>
    public class SSMG_VERSION_ACK : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_VERSION_ACK"/> class.
        /// </summary>
        public SSMG_VERSION_ACK()
        {
            this.data = new byte[10];
            this.offset = (ushort)14;
            this.ID = (ushort)11;
        }

        /// <summary>
        /// The SetResult.
        /// </summary>
        /// <param name="res">The res<see cref="SSMG_VERSION_ACK.Result"/>.</param>
        public void SetResult(SSMG_VERSION_ACK.Result res)
        {
            this.PutShort((short)res, (ushort)2);
        }

        /// <summary>
        /// The SetVersion.
        /// </summary>
        /// <param name="version">The version<see cref="string"/>.</param>
        public void SetVersion(string version)
        {
            this.PutBytes(Conversions.HexStr2Bytes(version), (ushort)4);
        }

        /// <summary>
        /// Defines the Result.
        /// </summary>
        public enum Result
        {
            /// <summary>
            /// Defines the VERSION_MISSMATCH.
            /// </summary>
            VERSION_MISSMATCH = -1,

            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,
        }
    }
}
