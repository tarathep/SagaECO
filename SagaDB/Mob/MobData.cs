namespace SagaDB.Mob
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MobData" />.
    /// </summary>
    public class MobData
    {
        /// <summary>
        /// Defines the dropItems.
        /// </summary>
        public List<MobData.DropData> dropItems = new List<MobData.DropData>();

        /// <summary>
        /// Defines the dropItemsSpecial.
        /// </summary>
        public List<MobData.DropData> dropItemsSpecial = new List<MobData.DropData>();

        /// <summary>
        /// Defines the elements.
        /// </summary>
        public Dictionary<Elements, int> elements = new Dictionary<Elements, int>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        public uint id;

        /// <summary>
        /// Defines the pictid.
        /// </summary>
        public uint pictid;

        /// <summary>
        /// Defines the name.
        /// </summary>
        public string name;

        /// <summary>
        /// Defines the speed.
        /// </summary>
        public ushort speed;

        /// <summary>
        /// Defines the mobType.
        /// </summary>
        public MobType mobType;

        /// <summary>
        /// Defines the attackType.
        /// </summary>
        public ATTACK_TYPE attackType;

        /// <summary>
        /// Defines the mobSize.
        /// </summary>
        public float mobSize;

        /// <summary>
        /// Defines the fly.
        /// </summary>
        public bool fly;

        /// <summary>
        /// Defines the undead.
        /// </summary>
        public bool undead;

        /// <summary>
        /// Defines the hp.
        /// </summary>
        public uint hp;

        /// <summary>
        /// Defines the mp.
        /// </summary>
        public uint mp;

        /// <summary>
        /// Defines the sp.
        /// </summary>
        public uint sp;

        /// <summary>
        /// Defines the level.
        /// </summary>
        public byte level;

        /// <summary>
        /// Defines the atk_min.
        /// </summary>
        public ushort atk_min;

        /// <summary>
        /// Defines the atk_max.
        /// </summary>
        public ushort atk_max;

        /// <summary>
        /// Defines the matk_min.
        /// </summary>
        public ushort matk_min;

        /// <summary>
        /// Defines the matk_max.
        /// </summary>
        public ushort matk_max;

        /// <summary>
        /// Defines the def.
        /// </summary>
        public ushort def;

        /// <summary>
        /// Defines the def_add.
        /// </summary>
        public ushort def_add;

        /// <summary>
        /// Defines the mdef.
        /// </summary>
        public ushort mdef;

        /// <summary>
        /// Defines the mdef_add.
        /// </summary>
        public ushort mdef_add;

        /// <summary>
        /// Defines the str.
        /// </summary>
        public ushort str;

        /// <summary>
        /// Defines the mag.
        /// </summary>
        public ushort mag;

        /// <summary>
        /// Defines the vit.
        /// </summary>
        public ushort vit;

        /// <summary>
        /// Defines the dex.
        /// </summary>
        public ushort dex;

        /// <summary>
        /// Defines the agi.
        /// </summary>
        public ushort agi;

        /// <summary>
        /// Defines the intel.
        /// </summary>
        public ushort intel;

        /// <summary>
        /// Defines the cri.
        /// </summary>
        public ushort cri;

        /// <summary>
        /// Defines the hit_melee.
        /// </summary>
        public ushort hit_melee;

        /// <summary>
        /// Defines the hit_ranged.
        /// </summary>
        public ushort hit_ranged;

        /// <summary>
        /// Defines the avoid_melee.
        /// </summary>
        public ushort avoid_melee;

        /// <summary>
        /// Defines the avoid_ranged.
        /// </summary>
        public ushort avoid_ranged;

        /// <summary>
        /// Defines the aspd.
        /// </summary>
        public short aspd;

        /// <summary>
        /// Defines the cspd.
        /// </summary>
        public short cspd;

        /// <summary>
        /// Defines the baseExp.
        /// </summary>
        public uint baseExp;

        /// <summary>
        /// Defines the jobExp.
        /// </summary>
        public uint jobExp;

        /// <summary>
        /// Defines the stampDrop.
        /// </summary>
        public MobData.DropData stampDrop;

        /// <summary>
        /// Defines the aiMode.
        /// </summary>
        public int aiMode;

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.name;
        }

        /// <summary>
        /// Defines the <see cref="DropData" />.
        /// </summary>
        public class DropData
        {
            /// <summary>
            /// Defines the ItemID.
            /// </summary>
            public uint ItemID;

            /// <summary>
            /// Defines the TreasureGroup.
            /// </summary>
            public string TreasureGroup;

            /// <summary>
            /// Defines the Rate.
            /// </summary>
            public int Rate;

            /// <summary>
            /// Defines the Party.
            /// </summary>
            public bool Party;
        }
    }
}
