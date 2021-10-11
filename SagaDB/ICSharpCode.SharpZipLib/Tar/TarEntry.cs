namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="TarEntry" />.
    /// </summary>
    public class TarEntry : ICloneable
    {
        /// <summary>
        /// Defines the file.
        /// </summary>
        private string file;

        /// <summary>
        /// Defines the header.
        /// </summary>
        private TarHeader header;

        /// <summary>
        /// Prevents a default instance of the <see cref="TarEntry"/> class from being created.
        /// </summary>
        private TarEntry()
        {
            this.header = new TarHeader();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TarEntry"/> class.
        /// </summary>
        /// <param name="headerBuffer">The headerBuffer<see cref="byte[]"/>.</param>
        public TarEntry(byte[] headerBuffer)
        {
            this.header = new TarHeader();
            this.header.ParseBuffer(headerBuffer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TarEntry"/> class.
        /// </summary>
        /// <param name="header">The header<see cref="TarHeader"/>.</param>
        public TarEntry(TarHeader header)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header));
            this.header = (TarHeader)header.Clone();
        }

        /// <summary>
        /// The Clone.
        /// </summary>
        /// <returns>The <see cref="object"/>.</returns>
        public object Clone()
        {
            return (object)new TarEntry()
            {
                file = this.file,
                header = (TarHeader)this.header.Clone(),
                Name = this.Name
            };
        }

        /// <summary>
        /// The CreateTarEntry.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="TarEntry"/>.</returns>
        public static TarEntry CreateTarEntry(string name)
        {
            TarEntry tarEntry = new TarEntry();
            TarEntry.NameTarHeader(tarEntry.header, name);
            return tarEntry;
        }

        /// <summary>
        /// The CreateEntryFromFile.
        /// </summary>
        /// <param name="fileName">The fileName<see cref="string"/>.</param>
        /// <returns>The <see cref="TarEntry"/>.</returns>
        public static TarEntry CreateEntryFromFile(string fileName)
        {
            TarEntry tarEntry = new TarEntry();
            tarEntry.GetFileTarHeader(tarEntry.header, fileName);
            return tarEntry;
        }

        /// <summary>
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            TarEntry tarEntry = obj as TarEntry;
            if (tarEntry != null)
                return this.Name.Equals(tarEntry.Name);
            return false;
        }

        /// <summary>
        /// The GetHashCode.
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }

        /// <summary>
        /// The IsDescendent.
        /// </summary>
        /// <param name="toTest">The toTest<see cref="TarEntry"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsDescendent(TarEntry toTest)
        {
            if (toTest == null)
                throw new ArgumentNullException(nameof(toTest));
            return toTest.Name.StartsWith(this.Name);
        }

        /// <summary>
        /// Gets the TarHeader.
        /// </summary>
        public TarHeader TarHeader
        {
            get
            {
                return this.header;
            }
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.header.Name;
            }
            set
            {
                this.header.Name = value;
            }
        }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        public int UserId
        {
            get
            {
                return this.header.UserId;
            }
            set
            {
                this.header.UserId = value;
            }
        }

        /// <summary>
        /// Gets or sets the GroupId.
        /// </summary>
        public int GroupId
        {
            get
            {
                return this.header.GroupId;
            }
            set
            {
                this.header.GroupId = value;
            }
        }

        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        public string UserName
        {
            get
            {
                return this.header.UserName;
            }
            set
            {
                this.header.UserName = value;
            }
        }

        /// <summary>
        /// Gets or sets the GroupName.
        /// </summary>
        public string GroupName
        {
            get
            {
                return this.header.GroupName;
            }
            set
            {
                this.header.GroupName = value;
            }
        }

        /// <summary>
        /// The SetIds.
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/>.</param>
        /// <param name="groupId">The groupId<see cref="int"/>.</param>
        public void SetIds(int userId, int groupId)
        {
            this.UserId = userId;
            this.GroupId = groupId;
        }

        /// <summary>
        /// The SetNames.
        /// </summary>
        /// <param name="userName">The userName<see cref="string"/>.</param>
        /// <param name="groupName">The groupName<see cref="string"/>.</param>
        public void SetNames(string userName, string groupName)
        {
            this.UserName = userName;
            this.GroupName = groupName;
        }

        /// <summary>
        /// Gets or sets the ModTime.
        /// </summary>
        public DateTime ModTime
        {
            get
            {
                return this.header.ModTime;
            }
            set
            {
                this.header.ModTime = value;
            }
        }

        /// <summary>
        /// Gets the File.
        /// </summary>
        public string File
        {
            get
            {
                return this.file;
            }
        }

        /// <summary>
        /// Gets or sets the Size.
        /// </summary>
        public long Size
        {
            get
            {
                return this.header.Size;
            }
            set
            {
                this.header.Size = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsDirectory.
        /// </summary>
        public bool IsDirectory
        {
            get
            {
                if (this.file != null)
                    return Directory.Exists(this.file);
                return this.header != null && (this.header.TypeFlag == (byte)53 || this.Name.EndsWith("/"));
            }
        }

        /// <summary>
        /// The GetFileTarHeader.
        /// </summary>
        /// <param name="header">The header<see cref="TarHeader"/>.</param>
        /// <param name="file">The file<see cref="string"/>.</param>
        public void GetFileTarHeader(TarHeader header, string file)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header));
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            this.file = file;
            string str1 = file;
            if (str1.IndexOf(Environment.CurrentDirectory) == 0)
                str1 = str1.Substring(Environment.CurrentDirectory.Length);
            string str2 = str1.Replace(Path.DirectorySeparatorChar, '/');
            while (str2.StartsWith("/"))
                str2 = str2.Substring(1);
            header.LinkName = string.Empty;
            header.Name = str2;
            if (Directory.Exists(file))
            {
                header.Mode = 1003;
                header.TypeFlag = (byte)53;
                if (header.Name.Length == 0 || header.Name[header.Name.Length - 1] != '/')
                    header.Name += "/";
                header.Size = 0L;
            }
            else
            {
                header.Mode = 33216;
                header.TypeFlag = (byte)48;
                header.Size = new FileInfo(file.Replace('/', Path.DirectorySeparatorChar)).Length;
            }
            header.ModTime = System.IO.File.GetLastWriteTime(file.Replace('/', Path.DirectorySeparatorChar)).ToUniversalTime();
            header.DevMajor = 0;
            header.DevMinor = 0;
        }

        /// <summary>
        /// The GetDirectoryEntries.
        /// </summary>
        /// <returns>The <see cref="TarEntry[]"/>.</returns>
        public TarEntry[] GetDirectoryEntries()
        {
            if (this.file == null || !Directory.Exists(this.file))
                return new TarEntry[0];
            string[] fileSystemEntries = Directory.GetFileSystemEntries(this.file);
            TarEntry[] tarEntryArray = new TarEntry[fileSystemEntries.Length];
            for (int index = 0; index < fileSystemEntries.Length; ++index)
                tarEntryArray[index] = TarEntry.CreateEntryFromFile(fileSystemEntries[index]);
            return tarEntryArray;
        }

        /// <summary>
        /// The WriteEntryHeader.
        /// </summary>
        /// <param name="outBuffer">The outBuffer<see cref="byte[]"/>.</param>
        public void WriteEntryHeader(byte[] outBuffer)
        {
            this.header.WriteHeader(outBuffer);
        }

        /// <summary>
        /// The AdjustEntryName.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="newName">The newName<see cref="string"/>.</param>
        public static void AdjustEntryName(byte[] buffer, string newName)
        {
            int offset = 0;
            TarHeader.GetNameBytes(newName, buffer, offset, 100);
        }

        /// <summary>
        /// The NameTarHeader.
        /// </summary>
        /// <param name="header">The header<see cref="TarHeader"/>.</param>
        /// <param name="name">The name<see cref="string"/>.</param>
        public static void NameTarHeader(TarHeader header, string name)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header));
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            bool flag = name.EndsWith("/");
            header.Name = name;
            header.Mode = flag ? 1003 : 33216;
            header.UserId = 0;
            header.GroupId = 0;
            header.Size = 0L;
            header.ModTime = DateTime.UtcNow;
            header.TypeFlag = flag ? (byte)53 : (byte)48;
            header.LinkName = string.Empty;
            header.UserName = string.Empty;
            header.GroupName = string.Empty;
            header.DevMajor = 0;
            header.DevMinor = 0;
        }
    }
}
