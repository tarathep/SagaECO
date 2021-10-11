namespace ICSharpCode.SharpZipLib.Zip
{
    using ICSharpCode.SharpZipLib.Core;
    using System;
    using System.Collections;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="FastZip" />.
    /// </summary>
    public class FastZip
    {
        /// <summary>
        /// Defines the entryFactory_.
        /// </summary>
        private IEntryFactory entryFactory_ = (IEntryFactory)new ZipEntryFactory();

        /// <summary>
        /// Defines the useZip64_.
        /// </summary>
        private UseZip64 useZip64_ = UseZip64.Dynamic;

        /// <summary>
        /// Defines the continueRunning_.
        /// </summary>
        private bool continueRunning_;

        /// <summary>
        /// Defines the buffer_.
        /// </summary>
        private byte[] buffer_;

        /// <summary>
        /// Defines the outputStream_.
        /// </summary>
        private ZipOutputStream outputStream_;

        /// <summary>
        /// Defines the zipFile_.
        /// </summary>
        private ZipFile zipFile_;

        /// <summary>
        /// Defines the sourceDirectory_.
        /// </summary>
        private string sourceDirectory_;

        /// <summary>
        /// Defines the fileFilter_.
        /// </summary>
        private NameFilter fileFilter_;

        /// <summary>
        /// Defines the directoryFilter_.
        /// </summary>
        private NameFilter directoryFilter_;

        /// <summary>
        /// Defines the overwrite_.
        /// </summary>
        private FastZip.Overwrite overwrite_;

        /// <summary>
        /// Defines the confirmDelegate_.
        /// </summary>
        private FastZip.ConfirmOverwriteDelegate confirmDelegate_;

        /// <summary>
        /// Defines the restoreDateTimeOnExtract_.
        /// </summary>
        private bool restoreDateTimeOnExtract_;

        /// <summary>
        /// Defines the restoreAttributesOnExtract_.
        /// </summary>
        private bool restoreAttributesOnExtract_;

        /// <summary>
        /// Defines the createEmptyDirectories_.
        /// </summary>
        private bool createEmptyDirectories_;

        /// <summary>
        /// Defines the events_.
        /// </summary>
        private FastZipEvents events_;

        /// <summary>
        /// Defines the extractNameTransform_.
        /// </summary>
        private INameTransform extractNameTransform_;

        /// <summary>
        /// Defines the password_.
        /// </summary>
        private string password_;

        /// <summary>
        /// Initializes a new instance of the <see cref="FastZip"/> class.
        /// </summary>
        public FastZip()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FastZip"/> class.
        /// </summary>
        /// <param name="events">The events<see cref="FastZipEvents"/>.</param>
        public FastZip(FastZipEvents events)
        {
            this.events_ = events;
        }

        /// <summary>
        /// Gets or sets a value indicating whether CreateEmptyDirectories.
        /// </summary>
        public bool CreateEmptyDirectories
        {
            get
            {
                return this.createEmptyDirectories_;
            }
            set
            {
                this.createEmptyDirectories_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the Password.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password_;
            }
            set
            {
                this.password_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the NameTransform.
        /// </summary>
        public INameTransform NameTransform
        {
            get
            {
                return this.entryFactory_.NameTransform;
            }
            set
            {
                this.entryFactory_.NameTransform = value;
            }
        }

        /// <summary>
        /// Gets or sets the EntryFactory.
        /// </summary>
        public IEntryFactory EntryFactory
        {
            get
            {
                return this.entryFactory_;
            }
            set
            {
                if (value == null)
                    this.entryFactory_ = (IEntryFactory)new ZipEntryFactory();
                else
                    this.entryFactory_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the UseZip64.
        /// </summary>
        public UseZip64 UseZip64
        {
            get
            {
                return this.useZip64_;
            }
            set
            {
                this.useZip64_ = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether RestoreDateTimeOnExtract.
        /// </summary>
        public bool RestoreDateTimeOnExtract
        {
            get
            {
                return this.restoreDateTimeOnExtract_;
            }
            set
            {
                this.restoreDateTimeOnExtract_ = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether RestoreAttributesOnExtract.
        /// </summary>
        public bool RestoreAttributesOnExtract
        {
            get
            {
                return this.restoreAttributesOnExtract_;
            }
            set
            {
                this.restoreAttributesOnExtract_ = value;
            }
        }

        /// <summary>
        /// The CreateZip.
        /// </summary>
        /// <param name="zipFileName">The zipFileName<see cref="string"/>.</param>
        /// <param name="sourceDirectory">The sourceDirectory<see cref="string"/>.</param>
        /// <param name="recurse">The recurse<see cref="bool"/>.</param>
        /// <param name="fileFilter">The fileFilter<see cref="string"/>.</param>
        /// <param name="directoryFilter">The directoryFilter<see cref="string"/>.</param>
        public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
        {
            this.CreateZip((Stream)File.Create(zipFileName), sourceDirectory, recurse, fileFilter, directoryFilter);
        }

        /// <summary>
        /// The CreateZip.
        /// </summary>
        /// <param name="zipFileName">The zipFileName<see cref="string"/>.</param>
        /// <param name="sourceDirectory">The sourceDirectory<see cref="string"/>.</param>
        /// <param name="recurse">The recurse<see cref="bool"/>.</param>
        /// <param name="fileFilter">The fileFilter<see cref="string"/>.</param>
        public void CreateZip(string zipFileName, string sourceDirectory, bool recurse, string fileFilter)
        {
            this.CreateZip((Stream)File.Create(zipFileName), sourceDirectory, recurse, fileFilter, (string)null);
        }

        /// <summary>
        /// The CreateZip.
        /// </summary>
        /// <param name="outputStream">The outputStream<see cref="Stream"/>.</param>
        /// <param name="sourceDirectory">The sourceDirectory<see cref="string"/>.</param>
        /// <param name="recurse">The recurse<see cref="bool"/>.</param>
        /// <param name="fileFilter">The fileFilter<see cref="string"/>.</param>
        /// <param name="directoryFilter">The directoryFilter<see cref="string"/>.</param>
        public void CreateZip(Stream outputStream, string sourceDirectory, bool recurse, string fileFilter, string directoryFilter)
        {
            this.NameTransform = (INameTransform)new ZipNameTransform(sourceDirectory);
            this.sourceDirectory_ = sourceDirectory;
            using (this.outputStream_ = new ZipOutputStream(outputStream))
            {
                if (this.password_ != null)
                    this.outputStream_.Password = this.password_;
                this.outputStream_.UseZip64 = this.UseZip64;
                FileSystemScanner fileSystemScanner = new FileSystemScanner(fileFilter, directoryFilter);
                fileSystemScanner.ProcessFile += new ProcessFileHandler(this.ProcessFile);
                if (this.CreateEmptyDirectories)
                    fileSystemScanner.ProcessDirectory += new ProcessDirectoryHandler(this.ProcessDirectory);
                if (this.events_ != null)
                {
                    if (this.events_.FileFailure != null)
                        fileSystemScanner.FileFailure += this.events_.FileFailure;
                    if (this.events_.DirectoryFailure != null)
                        fileSystemScanner.DirectoryFailure += this.events_.DirectoryFailure;
                }
                fileSystemScanner.Scan(sourceDirectory, recurse);
            }
        }

        /// <summary>
        /// The ExtractZip.
        /// </summary>
        /// <param name="zipFileName">The zipFileName<see cref="string"/>.</param>
        /// <param name="targetDirectory">The targetDirectory<see cref="string"/>.</param>
        /// <param name="fileFilter">The fileFilter<see cref="string"/>.</param>
        public void ExtractZip(string zipFileName, string targetDirectory, string fileFilter)
        {
            this.ExtractZip(zipFileName, targetDirectory, FastZip.Overwrite.Always, (FastZip.ConfirmOverwriteDelegate)null, fileFilter, (string)null, this.restoreDateTimeOnExtract_);
        }

        /// <summary>
        /// The ExtractZip.
        /// </summary>
        /// <param name="zipFileName">The zipFileName<see cref="string"/>.</param>
        /// <param name="targetDirectory">The targetDirectory<see cref="string"/>.</param>
        /// <param name="overwrite">The overwrite<see cref="FastZip.Overwrite"/>.</param>
        /// <param name="confirmDelegate">The confirmDelegate<see cref="FastZip.ConfirmOverwriteDelegate"/>.</param>
        /// <param name="fileFilter">The fileFilter<see cref="string"/>.</param>
        /// <param name="directoryFilter">The directoryFilter<see cref="string"/>.</param>
        /// <param name="restoreDateTime">The restoreDateTime<see cref="bool"/>.</param>
        public void ExtractZip(string zipFileName, string targetDirectory, FastZip.Overwrite overwrite, FastZip.ConfirmOverwriteDelegate confirmDelegate, string fileFilter, string directoryFilter, bool restoreDateTime)
        {
            if (overwrite == FastZip.Overwrite.Prompt && confirmDelegate == null)
                throw new ArgumentNullException(nameof(confirmDelegate));
            this.continueRunning_ = true;
            this.overwrite_ = overwrite;
            this.confirmDelegate_ = confirmDelegate;
            this.extractNameTransform_ = (INameTransform)new WindowsNameTransform(targetDirectory);
            this.fileFilter_ = new NameFilter(fileFilter);
            this.directoryFilter_ = new NameFilter(directoryFilter);
            this.restoreDateTimeOnExtract_ = restoreDateTime;
            using (this.zipFile_ = new ZipFile(zipFileName))
            {
                if (this.password_ != null)
                    this.zipFile_.Password = this.password_;
                IEnumerator enumerator = this.zipFile_.GetEnumerator();
                while (this.continueRunning_ && enumerator.MoveNext())
                {
                    ZipEntry current = (ZipEntry)enumerator.Current;
                    if (current.IsFile)
                    {
                        if (this.directoryFilter_.IsMatch(Path.GetDirectoryName(current.Name)) && this.fileFilter_.IsMatch(current.Name))
                            this.ExtractEntry(current);
                    }
                    else if (current.IsDirectory && (this.directoryFilter_.IsMatch(current.Name) && this.CreateEmptyDirectories))
                        this.ExtractEntry(current);
                }
            }
        }

        /// <summary>
        /// The ProcessDirectory.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="DirectoryEventArgs"/>.</param>
        private void ProcessDirectory(object sender, DirectoryEventArgs e)
        {
            if (e.HasMatchingFiles || !this.CreateEmptyDirectories)
                return;
            if (this.events_ != null)
                this.events_.OnProcessDirectory(e.Name, e.HasMatchingFiles);
            if (e.ContinueRunning && e.Name != this.sourceDirectory_)
                this.outputStream_.PutNextEntry(this.entryFactory_.MakeDirectoryEntry(e.Name));
        }

        /// <summary>
        /// The ProcessFile.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="ScanEventArgs"/>.</param>
        private void ProcessFile(object sender, ScanEventArgs e)
        {
            if (this.events_ != null && this.events_.ProcessFile != null)
                this.events_.ProcessFile(sender, e);
            if (!e.ContinueRunning)
                return;
            using (FileStream fileStream = File.OpenRead(e.Name))
            {
                this.outputStream_.PutNextEntry(this.entryFactory_.MakeFileEntry(e.Name));
                this.AddFileContents(e.Name, (Stream)fileStream);
            }
        }

        /// <summary>
        /// The AddFileContents.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="stream">The stream<see cref="Stream"/>.</param>
        private void AddFileContents(string name, Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            if (this.buffer_ == null)
                this.buffer_ = new byte[4096];
            if (this.events_ != null && this.events_.Progress != null)
                StreamUtils.Copy(stream, (Stream)this.outputStream_, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, (object)this, name);
            else
                StreamUtils.Copy(stream, (Stream)this.outputStream_, this.buffer_);
            if (this.events_ == null)
                return;
            this.continueRunning_ = this.events_.OnCompletedFile(name);
        }

        /// <summary>
        /// The ExtractFileEntry.
        /// </summary>
        /// <param name="entry">The entry<see cref="ZipEntry"/>.</param>
        /// <param name="targetName">The targetName<see cref="string"/>.</param>
        private void ExtractFileEntry(ZipEntry entry, string targetName)
        {
            bool flag = true;
            if (this.overwrite_ != FastZip.Overwrite.Always && File.Exists(targetName))
                flag = this.overwrite_ == FastZip.Overwrite.Prompt && this.confirmDelegate_ != null && this.confirmDelegate_(targetName);
            if (!flag)
                return;
            if (this.events_ != null)
                this.continueRunning_ = this.events_.OnProcessFile(entry.Name);
            if (this.continueRunning_)
            {
                try
                {
                    using (FileStream fileStream = File.Create(targetName))
                    {
                        if (this.buffer_ == null)
                            this.buffer_ = new byte[4096];
                        if (this.events_ != null && this.events_.Progress != null)
                            StreamUtils.Copy(this.zipFile_.GetInputStream(entry), (Stream)fileStream, this.buffer_, this.events_.Progress, this.events_.ProgressInterval, (object)this, entry.Name, entry.Size);
                        else
                            StreamUtils.Copy(this.zipFile_.GetInputStream(entry), (Stream)fileStream, this.buffer_);
                        if (this.events_ != null)
                            this.continueRunning_ = this.events_.OnCompletedFile(entry.Name);
                    }
                    if (this.restoreDateTimeOnExtract_)
                        File.SetLastWriteTime(targetName, entry.DateTime);
                    if (this.RestoreAttributesOnExtract && entry.IsDOSEntry && entry.ExternalFileAttributes != -1)
                    {
                        FileAttributes fileAttributes = (FileAttributes)(entry.ExternalFileAttributes & 163);
                        File.SetAttributes(targetName, fileAttributes);
                    }
                }
                catch (Exception ex)
                {
                    if (this.events_ != null)
                    {
                        this.continueRunning_ = this.events_.OnFileFailure(targetName, ex);
                    }
                    else
                    {
                        this.continueRunning_ = false;
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// The ExtractEntry.
        /// </summary>
        /// <param name="entry">The entry<see cref="ZipEntry"/>.</param>
        private void ExtractEntry(ZipEntry entry)
        {
            bool flag = entry.IsCompressionMethodSupported();
            string str = entry.Name;
            if (flag)
            {
                if (entry.IsFile)
                    str = this.extractNameTransform_.TransformFile(str);
                else if (entry.IsDirectory)
                    str = this.extractNameTransform_.TransformDirectory(str);
                flag = str != null && str.Length != 0;
            }
            string path = (string)null;
            if (flag)
                path = !entry.IsDirectory ? Path.GetDirectoryName(Path.GetFullPath(str)) : str;
            if (flag && !Directory.Exists(path))
            {
                if (!entry.IsDirectory || this.CreateEmptyDirectories)
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                    }
                    catch (Exception ex)
                    {
                        flag = false;
                        if (this.events_ != null)
                        {
                            this.continueRunning_ = !entry.IsDirectory ? this.events_.OnFileFailure(str, ex) : this.events_.OnDirectoryFailure(str, ex);
                        }
                        else
                        {
                            this.continueRunning_ = false;
                            throw;
                        }
                    }
                }
            }
            if (!flag || !entry.IsFile)
                return;
            this.ExtractFileEntry(entry, str);
        }

        /// <summary>
        /// The MakeExternalAttributes.
        /// </summary>
        /// <param name="info">The info<see cref="FileInfo"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private static int MakeExternalAttributes(FileInfo info)
        {
            return (int)info.Attributes;
        }

        /// <summary>
        /// The NameIsValid.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private static bool NameIsValid(string name)
        {
            return name != null && name.Length > 0 && name.IndexOfAny(Path.GetInvalidPathChars()) < 0;
        }

        /// <summary>
        /// Defines the Overwrite.
        /// </summary>
        public enum Overwrite
        {
            /// <summary>
            /// Defines the Prompt.
            /// </summary>
            Prompt,

            /// <summary>
            /// Defines the Never.
            /// </summary>
            Never,

            /// <summary>
            /// Defines the Always.
            /// </summary>
            Always,
        }

        /// <summary>
        /// The ConfirmOverwriteDelegate.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public delegate bool ConfirmOverwriteDelegate(string fileName);
    }
}
