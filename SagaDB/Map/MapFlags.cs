namespace SagaDB.Map
{
    /// <summary>
    /// Defines the MapFlags.
    /// </summary>
    public enum MapFlags
    {
        /// <summary>
        /// Defines the Healing.
        /// </summary>
        Healing = 1,

        /// <summary>
        /// Defines the Cold.
        /// </summary>
        Cold = 2,

        /// <summary>
        /// Defines the Hot.
        /// </summary>
        Hot = 4,

        /// <summary>
        /// Defines the Wet.
        /// </summary>
        Wet = 8,

        /// <summary>
        /// Defines the Wrp.
        /// </summary>
        Wrp = 16, // 0x00000010

        /// <summary>
        /// Defines the Dominion.
        /// </summary>
        Dominion = 32, // 0x00000020

        /// <summary>
        /// Defines the FGarden.
        /// </summary>
        FGarden = 64, // 0x00000040
    }
}
