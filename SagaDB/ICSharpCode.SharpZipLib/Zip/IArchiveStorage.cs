namespace ICSharpCode.SharpZipLib.Zip
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="IArchiveStorage" />.
    /// </summary>
    public interface IArchiveStorage
    {
        /// <summary>
        /// Gets the UpdateMode.
        /// </summary>
        FileUpdateMode UpdateMode { get; }

        /// <summary>
        /// The GetTemporaryOutput.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        Stream GetTemporaryOutput();

        /// <summary>
        /// The ConvertTemporaryToFinal.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        Stream ConvertTemporaryToFinal();

        /// <summary>
        /// The MakeTemporaryCopy.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        Stream MakeTemporaryCopy(Stream stream);

        /// <summary>
        /// The OpenForDirectUpdate.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        Stream OpenForDirectUpdate(Stream stream);

        /// <summary>
        /// The Dispose.
        /// </summary>
        void Dispose();
    }
}
