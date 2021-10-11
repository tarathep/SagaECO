namespace SagaMap.Skill.SkillDefinations.Vates
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="UndeadInfo" />.
    /// </summary>
    public class UndeadInfo : ISkill
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
            Knowledge knowledge = new Knowledge(args.skill, sActor, nameof(UndeadInfo), new MobType[9]
            {
        MobType.UNDEAD,
        MobType.UNDEAD_BOSS,
        MobType.UNDEAD_BOSS_BOMB_SKILL,
        MobType.UNDEAD_BOSS_CHAMP_BOMB_SKILL_NOTPTDROPRANGE,
        MobType.UNDEAD_BOSS_SKILL,
        MobType.UNDEAD_BOSS_SKILL_CHAMP,
        MobType.UNDEAD_BOSS_SKILL_NOTPTDROPRANGE,
        MobType.UNDEAD_NOTOUCH,
        MobType.UNDEAD_SKILL
            });
            SkillHandler.ApplyAddition(sActor, (Addition)knowledge);
        }
    }
}
