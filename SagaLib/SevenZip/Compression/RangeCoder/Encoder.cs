namespace SevenZip.Compression.RangeCoder
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="Encoder" />.
    /// </summary>
    internal class Encoder
    {
        /// <summary>
        /// Defines the kTopValue.
        /// </summary>
        public const uint kTopValue = 16777216;

        /// <summary>
        /// Defines the Stream.
        /// </summary>
        private Stream Stream;

        /// <summary>
        /// Defines the Low.
        /// </summary>
        public ulong Low;

        /// <summary>
        /// Defines the Range.
        /// </summary>
        public uint Range;

        /// <summary>
        /// Defines the _cacheSize.
        /// </summary>
        private uint _cacheSize;

        /// <summary>
        /// Defines the _cache.
        /// </summary>
        private byte _cache;

        /// <summary>
        /// Defines the StartPosition.
        /// </summary>
        private long StartPosition;

        /// <summary>
        /// The SetStream.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public void SetStream(Stream stream)
        {
            this.Stream = stream;
        }

        /// <summary>
        /// The ReleaseStream.
        /// </summary>
        public void ReleaseStream()
        {
            this.Stream = (Stream)null;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        public void Init()
        {
            this.StartPosition = this.Stream.Position;
            this.Low = 0UL;
            this.Range = uint.MaxValue;
            this._cacheSize = 1U;
            this._cache = (byte)0;
        }

        /// <summary>
        /// The FlushData.
        /// </summary>
        public void FlushData()
        {
            for (int index = 0; index < 5; ++index)
                this.ShiftLow();
        }

        /// <summary>
        /// The FlushStream.
        /// </summary>
        public void FlushStream()
        {
            this.Stream.Flush();
        }

        /// <summary>
        /// The CloseStream.
        /// </summary>
        public void CloseStream()
        {
            this.Stream.Close();
        }

        /// <summary>
        /// The Encode.
        /// </summary>
        /// <param name="start">The start<see cref="uint"/>.</param>
        /// <param name="size">The size<see cref="uint"/>.</param>
        /// <param name="total">The total<see cref="uint"/>.</param>
        public void Encode(uint start, uint size, uint total)
        {
            this.Low += (ulong)(start * (this.Range /= total));
            this.Range *= size;
            while (this.Range < 16777216U)
            {
                this.Range <<= 8;
                this.ShiftLow();
            }
        }

        /// <summary>
        /// The ShiftLow.
        /// </summary>
        public void ShiftLow()
        {
            if ((uint)this.Low < 4278190080U || (uint)(this.Low >> 32) == 1U)
            {
                byte num = this._cache;
                do
                {
                    this.Stream.WriteByte((byte)((ulong)num + (this.Low >> 32)));
                    num = byte.MaxValue;
                }
                while (--this._cacheSize != 0U);
                this._cache = (byte)((uint)this.Low >> 24);
            }
            ++this._cacheSize;
            this.Low = (ulong)((uint)this.Low << 8);
        }

        /// <summary>
        /// The EncodeDirectBits.
        /// </summary>
        /// <param name="v">The v<see cref="uint"/>.</param>
        /// <param name="numTotalBits">The numTotalBits<see cref="int"/>.</param>
        public void EncodeDirectBits(uint v, int numTotalBits)
        {
            for (int index = numTotalBits - 1; index >= 0; --index)
            {
                this.Range >>= 1;
                if (((int)(v >> index) & 1) == 1)
                    this.Low += (ulong)this.Range;
                if (this.Range < 16777216U)
                {
                    this.Range <<= 8;
                    this.ShiftLow();
                }
            }
        }

        /// <summary>
        /// The EncodeBit.
        /// </summary>
        /// <param name="size0">The size0<see cref="uint"/>.</param>
        /// <param name="numTotalBits">The numTotalBits<see cref="int"/>.</param>
        /// <param name="symbol">The symbol<see cref="uint"/>.</param>
        public void EncodeBit(uint size0, int numTotalBits, uint symbol)
        {
            uint num = (this.Range >> numTotalBits) * size0;
            if (symbol == 0U)
            {
                this.Range = num;
            }
            else
            {
                this.Low += (ulong)num;
                this.Range -= num;
            }
            while (this.Range < 16777216U)
            {
                this.Range <<= 8;
                this.ShiftLow();
            }
        }

        /// <summary>
        /// The GetProcessedSizeAdd.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        public long GetProcessedSizeAdd()
        {
            return (long)this._cacheSize + this.Stream.Position - this.StartPosition + 4L;
        }
    }
}
