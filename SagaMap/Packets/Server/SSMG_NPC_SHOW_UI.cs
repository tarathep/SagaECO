namespace SagaMap.Packets.Server
{
    using SagaLib;
    using SagaMap.Scripting;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SHOW_UI" />.
    /// </summary>
    public class SSMG_NPC_SHOW_UI : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SHOW_UI"/> class.
        /// </summary>
        public SSMG_NPC_SHOW_UI()
        {
            this.data = new byte[10];
            this.offset = (ushort)2;
            this.ID = (ushort)1570;
            this.PutUInt(1U, (ushort)2);
        }

        /// <summary>
        /// Sets the UIType.
        /// </summary>
        public UIType UIType
        {
            set
            {
                this.PutInt((int)value, (ushort)6);
            }
        }
    }
}
