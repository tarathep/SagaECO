namespace SagaMap.Mob
{
    /// <summary>
    /// Defines the AIFlag.
    /// </summary>
    public enum AIFlag
    {
        /// <summary>
        /// Defines the Normal.
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Defines the Active.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Defines the NoAttack.
        /// </summary>
        NoAttack = 2,

        /// <summary>
        /// Defines the NoMove.
        /// </summary>
        NoMove = 4,

        /// <summary>
        /// Defines the RunAway.
        /// </summary>
        RunAway = 8,

        /// <summary>
        /// Defines the HelpSameType.
        /// </summary>
        HelpSameType = 16, // 0x00000010

        /// <summary>
        /// Defines the HateHeal.
        /// </summary>
        HateHeal = 32, // 0x00000020

        /// <summary>
        /// Defines the HateMagic.
        /// </summary>
        HateMagic = 64, // 0x00000040

        /// <summary>
        /// Defines the Symbol.
        /// </summary>
        Symbol = 128, // 0x00000080

        /// <summary>
        /// Defines the SymbolTrash.
        /// </summary>
        SymbolTrash = 256, // 0x00000100
    }
}
