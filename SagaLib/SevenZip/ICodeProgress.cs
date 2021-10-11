namespace SevenZip
{
    /// <summary>
    /// Defines the <see cref="ICodeProgress" />.
    /// </summary>
    public interface ICodeProgress
    {
        /// <summary>
        /// The SetProgress.
        /// </summary>
        /// <param name="inSize">input size. -1 if unknown.</param>
        /// <param name="outSize">output size. -1 if unknown.</param>
        void SetProgress(long inSize, long outSize);
    }
}
