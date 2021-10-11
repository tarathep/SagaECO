namespace SevenZip.CommandLineParser
{
    /// <summary>
    /// Defines the <see cref="SwitchForm" />.
    /// </summary>
    public class SwitchForm
    {
        /// <summary>
        /// Defines the IDString.
        /// </summary>
        public string IDString;

        /// <summary>
        /// Defines the Type.
        /// </summary>
        public SwitchType Type;

        /// <summary>
        /// Defines the Multi.
        /// </summary>
        public bool Multi;

        /// <summary>
        /// Defines the MinLen.
        /// </summary>
        public int MinLen;

        /// <summary>
        /// Defines the MaxLen.
        /// </summary>
        public int MaxLen;

        /// <summary>
        /// Defines the PostCharSet.
        /// </summary>
        public string PostCharSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchForm"/> class.
        /// </summary>
        /// <param name="idString">The idString<see cref="string"/>.</param>
        /// <param name="type">The type<see cref="SwitchType"/>.</param>
        /// <param name="multi">The multi<see cref="bool"/>.</param>
        /// <param name="minLen">The minLen<see cref="int"/>.</param>
        /// <param name="maxLen">The maxLen<see cref="int"/>.</param>
        /// <param name="postCharSet">The postCharSet<see cref="string"/>.</param>
        public SwitchForm(string idString, SwitchType type, bool multi, int minLen, int maxLen, string postCharSet)
        {
            this.IDString = idString;
            this.Type = type;
            this.Multi = multi;
            this.MinLen = minLen;
            this.MaxLen = maxLen;
            this.PostCharSet = postCharSet;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchForm"/> class.
        /// </summary>
        /// <param name="idString">The idString<see cref="string"/>.</param>
        /// <param name="type">The type<see cref="SwitchType"/>.</param>
        /// <param name="multi">The multi<see cref="bool"/>.</param>
        /// <param name="minLen">The minLen<see cref="int"/>.</param>
        public SwitchForm(string idString, SwitchType type, bool multi, int minLen)
      : this(idString, type, multi, minLen, 0, "")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SwitchForm"/> class.
        /// </summary>
        /// <param name="idString">The idString<see cref="string"/>.</param>
        /// <param name="type">The type<see cref="SwitchType"/>.</param>
        /// <param name="multi">The multi<see cref="bool"/>.</param>
        public SwitchForm(string idString, SwitchType type, bool multi)
      : this(idString, type, multi, 0)
        {
        }
    }
}
