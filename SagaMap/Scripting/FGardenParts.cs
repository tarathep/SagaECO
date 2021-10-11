namespace SagaMap.Scripting
{
    /// <summary>
    /// Defines the FGardenParts.
    /// </summary>
    public enum FGardenParts
    {
        /// <summary>
        /// Defines the Foundation.
        /// </summary>
        Foundation = 1,

        /// <summary>
        /// Defines the Engine.
        /// </summary>
        Engine = 2,

        /// <summary>
        /// Defines the Sail1.
        /// </summary>
        Sail1 = 4,

        /// <summary>
        /// Defines the Sail2.
        /// </summary>
        Sail2 = 8,

        /// <summary>
        /// Defines the Sail3.
        /// </summary>
        Sail3 = 16, // 0x00000010

        /// <summary>
        /// Defines the Sail4.
        /// </summary>
        Sail4 = 32, // 0x00000020

        /// <summary>
        /// Defines the Sail5.
        /// </summary>
        Sail5 = 64, // 0x00000040

        /// <summary>
        /// Defines the SailComplete.
        /// </summary>
        SailComplete = 128, // 0x00000080

        /// <summary>
        /// Defines the Wheel.
        /// </summary>
        Wheel = 256, // 0x00000100

        /// <summary>
        /// Defines the Steer.
        /// </summary>
        Steer = 512, // 0x00000200

        /// <summary>
        /// Defines the Catalyst.
        /// </summary>
        Catalyst = 1024, // 0x00000400
    }
}
