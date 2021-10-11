namespace SagaDB.Actor
{
    using SagaDB.Mob;

    /// <summary>
    /// Defines the <see cref="ActorShadow" />.
    /// </summary>
    public class ActorShadow : ActorPet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ActorShadow"/> class.
        /// </summary>
        /// <param name="creator">The creator<see cref="ActorPC"/>.</param>
        public ActorShadow(ActorPC creator)
        {
            this.baseData = new MobData();
            this.baseData.level = creator.Level;
            this.Status.attackType = creator.Status.attackType;
            this.Status.aspd = creator.Status.aspd;
            this.Status.def = creator.Status.def;
            this.Status.def_add = creator.Status.def_add;
            this.Status.mdef = creator.Status.mdef;
            this.Status.mdef_add = creator.Status.mdef_add;
            this.Status.min_atk1 = creator.Status.min_atk1;
            this.Status.max_atk1 = creator.Status.max_atk1;
            this.Status.min_atk2 = creator.Status.min_atk2;
            this.Status.max_atk2 = creator.Status.max_atk2;
            this.Status.min_atk3 = creator.Status.min_atk3;
            this.Status.max_atk3 = creator.Status.max_atk3;
            this.Status.min_matk = creator.Status.min_matk;
            this.Status.max_matk = creator.Status.max_matk;
            this.Status.hit_melee = creator.Status.hit_melee;
            this.Status.hit_ranged = creator.Status.hit_ranged;
            this.Status.avoid_melee = creator.Status.avoid_melee;
            this.Status.avoid_ranged = creator.Status.avoid_ranged;
            this.MaxHP = 1U;
            this.HP = 1U;
            this.type = ActorType.SHADOW;
            this.sightRange = 1500U;
            this.Owner = creator;
            this.Speed = (ushort)100;
        }
    }
}
