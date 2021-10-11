namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="PetCastSkill" />.
    /// </summary>
    public class PetCastSkill : ISkill
    {
        /// <summary>
        /// Defines the NextSkillID.
        /// </summary>
        private uint NextSkillID;

        /// <summary>
        /// Defines the MobType.
        /// </summary>
        private string MobType;

        /// <summary>
        /// Initializes a new instance of the <see cref="PetCastSkill"/> class.
        /// </summary>
        /// <param name="NextSkillID">寵物技能ID.</param>
        /// <param name="PetType">The PetType<see cref="string"/>.</param>
        public PetCastSkill(uint NextSkillID, string PetType)
        {
            this.NextSkillID = NextSkillID;
            this.MobType = PetType;
        }

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }

        /// <summary>
        /// The Proc.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            ActorPet pet = Singleton<SkillHandler>.Instance.GetPet(sActor);
            if (pet == null || !Singleton<SkillHandler>.Instance.CheckMobType((ActorMob)pet, this.MobType))
                return;
            Singleton<SkillHandler>.Instance.GetMobAI(pet).CastSkill(this.NextSkillID, level, dActor);
        }
    }
}
