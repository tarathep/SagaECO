namespace SagaMap.Mob
{
    /// <summary>
    /// Defines the <see cref="AICommand" />.
    /// </summary>
    public interface AICommand
    {
        /// <summary>
        /// The GetName.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        string GetName();

        /// <summary>
        /// The Update.
        /// </summary>
        /// <param name="para">The para<see cref="object"/>.</param>
        void Update(object para);

        /// <summary>
        /// Gets or sets the Status.
        /// </summary>
        CommandStatus Status { get; set; }

        /// <summary>
        /// The Dispose.
        /// </summary>
        void Dispose();
    }
}
