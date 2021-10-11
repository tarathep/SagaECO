namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
    using System;

    /// <summary>
    /// Defines the <see cref="OutputWindow" />.
    /// </summary>
    public class OutputWindow
    {
        /// <summary>
        /// Defines the window.
        /// </summary>
        private byte[] window = new byte[32768];

        /// <summary>
        /// Defines the WindowSize.
        /// </summary>
        private const int WindowSize = 32768;

        /// <summary>
        /// Defines the WindowMask.
        /// </summary>
        private const int WindowMask = 32767;

        /// <summary>
        /// Defines the windowEnd.
        /// </summary>
        private int windowEnd;

        /// <summary>
        /// Defines the windowFilled.
        /// </summary>
        private int windowFilled;

        /// <summary>
        /// The Write.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        public void Write(int value)
        {
            if (this.windowFilled++ == 32768)
                throw new InvalidOperationException("Window full");
            this.window[this.windowEnd++] = (byte)value;
            this.windowEnd &= (int)short.MaxValue;
        }

        /// <summary>
        /// The SlowRepeat.
        /// </summary>
        /// <param name="repStart">The repStart<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <param name="distance">The distance<see cref="int"/>.</param>
        private void SlowRepeat(int repStart, int length, int distance)
        {
            while (length-- > 0)
            {
                this.window[this.windowEnd++] = this.window[repStart++];
                this.windowEnd &= (int)short.MaxValue;
                repStart &= (int)short.MaxValue;
            }
        }

        /// <summary>
        /// The Repeat.
        /// </summary>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <param name="distance">The distance<see cref="int"/>.</param>
        public void Repeat(int length, int distance)
        {
            if ((this.windowFilled += length) > 32768)
                throw new InvalidOperationException("Window full");
            int num1 = this.windowEnd - distance & (int)short.MaxValue;
            int num2 = 32768 - length;
            if (num1 <= num2 && this.windowEnd < num2)
            {
                if (length <= distance)
                {
                    Array.Copy((Array)this.window, num1, (Array)this.window, this.windowEnd, length);
                    this.windowEnd += length;
                }
                else
                {
                    while (length-- > 0)
                        this.window[this.windowEnd++] = this.window[num1++];
                }
            }
            else
                this.SlowRepeat(num1, length, distance);
        }

        /// <summary>
        /// The CopyStored.
        /// </summary>
        /// <param name="input">The input<see cref="StreamManipulator"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CopyStored(StreamManipulator input, int length)
        {
            length = Math.Min(Math.Min(length, 32768 - this.windowFilled), input.AvailableBytes);
            int length1 = 32768 - this.windowEnd;
            int num;
            if (length > length1)
            {
                num = input.CopyBytes(this.window, this.windowEnd, length1);
                if (num == length1)
                    num += input.CopyBytes(this.window, 0, length - length1);
            }
            else
                num = input.CopyBytes(this.window, this.windowEnd, length);
            this.windowEnd = this.windowEnd + num & (int)short.MaxValue;
            this.windowFilled += num;
            return num;
        }

        /// <summary>
        /// The CopyDict.
        /// </summary>
        /// <param name="dictionary">The dictionary<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        public void CopyDict(byte[] dictionary, int offset, int length)
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary));
            if (this.windowFilled > 0)
                throw new InvalidOperationException();
            if (length > 32768)
            {
                offset += length - 32768;
                length = 32768;
            }
            Array.Copy((Array)dictionary, offset, (Array)this.window, 0, length);
            this.windowEnd = length & (int)short.MaxValue;
        }

        /// <summary>
        /// The GetFreeSpace.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetFreeSpace()
        {
            return 32768 - this.windowFilled;
        }

        /// <summary>
        /// The GetAvailable.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetAvailable()
        {
            return this.windowFilled;
        }

        /// <summary>
        /// The CopyOutput.
        /// </summary>
        /// <param name="output">The output<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="len">The len<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int CopyOutput(byte[] output, int offset, int len)
        {
            int num1 = this.windowEnd;
            if (len > this.windowFilled)
                len = this.windowFilled;
            else
                num1 = this.windowEnd - this.windowFilled + len & (int)short.MaxValue;
            int num2 = len;
            int length = len - num1;
            if (length > 0)
            {
                Array.Copy((Array)this.window, 32768 - length, (Array)output, offset, length);
                offset += length;
                len = num1;
            }
            Array.Copy((Array)this.window, num1 - len, (Array)output, offset, len);
            this.windowFilled -= num2;
            if (this.windowFilled < 0)
                throw new InvalidOperationException();
            return num2;
        }

        /// <summary>
        /// The Reset.
        /// </summary>
        public void Reset()
        {
            this.windowFilled = this.windowEnd = 0;
        }
    }
}
