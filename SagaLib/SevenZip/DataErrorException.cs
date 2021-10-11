namespace SevenZip
{
    using System;

    /// <summary>
    /// The exception that is thrown when an error in input stream occurs during decoding.
    /// </summary>
    internal class DataErrorException : ApplicationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataErrorException"/> class.
        /// </summary>
        public DataErrorException()
      : base("Data Error")
        {
        }
    }
}
