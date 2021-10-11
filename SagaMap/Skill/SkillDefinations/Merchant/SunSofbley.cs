namespace SagaMap.Skill.SkillDefinations.Merchant
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SunSofbley" />.
    /// </summary>
    public class SunSofbley : ISkill
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
            ActorPC actorPc1 = (ActorPC)sActor;
            if (actorPc1.Party == null)
                return;
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(sActor, (short)100, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                    dActor1.Add(dActor2);
            }
            int num1 = 0;
            foreach (SagaDB.Actor.Actor actor in map.GetActorsArea(sActor, (short)300, false))
            {
                if (actor.type == ActorType.PC)
                {
                    ActorPC actorPc2 = (ActorPC)actor;
                    if (actorPc2.Party != null && (int)actorPc2.Party.ID == (int)actorPc1.Party.ID)
                        num1 += (int)(Math.Floor((double)actorPc2.HP / 15.0) + Math.Floor((double)actorPc2.MP / 5.0) + Math.Floor((double)actorPc2.SP / 5.0));
                }
            }
            float[] numArray = new float[6]
            {
        0.0f,
        0.166f,
        0.2f,
        0.25f,
        0.33f,
        0.5f
            };
            int num2 = (int)((double)sActor.HP * (double)numArray[(int)level] + (double)num1);
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, SkillHandler.DefType.Def, Elements.Neutral, (float)num2, 0, true);
        }
    }
}
