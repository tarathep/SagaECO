namespace SagaDB.Actor
{
    using SagaDB.Mob;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="ActorMob" />.
    /// </summary>
    public class ActorMob : SagaDB.Actor.Actor, IStats
    {
        /// <summary>
        /// Defines the baseData.
        /// </summary>
        protected MobData baseData;

        /// <summary>
        /// Gets the MobID.
        /// </summary>
        public uint MobID
        {
            get
            {
                return this.baseData.id;
            }
        }

        /// <summary>
        /// Gets or sets the Str.
        /// </summary>
        public ushort Str
        {
            get
            {
                return this.baseData.str;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the Dex.
        /// </summary>
        public ushort Dex
        {
            get
            {
                return this.baseData.dex;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the Int.
        /// </summary>
        public ushort Int
        {
            get
            {
                return this.baseData.intel;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the Vit.
        /// </summary>
        public ushort Vit
        {
            get
            {
                return this.baseData.vit;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the Agi.
        /// </summary>
        public ushort Agi
        {
            get
            {
                return this.baseData.agi;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the Mag.
        /// </summary>
        public ushort Mag
        {
            get
            {
                return this.baseData.mag;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets the Level.
        /// </summary>
        public override byte Level
        {
            get
            {
                return this.baseData.level;
            }
        }

        /// <summary>
        /// Gets the BaseData.
        /// </summary>
        public MobData BaseData
        {
            get
            {
                return this.baseData;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorMob"/> class.
        /// </summary>
        public ActorMob()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorMob"/> class.
        /// </summary>
        /// <param name="mobID">The mobID<see cref="uint"/>.</param>
        public ActorMob(uint mobID)
        {
            this.type = ActorType.MOB;
            this.baseData = Singleton<MobFactory>.Instance.GetMobData(mobID);
            this.MaxHP = this.baseData.hp;
            this.HP = this.MaxHP;
            this.MaxMP = this.baseData.mp;
            this.MP = this.MaxMP;
            this.MaxSP = this.baseData.sp;
            this.SP = this.MaxSP;
            this.Name = this.baseData.name;
            this.Speed = this.baseData.speed;
            this.Status.attackType = this.baseData.attackType;
            this.Status.aspd = this.baseData.aspd;
            this.Status.cspd = this.baseData.cspd;
            this.Status.def = this.baseData.def;
            this.Status.def_add = (short)this.baseData.def_add;
            this.Status.mdef = this.baseData.mdef;
            this.Status.mdef_add = (short)this.baseData.mdef_add;
            this.Status.min_atk1 = this.baseData.atk_min;
            this.Status.max_atk1 = this.baseData.atk_max;
            this.Status.min_atk2 = this.baseData.atk_min;
            this.Status.max_atk2 = this.baseData.atk_max;
            this.Status.min_atk3 = this.baseData.atk_min;
            this.Status.max_atk3 = this.baseData.atk_max;
            this.Status.min_matk = this.baseData.matk_min;
            this.Status.max_matk = this.baseData.matk_max;
            foreach (Elements key in this.baseData.elements.Keys)
            {
                this.Elements[key] = this.baseData.elements[key];
                this.AttackElements[key] = this.baseData.elements[key];
            }
            this.Status.hit_melee = this.baseData.hit_melee;
            this.Status.hit_ranged = this.baseData.hit_ranged;
            this.Status.avoid_melee = this.baseData.avoid_melee;
            this.Status.avoid_ranged = this.baseData.avoid_ranged;
            this.Status.undead = this.baseData.undead;
        }
    }
}
