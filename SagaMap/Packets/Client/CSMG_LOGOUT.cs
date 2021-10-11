namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_LOGOUT" />.
    /// </summary>
    public class CSMG_LOGOUT : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_LOGOUT"/> class.
        /// </summary>
        public CSMG_LOGOUT()
        {
            this.offset = (ushort)8;
        }

        /// <summary>
        /// Gets the Result.
        /// </summary>
        public CSMG_LOGOUT.Results Result
        {
            get
            {
                return (CSMG_LOGOUT.Results)this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_LOGOUT();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnLogout(this);
        }

        /// <summary>
        /// Defines the Results.
        /// </summary>
        public enum Results
        {
            /// <summary>
            /// Defines the OK.
            /// </summary>
            OK = 0,

            /// <summary>
            /// Defines the CANCEL.
            /// </summary>
            CANCEL = 249, // 0x000000F9

            /// <summary>
            /// Defines the FAILED.
            /// </summary>
            FAILED = 255, // 0x000000FF
        }
    }
}
