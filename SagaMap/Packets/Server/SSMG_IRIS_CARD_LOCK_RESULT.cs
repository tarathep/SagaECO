namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_IRIS_CARD_LOCK_RESULT" />.
    /// </summary>
    public class SSMG_IRIS_CARD_LOCK_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_IRIS_CARD_LOCK_RESULT"/> class.
        /// </summary>
        public SSMG_IRIS_CARD_LOCK_RESULT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)7626;
        }
    }
}
