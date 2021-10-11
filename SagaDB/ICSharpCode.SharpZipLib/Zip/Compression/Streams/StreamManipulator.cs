namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using System;

    /// <summary>
    /// Defines the <see cref="StreamManipulator" />.
    /// </summary>
    public class StreamManipulator
    {
        /// <summary>
        /// Defines the window_.
        /// </summary>
        private byte[] window_;

        /// <summary>
        /// Defines the windowStart_.
        /// </summary>
        private int windowStart_;

        /// <summary>
        /// Defines the windowEnd_.
        /// </summary>
        private int windowEnd_;

        /// <summary>
        /// Defines the buffer_.
        /// </summary>
        private uint buffer_;

        /// <summary>
        /// Defines the bitsInBuffer_.
        /// </summary>
        private int bitsInBuffer_;

        /// <summary>
        /// The PeekBits.
        /// </summary>
        /// <param name="bitCount">The bitCount<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int PeekBits(int bitCount)
        {
            if (this.bitsInBuffer_ < bitCount)
            {
                if (this.windowStart_ == this.windowEnd_)
                    return -1;
                this.buffer_ |= (uint)(((int)this.window_[this.windowStart_++] & (int)byte.MaxValue | ((int)this.window_[this.windowStart_++] & (int)byte.MaxValue) << 8) << this.bitsInBuffer_);
                this.bitsInBuffer_ += 16;
            }
            return (int)((long)this.buffer_ & (long)((1 << bitCount) - 1));
        }

        /// <summary>
        /// The DropBits.
        /// </summary>
        /// <param name="bitCount">The bitCount<see cref="int"/>.</param>
        public void DropBits(int bitCount)
        {
            this.buffer_ >>= bitCount;
            this.bitsInBuffer_ -= bitCount;
        }

        /// <summary>
        /// The GetBits.
        /// </summary>
        /// <param name="bitCount">The bitCount<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetBits(int bitCount)
        {
            int num = this.PeekBits(bitCount);
            if (num >= 0)
                this.DropBits(bitCount);
            return num;
        }

        /// <summary>
        /// Gets the AvailableBits.
        /// </summary>
        public int AvailableBits
        {
            get
            {
                return this.bitsInBuffer_;
            }
        }

        /// <summary>
        /// Gets the AvailableBytes.
        /// </summary>
        public int AvailableBytes
        {
            get
            {
                return this.windowEnd_ - this.windowStart_ + (this.bitsInBuffer_ >> 3);
            }
        }

        /// <summary>
        /// The SkipToByteBoundary.
        /// </summary>
        public void SkipToByteBoundary()
        {
            this.buffer_ >>= this.bitsInBuffer_ & 7;
            this.bitsInBuffer_ &= -8;
        }

        /// <summary>
        /// Gets a value indicating whether IsNeedingInput.
        /// </summary>
        public bool IsNeedingInput
        {
            get
            {
                return this.windowStart_ == this.windowEnd_;
            }
        }

        /// <summary>
        /// The CopyBytes.
        /// </summary>
        /// <param name="output">The output<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CopyBytes(byte[] output, int offset, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            if ((this.bitsInBuffer_ & 7) != 0)
                throw new InvalidOperationException("Bit buffer is not byte aligned!");
            int num1 = 0;
            while (this.bitsInBuffer_ > 0 && length > 0)
            {
                output[offset++] = (byte)this.buffer_;
                this.buffer_ >>= 8;
                this.bitsInBuffer_ -= 8;
                --length;
                ++num1;
            }
            if (length == 0)
                return num1;
            int num2 = this.windowEnd_ - this.windowStart_;
            if (length > num2)
                length = num2;
            Array.Copy((Array)this.window_, this.windowStart_, (Array)output, offset, length);
            this.windowStart_ += length;
            if ((this.windowStart_ - this.windowEnd_ & 1) != 0)
            {
                this.buffer_ = (uint)this.window_[this.windowStart_++] & (uint)byte.MaxValue;
                this.bitsInBuffer_ = 8;
            }
            return num1 + length;
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        public void Reset()
        {
            this.buffer_ = 0U;
            this.windowStart_ = this.windowEnd_ = this.bitsInBuffer_ = 0;
        }

        /// <summary>
        /// The SetInput.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void SetInput(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Cannot be negative");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            if (this.windowStart_ < this.windowEnd_)
                throw new InvalidOperationException("Old input was not completely processed");
            int num = offset + count;
            if (offset > num || num > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(count));
            if ((count & 1) != 0)
            {
                this.buffer_ |= (uint)(((int)buffer[offset++] & (int)byte.MaxValue) << this.bitsInBuffer_);
                this.bitsInBuffer_ += 8;
            }
            this.window_ = buffer;
            this.windowStart_ = offset;
            this.windowEnd_ = num;
        }
    }
}
