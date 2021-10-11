namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_ACTOR_OPTION" />.
    /// </summary>
    public class SSMG_ACTOR_OPTION : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_ACTOR_OPTION"/> class.
        /// </summary>
        public SSMG_ACTOR_OPTION()
        {
            this.data = new byte[6];
            this.offset = (ushort)2;
            this.ID = (ushort)6751;
        }

        /// <summary>
        /// Sets the Option.
        /// </summary>
        public SSMG_ACTOR_OPTION.Options Option
        {
            set
            {
                this.PutUInt((uint)value, (ushort)2);
            }
        }

        /// <summary>
        /// Defines the Options.
        /// </summary>
        public enum Options
        {
            /// <summary>
            /// Defines the NONE.
            /// </summary>
            NONE,

            /// <summary>
            /// Defines the NO_TRADE.
            /// </summary>
            NO_TRADE,

            /// <summary>
            /// Defines the NO_PARTY.
            /// </summary>
            NO_PARTY,

            /// <summary>
            /// Defines the NO_SPIRIT_POSSESSION.
            /// </summary>
            NO_SPIRIT_POSSESSION,

            /// <summary>
            /// Defines the NO_RING.
            /// </summary>
            NO_RING,
        }
    }
}
