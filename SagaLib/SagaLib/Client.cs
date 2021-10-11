namespace SagaLib
{
    using System.Collections.Generic;
    using System.Net.Sockets;

    /// <summary>
    /// Defines the <see cref="Client" />.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Defines the netIO.
        /// </summary>
        public NetIO netIO;

        /// <summary>
        /// Defines the SessionID.
        /// </summary>
        public uint SessionID;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        public Client()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="mSock">The mSock<see cref="Socket"/>.</param>
        /// <param name="mCommandTable">The mCommandTable<see cref="Dictionary{ushort, Packet}"/>.</param>
        public Client(Socket mSock, Dictionary<ushort, Packet> mCommandTable)
        {
            this.netIO = new NetIO(mSock, mCommandTable, this);
        }

        /// <summary>
        /// The OnConnect.
        /// </summary>
        public virtual void OnConnect()
        {
        }

        /// <summary>
        /// The OnDisconnect.
        /// </summary>
        public virtual void OnDisconnect()
        {
        }
    }
}
