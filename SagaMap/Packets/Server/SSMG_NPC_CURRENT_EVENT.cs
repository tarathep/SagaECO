namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_CURRENT_EVENT" />.
    /// </summary>
    public class SSMG_NPC_CURRENT_EVENT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_CURRENT_EVENT"/> class.
        /// </summary>
        public SSMG_NPC_CURRENT_EVENT()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)1512;
        }

        /// <summary>
        /// Sets the EventID.
        /// </summary>
        public uint EventID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
