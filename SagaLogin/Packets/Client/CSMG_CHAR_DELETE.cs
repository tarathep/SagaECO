namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="CSMG_CHAR_DELETE" />.
    /// </summary>
    public class CSMG_CHAR_DELETE : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_CHAR_DELETE"/> class.
        /// </summary>
        public CSMG_CHAR_DELETE()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Slot.
        /// </summary>
        public byte Slot
        {
            get
            {
                return this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// Gets the DeletePassword.
        /// </summary>
        public string DeletePassword
        {
            get
            {
                return Encoding.ASCII.GetString(this.GetBytes((ushort)(byte)((uint)this.GetByte((ushort)3) - 1U), (ushort)4));
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_CHAR_DELETE();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnCharDelete(this);
        }
    }
}
