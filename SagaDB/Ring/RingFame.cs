namespace SagaDB.Ring
{
    /// <summary>
    /// Defines the <see cref="RingFame" />.
    /// </summary>
    public class RingFame
    {
        /// <summary>
        /// Defines the level.
        /// </summary>
        private uint level;

        /// <summary>
        /// Defines the fame.
        /// </summary>
        private uint fame;

        /// <summary>
        /// Gets or sets the Level.
        /// </summary>
        public uint Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }

        /// <summary>
        /// Gets or sets the Fame.
        /// </summary>
        public uint Fame
        {
            get
            {
                return this.fame;
            }
            set
            {
                this.fame = value;
            }
        }
    }
}
