namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
    using System;

    /// <summary>
    /// Defines the <see cref="InflaterDynHeader" />.
    /// </summary>
    internal class InflaterDynHeader
    {
        /// <summary>
        /// Defines the repMin.
        /// </summary>
        private static readonly int[] repMin = new int[3]
    {
      3,
      3,
      11
    };

        /// <summary>
        /// Defines the repBits.
        /// </summary>
        private static readonly int[] repBits = new int[3]
    {
      2,
      3,
      7
    };

        /// <summary>
        /// Defines the BL_ORDER.
        /// </summary>
        private static readonly int[] BL_ORDER = new int[19]
    {
      16,
      17,
      18,
      0,
      8,
      7,
      9,
      6,
      10,
      5,
      11,
      4,
      12,
      3,
      13,
      2,
      14,
      1,
      15
    };

        /// <summary>
        /// Defines the LNUM.
        /// </summary>
        private const int LNUM = 0;

        /// <summary>
        /// Defines the DNUM.
        /// </summary>
        private const int DNUM = 1;

        /// <summary>
        /// Defines the BLNUM.
        /// </summary>
        private const int BLNUM = 2;

        /// <summary>
        /// Defines the BLLENS.
        /// </summary>
        private const int BLLENS = 3;

        /// <summary>
        /// Defines the LENS.
        /// </summary>
        private const int LENS = 4;

        /// <summary>
        /// Defines the REPS.
        /// </summary>
        private const int REPS = 5;

        /// <summary>
        /// Defines the blLens.
        /// </summary>
        private byte[] blLens;

        /// <summary>
        /// Defines the litdistLens.
        /// </summary>
        private byte[] litdistLens;

        /// <summary>
        /// Defines the blTree.
        /// </summary>
        private InflaterHuffmanTree blTree;

        /// <summary>
        /// Defines the mode.
        /// </summary>
        private int mode;

        /// <summary>
        /// Defines the lnum.
        /// </summary>
        private int lnum;

        /// <summary>
        /// Defines the dnum.
        /// </summary>
        private int dnum;

        /// <summary>
        /// Defines the blnum.
        /// </summary>
        private int blnum;

        /// <summary>
        /// Defines the num.
        /// </summary>
        private int num;

        /// <summary>
        /// Defines the repSymbol.
        /// </summary>
        private int repSymbol;

        /// <summary>
        /// Defines the lastLen.
        /// </summary>
        private byte lastLen;

        /// <summary>
        /// Defines the ptr.
        /// </summary>
        private int ptr;

        /// <summary>
        /// The Decode.
        /// </summary>
        /// <param name="input">The input<see cref="StreamManipulator"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Decode(StreamManipulator input)
        {
            while (true)
            {
                switch (this.mode)
                {
                    case 0:
                        this.lnum = input.PeekBits(5);
                        if (this.lnum >= 0)
                        {
                            this.lnum += 257;
                            input.DropBits(5);
                            this.mode = 1;
                            goto case 1;
                        }
                        else
                            goto label_2;
                    case 1:
                        this.dnum = input.PeekBits(5);
                        if (this.dnum >= 0)
                        {
                            ++this.dnum;
                            input.DropBits(5);
                            this.num = this.lnum + this.dnum;
                            this.litdistLens = new byte[this.num];
                            this.mode = 2;
                            goto case 2;
                        }
                        else
                            goto label_5;
                    case 2:
                        this.blnum = input.PeekBits(4);
                        if (this.blnum >= 0)
                        {
                            this.blnum += 4;
                            input.DropBits(4);
                            this.blLens = new byte[19];
                            this.ptr = 0;
                            this.mode = 3;
                            goto case 3;
                        }
                        else
                            goto label_8;
                    case 3:
                        for (; this.ptr < this.blnum; ++this.ptr)
                        {
                            int num = input.PeekBits(3);
                            if (num < 0)
                                return false;
                            input.DropBits(3);
                            this.blLens[InflaterDynHeader.BL_ORDER[this.ptr]] = (byte)num;
                        }
                        this.blTree = new InflaterHuffmanTree(this.blLens);
                        this.blLens = (byte[])null;
                        this.ptr = 0;
                        this.mode = 4;
                        goto case 4;
                    case 4:
                        int symbol;
                        while (((symbol = this.blTree.GetSymbol(input)) & -16) == 0)
                        {
                            this.litdistLens[this.ptr++] = this.lastLen = (byte)symbol;
                            if (this.ptr == this.num)
                                return true;
                        }
                        if (symbol >= 0)
                        {
                            if (symbol >= 17)
                                this.lastLen = (byte)0;
                            else if (this.ptr == 0)
                                goto label_23;
                            this.repSymbol = symbol - 16;
                            this.mode = 5;
                            goto case 5;
                        }
                        else
                            goto label_19;
                    case 5:
                        int repBit = InflaterDynHeader.repBits[this.repSymbol];
                        int num1 = input.PeekBits(repBit);
                        if (num1 >= 0)
                        {
                            input.DropBits(repBit);
                            int num2 = num1 + InflaterDynHeader.repMin[this.repSymbol];
                            if (this.ptr + num2 <= this.num)
                            {
                                while (num2-- > 0)
                                    this.litdistLens[this.ptr++] = this.lastLen;
                                if (this.ptr != this.num)
                                {
                                    this.mode = 4;
                                    continue;
                                }
                                goto label_32;
                            }
                            else
                                goto label_28;
                        }
                        else
                            goto label_26;
                    default:
                        continue;
                }
            }
        label_2:
            return false;
        label_5:
            return false;
        label_8:
            return false;
        label_19:
            return false;
        label_23:
            throw new SharpZipBaseException();
        label_26:
            return false;
        label_28:
            throw new SharpZipBaseException();
        label_32:
            return true;
        }

        /// <summary>
        /// The BuildLitLenTree.
        /// </summary>
        /// <returns>The <see cref="InflaterHuffmanTree"/>.</returns>
        public InflaterHuffmanTree BuildLitLenTree()
        {
            byte[] codeLengths = new byte[this.lnum];
            Array.Copy((Array)this.litdistLens, 0, (Array)codeLengths, 0, this.lnum);
            return new InflaterHuffmanTree(codeLengths);
        }

        /// <summary>
        /// The BuildDistTree.
        /// </summary>
        /// <returns>The <see cref="InflaterHuffmanTree"/>.</returns>
        public InflaterHuffmanTree BuildDistTree()
        {
            byte[] codeLengths = new byte[this.dnum];
            Array.Copy((Array)this.litdistLens, this.lnum, (Array)codeLengths, 0, this.dnum);
            return new InflaterHuffmanTree(codeLengths);
        }
    }
}
