namespace SagaMap
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="MoveArg" />.
    /// </summary>
    public class MoveArg : MapEventArgs
    {
        /// <summary>
        /// Defines the x.
        /// </summary>
        public ushort x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        public ushort y;

        /// <summary>
        /// Defines the dir.
        /// </summary>
        public ushort dir;

        /// <summary>
        /// Defines the type.
        /// </summary>
        public MoveType type;
    }
}
