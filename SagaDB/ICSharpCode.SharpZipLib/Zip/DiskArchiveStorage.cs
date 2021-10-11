namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="DiskArchiveStorage" />.
    /// </summary>
    public class DiskArchiveStorage : BaseArchiveStorage
    {
        /// <summary>
        /// Defines the temporaryStream_.
        /// </summary>
        private Stream temporaryStream_;

        /// <summary>
        /// Defines the fileName_.
        /// </summary>
        private string fileName_;

        /// <summary>
        /// Defines the temporaryName_.
        /// </summary>
        private string temporaryName_;

        /// <summary>
        /// Initializes a new instance of the <see cref="DiskArchiveStorage"/> class.
        /// </summary>
        /// <param name="file">The file<see cref="ZipFile"/>.</param>
        /// <param name="updateMode">The updateMode<see cref="FileUpdateMode"/>.</param>
        public DiskArchiveStorage(ZipFile file, FileUpdateMode updateMode)
      : base(updateMode)
        {
            if (file.Name == null)
                throw new ZipException("Cant handle non file archives");
            this.fileName_ = file.Name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DiskArchiveStorage"/> class.
        /// </summary>
        /// <param name="file">The file<see cref="ZipFile"/>.</param>
        public DiskArchiveStorage(ZipFile file)
      : this(file, FileUpdateMode.Safe)
        {
        }

        /// <summary>
        /// The GetTemporaryOutput.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        public override Stream GetTemporaryOutput()
        {
            if (this.temporaryName_ != null)
            {
                this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.temporaryName_, true);
                this.temporaryStream_ = (Stream)File.OpenWrite(this.temporaryName_);
            }
            else
            {
                this.temporaryName_ = Path.GetTempFileName();
                this.temporaryStream_ = (Stream)File.OpenWrite(this.temporaryName_);
            }
            return this.temporaryStream_;
        }

        /// <summary>
        /// The ConvertTemporaryToFinal.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        public override Stream ConvertTemporaryToFinal()
        {
            if (this.temporaryStream_ == null)
                throw new ZipException("No temporary stream has been created");
            Stream stream1 = (Stream)null;
            string tempFileName = DiskArchiveStorage.GetTempFileName(this.fileName_, false);
            bool flag = false;
            Stream stream2;
            try
            {
                this.temporaryStream_.Close();
                File.Move(this.fileName_, tempFileName);
                File.Move(this.temporaryName_, this.fileName_);
                flag = true;
                File.Delete(tempFileName);
                stream2 = (Stream)File.OpenRead(this.fileName_);
            }
            catch (Exception ex)
            {
                stream1 = (Stream)null;
                if (!flag)
                {
                    File.Move(tempFileName, this.fileName_);
                    File.Delete(this.temporaryName_);
                }
                throw;
            }
            return stream2;
        }

        /// <summary>
        /// The MakeTemporaryCopy.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public override Stream MakeTemporaryCopy(Stream stream)
        {
            stream.Close();
            this.temporaryName_ = DiskArchiveStorage.GetTempFileName(this.fileName_, true);
            File.Copy(this.fileName_, this.temporaryName_, true);
            this.temporaryStream_ = (Stream)new FileStream(this.temporaryName_, FileMode.Open, FileAccess.ReadWrite);
            return this.temporaryStream_;
        }

        /// <summary>
        /// The OpenForDirectUpdate.
        /// </summary>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public override Stream OpenForDirectUpdate(Stream stream)
        {
            Stream stream1;
            if (stream == null || !stream.CanWrite)
            {
                stream?.Close();
                stream1 = (Stream)new FileStream(this.fileName_, FileMode.Open, FileAccess.ReadWrite);
            }
            else
                stream1 = stream;
            return stream1;
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

        /// <summary>
        /// The GetTempFileName.
        /// </summary>
        /// <param name="original">The original<see cref="string"/>.</param>
        /// <param name="makeTempFile">The makeTempFile<see cref="bool"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string GetTempFileName(string original, bool makeTempFile)
        {
            string str = (string)null;
            if (original == null)
            {
                str = Path.GetTempFileName();
            }
            else
            {
                int num = 0;
                int second = DateTime.Now.Second;
                while (str == null)
                {
                    ++num;
                    string path = string.Format("{0}.{1}{2}.tmp", (object)original, (object)second, (object)num);
                    if (!File.Exists(path))
                    {
                        if (makeTempFile)
                        {
                            try
                            {
                                using (File.Create(path))
                                    ;
                                str = path;
                            }
                            catch
                            {
                                second = DateTime.Now.Second;
                            }
                        }
                        else
                            str = path;
                    }
                }
            }
            return str;
        }
    }
}
