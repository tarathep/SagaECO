namespace SagaMap.Packets.Server
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SSMG_DEM_DEMIC_DATA" />.
    /// </summary>
    public class SSMG_DEM_DEMIC_DATA : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_DEM_DEMIC_DATA"/> class.
        /// </summary>
        public SSMG_DEM_DEMIC_DATA()
        {
            this.data = new byte[166];
            this.offset = (ushort)2;
            this.ID = (ushort)7754;
            this.Size = (byte)81;
        }

        /// <summary>
        /// Sets the Page.
        /// </summary>
        public byte Page
        {
            set
            {
                this.PutByte(value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the Size.
        /// </summary>
        public byte Size
        {
            set
            {
                this.PutByte(value, (ushort)3);
            }
        }

        /// <summary>
        /// Sets the Chips.
        /// </summary>
        public short[,] Chips
        {
            set
            {
                this.offset = (ushort)4;
                for (int index1 = 0; index1 < 9; ++index1)
                {
                    for (int index2 = 0; index2 < 9; ++index2)
                        this.PutShort(value[index2, index1]);
                }
            }
        }
    }
}
