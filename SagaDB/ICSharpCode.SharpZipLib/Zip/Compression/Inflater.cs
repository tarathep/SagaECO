namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using ICSharpCode.SharpZipLib.Checksums;
    using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
    using System;

    /// <summary>
    /// Defines the <see cref="Inflater" />.
    /// </summary>
    public class Inflater
    {
        /// <summary>
        /// Defines the CPLENS.
        /// </summary>
        private static readonly int[] CPLENS = new int[29]
    {
      3,
      4,
      5,
      6,
      7,
      8,
      9,
      10,
      11,
      13,
      15,
      17,
      19,
      23,
      27,
      31,
      35,
      43,
      51,
      59,
      67,
      83,
      99,
      115,
      131,
      163,
      195,
      227,
      258
    };

        /// <summary>
        /// Defines the CPLEXT.
        /// </summary>
        private static readonly int[] CPLEXT = new int[29]
    {
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      0,
      1,
      1,
      1,
      1,
      2,
      2,
      2,
      2,
      3,
      3,
      3,
      3,
      4,
      4,
      4,
      4,
      5,
      5,
      5,
      5,
      0
    };

        /// <summary>
        /// Defines the CPDIST.
        /// </summary>
        private static readonly int[] CPDIST = new int[30]
    {
      1,
      2,
      3,
      4,
      5,
      7,
      9,
      13,
      17,
      25,
      33,
      49,
      65,
      97,
      129,
      193,
      257,
      385,
      513,
      769,
      1025,
      1537,
      2049,
      3073,
      4097,
      6145,
      8193,
      12289,
      16385,
      24577
    };

        /// <summary>
        /// Defines the CPDEXT.
        /// </summary>
        private static readonly int[] CPDEXT = new int[30]
    {
      0,
      0,
      0,
      0,
      1,
      1,
      2,
      2,
      3,
      3,
      4,
      4,
      5,
      5,
      6,
      6,
      7,
      7,
      8,
      8,
      9,
      9,
      10,
      10,
      11,
      11,
      12,
      12,
      13,
      13
    };

        /// <summary>
        /// Defines the DECODE_HEADER.
        /// </summary>
        private const int DECODE_HEADER = 0;

        /// <summary>
        /// Defines the DECODE_DICT.
        /// </summary>
        private const int DECODE_DICT = 1;

        /// <summary>
        /// Defines the DECODE_BLOCKS.
        /// </summary>
        private const int DECODE_BLOCKS = 2;

        /// <summary>
        /// Defines the DECODE_STORED_LEN1.
        /// </summary>
        private const int DECODE_STORED_LEN1 = 3;

        /// <summary>
        /// Defines the DECODE_STORED_LEN2.
        /// </summary>
        private const int DECODE_STORED_LEN2 = 4;

        /// <summary>
        /// Defines the DECODE_STORED.
        /// </summary>
        private const int DECODE_STORED = 5;

        /// <summary>
        /// Defines the DECODE_DYN_HEADER.
        /// </summary>
        private const int DECODE_DYN_HEADER = 6;

        /// <summary>
        /// Defines the DECODE_HUFFMAN.
        /// </summary>
        private const int DECODE_HUFFMAN = 7;

        /// <summary>
        /// Defines the DECODE_HUFFMAN_LENBITS.
        /// </summary>
        private const int DECODE_HUFFMAN_LENBITS = 8;

        /// <summary>
        /// Defines the DECODE_HUFFMAN_DIST.
        /// </summary>
        private const int DECODE_HUFFMAN_DIST = 9;

        /// <summary>
        /// Defines the DECODE_HUFFMAN_DISTBITS.
        /// </summary>
        private const int DECODE_HUFFMAN_DISTBITS = 10;

        /// <summary>
        /// Defines the DECODE_CHKSUM.
        /// </summary>
        private const int DECODE_CHKSUM = 11;

        /// <summary>
        /// Defines the FINISHED.
        /// </summary>
        private const int FINISHED = 12;

        /// <summary>
        /// Defines the mode.
        /// </summary>
        private int mode;

        /// <summary>
        /// Defines the readAdler.
        /// </summary>
        private int readAdler;

        /// <summary>
        /// Defines the neededBits.
        /// </summary>
        private int neededBits;

        /// <summary>
        /// Defines the repLength.
        /// </summary>
        private int repLength;

        /// <summary>
        /// Defines the repDist.
        /// </summary>
        private int repDist;

        /// <summary>
        /// Defines the uncomprLen.
        /// </summary>
        private int uncomprLen;

        /// <summary>
        /// Defines the isLastBlock.
        /// </summary>
        private bool isLastBlock;

        /// <summary>
        /// Defines the totalOut.
        /// </summary>
        private long totalOut;

        /// <summary>
        /// Defines the totalIn.
        /// </summary>
        private long totalIn;

        /// <summary>
        /// Defines the noHeader.
        /// </summary>
        private bool noHeader;

        /// <summary>
        /// Defines the input.
        /// </summary>
        private StreamManipulator input;

        /// <summary>
        /// Defines the outputWindow.
        /// </summary>
        private OutputWindow outputWindow;

        /// <summary>
        /// Defines the dynHeader.
        /// </summary>
        private InflaterDynHeader dynHeader;

        /// <summary>
        /// Defines the litlenTree.
        /// </summary>
        private InflaterHuffmanTree litlenTree;

        /// <summary>
        /// Defines the distTree.
        /// </summary>
        private InflaterHuffmanTree distTree;

        /// <summary>
        /// Defines the adler.
        /// </summary>
        private Adler32 adler;

        /// <summary>
        /// Initializes a new instance of the <see cref="Inflater"/> class.
        /// </summary>
        public Inflater()
      : this(false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Inflater"/> class.
        /// </summary>
        /// <param name="noHeader">The noHeader<see cref="bool"/>.</param>
        public Inflater(bool noHeader)
        {
            this.noHeader = noHeader;
            this.adler = new Adler32();
            this.input = new StreamManipulator();
            this.outputWindow = new OutputWindow();
            this.mode = noHeader ? 2 : 0;
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        public void Reset()
        {
            this.mode = this.noHeader ? 2 : 0;
            this.totalIn = 0L;
            this.totalOut = 0L;
            this.input.Reset();
            this.outputWindow.Reset();
            this.dynHeader = (InflaterDynHeader)null;
            this.litlenTree = (InflaterHuffmanTree)null;
            this.distTree = (InflaterHuffmanTree)null;
            this.isLastBlock = false;
            this.adler.Reset();
        }

        /// <summary>
        /// The DecodeHeader.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool DecodeHeader()
        {
            int num1 = this.input.PeekBits(16);
            if (num1 < 0)
                return false;
            this.input.DropBits(16);
            int num2 = (num1 << 8 | num1 >> 8) & (int)ushort.MaxValue;
            if (num2 % 31 != 0)
                throw new SharpZipBaseException("Header checksum illegal");
            if ((num2 & 3840) != 2048)
                throw new SharpZipBaseException("Compression Method unknown");
            if ((num2 & 32) == 0)
            {
                this.mode = 2;
            }
            else
            {
                this.mode = 1;
                this.neededBits = 32;
            }
            return true;
        }

        /// <summary>
        /// The DecodeDict.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool DecodeDict()
        {
            while (this.neededBits > 0)
            {
                int num = this.input.PeekBits(8);
                if (num < 0)
                    return false;
                this.input.DropBits(8);
                this.readAdler = this.readAdler << 8 | num;
                this.neededBits -= 8;
            }
            return false;
        }

        /// <summary>
        /// The DecodeHuffman.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool DecodeHuffman()
        {
            int freeSpace = this.outputWindow.GetFreeSpace();
            while (freeSpace >= 258)
            {
                switch (this.mode)
                {
                    case 7:
                        int symbol1;
                        while (((symbol1 = this.litlenTree.GetSymbol(this.input)) & -256) == 0)
                        {
                            this.outputWindow.Write(symbol1);
                            if (--freeSpace < 258)
                                return true;
                        }
                        if (symbol1 < 257)
                        {
                            if (symbol1 < 0)
                                return false;
                            this.distTree = (InflaterHuffmanTree)null;
                            this.litlenTree = (InflaterHuffmanTree)null;
                            this.mode = 2;
                            return true;
                        }
                        try
                        {
                            this.repLength = Inflater.CPLENS[symbol1 - 257];
                            this.neededBits = Inflater.CPLEXT[symbol1 - 257];
                            goto case 8;
                        }
                        catch (Exception ex)
                        {
                            throw new SharpZipBaseException("Illegal rep length code");
                        }
                    case 8:
                        if (this.neededBits > 0)
                        {
                            this.mode = 8;
                            int num = this.input.PeekBits(this.neededBits);
                            if (num < 0)
                                return false;
                            this.input.DropBits(this.neededBits);
                            this.repLength += num;
                        }
                        this.mode = 9;
                        goto case 9;
                    case 9:
                        int symbol2 = this.distTree.GetSymbol(this.input);
                        if (symbol2 < 0)
                            return false;
                        try
                        {
                            this.repDist = Inflater.CPDIST[symbol2];
                            this.neededBits = Inflater.CPDEXT[symbol2];
                            goto case 10;
                        }
                        catch (Exception ex)
                        {
                            throw new SharpZipBaseException("Illegal rep dist code");
                        }
                    case 10:
                        if (this.neededBits > 0)
                        {
                            this.mode = 10;
                            int num = this.input.PeekBits(this.neededBits);
                            if (num < 0)
                                return false;
                            this.input.DropBits(this.neededBits);
                            this.repDist += num;
                        }
                        this.outputWindow.Repeat(this.repLength, this.repDist);
                        freeSpace -= this.repLength;
                        this.mode = 7;
                        continue;
                    default:
                        throw new SharpZipBaseException("Inflater unknown mode");
                }
            }
            return true;
        }

        /// <summary>
        /// The DecodeChksum.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool DecodeChksum()
        {
            while (this.neededBits > 0)
            {
                int num = this.input.PeekBits(8);
                if (num < 0)
                    return false;
                this.input.DropBits(8);
                this.readAdler = this.readAdler << 8 | num;
                this.neededBits -= 8;
            }
            if ((int)this.adler.Value != this.readAdler)
                throw new SharpZipBaseException("Adler chksum doesn't match: " + (object)(int)this.adler.Value + " vs. " + (object)this.readAdler);
            this.mode = 12;
            return false;
        }

        /// <summary>
        /// The Decode.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool Decode()
        {
            switch (this.mode)
            {
                case 0:
                    return this.DecodeHeader();
                case 1:
                    return this.DecodeDict();
                case 2:
                    if (this.isLastBlock)
                    {
                        if (this.noHeader)
                        {
                            this.mode = 12;
                            return false;
                        }
                        this.input.SkipToByteBoundary();
                        this.neededBits = 32;
                        this.mode = 11;
                        return true;
                    }
                    int num1 = this.input.PeekBits(3);
                    if (num1 < 0)
                        return false;
                    this.input.DropBits(3);
                    if ((num1 & 1) != 0)
                        this.isLastBlock = true;
                    switch (num1 >> 1)
                    {
                        case 0:
                            this.input.SkipToByteBoundary();
                            this.mode = 3;
                            break;
                        case 1:
                            this.litlenTree = InflaterHuffmanTree.defLitLenTree;
                            this.distTree = InflaterHuffmanTree.defDistTree;
                            this.mode = 7;
                            break;
                        case 2:
                            this.dynHeader = new InflaterDynHeader();
                            this.mode = 6;
                            break;
                        default:
                            throw new SharpZipBaseException("Unknown block type " + (object)num1);
                    }
                    return true;
                case 3:
                    if ((this.uncomprLen = this.input.PeekBits(16)) < 0)
                        return false;
                    this.input.DropBits(16);
                    this.mode = 4;
                    goto case 4;
                case 4:
                    int num2 = this.input.PeekBits(16);
                    if (num2 < 0)
                        return false;
                    this.input.DropBits(16);
                    if (num2 != (this.uncomprLen ^ (int)ushort.MaxValue))
                        throw new SharpZipBaseException("broken uncompressed block");
                    this.mode = 5;
                    goto case 5;
                case 5:
                    this.uncomprLen -= this.outputWindow.CopyStored(this.input, this.uncomprLen);
                    if (this.uncomprLen != 0)
                        return !this.input.IsNeedingInput;
                    this.mode = 2;
                    return true;
                case 6:
                    if (!this.dynHeader.Decode(this.input))
                        return false;
                    this.litlenTree = this.dynHeader.BuildLitLenTree();
                    this.distTree = this.dynHeader.BuildDistTree();
                    this.mode = 7;
                    goto case 7;
                case 7:
                case 8:
                case 9:
                case 10:
                    return this.DecodeHuffman();
                case 11:
                    return this.DecodeChksum();
                case 12:
                    return false;
                default:
                    throw new SharpZipBaseException("Inflater.Decode unknown mode");
            }
        }

        /// <summary>
        /// The SetDictionary.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        public void SetDictionary(byte[] buffer)
        {
            this.SetDictionary(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// The SetDictionary.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void SetDictionary(byte[] buffer, int index, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (!this.IsNeedingDictionary)
                throw new InvalidOperationException("Dictionary is not needed");
            this.adler.Update(buffer, index, count);
            if ((int)this.adler.Value != this.readAdler)
                throw new SharpZipBaseException("Wrong adler checksum");
            this.adler.Reset();
            this.outputWindow.CopyDict(buffer, index, count);
            this.mode = 2;
        }

        /// <summary>
        /// The SetInput.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        public void SetInput(byte[] buffer)
        {
            this.SetInput(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// The SetInput.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void SetInput(byte[] buffer, int index, int count)
        {
            this.input.SetInput(buffer, index, count);
            this.totalIn += (long)count;
        }

        /// <summary>
        /// The Inflate.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Inflate(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            return this.Inflate(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// The Inflate.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Inflate(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "count cannot be negative");
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "offset cannot be negative");
            if (offset + count > buffer.Length)
                throw new ArgumentException("count exceeds buffer bounds");
            if (count == 0)
            {
                if (!this.IsFinished)
                    this.Decode();
                return 0;
            }
            int num = 0;
            do
            {
                if (this.mode != 11)
                {
                    int count1 = this.outputWindow.CopyOutput(buffer, offset, count);
                    if (count1 > 0)
                    {
                        this.adler.Update(buffer, offset, count1);
                        offset += count1;
                        num += count1;
                        this.totalOut += (long)count1;
                        count -= count1;
                        if (count == 0)
                            return num;
                    }
                }
            }
            while (this.Decode() || this.outputWindow.GetAvailable() > 0 && this.mode != 11);
            return num;
        }

        /// <summary>
        /// Gets a value indicating whether IsNeedingInput.
        /// </summary>
        public bool IsNeedingInput
        {
            get
            {
                return this.input.IsNeedingInput;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsNeedingDictionary.
        /// </summary>
        public bool IsNeedingDictionary
        {
            get
            {
                return this.mode == 1 && this.neededBits == 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsFinished.
        /// </summary>
        public bool IsFinished
        {
            get
            {
                return this.mode == 12 && this.outputWindow.GetAvailable() == 0;
            }
        }

        /// <summary>
        /// Gets the Adler.
        /// </summary>
        public int Adler
        {
            get
            {
                return this.IsNeedingDictionary ? this.readAdler : (int)this.adler.Value;
            }
        }

        /// <summary>
        /// Gets the TotalOut.
        /// </summary>
        public long TotalOut
        {
            get
            {
                return this.totalOut;
            }
        }

        /// <summary>
        /// Gets the TotalIn.
        /// </summary>
        public long TotalIn
        {
            get
            {
                return this.totalIn - (long)this.RemainingInput;
            }
        }

        /// <summary>
        /// Gets the RemainingInput.
        /// </summary>
        public int RemainingInput
        {
            get
            {
                return this.input.AvailableBytes;
            }
        }
    }
}
