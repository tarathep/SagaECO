namespace SagaMap.Packets.Client
{
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CSMG_DEM_CHIP_BUY" />.
    /// </summary>
    public class CSMG_DEM_CHIP_BUY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CSMG_DEM_CHIP_BUY"/> class.
        /// </summary>
        public CSMG_DEM_CHIP_BUY()
        {
            this.offset = (ushort)2;
        }

        /// <summary>
        /// Gets the ItemIDs.
        /// </summary>
        public uint[] ItemIDs
        {
            get
            {
                uint[] numArray = new uint[(int)this.GetByte((ushort)2)];
                for (int index = 0; index < numArray.Length; ++index)
                    numArray[index] = this.GetUInt((ushort)(3 + index * 4));
                return numArray;
            }
        }

        /// <summary>
        /// Gets the Counts.
        /// </summary>
        public int[] Counts
        {
            get
            {
                byte num = this.GetByte((ushort)2);
                int[] numArray = new int[(int)num];
                for (int index = 0; index < (int)num; ++index)
                    numArray[index] = this.GetInt((ushort)(4 + (int)num * 4 + index * 4));
                return numArray;
            }
        }

        /// <summary>
        /// The New.
        /// </summary>
        /// <returns>The <see cref="Packet"/>.</returns>
        public override Packet New()
        {
            return (Packet)new CSMG_DEM_CHIP_BUY();
        }

        /// <summary>
        /// The Parse.
        /// </summary>
        /// <param name="client">The client<see cref="SagaLib.Client"/>.</param>
        public override void Parse(SagaLib.Client client)
        {
            ((MapClient)client).OnDEMChipBuy(this);
        }
    }
}
