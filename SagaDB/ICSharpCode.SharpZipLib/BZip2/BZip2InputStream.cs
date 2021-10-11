namespace ICSharpCode.SharpZipLib.BZip2
{
    using ICSharpCode.SharpZipLib.Checksums;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="BZip2InputStream" />.
    /// </summary>
    public class BZip2InputStream : Stream
    {
        /// <summary>
        /// Defines the mCrc.
        /// </summary>
        private IChecksum mCrc = (IChecksum)new StrangeCRC();

        /// <summary>
        /// Defines the inUse.
        /// </summary>
        private bool[] inUse = new bool[256];

        /// <summary>
        /// Defines the seqToUnseq.
        /// </summary>
        private byte[] seqToUnseq = new byte[256];

        /// <summary>
        /// Defines the unseqToSeq.
        /// </summary>
        private byte[] unseqToSeq = new byte[256];

        /// <summary>
        /// Defines the selector.
        /// </summary>
        private byte[] selector = new byte[18002];

        /// <summary>
        /// Defines the selectorMtf.
        /// </summary>
        private byte[] selectorMtf = new byte[18002];

        /// <summary>
        /// Defines the unzftab.
        /// </summary>
        private int[] unzftab = new int[256];

        /// <summary>
        /// Defines the limit.
        /// </summary>
        private int[][] limit = new int[6][];

        /// <summary>
        /// Defines the baseArray.
        /// </summary>
        private int[][] baseArray = new int[6][];

        /// <summary>
        /// Defines the perm.
        /// </summary>
        private int[][] perm = new int[6][];

        /// <summary>
        /// Defines the minLens.
        /// </summary>
        private int[] minLens = new int[6];

        /// <summary>
        /// Defines the currentChar.
        /// </summary>
        private int currentChar = -1;

        /// <summary>
        /// Defines the currentState.
        /// </summary>
        private int currentState = 1;

        /// <summary>
        /// Defines the isStreamOwner.
        /// </summary>
        private bool isStreamOwner = true;

        /// <summary>
        /// Defines the START_BLOCK_STATE.
        /// </summary>
        private const int START_BLOCK_STATE = 1;

        /// <summary>
        /// Defines the RAND_PART_A_STATE.
        /// </summary>
        private const int RAND_PART_A_STATE = 2;

        /// <summary>
        /// Defines the RAND_PART_B_STATE.
        /// </summary>
        private const int RAND_PART_B_STATE = 3;

        /// <summary>
        /// Defines the RAND_PART_C_STATE.
        /// </summary>
        private const int RAND_PART_C_STATE = 4;

        /// <summary>
        /// Defines the NO_RAND_PART_A_STATE.
        /// </summary>
        private const int NO_RAND_PART_A_STATE = 5;

        /// <summary>
        /// Defines the NO_RAND_PART_B_STATE.
        /// </summary>
        private const int NO_RAND_PART_B_STATE = 6;

        /// <summary>
        /// Defines the NO_RAND_PART_C_STATE.
        /// </summary>
        private const int NO_RAND_PART_C_STATE = 7;

        /// <summary>
        /// Defines the last.
        /// </summary>
        private int last;

        /// <summary>
        /// Defines the origPtr.
        /// </summary>
        private int origPtr;

        /// <summary>
        /// Defines the blockSize100k.
        /// </summary>
        private int blockSize100k;

        /// <summary>
        /// Defines the blockRandomised.
        /// </summary>
        private bool blockRandomised;

        /// <summary>
        /// Defines the bsBuff.
        /// </summary>
        private int bsBuff;

        /// <summary>
        /// Defines the bsLive.
        /// </summary>
        private int bsLive;

        /// <summary>
        /// Defines the nInUse.
        /// </summary>
        private int nInUse;

        /// <summary>
        /// Defines the tt.
        /// </summary>
        private int[] tt;

        /// <summary>
        /// Defines the ll8.
        /// </summary>
        private byte[] ll8;

        /// <summary>
        /// Defines the baseStream.
        /// </summary>
        private Stream baseStream;

        /// <summary>
        /// Defines the streamEnd.
        /// </summary>
        private bool streamEnd;

        /// <summary>
        /// Defines the storedBlockCRC.
        /// </summary>
        private int storedBlockCRC;

        /// <summary>
        /// Defines the storedCombinedCRC.
        /// </summary>
        private int storedCombinedCRC;

        /// <summary>
        /// Defines the computedBlockCRC.
        /// </summary>
        private int computedBlockCRC;

        /// <summary>
        /// Defines the computedCombinedCRC.
        /// </summary>
        private uint computedCombinedCRC;

        /// <summary>
        /// Defines the count.
        /// </summary>
        private int count;

        /// <summary>
        /// Defines the chPrev.
        /// </summary>
        private int chPrev;

        /// <summary>
        /// Defines the ch2.
        /// </summary>
        private int ch2;

        /// <summary>
        /// Defines the tPos.
        /// </summary>
        private int tPos;

        /// <summary>
        /// Defines the rNToGo.
        /// </summary>
        private int rNToGo;

        /// <summary>
        /// Defines the rTPos.
        /// </summary>
        private int rTPos;

        /// <summary>
        /// Defines the i2.
        /// </summary>
        private int i2;

        /// <summary>
        /// Defines the j2.
        /// </summary>
        private int j2;

        /// <summary>
        /// Defines the z.
        /// </summary>
        private byte z;

        /// <summary>
        /// Initializes a new instance of the <see cref="BZip2InputStream"/> class.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        public BZip2InputStream(Stream stream)
        {
            for (int index = 0; index < 6; ++index)
            {
                this.limit[index] = new int[258];
                this.baseArray[index] = new int[258];
                this.perm[index] = new int[258];
            }
            this.BsSetStream(stream);
            this.Initialize();
            this.InitBlock();
            this.SetupBlock();
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
        /// Gets a value indicating whether CanRead.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return this.baseStream.CanRead;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanSeek.
        /// </summary>
        public override bool CanSeek
        {
            get
            {
                return this.baseStream.CanSeek;
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
                return this.baseStream.Length;
            }
        }

        /// <summary>
        /// Gets or sets the Position.
        /// </summary>
        public override long Position
        {
            get
            {
                return this.baseStream.Position;
            }
            set
            {
                throw new NotSupportedException("BZip2InputStream position cannot be set");
            }
        }

        /// <summary>
        /// The Flush.
        /// </summary>
        public override void Flush()
        {
            if (this.baseStream == null)
                return;
            this.baseStream.Flush();
        }

        /// <summary>
        /// The Seek.
        /// </summary>
        /// <param name="offset">The offset<see cref="long"/>.</param>
        /// <param name="origin">The origin<see cref="SeekOrigin"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("BZip2InputStream Seek not supported");
        }

        /// <summary>
        /// The SetLength.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("BZip2InputStream SetLength not supported");
        }

        /// <summary>
        /// The Write.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("BZip2InputStream Write not supported");
        }

        /// <summary>
        /// The WriteByte.
        /// </summary>
        /// <param name="value">The value<see cref="byte"/>.</param>
        public override void WriteByte(byte value)
        {
            throw new NotSupportedException("BZip2InputStream WriteByte not supported");
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
            for (int index = 0; index < count; ++index)
            {
                int num = this.ReadByte();
                if (num == -1)
                    return index;
                buffer[offset + index] = (byte)num;
            }
            return count;
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public override void Close()
        {
            if (!this.IsStreamOwner || this.baseStream == null)
                return;
            this.baseStream.Close();
        }

        /// <summary>
        /// The ReadByte.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int ReadByte()
        {
            if (this.streamEnd)
                return -1;
            int currentChar = this.currentChar;
            switch (this.currentState)
            {
                case 3:
                    this.SetupRandPartB();
                    break;
                case 4:
                    this.SetupRandPartC();
                    break;
                case 6:
                    this.SetupNoRandPartB();
                    break;
                case 7:
                    this.SetupNoRandPartC();
                    break;
            }
            return currentChar;
        }

        /// <summary>
        /// The MakeMaps.
        /// </summary>
        private void MakeMaps()
        {
            this.nInUse = 0;
            for (int index = 0; index < 256; ++index)
            {
                if (this.inUse[index])
                {
                    this.seqToUnseq[this.nInUse] = (byte)index;
                    this.unseqToSeq[index] = (byte)this.nInUse;
                    ++this.nInUse;
                }
            }
        }

        /// <summary>
        /// The Initialize.
        /// </summary>
        private void Initialize()
        {
            char uchar1 = this.BsGetUChar();
            char uchar2 = this.BsGetUChar();
            char uchar3 = this.BsGetUChar();
            char uchar4 = this.BsGetUChar();
            if (uchar1 != 'B' || uchar2 != 'Z' || (uchar3 != 'h' || uchar4 < '1') || uchar4 > '9')
            {
                this.streamEnd = true;
            }
            else
            {
                this.SetDecompressStructureSizes((int)uchar4 - 48);
                this.computedCombinedCRC = 0U;
            }
        }

        /// <summary>
        /// The InitBlock.
        /// </summary>
        private void InitBlock()
        {
            char uchar1 = this.BsGetUChar();
            char uchar2 = this.BsGetUChar();
            char uchar3 = this.BsGetUChar();
            char uchar4 = this.BsGetUChar();
            char uchar5 = this.BsGetUChar();
            char uchar6 = this.BsGetUChar();
            if (uchar1 == '\x0017' && uchar2 == 'r' && (uchar3 == 'E' && uchar4 == '8') && uchar5 == 'P' && uchar6 == '\x0090')
                this.Complete();
            else if (uchar1 != '1' || uchar2 != 'A' || (uchar3 != 'Y' || uchar4 != '&') || uchar5 != 'S' || uchar6 != 'Y')
            {
                BZip2InputStream.BadBlockHeader();
                this.streamEnd = true;
            }
            else
            {
                this.storedBlockCRC = this.BsGetInt32();
                this.blockRandomised = this.BsR(1) == 1;
                this.GetAndMoveToFrontDecode();
                this.mCrc.Reset();
                this.currentState = 1;
            }
        }

        /// <summary>
        /// The EndBlock.
        /// </summary>
        private void EndBlock()
        {
            this.computedBlockCRC = (int)this.mCrc.Value;
            if (this.storedBlockCRC != this.computedBlockCRC)
                BZip2InputStream.CrcError();
            this.computedCombinedCRC = (uint)((int)this.computedCombinedCRC << 1 & -1) | this.computedCombinedCRC >> 31;
            this.computedCombinedCRC ^= (uint)this.computedBlockCRC;
        }

        /// <summary>
        /// The Complete.
        /// </summary>
        private void Complete()
        {
            this.storedCombinedCRC = this.BsGetInt32();
            if (this.storedCombinedCRC != (int)this.computedCombinedCRC)
                BZip2InputStream.CrcError();
            this.streamEnd = true;
        }

        /// <summary>
        /// The BsSetStream.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        private void BsSetStream(Stream stream)
        {
            this.baseStream = stream;
            this.bsLive = 0;
            this.bsBuff = 0;
        }

        /// <summary>
        /// The FillBuffer.
        /// </summary>
        private void FillBuffer()
        {
            int num = 0;
            try
            {
                num = this.baseStream.ReadByte();
            }
            catch (Exception ex)
            {
                BZip2InputStream.CompressedStreamEOF();
            }
            if (num == -1)
                BZip2InputStream.CompressedStreamEOF();
            this.bsBuff = this.bsBuff << 8 | num & (int)byte.MaxValue;
            this.bsLive += 8;
        }

        /// <summary>
        /// The BsR.
        /// </summary>
        /// <param name="n">The n<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int BsR(int n)
        {
            while (this.bsLive < n)
                this.FillBuffer();
            int num = this.bsBuff >> this.bsLive - n & (1 << n) - 1;
            this.bsLive -= n;
            return num;
        }

        /// <summary>
        /// The BsGetUChar.
        /// </summary>
        /// <returns>The <see cref="char"/>.</returns>
        private char BsGetUChar()
        {
            return (char)this.BsR(8);
        }

        /// <summary>
        /// The BsGetIntVS.
        /// </summary>
        /// <param name="numBits">The numBits<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private int BsGetIntVS(int numBits)
        {
            return this.BsR(numBits);
        }

        /// <summary>
        /// The BsGetInt32.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        private int BsGetInt32()
        {
            return ((this.BsR(8) << 8 | this.BsR(8)) << 8 | this.BsR(8)) << 8 | this.BsR(8);
        }

        /// <summary>
        /// The RecvDecodingTables.
        /// </summary>
        private void RecvDecodingTables()
        {
            char[][] chArray = new char[6][];
            for (int index = 0; index < 6; ++index)
                chArray[index] = new char[258];
            bool[] flagArray = new bool[16];
            for (int index = 0; index < 16; ++index)
                flagArray[index] = this.BsR(1) == 1;
            for (int index1 = 0; index1 < 16; ++index1)
            {
                if (flagArray[index1])
                {
                    for (int index2 = 0; index2 < 16; ++index2)
                        this.inUse[index1 * 16 + index2] = this.BsR(1) == 1;
                }
                else
                {
                    for (int index2 = 0; index2 < 16; ++index2)
                        this.inUse[index1 * 16 + index2] = false;
                }
            }
            this.MakeMaps();
            int alphaSize = this.nInUse + 2;
            int num1 = this.BsR(3);
            int num2 = this.BsR(15);
            for (int index = 0; index < num2; ++index)
            {
                int num3 = 0;
                while (this.BsR(1) == 1)
                    ++num3;
                this.selectorMtf[index] = (byte)num3;
            }
            byte[] numArray = new byte[6];
            for (int index = 0; index < num1; ++index)
                numArray[index] = (byte)index;
            for (int index1 = 0; index1 < num2; ++index1)
            {
                int index2 = (int)this.selectorMtf[index1];
                byte num3 = numArray[index2];
                for (; index2 > 0; --index2)
                    numArray[index2] = numArray[index2 - 1];
                numArray[0] = num3;
                this.selector[index1] = num3;
            }
            for (int index1 = 0; index1 < num1; ++index1)
            {
                int num3 = this.BsR(5);
                for (int index2 = 0; index2 < alphaSize; ++index2)
                {
                    while (this.BsR(1) == 1)
                    {
                        if (this.BsR(1) == 0)
                            ++num3;
                        else
                            --num3;
                    }
                    chArray[index1][index2] = (char)num3;
                }
            }
            for (int index1 = 0; index1 < num1; ++index1)
            {
                int num3 = 32;
                int num4 = 0;
                for (int index2 = 0; index2 < alphaSize; ++index2)
                {
                    num4 = Math.Max(num4, (int)chArray[index1][index2]);
                    num3 = Math.Min(num3, (int)chArray[index1][index2]);
                }
                BZip2InputStream.HbCreateDecodeTables(this.limit[index1], this.baseArray[index1], this.perm[index1], chArray[index1], num3, num4, alphaSize);
                this.minLens[index1] = num3;
            }
        }

        /// <summary>
        /// The GetAndMoveToFrontDecode.
        /// </summary>
        private void GetAndMoveToFrontDecode()
        {
            byte[] numArray = new byte[256];
            int num1 = 100000 * this.blockSize100k;
            this.origPtr = this.BsGetIntVS(24);
            this.RecvDecodingTables();
            int num2 = this.nInUse + 1;
            int index1 = -1;
            int num3 = 0;
            for (int index2 = 0; index2 <= (int)byte.MaxValue; ++index2)
                this.unzftab[index2] = 0;
            for (int index2 = 0; index2 <= (int)byte.MaxValue; ++index2)
                numArray[index2] = (byte)index2;
            this.last = -1;
            if (num3 == 0)
            {
                ++index1;
                num3 = 50;
            }
            int num4 = num3 - 1;
            int index3 = (int)this.selector[index1];
            int minLen1 = this.minLens[index3];
            int num5;
            int num6;
            for (num5 = this.BsR(minLen1); num5 > this.limit[index3][minLen1]; num5 = num5 << 1 | num6)
            {
                if (minLen1 > 20)
                    throw new BZip2Exception("Bzip data error");
                ++minLen1;
                while (this.bsLive < 1)
                    this.FillBuffer();
                num6 = this.bsBuff >> this.bsLive - 1 & 1;
                --this.bsLive;
            }
            if (num5 - this.baseArray[index3][minLen1] < 0 || num5 - this.baseArray[index3][minLen1] >= 258)
                throw new BZip2Exception("Bzip data error");
            int num7 = this.perm[index3][num5 - this.baseArray[index3][minLen1]];
            while (true)
            {
                do
                {
                    if (num7 != num2)
                    {
                        if (num7 == 0 || num7 == 1)
                        {
                            int num8 = -1;
                            int num9 = 1;
                            do
                            {
                                switch (num7)
                                {
                                    case 0:
                                        num8 += num9;
                                        break;
                                    case 1:
                                        num8 += 2 * num9;
                                        break;
                                }
                                num9 <<= 1;
                                if (num4 == 0)
                                {
                                    ++index1;
                                    num4 = 50;
                                }
                                --num4;
                                int index2 = (int)this.selector[index1];
                                int minLen2 = this.minLens[index2];
                                int num10;
                                int num11;
                                for (num10 = this.BsR(minLen2); num10 > this.limit[index2][minLen2]; num10 = num10 << 1 | num11)
                                {
                                    ++minLen2;
                                    while (this.bsLive < 1)
                                        this.FillBuffer();
                                    num11 = this.bsBuff >> this.bsLive - 1 & 1;
                                    --this.bsLive;
                                }
                                num7 = this.perm[index2][num10 - this.baseArray[index2][minLen2]];
                            }
                            while (num7 == 0 || num7 == 1);
                            int num12 = num8 + 1;
                            byte num13 = this.seqToUnseq[(int)numArray[0]];
                            this.unzftab[(int)num13] += num12;
                            for (; num12 > 0; --num12)
                            {
                                ++this.last;
                                this.ll8[this.last] = num13;
                            }
                        }
                        else
                            goto label_39;
                    }
                    else
                        goto label_38;
                }
                while (this.last < num1);
                BZip2InputStream.BlockOverrun();
                continue;
            label_39:
                ++this.last;
                if (this.last >= num1)
                    BZip2InputStream.BlockOverrun();
                byte num14 = numArray[num7 - 1];
                ++this.unzftab[(int)this.seqToUnseq[(int)num14]];
                this.ll8[this.last] = this.seqToUnseq[(int)num14];
                for (int index2 = num7 - 1; index2 > 0; --index2)
                    numArray[index2] = numArray[index2 - 1];
                numArray[0] = num14;
                if (num4 == 0)
                {
                    ++index1;
                    num4 = 50;
                }
                --num4;
                int index4 = (int)this.selector[index1];
                int minLen3 = this.minLens[index4];
                int num15;
                int num16;
                for (num15 = this.BsR(minLen3); num15 > this.limit[index4][minLen3]; num15 = num15 << 1 | num16)
                {
                    ++minLen3;
                    while (this.bsLive < 1)
                        this.FillBuffer();
                    num16 = this.bsBuff >> this.bsLive - 1 & 1;
                    --this.bsLive;
                }
                num7 = this.perm[index4][num15 - this.baseArray[index4][minLen3]];
            }
        label_38:;
        }

        /// <summary>
        /// The SetupBlock.
        /// </summary>
        private void SetupBlock()
        {
            int[] numArray = new int[257];
            numArray[0] = 0;
            Array.Copy((Array)this.unzftab, 0, (Array)numArray, 1, 256);
            for (int index = 1; index <= 256; ++index)
                numArray[index] += numArray[index - 1];
            for (int index = 0; index <= this.last; ++index)
            {
                byte num = this.ll8[index];
                this.tt[numArray[(int)num]] = index;
                ++numArray[(int)num];
            }
            this.tPos = this.tt[this.origPtr];
            this.count = 0;
            this.i2 = 0;
            this.ch2 = 256;
            if (this.blockRandomised)
            {
                this.rNToGo = 0;
                this.rTPos = 0;
                this.SetupRandPartA();
            }
            else
                this.SetupNoRandPartA();
        }

        /// <summary>
        /// The SetupRandPartA.
        /// </summary>
        private void SetupRandPartA()
        {
            if (this.i2 <= this.last)
            {
                this.chPrev = this.ch2;
                this.ch2 = (int)this.ll8[this.tPos];
                this.tPos = this.tt[this.tPos];
                if (this.rNToGo == 0)
                {
                    this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
                    ++this.rTPos;
                    if (this.rTPos == 512)
                        this.rTPos = 0;
                }
                --this.rNToGo;
                this.ch2 ^= this.rNToGo == 1 ? 1 : 0;
                ++this.i2;
                this.currentChar = this.ch2;
                this.currentState = 3;
                this.mCrc.Update(this.ch2);
            }
            else
            {
                this.EndBlock();
                this.InitBlock();
                this.SetupBlock();
            }
        }

        /// <summary>
        /// The SetupNoRandPartA.
        /// </summary>
        private void SetupNoRandPartA()
        {
            if (this.i2 <= this.last)
            {
                this.chPrev = this.ch2;
                this.ch2 = (int)this.ll8[this.tPos];
                this.tPos = this.tt[this.tPos];
                ++this.i2;
                this.currentChar = this.ch2;
                this.currentState = 6;
                this.mCrc.Update(this.ch2);
            }
            else
            {
                this.EndBlock();
                this.InitBlock();
                this.SetupBlock();
            }
        }

        /// <summary>
        /// The SetupRandPartB.
        /// </summary>
        private void SetupRandPartB()
        {
            if (this.ch2 != this.chPrev)
            {
                this.currentState = 2;
                this.count = 1;
                this.SetupRandPartA();
            }
            else
            {
                ++this.count;
                if (this.count >= 4)
                {
                    this.z = this.ll8[this.tPos];
                    this.tPos = this.tt[this.tPos];
                    if (this.rNToGo == 0)
                    {
                        this.rNToGo = BZip2Constants.RandomNumbers[this.rTPos];
                        ++this.rTPos;
                        if (this.rTPos == 512)
                            this.rTPos = 0;
                    }
                    --this.rNToGo;
                    this.z ^= this.rNToGo == 1 ? (byte)1 : (byte)0;
                    this.j2 = 0;
                    this.currentState = 4;
                    this.SetupRandPartC();
                }
                else
                {
                    this.currentState = 2;
                    this.SetupRandPartA();
                }
            }
        }

        /// <summary>
        /// The SetupRandPartC.
        /// </summary>
        private void SetupRandPartC()
        {
            if (this.j2 < (int)this.z)
            {
                this.currentChar = this.ch2;
                this.mCrc.Update(this.ch2);
                ++this.j2;
            }
            else
            {
                this.currentState = 2;
                ++this.i2;
                this.count = 0;
                this.SetupRandPartA();
            }
        }

        /// <summary>
        /// The SetupNoRandPartB.
        /// </summary>
        private void SetupNoRandPartB()
        {
            if (this.ch2 != this.chPrev)
            {
                this.currentState = 5;
                this.count = 1;
                this.SetupNoRandPartA();
            }
            else
            {
                ++this.count;
                if (this.count >= 4)
                {
                    this.z = this.ll8[this.tPos];
                    this.tPos = this.tt[this.tPos];
                    this.currentState = 7;
                    this.j2 = 0;
                    this.SetupNoRandPartC();
                }
                else
                {
                    this.currentState = 5;
                    this.SetupNoRandPartA();
                }
            }
        }

        /// <summary>
        /// The SetupNoRandPartC.
        /// </summary>
        private void SetupNoRandPartC()
        {
            if (this.j2 < (int)this.z)
            {
                this.currentChar = this.ch2;
                this.mCrc.Update(this.ch2);
                ++this.j2;
            }
            else
            {
                this.currentState = 5;
                ++this.i2;
                this.count = 0;
                this.SetupNoRandPartA();
            }
        }

        /// <summary>
        /// The SetDecompressStructureSizes.
        /// </summary>
        /// <param name="newSize100k">The newSize100k<see cref="int"/>.</param>
        private void SetDecompressStructureSizes(int newSize100k)
        {
            if (0 > newSize100k || newSize100k > 9 || 0 > this.blockSize100k || this.blockSize100k > 9)
                throw new BZip2Exception("Invalid block size");
            this.blockSize100k = newSize100k;
            if (newSize100k == 0)
                return;
            int length = 100000 * newSize100k;
            this.ll8 = new byte[length];
            this.tt = new int[length];
        }

        /// <summary>
        /// The CompressedStreamEOF.
        /// </summary>
        private static void CompressedStreamEOF()
        {
            throw new EndOfStreamException("BZip2 input stream end of compressed stream");
        }

        /// <summary>
        /// The BlockOverrun.
        /// </summary>
        private static void BlockOverrun()
        {
            throw new BZip2Exception("BZip2 input stream block overrun");
        }

        /// <summary>
        /// The BadBlockHeader.
        /// </summary>
        private static void BadBlockHeader()
        {
            throw new BZip2Exception("BZip2 input stream bad block header");
        }

        /// <summary>
        /// The CrcError.
        /// </summary>
        private static void CrcError()
        {
            throw new BZip2Exception("BZip2 input stream crc error");
        }

        /// <summary>
        /// The HbCreateDecodeTables.
        /// </summary>
        /// <param name="limit">The limit<see cref="int[]"/>.</param>
        /// <param name="baseArray">The baseArray<see cref="int[]"/>.</param>
        /// <param name="perm">The perm<see cref="int[]"/>.</param>
        /// <param name="length">The length<see cref="char[]"/>.</param>
        /// <param name="minLen">The minLen<see cref="int"/>.</param>
        /// <param name="maxLen">The maxLen<see cref="int"/>.</param>
        /// <param name="alphaSize">The alphaSize<see cref="int"/>.</param>
        private static void HbCreateDecodeTables(int[] limit, int[] baseArray, int[] perm, char[] length, int minLen, int maxLen, int alphaSize)
        {
            int index1 = 0;
            for (int index2 = minLen; index2 <= maxLen; ++index2)
            {
                for (int index3 = 0; index3 < alphaSize; ++index3)
                {
                    if ((int)length[index3] == index2)
                    {
                        perm[index1] = index3;
                        ++index1;
                    }
                }
            }
            for (int index2 = 0; index2 < 23; ++index2)
                baseArray[index2] = 0;
            for (int index2 = 0; index2 < alphaSize; ++index2)
                ++baseArray[(int)length[index2] + 1];
            for (int index2 = 1; index2 < 23; ++index2)
                baseArray[index2] += baseArray[index2 - 1];
            for (int index2 = 0; index2 < 23; ++index2)
                limit[index2] = 0;
            int num1 = 0;
            for (int index2 = minLen; index2 <= maxLen; ++index2)
            {
                int num2 = num1 + (baseArray[index2 + 1] - baseArray[index2]);
                limit[index2] = num2 - 1;
                num1 = num2 << 1;
            }
            for (int index2 = minLen + 1; index2 <= maxLen; ++index2)
                baseArray[index2] = (limit[index2 - 1] + 1 << 1) - baseArray[index2];
        }
    }
}
