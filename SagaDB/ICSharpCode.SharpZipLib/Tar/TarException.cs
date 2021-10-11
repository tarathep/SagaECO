namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the <see cref="TarException" />.
    /// </summary>
    [Serializable]
    public class TarException : SharpZipBaseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TarException"/> class.
        /// </summary>
        /// <param name="info">The info<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected TarException(SerializationInfo info, StreamingContext context)
      : base(info, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TarException"/> class.
        /// </summary>
        public TarException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TarException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public TarException(string message)
      : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TarException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public TarException(string message, Exception exception)
      : base(message, exception)
        {
        }
    }
}
