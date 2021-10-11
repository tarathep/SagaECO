namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SYNTHESE_HEADER" />.
    /// </summary>
    public class SSMG_NPC_SYNTHESE_HEADER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SYNTHESE_HEADER"/> class.
        /// </summary>
        public SSMG_NPC_SYNTHESE_HEADER()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)5045;
            this.Unknown = (byte)1;
        }

        /// <summary>
        /// Sets the Unknown.
        /// </summary>
        public byte Unknown
        {
            set
            {
                this.PutByte(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Count.
        /// </summary>
        public byte Count
        {
            set
            {
                this.PutByte(value, (ushort)3);
            }
        }
    }
}
