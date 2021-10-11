namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_VSHOP_INFO_HEADER" />.
    /// </summary>
    public class SSMG_VSHOP_INFO_HEADER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_VSHOP_INFO_HEADER"/> class.
        /// </summary>
        public SSMG_VSHOP_INFO_HEADER()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)1615;
        }

        /// <summary>
        /// Sets the Page.
        /// </summary>
        public uint Page
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
