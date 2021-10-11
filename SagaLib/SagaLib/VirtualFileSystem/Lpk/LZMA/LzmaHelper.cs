namespace SagaLib.VirtualFileSystem.Lpk.LZMA
{
    using SevenZip;
    using SevenZip.Compression.LZMA;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="LzmaHelper" />.
    /// </summary>
    public static class LzmaHelper
    {
        /// <summary>
        /// Defines the dictionary.
        /// </summary>
        private static int dictionary = 8388608;

        /// <summary>
        /// Defines the eos.
        /// </summary>
        private static bool eos = false;

        /// <summary>
        /// Defines the propIDs.
        /// </summary>
        private static CoderPropID[] propIDs = new CoderPropID[8]
    {
      CoderPropID.DictionarySize,
      CoderPropID.PosStateBits,
      CoderPropID.LitContextBits,
      CoderPropID.LitPosBits,
      CoderPropID.Algorithm,
      CoderPropID.NumFastBytes,
      CoderPropID.MatchFinder,
      CoderPropID.EndMarker
    };

        /// <summary>
        /// Defines the properties.
        /// </summary>
        private static object[] properties = new object[8]
    {
      (object) LzmaHelper.dictionary,
      (object) 2,
      (object) 3,
      (object) 0,
      (object) 2,
      (object) 128,
      (object) "bt4",
      (object) LzmaHelper.eos
    };

        /// <summary>
        /// Defines the props.
        /// </summary>
        private static byte[] props = new byte[5]
    {
      (byte) 93,
      (byte) 0,
      (byte) 0,
      (byte) 128,
      (byte) 0
    };

        /// <summary>
        /// The Compress.
        /// </summary>
        /// <param name="inStream">The inStream<see cref="Stream"/>.</param>
        /// <param name="outStream">The outStream<see cref="Stream"/>.</param>
        /// <param name="progress">The progress<see cref="ICodeProgress"/>.</param>
        public static void Compress(Stream inStream, Stream outStream, ICodeProgress progress)
        {
            Encoder encoder = new Encoder();
            encoder.SetCoderProperties(LzmaHelper.propIDs, LzmaHelper.properties);
            encoder.Code(inStream, outStream, -1L, -1L, progress);
        }

        /// <summary>
        /// The Decompress.
        /// </summary>
        /// <param name="inStream">The inStream<see cref="Stream"/>.</param>
        /// <param name="outStream">The outStream<see cref="Stream"/>.</param>
        /// <param name="size">The size<see cref="long"/>.</param>
        /// <param name="uncompressedSize">The uncompressedSize<see cref="long"/>.</param>
        /// <param name="progress">The progress<see cref="ICodeProgress"/>.</param>
        public static void Decompress(Stream inStream, Stream outStream, long size, long uncompressedSize, ICodeProgress progress)
        {
            Decoder decoder = new Decoder();
            decoder.SetDecoderProperties(LzmaHelper.props);
            decoder.Code(inStream, outStream, size, uncompressedSize, progress);
        }

        /// <summary>
        /// The CRC32.
        /// </summary>
        /// <param name="inStream">The inStream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public static uint CRC32(Stream inStream)
        {
            CRC crc = new CRC();
            crc.Init();
            byte[] numArray = new byte[1024];
            while (inStream.Position < inStream.Length)
            {
                int count = inStream.Position + 1024L >= inStream.Length ? (int)(inStream.Length - inStream.Position) : 1024;
                inStream.Read(numArray, 0, count);
                crc.Update(numArray, 0U, (uint)count);
            }
            return crc.GetDigest();
        }
    }
}
