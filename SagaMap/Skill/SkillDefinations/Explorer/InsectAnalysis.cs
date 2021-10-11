namespace SagaMap.Skill.SkillDefinations.Explorer
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="InsectAnalysis" />.
    /// </summary>
    public class InsectAnalysis : ISkill
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
                mobTypeList.Add(MobType.INSECT);
                mobTypeList.Add(MobType.INSECT_BOSS);
                mobTypeList.Add(MobType.INSECT_BOSS_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.INSECT_BOSS_SKILL);
                mobTypeList.Add(MobType.INSECT_NOTOUCH);
                mobTypeList.Add(MobType.INSECT_NOTPTDROPRANGE);
                mobTypeList.Add(MobType.INSECT_RIDE);
                mobTypeList.Add(MobType.INSECT_SKILL);
                mobTypeList.Add(MobType.INSECT_UNITE);
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
