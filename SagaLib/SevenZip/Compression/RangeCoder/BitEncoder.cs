namespace SevenZip.Compression.RangeCoder
{
    /// <summary>
    /// Defines the <see cref="BitEncoder" />.
    /// </summary>
    internal struct BitEncoder
    {
        /// <summary>
        /// Defines the ProbPrices.
        /// </summary>
        private static uint[] ProbPrices = new uint[(512)];

        /// <summary>
        /// Defines the kNumBitModelTotalBits.
        /// </summary>
        public const int kNumBitModelTotalBits = 11;

        /// <summary>
        /// Defines the kBitModelTotal.
        /// </summary>
        public const uint kBitModelTotal = 2048;

        /// <summary>
        /// Defines the kNumMoveBits.
        /// </summary>
        private const int kNumMoveBits = 5;

        /// <summary>
        /// Defines the kNumMoveReducingBits.
        /// </summary>
        private const int kNumMoveReducingBits = 2;

        /// <summary>
        /// Defines the kNumBitPriceShiftBits.
        /// </summary>
        public const int kNumBitPriceShiftBits = 6;

        /// <summary>
        /// Defines the Prob.
        /// </summary>
        private uint Prob;

        /// <summary>
        /// The Init.
        /// </summary>
        public void Init()
        {
            this.Prob = 1024U;
        }

        /// <summary>
        /// The UpdateModel.
        /// </summary>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        public void UpdateModel(uint symbol)
        {
            if (symbol == 0U)
                this.Prob += 2048U - this.Prob >> 5;
            else
                this.Prob -= this.Prob >> 5;
        }

        /// <summary>
        /// The Encode.
        /// </summary>
        /// <param name="encoder">The encoder<see cref="Encoder"/>.</param>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        public void Encode(Encoder encoder, uint symbol)
        {
            uint num = (encoder.Range >> 11) * this.Prob;
            if (symbol == 0U)
            {
                encoder.Range = num;
                this.Prob += 2048U - this.Prob >> 5;
            }
            else
            {
                encoder.Low += (ulong)num;
                encoder.Range -= num;
                this.Prob -= this.Prob >> 5;
            }
            if (encoder.Range >= 16777216U)
                return;
            encoder.Range <<= 8;
            encoder.ShiftLow();
        }

        /// <summary>
        /// Initializes static members of the <see cref=""/> class.
        /// </summary>
        static BitEncoder()
        {
            for (int index1 = 8; index1 >= 0; --index1)
            {
                uint num1 = (uint)(1 << 9 - index1 - 1);
                uint num2 = (uint)(1 << 9 - index1);
                for (uint index2 = num1; index2 < num2; ++index2)
                    BitEncoder.ProbPrices[index2] = (uint)(index1 << 6) + ((uint)((int)num2 - (int)index2 << 6) >> 9 - index1 - 1);
            }
        }

        /// <summary>
        /// The GetPrice.
        /// </summary>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetPrice(uint symbol)
        {
            return BitEncoder.ProbPrices[(((long)(this.Prob - symbol) ^ (long)-(int)symbol) & 2047L) >> 2];
        }

        /// <summary>
        /// The GetPrice0.
        /// </summary>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetPrice0()
        {
            return BitEncoder.ProbPrices[(this.Prob >> 2)];
        }

        /// <summary>
        /// The GetPrice1.
        /// </summary>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetPrice1()
        {
            return BitEncoder.ProbPrices[(2048U - this.Prob >> 2)];
        }
    }
}
