namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ZipEntry" />.
    /// </summary>
    public class ZipEntry : ICloneable
    {
        /// <summary>
        /// Defines the externalFileAttributes.
        /// </summary>
        private int externalFileAttributes = -1;

        /// <summary>
        /// Defines the method.
        /// </summary>
        private CompressionMethod method = CompressionMethod.Deflated;

        /// <summary>
        /// Defines the zipFileIndex.
        /// </summary>
        private long zipFileIndex = -1;

        /// <summary>
        /// Defines the known.
        /// </summary>
        private ZipEntry.Known known;

        /// <summary>
        /// Defines the versionMadeBy.
        /// </summary>
        private ushort versionMadeBy;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the size.
        /// </summary>
        private ulong size;

        /// <summary>
        /// Defines the compressedSize.
        /// </summary>
        private ulong compressedSize;

        /// <summary>
        /// Defines the versionToExtract.
        /// </summary>
        private ushort versionToExtract;

        /// <summary>
        /// Defines the crc.
        /// </summary>
        private uint crc;

        /// <summary>
        /// Defines the dosTime.
        /// </summary>
        private uint dosTime;

        /// <summary>
        /// Defines the extra.
        /// </summary>
        private byte[] extra;

        /// <summary>
        /// Defines the comment.
        /// </summary>
        private string comment;

        /// <summary>
        /// Defines the flags.
        /// </summary>
        private int flags;

        /// <summary>
        /// Defines the offset.
        /// </summary>
        private long offset;

        /// <summary>
        /// Defines the forceZip64_.
        /// </summary>
        private bool forceZip64_;

        /// <summary>
        /// Defines the cryptoCheckValue_.
        /// </summary>
        private byte cryptoCheckValue_;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipEntry"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        public ZipEntry(string name)
      : this(name, 0, 45, CompressionMethod.Deflated)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipEntry"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="versionRequiredToExtract">The versionRequiredToExtract<see cref="int"/>.</param>
        internal ZipEntry(string name, int versionRequiredToExtract)
      : this(name, versionRequiredToExtract, 45, CompressionMethod.Deflated)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipEntry"/> class.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="versionRequiredToExtract">The versionRequiredToExtract<see cref="int"/>.</param>
        /// <param name="madeByInfo">The madeByInfo<see cref="int"/>.</param>
        /// <param name="method">The method<see cref="CompressionMethod"/>.</param>
        internal ZipEntry(string name, int versionRequiredToExtract, int madeByInfo, CompressionMethod method)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (name.Length > (int)ushort.MaxValue)
                throw new ArgumentException("Name is too long", nameof(name));
            if (versionRequiredToExtract != 0 && versionRequiredToExtract < 10)
                throw new ArgumentOutOfRangeException(nameof(versionRequiredToExtract));
            this.DateTime = DateTime.Now;
            this.name = name;
            this.versionMadeBy = (ushort)madeByInfo;
            this.versionToExtract = (ushort)versionRequiredToExtract;
            this.method = method;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipEntry"/> class.
        /// </summary>
        /// <param name="entry">The entry<see cref="ZipEntry"/>.</param>
        [Obsolete("Use Clone instead")]
        public ZipEntry(ZipEntry entry)
        {
            if (entry == null)
                throw new ArgumentNullException(nameof(entry));
            this.known = entry.known;
            this.name = entry.name;
            this.size = entry.size;
            this.compressedSize = entry.compressedSize;
            this.crc = entry.crc;
            this.dosTime = entry.dosTime;
            this.method = entry.method;
            this.comment = entry.comment;
            this.versionToExtract = entry.versionToExtract;
            this.versionMadeBy = entry.versionMadeBy;
            this.externalFileAttributes = entry.externalFileAttributes;
            this.flags = entry.flags;
            this.zipFileIndex = entry.zipFileIndex;
            this.offset = entry.offset;
            this.forceZip64_ = entry.forceZip64_;
            if (entry.extra == null)
                return;
            this.extra = new byte[entry.extra.Length];
            Array.Copy((Array)entry.extra, 0, (Array)this.extra, 0, entry.extra.Length);
        }

        /// <summary>
        /// Gets a value indicating whether HasCrc.
        /// </summary>
        public bool HasCrc
        {
            get
            {
                return (this.known & ZipEntry.Known.Crc) != ZipEntry.Known.None;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsCrypted.
        /// </summary>
        public bool IsCrypted
        {
            get
            {
                return (this.flags & 1) != 0;
            }
            set
            {
                if (value)
                    this.flags |= 1;
                else
                    this.flags &= -2;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsUnicodeText.
        /// </summary>
        public bool IsUnicodeText
        {
            get
            {
                return (this.flags & 2048) != 0;
            }
            set
            {
                if (value)
                    this.flags |= 2048;
                else
                    this.flags &= -2049;
            }
        }

        /// <summary>
        /// Gets or sets the CryptoCheckValue.
        /// </summary>
        internal byte CryptoCheckValue
        {
            get
            {
                return this.cryptoCheckValue_;
            }
            set
            {
                this.cryptoCheckValue_ = value;
            }
        }

        /// <summary>
        /// Gets or sets the Flags.
        /// </summary>
        public int Flags
        {
            get
            {
                return this.flags;
            }
            set
            {
                this.flags = value;
            }
        }

        /// <summary>
        /// Gets or sets the ZipFileIndex.
        /// </summary>
        public long ZipFileIndex
        {
            get
            {
                return this.zipFileIndex;
            }
            set
            {
                this.zipFileIndex = value;
            }
        }

        /// <summary>
        /// Gets or sets the Offset.
        /// </summary>
        public long Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
            }
        }

        /// <summary>
        /// Gets or sets the ExternalFileAttributes.
        /// </summary>
        public int ExternalFileAttributes
        {
            get
            {
                if ((this.known & ZipEntry.Known.ExternalAttributes) == ZipEntry.Known.None)
                    return -1;
                return this.externalFileAttributes;
            }
            set
            {
                this.externalFileAttributes = value;
                this.known |= ZipEntry.Known.ExternalAttributes;
            }
        }

        /// <summary>
        /// Gets the VersionMadeBy.
        /// </summary>
        public int VersionMadeBy
        {
            get
            {
                return (int)this.versionMadeBy & (int)byte.MaxValue;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsDOSEntry.
        /// </summary>
        public bool IsDOSEntry
        {
            get
            {
                return this.HostSystem == 0 || this.HostSystem == 10;
            }
        }

        /// <summary>
        /// The HasDosAttributes.
        /// </summary>
        /// <param name="attributes">The attributes<see cref="int"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool HasDosAttributes(int attributes)
        {
            bool flag = false;
            if ((this.known & ZipEntry.Known.ExternalAttributes) != ZipEntry.Known.None && ((this.HostSystem == 0 || this.HostSystem == 10) && (this.ExternalFileAttributes & attributes) == attributes))
                flag = true;
            return flag;
        }

        /// <summary>
        /// Gets or sets the HostSystem.
        /// </summary>
        public int HostSystem
        {
            get
            {
                return (int)this.versionMadeBy >> 8 & (int)byte.MaxValue;
            }
            set
            {
                this.versionMadeBy &= (ushort)byte.MaxValue;
                this.versionMadeBy |= (ushort)((value & (int)byte.MaxValue) << 8);
            }
        }

        /// <summary>
        /// Gets the Version.
        /// </summary>
        public int Version
        {
            get
            {
                if (this.versionToExtract != (ushort)0)
                    return (int)this.versionToExtract;
                int num = 10;
                if (this.CentralHeaderRequiresZip64)
                    num = 45;
                else if (CompressionMethod.Deflated == this.method)
                    num = 20;
                else if (this.IsDirectory)
                    num = 20;
                else if (this.IsCrypted)
                    num = 20;
                else if (this.HasDosAttributes(8))
                    num = 11;
                return num;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanDecompress.
        /// </summary>
        public bool CanDecompress
        {
            get
            {
                return this.Version <= 45 && (this.Version == 10 || this.Version == 11 || (this.Version == 20 || this.Version == 45)) && this.IsCompressionMethodSupported();
            }
        }

        /// <summary>
        /// The ForceZip64.
        /// </summary>
        public void ForceZip64()
        {
            this.forceZip64_ = true;
        }

        /// <summary>
        /// The IsZip64Forced.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsZip64Forced()
        {
            return this.forceZip64_;
        }

        /// <summary>
        /// Gets a value indicating whether LocalHeaderRequiresZip64.
        /// </summary>
        public bool LocalHeaderRequiresZip64
        {
            get
            {
                bool flag = this.forceZip64_;
                if (!flag)
                {
                    ulong compressedSize = this.compressedSize;
                    if (this.versionToExtract == (ushort)0 && this.IsCrypted)
                        compressedSize += 12UL;
                    flag = (this.size >= (ulong)uint.MaxValue || compressedSize >= (ulong)uint.MaxValue) && (this.versionToExtract == (ushort)0 || this.versionToExtract >= (ushort)45);
                }
                return flag;
            }
        }

        /// <summary>
        /// Gets a value indicating whether CentralHeaderRequiresZip64.
        /// </summary>
        public bool CentralHeaderRequiresZip64
        {
            get
            {
                return this.LocalHeaderRequiresZip64 || this.offset >= (long)uint.MaxValue;
            }
        }

        /// <summary>
        /// Gets or sets the DosTime.
        /// </summary>
        public long DosTime
        {
            get
            {
                if ((this.known & ZipEntry.Known.Time) == ZipEntry.Known.None)
                    return 0;
                return (long)this.dosTime;
            }
            set
            {
                this.dosTime = (uint)value;
                this.known |= ZipEntry.Known.Time;
            }
        }

        /// <summary>
        /// Gets or sets the DateTime.
        /// </summary>
        public DateTime DateTime
        {
            get
            {
                uint num1 = Math.Min(59U, (uint)(2 * ((int)this.dosTime & 31)));
                uint num2 = Math.Min(59U, this.dosTime >> 5 & 63U);
                uint num3 = Math.Min(23U, this.dosTime >> 11 & 31U);
                uint num4 = Math.Max(1U, Math.Min(12U, this.dosTime >> 21 & 15U));
                uint num5 = (uint)(((int)(this.dosTime >> 25) & (int)sbyte.MaxValue) + 1980);
                int day = Math.Max(1, Math.Min(DateTime.DaysInMonth((int)num5, (int)num4), (int)(this.dosTime >> 16) & 31));
                return new DateTime((int)num5, (int)num4, day, (int)num3, (int)num2, (int)num1);
            }
            set
            {
                uint num1 = (uint)value.Year;
                uint num2 = (uint)value.Month;
                uint num3 = (uint)value.Day;
                uint num4 = (uint)value.Hour;
                uint num5 = (uint)value.Minute;
                uint num6 = (uint)value.Second;
                if (num1 < 1980U)
                {
                    num1 = 1980U;
                    num2 = 1U;
                    num3 = 1U;
                    num4 = 0U;
                    num5 = 0U;
                    num6 = 0U;
                }
                else if (num1 > 2107U)
                {
                    num1 = 2107U;
                    num2 = 12U;
                    num3 = 31U;
                    num4 = 23U;
                    num5 = 59U;
                    num6 = 59U;
                }
                this.DosTime = (long)((uint)(((int)num1 - 1980 & (int)sbyte.MaxValue) << 25 | (int)num2 << 21 | (int)num3 << 16 | (int)num4 << 11 | (int)num5 << 5) | num6 >> 1);
            }
        }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets or sets the Size.
        /// </summary>
        public long Size
        {
            get
            {
                return (this.known & ZipEntry.Known.Size) != ZipEntry.Known.None ? (long)this.size : -1L;
            }
            set
            {
                this.size = (ulong)value;
                this.known |= ZipEntry.Known.Size;
            }
        }

        /// <summary>
        /// Gets or sets the CompressedSize.
        /// </summary>
        public long CompressedSize
        {
            get
            {
                return (this.known & ZipEntry.Known.CompressedSize) != ZipEntry.Known.None ? (long)this.compressedSize : -1L;
            }
            set
            {
                this.compressedSize = (ulong)value;
                this.known |= ZipEntry.Known.CompressedSize;
            }
        }

        /// <summary>
        /// Gets or sets the Crc.
        /// </summary>
        public long Crc
        {
            get
            {
                return (this.known & ZipEntry.Known.Crc) != ZipEntry.Known.None ? (long)this.crc & (long)uint.MaxValue : -1L;
            }
            set
            {
                if (((long)this.crc & -4294967296L) != 0L)
                    throw new ArgumentOutOfRangeException(nameof(value));
                this.crc = (uint)value;
                this.known |= ZipEntry.Known.Crc;
            }
        }

        /// <summary>
        /// Gets or sets the CompressionMethod.
        /// </summary>
        public CompressionMethod CompressionMethod
        {
            get
            {
                return this.method;
            }
            set
            {
                if (!ZipEntry.IsCompressionMethodSupported(value))
                    throw new NotSupportedException("Compression method not supported");
                this.method = value;
            }
        }

        /// <summary>
        /// Gets or sets the ExtraData.
        /// </summary>
        public byte[] ExtraData
        {
            get
            {
                return this.extra;
            }
            set
            {
                if (value == null)
                {
                    this.extra = (byte[])null;
                }
                else
                {
                    if (value.Length > (int)ushort.MaxValue)
                        throw new ArgumentOutOfRangeException(nameof(value));
                    this.extra = new byte[value.Length];
                    Array.Copy((Array)value, 0, (Array)this.extra, 0, value.Length);
                }
            }
        }

        /// <summary>
        /// The ProcessExtraData.
        /// </summary>
        /// <param name="localHeader">The localHeader<see cref="bool"/>.</param>
        internal void ProcessExtraData(bool localHeader)
        {
            ZipExtraData zipExtraData = new ZipExtraData(this.extra);
            if (zipExtraData.Find(1))
            {
                if (((int)this.versionToExtract & (int)byte.MaxValue) < 45)
                    throw new ZipException("Zip64 Extended information found but version is not valid");
                this.forceZip64_ = true;
                if (zipExtraData.ValueLength < 4)
                    throw new ZipException("Extra data extended Zip64 information length is invalid");
                if (localHeader || this.size == (ulong)uint.MaxValue)
                    this.size = (ulong)zipExtraData.ReadLong();
                if (localHeader || this.compressedSize == (ulong)uint.MaxValue)
                    this.compressedSize = (ulong)zipExtraData.ReadLong();
                if (!localHeader && this.offset == (long)uint.MaxValue)
                    this.offset = zipExtraData.ReadLong();
            }
            else if (((int)this.versionToExtract & (int)byte.MaxValue) >= 45 && (this.size == (ulong)uint.MaxValue || this.compressedSize == (ulong)uint.MaxValue))
                throw new ZipException("Zip64 Extended information required but is missing.");
            if (zipExtraData.Find(10))
            {
                if (zipExtraData.ValueLength < 8)
                    throw new ZipException("NTFS Extra data invalid");
                zipExtraData.ReadInt();
                while (zipExtraData.UnreadCount >= 4)
                {
                    int num = zipExtraData.ReadShort();
                    int amount = zipExtraData.ReadShort();
                    if (num == 1)
                    {
                        if (amount < 24)
                            break;
                        long fileTime = zipExtraData.ReadLong();
                        zipExtraData.ReadLong();
                        zipExtraData.ReadLong();
                        this.DateTime = DateTime.FromFileTime(fileTime);
                        break;
                    }
                    zipExtraData.Skip(amount);
                }
            }
            else
            {
                if (!zipExtraData.Find(21589))
                    return;
                int valueLength = zipExtraData.ValueLength;
                if ((zipExtraData.ReadByte() & 1) != 0 && valueLength >= 5)
                {
                    int seconds = zipExtraData.ReadInt();
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0);
                    dateTime = dateTime.ToUniversalTime() + new TimeSpan(0, 0, 0, seconds, 0);
                    this.DateTime = dateTime.ToLocalTime();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Comment.
        /// </summary>
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                if (value != null && value.Length > (int)ushort.MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(value), "cannot exceed 65535");
                this.comment = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsDirectory.
        /// </summary>
        public bool IsDirectory
        {
            get
            {
                int length = this.name.Length;
                return length > 0 && (this.name[length - 1] == '/' || this.name[length - 1] == '\\') || this.HasDosAttributes(16);
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsFile.
        /// </summary>
        public bool IsFile
        {
            get
            {
                return !this.IsDirectory && !this.HasDosAttributes(8);
            }
        }

        /// <summary>
        /// The IsCompressionMethodSupported.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCompressionMethodSupported()
        {
            return ZipEntry.IsCompressionMethodSupported(this.CompressionMethod);
        }

        /// <summary>
        /// The Clone.
        /// </summary>
        /// <returns>The <see cref="object"/>.</returns>
        public object Clone()
        {
            ZipEntry zipEntry = (ZipEntry)this.MemberwiseClone();
            if (this.extra != null)
            {
                zipEntry.extra = new byte[this.extra.Length];
                Array.Copy((Array)this.extra, 0, (Array)zipEntry.extra, 0, this.extra.Length);
            }
            return (object)zipEntry;
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.name;
        }

        /// <summary>
        /// The IsCompressionMethodSupported.
        /// </summary>
        /// <param name="method">The method<see cref="CompressionMethod"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsCompressionMethodSupported(CompressionMethod method)
        {
            return method == CompressionMethod.Deflated || method == CompressionMethod.Stored;
        }

        /// <summary>
        /// The CleanName.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string CleanName(string name)
        {
            if (name == null)
                return string.Empty;
            if (Path.IsPathRooted(name))
                name = name.Substring(Path.GetPathRoot(name).Length);
            name = name.Replace("\\", "/");
            while (name.Length > 0 && name[0] == '/')
                name = name.Remove(0, 1);
            return name;
        }

        /// <summary>
        /// Defines the Known.
        /// </summary>
        [System.Flags]
        private enum Known : byte
        {
            /// <summary>
            /// Defines the None.
            /// </summary>
            None = 0,

            /// <summary>
            /// Defines the Size.
            /// </summary>
            Size = 1,

            /// <summary>
            /// Defines the CompressedSize.
            /// </summary>
            CompressedSize = 2,

            /// <summary>
            /// Defines the Crc.
            /// </summary>
            Crc = 4,

            /// <summary>
            /// Defines the Time.
            /// </summary>
            Time = 8,

            /// <summary>
            /// Defines the ExternalAttributes.
            /// </summary>
            ExternalAttributes = 16, // 0x10
        }
    }
}
