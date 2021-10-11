namespace ICSharpCode.SharpZipLib.Zip
{
    using ICSharpCode.SharpZipLib.Core;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="MemoryArchiveStorage" />.
    /// </summary>
    public class MemoryArchiveStorage : BaseArchiveStorage
    {
        /// <summary>
        /// Defines the temporaryStream_.
        /// </summary>
        private MemoryStream temporaryStream_;

        /// <summary>
        /// Defines the finalStream_.
        /// </summary>
        private MemoryStream finalStream_;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryArchiveStorage"/> class.
        /// </summary>
        public MemoryArchiveStorage()
      : base(FileUpdateMode.Direct)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemoryArchiveStorage"/> class.
        /// </summary>
        /// <param name="updateMode">The updateMode<see cref="FileUpdateMode"/>.</param>
        public MemoryArchiveStorage(FileUpdateMode updateMode)
      : base(updateMode)
        {
        }

        /// <summary>
        /// Gets the FinalStream.
        /// </summary>
        public MemoryStream FinalStream
        {
            get
            {
                return this.finalStream_;
            }
        }

        /// <summary>
        /// The GetTemporaryOutput.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        public override Stream GetTemporaryOutput()
        {
            this.temporaryStream_ = new MemoryStream();
            return (Stream)this.temporaryStream_;
        }

        /// <summary>
        /// The ConvertTemporaryToFinal.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        public override Stream ConvertTemporaryToFinal()
        {
            if (this.temporaryStream_ == null)
                throw new ZipException("No temporary stream has been created");
            this.finalStream_ = new MemoryStream(this.temporaryStream_.ToArray());
            return (Stream)this.finalStream_;
        }

        /// <summary>
        /// The MakeTemporaryCopy.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public override Stream MakeTemporaryCopy(Stream stream)
        {
            this.temporaryStream_ = new MemoryStream();
            stream.Position = 0L;
            StreamUtils.Copy(stream, (Stream)this.temporaryStream_, new byte[4096]);
            return (Stream)this.temporaryStream_;
        }

        /// <summary>
        /// The OpenForDirectUpdate.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public override Stream OpenForDirectUpdate(Stream stream)
        {
            Stream destination;
            if (stream == null || !stream.CanWrite)
            {
                destination = (Stream)new MemoryStream();
                if (stream != null)
                {
                    stream.Position = 0L;
                    StreamUtils.Copy(stream, destination, new byte[4096]);
                    stream.Close();
                }
            }
            else
                destination = stream;
            return destination;
        }

        /// <summary>
        /// The Dispose.
        /// </summary>
        public override void Dispose()
        {
            if (this.temporaryStream_ == null)
                return;
            this.temporaryStream_.Close();
        }
    }
}
