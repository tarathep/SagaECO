namespace ICSharpCode.SharpZipLib.Core
{
    /// <summary>
    /// Defines the <see cref="INameTransform" />.
    /// </summary>
    public interface INameTransform
    {
        /// <summary>
        /// The TransformFile.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string TransformFile(string name);

        /// <summary>
        /// The TransformDirectory.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        string TransformDirectory(string name);
    }
}
