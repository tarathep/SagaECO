namespace SagaMap.Mob
{
    /// <summary>
    /// Defines the <see cref="MapNode" />.
    /// </summary>
    public class MapNode
    {
        /// <summary>
        /// Defines the G.
        /// </summary>
        public int G;

        /// <summary>
        /// Defines the H.
        /// </summary>
        public int H;

        /// <summary>
        /// Defines the F.
        /// </summary>
        public int F;

        /// <summary>
        /// Defines the Previous.
        /// </summary>
        public MapNode Previous;

        /// <summary>
        /// Defines the x.
        /// </summary>
        public byte x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        public byte y;
    }
}
