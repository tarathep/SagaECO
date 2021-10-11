namespace SevenZip
{
    using System.IO;

    /// <summary>
    /// Defines the <see cref="ICoder" />.
    /// </summary>
    public interface ICoder
    {
        /// <summary>
        /// The Code.
        /// </summary>
        /// <param name="inStream">input Stream.</param>
        /// <param name="outStream">output Stream.</param>
        /// <param name="inSize">input Size. -1 if unknown.</param>
        /// <param name="outSize">output Size. -1 if unknown.</param>
        /// <param name="progress">callback progress reference.</param>
        void Code(Stream inStream, Stream outStream, long inSize, long outSize, ICodeProgress progress);
    }
}
