namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="EventMoveSkill" />.
    /// </summary>
    public class EventMoveSkill : ISkill
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
            uint num = 14110000;
            foreach (KeyValuePair<uint, SagaDB.Actor.Actor> actor in Singleton<MapManager>.Instance.GetMap(sActor.MapID).Actors)
            {
                if (actor.Value.type == ActorType.MOB)
                {
                    ActorMob actorMob = (ActorMob)actor.Value;
                    if ((int)actorMob.BaseData.id == (int)num)
                    {
                        Singleton<SkillHandler>.Instance.AttractMob(sActor, (SagaDB.Actor.Actor)actorMob);
                        break;
                    }
                }
            }
        }
    }
}
