namespace SagaDB.Actor
{
    /// <summary>
    /// Defines the GolemType.
    /// </summary>
    public enum GolemType
    {
        /// <summary>
        /// Defines the Sell.
        /// </summary>
        Sell = 0,

        /// <summary>
        /// Defines the Buy.
        /// </summary>
        Buy = 1,

        /// <summary>
        /// Defines the Plant.
        /// </summary>
        Plant = 3,

        /// <summary>
        /// Defines the Mineral.
        /// </summary>
        Mineral = 4,

        /// <summary>
        /// Defines the Food.
        /// </summary>
        Food = 5,

        /// <summary>
        /// Defines the Magic.
        /// </summary>
        Magic = 6,

        /// <summary>
        /// Defines the TreasureBox.
        /// </summary>
        TreasureBox = 7,

        /// <summary>
        /// Defines the Excavation.
        /// </summary>
        Excavation = 8,

        /// <summary>
        /// Defines the Any.
        /// </summary>
        Any = 9,

        /// <summary>
        /// Defines the Strange.
        /// </summary>
        Strange = 10, // 0x0000000A

        /// <summary>
        /// Defines the None.
        /// </summary>
        None = 255, // 0x000000FF
    }
}
