namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="TarOutputStream" />.
    /// </summary>
    public class TarOutputStream : Stream
    {
        /// <summary>
        /// Defines the currBytes.
        /// </summary>
        private long currBytes;

        /// <summary>
        /// Defines the assemblyBufferLength.
        /// </summary>
        private int assemblyBufferLength;

        /// <summary>
        /// Defines the isClosed.
        /// </summary>
        private bool isClosed;

        /// <summary>
        /// Defines the currSize.
        /// </summary>
        protected long currSize;

        /// <summary>
        /// Defines the blockBuffer.
        /// </summary>
        protected byte[] blockBuffer;

        /// <summary>
        /// Defines the assemblyBuffer.
        /// </summary>
        protected byte[] assemblyBuffer;

        /// <summary>
        /// Defines the buffer.
        /// </summary>
        protected TarBuffer buffer;

        /// <summary>
        /// Defines the outputStream.
        /// </summary>
        protected Stream outputStream;

        /// <summary>
        /// Initializes a new instance of the <see cref="TarOutputStream"/> class.
        /// </summary>
        /// <param name="outputStream">The outputStream<see cref="Stream"/>.</param>
        public TarOutputStream(Stream outputStream)
      : this(outputStream, 20)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TarOutputStream"/> class.
        /// </summary>
        /// <param name="outputStream">The outputStream<see cref="Stream"/>.</param>
        /// <param name="blockFactor">The blockFactor<see cref="int"/>.</param>
        public TarOutputStream(Stream outputStream, int blockFactor)
        {
            if (outputStream == null)
                throw new ArgumentNullException(nameof(outputStream));
            this.outputStream = outputStream;
            this.buffer = TarBuffer.CreateOutputTarBuffer(outputStream, blockFactor);
            this.assemblyBuffer = new byte[512];
            this.blockBuffer = new byte[512];
        }

        /// <summary>
        /// Gets a value indicating whether CanRead.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return this.outputStream.CanRead;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanSeek.
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return this.outputStream.CanSeek;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanWrite.
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return this.outputStream.CanWrite;
            }
        }

        /// <summary>
        /// Gets the Length.
        /// </summary>
        public override long Length
        {
            get
            {
                return this.outputStream.Length;
            }
        }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public override long Position
        {
            get
            {
                return this.outputStream.Position;
            }
            set
            {
                this.outputStream.Position = value;
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
            return this.outputStream.Seek(offset, origin);
        }

        /// <summary>
        /// The SetLength.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public override void SetLength(long value)
        {
            this.outputStream.SetLength(value);
        }

        /// <summary>
        /// The ReadByte.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int ReadByte()
        {
            return this.outputStream.ReadByte();
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
            return this.outputStream.Read(buffer, offset, count);
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        public override void Flush()
        {
            this.outputStream.Flush();
        }

        /// <summary>
        /// The Finish.
        /// </summary>
        public void Finish()
        {
            if (this.IsEntryOpen)
                this.CloseEntry();
            this.WriteEofBlock();
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public override void Close()
        {
            if (this.isClosed)
                return;
            this.isClosed = true;
            this.Finish();
            this.buffer.Close();
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
        /// Gets a value indicating whether IsEntryOpen.
        /// </summary>
        private bool IsEntryOpen
        {
            get
            {
                return this.currBytes < this.currSize;
            }
        }

        /// <summary>
        /// The PutNextEntry.
        /// </summary>
        /// <param name="entry">The entry<see cref="TarEntry"/>.</param>
        public void PutNextEntry(TarEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            if (entry.TarHeader.Name.Length >= 100)
            {
                TarHeader tarHeader = new TarHeader();
                tarHeader.TypeFlag = (byte)76;
                tarHeader.Name += "././@LongLink";
                tarHeader.UserId = 0;
                tarHeader.GroupId = 0;
                tarHeader.GroupName = "";
                tarHeader.UserName = "";
                tarHeader.LinkName = "";
                tarHeader.Size = (long)entry.TarHeader.Name.Length;
                tarHeader.WriteHeader(this.blockBuffer);
                this.buffer.WriteBlock(this.blockBuffer);
                int nameOffset = 0;
                while (nameOffset < entry.TarHeader.Name.Length)
                {
                    Array.Clear((Array)this.blockBuffer, 0, this.blockBuffer.Length);
                    TarHeader.GetAsciiBytes(entry.TarHeader.Name, nameOffset, this.blockBuffer, 0, 512);
                    nameOffset += 512;
                    this.buffer.WriteBlock(this.blockBuffer);
                }
            }
            entry.WriteEntryHeader(this.blockBuffer);
            this.buffer.WriteBlock(this.blockBuffer);
            this.currBytes = 0L;
            this.currSize = entry.IsDirectory ? 0L : entry.Size;
        }

        /// <summary>
        /// The CloseEntry.
        /// </summary>
        public void CloseEntry()
        {
            if (this.assemblyBufferLength > 0)
            {
                Array.Clear((Array)this.assemblyBuffer, this.assemblyBufferLength, this.assemblyBuffer.Length - this.assemblyBufferLength);
                this.buffer.WriteBlock(this.assemblyBuffer);
                this.currBytes += (long)this.assemblyBufferLength;
                this.assemblyBufferLength = 0;
            }
            if (this.currBytes < this.currSize)
                throw new TarException(string.Format("Entry closed at '{0}' before the '{1}' bytes specified in the header were written", (object)this.currBytes, (object)this.currSize));
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
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Cannot be negative");
            if (buffer.Length - offset < count)
                throw new ArgumentException("offset and count combination is invalid");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            if (this.currBytes + (long)count > this.currSize)
                throw new ArgumentOutOfRangeException(nameof(count), string.Format("request to write '{0}' bytes exceeds size in header of '{1}' bytes", (object)count, (object)this.currSize));
            if (this.assemblyBufferLength > 0)
            {
                if (this.assemblyBufferLength + count >= this.blockBuffer.Length)
                {
                    int length = this.blockBuffer.Length - this.assemblyBufferLength;
                    Array.Copy((Array)this.assemblyBuffer, 0, (Array)this.blockBuffer, 0, this.assemblyBufferLength);
                    Array.Copy((Array)buffer, offset, (Array)this.blockBuffer, this.assemblyBufferLength, length);
                    this.buffer.WriteBlock(this.blockBuffer);
                    this.currBytes += (long)this.blockBuffer.Length;
                    offset += length;
                    count -= length;
                    this.assemblyBufferLength = 0;
                }
                else
                {
                    Array.Copy((Array)buffer, offset, (Array)this.assemblyBuffer, this.assemblyBufferLength, count);
                    offset += count;
                    this.assemblyBufferLength += count;
                    count -= count;
                }
            }
            while (count > 0)
            {
                if (count < this.blockBuffer.Length)
                {
                    Array.Copy((Array)buffer, offset, (Array)this.assemblyBuffer, this.assemblyBufferLength, count);
                    this.assemblyBufferLength += count;
                    break;
                }
                this.buffer.WriteBlock(buffer, offset);
                int length = this.blockBuffer.Length;
                this.currBytes += (long)length;
                count -= length;
                offset += length;
            }
        }

        /// <summary>
        /// The WriteEofBlock.
        /// </summary>
        private void WriteEofBlock()
        {
            Array.Clear((Array)this.blockBuffer, 0, this.blockBuffer.Length);
            this.buffer.WriteBlock(this.blockBuffer);
        }
    }
}
