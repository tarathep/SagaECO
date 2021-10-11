namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SYNTHESE_RESULT" />.
    /// </summary>
    public class SSMG_NPC_SYNTHESE_RESULT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SYNTHESE_RESULT"/> class.
        /// </summary>
        public SSMG_NPC_SYNTHESE_RESULT()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)5048;
        }

        /// <summary>
        /// Sets the Result.
        /// </summary>
        public byte Result
        {
            set
            {
                this.PutByte(value, (ushort)2);
            }
        }
    }
}
