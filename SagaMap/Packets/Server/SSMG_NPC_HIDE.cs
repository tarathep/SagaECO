namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_HIDE" />.
    /// </summary>
    public class SSMG_NPC_HIDE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_HIDE"/> class.
        /// </summary>
        public SSMG_NPC_HIDE()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)1507;
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
