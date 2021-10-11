namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_NAVIGATION" />.
    /// </summary>
    public class SSMG_NPC_NAVIGATION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_NAVIGATION"/> class.
        /// </summary>
        public SSMG_NPC_NAVIGATION()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)6700;
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)3);
            }
        }
    }
}
