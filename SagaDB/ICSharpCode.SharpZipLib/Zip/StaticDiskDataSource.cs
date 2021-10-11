namespace ICSharpCode.SharpZipLib.Zip
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="StaticDiskDataSource" />.
    /// </summary>
    public class StaticDiskDataSource : IStaticDataSource
    {
        /// <summary>
        /// Defines the fileName_.
        /// </summary>
        private string fileName_;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticDiskDataSource"/> class.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        public StaticDiskDataSource(string fileName)
        {
            this.fileName_ = fileName;
        }

        /// <summary>
        /// The GetSource.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        public Stream GetSource()
        {
            return (Stream)File.OpenRead(this.fileName_);
        }
    }
}
