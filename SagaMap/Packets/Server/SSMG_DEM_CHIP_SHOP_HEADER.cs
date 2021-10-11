namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_CHIP_SHOP_HEADER" />.
    /// </summary>
    public class SSMG_DEM_CHIP_SHOP_HEADER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_CHIP_SHOP_HEADER"/> class.
        /// </summary>
        public SSMG_DEM_CHIP_SHOP_HEADER()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)1593;
        }

        /// <summary>
        /// Sets the CategoryID.
        /// </summary>
        public uint CategoryID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
