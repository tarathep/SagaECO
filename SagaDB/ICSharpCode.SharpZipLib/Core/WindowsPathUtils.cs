namespace ICSharpCode.SharpZipLib.Core
{
    /// <summary>
    /// Defines the <see cref="WindowsPathUtils" />.
    /// </summary>
    public abstract class WindowsPathUtils
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsPathUtils"/> class.
        /// </summary>
        internal WindowsPathUtils()
        {
        }

        /// <summary>
        /// The DropPathRoot.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string DropPathRoot(string path)
        {
            string str = path;
            if (path != null && path.Length > 0)
            {
                if (path[0] == '\\' || path[0] == '/')
                {
                    if (path.Length > 1 && (path[1] == '\\' || path[1] == '/'))
                    {
                        int index = 2;
                        int num = 2;
                        while (index <= path.Length && (path[index] != '\\' && path[index] != '/' || --num > 0))
                            ++index;
                        int startIndex = index + 1;
                        str = startIndex >= path.Length ? "" : path.Substring(startIndex);
                    }
                }
                else if (path.Length > 1 && path[1] == ':')
                {
                    int count = 2;
                    if (path.Length > 2 && (path[2] == '\\' || path[2] == '/'))
                        count = 3;
                    str = str.Remove(0, count);
                }
            }
            return str;
        }
    }
}
