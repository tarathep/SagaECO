namespace ICSharpCode.SharpZipLib.GZip
{
    using ICSharpCode.SharpZipLib.Checksums;
    using ICSharpCode.SharpZipLib.Zip.Compression;
    using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="GZipOutputStream" />.
    /// </summary>
    public class GZipOutputStream : DeflaterOutputStream
    {
        /// <summary>
        /// Defines the crc.
        /// </summary>
        protected Crc32 crc = new Crc32();

        /// <summary>
        /// Defines the state_.
        /// </summary>
        private GZipOutputStream.OutputState state_ = GZipOutputStream.OutputState.Header;

        /// <summary>
        /// Initializes a new instance of the <see cref="GZipOutputStream"/> class.
        /// </summary>
        /// <param name="baseOutputStream">The baseOutputStream<see cref="Stream"/>.</param>
        public GZipOutputStream(Stream baseOutputStream)
      : this(baseOutputStream, 4096)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GZipOutputStream"/> class.
        /// </summary>
        /// <param name="baseOutputStream">The baseOutputStream<see cref="Stream"/>.</param>
        /// <param name="size">The size<see cref="int"/>.</param>
        public GZipOutputStream(Stream baseOutputStream, int size)
      : base(baseOutputStream, new Deflater(-1, true), size)
        {
        }

        /// <summary>
        /// The SetLevel.
        /// </summary>
        /// <param name="level">The level<see cref="int"/>.</param>
        public void SetLevel(int level)
        {
            if (level < 1)
                throw new ArgumentOutOfRangeException(nameof(level));
            this.deflater_.SetLevel(level);
        }

        /// <summary>
        /// The GetLevel.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetLevel()
        {
            return this.deflater_.GetLevel();
        }

        /// <summary>
        /// The Write.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.state_ == GZipOutputStream.OutputState.Header)
                this.WriteHeader();
            if (this.state_ != GZipOutputStream.OutputState.Footer)
                throw new InvalidOperationException("Write not permitted in current state");
            this.crc.Update(buffer, offset, count);
            base.Write(buffer, offset, count);
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public override void Close()
        {
            try
            {
                this.Finish();
            }
            finally
            {
                if (this.state_ != GZipOutputStream.OutputState.Closed)
                {
                    this.state_ = GZipOutputStream.OutputState.Closed;
                    if (this.IsStreamOwner)
                        this.baseOutputStream_.Close();
                }
            }
        }

        /// <summary>
        /// The Finish.
        /// </summary>
        public override void Finish()
        {
            if (this.state_ == GZipOutputStream.OutputState.Header)
                this.WriteHeader();
            if (this.state_ != GZipOutputStream.OutputState.Footer)
                return;
            this.state_ = GZipOutputStream.OutputState.Finished;
            base.Finish();
            uint num1 = (uint)((ulong)this.deflater_.TotalIn & (ulong)uint.MaxValue);
            uint num2 = (uint)((ulong)this.crc.Value & (ulong)uint.MaxValue);
            byte[] buffer = new byte[8]
            {
        (byte) num2,
        (byte) (num2 >> 8),
        (byte) (num2 >> 16),
        (byte) (num2 >> 24),
        (byte) num1,
        (byte) (num1 >> 8),
        (byte) (num1 >> 16),
        (byte) (num1 >> 24)
            };
            this.baseOutputStream_.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// The WriteHeader.
        /// </summary>
        private void WriteHeader()
        {
            if (this.state_ != GZipOutputStream.OutputState.Header)
                return;
            this.state_ = GZipOutputStream.OutputState.Footer;
            int num = (int)((DateTime.Now.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000L);
            byte[] buffer = new byte[10]
            {
        (byte) 31,
        (byte) 139,
        (byte) 8,
        (byte) 0,
        (byte) num,
        (byte) (num >> 8),
        (byte) (num >> 16),
        (byte) (num >> 24),
        (byte) 0,
        byte.MaxValue
            };
            this.baseOutputStream_.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Defines the OutputState.
        /// </summary>
        private enum OutputState
        {
            /// <summary>
            /// Defines the Header.
            /// </summary>
            Header,

            /// <summary>
            /// Defines the Footer.
            /// </summary>
            Footer,

            /// <summary>
            /// Defines the Finished.
            /// </summary>
            Finished,

            /// <summary>
            /// Defines the Closed.
            /// </summary>
            Closed,
        }
    }
}
