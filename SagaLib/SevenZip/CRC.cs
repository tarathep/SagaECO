namespace SevenZip
{
    /// <summary>
    /// Defines the <see cref="CRC" />.
    /// </summary>
    internal class CRC
    {
        /// <summary>
        /// Defines the _value.
        /// </summary>
        private uint _value = uint.MaxValue;

        /// <summary>
        /// Defines the Table.
        /// </summary>
        public static readonly uint[] Table = new uint[256];

        /// <summary>
        /// Initializes static members of the <see cref="CRC"/> class.
        /// </summary>
        static CRC()
        {
            for (uint index1 = 0; index1 < 256U; ++index1)
            {
                uint num = index1;
                for (int index2 = 0; index2 < 8; ++index2)
                {
                    if (((int)num & 1) != 0)
                        num = num >> 1 ^ 3988292384U;
                    else
                        num >>= 1;
                }
                CRC.Table[index1] = num;
            }
        }

        /// <summary>
        /// The Init.
        /// </summary>
        public void Init()
        {
            this._value = uint.MaxValue;
        }

        /// <summary>
        /// The UpdateByte.
        /// </summary>
        /// <param name="b">The b<see cref="byte"/>.</param>
        public void UpdateByte(byte b)
        {
            this._value = CRC.Table[(int)(byte)this._value ^ (int)b] ^ this._value >> 8;
        }

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="uint"/>.</param>
        /// <param name="size">The size<see cref="uint"/>.</param>
        public void Update(byte[] data, uint offset, uint size)
        {
            for (uint index = 0; index < size; ++index)
                this._value = CRC.Table[(int)(byte)this._value ^ (int)data[(offset + index)]] ^ this._value >> 8;
        }

        /// <summary>
        /// The GetDigest.
        /// </summary>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetDigest()
        {
            return this._value ^ uint.MaxValue;
        }

        /// <summary>
        /// The CalculateDigest.
        /// </summary>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="uint"/>.</param>
        /// <param name="size">The size<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        private static uint CalculateDigest(byte[] data, uint offset, uint size)
        {
            CRC crc = new CRC();
            crc.Update(data, offset, size);
            return crc.GetDigest();
        }

        /// <summary>
        /// The VerifyDigest.
        /// </summary>
        /// <param name="digest">The digest<see cref="uint"/>.</param>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="uint"/>.</param>
        /// <param name="size">The size<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool VerifyDigest(uint digest, byte[] data, uint offset, uint size)
        {
            return (int)CRC.CalculateDigest(data, offset, size) == (int)digest;
        }
    }
}
