namespace SevenZip.Compression.LZ
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="IInWindowStream" />.
    /// </summary>
    internal interface IInWindowStream
    {
        /// <summary>
        /// The SetStream.
        /// </summary>
        /// <param name="inStream">The inStream<see cref="Stream"/>.</param>
        void SetStream(Stream inStream);

        /// <summary>
        /// The Init.
        /// </summary>
        void Init();

        /// <summary>
        /// The ReleaseStream.
        /// </summary>
        void ReleaseStream();

        /// <summary>
        /// The GetIndexByte.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <returns>The <see cref="byte"/>.</returns>
        byte GetIndexByte(int index);

        /// <summary>
        /// The GetMatchLen.
        /// </summary>
        /// <param name="index">The index<see cref="int"/>.</param>
        /// <param name="distance">The distance<see cref="uint"/>.</param>
        /// <param name="limit">The limit<see cref="uint"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        uint GetMatchLen(int index, uint distance, uint limit);

        /// <summary>
        /// The GetNumAvailableBytes.
        /// </summary>
        /// <returns>The <see cref="uint"/>.</returns>
        uint GetNumAvailableBytes();
    }
}
