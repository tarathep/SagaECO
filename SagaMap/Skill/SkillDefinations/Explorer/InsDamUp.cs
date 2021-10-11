namespace SagaMap.Skill.SkillDefinations.Explorer
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="InsDamUp" />.
    /// </summary>
    public class InsDamUp : ISkill
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
            ushort addValue = new ushort[6]
            {
        (ushort) 0,
        (ushort) 3,
        (ushort) 6,
        (ushort) 9,
        (ushort) 12,
        (ushort) 15
            }[(int)level];
            SomeTypeDamUp someTypeDamUp = new SomeTypeDamUp(args.skill, dActor, nameof(InsDamUp));
            someTypeDamUp.AddMobType(MobType.INSECT, addValue);
            someTypeDamUp.AddMobType(MobType.INSECT_BOSS, addValue);
            someTypeDamUp.AddMobType(MobType.INSECT_BOSS_NOTPTDROPRANGE, addValue);
            someTypeDamUp.AddMobType(MobType.INSECT_BOSS_SKILL, addValue);
            someTypeDamUp.AddMobType(MobType.INSECT_NOTOUCH, addValue);
            someTypeDamUp.AddMobType(MobType.INSECT_NOTPTDROPRANGE, addValue);
            someTypeDamUp.AddMobType(MobType.INSECT_RIDE, addValue);
            someTypeDamUp.AddMobType(MobType.INSECT_SKILL, addValue);
            someTypeDamUp.AddMobType(MobType.INSECT_UNITE, addValue);
            SkillHandler.ApplyAddition(dActor, (Addition)someTypeDamUp);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
        }
    }
}
