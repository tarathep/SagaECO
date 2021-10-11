namespace SagaLogin.Packets.Map
{
    using SagaLib;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="INTERN_LOGIN_REQUEST_CONFIG" />.
    /// </summary>
    public class INTERN_LOGIN_REQUEST_CONFIG : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="INTERN_LOGIN_REQUEST_CONFIG"/> class.
        /// </summary>
        public INTERN_LOGIN_REQUEST_CONFIG()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Version.
        /// </summary>
        public Version Version
        {
            get
            {
                return (Version)this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new INTERN_LOGIN_REQUEST_CONFIG();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnInternMapRequestConfig(this);
        }
    }
}
