namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_CHANGE_VIEW" />.
    /// </summary>
    public class SSMG_NPC_CHANGE_VIEW : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_CHANGE_VIEW"/> class.
        /// </summary>
        public SSMG_NPC_CHANGE_VIEW()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)1511;
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

        /// <summary>
        /// Sets the MobID.
        /// </summary>
        public uint MobID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }
    }
}
