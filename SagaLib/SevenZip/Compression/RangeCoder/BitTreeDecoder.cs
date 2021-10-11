namespace SevenZip.Compression.RangeCoder
{
    /// <summary>
    /// Defines the <see cref="BitTreeDecoder" />.
    /// </summary>
    internal struct BitTreeDecoder
    {
        /// <summary>
        /// Defines the Models.
        /// </summary>
        private BitDecoder[] Models;

        /// <summary>
        /// Defines the NumBitLevels.
        /// </summary>
        private int NumBitLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class.
        /// </summary>
        /// <param name="numBitLevels">The numBitLevels<see cref="int"/>.</param>
        public BitTreeDecoder(int numBitLevels)
        {
            this.NumBitLevels = numBitLevels;
            this.Models = new BitDecoder[1 << numBitLevels];
        }

        /// <summary>
        /// The Init.
        /// </summary>
        public void Init()
        {
            for (uint index = 1; (long)index < (long)(1 << this.NumBitLevels); ++index)
                this.Models[index].Init();
        }

        /// <summary>
        /// The Decode.
        /// </summary>
        /// <param name="rangeDecoder">The rangeDecoder<see cref="Decoder"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint Decode(Decoder rangeDecoder)
        {
            uint num = 1;
            for (int numBitLevels = this.NumBitLevels; numBitLevels > 0; --numBitLevels)
                num = (num << 1) + this.Models[num].Decode(rangeDecoder);
            return num - (uint)(1 << this.NumBitLevels);
        }

        /// <summary>
        /// The ReverseDecode.
        /// </summary>
        /// <param name="rangeDecoder">The rangeDecoder<see cref="Decoder"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint ReverseDecode(Decoder rangeDecoder)
        {
            uint num1 = 1;
            uint num2 = 0;
            for (int index = 0; index < this.NumBitLevels; ++index)
            {
                uint num3 = this.Models[num1].Decode(rangeDecoder);
                num1 = (num1 << 1) + num3;
                num2 |= num3 << index;
            }
            return num2;
        }

        /// <summary>
        /// The ReverseDecode.
        /// </summary>
        /// <param name="Models">The Models<see cref="BitDecoder[]"/>.</param>
        /// <param name="startIndex">The startIndex<see cref="uint"/>.</param>
        /// <param name="rangeDecoder">The rangeDecoder<see cref="Decoder"/>.</param>
        /// <param name="NumBitLevels">The NumBitLevels<see cref="int"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public static uint ReverseDecode(BitDecoder[] Models, uint startIndex, Decoder rangeDecoder, int NumBitLevels)
        {
            uint num1 = 1;
            uint num2 = 0;
            for (int index = 0; index < NumBitLevels; ++index)
            {
                uint num3 = Models[(startIndex + num1)].Decode(rangeDecoder);
                num1 = (num1 << 1) + num3;
                num2 |= num3 << index;
            }
            return num2;
        }
    }
}
