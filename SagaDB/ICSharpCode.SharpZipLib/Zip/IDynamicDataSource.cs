namespace ICSharpCode.SharpZipLib.Zip
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="IDynamicDataSource" />.
    /// </summary>
    public interface IDynamicDataSource
    {
        /// <summary>
        /// The GetSource.
        /// </summary>
        /// <param name="entry">The entry<see cref="ZipEntry"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        Stream GetSource(ZipEntry entry, string name);
    }
}
