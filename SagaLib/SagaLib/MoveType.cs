namespace SagaLib
{
    /// <summary>
    /// Defines the MoveType.
    /// </summary>
    public enum MoveType
    {
        /// <summary>
        /// Defines the NONE.
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Defines the CHANGE_DIR.
        /// </summary>
        CHANGE_DIR = 1,

        /// <summary>
        /// Defines the WALK.
        /// </summary>
        WALK = 6,

        /// <summary>
        /// Defines the RUN.
        /// </summary>
        RUN = 7,

        /// <summary>
        /// Defines the FORCE_MOVEMENT.
        /// </summary>
        FORCE_MOVEMENT = 8,

        /// <summary>
        /// Defines the WARP.
        /// </summary>
        WARP = 14, // 0x0000000E
    }
}
