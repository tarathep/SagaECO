namespace ICSharpCode.SharpZipLib.Core
{
    /// <summary>
    /// Defines the <see cref="IScanFilter" />.
    /// </summary>
    public interface IScanFilter
    {
        /// <summary>
        /// The IsMatch.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        bool IsMatch(string name);
    }
}
