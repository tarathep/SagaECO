namespace SagaMap.Packets.Login
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.LoginServer;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;

    /// <summary>
    /// Defines the <see cref="INTERN_LOGIN_REQUEST_CONFIG_ANSWER" />.
    /// </summary>
    public class INTERN_LOGIN_REQUEST_CONFIG_ANSWER : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="INTERN_LOGIN_REQUEST_CONFIG_ANSWER"/> class.
        /// </summary>
        public INTERN_LOGIN_REQUEST_CONFIG_ANSWER()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets a value indicating whether AuthOK.
        /// </summary>
        public bool AuthOK
        {
            get
            {
                return this.GetByte((ushort)2) == (byte)1;
            }
        }

        /// <summary>
        /// Gets the StartupSetting.
        /// </summary>
        public Dictionary<PC_RACE, SagaLogin.Configurations.StartupSetting> StartupSetting
        {
            get
            {
                return (Dictionary<PC_RACE, SagaLogin.Configurations.StartupSetting>)new BinaryFormatter().Deserialize((Stream)new MemoryStream(this.GetBytes((ushort)this.GetUInt((ushort)3), (ushort)7)));
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new INTERN_LOGIN_REQUEST_CONFIG_ANSWER();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((LoginSession)client).OnGetConfig(this);
        }
    }
}
