namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>
    /// Defines the <see cref="InflaterInputBuffer" />.
    /// </summary>
    public class InflaterInputBuffer
    {
        /// <summary>
        /// Defines the rawLength.
        /// </summary>
        private int rawLength;

        /// <summary>
        /// Defines the rawData.
        /// </summary>
        private byte[] rawData;

        /// <summary>
        /// Defines the clearTextLength.
        /// </summary>
        private int clearTextLength;

        /// <summary>
        /// Defines the clearText.
        /// </summary>
        private byte[] clearText;

        /// <summary>
        /// Defines the internalClearText.
        /// </summary>
        private byte[] internalClearText;

        /// <summary>
        /// Defines the available.
        /// </summary>
        private int available;

        /// <summary>
        /// Defines the cryptoTransform.
        /// </summary>
        private ICryptoTransform cryptoTransform;

        /// <summary>
        /// Defines the inputStream.
        /// </summary>
        private Stream inputStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="InflaterInputBuffer"/> class.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public InflaterInputBuffer(Stream stream)
      : this(stream, 4096)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InflaterInputBuffer"/> class.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <param name="bufferSize">The bufferSize<see cref="int"/>.</param>
        public InflaterInputBuffer(Stream stream, int bufferSize)
        {
            this.inputStream = stream;
            if (bufferSize < 1024)
                bufferSize = 1024;
            this.rawData = new byte[bufferSize];
            this.clearText = this.rawData;
        }

        /// <summary>
        /// Gets the RawLength.
        /// </summary>
        public int RawLength
        {
            get
            {
                return this.rawLength;
            }
        }

        /// <summary>
        /// Gets the RawData.
        /// </summary>
        public byte[] RawData
        {
            get
            {
                return this.rawData;
            }
        }

        /// <summary>
        /// Gets the ClearTextLength.
        /// </summary>
        public int ClearTextLength
        {
            get
            {
                return this.clearTextLength;
            }
        }

        /// <summary>
        /// Gets the ClearText.
        /// </summary>
        public byte[] ClearText
        {
            get
            {
                return this.clearText;
            }
        }

        /// <summary>
        /// Gets or sets the Available.
        /// </summary>
        public int Available
        {
            get
            {
                return this.available;
            }
            set
            {
                this.available = value;
            }
        }

        /// <summary>
        /// The SetInflaterInput.
        /// </summary>
        /// <param name="inflater">The inflater<see cref="Inflater"/>.</param>
        public void SetInflaterInput(Inflater inflater)
        {
            if (this.available <= 0)
                return;
            inflater.SetInput(this.clearText, this.clearTextLength - this.available, this.available);
            this.available = 0;
        }

        /// <summary>
        /// The Fill.
        /// </summary>
        public void Fill()
        {
            this.rawLength = 0;
            int length = this.rawData.Length;
            while (length > 0)
            {
                int num = this.inputStream.Read(this.rawData, this.rawLength, length);
                if (num <= 0)
                {
                    if (this.rawLength == 0)
                        throw new SharpZipBaseException("Unexpected EOF");
                    break;
                }
                this.rawLength += num;
                length -= num;
            }
            this.clearTextLength = this.cryptoTransform == null ? this.rawLength : this.cryptoTransform.TransformBlock(this.rawData, 0, this.rawLength, this.clearText, 0);
            this.available = this.clearTextLength;
        }

        /// <summary>
        /// The ReadRawBuffer.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReadRawBuffer(byte[] buffer)
        {
            return this.ReadRawBuffer(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// The ReadRawBuffer.
        /// </summary>
        /// <param name="outBuffer">The outBuffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReadRawBuffer(byte[] outBuffer, int offset, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            int destinationIndex = offset;
            int val1 = length;
            while (val1 > 0)
            {
                if (this.available <= 0)
                {
                    this.Fill();
                    if (this.available <= 0)
                        return 0;
                }
                int length1 = Math.Min(val1, this.available);
                Array.Copy((Array)this.rawData, this.rawLength - this.available, (Array)outBuffer, destinationIndex, length1);
                destinationIndex += length1;
                val1 -= length1;
                this.available -= length1;
            }
            return length;
        }

        /// <summary>
        /// The ReadClearTextBuffer.
        /// </summary>
        /// <param name="outBuffer">The outBuffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReadClearTextBuffer(byte[] outBuffer, int offset, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
            int destinationIndex = offset;
            int val1 = length;
            while (val1 > 0)
            {
                if (this.available <= 0)
                {
                    this.Fill();
                    if (this.available <= 0)
                        return 0;
                }
                int length1 = Math.Min(val1, this.available);
                Array.Copy((Array)this.clearText, this.clearTextLength - this.available, (Array)outBuffer, destinationIndex, length1);
                destinationIndex += length1;
                val1 -= length1;
                this.available -= length1;
            }
            return length;
        }

        /// <summary>
        /// The ReadLeByte.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReadLeByte()
        {
            if (this.available <= 0)
            {
                this.Fill();
                if (this.available <= 0)
                    throw new ZipException("EOF in header");
            }
            byte num = this.rawData[this.rawLength - this.available];
            --this.available;
            return (int)num;
        }

        /// <summary>
        /// The ReadLeShort.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReadLeShort()
        {
            return this.ReadLeByte() | this.ReadLeByte() << 8;
        }

        /// <summary>
        /// The ReadLeInt.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int ReadLeInt()
        {
            return this.ReadLeShort() | this.ReadLeShort() << 16;
        }

        /// <summary>
        /// The ReadLeLong.
        /// </summary>
        /// <returns>The <see cref="long"/>.</returns>
        public long ReadLeLong()
        {
            return (long)(uint)this.ReadLeInt() | (long)this.ReadLeInt() << 32;
        }

        /// <summary>
        /// Sets the CryptoTransform.
        /// </summary>
        public ICryptoTransform CryptoTransform
        {
            set
            {
                this.cryptoTransform = value;
                if (this.cryptoTransform != null)
                {
                    if (this.rawData == this.clearText)
                    {
                        if (this.internalClearText == null)
                            this.internalClearText = new byte[this.rawData.Length];
                        this.clearText = this.internalClearText;
                    }
                    this.clearTextLength = this.rawLength;
                    if (this.available <= 0)
                        return;
                    this.cryptoTransform.TransformBlock(this.rawData, this.rawLength - this.available, this.available, this.clearText, this.rawLength - this.available);
                }
                else
                {
                    this.clearText = this.rawData;
                    this.clearTextLength = this.rawLength;
                }
            }
        }
    }
}
