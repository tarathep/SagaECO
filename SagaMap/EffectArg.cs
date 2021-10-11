namespace SagaMap
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="EffectArg" />.
    /// </summary>
    public class EffectArg : MapEventArgs
    {
        /// <summary>
        /// Defines the x.
        /// </summary>
        public byte x = byte.MaxValue;

        /// <summary>
        /// Defines the y.
        /// </summary>
        public byte y = byte.MaxValue;

        /// <summary>
        /// Defines the oneTime.
        /// </summary>
        public bool oneTime = true;

        /// <summary>
        /// Defines the actorID.
        /// </summary>
        public uint actorID;

        /// <summary>
        /// Defines the effectID.
        /// </summary>
        public uint effectID;
    }
}
