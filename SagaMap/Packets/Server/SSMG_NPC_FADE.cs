namespace SagaMap.Packets.Server
{
    using SagaLib;
    using SagaMap.Scripting;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_FADE" />.
    /// </summary>
    public class SSMG_NPC_FADE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_FADE"/> class.
        /// </summary>
        public SSMG_NPC_FADE()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)1545;
        }

        /// <summary>
        /// Sets the FadeType.
        /// </summary>
        public FadeType FadeType
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the FadeEffect.
        /// </summary>
        public FadeEffect FadeEffect
        {
            set
            {
                this.PutByte((byte)value, (ushort)3);
            }
        }
    }
}
