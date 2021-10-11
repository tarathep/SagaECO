namespace SagaDB.Actor
{
    using SagaDB.Mob;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="ActorPet" />.
    /// </summary>
    public class ActorPet : ActorMob, IStats
    {
        /// <summary>
        /// Defines the owner.
        /// </summary>
        private ActorPC owner;

        /// <summary>
        /// Defines the ride.
        /// </summary>
        private bool ride;

        /// <summary>
        /// Defines the limits.
        /// </summary>
        private MobData limits;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorPet"/> class.
        /// </summary>
        public ActorPet()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorPet"/> class.
        /// </summary>
        /// <param name="mobID">The mobID<see cref="uint"/>.</param>
        /// <param name="pet">The pet<see cref="SagaDB.Item.Item"/>.</param>
        public ActorPet(uint mobID, SagaDB.Item.Item pet)
        {
            this.type = ActorType.PET;
            this.baseData = Singleton<MobFactory>.Instance.GetPetData(mobID);
            this.limits = Singleton<MobFactory>.Instance.GetPetLimit(mobID);
            if ((long)pet.HP > (long)this.limits.hp)
                pet.HP = (short)this.limits.hp;
            this.MaxHP = (uint)((ulong)this.baseData.hp + (ulong)pet.HP);
            this.HP = this.MaxHP;
            this.MaxMP = (uint)((ulong)this.baseData.mp + (ulong)pet.MP);
            this.MP = this.MaxMP;
            this.MaxSP = (uint)((ulong)this.baseData.sp + (ulong)pet.SP);
            this.SP = this.MaxSP;
            this.Name = this.baseData.name;
            this.Speed = this.baseData.speed;
            this.Status.attackType = this.baseData.attackType;
            if ((int)pet.ASPD > (int)this.limits.aspd)
                pet.ASPD = this.limits.aspd;
            this.Status.aspd = (short)((int)this.baseData.aspd + (int)pet.ASPD);
            if ((int)pet.CSPD > (int)this.limits.cspd)
                pet.CSPD = this.limits.cspd;
            this.Status.cspd = (short)((int)this.baseData.cspd + (int)pet.CSPD);
            this.Status.def = this.baseData.def;
            if ((int)pet.Def > (int)this.limits.def_add)
                pet.Def = (short)this.limits.def_add;
            this.Status.def_add = (short)((int)this.baseData.def_add + (int)pet.Def);
            this.Status.mdef = this.baseData.mdef;
            if ((int)pet.MDef > (int)this.limits.mdef_add)
                pet.MDef = (short)this.limits.mdef_add;
            this.Status.mdef_add = (short)((int)this.baseData.mdef_add + (int)pet.MDef);
            if ((int)pet.Atk1 > (int)this.limits.atk_max)
                pet.Atk1 = (short)this.limits.atk_max;
            this.Status.min_atk1 = (ushort)((uint)this.baseData.atk_min + (uint)pet.Atk1);
            this.Status.max_atk1 = (ushort)((uint)this.baseData.atk_max + (uint)pet.Atk1);
            this.Status.min_atk2 = (ushort)((uint)this.baseData.atk_min + (uint)pet.Atk2);
            this.Status.max_atk2 = (ushort)((uint)this.baseData.atk_max + (uint)pet.Atk2);
            this.Status.min_atk3 = (ushort)((uint)this.baseData.atk_min + (uint)pet.Atk3);
            this.Status.max_atk3 = (ushort)((uint)this.baseData.atk_max + (uint)pet.Atk3);
            if ((int)pet.MAtk > (int)this.limits.matk_max)
                pet.MAtk = (short)this.limits.matk_max;
            this.Status.min_matk = (ushort)((uint)this.baseData.matk_min + (uint)pet.MAtk);
            this.Status.max_matk = (ushort)((uint)this.baseData.matk_max + (uint)pet.MAtk);
            if ((int)pet.HitMelee > (int)this.limits.hit_melee)
                pet.HitMelee = (short)this.limits.hit_melee;
            this.Status.hit_melee = (ushort)((uint)this.baseData.hit_melee + (uint)pet.HitMelee);
            if ((int)pet.HitRanged > (int)this.limits.hit_ranged)
                pet.HitRanged = (short)this.limits.hit_ranged;
            this.Status.hit_ranged = (ushort)((uint)this.baseData.hit_ranged + (uint)pet.HitRanged);
            if ((int)pet.AvoidMelee > (int)this.limits.avoid_melee)
                pet.AvoidMelee = (short)this.limits.avoid_melee;
            this.Status.avoid_melee = (ushort)((uint)this.baseData.avoid_melee + (uint)pet.AvoidMelee);
            if ((int)pet.AvoidRanged > (int)this.limits.avoid_ranged)
                pet.AvoidRanged = (short)this.limits.avoid_ranged;
            this.Status.avoid_ranged = (ushort)((uint)this.baseData.avoid_ranged + (uint)pet.AvoidRanged);
            this.sightRange = 1500U;
        }

        /// <summary>
        /// Gets or sets the Owner.
        /// </summary>
        public ActorPC Owner
        {
            get
            {
                return this.owner;
            }
            set
            {
                this.owner = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether Ride.
        /// </summary>
        public bool Ride
        {
            get
            {
                return this.ride;
            }
            set
            {
                this.ride = value;
            }
        }

        /// <summary>
        /// Gets the PetID.
        /// </summary>
        public uint PetID
        {
            get
            {
                return this.BaseData.id;
            }
        }

        /// <summary>
        /// Gets the Limits.
        /// </summary>
        public MobData Limits
        {
            get
            {
                return this.limits;
            }
        }
    }
}
