namespace SagaLogin.Configurations
{
    using SagaDB.Item;
    using System;

    /// <summary>
    /// Defines the <see cref="StartItem" />.
    /// </summary>
    [Serializable]
    public class StartItem
    {
        /// <summary>
        /// Defines the ItemID.
        /// </summary>
        public uint ItemID;

        /// <summary>
        /// Defines the Slot.
        /// </summary>
        public ContainerType Slot;

        /// <summary>
        /// Defines the Count.
        /// </summary>
        public byte Count;
    }
}
