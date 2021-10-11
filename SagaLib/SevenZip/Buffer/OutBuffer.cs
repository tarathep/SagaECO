namespace SevenZip.Buffer
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="OutBuffer" />.
    /// </summary>
    public class OutBuffer
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
        /// Defines the m_BufferSize.
        /// </summary>
        private uint m_BufferSize;

        /// <summary>
        /// Defines the m_Stream.
        /// </summary>
        private Stream m_Stream;

        /// <summary>
        /// Defines the m_ProcessedSize.
        /// </summary>
        private ulong m_ProcessedSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutBuffer"/> class.
        /// </summary>
        /// <param name="bufferSize">The bufferSize<see cref="uint"/>.</param>
        public OutBuffer(uint bufferSize)
        {
            this.m_Buffer = new byte[bufferSize];
            this.m_BufferSize = bufferSize;
        }

        /// <summary>
        /// The SetStream.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public void SetStream(Stream stream)
        {
            this.m_Stream = stream;
        }

        /// <summary>
        /// The FlushStream.
        /// </summary>
        public void FlushStream()
        {
            this.m_Stream.Flush();
        }

        /// <summary>
        /// The CloseStream.
        /// </summary>
        public void CloseStream()
        {
            this.m_Stream.Close();
        }

        /// <summary>
        /// The ReleaseStream.
        /// </summary>
        public void ReleaseStream()
        {
            this.m_Stream = (Stream)null;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        public void Init()
        {
            this.m_ProcessedSize = 0UL;
            this.m_Pos = 0U;
        }

        /// <summary>
        /// The WriteByte.
        /// </summary>
        /// <param name="b">The b<see cref="byte"/>.</param>
        public void WriteByte(byte b)
        {
            this.m_Buffer[this.m_Pos++] = b;
            if (this.m_Pos < this.m_BufferSize)
                return;
            this.FlushData();
        }

        /// <summary>
        /// The FlushData.
        /// </summary>
        public void FlushData()
        {
            if (this.m_Pos == 0U)
                return;
            this.m_Stream.Write(this.m_Buffer, 0, (int)this.m_Pos);
            this.m_Pos = 0U;
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
