namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_LOGIN_FINISHED" />.
    /// </summary>
    public class SSMG_LOGIN_FINISHED : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_LOGIN_FINISHED"/> class.
        /// </summary>
        public SSMG_LOGIN_FINISHED()
        {
            this.data = new byte[7];
            this.offset = (ushort)2;
            this.ID = (ushort)7015;
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
    }
}
