namespace SagaDB.Skill
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="SkillData" />.
    /// </summary>
    public class SkillData
    {
        /// <summary>
        /// Defines the flag.
        /// </summary>
        public BitMask<SkillFlags> flag = new BitMask<SkillFlags>();

        /// <summary>
        /// Defines the equipFlag.
        /// </summary>
        public BitMask<EquipFlags> equipFlag = new BitMask<EquipFlags>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        public uint id;

        /// <summary>
        /// Defines the name.
        /// </summary>
        public string name;

        /// <summary>
        /// Defines the active.
        /// </summary>
        public bool active;

        /// <summary>
        /// Defines the maxLv.
        /// </summary>
        public byte maxLv;

        /// <summary>
        /// Defines the lv.
        /// </summary>
        public byte lv;

        /// <summary>
        /// Defines the castTime.
        /// </summary>
        public int castTime;

        /// <summary>
        /// Defines the delay.
        /// </summary>
        public int delay;

        /// <summary>
        /// Defines the range.
        /// </summary>
        public byte range;

        /// <summary>
        /// Defines the target.
        /// </summary>
        public byte target;

        /// <summary>
        /// Defines the target2.
        /// </summary>
        public byte target2;

        /// <summary>
        /// Defines the effectRange.
        /// </summary>
        public byte effectRange;

        /// <summary>
        /// Defines the castRange.
        /// </summary>
        public byte castRange;

        /// <summary>
        /// Defines the mp.
        /// </summary>
        public ushort mp;

        /// <summary>
        /// Defines the sp.
        /// </summary>
        public ushort sp;

        /// <summary>
        /// Defines the effect.
        /// </summary>
        public uint effect;

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.name;
        }
    }
}
