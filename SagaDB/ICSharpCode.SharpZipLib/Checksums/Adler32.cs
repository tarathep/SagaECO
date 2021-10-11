namespace ICSharpCode.SharpZipLib.Checksums
{
    using System;

    /// <summary>
    /// Defines the <see cref="Adler32" />.
    /// </summary>
    public sealed class Adler32 : IChecksum
    {
        /// <summary>
        /// Defines the BASE.
        /// </summary>
        private const uint BASE = 65521;

        /// <summary>
        /// Defines the checksum.
        /// </summary>
        private uint checksum;

        /// <summary>
        /// Gets the Value.
        /// </summary>
        public long Value
        {
            get
            {
                return (long)this.checksum;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adler32"/> class.
        /// </summary>
        public Adler32()
        {
            this.Reset();
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        public void Reset()
        {
            this.checksum = 1U;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        public void Update(int value)
        {
            uint num1 = this.checksum & (uint)ushort.MaxValue;
            uint num2 = this.checksum >> 16;
            uint num3 = (num1 + (uint)(value & (int)byte.MaxValue)) % 65521U;
            this.checksum = ((num3 + num2) % 65521U << 16) + num3;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        public void Update(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            this.Update(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void Update(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "cannot be negative");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "cannot be negative");
            if (offset >= buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset), "not a valid index into buffer");
            if (offset + count > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(count), "exceeds buffer size");
            uint num1 = this.checksum & (uint)ushort.MaxValue;
            uint num2 = this.checksum >> 16;
            while (count > 0)
            {
                int num3 = 3800;
                if (num3 > count)
                    num3 = count;
                count -= num3;
                while (--num3 >= 0)
                {
                    num1 += (uint)buffer[offset++] & (uint)byte.MaxValue;
                    num2 += num1;
                }
                num1 %= 65521U;
                num2 %= 65521U;
            }
            this.checksum = num2 << 16 | num1;
        }
    }
}
