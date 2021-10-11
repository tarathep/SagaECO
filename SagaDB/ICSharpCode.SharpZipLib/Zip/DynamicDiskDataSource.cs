namespace ICSharpCode.SharpZipLib.Zip
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="DynamicDiskDataSource" />.
    /// </summary>
    public class DynamicDiskDataSource : IDynamicDataSource
    {
        /// <summary>
        /// The GetSource.
        /// </summary>
        /// <param name="entry">The entry<see cref="ZipEntry"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public Stream GetSource(ZipEntry entry, string name)
        {
            Stream stream = (Stream)null;
            if (name != null)
                stream = (Stream)File.OpenRead(name);
            return stream;
        }
    }
}
