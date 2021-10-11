namespace SagaMap.Packets.Server
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_FORM_CHANGE" />.
    /// </summary>
    public class SSMG_DEM_FORM_CHANGE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_FORM_CHANGE"/> class.
        /// </summary>
        public SSMG_DEM_FORM_CHANGE()
        {
            this.data = new byte[3];
            this.offset = (ushort)2;
            this.ID = (ushort)7806;
        }

        /// <summary>
        /// Sets the Form.
        /// </summary>
        public DEM_FORM Form
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }
    }
}
