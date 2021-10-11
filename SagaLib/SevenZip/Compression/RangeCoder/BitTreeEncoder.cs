namespace SevenZip.Compression.RangeCoder
{
    /// <summary>
    /// Defines the <see cref="BitTreeEncoder" />.
    /// </summary>
    internal struct BitTreeEncoder
    {
        /// <summary>
        /// Defines the Models.
        /// </summary>
        private BitEncoder[] Models;

        /// <summary>
        /// Defines the NumBitLevels.
        /// </summary>
        private int NumBitLevels;

        /// <summary>
        /// Initializes a new instance of the <see cref=""/> class.
        /// </summary>
        /// <param name="numBitLevels">The numBitLevels<see cref="int"/>.</param>
        public BitTreeEncoder(int numBitLevels)
        {
            this.NumBitLevels = numBitLevels;
            this.Models = new BitEncoder[1 << numBitLevels];
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
        /// The Encode.
        /// </summary>
        /// <param name="rangeEncoder">The rangeEncoder<see cref="Encoder"/>.</param>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        public void Encode(Encoder rangeEncoder, uint symbol)
        {
            uint num = 1;
            int numBitLevels = this.NumBitLevels;
            while (numBitLevels > 0)
            {
                --numBitLevels;
                uint symbol1 = symbol >> numBitLevels & 1U;
                this.Models[num].Encode(rangeEncoder, symbol1);
                num = num << 1 | symbol1;
            }
        }

        /// <summary>
        /// The ReverseEncode.
        /// </summary>
        /// <param name="rangeEncoder">The rangeEncoder<see cref="Encoder"/>.</param>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        public void ReverseEncode(Encoder rangeEncoder, uint symbol)
        {
            uint num = 1;
            for (uint index = 0; (long)index < (long)this.NumBitLevels; ++index)
            {
                uint symbol1 = symbol & 1U;
                this.Models[num].Encode(rangeEncoder, symbol1);
                num = num << 1 | symbol1;
                symbol >>= 1;
            }
        }

        /// <summary>
        /// The GetPrice.
        /// </summary>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetPrice(uint symbol)
        {
            uint num1 = 0;
            uint num2 = 1;
            int numBitLevels = this.NumBitLevels;
            while (numBitLevels > 0)
            {
                --numBitLevels;
                uint symbol1 = symbol >> numBitLevels & 1U;
                num1 += this.Models[num2].GetPrice(symbol1);
                num2 = (num2 << 1) + symbol1;
            }
            return num1;
        }

        /// <summary>
        /// The ReverseGetPrice.
        /// </summary>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint ReverseGetPrice(uint symbol)
        {
            uint num1 = 0;
            uint num2 = 1;
            for (int numBitLevels = this.NumBitLevels; numBitLevels > 0; --numBitLevels)
            {
                uint symbol1 = symbol & 1U;
                symbol >>= 1;
                num1 += this.Models[num2].GetPrice(symbol1);
                num2 = num2 << 1 | symbol1;
            }
            return num1;
        }

        /// <summary>
        /// The ReverseGetPrice.
        /// </summary>
        /// <param name="Models">The Models<see cref="BitEncoder[]"/>.</param>
        /// <param name="startIndex">The startIndex<see cref="uint"/>.</param>
        /// <param name="NumBitLevels">The NumBitLevels<see cref="int"/>.</param>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public static uint ReverseGetPrice(BitEncoder[] Models, uint startIndex, int NumBitLevels, uint symbol)
        {
            uint num1 = 0;
            uint num2 = 1;
            for (int index = NumBitLevels; index > 0; --index)
            {
                uint symbol1 = symbol & 1U;
                symbol >>= 1;
                num1 += Models[(startIndex + num2)].GetPrice(symbol1);
                num2 = num2 << 1 | symbol1;
            }
            return num1;
        }

        /// <summary>
        /// The ReverseEncode.
        /// </summary>
        /// <param name="Models">The Models<see cref="BitEncoder[]"/>.</param>
        /// <param name="startIndex">The startIndex<see cref="uint"/>.</param>
        /// <param name="rangeEncoder">The rangeEncoder<see cref="Encoder"/>.</param>
        /// <param name="NumBitLevels">The NumBitLevels<see cref="int"/>.</param>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        public static void ReverseEncode(BitEncoder[] Models, uint startIndex, Encoder rangeEncoder, int NumBitLevels, uint symbol)
        {
            uint num = 1;
            for (int index = 0; index < NumBitLevels; ++index)
            {
                uint symbol1 = symbol & 1U;
                Models[(startIndex + num)].Encode(rangeEncoder, symbol1);
                num = num << 1 | symbol1;
                symbol >>= 1;
            }
        }
    }
}
