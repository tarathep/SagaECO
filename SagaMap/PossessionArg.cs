namespace SagaMap
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="PossessionArg" />.
    /// </summary>
    public class PossessionArg : MapEventArgs
    {
        /// <summary>
        /// Defines the cancel.
        /// </summary>
        public bool cancel = false;

        /// <summary>
        /// Defines the fromID.
        /// </summary>
        public uint fromID;

        /// <summary>
        /// Defines the toID.
        /// </summary>
        public uint toID;

        /// <summary>
        /// Defines the result.
        /// </summary>
        public int result;

        /// <summary>
        /// Defines the comment.
        /// </summary>
        public string comment;

        /// <summary>
        /// Defines the x.
        /// </summary>
        public byte x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        public byte y;

        /// <summary>
        /// Defines the dir.
        /// </summary>
        public byte dir;
    }
}
