namespace SagaMap.Skill.SkillDefinations.Ranger
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="AnimalInfo" />.
    /// </summary>
    public class AnimalInfo : ISkill
    {
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
            Knowledge knowledge = new Knowledge(args.skill, sActor, nameof(AnimalInfo), new MobType[12]
            {
        MobType.ANIMAL,
        MobType.ANIMAL_BOMB_SKILL,
        MobType.ANIMAL_BOSS,
        MobType.ANIMAL_BOSS_SKILL,
        MobType.ANIMAL_BOSS_SKILL_NOTPTDROPRANGE,
        MobType.ANIMAL_NOTOUCH,
        MobType.ANIMAL_NOTPTDROPRANGE,
        MobType.ANIMAL_RIDE,
        MobType.ANIMAL_RIDE_BREEDER,
        MobType.ANIMAL_SKILL,
        MobType.ANIMAL_SPBOSS_SKILL,
        MobType.ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE
            });
            SkillHandler.ApplyAddition(sActor, (Addition)knowledge);
        }
    }
}
