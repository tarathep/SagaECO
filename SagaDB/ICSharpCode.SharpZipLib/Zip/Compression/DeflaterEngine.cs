namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using ICSharpCode.SharpZipLib.Checksums;
    using System;

    /// <summary>
    /// Defines the <see cref="DeflaterEngine" />.
    /// </summary>
    public class DeflaterEngine : DeflaterConstants
    {
        /// <summary>
        /// Defines the TooFar.
        /// </summary>
        private const int TooFar = 4096;

        /// <summary>
        /// Defines the ins_h.
        /// </summary>
        private int ins_h;

        /// <summary>
        /// Defines the head.
        /// </summary>
        private short[] head;

        /// <summary>
        /// Defines the prev.
        /// </summary>
        private short[] prev;

        /// <summary>
        /// Defines the matchStart.
        /// </summary>
        private int matchStart;

        /// <summary>
        /// Defines the matchLen.
        /// </summary>
        private int matchLen;

        /// <summary>
        /// Defines the prevAvailable.
        /// </summary>
        private bool prevAvailable;

        /// <summary>
        /// Defines the blockStart.
        /// </summary>
        private int blockStart;

        /// <summary>
        /// Defines the strstart.
        /// </summary>
        private int strstart;

        /// <summary>
        /// Defines the lookahead.
        /// </summary>
        private int lookahead;

        /// <summary>
        /// Defines the window.
        /// </summary>
        private byte[] window;

        /// <summary>
        /// Defines the strategy.
        /// </summary>
        private DeflateStrategy strategy;

        /// <summary>
        /// Defines the max_chain.
        /// </summary>
        private int max_chain;

        /// <summary>
        /// Defines the max_lazy.
        /// </summary>
        private int max_lazy;

        /// <summary>
        /// Defines the niceLength.
        /// </summary>
        private int niceLength;

        /// <summary>
        /// Defines the goodLength.
        /// </summary>
        private int goodLength;

        /// <summary>
        /// Defines the compressionFunction.
        /// </summary>
        private int compressionFunction;

        /// <summary>
        /// Defines the inputBuf.
        /// </summary>
        private byte[] inputBuf;

        /// <summary>
        /// Defines the totalIn.
        /// </summary>
        private long totalIn;

        /// <summary>
        /// Defines the inputOff.
        /// </summary>
        private int inputOff;

        /// <summary>
        /// Defines the inputEnd.
        /// </summary>
        private int inputEnd;

        /// <summary>
        /// Defines the pending.
        /// </summary>
        private DeflaterPending pending;

        /// <summary>
        /// Defines the huffman.
        /// </summary>
        private DeflaterHuffman huffman;

        /// <summary>
        /// Defines the adler.
        /// </summary>
        private Adler32 adler;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeflaterEngine"/> class.
        /// </summary>
        /// <param name="pending">The pending<see cref="DeflaterPending"/>.</param>
        public DeflaterEngine(DeflaterPending pending)
        {
            this.pending = pending;
            this.huffman = new DeflaterHuffman(pending);
            this.adler = new Adler32();
            this.window = new byte[65536];
            this.head = new short[32768];
            this.prev = new short[32768];
            this.blockStart = this.strstart = 1;
        }

        /// <summary>
        /// The Deflate.
        /// </summary>
        /// <param name="flush">The flush<see cref="bool"/>.</param>
        /// <param name="finish">The finish<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Deflate(bool flush, bool finish)
        {
            bool flag;
            do
            {
                this.FillWindow();
                bool flush1 = flush && this.inputOff == this.inputEnd;
                switch (this.compressionFunction)
                {
                    case 0:
                        flag = this.DeflateStored(flush1, finish);
                        break;
                    case 1:
                        flag = this.DeflateFast(flush1, finish);
                        break;
                    case 2:
                        flag = this.DeflateSlow(flush1, finish);
                        break;
                    default:
                        throw new InvalidOperationException("unknown compressionFunction");
                }
            }
            while (this.pending.IsFlushed && flag);
            return flag;
        }

        /// <summary>
        /// The SetInput.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void SetInput(byte[] buffer, int offset, int count)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            if (this.inputOff < this.inputEnd)
                throw new InvalidOperationException("Old input was not completely processed");
            int num = offset + count;
            if (offset > num || num > buffer.Length)
                throw new ArgumentOutOfRangeException(nameof(count));
            this.inputBuf = buffer;
            this.inputOff = offset;
            this.inputEnd = num;
        }

        /// <summary>
        /// The NeedsInput.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool NeedsInput()
        {
            return this.inputEnd == this.inputOff;
        }

        /// <summary>
        /// The SetDictionary.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        public void SetDictionary(byte[] buffer, int offset, int length)
        {
            this.adler.Update(buffer, offset, length);
            if (length < 3)
                return;
            if (length > 32506)
            {
                offset += length - 32506;
                length = 32506;
            }
            Array.Copy((Array)buffer, offset, (Array)this.window, this.strstart, length);
            this.UpdateHash();
            --length;
            while (--length > 0)
            {
                this.InsertString();
                ++this.strstart;
            }
            this.strstart += 2;
            this.blockStart = this.strstart;
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        public void Reset()
        {
            this.huffman.Reset();
            this.adler.Reset();
            this.blockStart = this.strstart = 1;
            this.lookahead = 0;
            this.totalIn = 0L;
            this.prevAvailable = false;
            this.matchLen = 2;
            for (int index = 0; index < 32768; ++index)
                this.head[index] = (short)0;
            for (int index = 0; index < 32768; ++index)
                this.prev[index] = (short)0;
        }

        /// <summary>
        /// The ResetAdler.
        /// </summary>
        public void ResetAdler()
        {
            this.adler.Reset();
        }

        /// <summary>
        /// Gets the Adler.
        /// </summary>
        public int Adler
        {
            get
            {
                return (int)this.adler.Value;
            }
        }

        /// <summary>
        /// Gets the TotalIn.
        /// </summary>
        public long TotalIn
        {
            get
            {
                return this.totalIn;
            }
        }

        /// <summary>
        /// Gets or sets the Strategy.
        /// </summary>
        public DeflateStrategy Strategy
        {
            get
            {
                return this.strategy;
            }
            set
            {
                this.strategy = value;
            }
        }

        /// <summary>
        /// The SetLevel.
        /// </summary>
        /// <param name="level">The level<see cref="int"/>.</param>
        public void SetLevel(int level)
        {
            if (level < 0 || level > 9)
                throw new ArgumentOutOfRangeException(nameof(level));
            this.goodLength = DeflaterConstants.GOOD_LENGTH[level];
            this.max_lazy = DeflaterConstants.MAX_LAZY[level];
            this.niceLength = DeflaterConstants.NICE_LENGTH[level];
            this.max_chain = DeflaterConstants.MAX_CHAIN[level];
            if (DeflaterConstants.COMPR_FUNC[level] == this.compressionFunction)
                return;
            switch (this.compressionFunction)
            {
                case 0:
                    if (this.strstart > this.blockStart)
                    {
                        this.huffman.FlushStoredBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
                        this.blockStart = this.strstart;
                    }
                    this.UpdateHash();
                    break;
                case 1:
                    if (this.strstart > this.blockStart)
                    {
                        this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
                        this.blockStart = this.strstart;
                        break;
                    }
                    break;
                case 2:
                    if (this.prevAvailable)
                        this.huffman.TallyLit((int)this.window[this.strstart - 1] & (int)byte.MaxValue);
                    if (this.strstart > this.blockStart)
                    {
                        this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, false);
                        this.blockStart = this.strstart;
                    }
                    this.prevAvailable = false;
                    this.matchLen = 2;
                    break;
            }
            this.compressionFunction = DeflaterConstants.COMPR_FUNC[level];
        }

        /// <summary>
        /// The FillWindow.
        /// </summary>
        public void FillWindow()
        {
            if (this.strstart >= 65274)
                this.SlideWindow();
            while (this.lookahead < 262 && this.inputOff < this.inputEnd)
            {
                int num = 65536 - this.lookahead - this.strstart;
                if (num > this.inputEnd - this.inputOff)
                    num = this.inputEnd - this.inputOff;
                Array.Copy((Array)this.inputBuf, this.inputOff, (Array)this.window, this.strstart + this.lookahead, num);
                this.adler.Update(this.inputBuf, this.inputOff, num);
                this.inputOff += num;
                this.totalIn += (long)num;
                this.lookahead += num;
            }
            if (this.lookahead < 3)
                return;
            this.UpdateHash();
        }

        /// <summary>
        /// The UpdateHash.
        /// </summary>
        private void UpdateHash()
        {
            this.ins_h = (int)this.window[this.strstart] << 5 ^ (int)this.window[this.strstart + 1];
        }

        /// <summary>
        /// The InsertString.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        private int InsertString()
        {
            int index = (this.ins_h << 5 ^ (int)this.window[this.strstart + 2]) & (int)short.MaxValue;
            short num;
            this.prev[this.strstart & (int)short.MaxValue] = num = this.head[index];
            this.head[index] = (short)this.strstart;
            this.ins_h = index;
            return (int)num & (int)ushort.MaxValue;
        }

        /// <summary>
        /// The SlideWindow.
        /// </summary>
        private void SlideWindow()
        {
            Array.Copy((Array)this.window, 32768, (Array)this.window, 0, 32768);
            this.matchStart -= 32768;
            this.strstart -= 32768;
            this.blockStart -= 32768;
            for (int index = 0; index < 32768; ++index)
            {
                int num = (int)this.head[index] & (int)ushort.MaxValue;
                this.head[index] = num >= 32768 ? (short)(num - 32768) : (short)0;
            }
            for (int index = 0; index < 32768; ++index)
            {
                int num = (int)this.prev[index] & (int)ushort.MaxValue;
                this.prev[index] = num >= 32768 ? (short)(num - 32768) : (short)0;
            }
        }

        /// <summary>
        /// The FindLongestMatch.
        /// </summary>
        /// <param name="curMatch">The curMatch<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool FindLongestMatch(int curMatch)
        {
            int maxChain = this.max_chain;
            int num1 = this.niceLength;
            short[] prev = this.prev;
            int strstart = this.strstart;
            int index = this.strstart + this.matchLen;
            int val1 = Math.Max(this.matchLen, 2);
            int num2 = Math.Max(this.strstart - 32506, 0);
            int num3 = this.strstart + 258 - 1;
            byte num4 = this.window[index - 1];
            byte num5 = this.window[index];
            if (val1 >= this.goodLength)
                maxChain >>= 2;
            if (num1 > this.lookahead)
                num1 = this.lookahead;
            do
            {
                if ((int)this.window[curMatch + val1] == (int)num5 && (int)this.window[curMatch + val1 - 1] == (int)num4 && (int)this.window[curMatch] == (int)this.window[strstart] && (int)this.window[curMatch + 1] == (int)this.window[strstart + 1])
                {
                    int num6 = curMatch + 2;
                    int num7 = strstart + 2;
                    do
                        ;
                    while ((int)this.window[++num7] == (int)this.window[++num6] && (int)this.window[++num7] == (int)this.window[++num6] && ((int)this.window[++num7] == (int)this.window[++num6] && (int)this.window[++num7] == (int)this.window[++num6]) && ((int)this.window[++num7] == (int)this.window[++num6] && (int)this.window[++num7] == (int)this.window[++num6] && ((int)this.window[++num7] == (int)this.window[++num6] && (int)this.window[++num7] == (int)this.window[++num6])) && num7 < num3);
                    if (num7 > index)
                    {
                        this.matchStart = curMatch;
                        index = num7;
                        val1 = num7 - this.strstart;
                        if (val1 < num1)
                        {
                            num4 = this.window[index - 1];
                            num5 = this.window[index];
                        }
                        else
                            break;
                    }
                    strstart = this.strstart;
                }
            }
            while ((curMatch = (int)prev[curMatch & (int)short.MaxValue] & (int)ushort.MaxValue) > num2 && --maxChain != 0);
            this.matchLen = Math.Min(val1, this.lookahead);
            return this.matchLen >= 3;
        }

        /// <summary>
        /// The DeflateStored.
        /// </summary>
        /// <param name="flush">The flush<see cref="bool"/>.</param>
        /// <param name="finish">The finish<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool DeflateStored(bool flush, bool finish)
        {
            if (!flush && this.lookahead == 0)
                return false;
            this.strstart += this.lookahead;
            this.lookahead = 0;
            int storedLength = this.strstart - this.blockStart;
            if (storedLength < DeflaterConstants.MAX_BLOCK_SIZE && (this.blockStart >= 32768 || storedLength < 32506) && !flush)
                return true;
            bool lastBlock = finish;
            if (storedLength > DeflaterConstants.MAX_BLOCK_SIZE)
            {
                storedLength = DeflaterConstants.MAX_BLOCK_SIZE;
                lastBlock = false;
            }
            this.huffman.FlushStoredBlock(this.window, this.blockStart, storedLength, lastBlock);
            this.blockStart += storedLength;
            return !lastBlock;
        }

        /// <summary>
        /// The DeflateFast.
        /// </summary>
        /// <param name="flush">The flush<see cref="bool"/>.</param>
        /// <param name="finish">The finish<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool DeflateFast(bool flush, bool finish)
        {
            if (this.lookahead < 262 && !flush)
                return false;
            while (this.lookahead >= 262 || flush)
            {
                if (this.lookahead == 0)
                {
                    this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
                    this.blockStart = this.strstart;
                    return false;
                }
                if (this.strstart > 65274)
                    this.SlideWindow();
                int curMatch;
                if (this.lookahead >= 3 && (curMatch = this.InsertString()) != 0 && (this.strategy != DeflateStrategy.HuffmanOnly && this.strstart - curMatch <= 32506) && this.FindLongestMatch(curMatch))
                {
                    bool flag = this.huffman.TallyDist(this.strstart - this.matchStart, this.matchLen);
                    this.lookahead -= this.matchLen;
                    if (this.matchLen <= this.max_lazy && this.lookahead >= 3)
                    {
                        while (--this.matchLen > 0)
                        {
                            ++this.strstart;
                            this.InsertString();
                        }
                        ++this.strstart;
                    }
                    else
                    {
                        this.strstart += this.matchLen;
                        if (this.lookahead >= 2)
                            this.UpdateHash();
                    }
                    this.matchLen = 2;
                    if (!flag)
                        continue;
                }
                else
                {
                    this.huffman.TallyLit((int)this.window[this.strstart] & (int)byte.MaxValue);
                    ++this.strstart;
                    --this.lookahead;
                }
                if (this.huffman.IsFull())
                {
                    bool lastBlock = finish && this.lookahead == 0;
                    this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, lastBlock);
                    this.blockStart = this.strstart;
                    return !lastBlock;
                }
            }
            return true;
        }

        /// <summary>
        /// The DeflateSlow.
        /// </summary>
        /// <param name="flush">The flush<see cref="bool"/>.</param>
        /// <param name="finish">The finish<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool DeflateSlow(bool flush, bool finish)
        {
            if (this.lookahead < 262 && !flush)
                return false;
            while (this.lookahead >= 262 || flush)
            {
                if (this.lookahead == 0)
                {
                    if (this.prevAvailable)
                        this.huffman.TallyLit((int)this.window[this.strstart - 1] & (int)byte.MaxValue);
                    this.prevAvailable = false;
                    this.huffman.FlushBlock(this.window, this.blockStart, this.strstart - this.blockStart, finish);
                    this.blockStart = this.strstart;
                    return false;
                }
                if (this.strstart >= 65274)
                    this.SlideWindow();
                int matchStart = this.matchStart;
                int matchLen = this.matchLen;
                if (this.lookahead >= 3)
                {
                    int curMatch = this.InsertString();
                    if (this.strategy != DeflateStrategy.HuffmanOnly && curMatch != 0 && this.strstart - curMatch <= 32506 && this.FindLongestMatch(curMatch) && (this.matchLen <= 5 && (this.strategy == DeflateStrategy.Filtered || this.matchLen == 3 && this.strstart - this.matchStart > 4096)))
                        this.matchLen = 2;
                }
                if (matchLen >= 3 && this.matchLen <= matchLen)
                {
                    this.huffman.TallyDist(this.strstart - 1 - matchStart, matchLen);
                    int num = matchLen - 2;
                    do
                    {
                        ++this.strstart;
                        --this.lookahead;
                        if (this.lookahead >= 3)
                            this.InsertString();
                    }
                    while (--num > 0);
                    ++this.strstart;
                    --this.lookahead;
                    this.prevAvailable = false;
                    this.matchLen = 2;
                }
                else
                {
                    if (this.prevAvailable)
                        this.huffman.TallyLit((int)this.window[this.strstart - 1] & (int)byte.MaxValue);
                    this.prevAvailable = true;
                    ++this.strstart;
                    --this.lookahead;
                }
                if (this.huffman.IsFull())
                {
                    int storedLength = this.strstart - this.blockStart;
                    if (this.prevAvailable)
                        --storedLength;
                    bool lastBlock = finish && this.lookahead == 0 && !this.prevAvailable;
                    this.huffman.FlushBlock(this.window, this.blockStart, storedLength, lastBlock);
                    this.blockStart += storedLength;
                    return !lastBlock;
                }
            }
            return true;
        }
    }
}
