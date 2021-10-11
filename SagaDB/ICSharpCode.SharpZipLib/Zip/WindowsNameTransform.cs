namespace ICSharpCode.SharpZipLib.Zip
{
    using ICSharpCode.SharpZipLib.Core;
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Defines the <see cref="WindowsNameTransform" />.
    /// </summary>
    public class WindowsNameTransform : INameTransform
    {
        /// <summary>
        /// Defines the replacementChar_.
        /// </summary>
        private char replacementChar_ = '_';

        /// <summary>
        /// Defines the MaxPath.
        /// </summary>
        private const int MaxPath = 260;

        /// <summary>
        /// Defines the baseDirectory_.
        /// </summary>
        private string baseDirectory_;

        /// <summary>
        /// Defines the trimIncomingPaths_.
        /// </summary>
        private bool trimIncomingPaths_;

        /// <summary>
        /// Defines the InvalidEntryChars.
        /// </summary>
        private static readonly char[] InvalidEntryChars;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsNameTransform"/> class.
        /// </summary>
        /// <param name="baseDirectory">The baseDirectory<see cref="string"/>.</param>
        public WindowsNameTransform(string baseDirectory)
        {
            if (baseDirectory == null)
                throw new ArgumentNullException(nameof(baseDirectory), "Directory name is invalid");
            this.BaseDirectory = baseDirectory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsNameTransform"/> class.
        /// </summary>
        public WindowsNameTransform()
        {
        }

        /// <summary>
        /// Gets or sets the BaseDirectory.
        /// </summary>
        public string BaseDirectory
        {
            get
            {
                return this.baseDirectory_;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));
                this.baseDirectory_ = Path.GetFullPath(value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether TrimIncomingPaths.
        /// </summary>
        public bool TrimIncomingPaths
        {
            get
            {
                return this.trimIncomingPaths_;
            }
            set
            {
                this.trimIncomingPaths_ = value;
            }
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
            while (name.EndsWith("\\"))
                name = name.Remove(name.Length - 1, 1);
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
                name = WindowsNameTransform.MakeValidName(name, this.replacementChar_);
                if (this.trimIncomingPaths_)
                    name = Path.GetFileName(name);
                if (this.baseDirectory_ != null)
                    name = Path.Combine(this.baseDirectory_, name);
            }
            else
                name = string.Empty;
            return name;
        }

        /// <summary>
        /// The IsValidName.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsValidName(string name)
        {
            return name != null && name.Length <= 260 && string.Compare(name, WindowsNameTransform.MakeValidName(name, '_')) == 0;
        }

        /// <summary>
        /// Initializes static members of the <see cref="WindowsNameTransform"/> class.
        /// </summary>
        static WindowsNameTransform()
        {
            char[] invalidPathChars = Path.GetInvalidPathChars();
            int length = invalidPathChars.Length + 3;
            WindowsNameTransform.InvalidEntryChars = new char[length];
            Array.Copy((Array)invalidPathChars, 0, (Array)WindowsNameTransform.InvalidEntryChars, 0, invalidPathChars.Length);
            WindowsNameTransform.InvalidEntryChars[length - 1] = '*';
            WindowsNameTransform.InvalidEntryChars[length - 2] = '?';
            WindowsNameTransform.InvalidEntryChars[length - 2] = ':';
        }

        /// <summary>
        /// The MakeValidName.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="replacement">The replacement<see cref="char"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string MakeValidName(string name, char replacement)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            name = WindowsPathUtils.DropPathRoot(name.Replace("/", "\\"));
            while (name.Length > 0 && name[0] == '\\')
                name = name.Remove(0, 1);
            while (name.Length > 0 && name[name.Length - 1] == '\\')
                name = name.Remove(name.Length - 1, 1);
            for (int startIndex = name.IndexOf("\\\\"); startIndex >= 0; startIndex = name.IndexOf("\\\\"))
                name = name.Remove(startIndex, 1);
            int index = name.IndexOfAny(WindowsNameTransform.InvalidEntryChars);
            if (index >= 0)
            {
                StringBuilder stringBuilder = new StringBuilder(name);
                for (; index >= 0; index = index < name.Length ? name.IndexOfAny(WindowsNameTransform.InvalidEntryChars, index + 1) : -1)
                    stringBuilder[index] = replacement;
                name = stringBuilder.ToString();
            }
            if (name.Length > 260)
                throw new PathTooLongException();
            return name;
        }

        /// <summary>
        /// Gets or sets the Replacement.
        /// </summary>
        public char Replacement
        {
            get
            {
                return this.replacementChar_;
            }
            set
            {
                for (int index = 0; index < WindowsNameTransform.InvalidEntryChars.Length; ++index)
                {
                    if ((int)WindowsNameTransform.InvalidEntryChars[index] == (int)value)
                        throw new ArgumentException("invalid path character");
                }
                if (value == '\\' || value == '/')
                    throw new ArgumentException("invalid replacement character");
                this.replacementChar_ = value;
            }
        }
    }
}
