namespace ICSharpCode.SharpZipLib.Zip
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the <see cref="ZipException" />.
    /// </summary>
    [Serializable]
    public class ZipException : SharpZipBaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ZipException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected ZipException(SerializationInfo info, StreamingContext context)
      : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipException"/> class.
        /// </summary>
        public ZipException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public ZipException(string message)
      : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public ZipException(string message, Exception exception)
      : base(message, exception)
        {
        }
    }
}
