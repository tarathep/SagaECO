namespace SagaMap.Skill.SkillDefinations.Explorer
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AnimalAnalysis" />.
    /// </summary>
    public class AnimalAnalysis : ISkill
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
                mobTypeList.Add(MobType.ANIMAL);
                mobTypeList.Add(MobType.ANIMAL_BOMB_SKILL);
                mobTypeList.Add(MobType.ANIMAL_BOSS);
                mobTypeList.Add(MobType.ANIMAL_BOSS_SKILL);
                mobTypeList.Add(MobType.ANIMAL_BOSS_SKILL_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.ANIMAL_NOTOUCH);
                mobTypeList.Add(MobType.ANIMAL_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.ANIMAL_RIDE);
                mobTypeList.Add(MobType.ANIMAL_RIDE_BREEDER);
                mobTypeList.Add(MobType.ANIMAL_SKILL);
                mobTypeList.Add(MobType.ANIMAL_SPBOSS_SKILL);
                mobTypeList.Add(MobType.ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.ANIMAL);
                mobTypeList.Add(MobType.ANIMAL_BOMB_SKILL);
                mobTypeList.Add(MobType.ANIMAL_BOSS);
                mobTypeList.Add(MobType.ANIMAL_BOSS_SKILL);
                mobTypeList.Add(MobType.ANIMAL_BOSS_SKILL_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.ANIMAL_NOTOUCH);
                mobTypeList.Add(MobType.ANIMAL_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.ANIMAL_RIDE);
                mobTypeList.Add(MobType.ANIMAL_RIDE_BREEDER);
                mobTypeList.Add(MobType.ANIMAL_SKILL);
                mobTypeList.Add(MobType.ANIMAL_SPBOSS_SKILL);
                mobTypeList.Add(MobType.ANIMAL_SPBOSS_SKILL_NOTPTDROPRANGE);
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
