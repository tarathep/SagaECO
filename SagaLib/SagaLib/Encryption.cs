namespace SagaLib
{
    using Mono.Math;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="Encryption" />.
    /// </summary>
    public class Encryption
    {
        /// <summary>
        /// Defines the Two.
        /// </summary>
        public static BigInteger Two = new BigInteger(2U);

        /// <summary>
        /// Defines the Module.
        /// </summary>
        public static BigInteger Module = new BigInteger("f488fd584e49dbcd20b49de49107366b336c380d451d0f7c88b31c7c5b2d8ef6f3c923c043f0a55b188d8ebb558cb85d38d334fd7c175743a31d186cde33212cb52aff3ce1b1294018118d7c84a70a72d686c40319c807297aca950cd9969fabd00a509b0246d3083d66a45d419f9c7cbd894b221926baaba25ec355e92f78c7");

        /// <summary>
        /// Defines the privateKey.
        /// </summary>
        private BigInteger privateKey = Encryption.Two;

        /// <summary>
        /// Defines the aesKey.
        /// </summary>
        private byte[] aesKey;

        /// <summary>
        /// Defines the aes.
        /// </summary>
        private Rijndael aes;

        /// <summary>
        /// Initializes a new instance of the <see cref="Encryption"/> class.
        /// </summary>
        public Encryption()
        {
            this.aes = Rijndael.Create();
            this.aes.Mode = CipherMode.ECB;
            this.aes.KeySize = 128;
            this.aes.Padding = PaddingMode.None;
        }

        /// <summary>
        /// The MakePrivateKey.
        /// </summary>
        public void MakePrivateKey()
        {
            SHA1 shA1 = SHA1.Create();
            byte[] numArray = new byte[40];
            shA1.TransformBlock(Encoding.ASCII.GetBytes(DateTime.Now.ToString() + (object)DateTime.Now.ToUniversalTime() + DateTime.Now.ToLongDateString()), 0, 40, numArray, 0);
            this.privateKey = new BigInteger(numArray);
        }

        /// <summary>
        /// The GetKeyExchangeBytes.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] GetKeyExchangeBytes()
        {
            if (this.privateKey == Encryption.Two)
                return (byte[])null;
            return Encryption.Two.modPow(this.privateKey, Encryption.Module).getBytes();
        }

        /// <summary>
        /// The MakeAESKey.
        /// </summary>
        /// <param name="keyExchangeBytes">The keyExchangeBytes<see cref="string"/>.</param>
        public void MakeAESKey(string keyExchangeBytes)
        {
            byte[] bytes = new BigInteger(keyExchangeBytes).modPow(this.privateKey, Encryption.Module).getBytes();
            this.aesKey = new byte[16];
            Array.Copy((Array)bytes, (Array)this.aesKey, 16);
            for (int index = 0; index < 16; ++index)
            {
                byte num1 = (byte)((uint)this.aesKey[index] >> 4);
                byte num2 = (byte)((uint)this.aesKey[index] & 15U);
                if (num1 > (byte)9)
                    num1 -= (byte)9;
                if (num2 > (byte)9)
                    num2 -= (byte)9;
                this.aesKey[index] = (byte)((uint)num1 << 4 | (uint)num2);
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsReady.
        /// </summary>
        public bool IsReady
        {
            get
            {
                return this.aesKey != null;
            }
        }

        /// <summary>
        /// The Encrypt.
        /// </summary>
        /// <param name="src">The src<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] Encrypt(byte[] src, int offset)
        {
            if (this.aesKey == null || offset == src.Length)
                return src;
            ICryptoTransform encryptor = this.aes.CreateEncryptor(this.aesKey, new byte[16]);
            int inputCount = src.Length - offset;
            byte[] outputBuffer = new byte[src.Length];
            src.CopyTo((Array)outputBuffer, 0);
            encryptor.TransformBlock(src, offset, inputCount, outputBuffer, offset);
            return outputBuffer;
        }

        /// <summary>
        /// The Decrypt.
        /// </summary>
        /// <param name="src">The src<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] Decrypt(byte[] src, int offset)
        {
            if (this.aesKey == null || offset == src.Length)
                return src;
            ICryptoTransform decryptor = this.aes.CreateDecryptor(this.aesKey, new byte[16]);
            int inputCount = src.Length - offset;
            byte[] outputBuffer = new byte[src.Length];
            src.CopyTo((Array)outputBuffer, 0);
            decryptor.TransformBlock(src, offset, inputCount, outputBuffer, offset);
            return outputBuffer;
        }
    }
}
