namespace ICSharpCode.SharpZipLib.Encryption
{
    using System;
    using System.Security.Cryptography;

    /// <summary>
    /// Defines the <see cref="PkzipClassicEncryptCryptoTransform" />.
    /// </summary>
    internal class PkzipClassicEncryptCryptoTransform : PkzipClassicCryptoBase, ICryptoTransform, IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PkzipClassicEncryptCryptoTransform"/> class.
        /// </summary>
        /// <param name="keyBlock">The keyBlock<see cref="byte[]"/>.</param>
        internal PkzipClassicEncryptCryptoTransform(byte[] keyBlock)
        {
            this.SetKeys(keyBlock);
        }

        /// <summary>
        /// The TransformFinalBlock.
        /// </summary>
        /// <param name="inputBuffer">The inputBuffer<see cref="byte[]"/>.</param>
        /// <param name="inputOffset">The inputOffset<see cref="int"/>.</param>
        /// <param name="inputCount">The inputCount<see cref="int"/>.</param>
        /// <returns>The <see cref="byte[]"/>.</returns>
        public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
        {
            byte[] outputBuffer = new byte[inputCount];
            this.TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, 0);
            return outputBuffer;
        }

        /// <summary>
        /// The TransformBlock.
        /// </summary>
        /// <param name="inputBuffer">The inputBuffer<see cref="byte[]"/>.</param>
        /// <param name="inputOffset">The inputOffset<see cref="int"/>.</param>
        /// <param name="inputCount">The inputCount<see cref="int"/>.</param>
        /// <param name="outputBuffer">The outputBuffer<see cref="byte[]"/>.</param>
        /// <param name="outputOffset">The outputOffset<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
        {
            for (int index = inputOffset; index < inputOffset + inputCount; ++index)
            {
                byte ch = inputBuffer[index];
                outputBuffer[outputOffset++] = (byte)((uint)inputBuffer[index] ^ (uint)this.TransformByte());
                this.UpdateKeys(ch);
            }
            return inputCount;
        }

        /// <summary>
        /// Gets a value indicating whether CanReuseTransform.
        /// </summary>
        public bool CanReuseTransform
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Gets the InputBlockSize.
        /// </summary>
        public int InputBlockSize
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets the OutputBlockSize.
        /// </summary>
        public int OutputBlockSize
        {
            get
            {
                return 1;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanTransformMultipleBlocks.
        /// </summary>
        public bool CanTransformMultipleBlocks
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public void Dispose()
        {
            this.Reset();
        }
    }
}
