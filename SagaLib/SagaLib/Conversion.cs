namespace SagaLib
{
    /// <summary>
    /// Defines the <see cref="Conversion" />.
    /// </summary>
    public static class Conversion
    {
        /// <summary>
        /// The Hex.
        /// </summary>
        /// <param name="Number">The Number<see cref="byte"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Hex(byte Number)
        {
            return Number.ToString("X");
        }

        /// <summary>
        /// The Hex.
        /// </summary>
        /// <param name="Number">The Number<see cref="uint"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string Hex(uint Number)
        {
            return Number.ToString("X");
        }
    }
}
