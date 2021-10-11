namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="TarHeader" />.
    /// </summary>
    public class TarHeader : ICloneable
    {
        /// <summary>
        /// Defines the dateTime1970.
        /// </summary>
        private static readonly DateTime dateTime1970 = new DateTime(1970, 1, 1, 0, 0, 0, 0);

        /// <summary>
        /// Defines the groupNameAsSet.
        /// </summary>
        internal static string groupNameAsSet = "None";

        /// <summary>
        /// Defines the defaultGroupName.
        /// </summary>
        internal static string defaultGroupName = "None";

        /// <summary>
        /// Defines the NAMELEN.
        /// </summary>
        public const int NAMELEN = 100;

        /// <summary>
        /// Defines the MODELEN.
        /// </summary>
        public const int MODELEN = 8;

        /// <summary>
        /// Defines the UIDLEN.
        /// </summary>
        public const int UIDLEN = 8;

        /// <summary>
        /// Defines the GIDLEN.
        /// </summary>
        public const int GIDLEN = 8;

        /// <summary>
        /// Defines the CHKSUMLEN.
        /// </summary>
        public const int CHKSUMLEN = 8;

        /// <summary>
        /// Defines the CHKSUMOFS.
        /// </summary>
        public const int CHKSUMOFS = 148;

        /// <summary>
        /// Defines the SIZELEN.
        /// </summary>
        public const int SIZELEN = 12;

        /// <summary>
        /// Defines the MAGICLEN.
        /// </summary>
        public const int MAGICLEN = 6;

        /// <summary>
        /// Defines the VERSIONLEN.
        /// </summary>
        public const int VERSIONLEN = 2;

        /// <summary>
        /// Defines the MODTIMELEN.
        /// </summary>
        public const int MODTIMELEN = 12;

        /// <summary>
        /// Defines the UNAMELEN.
        /// </summary>
        public const int UNAMELEN = 32;

        /// <summary>
        /// Defines the GNAMELEN.
        /// </summary>
        public const int GNAMELEN = 32;

        /// <summary>
        /// Defines the DEVLEN.
        /// </summary>
        public const int DEVLEN = 8;

        /// <summary>
        /// Defines the LF_OLDNORM.
        /// </summary>
        public const byte LF_OLDNORM = 0;

        /// <summary>
        /// Defines the LF_NORMAL.
        /// </summary>
        public const byte LF_NORMAL = 48;

        /// <summary>
        /// Defines the LF_LINK.
        /// </summary>
        public const byte LF_LINK = 49;

        /// <summary>
        /// Defines the LF_SYMLINK.
        /// </summary>
        public const byte LF_SYMLINK = 50;

        /// <summary>
        /// Defines the LF_CHR.
        /// </summary>
        public const byte LF_CHR = 51;

        /// <summary>
        /// Defines the LF_BLK.
        /// </summary>
        public const byte LF_BLK = 52;

        /// <summary>
        /// Defines the LF_DIR.
        /// </summary>
        public const byte LF_DIR = 53;

        /// <summary>
        /// Defines the LF_FIFO.
        /// </summary>
        public const byte LF_FIFO = 54;

        /// <summary>
        /// Defines the LF_CONTIG.
        /// </summary>
        public const byte LF_CONTIG = 55;

        /// <summary>
        /// Defines the LF_GHDR.
        /// </summary>
        public const byte LF_GHDR = 103;

        /// <summary>
        /// Defines the LF_XHDR.
        /// </summary>
        public const byte LF_XHDR = 120;

        /// <summary>
        /// Defines the LF_ACL.
        /// </summary>
        public const byte LF_ACL = 65;

        /// <summary>
        /// Defines the LF_GNU_DUMPDIR.
        /// </summary>
        public const byte LF_GNU_DUMPDIR = 68;

        /// <summary>
        /// Defines the LF_EXTATTR.
        /// </summary>
        public const byte LF_EXTATTR = 69;

        /// <summary>
        /// Defines the LF_META.
        /// </summary>
        public const byte LF_META = 73;

        /// <summary>
        /// Defines the LF_GNU_LONGLINK.
        /// </summary>
        public const byte LF_GNU_LONGLINK = 75;

        /// <summary>
        /// Defines the LF_GNU_LONGNAME.
        /// </summary>
        public const byte LF_GNU_LONGNAME = 76;

        /// <summary>
        /// Defines the LF_GNU_MULTIVOL.
        /// </summary>
        public const byte LF_GNU_MULTIVOL = 77;

        /// <summary>
        /// Defines the LF_GNU_NAMES.
        /// </summary>
        public const byte LF_GNU_NAMES = 78;

        /// <summary>
        /// Defines the LF_GNU_SPARSE.
        /// </summary>
        public const byte LF_GNU_SPARSE = 83;

        /// <summary>
        /// Defines the LF_GNU_VOLHDR.
        /// </summary>
        public const byte LF_GNU_VOLHDR = 86;

        /// <summary>
        /// Defines the TMAGIC.
        /// </summary>
        public const string TMAGIC = "ustar ";

        /// <summary>
        /// Defines the GNU_TMAGIC.
        /// </summary>
        public const string GNU_TMAGIC = "ustar  ";

        /// <summary>
        /// Defines the timeConversionFactor.
        /// </summary>
        private const long timeConversionFactor = 10000000;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the mode.
        /// </summary>
        private int mode;

        /// <summary>
        /// Defines the userId.
        /// </summary>
        private int userId;

        /// <summary>
        /// Defines the groupId.
        /// </summary>
        private int groupId;

        /// <summary>
        /// Defines the size.
        /// </summary>
        private long size;

        /// <summary>
        /// Defines the modTime.
        /// </summary>
        private DateTime modTime;

        /// <summary>
        /// Defines the checksum.
        /// </summary>
        private int checksum;

        /// <summary>
        /// Defines the isChecksumValid.
        /// </summary>
        private bool isChecksumValid;

        /// <summary>
        /// Defines the typeFlag.
        /// </summary>
        private byte typeFlag;

        /// <summary>
        /// Defines the linkName.
        /// </summary>
        private string linkName;

        /// <summary>
        /// Defines the magic.
        /// </summary>
        private string magic;

        /// <summary>
        /// Defines the version.
        /// </summary>
        private string version;

        /// <summary>
        /// Defines the userName.
        /// </summary>
        private string userName;

        /// <summary>
        /// Defines the groupName.
        /// </summary>
        private string groupName;

        /// <summary>
        /// Defines the devMajor.
        /// </summary>
        private int devMajor;

        /// <summary>
        /// Defines the devMinor.
        /// </summary>
        private int devMinor;

        /// <summary>
        /// Defines the userIdAsSet.
        /// </summary>
        internal static int userIdAsSet;

        /// <summary>
        /// Defines the groupIdAsSet.
        /// </summary>
        internal static int groupIdAsSet;

        /// <summary>
        /// Defines the userNameAsSet.
        /// </summary>
        internal static string userNameAsSet;

        /// <summary>
        /// Defines the defaultUserId.
        /// </summary>
        internal static int defaultUserId;

        /// <summary>
        /// Defines the defaultGroupId.
        /// </summary>
        internal static int defaultGroupId;

        /// <summary>
        /// Defines the defaultUser.
        /// </summary>
        internal static string defaultUser;

        /// <summary>
        /// Initializes a new instance of the <see cref="TarHeader"/> class.
        /// </summary>
        public TarHeader()
        {
            this.Magic = "ustar ";
            this.Version = " ";
            this.Name = "";
            this.LinkName = "";
            this.UserId = TarHeader.defaultUserId;
            this.GroupId = TarHeader.defaultGroupId;
            this.UserName = TarHeader.defaultUser;
            this.GroupName = TarHeader.defaultGroupName;
            this.Size = 0L;
        }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.name = value;
            }
        }

        /// <summary>
        /// The GetName.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        [Obsolete("Use the Name property instead", true)]
        public string GetName()
        {
            return this.name;
        }

        /// <summary>
        /// Gets or sets the Mode.
        /// </summary>
        public int Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                this.mode = value;
            }
        }

        /// <summary>
        /// Gets or sets the UserId.
        /// </summary>
        public int UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        /// <summary>
        /// Gets or sets the GroupId.
        /// </summary>
        public int GroupId
        {
            get
            {
                return this.groupId;
            }
            set
            {
                this.groupId = value;
            }
        }

        /// <summary>
        /// Gets or sets the Size.
        /// </summary>
        public long Size
        {
            get
            {
                return this.size;
            }
            set
            {
                if (value < 0L)
                    throw new ArgumentOutOfRangeException(nameof(value), "Cannot be less than zero");
                this.size = value;
            }
        }

        /// <summary>
        /// Gets or sets the ModTime.
        /// </summary>
        public DateTime ModTime
        {
            get
            {
                return this.modTime;
            }
            set
            {
                if (value < TarHeader.dateTime1970)
                    throw new ArgumentOutOfRangeException(nameof(value), "ModTime cannot be before Jan 1st 1970");
                this.modTime = new DateTime(value.Year, value.Month, value.Day, value.Hour, value.Minute, value.Second);
            }
        }

        /// <summary>
        /// Gets the Checksum.
        /// </summary>
        public int Checksum
        {
            get
            {
                return this.checksum;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsChecksumValid.
        /// </summary>
        public bool IsChecksumValid
        {
            get
            {
                return this.isChecksumValid;
            }
        }

        /// <summary>
        /// Gets or sets the TypeFlag.
        /// </summary>
        public byte TypeFlag
        {
            get
            {
                return this.typeFlag;
            }
            set
            {
                this.typeFlag = value;
            }
        }

        /// <summary>
        /// Gets or sets the LinkName.
        /// </summary>
        public string LinkName
        {
            get
            {
                return this.linkName;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.linkName = value;
            }
        }

        /// <summary>
        /// Gets or sets the Magic.
        /// </summary>
        public string Magic
        {
            get
            {
                return this.magic;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.magic = value;
            }
        }

        /// <summary>
        /// Gets or sets the Version.
        /// </summary>
        public string Version
        {
            get
            {
                return this.version;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.version = value;
            }
        }

        /// <summary>
        /// Gets or sets the UserName.
        /// </summary>
        public string UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                if (value != null)
                {
                    this.userName = value.Substring(0, Math.Min(32, value.Length));
                }
                else
                {
                    string str = Environment.UserName;
                    if (str.Length > 32)
                        str = str.Substring(0, 32);
                    this.userName = str;
                }
            }
        }

        /// <summary>
        /// Gets or sets the GroupName.
        /// </summary>
        public string GroupName
        {
            get
            {
                return this.groupName;
            }
            set
            {
                if (value == null)
                    this.groupName = "None";
                else
                    this.groupName = value;
            }
        }

        /// <summary>
        /// Gets or sets the DevMajor.
        /// </summary>
        public int DevMajor
        {
            get
            {
                return this.devMajor;
            }
            set
            {
                this.devMajor = value;
            }
        }

        /// <summary>
        /// Gets or sets the DevMinor.
        /// </summary>
        public int DevMinor
        {
            get
            {
                return this.devMinor;
            }
            set
            {
                this.devMinor = value;
            }
        }

        /// <summary>
        /// The Clone.
        /// </summary>
        /// <returns>The <see cref="object"/>.</returns>
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /// <summary>
        /// The ParseBuffer.
        /// </summary>
        /// <param name="header">The header<see cref="byte[]"/>.</param>
        public void ParseBuffer(byte[] header)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header));
            int offset1 = 0;
            this.name = TarHeader.ParseName(header, offset1, 100).ToString();
            int offset2 = offset1 + 100;
            this.mode = (int)TarHeader.ParseOctal(header, offset2, 8);
            int offset3 = offset2 + 8;
            this.UserId = (int)TarHeader.ParseOctal(header, offset3, 8);
            int offset4 = offset3 + 8;
            this.GroupId = (int)TarHeader.ParseOctal(header, offset4, 8);
            int offset5 = offset4 + 8;
            this.Size = TarHeader.ParseOctal(header, offset5, 12);
            int offset6 = offset5 + 12;
            this.ModTime = TarHeader.GetDateTimeFromCTime(TarHeader.ParseOctal(header, offset6, 12));
            int offset7 = offset6 + 12;
            this.checksum = (int)TarHeader.ParseOctal(header, offset7, 8);
            int num = offset7 + 8;
            byte[] numArray = header;
            int index = num;
            int offset8 = index + 1;
            this.TypeFlag = numArray[index];
            this.LinkName = TarHeader.ParseName(header, offset8, 100).ToString();
            int offset9 = offset8 + 100;
            this.Magic = TarHeader.ParseName(header, offset9, 6).ToString();
            int offset10 = offset9 + 6;
            this.Version = TarHeader.ParseName(header, offset10, 2).ToString();
            int offset11 = offset10 + 2;
            this.UserName = TarHeader.ParseName(header, offset11, 32).ToString();
            int offset12 = offset11 + 32;
            this.GroupName = TarHeader.ParseName(header, offset12, 32).ToString();
            int offset13 = offset12 + 32;
            this.DevMajor = (int)TarHeader.ParseOctal(header, offset13, 8);
            int offset14 = offset13 + 8;
            this.DevMinor = (int)TarHeader.ParseOctal(header, offset14, 8);
            this.isChecksumValid = this.Checksum == TarHeader.MakeCheckSum(header);
        }

        /// <summary>
        /// The WriteHeader.
        /// </summary>
        /// <param name="outBuffer">The outBuffer<see cref="byte[]"/>.</param>
        public void WriteHeader(byte[] outBuffer)
        {
            if (outBuffer == null)
                throw new ArgumentNullException(nameof(outBuffer));
            int offset1 = 0;
            int nameBytes1 = TarHeader.GetNameBytes(this.Name, outBuffer, offset1, 100);
            int octalBytes1 = TarHeader.GetOctalBytes((long)this.mode, outBuffer, nameBytes1, 8);
            int octalBytes2 = TarHeader.GetOctalBytes((long)this.UserId, outBuffer, octalBytes1, 8);
            int octalBytes3 = TarHeader.GetOctalBytes((long)this.GroupId, outBuffer, octalBytes2, 8);
            int longOctalBytes1 = TarHeader.GetLongOctalBytes(this.Size, outBuffer, octalBytes3, 12);
            int longOctalBytes2 = TarHeader.GetLongOctalBytes((long)TarHeader.GetCTime(this.ModTime), outBuffer, longOctalBytes1, 12);
            int offset2 = longOctalBytes2;
            for (int index = 0; index < 8; ++index)
                outBuffer[longOctalBytes2++] = (byte)32;
            byte[] numArray = outBuffer;
            int index1 = longOctalBytes2;
            int offset3 = index1 + 1;
            int typeFlag = (int)this.TypeFlag;
            numArray[index1] = (byte)typeFlag;
            int nameBytes2 = TarHeader.GetNameBytes(this.LinkName, outBuffer, offset3, 100);
            int asciiBytes = TarHeader.GetAsciiBytes(this.Magic, 0, outBuffer, nameBytes2, 6);
            int nameBytes3 = TarHeader.GetNameBytes(this.Version, outBuffer, asciiBytes, 2);
            int nameBytes4 = TarHeader.GetNameBytes(this.UserName, outBuffer, nameBytes3, 32);
            int offset4 = TarHeader.GetNameBytes(this.GroupName, outBuffer, nameBytes4, 32);
            if (this.TypeFlag == (byte)51 || this.TypeFlag == (byte)52)
            {
                int octalBytes4 = TarHeader.GetOctalBytes((long)this.DevMajor, outBuffer, offset4, 8);
                offset4 = TarHeader.GetOctalBytes((long)this.DevMinor, outBuffer, octalBytes4, 8);
            }
            while (offset4 < outBuffer.Length)
                outBuffer[offset4++] = (byte)0;
            this.checksum = TarHeader.ComputeCheckSum(outBuffer);
            TarHeader.GetCheckSumOctalBytes((long)this.checksum, outBuffer, offset2, 8);
            this.isChecksumValid = true;
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
        /// The Equals.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool Equals(object obj)
        {
            TarHeader tarHeader = obj as TarHeader;
            if (tarHeader != null)
                return this.name == tarHeader.name && this.mode == tarHeader.mode && (this.UserId == tarHeader.UserId && this.GroupId == tarHeader.GroupId) && (this.Size == tarHeader.Size && this.ModTime == tarHeader.ModTime && (this.Checksum == tarHeader.Checksum && (int)this.TypeFlag == (int)tarHeader.TypeFlag)) && (this.LinkName == tarHeader.LinkName && this.Magic == tarHeader.Magic && (this.Version == tarHeader.Version && this.UserName == tarHeader.UserName) && (this.GroupName == tarHeader.GroupName && this.DevMajor == tarHeader.DevMajor)) && this.DevMinor == tarHeader.DevMinor;
            return false;
        }

        /// <summary>
        /// The SetValueDefaults.
        /// </summary>
        /// <param name="userId">The userId<see cref="int"/>.</param>
        /// <param name="userName">The userName<see cref="string"/>.</param>
        /// <param name="groupId">The groupId<see cref="int"/>.</param>
        /// <param name="groupName">The groupName<see cref="string"/>.</param>
        internal static void SetValueDefaults(int userId, string userName, int groupId, string groupName)
        {
            TarHeader.defaultUserId = TarHeader.userIdAsSet = userId;
            TarHeader.defaultUser = TarHeader.userNameAsSet = userName;
            TarHeader.defaultGroupId = TarHeader.groupIdAsSet = groupId;
            TarHeader.defaultGroupName = TarHeader.groupNameAsSet = groupName;
        }

        /// <summary>
        /// The RestoreSetValues.
        /// </summary>
        internal static void RestoreSetValues()
        {
            TarHeader.defaultUserId = TarHeader.userIdAsSet;
            TarHeader.defaultUser = TarHeader.userNameAsSet;
            TarHeader.defaultGroupId = TarHeader.groupIdAsSet;
            TarHeader.defaultGroupName = TarHeader.groupNameAsSet;
        }

        /// <summary>
        /// The ParseOctal.
        /// </summary>
        /// <param name="header">The header<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="long"/>.</returns>
        public static long ParseOctal(byte[] header, int offset, int length)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header));
            long num1 = 0;
            bool flag = true;
            int num2 = offset + length;
            for (int index = offset; index < num2 && header[index] != (byte)0; ++index)
            {
                if (header[index] == (byte)32 || header[index] == (byte)48)
                {
                    if (!flag)
                    {
                        if (header[index] == (byte)32)
                            break;
                    }
                    else
                        continue;
                }
                flag = false;
                num1 = (num1 << 3) + (long)((int)header[index] - 48);
            }
            return num1;
        }

        /// <summary>
        /// The ParseName.
        /// </summary>
        /// <param name="header">The header<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="StringBuilder"/>.</returns>
        public static StringBuilder ParseName(byte[] header, int offset, int length)
        {
            if (header == null)
                throw new ArgumentNullException(nameof(header));
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "Cannot be less than zero");
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), "Cannot be less than zero");
            if (offset + length > header.Length)
                throw new ArgumentException("Exceeds header size", nameof(length));
            StringBuilder stringBuilder = new StringBuilder(length);
            for (int index = offset; index < offset + length && header[index] != (byte)0; ++index)
                stringBuilder.Append((char)header[index]);
            return stringBuilder;
        }

        /// <summary>
        /// The GetNameBytes.
        /// </summary>
        /// <param name="name">The name<see cref="StringBuilder"/>.</param>
        /// <param name="nameOffset">The nameOffset<see cref="int"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="bufferOffset">The bufferOffset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetNameBytes(StringBuilder name, int nameOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            return TarHeader.GetNameBytes(name.ToString(), nameOffset, buffer, bufferOffset, length);
        }

        /// <summary>
        /// The GetNameBytes.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="nameOffset">The nameOffset<see cref="int"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="bufferOffset">The bufferOffset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetNameBytes(string name, int nameOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            int num;
            for (num = 0; num < length - 1 && nameOffset + num < name.Length; ++num)
                buffer[bufferOffset + num] = (byte)name[nameOffset + num];
            for (; num < length; ++num)
                buffer[bufferOffset + num] = (byte)0;
            return bufferOffset + length;
        }

        /// <summary>
        /// The GetNameBytes.
        /// </summary>
        /// <param name="name">The name<see cref="StringBuilder"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetNameBytes(StringBuilder name, byte[] buffer, int offset, int length)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            return TarHeader.GetNameBytes(name.ToString(), 0, buffer, offset, length);
        }

        /// <summary>
        /// The GetNameBytes.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetNameBytes(string name, byte[] buffer, int offset, int length)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            return TarHeader.GetNameBytes(name, 0, buffer, offset, length);
        }

        /// <summary>
        /// The GetAsciiBytes.
        /// </summary>
        /// <param name="toAdd">The toAdd<see cref="string"/>.</param>
        /// <param name="nameOffset">The nameOffset<see cref="int"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="bufferOffset">The bufferOffset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetAsciiBytes(string toAdd, int nameOffset, byte[] buffer, int bufferOffset, int length)
        {
            if (toAdd == null)
                throw new ArgumentNullException(nameof(toAdd));
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            for (int index = 0; index < length && nameOffset + index < toAdd.Length; ++index)
                buffer[bufferOffset + index] = (byte)toAdd[nameOffset + index];
            return bufferOffset + length;
        }

        /// <summary>
        /// The GetOctalBytes.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetOctalBytes(long value, byte[] buffer, int offset, int length)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            int num1 = length - 1;
            buffer[offset + num1] = (byte)0;
            int num2 = num1 - 1;
            if (value > 0L)
            {
                for (long index = value; num2 >= 0 && index > 0L; --num2)
                {
                    buffer[offset + num2] = (byte)(48U + (uint)(byte)((ulong)index & 7UL));
                    index >>= 3;
                }
            }
            for (; num2 >= 0; --num2)
                buffer[offset + num2] = (byte)48;
            return offset + length;
        }

        /// <summary>
        /// The GetLongOctalBytes.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetLongOctalBytes(long value, byte[] buffer, int offset, int length)
        {
            return TarHeader.GetOctalBytes(value, buffer, offset, length);
        }

        /// <summary>
        /// The GetCheckSumOctalBytes.
        /// </summary>
        /// <param name="value">The value<see cref="long"/>.</param>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <param name="offset">The offset<see cref="int"/>.</param>
        /// <param name="length">The length<see cref="int"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private static int GetCheckSumOctalBytes(long value, byte[] buffer, int offset, int length)
        {
            TarHeader.GetOctalBytes(value, buffer, offset, length - 1);
            return offset + length;
        }

        /// <summary>
        /// The ComputeCheckSum.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private static int ComputeCheckSum(byte[] buffer)
        {
            int num = 0;
            for (int index = 0; index < buffer.Length; ++index)
                num += (int)buffer[index];
            return num;
        }

        /// <summary>
        /// The MakeCheckSum.
        /// </summary>
        /// <param name="buffer">The buffer<see cref="byte[]"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private static int MakeCheckSum(byte[] buffer)
        {
            int num = 0;
            for (int index = 0; index < 148; ++index)
                num += (int)buffer[index];
            for (int index = 0; index < 8; ++index)
                num += 32;
            for (int index = 156; index < buffer.Length; ++index)
                num += (int)buffer[index];
            return num;
        }

        /// <summary>
        /// The GetCTime.
        /// </summary>
        /// <param name="dateTime">The dateTime<see cref="DateTime"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        private static int GetCTime(DateTime dateTime)
        {
            return (int)((dateTime.Ticks - TarHeader.dateTime1970.Ticks) / 10000000L);
        }

        /// <summary>
        /// The GetDateTimeFromCTime.
        /// </summary>
        /// <param name="ticks">The ticks<see cref="long"/>.</param>
        /// <returns>The <see cref="DateTime"/>.</returns>
        private static DateTime GetDateTimeFromCTime(long ticks)
        {
            DateTime dateTime;
            try
            {
                dateTime = new DateTime(TarHeader.dateTime1970.Ticks + ticks * 10000000L);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                dateTime = TarHeader.dateTime1970;
            }
            return dateTime;
        }
    }
}
