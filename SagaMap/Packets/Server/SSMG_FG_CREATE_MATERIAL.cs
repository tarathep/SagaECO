namespace SagaMap.Packets.Server
{
    using SagaLib;
    using SagaMap.Scripting;

    /// <summary>
    /// Defines the <see cref="SSMG_FG_CREATE_MATERIAL" />.
    /// </summary>
    public class SSMG_FG_CREATE_MATERIAL : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FG_CREATE_MATERIAL"/> class.
        /// </summary>
        public SSMG_FG_CREATE_MATERIAL()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)7220;
        }

        /// <summary>
        /// Sets the Parts.
        /// </summary>
        public BitMask<FGardenParts> Parts
        {
            set
            {
                this.PutInt(value.Value, (ushort)2);
            }
        }
    }
}
