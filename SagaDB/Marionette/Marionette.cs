namespace SagaDB.Marionette
{
    using SagaDB.Mob;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Marionette" />.
    /// </summary>
    public class Marionette
    {
        /// <summary>
        /// Defines the skills.
        /// </summary>
        public List<ushort> skills = new List<ushort>();

        /// <summary>
        /// Defines the gather.
        /// </summary>
        public Dictionary<GatherType, bool> gather = new Dictionary<GatherType, bool>();

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the picID.
        /// </summary>
        private uint picID;

        /// <summary>
        /// Defines the type.
        /// </summary>
        private MobType type;

        /// <summary>
        /// Defines the duration.
        /// </summary>
        private int duration;

        /// <summary>
        /// Defines the delay.
        /// </summary>
        private int delay;

        /// <summary>
        /// Defines the str.
        /// </summary>
        public short str;

        /// <summary>
        /// Defines the dex.
        /// </summary>
        public short dex;

        /// <summary>
        /// Defines the vit.
        /// </summary>
        public short vit;

        /// <summary>
        /// Defines the intel.
        /// </summary>
        public short intel;

        /// <summary>
        /// Defines the agi.
        /// </summary>
        public short agi;

        /// <summary>
        /// Defines the mag.
        /// </summary>
        public short mag;

        /// <summary>
        /// Defines the hp.
        /// </summary>
        public short hp;

        /// <summary>
        /// Defines the mp.
        /// </summary>
        public short mp;

        /// <summary>
        /// Defines the sp.
        /// </summary>
        public short sp;

        /// <summary>
        /// Defines the move_speed.
        /// </summary>
        public short move_speed;

        /// <summary>
        /// Defines the min_atk1.
        /// </summary>
        public short min_atk1;

        /// <summary>
        /// Defines the min_atk2.
        /// </summary>
        public short min_atk2;

        /// <summary>
        /// Defines the min_atk3.
        /// </summary>
        public short min_atk3;

        /// <summary>
        /// Defines the max_atk1.
        /// </summary>
        public short max_atk1;

        /// <summary>
        /// Defines the max_atk2.
        /// </summary>
        public short max_atk2;

        /// <summary>
        /// Defines the max_atk3.
        /// </summary>
        public short max_atk3;

        /// <summary>
        /// Defines the min_matk.
        /// </summary>
        public short min_matk;

        /// <summary>
        /// Defines the max_matk.
        /// </summary>
        public short max_matk;

        /// <summary>
        /// Defines the def.
        /// </summary>
        public short def;

        /// <summary>
        /// Defines the def_add.
        /// </summary>
        public short def_add;

        /// <summary>
        /// Defines the mdef.
        /// </summary>
        public short mdef;

        /// <summary>
        /// Defines the mdef_add.
        /// </summary>
        public short mdef_add;

        /// <summary>
        /// Defines the hit_melee.
        /// </summary>
        public short hit_melee;

        /// <summary>
        /// Defines the hit_ranged.
        /// </summary>
        public short hit_ranged;

        /// <summary>
        /// Defines the hit_magic.
        /// </summary>
        public short hit_magic;

        /// <summary>
        /// Defines the hit_cri.
        /// </summary>
        public short hit_cri;

        /// <summary>
        /// Defines the avoid_melee.
        /// </summary>
        public short avoid_melee;

        /// <summary>
        /// Defines the avoid_ranged.
        /// </summary>
        public short avoid_ranged;

        /// <summary>
        /// Defines the avoid_magic.
        /// </summary>
        public short avoid_magic;

        /// <summary>
        /// Defines the avoid_cri.
        /// </summary>
        public short avoid_cri;

        /// <summary>
        /// Defines the hp_recover.
        /// </summary>
        public short hp_recover;

        /// <summary>
        /// Defines the mp_recover.
        /// </summary>
        public short mp_recover;

        /// <summary>
        /// Defines the aspd.
        /// </summary>
        public short aspd;

        /// <summary>
        /// Defines the cspd.
        /// </summary>
        public short cspd;

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public uint ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        /// <summary>
        /// Gets or sets the PictID.
        /// </summary>
        public uint PictID
        {
            get
            {
                return this.picID;
            }
            set
            {
                this.picID = value;
            }
        }

        /// <summary>
        /// Gets or sets the MobType.
        /// </summary>
        public MobType MobType
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        /// <summary>
        /// Gets or sets the Duration.
        /// </summary>
        public int Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the Delay.
        /// </summary>
        public int Delay
        {
            get
            {
                return this.delay;
            }
            set
            {
                this.delay = value;
            }
        }

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
