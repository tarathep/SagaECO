namespace SagaMap.Skill.SkillDefinations.Alchemist
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="PlantAnalysis" />.
    /// </summary>
    public class PlantAnalysis : ISkill
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
            if (dActor.type == ActorType.MOB)
            {
                List<MobType> mobTypeList = new List<MobType>();
                mobTypeList.Add(MobType.PLANT);
                mobTypeList.Add(MobType.PLANT_BOSS);
                mobTypeList.Add(MobType.PLANT_BOSS_SKILL);
                mobTypeList.Add(MobType.PLANT_BOSS_SKILL_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.PLANT_MARK);
                mobTypeList.Add(MobType.PLANT_MATERIAL);
                mobTypeList.Add(MobType.PLANT_MATERIAL_BOSS_MARK);
                mobTypeList.Add(MobType.PLANT_MATERIAL_EAST);
                mobTypeList.Add(MobType.PLANT_MATERIAL_EAST_BOSS_SKILL_WALL);
                mobTypeList.Add(MobType.PLANT_MATERIAL_HETERODOXY);
                mobTypeList.Add(MobType.PLANT_MATERIAL_NORTH);
                mobTypeList.Add(MobType.PLANT_MATERIAL_NORTH_BOSS_SKILL_WALL);
                mobTypeList.Add(MobType.PLANT_MATERIAL_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.PLANT_MATERIAL_SKILL);
                mobTypeList.Add(MobType.PLANT_MATERIAL_SOUTH);
                mobTypeList.Add(MobType.PLANT_MATERIAL_SOUTH_BOSS_SKILL_WALL);
                mobTypeList.Add(MobType.PLANT_MATERIAL_WEST);
                mobTypeList.Add(MobType.PLANT_MATERIAL_WEST_BOSS_SKILL_WALL);
                mobTypeList.Add(MobType.PLANT_NOTOUCH);
                mobTypeList.Add(MobType.PLANT_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.PLANT_SKILL);
                mobTypeList.Add(MobType.PLANT_UNITE);
                ActorMob actorMob = (ActorMob)dActor;
                if (mobTypeList.Contains(actorMob.BaseData.mobType))
                    return 0;
            }
            return -4;
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
            Analysis analysis = new Analysis(args.skill, dActor);
            SkillHandler.ApplyAddition(dActor, (Addition)analysis);
        }
    }
}
