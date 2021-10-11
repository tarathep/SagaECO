namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_GOLEM_SET_TYPE" />.
    /// </summary>
    public class SSMG_GOLEM_SET_TYPE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_GOLEM_SET_TYPE"/> class.
        /// </summary>
        public SSMG_GOLEM_SET_TYPE()
        {
            this.data = new byte[4];
            this.offset = (ushort)2;
            this.ID = (ushort)6111;
        }

        /// <summary>
        /// Sets the GolemType.
        /// </summary>
        public GolemType GolemType
        {
            set
            {
                this.PutUShort((ushort)value, (ushort)2);
            }
        }
    }
}
