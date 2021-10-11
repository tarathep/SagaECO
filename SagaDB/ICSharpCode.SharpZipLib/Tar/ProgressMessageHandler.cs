namespace ICSharpCode.SharpZipLib.Tar
{
    /// <summary>
    /// The ProgressMessageHandler.
    /// </summary>
    /// <param name="archive">The archive<see cref="TarArchive"/>.</param>
    /// <param name="entry">The entry<see cref="TarEntry"/>.</param>
    /// <param name="message">The message<see cref="string"/>.</param>
    public delegate void ProgressMessageHandler(TarArchive archive, TarEntry entry, string message);
}
