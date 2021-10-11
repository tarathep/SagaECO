namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="TarBuffer" />.
    /// </summary>
    public class TarBuffer
    {
        /// <summary>
        /// Defines the recordSize.
        /// </summary>
        private int recordSize = 10240;

        /// <summary>
        /// Defines the blockFactor.
        /// </summary>
        private int blockFactor = 20;

        /// <summary>
        /// Defines the BlockSize.
        /// </summary>
        public const int BlockSize = 512;

        /// <summary>
        /// Defines the DefaultBlockFactor.
        /// </summary>
        public const int DefaultBlockFactor = 20;

        /// <summary>
        /// Defines the DefaultRecordSize.
        /// </summary>
        public const int DefaultRecordSize = 10240;

        /// <summary>
        /// Defines the inputStream.
        /// </summary>
        private Stream inputStream;

        /// <summary>
        /// Defines the outputStream.
        /// </summary>
        private Stream outputStream;

        /// <summary>
        /// Defines the recordBuffer.
        /// </summary>
        private byte[] recordBuffer;

        /// <summary>
        /// Defines the currentBlockIndex.
        /// </summary>
        private int currentBlockIndex;

        /// <summary>
        /// Defines the currentRecordIndex.
        /// </summary>
        private int currentRecordIndex;

        /// <summary>
        /// Gets the RecordSize.
        /// </summary>
        public int RecordSize
        {
            get
            {
                return this.recordSize;
            }
        }

        /// <summary>
        /// The GetRecordSize.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [Obsolete("Use RecordSize property instead")]
        public int GetRecordSize()
        {
            return this.recordSize;
        }

        /// <summary>
        /// Gets the BlockFactor.
        /// </summary>
        public int BlockFactor
        {
            get
            {
                return this.blockFactor;
            }
        }

        /// <summary>
        /// The GetBlockFactor.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [Obsolete("Use BlockFactor property instead")]
        public int GetBlockFactor()
        {
            return this.blockFactor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TarBuffer"/> class.
        /// </summary>
        protected TarBuffer()
        {
        }

        /// <summary>
        /// The CreateInputTarBuffer.
        /// </summary>
        /// <param name="inputStream">The inputStream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="TarBuffer"/>.</returns>
        public static TarBuffer CreateInputTarBuffer(Stream inputStream)
        {
            if (inputStream == null)
                throw new ArgumentNullException(nameof(inputStream));
            return TarBuffer.CreateInputTarBuffer(inputStream, 20);
        }

        /// <summary>
        /// The CreateInputTarBuffer.
        /// </summary>
        /// <param name="inputStream">The inputStream<see cref="Stream"/>.</param>
        /// <param name="blockFactor">The blockFactor<see cref="int"/>.</param>
        /// <returns>The <see cref="TarBuffer"/>.</returns>
        public static TarBuffer CreateInputTarBuffer(Stream inputStream, int blockFactor)
        {
            if (inputStream == null)
                throw new ArgumentNullException(nameof(inputStream));
            if (blockFactor <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockFactor), "Factor cannot be negative");
            TarBuffer tarBuffer = new TarBuffer();
            tarBuffer.inputStream = inputStream;
            tarBuffer.outputStream = (Stream)null;
            tarBuffer.Initialize(blockFactor);
            return tarBuffer;
        }

        /// <summary>
        /// The CreateOutputTarBuffer.
        /// </summary>
        /// <param name="outputStream">The outputStream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="TarBuffer"/>.</returns>
        public static TarBuffer CreateOutputTarBuffer(Stream outputStream)
        {
            if (outputStream == null)
                throw new ArgumentNullException(nameof(outputStream));
            return TarBuffer.CreateOutputTarBuffer(outputStream, 20);
        }

        /// <summary>
        /// The CreateOutputTarBuffer.
        /// </summary>
        /// <param name="outputStream">The outputStream<see cref="Stream"/>.</param>
        /// <param name="blockFactor">The blockFactor<see cref="int"/>.</param>
        /// <returns>The <see cref="TarBuffer"/>.</returns>
        public static TarBuffer CreateOutputTarBuffer(Stream outputStream, int blockFactor)
        {
            if (outputStream == null)
                throw new ArgumentNullException(nameof(outputStream));
            if (blockFactor <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockFactor), "Factor cannot be negative");
            TarBuffer tarBuffer = new TarBuffer();
            tarBuffer.inputStream = (Stream)null;
            tarBuffer.outputStream = outputStream;
            tarBuffer.Initialize(blockFactor);
            return tarBuffer;
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        /// <param name="blockFactor">The blockFactor<see cref="int"/>.</param>
        private void Initialize(int blockFactor)
        {
            this.blockFactor = blockFactor;
            this.recordSize = blockFactor * 512;
            this.recordBuffer = new byte[this.RecordSize];
            if (this.inputStream != null)
            {
                this.currentRecordIndex = -1;
                this.currentBlockIndex = this.BlockFactor;
            }
            else
            {
                this.currentRecordIndex = 0;
                this.currentBlockIndex = 0;
            }
        }

        /// <summary>
        /// The IsEOFBlock.
        /// </summary>
        /// <param name="block">The block<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        [Obsolete("Use IsEndOfArchiveBlock instead")]
        public bool IsEOFBlock(byte[] block)
        {
            if (block == null)
                throw new ArgumentNullException(nameof(block));
            if (block.Length != 512)
                throw new ArgumentException("block length is invalid");
            for (int index = 0; index < 512; ++index)
            {
                if (block[index] != (byte)0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// The IsEndOfArchiveBlock.
        /// </summary>
        /// <param name="block">The block<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsEndOfArchiveBlock(byte[] block)
        {
            if (block == null)
                throw new ArgumentNullException(nameof(block));
            if (block.Length != 512)
                throw new ArgumentException("block length is invalid");
            for (int index = 0; index < 512; ++index)
            {
                if (block[index] != (byte)0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// The SkipBlock.
        /// </summary>
        public void SkipBlock()
        {
            if (this.inputStream == null)
                throw new TarException("no input stream defined");
            if (this.currentBlockIndex >= this.BlockFactor && !this.ReadRecord())
                throw new TarException("Failed to read a record");
            ++this.currentBlockIndex;
        }

        /// <summary>
        /// The ReadBlock.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] ReadBlock()
        {
            if (this.inputStream == null)
                throw new TarException("TarBuffer.ReadBlock - no input stream defined");
            if (this.currentBlockIndex >= this.BlockFactor && !this.ReadRecord())
                throw new TarException("Failed to read a record");
            byte[] numArray = new byte[512];
            Array.Copy((Array)this.recordBuffer, this.currentBlockIndex * 512, (Array)numArray, 0, 512);
            ++this.currentBlockIndex;
            return numArray;
        }

        /// <summary>
        /// The ReadRecord.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool ReadRecord()
        {
            if (this.inputStream == null)
                throw new TarException("no input stream stream defined");
            this.currentBlockIndex = 0;
            int offset = 0;
            int recordSize = this.RecordSize;
            while (recordSize > 0)
            {
                long num = (long)this.inputStream.Read(this.recordBuffer, offset, recordSize);
                if (num > 0L)
                {
                    offset += (int)num;
                    recordSize -= (int)num;
                }
                else
                    break;
            }
            ++this.currentRecordIndex;
            return true;
        }

        /// <summary>
        /// Gets the CurrentBlock.
        /// </summary>
        public int CurrentBlock
        {
            get
            {
                return this.currentBlockIndex;
            }
        }

        /// <summary>
        /// The GetCurrentBlockNum.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [Obsolete("Use CurrentBlock property instead")]
        public int GetCurrentBlockNum()
        {
            return this.currentBlockIndex;
        }

        /// <summary>
        /// Gets the CurrentRecord.
        /// </summary>
        public int CurrentRecord
        {
            get
            {
                return this.currentRecordIndex;
            }
        }

        /// <summary>
        /// The GetCurrentRecordNum.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        [Obsolete("Use CurrentRecord property instead")]
        public int GetCurrentRecordNum()
        {
            return this.currentRecordIndex;
        }

        /// <summary>
        /// The WriteBlock.
        /// </summary>
        /// <param name="block">The block<see cref="byte[]"/>.</param>
        public void WriteBlock(byte[] block)
        {
            if (block == null)
                throw new ArgumentNullException(nameof(block));
            if (this.outputStream == null)
                throw new TarException("TarBuffer.WriteBlock - no output stream defined");
            if (block.Length != 512)
                throw new TarException(string.Format("TarBuffer.WriteBlock - block to write has length '{0}' which is not the block size of '{1}'", (object)block.Length, (object)512));
            if (this.currentBlockIndex >= this.BlockFactor)
                this.WriteRecord();
            Array.Copy((Array)block, 0, (Array)this.recordBuffer, this.currentBlockIndex * 512, 512);
            ++this.currentBlockIndex;
        }

        /// <summary>
        /// The WriteBlock.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        public void WriteBlock(byte[] buffer, int offset)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (this.outputStream == null)
                throw new TarException("TarBuffer.WriteBlock - no output stream stream defined");
            if (offset < 0 || offset >= buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (offset + 512 > buffer.Length)
                throw new TarException(string.Format("TarBuffer.WriteBlock - record has length '{0}' with offset '{1}' which is less than the record size of '{2}'", (object)buffer.Length, (object)offset, (object)this.recordSize));
            if (this.currentBlockIndex >= this.BlockFactor)
                this.WriteRecord();
            Array.Copy((Array)buffer, offset, (Array)this.recordBuffer, this.currentBlockIndex * 512, 512);
            ++this.currentBlockIndex;
        }

        /// <summary>
        /// The WriteRecord.
        /// </summary>
        private void WriteRecord()
        {
            if (this.outputStream == null)
                throw new TarException("TarBuffer.WriteRecord no output stream defined");
            this.outputStream.Write(this.recordBuffer, 0, this.RecordSize);
            this.outputStream.Flush();
            this.currentBlockIndex = 0;
            ++this.currentRecordIndex;
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        private void Flush()
        {
            if (this.outputStream == null)
                throw new TarException("TarBuffer.Flush no output stream defined");
            if (this.currentBlockIndex > 0)
            {
                int index = this.currentBlockIndex * 512;
                Array.Clear((Array)this.recordBuffer, index, this.RecordSize - index);
                this.WriteRecord();
            }
            this.outputStream.Flush();
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public void Close()
        {
            if (this.outputStream != null)
            {
                this.Flush();
                this.outputStream.Close();
                this.outputStream = (Stream)null;
            }
            else
            {
                if (this.inputStream == null)
                    return;
                this.inputStream.Close();
                this.inputStream = (Stream)null;
            }
        }
    }
}
