namespace SagaLogin.Configurations
{
    using System;

    /// <summary>
    /// Defines the <see cref="StartupSetting" />.
    /// </summary>
    [Serializable]
    public class StartupSetting
    {
        /// <summary>
        /// Defines the Str.
        /// </summary>
        public ushort Str;

        /// <summary>
        /// Defines the Dex.
        /// </summary>
        public ushort Dex;

        /// <summary>
        /// Defines the Int.
        /// </summary>
        public ushort Int;

        /// <summary>
        /// Defines the Vit.
        /// </summary>
        public ushort Vit;

        /// <summary>
        /// Defines the Agi.
        /// </summary>
        public ushort Agi;

        /// <summary>
        /// Defines the Mag.
        /// </summary>
        public ushort Mag;

        /// <summary>
        /// Defines the StartMap.
        /// </summary>
        public uint StartMap;

        /// <summary>
        /// Defines the X.
        /// </summary>
        public byte X;

        /// <summary>
        /// Defines the Y.
        /// </summary>
        public byte Y;

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return string.Format("Stats:[S:{0},D:{1},I:{2},V:{3},A:{4},M:{5}\r\n       StartPoint:[{6}({7},{8})]", (object)this.Str, (object)this.Dex, (object)this.Int, (object)this.Vit, (object)this.Agi, (object)this.Mag, (object)this.StartMap, (object)this.X, (object)this.Y);
        }
    }
}
