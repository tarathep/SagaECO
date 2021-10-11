namespace SagaMap.Skill.SkillDefinations.Trader
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="HumanInfo" />.
    /// </summary>
    public class HumanInfo : ISkill
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
            Knowledge knowledge = new Knowledge(args.skill, sActor, nameof(HumanInfo), new MobType[12]
            {
        MobType.HUMAN,
        MobType.HUMAN_BOSS,
        MobType.HUMAN_BOSS_CHAMP,
        MobType.HUMAN_BOSS_SKILL,
        MobType.HUMAN_CHAMP,
        MobType.HUMAN_NOTOUCH,
        MobType.HUMAN_RIDE,
        MobType.HUMAN_SKILL,
        MobType.HUMAN_SKILL_BOSS_CHAMP,
        MobType.HUMAN_SKILL_CHAMP,
        MobType.HUMAN_SMARK_BOSS_HETERODOXY,
        MobType.HUMAN_SMARK_HETERODOXY
            });
            SkillHandler.ApplyAddition(sActor, (Addition)knowledge);
        }
    }
}
