namespace SagaLib.VirtualFileSystem
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="RealFileSystem" />.
    /// </summary>
    public class RealFileSystem : IFileSystem
    {
        /// <summary>
        /// Defines the rootPath.
        /// </summary>
        private string rootPath = ".";

        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Init(string path)
        {
            this.rootPath = !(path != "") ? "" : path + "/";
            return true;
        }

        /// <summary>
        /// The OpenFile.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        public Stream OpenFile(string path)
        {
            if (path.IndexOf(":") < 0)
                return (Stream)new FileStream(this.rootPath + path, FileMode.Open, FileAccess.Read);
            return (Stream)new FileStream(path, FileMode.Open, FileAccess.Read);
        }

        /// <summary>
        /// The SearchFile.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="pattern">The pattern<see cref="string"/>.</param>
        /// <returns>The <see cref="string[]"/>.</returns>
        public string[] SearchFile(string path, string pattern)
        {
            return Directory.GetFiles(this.rootPath + path, pattern);
        }

        /// <summary>
        /// The SearchFile.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="pattern">The pattern<see cref="string"/>.</param>
        /// <param name="option">The option<see cref="SearchOption"/>.</param>
        /// <returns>The <see cref="string[]"/>.</returns>
        public string[] SearchFile(string path, string pattern, SearchOption option)
        {
            return Directory.GetFiles(this.rootPath + path, pattern, option);
        }

        /// <summary>
        /// The Close.
        /// </summary>
        public void Close()
        {
        }
    }
}
