namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;

    /// <summary>
    /// Defines the <see cref="PendingBuffer" />.
    /// </summary>
    public class PendingBuffer
    {
        /// <summary>
        /// Defines the buffer_.
        /// </summary>
        private byte[] buffer_;

        /// <summary>
        /// Defines the start.
        /// </summary>
        private int start;

        /// <summary>
        /// Defines the end.
        /// </summary>
        private int end;

        /// <summary>
        /// Defines the bits.
        /// </summary>
        private uint bits;

        /// <summary>
        /// Defines the bitCount.
        /// </summary>
        private int bitCount;

        /// <summary>
        /// Initializes a new instance of the <see cref="PendingBuffer"/> class.
        /// </summary>
        public PendingBuffer()
      : this(4096)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PendingBuffer"/> class.
        /// </summary>
        /// <param name="bufferSize">The bufferSize<see cref="int"/>.</param>
        public PendingBuffer(int bufferSize)
        {
            this.buffer_ = new byte[bufferSize];
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        public void Reset()
        {
            this.start = this.end = this.bitCount = 0;
        }

        /// <summary>
        /// The WriteByte.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        public void WriteByte(int value)
        {
            this.buffer_[this.end++] = (byte)value;
        }

        /// <summary>
        /// The WriteShort.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        public void WriteShort(int value)
        {
            this.buffer_[this.end++] = (byte)value;
            this.buffer_[this.end++] = (byte)(value >> 8);
        }

        /// <summary>
        /// The WriteInt.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        public void WriteInt(int value)
        {
            this.buffer_[this.end++] = (byte)value;
            this.buffer_[this.end++] = (byte)(value >> 8);
            this.buffer_[this.end++] = (byte)(value >> 16);
            this.buffer_[this.end++] = (byte)(value >> 24);
        }

        /// <summary>
        /// The WriteBlock.
        /// </summary>
        /// <param name="block">The block<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        public void WriteBlock(byte[] block, int offset, int length)
        {
            Array.Copy((Array)block, offset, (Array)this.buffer_, this.end, length);
            this.end += length;
        }

        /// <summary>
        /// Gets the BitCount.
        /// </summary>
        public int BitCount
        {
            get
            {
                return this.bitCount;
            }
        }

        /// <summary>
        /// The AlignToByte.
        /// </summary>
        public void AlignToByte()
        {
            if (this.bitCount > 0)
            {
                this.buffer_[this.end++] = (byte)this.bits;
                if (this.bitCount > 8)
                    this.buffer_[this.end++] = (byte)(this.bits >> 8);
            }
            this.bits = 0U;
            this.bitCount = 0;
        }

        /// <summary>
        /// The WriteBits.
        /// </summary>
        /// <param name="b">The b<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void WriteBits(int b, int count)
        {
            this.bits |= (uint)(b << this.bitCount);
            this.bitCount += count;
            if (this.bitCount < 16)
                return;
            this.buffer_[this.end++] = (byte)this.bits;
            this.buffer_[this.end++] = (byte)(this.bits >> 8);
            this.bits >>= 16;
            this.bitCount -= 16;
        }

        /// <summary>
        /// The WriteShortMSB.
        /// </summary>
        /// <param name="s">The s<see cref="int"/>.</param>
        public void WriteShortMSB(int s)
        {
            this.buffer_[this.end++] = (byte)(s >> 8);
            this.buffer_[this.end++] = (byte)s;
        }

        /// <summary>
        /// Gets a value indicating whether IsFlushed.
        /// </summary>
        public bool IsFlushed
        {
            get
            {
                return this.end == 0;
            }
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        /// <param name="output">The output<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Flush(byte[] output, int offset, int length)
        {
            if (this.bitCount >= 8)
            {
                this.buffer_[this.end++] = (byte)this.bits;
                this.bits >>= 8;
                this.bitCount -= 8;
            }
            if (length > this.end - this.start)
            {
                length = this.end - this.start;
                Array.Copy((Array)this.buffer_, this.start, (Array)output, offset, length);
                this.start = 0;
                this.end = 0;
            }
            else
            {
                Array.Copy((Array)this.buffer_, this.start, (Array)output, offset, length);
                this.start += length;
            }
            return length;
        }

        /// <summary>
        /// The ToByteArray.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] ToByteArray()
        {
            byte[] numArray = new byte[this.end - this.start];
            Array.Copy((Array)this.buffer_, this.start, (Array)numArray, 0, numArray.Length);
            this.start = 0;
            this.end = 0;
            return numArray;
        }
    }
}
