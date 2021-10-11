namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ITEM_ACTOR_DISAPPEAR" />.
    /// </summary>
    public class SSMG_ITEM_ACTOR_DISAPPEAR : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ITEM_ACTOR_DISAPPEAR"/> class.
        /// </summary>
        public SSMG_ITEM_ACTOR_DISAPPEAR()
        {
            this.data = new byte[11];
            this.offset = (ushort)2;
            this.ID = (ushort)2015;
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
        /// Sets the Count.
        /// </summary>
        public byte Count
        {
            set
            {
                this.PutByte(value, (ushort)6);
            }
        }

        /// <summary>
        /// Sets the Looter.
        /// </summary>
        public uint Looter
        {
            set
            {
                this.PutUInt(value, (ushort)7);
            }
        }
    }
}
