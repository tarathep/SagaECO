namespace SagaLib
{
    using System;

    /// <summary>
    /// Defines the <see cref="Conversions" />.
    /// </summary>
    public static class Conversions
    {
        /// <summary>
        /// The ToByte.
        /// </summary>
        /// <param name="Value">The Value<see cref="string"/>.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        public static byte ToByte(string Value)
        {
            if (Value == null)
                return 0;
            return (byte)Convert.ToInt64(Value, 16);
        }

        /// <summary>
        /// The ToInteger.
        /// </summary>
        /// <param name="Value">The Value<see cref="string"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int ToInteger(string Value)
        {
            if (Value == null)
                return 0;
            return (int)Convert.ToInt64(Value, 16);
        }

        /// <summary>
        /// The bytes2HexString.
        /// </summary>
        /// <param name="b">The b<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string bytes2HexString(byte[] b)
        {
            string str1 = "";
            for (int index = 0; index < b.Length; ++index)
            {
                string str2 = b[index].ToString("X2");
                str1 += str2;
            }
            return str1;
        }

        /// <summary>
        /// The uint2HexString.
        /// </summary>
        /// <param name="b">The b<see cref="uint[]"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string uint2HexString(uint[] b)
        {
            string str1 = "";
            if (b == null)
                return "";
            for (int index1 = 0; index1 < b.Length; ++index1)
            {
                string str2 = Conversion.Hex(b[index1]);
                if (str2.Length != 8)
                {
                    for (int index2 = 0; index2 < 8 - str2.Length; ++index2)
                        str2 = "0" + str2;
                }
                str1 += str2;
            }
            return str1;
        }

        /// <summary>
        /// The HexStr2Bytes.
        /// </summary>
        /// <param name="s">The s<see cref="string"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public static byte[] HexStr2Bytes(string s)
        {
            byte[] numArray = new byte[s.Length / 2];
            for (int index = 0; index < s.Length / 2; ++index)
                numArray[index] = Conversions.ToByte(s.Substring(index * 2, 2));
            return numArray;
        }

        /// <summary>
        /// The HexStr2uint.
        /// </summary>
        /// <param name="s">The s<see cref="string"/>.</param>
        /// <returns>The <see cref="uint[]"/>.</returns>
        public static uint[] HexStr2uint(string s)
        {
            uint[] numArray = new uint[s.Length / 8];
            for (int index = 0; index < s.Length / 8; ++index)
                numArray[index] = (uint)Conversions.ToInteger(s.Substring(index * 8, 8));
            return numArray;
        }
    }
}
