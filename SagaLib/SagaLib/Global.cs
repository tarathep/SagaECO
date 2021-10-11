namespace SagaLib
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    /// <summary>
    /// The global class contains objects that can be usefull throughout the entire application.
    /// </summary>
    public static class Global
    {
        /// <summary>
        /// Unicode encoder to encode en decode from bytes to string and visa versa..
        /// </summary>
        public static Encoding Unicode = Encoding.UTF8;

        /// <summary>
        /// Defines the Random.
        /// </summary>
        public static Global.RandomF Random = new Global.RandomF();

        /// <summary>
        /// Defines the MAX_SIGHT_RANGE.
        /// </summary>
        public static uint MAX_SIGHT_RANGE = 1500;

        /// <summary>
        /// Defines the clientMananger.
        /// </summary>
        public static ClientManager clientMananger;

        /// <summary>
        /// Make sure the length of a string doesn't exceed a given maximum length.
        /// </summary>
        /// <param name="s">String to process.</param>
        /// <param name="length">Maximum length for the string.</param>
        /// <returns>The string trimmed to a given size.</returns>
        public static string SetStringLength(string s, int length)
        {
            if (s.Length > length)
                s = s.Remove(length, s.Length - length);
            return s;
        }

        /// <summary>
        /// The PosX16to8.
        /// </summary>
        /// <param name="pos">The pos<see cref="short"/>.</param>
        /// <param name="width">The width<see cref="ushort"/>.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        public static byte PosX16to8(short pos, ushort width)
        {
            double num1 = (double)width / 2.0;
            double num2 = (double)((int)pos - 50) / 100.0 + num1;
            if (num2 < 0.0)
                num2 = 0.0;
            return (byte)num2;
        }

        /// <summary>
        /// The PosY16to8.
        /// </summary>
        /// <param name="pos">The pos<see cref="short"/>.</param>
        /// <param name="height">The height<see cref="ushort"/>.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        public static byte PosY16to8(short pos, ushort height)
        {
            double num1 = (double)height / 2.0;
            double num2 = (double)-((int)pos + 50) / 100.0 + num1;
            if (num2 < 0.0)
                num2 = 0.0;
            return (byte)num2;
        }

        /// <summary>
        /// The PosX8to16.
        /// </summary>
        /// <param name="pos">The pos<see cref="byte"/>.</param>
        /// <param name="width">The width<see cref="ushort"/>.</param>
        /// <returns>The <see cref="short"/>.</returns>
        public static short PosX8to16(byte pos, ushort width)
        {
            return (short)(((int)pos - (int)width / 2) * 100 + 50);
        }

        /// <summary>
        /// The PosY8to16.
        /// </summary>
        /// <param name="pos">The pos<see cref="byte"/>.</param>
        /// <param name="height">The height<see cref="ushort"/>.</param>
        /// <returns>The <see cref="short"/>.</returns>
        public static short PosY8to16(byte pos, ushort height)
        {
            return (short)(-((int)pos - (int)height / 2) * 100 - 50);
        }

        /// <summary>
        /// The MakeSightRange.
        /// </summary>
        /// <param name="range">The range<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public static uint MakeSightRange(uint range)
        {
            if (range > Global.MAX_SIGHT_RANGE)
                range = Global.MAX_SIGHT_RANGE;
            return range;
        }

        /// <summary>
        /// The MakeHourDelay.
        /// </summary>
        /// <param name="hours">The hours<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int MakeHourDelay(int hours)
        {
            return 3600000 * hours;
        }

        /// <summary>
        /// The MakeMinDelay.
        /// </summary>
        /// <param name="minutes">The minutes<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int MakeMinDelay(int minutes)
        {
            return 60000 * minutes;
        }

        /// <summary>
        /// The MakeSecDelay.
        /// </summary>
        /// <param name="seconds">The seconds<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int MakeSecDelay(int seconds)
        {
            return 1000 * seconds;
        }

        /// <summary>
        /// The MakeMilliDelay.
        /// </summary>
        /// <param name="milliseconds">The milliseconds<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int MakeMilliDelay(int milliseconds)
        {
            return milliseconds;
        }

        /// <summary>
        /// Defines the <see cref="RandomF" />.
        /// </summary>
        public class RandomF
        {
            /// <summary>
            /// Defines the random.
            /// </summary>
            public System.Random random = new System.Random(DateTime.Now.Millisecond);

            /// <summary>
            /// Defines the last.
            /// </summary>
            private int last;

            /// <summary>
            /// The Next.
            /// </summary>
            /// <param name="min">The min<see cref="int"/>.</param>
            /// <param name="max">The max<see cref="int"/>.</param>
            /// <returns>The <see cref="int"/>.</returns>
            [MethodImpl(MethodImplOptions.Synchronized)]
            public int Next(int min, int max)
            {
                if (max != int.MaxValue)
                    ++max;
                int num = this.random.Next(min, max);
                if (num == this.last)
                {
                    num = this.random.Next(min, max);
                    if (num == this.last)
                    {
                        num = this.random.Next(min, max);
                        if (num == this.last)
                        {
                            if (this.last == 0)
                            {
                                Logger.ShowDebug("Random function(min:" + min.ToString() + ",max:" + (object)max + ") returning value:" + this.last.ToString() + " for three times!", (Logger)null);
                                this.random = new System.Random(DateTime.Now.Millisecond);
                            }
                            else
                                this.last = num;
                        }
                    }
                    else
                        this.last = num;
                }
                else
                    this.last = num;
                return num;
            }

            /// <summary>
            /// The Next.
            /// </summary>
            /// <returns>The <see cref="int"/>.</returns>
            public int Next()
            {
                return this.random.Next();
            }

            /// <summary>
            /// The Next.
            /// </summary>
            /// <param name="max">The max<see cref="int"/>.</param>
            /// <returns>The <see cref="int"/>.</returns>
            public int Next(int max)
            {
                return this.Next(0, max);
            }

            /// <summary>
            /// The NextDouble.
            /// </summary>
            /// <returns>The <see cref="double"/>.</returns>
            public double NextDouble()
            {
                return this.random.NextDouble();
            }
        }
    }
}
