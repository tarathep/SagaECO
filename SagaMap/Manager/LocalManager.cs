namespace SagaMap.Manager
{
    using SagaLib;
    using SagaMap.Localization;
    using SagaMap.Localization.Languages;

    /// <summary>
    /// Defines the <see cref="LocalManager" />.
    /// </summary>
    public class LocalManager : Singleton<LocalManager>
    {
        /// <summary>
        /// Defines the stringset.
        /// </summary>
        private Strings stringset = (Strings)new English();

        /// <summary>
        /// Defines the lan.
        /// </summary>
        private LocalManager.Languages lan = LocalManager.Languages.English;

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return string.Format("{0}({1})", (object)this.stringset.LocalName, (object)this.stringset.EnglishName);
        }

        /// <summary>
        /// Gets or sets the CurrentLanguage.
        /// </summary>
        public LocalManager.Languages CurrentLanguage
        {
            get
            {
                return this.lan;
            }
            set
            {
                switch (value)
                {
                    case LocalManager.Languages.English:
                        this.stringset = (Strings)new English();
                        break;
                    case LocalManager.Languages.Chinese:
                        this.stringset = (Strings)new Chinese();
                        break;
                    case LocalManager.Languages.TChinese:
                        this.stringset = (Strings)new TChinese();
                        break;
                    case LocalManager.Languages.Japanese:
                        this.stringset = (Strings)new Japanese();
                        break;
                    default:
                        this.stringset = (Strings)new English();
                        break;
                }
                this.lan = value;
            }
        }

        /// <summary>
        /// Gets the Strings.
        /// </summary>
        public Strings Strings
        {
            get
            {
                return this.stringset;
            }
        }

        /// <summary>
        /// Defines the Languages.
        /// </summary>
        public enum Languages
        {
            /// <summary>
            /// Defines the English.
            /// </summary>
            English,

            /// <summary>
            /// Defines the Chinese.
            /// </summary>
            Chinese,

            /// <summary>
            /// Defines the TChinese.
            /// </summary>
            TChinese,

            /// <summary>
            /// Defines the Japanese.
            /// </summary>
            Japanese,
        }
    }
}
