namespace SevenZip.Compression.RangeCoder
{
    /// <summary>
    /// Defines the <see cref="BitDecoder" />.
    /// </summary>
    internal struct BitDecoder
    {
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
        /// Defines the Prob.
        /// </summary>
        private uint Prob;

        /// <summary>
        /// The UpdateModel.
        /// </summary>
        /// <param name="numMoveBits">The numMoveBits<see cref="int"/>.</param>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        public void UpdateModel(int numMoveBits, uint symbol)
        {
            if (symbol == 0U)
                this.Prob += 2048U - this.Prob >> numMoveBits;
            else
                this.Prob -= this.Prob >> numMoveBits;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        public void Init()
        {
            this.Prob = 1024U;
        }

        /// <summary>
        /// The Decode.
        /// </summary>
        /// <param name="rangeDecoder">The rangeDecoder<see cref="Decoder"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint Decode(Decoder rangeDecoder)
        {
            uint num = (rangeDecoder.Range >> 11) * this.Prob;
            if (rangeDecoder.Code < num)
            {
                rangeDecoder.Range = num;
                this.Prob += 2048U - this.Prob >> 5;
                if (rangeDecoder.Range < 16777216U)
                {
                    rangeDecoder.Code = rangeDecoder.Code << 8 | (uint)(byte)rangeDecoder.Stream.ReadByte();
                    rangeDecoder.Range <<= 8;
                }
                return 0;
            }
            rangeDecoder.Range -= num;
            rangeDecoder.Code -= num;
            this.Prob -= this.Prob >> 5;
            if (rangeDecoder.Range < 16777216U)
            {
                rangeDecoder.Code = rangeDecoder.Code << 8 | (uint)(byte)rangeDecoder.Stream.ReadByte();
                rangeDecoder.Range <<= 8;
            }
            return 1;
        }
    }
}
