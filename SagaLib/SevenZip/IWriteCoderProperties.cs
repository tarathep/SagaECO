namespace SevenZip
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="IWriteCoderProperties" />.
    /// </summary>
    public interface IWriteCoderProperties
    {
        /// <summary>
        /// The WriteCoderProperties.
        /// </summary>
        /// <param name="outStream">The outStream<see cref="Stream"/>.</param>
        void WriteCoderProperties(Stream outStream);
    }
}
