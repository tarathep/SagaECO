namespace ICSharpCode.SharpZipLib.Zip
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="IStaticDataSource" />.
    /// </summary>
    public interface IStaticDataSource
    {
        /// <summary>
        /// The GetSource.
        /// </summary>
        /// <returns>The <see cref="Stream"/>.</returns>
        Stream GetSource();
    }
}
