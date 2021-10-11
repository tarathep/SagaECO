namespace ICSharpCode.SharpZipLib.Zip
{
    using ICSharpCode.SharpZipLib.Core;
    using System;

    /// <summary>
    /// Defines the <see cref="FastZipEvents" />.
    /// </summary>
    public class FastZipEvents
    {
        /// <summary>
        /// Defines the progressInterval_.
        /// </summary>
        private TimeSpan progressInterval_ = TimeSpan.FromSeconds(3.0);

        /// <summary>
        /// Defines the ProcessDirectory.
        /// </summary>
        public ProcessDirectoryHandler ProcessDirectory;

        /// <summary>
        /// Defines the ProcessFile.
        /// </summary>
        public ProcessFileHandler ProcessFile;

        /// <summary>
        /// Defines the Progress.
        /// </summary>
        public ProgressHandler Progress;

        /// <summary>
        /// Defines the CompletedFile.
        /// </summary>
        public CompletedFileHandler CompletedFile;

        /// <summary>
        /// Defines the DirectoryFailure.
        /// </summary>
        public DirectoryFailureHandler DirectoryFailure;

        /// <summary>
        /// Defines the FileFailure.
        /// </summary>
        public FileFailureHandler FileFailure;

        /// <summary>
        /// The OnDirectoryFailure.
        /// </summary>
        /// <param name="directory">The directory<see cref="string"/>.</param>
        /// <param name="e">The e<see cref="Exception"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool OnDirectoryFailure(string directory, Exception e)
        {
            bool flag = false;
            DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
            if (directoryFailure != null)
            {
                ScanFailureEventArgs e1 = new ScanFailureEventArgs(directory, e);
                directoryFailure((object)this, e1);
                flag = e1.ContinueRunning;
            }
            return flag;
        }

        /// <summary>
        /// The OnFileFailure.
        /// </summary>
        /// <param name="file">The file<see cref="string"/>.</param>
        /// <param name="e">The e<see cref="Exception"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool OnFileFailure(string file, Exception e)
        {
            FileFailureHandler fileFailure = this.FileFailure;
            bool flag = fileFailure != null;
            if (flag)
            {
                ScanFailureEventArgs e1 = new ScanFailureEventArgs(file, e);
                fileFailure((object)this, e1);
                flag = e1.ContinueRunning;
            }
            return flag;
        }

        /// <summary>
        /// The OnProcessFile.
        /// </summary>
        /// <param name="file">The file<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool OnProcessFile(string file)
        {
            bool flag = true;
            if (this.ProcessFile != null)
            {
                ScanEventArgs e = new ScanEventArgs(file);
                this.ProcessFile((object)this, e);
                flag = e.ContinueRunning;
            }
            return flag;
        }

        /// <summary>
        /// The OnCompletedFile.
        /// </summary>
        /// <param name="file">The file<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool OnCompletedFile(string file)
        {
            bool flag = true;
            CompletedFileHandler completedFile = this.CompletedFile;
            if (completedFile != null)
            {
                ScanEventArgs e = new ScanEventArgs(file);
                completedFile((object)this, e);
                flag = e.ContinueRunning;
            }
            return flag;
        }

        /// <summary>
        /// The OnProcessDirectory.
        /// </summary>
        /// <param name="directory">The directory<see cref="string"/>.</param>
        /// <param name="hasMatchingFiles">The hasMatchingFiles<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool OnProcessDirectory(string directory, bool hasMatchingFiles)
        {
            bool flag = true;
            ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
            if (processDirectory != null)
            {
                DirectoryEventArgs e = new DirectoryEventArgs(directory, hasMatchingFiles);
                processDirectory((object)this, e);
                flag = e.ContinueRunning;
            }
            return flag;
        }

        /// <summary>
        /// Gets or sets the ProgressInterval.
        /// </summary>
        public TimeSpan ProgressInterval
        {
            get
            {
                return this.progressInterval_;
            }
            set
            {
                this.progressInterval_ = value;
            }
        }
    }
}
