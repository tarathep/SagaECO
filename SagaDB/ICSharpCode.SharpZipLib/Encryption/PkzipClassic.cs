namespace ICSharpCode.SharpZipLib.Encryption
{
    using ICSharpCode.SharpZipLib.Checksums;
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Defines the <see cref="PkzipClassic" />.
    /// </summary>
    public abstract class PkzipClassic : SymmetricAlgorithm
    {
        /// <summary>
        /// The GenerateKeys.
        /// </summary>
        /// <param name="seed">The seed<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public static byte[] GenerateKeys(byte[] seed)
        {
            if (seed == null)
                throw new ArgumentNullException(nameof(seed));
            if (seed.Length == 0)
                throw new ArgumentException("Length is zero", nameof(seed));
            uint[] numArray = new uint[3]
            {
        305419896U,
        591751049U,
        878082192U
            };
            for (int index = 0; index < seed.Length; ++index)
            {
                numArray[0] = Crc32.ComputeCrc32(numArray[0], seed[index]);
                numArray[1] = numArray[1] + (uint)(byte)numArray[0];
                numArray[1] = (uint)((int)numArray[1] * 134775813 + 1);
                numArray[2] = Crc32.ComputeCrc32(numArray[2], (byte)(numArray[1] >> 24));
            }
            return new byte[12]
            {
        (byte) (numArray[0] & (uint) byte.MaxValue),
        (byte) (numArray[0] >> 8 & (uint) byte.MaxValue),
        (byte) (numArray[0] >> 16 & (uint) byte.MaxValue),
        (byte) (numArray[0] >> 24 & (uint) byte.MaxValue),
        (byte) (numArray[1] & (uint) byte.MaxValue),
        (byte) (numArray[1] >> 8 & (uint) byte.MaxValue),
        (byte) (numArray[1] >> 16 & (uint) byte.MaxValue),
        (byte) (numArray[1] >> 24 & (uint) byte.MaxValue),
        (byte) (numArray[2] & (uint) byte.MaxValue),
        (byte) (numArray[2] >> 8 & (uint) byte.MaxValue),
        (byte) (numArray[2] >> 16 & (uint) byte.MaxValue),
        (byte) (numArray[2] >> 24 & (uint) byte.MaxValue)
            };
        }
    }
}
