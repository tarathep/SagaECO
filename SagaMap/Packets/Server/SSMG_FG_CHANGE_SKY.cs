namespace SagaMap.Packets.Server
{
    using SagaLib;
    using SagaMap.Scripting;

    /// <summary>
    /// Defines the <see cref="SSMG_FG_CHANGE_SKY" />.
    /// </summary>
    public class SSMG_FG_CHANGE_SKY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_FG_CHANGE_SKY"/> class.
        /// </summary>
        public SSMG_FG_CHANGE_SKY()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)5053;
        }

        /// <summary>
        /// Sets the Sky.
        /// </summary>
        public FG_Sky Sky
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }
    }
}
