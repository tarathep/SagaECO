namespace ICSharpCode.SharpZipLib.Zip
{
    using ICSharpCode.SharpZipLib.Core;
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ZipEntryFactory" />.
    /// </summary>
    public class ZipEntryFactory : IEntryFactory
    {
        /// <summary>
        /// Defines the fixedDateTime_.
        /// </summary>
        private DateTime fixedDateTime_ = DateTime.Now;

        /// <summary>
        /// Defines the getAttributes_.
        /// </summary>
        private int getAttributes_ = -1;

        /// <summary>
        /// Defines the nameTransform_.
        /// </summary>
        private INameTransform nameTransform_;

        /// <summary>
        /// Defines the timeSetting_.
        /// </summary>
        private ZipEntryFactory.TimeSetting timeSetting_;

        /// <summary>
        /// Defines the isUnicodeText_.
        /// </summary>
        private bool isUnicodeText_;

        /// <summary>
        /// Defines the setAttributes_.
        /// </summary>
        private int setAttributes_;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipEntryFactory"/> class.
        /// </summary>
        public ZipEntryFactory()
        {
            this.nameTransform_ = (INameTransform)new ZipNameTransform();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipEntryFactory"/> class.
        /// </summary>
        /// <param name="timeSetting">The timeSetting<see cref="ZipEntryFactory.TimeSetting"/>.</param>
        public ZipEntryFactory(ZipEntryFactory.TimeSetting timeSetting)
        {
            this.timeSetting_ = timeSetting;
            this.nameTransform_ = (INameTransform)new ZipNameTransform();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipEntryFactory"/> class.
        /// </summary>
        /// <param name="time">The time<see cref="DateTime"/>.</param>
        public ZipEntryFactory(DateTime time)
        {
            this.timeSetting_ = ZipEntryFactory.TimeSetting.Fixed;
            this.FixedDateTime = time;
            this.nameTransform_ = (INameTransform)new ZipNameTransform();
        }

        /// <summary>
        /// Gets or sets the NameTransform.
        /// </summary>
        public INameTransform NameTransform
        {
            get
            {
                return this.nameTransform_;
            }
            set
            {
                if (value == null)
                    this.nameTransform_ = (INameTransform)new ZipNameTransform();
                else
                    this.nameTransform_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the Setting.
        /// </summary>
        public ZipEntryFactory.TimeSetting Setting
        {
            get
            {
                return this.timeSetting_;
            }
            set
            {
                this.timeSetting_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the FixedDateTime.
        /// </summary>
        public DateTime FixedDateTime
        {
            get
            {
                return this.fixedDateTime_;
            }
            set
            {
                if (value.Year < 1970)
                    throw new ArgumentException("Value is too old to be valid", nameof(value));
                this.fixedDateTime_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the GetAttributes.
        /// </summary>
        public int GetAttributes
        {
            get
            {
                return this.getAttributes_;
            }
            set
            {
                this.getAttributes_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the SetAttributes.
        /// </summary>
        public int SetAttributes
        {
            get
            {
                return this.setAttributes_;
            }
            set
            {
                this.setAttributes_ = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsUnicodeText.
        /// </summary>
        public bool IsUnicodeText
        {
            get
            {
                return this.isUnicodeText_;
            }
            set
            {
                this.isUnicodeText_ = value;
            }
        }

        /// <summary>
        /// The MakeFileEntry.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="ZipEntry"/>.</returns>
        public ZipEntry MakeFileEntry(string fileName)
        {
            return this.MakeFileEntry(fileName, true);
        }

        /// <summary>
        /// The MakeFileEntry.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <param name="useFileSystem">The useFileSystem<see cref="bool"/>.</param>
        /// <returns>The <see cref="ZipEntry"/>.</returns>
        public ZipEntry MakeFileEntry(string fileName, bool useFileSystem)
        {
            ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformFile(fileName));
            zipEntry.IsUnicodeText = this.isUnicodeText_;
            int num1 = 0;
            bool flag = this.setAttributes_ != 0;
            FileInfo fileInfo = (FileInfo)null;
            if (useFileSystem)
                fileInfo = new FileInfo(fileName);
            if (fileInfo != null && fileInfo.Exists)
            {
                switch (this.timeSetting_)
                {
                    case ZipEntryFactory.TimeSetting.LastWriteTime:
                        zipEntry.DateTime = fileInfo.LastWriteTime;
                        break;
                    case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
                        zipEntry.DateTime = fileInfo.LastWriteTimeUtc;
                        break;
                    case ZipEntryFactory.TimeSetting.CreateTime:
                        zipEntry.DateTime = fileInfo.CreationTime;
                        break;
                    case ZipEntryFactory.TimeSetting.CreateTimeUtc:
                        zipEntry.DateTime = fileInfo.CreationTimeUtc;
                        break;
                    case ZipEntryFactory.TimeSetting.LastAccessTime:
                        zipEntry.DateTime = fileInfo.LastAccessTime;
                        break;
                    case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
                        zipEntry.DateTime = fileInfo.LastAccessTimeUtc;
                        break;
                    case ZipEntryFactory.TimeSetting.Fixed:
                        zipEntry.DateTime = this.fixedDateTime_;
                        break;
                    default:
                        throw new ZipException("Unhandled time setting in MakeFileEntry");
                }
                zipEntry.Size = fileInfo.Length;
                flag = true;
                num1 = (int)(fileInfo.Attributes & (FileAttributes)this.getAttributes_);
            }
            else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
                zipEntry.DateTime = this.fixedDateTime_;
            if (flag)
            {
                int num2 = num1 | this.setAttributes_;
                zipEntry.ExternalFileAttributes = num2;
            }
            return zipEntry;
        }

        /// <summary>
        /// The MakeDirectoryEntry.
        /// </summary>
        /// <param name="directoryName">The directoryName<see cref="string"/>.</param>
        /// <returns>The <see cref="ZipEntry"/>.</returns>
        public ZipEntry MakeDirectoryEntry(string directoryName)
        {
            return this.MakeDirectoryEntry(directoryName, true);
        }

        /// <summary>
        /// The MakeDirectoryEntry.
        /// </summary>
        /// <param name="directoryName">The directoryName<see cref="string"/>.</param>
        /// <param name="useFileSystem">The useFileSystem<see cref="bool"/>.</param>
        /// <returns>The <see cref="ZipEntry"/>.</returns>
        public ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem)
        {
            ZipEntry zipEntry = new ZipEntry(this.nameTransform_.TransformDirectory(directoryName));
            zipEntry.Size = 0L;
            int num1 = 0;
            DirectoryInfo directoryInfo = (DirectoryInfo)null;
            if (useFileSystem)
                directoryInfo = new DirectoryInfo(directoryName);
            if (directoryInfo != null && directoryInfo.Exists)
            {
                switch (this.timeSetting_)
                {
                    case ZipEntryFactory.TimeSetting.LastWriteTime:
                        zipEntry.DateTime = directoryInfo.LastWriteTime;
                        break;
                    case ZipEntryFactory.TimeSetting.LastWriteTimeUtc:
                        zipEntry.DateTime = directoryInfo.LastWriteTimeUtc;
                        break;
                    case ZipEntryFactory.TimeSetting.CreateTime:
                        zipEntry.DateTime = directoryInfo.CreationTime;
                        break;
                    case ZipEntryFactory.TimeSetting.CreateTimeUtc:
                        zipEntry.DateTime = directoryInfo.CreationTimeUtc;
                        break;
                    case ZipEntryFactory.TimeSetting.LastAccessTime:
                        zipEntry.DateTime = directoryInfo.LastAccessTime;
                        break;
                    case ZipEntryFactory.TimeSetting.LastAccessTimeUtc:
                        zipEntry.DateTime = directoryInfo.LastAccessTimeUtc;
                        break;
                    case ZipEntryFactory.TimeSetting.Fixed:
                        zipEntry.DateTime = this.fixedDateTime_;
                        break;
                    default:
                        throw new ZipException("Unhandled time setting in MakeDirectoryEntry");
                }
                num1 = (int)(directoryInfo.Attributes & (FileAttributes)this.getAttributes_);
            }
            else if (this.timeSetting_ == ZipEntryFactory.TimeSetting.Fixed)
                zipEntry.DateTime = this.fixedDateTime_;
            int num2 = num1 | (this.setAttributes_ | 16);
            zipEntry.ExternalFileAttributes = num2;
            return zipEntry;
        }

        /// <summary>
        /// Defines the TimeSetting.
        /// </summary>
        public enum TimeSetting
        {
            /// <summary>
            /// Defines the LastWriteTime.
            /// </summary>
            LastWriteTime,

            /// <summary>
            /// Defines the LastWriteTimeUtc.
            /// </summary>
            LastWriteTimeUtc,

            /// <summary>
            /// Defines the CreateTime.
            /// </summary>
            CreateTime,

            /// <summary>
            /// Defines the CreateTimeUtc.
            /// </summary>
            CreateTimeUtc,

            /// <summary>
            /// Defines the LastAccessTime.
            /// </summary>
            LastAccessTime,

            /// <summary>
            /// Defines the LastAccessTimeUtc.
            /// </summary>
            LastAccessTimeUtc,

            /// <summary>
            /// Defines the Fixed.
            /// </summary>
            Fixed,
        }
    }
}
