namespace SagaMap.Skill.SkillDefinations.Swordman
{
    using SagaDB.Actor;
    using SagaMap.ActorEventHandlers;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Provocation" />.
    /// </summary>
    public class Provocation : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC pc, SagaDB.Actor.Actor dActor, SkillArg args)
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
            if (dActor.type != ActorType.MOB)
                return;
            MobEventHandler e = (MobEventHandler)dActor.e;
            if (e.AI.Hate.ContainsKey(sActor.ActorID))
            {
                Dictionary<uint, uint> hate;
                uint actorId;
                (hate = e.AI.Hate)[actorId = sActor.ActorID] = hate[actorId] + dActor.MaxHP / 5U;
            }
            else
                e.AI.Hate.Add(sActor.ActorID, dActor.MaxHP / 5U);
        }
    }
}
