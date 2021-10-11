namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_WAIT" />.
    /// </summary>
    public class SSMG_NPC_WAIT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_WAIT"/> class.
        /// </summary>
        public SSMG_NPC_WAIT()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)1515;
        }

        /// <summary>
        /// Sets the Wait.
        /// </summary>
        public uint Wait
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
