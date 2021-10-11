namespace SagaMap.Skill.SkillDefinations.Shaman
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="ElementIInfo" />.
    /// </summary>
    public class ElementIInfo : ISkill
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
            Knowledge knowledge = new Knowledge(args.skill, sActor, nameof(ElementIInfo), new MobType[8]
            {
        MobType.ELEMENT,
        MobType.ELEMENT_BOSS_SKILL,
        MobType.ELEMENT_MATERIAL_NOTOUCH_SKILL,
        MobType.ELEMENT_NOTOUCH,
        MobType.ELEMENT_NOTOUCH_SKILL,
        MobType.ELEMENT_NOTPTDROPRANGE,
        MobType.ELEMENT_SKILL,
        MobType.ELEMENT_SKILL_BOSS
            });
            SkillHandler.ApplyAddition(sActor, (Addition)knowledge);
        }
    }
}
