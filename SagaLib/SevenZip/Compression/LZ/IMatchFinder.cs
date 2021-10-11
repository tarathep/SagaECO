namespace SevenZip.Compression.LZ
{
    /// <summary>
    /// Defines the <see cref="IMatchFinder" />.
    /// </summary>
    internal interface IMatchFinder : IInWindowStream
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="historySize">The historySize<see cref="uint"/>.</param>
        /// <param name="keepAddBufferBefore">The keepAddBufferBefore<see cref="uint"/>.</param>
        /// <param name="matchMaxLen">The matchMaxLen<see cref="uint"/>.</param>
        /// <param name="keepAddBufferAfter">The keepAddBufferAfter<see cref="uint"/>.</param>
        void Create(uint historySize, uint keepAddBufferBefore, uint matchMaxLen, uint keepAddBufferAfter);

        /// <summary>
        /// The GetMatches.
        /// </summary>
        /// <param name="distances">The distances<see cref="uint[]"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        uint GetMatches(uint[] distances);

        /// <summary>
        /// The Skip.
        /// </summary>
        /// <param name="num">The num<see cref="uint"/>.</param>
        void Skip(uint num);
    }
}
