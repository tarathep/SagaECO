namespace SevenZip.Buffer
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="InBuffer" />.
    /// </summary>
    public class InBuffer
    {
        /// <summary>
        /// Defines the m_Buffer.
        /// </summary>
        private byte[] m_Buffer;

        /// <summary>
        /// Defines the m_Pos.
        /// </summary>
        private uint m_Pos;

        /// <summary>
        /// Defines the m_Limit.
        /// </summary>
        private uint m_Limit;

        /// <summary>
        /// Defines the m_BufferSize.
        /// </summary>
        private uint m_BufferSize;

        /// <summary>
        /// Defines the m_Stream.
        /// </summary>
        private Stream m_Stream;

        /// <summary>
        /// Defines the m_StreamWasExhausted.
        /// </summary>
        private bool m_StreamWasExhausted;

        /// <summary>
        /// Defines the m_ProcessedSize.
        /// </summary>
        private ulong m_ProcessedSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="InBuffer"/> class.
        /// </summary>
        /// <param name="bufferSize">The bufferSize<see cref="uint"/>.</param>
        public InBuffer(uint bufferSize)
        {
            this.m_Buffer = new byte[bufferSize];
            this.m_BufferSize = bufferSize;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public void Init(Stream stream)
        {
            this.m_Stream = stream;
            this.m_ProcessedSize = 0UL;
            this.m_Limit = 0U;
            this.m_Pos = 0U;
            this.m_StreamWasExhausted = false;
        }

        /// <summary>
        /// The ReadBlock.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ReadBlock()
        {
            if (this.m_StreamWasExhausted)
                return false;
            this.m_ProcessedSize += (ulong)this.m_Pos;
            int num = this.m_Stream.Read(this.m_Buffer, 0, (int)this.m_BufferSize);
            this.m_Pos = 0U;
            this.m_Limit = (uint)num;
            this.m_StreamWasExhausted = num == 0;
            return !this.m_StreamWasExhausted;
        }

        /// <summary>
        /// The ReleaseStream.
        /// </summary>
        public void ReleaseStream()
        {
            this.m_Stream = (Stream)null;
        }

        /// <summary>
        /// The ReadByte.
        /// </summary>
        /// <param name="b">The b<see cref="byte"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ReadByte(byte b)
        {
            if (this.m_Pos >= this.m_Limit && !this.ReadBlock())
                return false;
            b = this.m_Buffer[this.m_Pos++];
            return true;
        }

        /// <summary>
        /// The ReadByte.
        /// </summary>
        /// <returns>The <see cref="byte"/>.</returns>
        public byte ReadByte()
        {
            if (this.m_Pos >= this.m_Limit && !this.ReadBlock())
                return byte.MaxValue;
            return this.m_Buffer[this.m_Pos++];
        }

        /// <summary>
        /// The GetProcessedSize.
        /// </summary>
        /// <returns>The <see cref="ulong"/>.</returns>
        public ulong GetProcessedSize()
        {
            return this.m_ProcessedSize + (ulong)this.m_Pos;
        }
    }
}
