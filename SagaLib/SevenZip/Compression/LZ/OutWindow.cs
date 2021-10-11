namespace SevenZip.Compression.LZ
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="OutWindow" />.
    /// </summary>
    public class OutWindow
    {
        /// <summary>
        /// Defines the _buffer.
        /// </summary>
        private byte[] _buffer;

        /// <summary>
        /// Defines the _pos.
        /// </summary>
        private uint _pos;

        /// <summary>
        /// Defines the _windowSize.
        /// </summary>
        private uint _windowSize;

        /// <summary>
        /// Defines the _streamPos.
        /// </summary>
        private uint _streamPos;

        /// <summary>
        /// Defines the _stream.
        /// </summary>
        private Stream _stream;

        /// <summary>
        /// Defines the TrainSize.
        /// </summary>
        public uint TrainSize;

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="windowSize">The windowSize<see cref="uint"/>.</param>
        public void Create(uint windowSize)
        {
            if ((int)this._windowSize != (int)windowSize)
            {
                try
                {
                    this._buffer = new byte[windowSize];
                }
                catch (OutOfMemoryException ex)
                {
                    GC.Collect();
                    this._buffer = new byte[windowSize];
                }
            }
            this._windowSize = windowSize;
            this._pos = 0U;
            this._streamPos = 0U;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <param name="solid">The solid<see cref="bool"/>.</param>
        public void Init(Stream stream, bool solid)
        {
            this.ReleaseStream();
            this._stream = stream;
            if (solid)
                return;
            this._streamPos = 0U;
            this._pos = 0U;
            this.TrainSize = 0U;
        }

        /// <summary>
        /// The Train.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Train(Stream stream)
        {
            long length = stream.Length;
            uint num1 = length < (long)this._windowSize ? (uint)length : this._windowSize;
            this.TrainSize = num1;
            stream.Position = length - (long)num1;
            this._streamPos = this._pos = 0U;
            while (num1 > 0U)
            {
                uint num2 = this._windowSize - this._pos;
                if (num1 < num2)
                    num2 = num1;
                int num3 = stream.Read(this._buffer, (int)this._pos, (int)num2);
                if (num3 == 0)
                    return false;
                num1 -= (uint)num3;
                this._pos += (uint)num3;
                this._streamPos += (uint)num3;
                if ((int)this._pos == (int)this._windowSize)
                    this._streamPos = this._pos = 0U;
            }
            return true;
        }

        /// <summary>
        /// The ReleaseStream.
        /// </summary>
        public void ReleaseStream()
        {
            this.Flush();
            this._stream = (Stream)null;
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        public void Flush()
        {
            uint num = this._pos - this._streamPos;
            if (num == 0U)
                return;
            this._stream.Write(this._buffer, (int)this._streamPos, (int)num);
            if (this._pos >= this._windowSize)
                this._pos = 0U;
            this._streamPos = this._pos;
        }

        /// <summary>
        /// The CopyBlock.
        /// </summary>
        /// <param name="distance">The distance<see cref="uint"/>.</param>
        /// <param name="len">The len<see cref="uint"/>.</param>
        public void CopyBlock(uint distance, uint len)
        {
            uint num = (uint)((int)this._pos - (int)distance - 1);
            if (num >= this._windowSize)
                num += this._windowSize;
            for (; len > 0U; --len)
            {
                if (num >= this._windowSize)
                    num = 0U;
                this._buffer[this._pos++] = this._buffer[num++];
                if (this._pos >= this._windowSize)
                    this.Flush();
            }
        }

        /// <summary>
        /// The PutByte.
        /// </summary>
        /// <param name="b">The b<see cref="byte"/>.</param>
        public void PutByte(byte b)
        {
            this._buffer[this._pos++] = b;
            if (this._pos < this._windowSize)
                return;
            this.Flush();
        }

        /// <summary>
        /// The GetByte.
        /// </summary>
        /// <param name="distance">The distance<see cref="uint"/>.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        public byte GetByte(uint distance)
        {
            uint num = (uint)((int)this._pos - (int)distance - 1);
            if (num >= this._windowSize)
                num += this._windowSize;
            return this._buffer[num];
        }
    }
}
