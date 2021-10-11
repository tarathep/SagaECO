namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_DEMIC_HEADER" />.
    /// </summary>
    public class SSMG_DEM_DEMIC_HEADER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_DEMIC_HEADER"/> class.
        /// </summary>
        public SSMG_DEM_DEMIC_HEADER()
        {
            this.data = new byte[12];
            this.offset = (ushort)2;
            this.ID = (ushort)7750;
            this.PutShort((short)9, (ushort)4);
            this.PutShort((short)448, (ushort)6);
            this.PutShort((short)208, (ushort)8);
        }

        /// <summary>
        /// Sets the CL.
        /// </summary>
        public short CL
        {
            set
            {
                this.PutShort(value, (ushort)2);
            }
        }
    }
}
