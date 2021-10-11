namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="RockAnalysis" />.
    /// </summary>
    public class RockAnalysis : ISkill
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
                mobTypeList.Add(MobType.ROCK);
                mobTypeList.Add(MobType.ROCK_BOMB_SKILL);
                mobTypeList.Add(MobType.ROCK_BOSS_SKILL_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.ROCK_BOSS_SKILL_WALL);
                mobTypeList.Add(MobType.ROCK_MATERIAL);
                mobTypeList.Add(MobType.ROCK_MATERIAL_BOSS_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.ROCK_MATERIAL_BOSS_SKILL_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.ROCK_MATERIAL_EAST_NOTOUCH);
                mobTypeList.Add(MobType.ROCK_MATERIAL_NORTH_NOTOUCH);
                mobTypeList.Add(MobType.ROCK_MATERIAL_SKILL);
                mobTypeList.Add(MobType.ROCK_MATERIAL_SOUTH_NOTOUCH);
                mobTypeList.Add(MobType.ROCK_MATERIAL_WEST_NOTOUCH);
                mobTypeList.Add(MobType.ROCK_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.ROCK_SKILL);
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
