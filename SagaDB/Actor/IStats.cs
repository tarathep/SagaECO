namespace SagaDB.Actor
{
    /// <summary>
    /// Defines the <see cref="IStats" />.
    /// </summary>
    public interface IStats
    {
        /// <summary>
        /// Gets or sets the Str.
        /// </summary>
        ushort Str { get; set; }

        /// <summary>
        /// Gets or sets the Dex.
        /// </summary>
        ushort Dex { get; set; }

        /// <summary>
        /// Gets or sets the Int.
        /// </summary>
        ushort Int { get; set; }

        /// <summary>
        /// Gets or sets the Vit.
        /// </summary>
        ushort Vit { get; set; }

        /// <summary>
        /// Gets or sets the Agi.
        /// </summary>
        ushort Agi { get; set; }

        /// <summary>
        /// Gets or sets the Mag.
        /// </summary>
        ushort Mag { get; set; }
    }
}
