namespace ICSharpCode.SharpZipLib.Tar
{
    using System;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the <see cref="InvalidHeaderException" />.
    /// </summary>
    [Serializable]
    public class InvalidHeaderException : TarException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHeaderException"/> class.
        /// </summary>
        /// <param name="information">The information<see cref="SerializationInfo"/>.</param>
        /// <param name="context">The context<see cref="StreamingContext"/>.</param>
        protected InvalidHeaderException(SerializationInfo information, StreamingContext context)
      : base(information, context)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHeaderException"/> class.
        /// </summary>
        public InvalidHeaderException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHeaderException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        public InvalidHeaderException(string message)
      : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidHeaderException"/> class.
        /// </summary>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="exception">The exception<see cref="Exception"/>.</param>
        public InvalidHeaderException(string message, Exception exception)
      : base(message, exception)
        {
        }
    }
}
