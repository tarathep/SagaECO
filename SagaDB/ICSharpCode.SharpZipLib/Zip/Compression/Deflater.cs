namespace ICSharpCode.SharpZipLib.Zip.Compression
{
    using System;

    /// <summary>
    /// Defines the <see cref="Deflater" />.
    /// </summary>
    public class Deflater
    {
        /// <summary>
        /// Defines the BEST_COMPRESSION.
        /// </summary>
        public const int BEST_COMPRESSION = 9;

        /// <summary>
        /// Defines the BEST_SPEED.
        /// </summary>
        public const int BEST_SPEED = 1;

        /// <summary>
        /// Defines the DEFAULT_COMPRESSION.
        /// </summary>
        public const int DEFAULT_COMPRESSION = -1;

        /// <summary>
        /// Defines the NO_COMPRESSION.
        /// </summary>
        public const int NO_COMPRESSION = 0;

        /// <summary>
        /// Defines the DEFLATED.
        /// </summary>
        public const int DEFLATED = 8;

        /// <summary>
        /// Defines the IS_SETDICT.
        /// </summary>
        private const int IS_SETDICT = 1;

        /// <summary>
        /// Defines the IS_FLUSHING.
        /// </summary>
        private const int IS_FLUSHING = 4;

        /// <summary>
        /// Defines the IS_FINISHING.
        /// </summary>
        private const int IS_FINISHING = 8;

        /// <summary>
        /// Defines the INIT_STATE.
        /// </summary>
        private const int INIT_STATE = 0;

        /// <summary>
        /// Defines the SETDICT_STATE.
        /// </summary>
        private const int SETDICT_STATE = 1;

        /// <summary>
        /// Defines the BUSY_STATE.
        /// </summary>
        private const int BUSY_STATE = 16;

        /// <summary>
        /// Defines the FLUSHING_STATE.
        /// </summary>
        private const int FLUSHING_STATE = 20;

        /// <summary>
        /// Defines the FINISHING_STATE.
        /// </summary>
        private const int FINISHING_STATE = 28;

        /// <summary>
        /// Defines the FINISHED_STATE.
        /// </summary>
        private const int FINISHED_STATE = 30;

        /// <summary>
        /// Defines the CLOSED_STATE.
        /// </summary>
        private const int CLOSED_STATE = 127;

        /// <summary>
        /// Defines the level.
        /// </summary>
        private int level;

        /// <summary>
        /// Defines the noZlibHeaderOrFooter.
        /// </summary>
        private bool noZlibHeaderOrFooter;

        /// <summary>
        /// Defines the state.
        /// </summary>
        private int state;

        /// <summary>
        /// Defines the totalOut.
        /// </summary>
        private long totalOut;

        /// <summary>
        /// Defines the pending.
        /// </summary>
        private DeflaterPending pending;

        /// <summary>
        /// Defines the engine.
        /// </summary>
        private DeflaterEngine engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="Deflater"/> class.
        /// </summary>
        public Deflater()
      : this(-1, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Deflater"/> class.
        /// </summary>
        /// <param name="level">The level<see cref="int"/>.</param>
        public Deflater(int level)
      : this(level, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Deflater"/> class.
        /// </summary>
        /// <param name="level">The level<see cref="int"/>.</param>
        /// <param name="noZlibHeaderOrFooter">The noZlibHeaderOrFooter<see cref="bool"/>.</param>
        public Deflater(int level, bool noZlibHeaderOrFooter)
        {
            if (level == -1)
                level = 6;
            else if (level < 0 || level > 9)
                throw new ArgumentOutOfRangeException(nameof(level));
            this.pending = new DeflaterPending();
            this.engine = new DeflaterEngine(this.pending);
            this.noZlibHeaderOrFooter = noZlibHeaderOrFooter;
            this.SetStrategy(DeflateStrategy.Default);
            this.SetLevel(level);
            this.Reset();
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        public void Reset()
        {
            this.state = this.noZlibHeaderOrFooter ? 16 : 0;
            this.totalOut = 0L;
            this.pending.Reset();
            this.engine.Reset();
        }

        /// <summary>
        /// Gets the Adler.
        /// </summary>
        public int Adler
        {
            get
            {
                return this.engine.Adler;
            }
        }

        /// <summary>
        /// Gets the TotalIn.
        /// </summary>
        public long TotalIn
        {
            get
            {
                return this.engine.TotalIn;
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
        /// The Flush.
        /// </summary>
        public void Flush()
        {
            this.state |= 4;
        }

        /// <summary>
        /// The Finish.
        /// </summary>
        public void Finish()
        {
            this.state |= 12;
        }

        /// <summary>
        /// Gets a value indicating whether IsFinished.
        /// </summary>
        public bool IsFinished
        {
            get
            {
                return this.state == 30 && this.pending.IsFlushed;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsNeedingInput.
        /// </summary>
        public bool IsNeedingInput
        {
            get
            {
                return this.engine.NeedsInput();
            }
        }

        /// <summary>
        /// The SetInput.
        /// </summary>
        /// <param name="input">The input<see cref="byte[]"/>.</param>
        public void SetInput(byte[] input)
        {
            this.SetInput(input, 0, input.Length);
        }

        /// <summary>
        /// The SetInput.
        /// </summary>
        /// <param name="input">The input<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void SetInput(byte[] input, int offset, int count)
        {
            if ((this.state & 8) != 0)
                throw new InvalidOperationException("Finish() already called");
            this.engine.SetInput(input, offset, count);
        }

        /// <summary>
        /// The SetLevel.
        /// </summary>
        /// <param name="level">The level<see cref="int"/>.</param>
        public void SetLevel(int level)
        {
            if (level == -1)
                level = 6;
            else if (level < 0 || level > 9)
                throw new ArgumentOutOfRangeException(nameof(level));
            if (this.level == level)
                return;
            this.level = level;
            this.engine.SetLevel(level);
        }

        /// <summary>
        /// The GetLevel.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetLevel()
        {
            return this.level;
        }

        /// <summary>
        /// The SetStrategy.
        /// </summary>
        /// <param name="strategy">The strategy<see cref="DeflateStrategy"/>.</param>
        public void SetStrategy(DeflateStrategy strategy)
        {
            this.engine.Strategy = strategy;
        }

        /// <summary>
        /// The Deflate.
        /// </summary>
        /// <param name="output">The output<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Deflate(byte[] output)
        {
            return this.Deflate(output, 0, output.Length);
        }

        /// <summary>
        /// The Deflate.
        /// </summary>
        /// <param name="output">The output<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int Deflate(byte[] output, int offset, int length)
        {
            int num1 = length;
            if (this.state == (int)sbyte.MaxValue)
                throw new InvalidOperationException("Deflater closed");
            if (this.state < 16)
            {
                int num2 = 30720;
                int num3 = this.level - 1 >> 1;
                if (num3 < 0 || num3 > 3)
                    num3 = 3;
                int num4 = num2 | num3 << 6;
                if ((this.state & 1) != 0)
                    num4 |= 32;
                this.pending.WriteShortMSB(num4 + (31 - num4 % 31));
                if ((this.state & 1) != 0)
                {
                    int adler = this.engine.Adler;
                    this.engine.ResetAdler();
                    this.pending.WriteShortMSB(adler >> 16);
                    this.pending.WriteShortMSB(adler & (int)ushort.MaxValue);
                }
                this.state = 16 | this.state & 12;
            }
            while (true)
            {
                int num2 = this.pending.Flush(output, offset, length);
                offset += num2;
                this.totalOut += (long)num2;
                length -= num2;
                if (length != 0 && this.state != 30)
                {
                    if (!this.engine.Deflate((this.state & 4) != 0, (this.state & 8) != 0))
                    {
                        if (this.state != 16)
                        {
                            if (this.state == 20)
                            {
                                if (this.level != 0)
                                {
                                    int num3 = 8 + (-this.pending.BitCount & 7);
                                    while (num3 > 0)
                                    {
                                        this.pending.WriteBits(2, 10);
                                        num3 -= 10;
                                    }
                                }
                                this.state = 16;
                            }
                            else if (this.state == 28)
                            {
                                this.pending.AlignToByte();
                                if (!this.noZlibHeaderOrFooter)
                                {
                                    int adler = this.engine.Adler;
                                    this.pending.WriteShortMSB(adler >> 16);
                                    this.pending.WriteShortMSB(adler & (int)ushort.MaxValue);
                                }
                                this.state = 30;
                            }
                        }
                        else
                            break;
                    }
                }
                else
                    goto label_26;
            }
            return num1 - length;
        label_26:
            return num1 - length;
        }

        /// <summary>
        /// The SetDictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary<see cref="byte[]"/>.</param>
        public void SetDictionary(byte[] dictionary)
        {
            this.SetDictionary(dictionary, 0, dictionary.Length);
        }

        /// <summary>
        /// The SetDictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary<see cref="byte[]"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void SetDictionary(byte[] dictionary, int index, int count)
        {
            if (this.state != 0)
                throw new InvalidOperationException();
            this.state = 1;
            this.engine.SetDictionary(dictionary, index, count);
        }
    }
}
