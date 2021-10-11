namespace ICSharpCode.SharpZipLib.Zip
{
    using ICSharpCode.SharpZipLib.Core;

    /// <summary>
    /// Defines the <see cref="IEntryFactory" />.
    /// </summary>
    public interface IEntryFactory
    {
        /// <summary>
        /// The MakeFileEntry.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="ZipEntry"/>.</returns>
        ZipEntry MakeFileEntry(string fileName);

        /// <summary>
        /// The MakeFileEntry.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <param name="useFileSystem">The useFileSystem<see cref="bool"/>.</param>
        /// <returns>The <see cref="ZipEntry"/>.</returns>
        ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

        /// <summary>
        /// The MakeDirectoryEntry.
        /// </summary>
        /// <param name="directoryName">The directoryName<see cref="string"/>.</param>
        /// <returns>The <see cref="ZipEntry"/>.</returns>
        ZipEntry MakeDirectoryEntry(string directoryName);

        /// <summary>
        /// The MakeDirectoryEntry.
        /// </summary>
        /// <param name="directoryName">The directoryName<see cref="string"/>.</param>
        /// <param name="useFileSystem">The useFileSystem<see cref="bool"/>.</param>
        /// <returns>The <see cref="ZipEntry"/>.</returns>
        ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);

        /// <summary>
        /// Gets or sets the NameTransform.
        /// </summary>
        INameTransform NameTransform { get; set; }
    }
}
