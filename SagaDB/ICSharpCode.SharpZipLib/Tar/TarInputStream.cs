namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="TarInputStream" />.
    /// </summary>
    public class TarInputStream : Stream
    {
        /// <summary>
        /// Defines the hasHitEOF.
        /// </summary>
        protected bool hasHitEOF;

        /// <summary>
        /// Defines the entrySize.
        /// </summary>
        protected long entrySize;

        /// <summary>
        /// Defines the entryOffset.
        /// </summary>
        protected long entryOffset;

        /// <summary>
        /// Defines the readBuffer.
        /// </summary>
        protected byte[] readBuffer;

        /// <summary>
        /// Defines the buffer.
        /// </summary>
        protected TarBuffer buffer;

        /// <summary>
        /// Defines the currentEntry.
        /// </summary>
        private TarEntry currentEntry;

        /// <summary>
        /// Defines the entryFactory.
        /// </summary>
        protected TarInputStream.IEntryFactory entryFactory;

        /// <summary>
        /// Defines the inputStream.
        /// </summary>
        private Stream inputStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="TarInputStream"/> class.
        /// </summary>
        /// <param name="inputStream">The inputStream<see cref="Stream"/>.</param>
        public TarInputStream(Stream inputStream)
      : this(inputStream, 20)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TarInputStream"/> class.
        /// </summary>
        /// <param name="inputStream">The inputStream<see cref="Stream"/>.</param>
        /// <param name="blockFactor">The blockFactor<see cref="int"/>.</param>
        public TarInputStream(Stream inputStream, int blockFactor)
        {
            this.inputStream = inputStream;
            this.buffer = TarBuffer.CreateInputTarBuffer(inputStream, blockFactor);
        }

        /// <summary>
        /// Gets a value indicating whether CanRead.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return this.inputStream.CanRead;
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
                return this.inputStream.Length;
            }
        }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public override long Position
        {
            get
            {
                return this.inputStream.Position;
            }
            set
            {
                throw new NotSupportedException("TarInputStream Seek not supported");
            }
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        public override void Flush()
        {
            this.inputStream.Flush();
        }

        /// <summary>
        /// The Seek.
        /// </summary>
        /// <param name="offset">The offset<see cref="long"/>.</param>
        /// <param name="origin">The origin<see cref="SeekOrigin"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("TarInputStream Seek not supported");
        }

        /// <summary>
        /// The SetLength.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("TarInputStream SetLength not supported");
        }

        /// <summary>
        /// The Write.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("TarInputStream Write not supported");
        }

        /// <summary>
        /// The WriteByte.
        /// </summary>
        /// <param name="value">The value<see cref="byte"/>.</param>
        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("TarInputStream WriteByte not supported");
        }

        /// <summary>
        /// The ReadByte.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int ReadByte()
        {
            byte[] buffer = new byte[1];
            if (this.Read(buffer, 0, 1) <= 0)
                return -1;
            return (int)buffer[0];
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
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            int num1 = 0;
            if (this.entryOffset >= this.entrySize)
                return 0;
            long num2 = (long)count;
            if (num2 + this.entryOffset > this.entrySize)
                num2 = this.entrySize - this.entryOffset;
            if (this.readBuffer != null)
            {
                int num3 = num2 > (long)this.readBuffer.Length ? this.readBuffer.Length : (int)num2;
                Array.Copy((Array)this.readBuffer, 0, (Array)buffer, offset, num3);
                if (num3 >= this.readBuffer.Length)
                {
                    this.readBuffer = (byte[])null;
                }
                else
                {
                    int length = this.readBuffer.Length - num3;
                    byte[] numArray = new byte[length];
                    Array.Copy((Array)this.readBuffer, num3, (Array)numArray, 0, length);
                    this.readBuffer = numArray;
                }
                num1 += num3;
                num2 -= (long)num3;
                offset += num3;
            }
            while (num2 > 0L)
            {
                byte[] numArray = this.buffer.ReadBlock();
                if (numArray == null)
                    throw new TarException("unexpected EOF with " + (object)num2 + " bytes unread");
                int num3 = (int)num2;
                int length = numArray.Length;
                if (length > num3)
                {
                    Array.Copy((Array)numArray, 0, (Array)buffer, offset, num3);
                    this.readBuffer = new byte[length - num3];
                    Array.Copy((Array)numArray, num3, (Array)this.readBuffer, 0, length - num3);
                }
                else
                {
                    num3 = length;
                    Array.Copy((Array)numArray, 0, (Array)buffer, offset, length);
                }
                num1 += num3;
                num2 -= (long)num3;
                offset += num3;
            }
            this.entryOffset += (long)num1;
            return num1;
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public override void Close()
        {
            this.buffer.Close();
        }

        /// <summary>
        /// The SetEntryFactory.
        /// </summary>
        /// <param name="factory">The factory<see cref="TarInputStream.IEntryFactory"/>.</param>
        public void SetEntryFactory(TarInputStream.IEntryFactory factory)
        {
            this.entryFactory = factory;
        }

        /// <summary>
        /// Gets the RecordSize.
        /// </summary>
        public int RecordSize
        {
            get
            {
                return this.buffer.RecordSize;
            }
        }

        /// <summary>
        /// The GetRecordSize.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return this.buffer.RecordSize;
        }

        /// <summary>
        /// Gets the Available.
        /// </summary>
        public long Available
        {
            get
            {
                return this.entrySize - this.entryOffset;
            }
        }

        /// <summary>
        /// The Skip.
        /// </summary>
        /// <param name="skipCount">The skipCount<see cref="long"/>.</param>
        public void Skip(long skipCount)
        {
            byte[] buffer = new byte[8192];
            long num1 = skipCount;
            while (num1 > 0L)
            {
                int count = num1 > (long)buffer.Length ? buffer.Length : (int)num1;
                int num2 = this.Read(buffer, 0, count);
                if (num2 == -1)
                    break;
                num1 -= (long)num2;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsMarkSupported.
        /// </summary>
        public bool IsMarkSupported
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// The Mark.
        /// </summary>
        /// <param name="markLimit">The markLimit<see cref="int"/>.</param>
        public void Mark(int markLimit)
        {
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        public void Reset()
        {
        }

        /// <summary>
        /// The GetNextEntry.
        /// </summary>
        /// <returns>The <see cref="TarEntry"/>.</returns>
        public TarEntry GetNextEntry()
        {
            if (this.hasHitEOF)
                return (TarEntry)null;
            if (this.currentEntry != null)
                this.SkipToNextEntry();
            byte[] numArray1 = this.buffer.ReadBlock();
            if (numArray1 == null)
                this.hasHitEOF = true;
            else if (TarBuffer.IsEndOfArchiveBlock(numArray1))
                this.hasHitEOF = true;
            if (this.hasHitEOF)
            {
                this.currentEntry = (TarEntry)null;
            }
            else
            {
                try
                {
                    TarHeader tarHeader = new TarHeader();
                    tarHeader.ParseBuffer(numArray1);
                    if (!tarHeader.IsChecksumValid)
                        throw new TarException("Header checksum is invalid");
                    this.entryOffset = 0L;
                    this.entrySize = tarHeader.Size;
                    StringBuilder stringBuilder = (StringBuilder)null;
                    if (tarHeader.TypeFlag == (byte)76)
                    {
                        byte[] numArray2 = new byte[512];
                        long entrySize = this.entrySize;
                        stringBuilder = new StringBuilder();
                        while (entrySize > 0L)
                        {
                            int length = this.Read(numArray2, 0, entrySize > (long)numArray2.Length ? numArray2.Length : (int)entrySize);
                            if (length == -1)
                                throw new InvalidHeaderException("Failed to read long name entry");
                            stringBuilder.Append(TarHeader.ParseName(numArray2, 0, length).ToString());
                            entrySize -= (long)length;
                        }
                        this.SkipToNextEntry();
                        numArray1 = this.buffer.ReadBlock();
                    }
                    else if (tarHeader.TypeFlag == (byte)103)
                    {
                        this.SkipToNextEntry();
                        numArray1 = this.buffer.ReadBlock();
                    }
                    else if (tarHeader.TypeFlag == (byte)120)
                    {
                        this.SkipToNextEntry();
                        numArray1 = this.buffer.ReadBlock();
                    }
                    else if (tarHeader.TypeFlag == (byte)86)
                    {
                        this.SkipToNextEntry();
                        numArray1 = this.buffer.ReadBlock();
                    }
                    else if (tarHeader.TypeFlag != (byte)48 && tarHeader.TypeFlag != (byte)0 && tarHeader.TypeFlag != (byte)53)
                    {
                        this.SkipToNextEntry();
                        numArray1 = this.buffer.ReadBlock();
                    }
                    if (this.entryFactory == null)
                    {
                        this.currentEntry = new TarEntry(numArray1);
                        if (stringBuilder != null)
                            this.currentEntry.Name = stringBuilder.ToString();
                    }
                    else
                        this.currentEntry = this.entryFactory.CreateEntry(numArray1);
                    this.entryOffset = 0L;
                    this.entrySize = this.currentEntry.Size;
                }
                catch (InvalidHeaderException ex)
                {
                    this.entrySize = 0L;
                    this.entryOffset = 0L;
                    this.currentEntry = (TarEntry)null;
                    throw new InvalidHeaderException(string.Format("Bad header in record {0} block {1} {2}", (object)this.buffer.CurrentRecord, (object)this.buffer.CurrentBlock, (object)ex.Message));
                }
            }
            return this.currentEntry;
        }

        /// <summary>
        /// The CopyEntryContents.
        /// </summary>
        /// <param name="outputStream">The outputStream<see cref="Stream"/>.</param>
        public void CopyEntryContents(Stream outputStream)
        {
            byte[] buffer = new byte[32768];
            while (true)
            {
                int count = this.Read(buffer, 0, buffer.Length);
                if (count > 0)
                    outputStream.Write(buffer, 0, count);
                else
                    break;
            }
        }

        /// <summary>
        /// The SkipToNextEntry.
        /// </summary>
        private void SkipToNextEntry()
        {
            long skipCount = this.entrySize - this.entryOffset;
            if (skipCount > 0L)
                this.Skip(skipCount);
            this.readBuffer = (byte[])null;
        }

        /// <summary>
        /// Defines the <see cref="IEntryFactory" />.
        /// </summary>
        public interface IEntryFactory
        {
            /// <summary>
            /// The CreateEntry.
            /// </summary>
            /// <param name="name">The name<see cref="string"/>.</param>
            /// <returns>The <see cref="TarEntry"/>.</returns>
            TarEntry CreateEntry(string name);

            /// <summary>
            /// The CreateEntryFromFile.
            /// </summary>
            /// <param name="fileName">The fileName<see cref="string"/>.</param>
            /// <returns>The <see cref="TarEntry"/>.</returns>
            TarEntry CreateEntryFromFile(string fileName);

            /// <summary>
            /// The CreateEntry.
            /// </summary>
            /// <param name="headerBuffer">The headerBuffer<see cref="byte[]"/>.</param>
            /// <returns>The <see cref="TarEntry"/>.</returns>
            TarEntry CreateEntry(byte[] headerBuffer);
        }

        /// <summary>
        /// Defines the <see cref="EntryFactoryAdapter" />.
        /// </summary>
        public class EntryFactoryAdapter : TarInputStream.IEntryFactory
        {
            /// <summary>
            /// The CreateEntry.
            /// </summary>
            /// <param name="name">The name<see cref="string"/>.</param>
            /// <returns>The <see cref="TarEntry"/>.</returns>
            public TarEntry CreateEntry(string name)
            {
                return TarEntry.CreateTarEntry(name);
            }

            /// <summary>
            /// The CreateEntryFromFile.
            /// </summary>
            /// <param name="fileName">The fileName<see cref="string"/>.</param>
            /// <returns>The <see cref="TarEntry"/>.</returns>
            public TarEntry CreateEntryFromFile(string fileName)
            {
                return TarEntry.CreateEntryFromFile(fileName);
            }

            /// <summary>
            /// The CreateEntry.
            /// </summary>
            /// <param name="headerBuffer">The headerBuffer<see cref="byte[]"/>.</param>
            /// <returns>The <see cref="TarEntry"/>.</returns>
            public TarEntry CreateEntry(byte[] headerBuffer)
            {
                return new TarEntry(headerBuffer);
            }
        }
    }
}
