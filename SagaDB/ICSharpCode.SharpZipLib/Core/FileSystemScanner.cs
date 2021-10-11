namespace ICSharpCode.SharpZipLib.Core
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="FileSystemScanner" />.
    /// </summary>
    public class FileSystemScanner
    {
        /// <summary>
        /// Defines the ProcessDirectory.
        /// </summary>
        public ProcessDirectoryHandler ProcessDirectory;

        /// <summary>
        /// Defines the ProcessFile.
        /// </summary>
        public ProcessFileHandler ProcessFile;

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
        /// Defines the fileFilter_.
        /// </summary>
        private IScanFilter fileFilter_;

        /// <summary>
        /// Defines the directoryFilter_.
        /// </summary>
        private IScanFilter directoryFilter_;

        /// <summary>
        /// Defines the alive_.
        /// </summary>
        private bool alive_;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemScanner"/> class.
        /// </summary>
        /// <param name="filter">The filter<see cref="string"/>.</param>
        public FileSystemScanner(string filter)
        {
            this.fileFilter_ = (IScanFilter)new PathFilter(filter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemScanner"/> class.
        /// </summary>
        /// <param name="fileFilter">The fileFilter<see cref="string"/>.</param>
        /// <param name="directoryFilter">The directoryFilter<see cref="string"/>.</param>
        public FileSystemScanner(string fileFilter, string directoryFilter)
        {
            this.fileFilter_ = (IScanFilter)new PathFilter(fileFilter);
            this.directoryFilter_ = (IScanFilter)new PathFilter(directoryFilter);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemScanner"/> class.
        /// </summary>
        /// <param name="fileFilter">The fileFilter<see cref="IScanFilter"/>.</param>
        public FileSystemScanner(IScanFilter fileFilter)
        {
            this.fileFilter_ = fileFilter;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileSystemScanner"/> class.
        /// </summary>
        /// <param name="fileFilter">The fileFilter<see cref="IScanFilter"/>.</param>
        /// <param name="directoryFilter">The directoryFilter<see cref="IScanFilter"/>.</param>
        public FileSystemScanner(IScanFilter fileFilter, IScanFilter directoryFilter)
        {
            this.fileFilter_ = fileFilter;
            this.directoryFilter_ = directoryFilter;
        }

        /// <summary>
        /// The OnDirectoryFailure.
        /// </summary>
        /// <param name="directory">The directory<see cref="string"/>.</param>
        /// <param name="e">The e<see cref="Exception"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool OnDirectoryFailure(string directory, Exception e)
        {
            DirectoryFailureHandler directoryFailure = this.DirectoryFailure;
            bool flag = directoryFailure != null;
            if (flag)
            {
                ScanFailureEventArgs e1 = new ScanFailureEventArgs(directory, e);
                directoryFailure((object)this, e1);
                this.alive_ = e1.ContinueRunning;
            }
            return flag;
        }

        /// <summary>
        /// The OnFileFailure.
        /// </summary>
        /// <param name="file">The file<see cref="string"/>.</param>
        /// <param name="e">The e<see cref="Exception"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool OnFileFailure(string file, Exception e)
        {
            bool flag = this.FileFailure != null;
            if (flag)
            {
                ScanFailureEventArgs e1 = new ScanFailureEventArgs(file, e);
                this.FileFailure((object)this, e1);
                this.alive_ = e1.ContinueRunning;
            }
            return flag;
        }

        /// <summary>
        /// The OnProcessFile.
        /// </summary>
        /// <param name="file">The file<see cref="string"/>.</param>
        private void OnProcessFile(string file)
        {
            ProcessFileHandler processFile = this.ProcessFile;
            if (processFile == null)
                return;
            ScanEventArgs e = new ScanEventArgs(file);
            processFile((object)this, e);
            this.alive_ = e.ContinueRunning;
        }

        /// <summary>
        /// The OnCompleteFile.
        /// </summary>
        /// <param name="file">The file<see cref="string"/>.</param>
        private void OnCompleteFile(string file)
        {
            CompletedFileHandler completedFile = this.CompletedFile;
            if (completedFile == null)
                return;
            ScanEventArgs e = new ScanEventArgs(file);
            completedFile((object)this, e);
            this.alive_ = e.ContinueRunning;
        }

        /// <summary>
        /// The OnProcessDirectory.
        /// </summary>
        /// <param name="directory">The directory<see cref="string"/>.</param>
        /// <param name="hasMatchingFiles">The hasMatchingFiles<see cref="bool"/>.</param>
        private void OnProcessDirectory(string directory, bool hasMatchingFiles)
        {
            ProcessDirectoryHandler processDirectory = this.ProcessDirectory;
            if (processDirectory == null)
                return;
            DirectoryEventArgs e = new DirectoryEventArgs(directory, hasMatchingFiles);
            processDirectory((object)this, e);
            this.alive_ = e.ContinueRunning;
        }

        /// <summary>
        /// The Scan.
        /// </summary>
        /// <param name="directory">The directory<see cref="string"/>.</param>
        /// <param name="recurse">The recurse<see cref="bool"/>.</param>
        public void Scan(string directory, bool recurse)
        {
            this.alive_ = true;
            this.ScanDir(directory, recurse);
        }

        /// <summary>
        /// The ScanDir.
        /// </summary>
        /// <param name="directory">The directory<see cref="string"/>.</param>
        /// <param name="recurse">The recurse<see cref="bool"/>.</param>
        private void ScanDir(string directory, bool recurse)
        {
            try
            {
                string[] files = Directory.GetFiles(directory);
                bool hasMatchingFiles = false;
                for (int index = 0; index < files.Length; ++index)
                {
                    if (!this.fileFilter_.IsMatch(files[index]))
                        files[index] = (string)null;
                    else
                        hasMatchingFiles = true;
                }
                this.OnProcessDirectory(directory, hasMatchingFiles);
                if (this.alive_ && hasMatchingFiles)
                {
                    foreach (string file in files)
                    {
                        try
                        {
                            if (file != null)
                            {
                                this.OnProcessFile(file);
                                if (!this.alive_)
                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            if (!this.OnFileFailure(file, ex))
                                throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this.OnDirectoryFailure(directory, ex))
                    throw;
            }
            if (!this.alive_ || !recurse)
                return;
            try
            {
                foreach (string directory1 in Directory.GetDirectories(directory))
                {
                    if (this.directoryFilter_ == null || this.directoryFilter_.IsMatch(directory1))
                    {
                        this.ScanDir(directory1, true);
                        if (!this.alive_)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (!this.OnDirectoryFailure(directory, ex))
                    throw;
            }
        }
    }
}
