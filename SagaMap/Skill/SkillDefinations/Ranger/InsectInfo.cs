namespace SagaMap.Skill.SkillDefinations.Ranger
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="InsectInfo" />.
    /// </summary>
    public class InsectInfo : ISkill
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
            Knowledge knowledge = new Knowledge(args.skill, sActor, nameof(InsectInfo), new MobType[9]
            {
        MobType.INSECT,
        MobType.INSECT_BOSS,
        MobType.INSECT_BOSS_NOTPTDROPRANGE,
        MobType.INSECT_BOSS_SKILL,
        MobType.INSECT_NOTOUCH,
        MobType.INSECT_NOTPTDROPRANGE,
        MobType.INSECT_RIDE,
        MobType.INSECT_SKILL,
        MobType.INSECT_UNITE
            });
            SkillHandler.ApplyAddition(sActor, (Addition)knowledge);
        }
    }
}
