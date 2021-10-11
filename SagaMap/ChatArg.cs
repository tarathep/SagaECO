namespace SagaMap
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="ChatArg" />.
    /// </summary>
    public class ChatArg : MapEventArgs
    {
        /// <summary>
        /// Defines the content.
        /// </summary>
        public string content;

        /// <summary>
        /// Defines the motion.
        /// </summary>
        public MotionType motion;

        /// <summary>
        /// Defines the loop.
        /// </summary>
        public byte loop;

        /// <summary>
        /// Defines the emotion.
        /// </summary>
        public uint emotion;
    }
}
