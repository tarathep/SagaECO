namespace SagaLib.VirtualFileSystem.Lpk
{
    using System;
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// Defines the <see cref="LpkOutputStream" />.
    /// </summary>
    public class LpkOutputStream : Stream
    {
        /// <summary>
        /// Defines the baseStream.
        /// </summary>
        private Stream baseStream;

        /// <summary>
        /// Defines the info.
        /// </summary>
        private LpkFileInfo info;

        /// <summary>
        /// Defines the gzip.
        /// </summary>
        private GZipStream gzip;

        /// <summary>
        /// Initializes a new instance of the <see cref="LpkOutputStream"/> class.
        /// </summary>
        /// <param name="lpk">The lpk<see cref="Stream"/>.</param>
        /// <param name="file">The file<see cref="LpkFileInfo"/>.</param>
        public LpkOutputStream(Stream lpk, LpkFileInfo file)
        {
            this.baseStream = lpk;
            this.info = file;
            this.baseStream.Position = (long)file.DataOffset;
            this.gzip = new GZipStream(this.baseStream, CompressionMode.Decompress, true);
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public override void Close()
        {
            base.Close();
            this.gzip.Close();
        }

        /// <summary>
        /// Gets a value indicating whether CanRead.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanSeek.
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanWrite.
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        public override void Flush()
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Gets the Length.
        /// </summary>
        public override long Length
        {
            get
            {
                return (long)this.info.UncompressedSize;
            }
        }

        /// <summary>
        /// Gets the CompressedLength.
        /// </summary>
        public long CompressedLength
        {
            get
            {
                return (long)this.info.FileSize;
            }
        }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// The Read.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.gzip.Read(buffer, offset, count);
        }

        /// <summary>
        /// The Seek.
        /// </summary>
        /// <param name="offset">The offset<see cref="long"/>.</param>
        /// <param name="origin">The origin<see cref="SeekOrigin"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The SetLength.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// The Write.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }
    }
}
