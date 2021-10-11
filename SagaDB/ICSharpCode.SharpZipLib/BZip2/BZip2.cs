namespace ICSharpCode.SharpZipLib.BZip2
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="BZip2" />.
    /// </summary>
    public sealed class BZip2
    {
        /// <summary>
        /// The Decompress.
        /// </summary>
        /// <param name="inStream">The inStream<see cref="Stream"/>.</param>
        /// <param name="outStream">The outStream<see cref="Stream"/>.</param>
        public static void Decompress(Stream inStream, Stream outStream)
        {
            if (inStream == null)
                throw new ArgumentNullException(nameof(inStream));
            if (outStream == null)
                throw new ArgumentNullException(nameof(outStream));
            using (outStream)
            {
                using (BZip2InputStream bzip2InputStream = new BZip2InputStream(inStream))
                {
                    for (int index = bzip2InputStream.ReadByte(); index != -1; index = bzip2InputStream.ReadByte())
                        outStream.WriteByte((byte)index);
                }
            }
        }

        /// <summary>
        /// The Compress.
        /// </summary>
        /// <param name="inStream">The inStream<see cref="Stream"/>.</param>
        /// <param name="outStream">The outStream<see cref="Stream"/>.</param>
        /// <param name="blockSize">The blockSize<see cref="int"/>.</param>
        public static void Compress(Stream inStream, Stream outStream, int blockSize)
        {
            if (inStream == null)
                throw new ArgumentNullException(nameof(inStream));
            if (outStream == null)
                throw new ArgumentNullException(nameof(outStream));
            using (inStream)
            {
                using (BZip2OutputStream bzip2OutputStream = new BZip2OutputStream(outStream, blockSize))
                {
                    for (int index = inStream.ReadByte(); index != -1; index = inStream.ReadByte())
                        bzip2OutputStream.WriteByte((byte)index);
                }
            }
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="BZip2"/> class from being created.
        /// </summary>
        private BZip2()
        {
        }
    }
}
