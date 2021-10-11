namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SumDeath5" />.
    /// </summary>
    public class SumDeath5 : ISkill
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
            float MATKBonus = 6f;
            int rate = 50;
            int lifetime = 1000;
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(dActor, (short)200, true);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor actor in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, actor))
                {
                    if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Confuse, rate))
                    {
                        Confuse confuse = new Confuse(args.skill, actor, lifetime);
                        SkillHandler.ApplyAddition(actor, (Addition)confuse);
                    }
                    dActor1.Add(actor);
                }
            }
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, Elements.Neutral, MATKBonus);
            if (sActor.type != ActorType.MOB)
                return;
            try
            {
                ActorMob actorMob = (ActorMob)sActor;
                ((MobEventHandler)actorMob.e).AI.Master.Slave.Remove((SagaDB.Actor.Actor)actorMob);
                actorMob.ClearTaskAddition();
                map.DeleteActor((SagaDB.Actor.Actor)actorMob);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
