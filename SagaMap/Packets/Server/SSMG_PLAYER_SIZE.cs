namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_PLAYER_SIZE" />.
    /// </summary>
    public class SSMG_PLAYER_SIZE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_PLAYER_SIZE"/> class.
        /// </summary>
        public SSMG_PLAYER_SIZE()
        {
            this.data = new byte[14];
            this.offset = (ushort)2;
            this.ID = (ushort)527;
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
        /// Sets the Size.
        /// </summary>
        public uint Size
        {
            set
            {
                this.PutUInt(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the unknwon.
        /// </summary>
        public uint unknwon
        {
            set
            {
                this.PutUInt(1500U, (ushort)10);
            }
        }
    }
}
