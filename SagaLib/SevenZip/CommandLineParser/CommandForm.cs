namespace SevenZip.CommandLineParser
{
    /// <summary>
    /// Defines the <see cref="CommandForm" />.
    /// </summary>
    public class CommandForm
    {
        /// <summary>
        /// Defines the IDString.
        /// </summary>
        public string IDString = "";

        /// <summary>
        /// Defines the PostStringMode.
        /// </summary>
        public bool PostStringMode;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandForm"/> class.
        /// </summary>
        /// <param name="idString">The idString<see cref="string"/>.</param>
        /// <param name="postStringMode">The postStringMode<see cref="bool"/>.</param>
        public CommandForm(string idString, bool postStringMode)
        {
            this.IDString = idString;
            this.PostStringMode = postStringMode;
        }
    }
}
