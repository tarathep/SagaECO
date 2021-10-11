namespace ICSharpCode.SharpZipLib.Core
{
    /// <summary>
    /// Defines the <see cref="DirectoryEventArgs" />.
    /// </summary>
    public class DirectoryEventArgs : ScanEventArgs
    {
        /// <summary>
        /// Defines the hasMatchingFiles_.
        /// </summary>
        private bool hasMatchingFiles_;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryEventArgs"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="hasMatchingFiles">The hasMatchingFiles<see cref="bool"/>.</param>
        public DirectoryEventArgs(string name, bool hasMatchingFiles)
      : base(name)
        {
            this.hasMatchingFiles_ = hasMatchingFiles;
        }

        /// <summary>
        /// Gets a value indicating whether HasMatchingFiles.
        /// </summary>
        public bool HasMatchingFiles
        {
            get
            {
                return this.hasMatchingFiles_;
            }
        }
    }
}
