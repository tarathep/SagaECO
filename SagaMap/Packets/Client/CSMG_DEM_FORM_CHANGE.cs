namespace SagaMap.Packets.Client
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_DEM_FORM_CHANGE" />.
    /// </summary>
    public class CSMG_DEM_FORM_CHANGE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_DEM_FORM_CHANGE"/> class.
        /// </summary>
        public CSMG_DEM_FORM_CHANGE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Form.
        /// </summary>
        public DEM_FORM Form
        {
            get
            {
                return (DEM_FORM)this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_DEM_FORM_CHANGE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnDEMFormChange(this);
        }
    }
}
