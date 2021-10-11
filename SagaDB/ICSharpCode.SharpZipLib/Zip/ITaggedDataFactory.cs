namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>
    /// Defines the <see cref="ITaggedDataFactory" />.
    /// </summary>
    internal interface ITaggedDataFactory
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="tag">The tag<see cref="short"/>.</param>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        /// <returns>The <see cref="ITaggedData"/>.</returns>
        ITaggedData Create(short tag, byte[] data, int offset, int count);
    }
}
