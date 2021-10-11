namespace SevenZip.Compression.RangeCoder
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="Decoder" />.
    /// </summary>
    internal class Decoder
    {
        /// <summary>
        /// Defines the kTopValue.
        /// </summary>
        public const uint kTopValue = 16777216;

        /// <summary>
        /// Defines the Range.
        /// </summary>
        public uint Range;

        /// <summary>
        /// Defines the Code.
        /// </summary>
        public uint Code;

        /// <summary>
        /// Defines the Stream.
        /// </summary>
        public Stream Stream;

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public void Init(Stream stream)
        {
            this.Stream = stream;
            this.Code = 0U;
            this.Range = uint.MaxValue;
            for (int index = 0; index < 5; ++index)
                this.Code = this.Code << 8 | (uint)(byte)this.Stream.ReadByte();
        }

        /// <summary>
        /// The ReleaseStream.
        /// </summary>
        public void ReleaseStream()
        {
            this.Stream = (Stream)null;
        }

        /// <summary>
        /// The CloseStream.
        /// </summary>
        public void CloseStream()
        {
            this.Stream.Close();
        }

        /// <summary>
        /// The Normalize.
        /// </summary>
        public void Normalize()
        {
            while (this.Range < 16777216U)
            {
                this.Code = this.Code << 8 | (uint)(byte)this.Stream.ReadByte();
                this.Range <<= 8;
            }
        }

        /// <summary>
        /// The Normalize2.
        /// </summary>
        public void Normalize2()
        {
            if (this.Range >= 16777216U)
                return;
            this.Code = this.Code << 8 | (uint)(byte)this.Stream.ReadByte();
            this.Range <<= 8;
        }

        /// <summary>
        /// The GetThreshold.
        /// </summary>
        /// <param name="total">The total<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetThreshold(uint total)
        {
            return this.Code / (this.Range /= total);
        }

        /// <summary>
        /// The Decode.
        /// </summary>
        /// <param name="start">The start<see cref="uint"/>.</param>
        /// <param name="size">The size<see cref="uint"/>.</param>
        /// <param name="total">The total<see cref="uint"/>.</param>
        public void Decode(uint start, uint size, uint total)
        {
            this.Code -= start * this.Range;
            this.Range *= size;
            this.Normalize();
        }

        /// <summary>
        /// The DecodeDirectBits.
        /// </summary>
        /// <param name="numTotalBits">The numTotalBits<see cref="int"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint DecodeDirectBits(int numTotalBits)
        {
            uint range = this.Range;
            uint num1 = this.Code;
            uint num2 = 0;
            for (int index = numTotalBits; index > 0; --index)
            {
                range >>= 1;
                uint num3 = num1 - range >> 31;
                num1 -= range & num3 - 1U;
                num2 = (uint)((int)num2 << 1 | 1 - (int)num3);
                if (range < 16777216U)
                {
                    num1 = num1 << 8 | (uint)(byte)this.Stream.ReadByte();
                    range <<= 8;
                }
            }
            this.Range = range;
            this.Code = num1;
            return num2;
        }

        /// <summary>
        /// The DecodeBit.
        /// </summary>
        /// <param name="size0">The size0<see cref="uint"/>.</param>
        /// <param name="numTotalBits">The numTotalBits<see cref="int"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint DecodeBit(uint size0, int numTotalBits)
        {
            uint num1 = (this.Range >> numTotalBits) * size0;
            uint num2;
            if (this.Code < num1)
            {
                num2 = 0U;
                this.Range = num1;
            }
            else
            {
                num2 = 1U;
                this.Code -= num1;
                this.Range -= num1;
            }
            this.Normalize();
            return num2;
        }
    }
}
