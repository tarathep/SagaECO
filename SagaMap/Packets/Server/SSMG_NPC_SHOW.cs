namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SHOW" />.
    /// </summary>
    public class SSMG_NPC_SHOW : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SHOW"/> class.
        /// </summary>
        public SSMG_NPC_SHOW()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)1506;
        }

        /// <summary>
        /// Sets the NPCID.
        /// </summary>
        public uint NPCID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }
    }
}
