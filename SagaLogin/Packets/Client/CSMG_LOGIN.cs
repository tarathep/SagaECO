namespace SagaLogin.Packets.Client
{
    using SagaLib;
    using SagaLogin.Network.Client;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="CSMG_LOGIN" />.
    /// </summary>
    public class CSMG_LOGIN : Packet
    {
        /// <summary>
        /// Defines the UserName.
        /// </summary>
        public string UserName;

        /// <summary>
        /// Defines the Password.
        /// </summary>
        public string Password;

        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_LOGIN"/> class.
        /// </summary>
        public CSMG_LOGIN()
        {
            this.size = 55U;
            this.offset = (ushort)8;
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_LOGIN();
        }

        /// <summary>
        /// The GetContent.
        /// </summary>
        public void GetContent()
        {
            ushort index1 = 2;
            Encoding ascii = Encoding.ASCII;
            byte num1 = this.GetByte(index1);
            ushort index2 = (ushort)((uint)index1 + 1U);
            this.UserName = ascii.GetString(this.GetBytes((ushort)((uint)num1 - 1U), index2));
            ushort index3 = (ushort)((uint)index2 + (uint)num1);
            byte num2 = this.GetByte(index3);
            ushort index4 = (ushort)((uint)index3 + 1U);
            this.Password = ascii.GetString(this.GetBytes((ushort)((uint)num2 - 1U), index4));
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnLogin(this);
        }
    }
}
