namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_SHOW_EFFECT" />.
    /// </summary>
    public class SSMG_NPC_SHOW_EFFECT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_SHOW_EFFECT"/> class.
        /// </summary>
        public SSMG_NPC_SHOW_EFFECT()
        {
            this.data = new byte[13];
            this.offset = (ushort)2;
            this.ID = (ushort)1550;
            this.PutByte(byte.MaxValue, (ushort)10);
            this.PutByte(byte.MaxValue, (ushort)11);
            this.PutByte((byte)1, (ushort)12);
        }

        /// <summary>
        /// Sets the ActorID.
        /// </summary>
        public uint ActorID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the EffectID.
        /// </summary>
        public uint EffectID
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the X.
        /// </summary>
        public byte X
        {
            set
            {
                this.PutByte(value, (ushort)10);
            }
        }

        /// <summary>
        /// Sets the Y.
        /// </summary>
        public byte Y
        {
            set
            {
                this.PutByte(value, (ushort)11);
            }
        }

        /// <summary>
        /// Sets a value indicating whether OneTime.
        /// </summary>
        public bool OneTime
        {
            set
            {
                if (value)
                    this.PutByte((byte)1, (ushort)12);
                else
                    this.PutByte((byte)0, (ushort)12);
            }
        }
    }
}
