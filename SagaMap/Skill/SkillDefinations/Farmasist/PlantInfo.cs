namespace SagaMap.Skill.SkillDefinations.Farmasist
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="PlantInfo" />.
    /// </summary>
    public class PlantInfo : ISkill
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
            Knowledge knowledge = new Knowledge(args.skill, sActor, nameof(PlantInfo), new MobType[22]
            {
        MobType.PLANT,
        MobType.PLANT_BOSS,
        MobType.PLANT_BOSS_SKILL,
        MobType.PLANT_BOSS_SKILL_NOTPTDROPRANGE,
        MobType.PLANT_MARK,
        MobType.PLANT_MATERIAL,
        MobType.PLANT_MATERIAL_BOSS_MARK,
        MobType.PLANT_MATERIAL_EAST,
        MobType.PLANT_MATERIAL_EAST_BOSS_SKILL_WALL,
        MobType.PLANT_MATERIAL_HETERODOXY,
        MobType.PLANT_MATERIAL_NORTH,
        MobType.PLANT_MATERIAL_NORTH_BOSS_SKILL_WALL,
        MobType.PLANT_MATERIAL_NOTPTDROPRANGE,
        MobType.PLANT_MATERIAL_SKILL,
        MobType.PLANT_MATERIAL_SOUTH,
        MobType.PLANT_MATERIAL_SOUTH_BOSS_SKILL_WALL,
        MobType.PLANT_MATERIAL_WEST,
        MobType.PLANT_MATERIAL_WEST_BOSS_SKILL_WALL,
        MobType.PLANT_NOTOUCH,
        MobType.PLANT_NOTPTDROPRANGE,
        MobType.PLANT_SKILL,
        MobType.PLANT_UNITE
            });
            SkillHandler.ApplyAddition(sActor, (Addition)knowledge);
        }
    }
}
