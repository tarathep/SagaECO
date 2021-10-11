namespace SagaMap.Skill.SkillDefinations.Ranger
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="AniHitUp" />.
    /// </summary>
    public class AniHitUp : ISkill
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
            SomeTypeHitUp someTypeHitUp = new SomeTypeHitUp(args.skill, dActor, nameof(AniHitUp));
            someTypeHitUp.AddMobType(MobType.ANIMAL, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_BOMB_SKILL, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_BOSS, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_BOSS_SKILL, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_BOSS_SKILL_NOTPTDROPRANGE, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_NOTOUCH, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_NOTPTDROPRANGE, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_RIDE, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_RIDE_BREEDER, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_SKILL, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_SPBOSS_SKILL, addValue);
            someTypeHitUp.AddMobType(MobType.ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE, addValue);
            SkillHandler.ApplyAddition(dActor, (Addition)someTypeHitUp);
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
