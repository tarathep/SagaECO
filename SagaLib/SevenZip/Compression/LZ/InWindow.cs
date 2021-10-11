namespace SevenZip.Compression.LZ
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="InWindow" />.
    /// </summary>
    public class InWindow
    {
        /// <summary>
        /// Defines the _bufferBase.
        /// </summary>
        public byte[] _bufferBase;

        /// <summary>
        /// Defines the _stream.
        /// </summary>
        private Stream _stream;

        /// <summary>
        /// Defines the _posLimit.
        /// </summary>
        private uint _posLimit;

        /// <summary>
        /// Defines the _streamEndWasReached.
        /// </summary>
        private bool _streamEndWasReached;

        /// <summary>
        /// Defines the _pointerToLastSafePosition.
        /// </summary>
        private uint _pointerToLastSafePosition;

        /// <summary>
        /// Defines the _bufferOffset.
        /// </summary>
        public uint _bufferOffset;

        /// <summary>
        /// Defines the _blockSize.
        /// </summary>
        public uint _blockSize;

        /// <summary>
        /// Defines the _pos.
        /// </summary>
        public uint _pos;

        /// <summary>
        /// Defines the _keepSizeBefore.
        /// </summary>
        private uint _keepSizeBefore;

        /// <summary>
        /// Defines the _keepSizeAfter.
        /// </summary>
        private uint _keepSizeAfter;

        /// <summary>
        /// Defines the _streamPos.
        /// </summary>
        public uint _streamPos;

        /// <summary>
        /// The MoveBlock.
        /// </summary>
        public void MoveBlock()
        {
            uint num1 = this._bufferOffset + this._pos - this._keepSizeBefore;
            if (num1 > 0U)
                --num1;
            uint num2 = this._bufferOffset + this._streamPos - num1;
            for (uint index = 0; index < num2; ++index)
                this._bufferBase[index] = this._bufferBase[(num1 + index)];
            this._bufferOffset -= num1;
        }

        /// <summary>
        /// The ReadBlock.
        /// </summary>
        public virtual void ReadBlock()
        {
            if (this._streamEndWasReached)
                return;
            while (true)
            {
                do
                {
                    int count = -(int)this._bufferOffset + (int)this._blockSize - (int)this._streamPos;
                    if (count == 0)
                        return;
                    int num = this._stream.Read(this._bufferBase, (int)this._bufferOffset + (int)this._streamPos, count);
                    if (num == 0)
                    {
                        this._posLimit = this._streamPos;
                        if (this._bufferOffset + this._posLimit > this._pointerToLastSafePosition)
                            this._posLimit = this._pointerToLastSafePosition - this._bufferOffset;
                        this._streamEndWasReached = true;
                        return;
                    }
                    this._streamPos += (uint)num;
                }
                while (this._streamPos < this._pos + this._keepSizeAfter);
                this._posLimit = this._streamPos - this._keepSizeAfter;
            }
        }

        /// <summary>
        /// The Free.
        /// </summary>
        private void Free()
        {
            this._bufferBase = (byte[])null;
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="keepSizeBefore">The keepSizeBefore<see cref="uint"/>.</param>
        /// <param name="keepSizeAfter">The keepSizeAfter<see cref="uint"/>.</param>
        /// <param name="keepSizeReserv">The keepSizeReserv<see cref="uint"/>.</param>
        public void Create(uint keepSizeBefore, uint keepSizeAfter, uint keepSizeReserv)
        {
            this._keepSizeBefore = keepSizeBefore;
            this._keepSizeAfter = keepSizeAfter;
            uint num = keepSizeBefore + keepSizeAfter + keepSizeReserv;
            if (this._bufferBase == null || (int)this._blockSize != (int)num)
            {
                this.Free();
                this._blockSize = num;
                this._bufferBase = new byte[this._blockSize];
            }
            this._pointerToLastSafePosition = this._blockSize - keepSizeAfter;
        }

        /// <summary>
        /// The SetStream.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public void SetStream(Stream stream)
        {
            this._stream = stream;
        }

        /// <summary>
        /// The ReleaseStream.
        /// </summary>
        public void ReleaseStream()
        {
            this._stream = (Stream)null;
        }

        /// <summary>
        /// The Init.
        /// </summary>
        public void Init()
        {
            this._bufferOffset = 0U;
            this._pos = 0U;
            this._streamPos = 0U;
            this._streamEndWasReached = false;
            this.ReadBlock();
        }

        /// <summary>
        /// The MovePos.
        /// </summary>
        public void MovePos()
        {
            ++this._pos;
            if (this._pos <= this._posLimit)
                return;
            if (this._bufferOffset + this._pos > this._pointerToLastSafePosition)
                this.MoveBlock();
            this.ReadBlock();
        }

        /// <summary>
        /// The GetIndexByte.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        public byte GetIndexByte(int index)
        {
            return this._bufferBase[(long)(this._bufferOffset + this._pos) + (long)index];
        }

        /// <summary>
        /// The GetMatchLen.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="distance">The distance<see cref="uint"/>.</param>
        /// <param name="limit">The limit<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetMatchLen(int index, uint distance, uint limit)
        {
            if (this._streamEndWasReached && (long)this._pos + (long)index + (long)limit > (long)this._streamPos)
                limit = this._streamPos - (uint)((ulong)this._pos + (ulong)index);
            ++distance;
            uint num1 = (uint)((int)this._bufferOffset + (int)this._pos + index);
            uint num2 = 0;
            while (num2 < limit && (int)this._bufferBase[(num1 + num2)] == (int)this._bufferBase[(num1 + num2 - distance)])
                ++num2;
            return num2;
        }

        /// <summary>
        /// The GetNumAvailableBytes.
        /// </summary>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetNumAvailableBytes()
        {
            return this._streamPos - this._pos;
        }

        /// <summary>
        /// The ReduceOffsets.
        /// </summary>
        /// <param name="subValue">The subValue<see cref="int"/>.</param>
        public void ReduceOffsets(int subValue)
        {
            this._bufferOffset += (uint)subValue;
            this._posLimit -= (uint)subValue;
            this._pos -= (uint)subValue;
            this._streamPos -= (uint)subValue;
        }
    }
}
