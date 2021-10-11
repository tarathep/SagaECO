namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_FUSION" />.
    /// </summary>
    public class SSMG_ITEM_FUSION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_FUSION"/> class.
        /// </summary>
        public SSMG_ITEM_FUSION()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)5080;
        }
    }
}
