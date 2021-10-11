namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Defines the <see cref="InflaterInputStream" />.
    /// </summary>
    public class InflaterInputStream : Stream
    {
        /// <summary>
        /// Defines the isStreamOwner.
        /// </summary>
        private bool isStreamOwner = true;

        /// <summary>
        /// Defines the inf.
        /// </summary>
        protected Inflater inf;

        /// <summary>
        /// Defines the inputBuffer.
        /// </summary>
        protected InflaterInputBuffer inputBuffer;

        /// <summary>
        /// Defines the baseInputStream.
        /// </summary>
        protected Stream baseInputStream;

        /// <summary>
        /// Defines the csize.
        /// </summary>
        protected long csize;

        /// <summary>
        /// Defines the isClosed.
        /// </summary>
        private bool isClosed;

        /// <summary>
        /// Initializes a new instance of the <see cref="InflaterInputStream"/> class.
        /// </summary>
        /// <param name="baseInputStream">The baseInputStream<see cref="Stream"/>.</param>
        public InflaterInputStream(Stream baseInputStream)
      : this(baseInputStream, new Inflater(), 4096)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InflaterInputStream"/> class.
        /// </summary>
        /// <param name="baseInputStream">The baseInputStream<see cref="Stream"/>.</param>
        /// <param name="inf">The inf<see cref="Inflater"/>.</param>
        public InflaterInputStream(Stream baseInputStream, Inflater inf)
      : this(baseInputStream, inf, 4096)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InflaterInputStream"/> class.
        /// </summary>
        /// <param name="baseInputStream">The baseInputStream<see cref="Stream"/>.</param>
        /// <param name="inflater">The inflater<see cref="Inflater"/>.</param>
        /// <param name="bufferSize">The bufferSize<see cref="int"/>.</param>
        public InflaterInputStream(Stream baseInputStream, Inflater inflater, int bufferSize)
        {
            if (baseInputStream == null)
                throw new ArgumentNullException(nameof(baseInputStream));
            if (inflater == null)
                throw new ArgumentNullException(nameof(inflater));
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            this.baseInputStream = baseInputStream;
            this.inf = inflater;
            this.inputBuffer = new InflaterInputBuffer(baseInputStream, bufferSize);
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsStreamOwner.
        /// </summary>
        public bool IsStreamOwner
        {
            get
            {
                return this.isStreamOwner;
            }
            set
            {
                this.isStreamOwner = value;
            }
        }

        /// <summary>
        /// The Skip.
        /// </summary>
        /// <param name="count">The count<see cref="long"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public long Skip(long count)
        {
            if (count <= 0L)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (this.baseInputStream.CanSeek)
            {
                this.baseInputStream.Seek(count, SeekOrigin.Current);
                return count;
            }
            int count1 = 2048;
            if (count < (long)count1)
                count1 = (int)count;
            byte[] buffer = new byte[count1];
            int num1 = 1;
            long num2 = count;
            while (num2 > 0L && num1 > 0)
            {
                if (num2 < (long)count1)
                    count1 = (int)num2;
                num1 = this.baseInputStream.Read(buffer, 0, count1);
                num2 -= (long)num1;
            }
            return count - num2;
        }

        /// <summary>
        /// The StopDecrypting.
        /// </summary>
        protected void StopDecrypting()
        {
            this.inputBuffer.CryptoTransform = (ICryptoTransform)null;
        }

        /// <summary>
        /// Gets the Available.
        /// </summary>
        public virtual int Available
        {
            get
            {
                return this.inf.IsFinished ? 0 : 1;
            }
        }

        /// <summary>
        /// The Fill.
        /// </summary>
        protected void Fill()
        {
            this.inputBuffer.Fill();
            this.inputBuffer.SetInflaterInput(this.inf);
        }

        /// <summary>
        /// Gets a value indicating whether CanRead.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return this.baseInputStream.CanRead;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanSeek.
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return false;
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
        /// Gets the Length.
        /// </summary>
        public override long Length
        {
            get
            {
                return (long)this.inputBuffer.RawLength;
            }
        }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public override long Position
        {
            get
            {
                return this.baseInputStream.Position;
            }
            set
            {
                throw new NotSupportedException("InflaterInputStream Position not supported");
            }
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        public override void Flush()
        {
            this.baseInputStream.Flush();
        }

        /// <summary>
        /// The Seek.
        /// </summary>
        /// <param name="offset">The offset<see cref="long"/>.</param>
        /// <param name="origin">The origin<see cref="SeekOrigin"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Seek not supported");
        }

        /// <summary>
        /// The SetLength.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("InflaterInputStream SetLength not supported");
        }

        /// <summary>
        /// The Write.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("InflaterInputStream Write not supported");
        }

        /// <summary>
        /// The WriteByte.
        /// </summary>
        /// <param name="value">The value<see cref="byte"/>.</param>
        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("InflaterInputStream WriteByte not supported");
        }

        /// <summary>
        /// The BeginWrite.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <param name="callback">The callback<see cref="AsyncCallback"/>.</param>
        /// <param name="state">The state<see cref="object"/>.</param>
        /// <returns>The <see cref="IAsyncResult"/>.</returns>
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotSupportedException("InflaterInputStream BeginWrite not supported");
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public override void Close()
        {
            if (this.isClosed)
                return;
            this.isClosed = true;
            if (this.isStreamOwner)
                this.baseInputStream.Close();
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
            if (this.inf.IsNeedingDictionary)
                throw new SharpZipBaseException("Need a dictionary");
            int count1 = count;
            while (true)
            {
                int num = this.inf.Inflate(buffer, offset, count1);
                offset += num;
                count1 -= num;
                if (count1 != 0 && !this.inf.IsFinished)
                {
                    if (this.inf.IsNeedingInput)
                        this.Fill();
                    else if (num == 0)
                        break;
                }
                else
                    goto label_9;
            }
            throw new ZipException("Dont know what to do");
        label_9:
            return count - count1;
        }
    }
}
