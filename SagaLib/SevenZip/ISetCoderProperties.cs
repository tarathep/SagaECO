namespace SevenZip
{
    /// <summary>
    /// Defines the <see cref="ISetCoderProperties" />.
    /// </summary>
    public interface ISetCoderProperties
    {
        /// <summary>
        /// The SetCoderProperties.
        /// </summary>
        /// <param name="propIDs">The propIDs<see cref="CoderPropID[]"/>.</param>
        /// <param name="properties">The properties<see cref="object[]"/>.</param>
        void SetCoderProperties(CoderPropID[] propIDs, object[] properties);
    }
}
