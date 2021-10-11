namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_NPC_PLAY_SOUND" />.
    /// </summary>
    public class SSMG_NPC_PLAY_SOUND : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_NPC_PLAY_SOUND"/> class.
        /// </summary>
        public SSMG_NPC_PLAY_SOUND()
        {
            this.data = new byte[13];
            this.offset = (ushort)2;
            this.ID = (ushort)1525;
        }

        /// <summary>
        /// Sets the SoundID.
        /// </summary>
        public uint SoundID
        {
            set
            {
                this.PutUInt(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Loop.
        /// </summary>
        public byte Loop
        {
            set
            {
                this.PutByte(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Volume.
        /// </summary>
        public uint Volume
        {
            set
            {
                this.PutUInt(value, (ushort)8);
            }
        }

        /// <summary>
        /// Sets the Balance.
        /// </summary>
        public byte Balance
        {
            set
            {
                this.PutByte(value, (ushort)12);
            }
        }
    }
}
