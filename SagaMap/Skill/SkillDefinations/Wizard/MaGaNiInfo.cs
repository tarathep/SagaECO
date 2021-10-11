namespace SagaMap.Skill.SkillDefinations.Wizard
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MaGaNiInfo" />.
    /// </summary>
    public class MaGaNiInfo : ISkill
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
            Knowledge knowledge = new Knowledge(args.skill, sActor, nameof(MaGaNiInfo), new MobType[10]
            {
        MobType.MAGIC_CREATURE,
        MobType.MAGIC_CREATURE_BOSS,
        MobType.MAGIC_CREATURE_BOSS_SKILL,
        MobType.MAGIC_CREATURE_BOSS_SKILL_NOTPTDROPRANGE,
        MobType.MAGIC_CREATURE_LVDIFF,
        MobType.MAGIC_CREATURE_MATERIAL,
        MobType.MAGIC_CREATURE_NOTOUCH,
        MobType.MAGIC_CREATURE_NOTPTDROPRANGE,
        MobType.MAGIC_CREATURE_RIDE,
        MobType.MAGIC_CREATURE_SKILL
            });
            SkillHandler.ApplyAddition(sActor, (Addition)knowledge);
        }
    }
}
