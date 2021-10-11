namespace SevenZip.CommandLineParser
{
    using System.Collections;

    /// <summary>
    /// Defines the <see cref="SwitchResult" />.
    /// </summary>
    public class SwitchResult
    {
        /// <summary>
        /// Defines the PostStrings.
        /// </summary>
        public ArrayList PostStrings = new ArrayList();

        /// <summary>
        /// Defines the ThereIs.
        /// </summary>
        public bool ThereIs;

        /// <summary>
        /// Defines the WithMinus.
        /// </summary>
        public bool WithMinus;

        /// <summary>
        /// Defines the PostCharIndex.
        /// </summary>
        public int PostCharIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchResult"/> class.
        /// </summary>
        public SwitchResult()
        {
            this.ThereIs = false;
        }
    }
}
