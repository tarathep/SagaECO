namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_PARTS" />.
    /// </summary>
    public class SSMG_DEM_PARTS : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_PARTS"/> class.
        /// </summary>
        public SSMG_DEM_PARTS()
        {
            this.data = new byte[2];
            this.offset = (ushort)2;
            this.ID = (ushort)7810;
        }
    }
}
