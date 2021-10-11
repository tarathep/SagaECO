namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_VSHOP_CATEGORY_REQUEST" />.
    /// </summary>
    public class CSMG_VSHOP_CATEGORY_REQUEST : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_VSHOP_CATEGORY_REQUEST"/> class.
        /// </summary>
        public CSMG_VSHOP_CATEGORY_REQUEST()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Page.
        /// </summary>
        public uint Page
        {
            get
            {
                return this.GetUInt((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_VSHOP_CATEGORY_REQUEST();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnVShopCategoryRequest(this);
        }
    }
}
