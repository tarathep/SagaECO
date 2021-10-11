namespace ICSharpCode.SharpZipLib.Zip
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="BaseArchiveStorage" />.
    /// </summary>
    public abstract class BaseArchiveStorage : IArchiveStorage
    {
        /// <summary>
        /// Defines the updateMode_.
        /// </summary>
        private FileUpdateMode updateMode_;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseArchiveStorage"/> class.
        /// </summary>
        /// <param name="updateMode">The updateMode<see cref="FileUpdateMode"/>.</param>
        protected BaseArchiveStorage(FileUpdateMode updateMode)
        {
            this.updateMode_ = updateMode;
        }

        /// <summary>
        /// The GetTemporaryOutput.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        public abstract Stream GetTemporaryOutput();

        /// <summary>
        /// The ConvertTemporaryToFinal.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        public abstract Stream ConvertTemporaryToFinal();

        /// <summary>
        /// The MakeTemporaryCopy.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public abstract Stream MakeTemporaryCopy(Stream stream);

        /// <summary>
        /// The OpenForDirectUpdate.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public abstract Stream OpenForDirectUpdate(Stream stream);

        /// <summary>
        /// The Dispose.
        /// </summary>
        public abstract void Dispose();

        /// <summary>
        /// Gets the UpdateMode.
        /// </summary>
        public FileUpdateMode UpdateMode
        {
            get
            {
                return this.updateMode_;
            }
        }
    }
}
