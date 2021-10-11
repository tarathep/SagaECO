namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_GOLEM_WAREHOUSE_SET" />.
    /// </summary>
    public class CSMG_GOLEM_WAREHOUSE_SET : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_GOLEM_WAREHOUSE_SET"/> class.
        /// </summary>
        public CSMG_GOLEM_WAREHOUSE_SET()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Title.
        /// </summary>
        public string Title
        {
            get
            {
                return Global.Unicode.GetString(this.GetBytes((ushort)this.GetByte((ushort)2), (ushort)3)).Replace("\0", "");
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_GOLEM_WAREHOUSE_SET();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnGolemWarehouseSet(this);
        }
    }
}
