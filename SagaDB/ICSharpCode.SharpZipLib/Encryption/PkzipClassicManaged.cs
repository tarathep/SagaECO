namespace ICSharpCode.SharpZipLib.Encryption
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Defines the <see cref="PkzipClassicManaged" />.
    /// </summary>
    public sealed class PkzipClassicManaged : PkzipClassic
    {
        /// <summary>
        /// Defines the key_.
        /// </summary>
        private byte[] key_;

        /// <summary>
        /// Gets or sets the BlockSize.
        /// </summary>
        public override int BlockSize
        {
            get
            {
                return 8;
            }
            set
            {
                if (value != 8)
                    throw new CryptographicException("Block size is invalid");
            }
        }

        /// <summary>
        /// Gets the LegalKeySizes.
        /// </summary>
        public override KeySizes[] LegalKeySizes
        {
            get
            {
                return new KeySizes[1] { new KeySizes(96, 96, 0) };
            }
        }

        /// <summary>
        /// The GenerateIV.
        /// </summary>
        public override void GenerateIV()
        {
        }

        /// <summary>
        /// Gets the LegalBlockSizes.
        /// </summary>
        public override KeySizes[] LegalBlockSizes
        {
            get
            {
                return new KeySizes[1] { new KeySizes(8, 8, 0) };
            }
        }

        /// <summary>
        /// Gets or sets the Key.
        /// </summary>
        public override byte[] Key
        {
            get
            {
                if (this.key_ == null)
                    this.GenerateKey();
                return (byte[])this.key_.Clone();
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                if (value.Length != 12)
                    throw new CryptographicException("Key size is illegal");
                this.key_ = (byte[])value.Clone();
            }
        }

        /// <summary>
        /// The GenerateKey.
        /// </summary>
        public override void GenerateKey()
        {
            this.key_ = new byte[12];
            new Random().NextBytes(this.key_);
        }

        /// <summary>
        /// The CreateEncryptor.
        /// </summary>
        /// <param name="rgbKey">The rgbKey<see cref="byte[]"/>.</param>
        /// <param name="rgbIV">The rgbIV<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="ICryptoTransform"/>.</returns>
        public override ICryptoTransform CreateEncryptor(byte[] rgbKey, byte[] rgbIV)
        {
            this.key_ = rgbKey;
            return (ICryptoTransform)new PkzipClassicEncryptCryptoTransform(this.Key);
        }

        /// <summary>
        /// The CreateDecryptor.
        /// </summary>
        /// <param name="rgbKey">The rgbKey<see cref="byte[]"/>.</param>
        /// <param name="rgbIV">The rgbIV<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="ICryptoTransform"/>.</returns>
        public override ICryptoTransform CreateDecryptor(byte[] rgbKey, byte[] rgbIV)
        {
            this.key_ = rgbKey;
            return (ICryptoTransform)new PkzipClassicDecryptCryptoTransform(this.Key);
        }
    }
}
