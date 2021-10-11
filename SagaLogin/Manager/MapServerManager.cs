namespace SagaLogin.Manager
{
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MapServerManager" />.
    /// </summary>
    public class MapServerManager : Singleton<MapServerManager>
    {
        /// <summary>
        /// Defines the servers.
        /// </summary>
        private Dictionary<uint, MapServer> servers = new Dictionary<uint, MapServer>();

        /// <summary>
        /// Gets the MapServers.
        /// </summary>
        public Dictionary<uint, MapServer> MapServers
        {
            get
            {
                return this.servers;
            }
        }
    }
}
