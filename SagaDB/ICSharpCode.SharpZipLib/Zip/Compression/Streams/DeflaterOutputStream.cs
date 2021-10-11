namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using ICSharpCode.SharpZipLib.Encryption;
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Defines the <see cref="DeflaterOutputStream" />.
    /// </summary>
    public class DeflaterOutputStream : Stream
    {
        /// <summary>
        /// Defines the isStreamOwner_.
        /// </summary>
        private bool isStreamOwner_ = true;

        /// <summary>
        /// Defines the password.
        /// </summary>
        private string password;

        /// <summary>
        /// Defines the cryptoTransform_.
        /// </summary>
        private ICryptoTransform cryptoTransform_;

        /// <summary>
        /// Defines the buffer_.
        /// </summary>
        private byte[] buffer_;

        /// <summary>
        /// Defines the deflater_.
        /// </summary>
        protected Deflater deflater_;

        /// <summary>
        /// Defines the baseOutputStream_.
        /// </summary>
        protected Stream baseOutputStream_;

        /// <summary>
        /// Defines the isClosed_.
        /// </summary>
        private bool isClosed_;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeflaterOutputStream"/> class.
        /// </summary>
        /// <param name="baseOutputStream">The baseOutputStream<see cref="Stream"/>.</param>
        public DeflaterOutputStream(Stream baseOutputStream)
      : this(baseOutputStream, new Deflater(), 512)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeflaterOutputStream"/> class.
        /// </summary>
        /// <param name="baseOutputStream">The baseOutputStream<see cref="Stream"/>.</param>
        /// <param name="deflater">The deflater<see cref="Deflater"/>.</param>
        public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater)
      : this(baseOutputStream, deflater, 512)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeflaterOutputStream"/> class.
        /// </summary>
        /// <param name="baseOutputStream">The baseOutputStream<see cref="Stream"/>.</param>
        /// <param name="deflater">The deflater<see cref="Deflater"/>.</param>
        /// <param name="bufferSize">The bufferSize<see cref="int"/>.</param>
        public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater, int bufferSize)
        {
            if (baseOutputStream == null)
                throw new ArgumentNullException(nameof(baseOutputStream));
            if (!baseOutputStream.CanWrite)
                throw new ArgumentException("Must support writing", nameof(baseOutputStream));
            if (deflater == null)
                throw new ArgumentNullException(nameof(deflater));
            if (bufferSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));
            this.baseOutputStream_ = baseOutputStream;
            this.buffer_ = new byte[bufferSize];
            this.deflater_ = deflater;
        }

        /// <summary>
        /// The Finish.
        /// </summary>
        public virtual void Finish()
        {
            this.deflater_.Finish();
            while (!this.deflater_.IsFinished)
            {
                int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
                if (num > 0)
                {
                    if (this.cryptoTransform_ != null)
                        this.EncryptBlock(this.buffer_, 0, num);
                    this.baseOutputStream_.Write(this.buffer_, 0, num);
                }
                else
                    break;
            }
            if (!this.deflater_.IsFinished)
                throw new SharpZipBaseException("Can't deflate all input?");
            this.baseOutputStream_.Flush();
            if (this.cryptoTransform_ == null)
                return;
            this.cryptoTransform_.Dispose();
            this.cryptoTransform_ = (ICryptoTransform)null;
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsStreamOwner.
        /// </summary>
        public bool IsStreamOwner
        {
            get
            {
                return this.isStreamOwner_;
            }
            set
            {
                this.isStreamOwner_ = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanPatchEntries.
        /// </summary>
        public bool CanPatchEntries
        {
            get
            {
                return this.baseOutputStream_.CanSeek;
            }
        }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (value != null && value.Length == 0)
                    this.password = (string)null;
                else
                    this.password = value;
            }
        }

        /// <summary>
        /// The EncryptBlock.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        protected void EncryptBlock(byte[] buffer, int offset, int length)
        {
            this.cryptoTransform_.TransformBlock(buffer, 0, length, buffer, 0);
        }

        /// <summary>
        /// The InitializePassword.
        /// </summary>
        /// <param name="password">The password<see cref="string"/>.</param>
        protected void InitializePassword(string password)
        {
            this.cryptoTransform_ = new PkzipClassicManaged().CreateEncryptor(PkzipClassic.GenerateKeys(ZipConstants.ConvertToArray(password)), (byte[])null);
        }

        /// <summary>
        /// The Deflate.
        /// </summary>
        protected void Deflate()
        {
            while (!this.deflater_.IsNeedingInput)
            {
                int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
                if (num > 0)
                {
                    if (this.cryptoTransform_ != null)
                        this.EncryptBlock(this.buffer_, 0, num);
                    this.baseOutputStream_.Write(this.buffer_, 0, num);
                }
                else
                    break;
            }
            if (!this.deflater_.IsNeedingInput)
                throw new SharpZipBaseException("DeflaterOutputStream can't deflate all input?");
        }

        /// <summary>
        /// Gets a value indicating whether CanRead.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return false;
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
                return this.baseOutputStream_.CanWrite;
            }
        }

        /// <summary>
        /// Gets the Length.
        /// </summary>
        public override long Length
        {
            get
            {
                return this.baseOutputStream_.Length;
            }
        }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public override long Position
        {
            get
            {
                return this.baseOutputStream_.Position;
            }
            set
            {
                throw new NotSupportedException("Position property not supported");
            }
        }

        /// <summary>
        /// The Seek.
        /// </summary>
        /// <param name="offset">The offset<see cref="long"/>.</param>
        /// <param name="origin">The origin<see cref="SeekOrigin"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("DeflaterOutputStream Seek not supported");
        }

        /// <summary>
        /// The SetLength.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("DeflaterOutputStream SetLength not supported");
        }

        /// <summary>
        /// The ReadByte.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int ReadByte()
        {
            throw new NotSupportedException("DeflaterOutputStream ReadByte not supported");
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
            throw new NotSupportedException("DeflaterOutputStream Read not supported");
        }

        /// <summary>
        /// The BeginRead.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <param name="callback">The callback<see cref="AsyncCallback"/>.</param>
        /// <param name="state">The state<see cref="object"/>.</param>
        /// <returns>The <see cref="IAsyncResult"/>.</returns>
        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            throw new NotSupportedException("DeflaterOutputStream BeginRead not currently supported");
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
            throw new NotSupportedException("BeginWrite is not supported");
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        public override void Flush()
        {
            this.deflater_.Flush();
            this.Deflate();
            this.baseOutputStream_.Flush();
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public override void Close()
        {
            if (this.isClosed_)
                return;
            this.isClosed_ = true;
            try
            {
                this.Finish();
                if (this.cryptoTransform_ != null)
                {
                    this.cryptoTransform_.Dispose();
                    this.cryptoTransform_ = (ICryptoTransform)null;
                }
            }
            finally
            {
                if (this.isStreamOwner_)
                    this.baseOutputStream_.Close();
            }
        }

        /// <summary>
        /// The WriteByte.
        /// </summary>
        /// <param name="value">The value<see cref="byte"/>.</param>
        public override void WriteByte(byte value)
        {
            this.Write(new byte[1] { value }, 0, 1);
        }

        /// <summary>
        /// The Write.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.deflater_.SetInput(buffer, offset, count);
            this.Deflate();
        }
    }
}
