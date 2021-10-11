namespace SagaLogin.Manager
{
    using SagaLogin.Network.Client;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MapServer" />.
    /// </summary>
    public class MapServer
    {
        /// <summary>
        /// Defines the HostedMaps.
        /// </summary>
        public List<uint> HostedMaps = new List<uint>();

        /// <summary>
        /// Defines the Client.
        /// </summary>
        public LoginClient Client;

        /// <summary>
        /// Defines the Password.
        /// </summary>
        public string Password;

        /// <summary>
        /// Defines the IP.
        /// </summary>
        public string IP;

        /// <summary>
        /// Defines the port.
        /// </summary>
        public int port;
    }
}
