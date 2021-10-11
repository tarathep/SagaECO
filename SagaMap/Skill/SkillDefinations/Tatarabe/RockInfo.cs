namespace SagaMap.Skill.SkillDefinations.Tatarabe
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="RockInfo" />.
    /// </summary>
    public class RockInfo : ISkill
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
            Knowledge knowledge = new Knowledge(args.skill, sActor, nameof(RockInfo), new MobType[14]
            {
        MobType.ROCK,
        MobType.ROCK_BOMB_SKILL,
        MobType.ROCK_BOSS_SKILL_NOTPTDROPRANGE,
        MobType.ROCK_BOSS_SKILL_WALL,
        MobType.ROCK_MATERIAL,
        MobType.ROCK_MATERIAL_BOSS_NOTPTDROPRANGE,
        MobType.ROCK_MATERIAL_BOSS_SKILL_NOTPTDROPRANGE,
        MobType.ROCK_MATERIAL_EAST_NOTOUCH,
        MobType.ROCK_MATERIAL_NORTH_NOTOUCH,
        MobType.ROCK_MATERIAL_SKILL,
        MobType.ROCK_MATERIAL_SOUTH_NOTOUCH,
        MobType.ROCK_MATERIAL_WEST_NOTOUCH,
        MobType.ROCK_NOTPTDROPRANGE,
        MobType.ROCK_SKILL
            });
            SkillHandler.ApplyAddition(sActor, (Addition)knowledge);
        }
    }
}
