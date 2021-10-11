namespace SagaLogin.Packets.Map
{
    using SagaLib;
    using SagaLogin.Manager;
    using SagaLogin.Network.Client;

    /// <summary>
    /// Defines the <see cref="INTERN_LOGIN_REGISTER" />.
    /// </summary>
    public class INTERN_LOGIN_REGISTER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="INTERN_LOGIN_REGISTER"/> class.
        /// </summary>
        public INTERN_LOGIN_REGISTER()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the MapServer.
        /// </summary>
        public MapServer MapServer
        {
            get
            {
                MapServer mapServer = new MapServer();
                byte num1 = this.GetByte((ushort)2);
                byte[] numArray = new byte[(int)num1];
                byte[] bytes1 = this.GetBytes((ushort)num1, (ushort)3);
                mapServer.Password = Global.Unicode.GetString(bytes1);
                ushort index1 = (ushort)(3U + (uint)num1);
                byte num2 = this.GetByte(index1);
                numArray = new byte[(int)num2];
                byte[] bytes2 = this.GetBytes((ushort)num2, (ushort)((uint)index1 + 1U));
                mapServer.IP = Global.Unicode.GetString(bytes2);
                ushort index2 = (ushort)(4U + (uint)num1 + (uint)num2);
                mapServer.port = this.GetInt(index2);
                byte num3 = this.GetByte((ushort)((uint)index2 + 4U));
                for (int index3 = 0; index3 < (int)num3; ++index3)
                    mapServer.HostedMaps.Add(this.GetUInt((ushort)((int)index2 + 5 + index3 * 4)));
                return mapServer;
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new INTERN_LOGIN_REGISTER();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginClient)client).OnInternMapRegister(this);
        }
    }
}
