namespace ICSharpCode.SharpZipLib.Zip
{
    using ICSharpCode.SharpZipLib.Core;
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="ZipNameTransform" />.
    /// </summary>
    public class ZipNameTransform : INameTransform
    {
        /// <summary>
        /// Defines the trimPrefix_.
        /// </summary>
        private string trimPrefix_;

        /// <summary>
        /// Defines the InvalidEntryChars.
        /// </summary>
        private static readonly char[] InvalidEntryChars;

        /// <summary>
        /// Defines the InvalidEntryCharsRelaxed.
        /// </summary>
        private static readonly char[] InvalidEntryCharsRelaxed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipNameTransform"/> class.
        /// </summary>
        public ZipNameTransform()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipNameTransform"/> class.
        /// </summary>
        /// <param name="trimPrefix">The trimPrefix<see cref="string"/>.</param>
        public ZipNameTransform(string trimPrefix)
        {
            this.TrimPrefix = trimPrefix;
        }

        /// <summary>
        /// Initializes static members of the <see cref="ZipNameTransform"/> class.
        /// </summary>
        static ZipNameTransform()
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            int length1 = invalidPathChars.Length + 2;
            ZipNameTransform.InvalidEntryCharsRelaxed = new char[length1];
            Array.Copy((Array)invalidPathChars, 0, (Array)ZipNameTransform.InvalidEntryCharsRelaxed, 0, invalidPathChars.Length);
            ZipNameTransform.InvalidEntryCharsRelaxed[length1 - 1] = '*';
            ZipNameTransform.InvalidEntryCharsRelaxed[length1 - 2] = '?';
            int length2 = invalidPathChars.Length + 4;
            ZipNameTransform.InvalidEntryChars = new char[length2];
            Array.Copy((Array)invalidPathChars, 0, (Array)ZipNameTransform.InvalidEntryChars, 0, invalidPathChars.Length);
            ZipNameTransform.InvalidEntryChars[length2 - 1] = ':';
            ZipNameTransform.InvalidEntryChars[length2 - 2] = '\\';
            ZipNameTransform.InvalidEntryChars[length2 - 3] = '*';
            ZipNameTransform.InvalidEntryChars[length2 - 4] = '?';
        }

        /// <summary>
        /// The TransformDirectory.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string TransformDirectory(string name)
        {
            name = this.TransformFile(name);
            if (name.Length <= 0)
                throw new ZipException("Cannot have an empty directory name");
            if (!name.EndsWith("/"))
                name += "/";
            return name;
        }

        /// <summary>
        /// The TransformFile.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string TransformFile(string name)
        {
            if (name != null)
            {
                if (this.trimPrefix_ != null && name.ToLower().IndexOf(this.trimPrefix_) == 0)
                    name = name.Substring(this.trimPrefix_.Length);
                name = name.Replace("\\", "/");
                name = WindowsPathUtils.DropPathRoot(name);
                while (name.Length > 0 && name[0] == '/')
                    name = name.Remove(0, 1);
                while (name.Length > 0 && name[name.Length - 1] == '/')
                    name = name.Remove(name.Length - 1, 1);
                for (int startIndex = name.IndexOf("//"); startIndex >= 0; startIndex = name.IndexOf("//"))
                    name = name.Remove(startIndex, 1);
                name = ZipNameTransform.MakeValidName(name, '_');
            }
            else
                name = string.Empty;
            return name;
        }

        /// <summary>
        /// Gets or sets the TrimPrefix.
        /// </summary>
        public string TrimPrefix
        {
            get
            {
                return this.trimPrefix_;
            }
            set
            {
                this.trimPrefix_ = value;
                if (this.trimPrefix_ == null)
                    return;
                this.trimPrefix_ = this.trimPrefix_.ToLower();
            }
        }

        /// <summary>
        /// The MakeValidName.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="replacement">The replacement<see cref="char"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private static string MakeValidName(string name, char replacement)
        {
            int index = name.IndexOfAny(ZipNameTransform.InvalidEntryChars);
            if (index >= 0)
            {
                StringBuilder stringBuilder = new StringBuilder(name);
                for (; index >= 0; index = index < name.Length ? name.IndexOfAny(ZipNameTransform.InvalidEntryChars, index + 1) : -1)
                    stringBuilder[index] = replacement;
                name = stringBuilder.ToString();
            }
            if (name.Length > (int)ushort.MaxValue)
                throw new PathTooLongException();
            return name;
        }

        /// <summary>
        /// The IsValidName.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="relaxed">The relaxed<see cref="bool"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsValidName(string name, bool relaxed)
        {
            bool flag = name != null;
            if (flag)
                flag = !relaxed ? name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0 : name.IndexOfAny(ZipNameTransform.InvalidEntryCharsRelaxed) < 0;
            return flag;
        }

        /// <summary>
        /// The IsValidName.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsValidName(string name)
        {
            return name != null && name.IndexOfAny(ZipNameTransform.InvalidEntryChars) < 0 && name.IndexOf('/') != 0;
        }
    }
}
