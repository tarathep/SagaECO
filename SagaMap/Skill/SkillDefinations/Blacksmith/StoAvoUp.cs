namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="StoAvoUp" />.
    /// </summary>
    public class StoAvoUp : ISkill
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
            SomeTypeAvoUp someTypeAvoUp = new SomeTypeAvoUp(args.skill, dActor, nameof(StoAvoUp));
            someTypeAvoUp.AddMobType(MobType.ROCK, addValue);
            someTypeAvoUp.AddMobType(MobType.ROCK_BOSS_SKILL_NOTPTDROPRANGE, addValue);
            someTypeAvoUp.AddMobType(MobType.ROCK_MATERIAL, addValue);
            someTypeAvoUp.AddMobType(MobType.ROCK_MATERIAL_NORTH_NOTOUCH, addValue);
            someTypeAvoUp.AddMobType(MobType.ROCK_MATERIAL_SKILL, addValue);
            someTypeAvoUp.AddMobType(MobType.ROCK_MATERIAL_SOUTH_NOTOUCH, addValue);
            SkillHandler.ApplyAddition(dActor, (Addition)someTypeAvoUp);
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
