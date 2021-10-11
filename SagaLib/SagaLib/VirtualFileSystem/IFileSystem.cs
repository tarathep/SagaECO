namespace SagaLib.VirtualFileSystem
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="IFileSystem" />.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// The Init.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool Init(string path);

        /// <summary>
        /// The OpenFile.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <returns>The <see cref="Stream"/>.</returns>
        Stream OpenFile(string path);

        /// <summary>
        /// The SearchFile.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="pattern">The pattern<see cref="string"/>.</param>
        /// <returns>The <see cref="string[]"/>.</returns>
        string[] SearchFile(string path, string pattern);

        /// <summary>
        /// The SearchFile.
        /// </summary>
        /// <param name="path">The path<see cref="string"/>.</param>
        /// <param name="pattern">The pattern<see cref="string"/>.</param>
        /// <param name="option">The option<see cref="SearchOption"/>.</param>
        /// <returns>The <see cref="string[]"/>.</returns>
        string[] SearchFile(string path, string pattern, SearchOption option);

        /// <summary>
        /// The Close.
        /// </summary>
        void Close();
    }
}
