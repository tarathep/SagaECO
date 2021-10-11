namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_DEM_STATS_PRE_CALC" />.
    /// </summary>
    public class CSMG_DEM_STATS_PRE_CALC : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_DEM_STATS_PRE_CALC"/> class.
        /// </summary>
        public CSMG_DEM_STATS_PRE_CALC()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Str.
        /// </summary>
        public ushort Str
        {
            get
            {
                return this.GetUShort((ushort)5);
            }
        }

        /// <summary>
        /// Gets the Dex.
        /// </summary>
        public ushort Dex
        {
            get
            {
                return this.GetUShort((ushort)7);
            }
        }

        /// <summary>
        /// Gets the Int.
        /// </summary>
        public ushort Int
        {
            get
            {
                return this.GetUShort((ushort)9);
            }
        }

        /// <summary>
        /// Gets the Vit.
        /// </summary>
        public ushort Vit
        {
            get
            {
                return this.GetUShort((ushort)11);
            }
        }

        /// <summary>
        /// Gets the Agi.
        /// </summary>
        public ushort Agi
        {
            get
            {
                return this.GetUShort((ushort)13);
            }
        }

        /// <summary>
        /// Gets the Mag.
        /// </summary>
        public ushort Mag
        {
            get
            {
                return this.GetUShort((ushort)15);
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_DEM_STATS_PRE_CALC();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnDEMStatsPreCalc(this);
        }
    }
}
