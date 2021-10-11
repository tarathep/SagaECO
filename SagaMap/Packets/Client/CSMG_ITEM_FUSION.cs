namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_ITEM_FUSION" />.
    /// </summary>
    public class CSMG_ITEM_FUSION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_ITEM_FUSION"/> class.
        /// </summary>
        public CSMG_ITEM_FUSION()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the EffectItem.
        /// </summary>
        public uint EffectItem
        {
            get
            {
                return this.GetUInt((ushort)6);
            }
        }

        /// <summary>
        /// Gets the ViewItem.
        /// </summary>
        public uint ViewItem
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
            return (Packet)new CSMG_ITEM_FUSION();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnItemFusion(this);
        }
    }
}
