namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_DEM_DEMIC_CONFIRM" />.
    /// </summary>
    public class CSMG_DEM_DEMIC_CONFIRM : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_DEM_DEMIC_CONFIRM"/> class.
        /// </summary>
        public CSMG_DEM_DEMIC_CONFIRM()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the Page.
        /// </summary>
        public byte Page
        {
            get
            {
                return this.GetByte((ushort)2);
            }
        }

        /// <summary>
        /// Gets the Chips.
        /// </summary>
        public short[,] Chips
        {
            get
            {
                short[,] numArray = new short[9, 9];
                this.offset = (ushort)4;
                for (int index1 = 0; index1 < 9; ++index1)
                {
                    for (int index2 = 0; index2 < 9; ++index2)
                        numArray[index2, index1] = this.GetShort();
                }
                return numArray;
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_DEM_DEMIC_CONFIRM();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnDEMDemicConfirm(this);
        }
    }
}
