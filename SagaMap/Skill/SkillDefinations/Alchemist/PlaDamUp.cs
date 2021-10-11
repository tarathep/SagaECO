namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="PlaDamUp" />.
    /// </summary>
    public class PlaDamUp : ISkill
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
            SomeTypeDamUp someTypeDamUp = new SomeTypeDamUp(args.skill, dActor, nameof(PlaDamUp));
            someTypeDamUp.AddMobType(MobType.PLANT, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_BOSS, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_BOSS_SKILL, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_BOSS_SKILL_NOTPTDROPRANGE, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MARK, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_BOSS_MARK, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_EAST, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_EAST_BOSS_SKILL_WALL, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_HETERODOXY, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_NORTH, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_NORTH_BOSS_SKILL_WALL, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_NOTPTDROPRANGE, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_SKILL, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_SOUTH, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_SOUTH_BOSS_SKILL_WALL, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_WEST, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_MATERIAL_WEST_BOSS_SKILL_WALL, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_NOTOUCH, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_NOTPTDROPRANGE, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_SKILL, addValue);
            someTypeDamUp.AddMobType(MobType.PLANT_UNITE, addValue);
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
