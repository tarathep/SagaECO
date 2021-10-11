namespace SagaMap.Skill.SkillDefinations.Event
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SymbolRepair" />.
    /// </summary>
    public class SymbolRepair : ISkill
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
            return dActor.type == ActorType.MOB && ((MobEventHandler)dActor.e).AI.Mode.Symbol ? 0 : -14;
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
            if (sActor.type != ActorType.PC)
                return;
            ActorPC actorPc = (ActorPC)sActor;
            float num = (float)((int)actorPc.DominionJobLevel * 10);
            if (actorPc.JobType == JobType.BACKPACKER)
                num *= 1.5f;
            if ((double)num > 320.0)
                num = 320f;
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, new List<SagaDB.Actor.Actor>()
      {
        dActor
      }, args, SkillHandler.DefType.IgnoreAll, Elements.Neutral, -num, 0, true);
        }
    }
}
