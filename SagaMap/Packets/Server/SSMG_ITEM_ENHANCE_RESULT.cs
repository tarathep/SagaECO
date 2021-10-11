namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_ENHANCE_RESULT" />.
    /// </summary>
    public class SSMG_ITEM_ENHANCE_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_ENHANCE_RESULT"/> class.
        /// </summary>
        public SSMG_ITEM_ENHANCE_RESULT()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)5064;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public int Result
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }
    }
}
