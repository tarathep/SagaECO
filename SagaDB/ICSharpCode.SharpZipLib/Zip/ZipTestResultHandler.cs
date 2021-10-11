namespace ICSharpCode.SharpZipLib.Zip
{
    /// <summary>
    /// The ZipTestResultHandler.
    /// </summary>
    /// <param name="status">The status<see cref="TestStatus"/>.</param>
    /// <param name="message">The message<see cref="string"/>.</param>
    public delegate void ZipTestResultHandler(TestStatus status, string message);
}
