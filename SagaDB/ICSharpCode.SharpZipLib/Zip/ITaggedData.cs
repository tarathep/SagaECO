namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>
    /// Defines the <see cref="ITaggedData" />.
    /// </summary>
    public interface ITaggedData
    {
        /// <summary>
        /// Gets the TagID.
        /// </summary>
        short TagID { get; }

        /// <summary>
        /// The SetData.
        /// </summary>
        /// <param name="data">The data<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        void SetData(byte[] data, int offset, int count);

        /// <summary>
        /// The GetData.
        /// </summary>
        /// <returns>The <see cref="byte[]"/>.</returns>
        byte[] GetData();
    }
}
