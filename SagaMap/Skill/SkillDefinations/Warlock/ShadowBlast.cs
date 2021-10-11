namespace SagaMap.Skill.SkillDefinations.Warlock
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ShadowBlast" />.
    /// </summary>
    public class ShadowBlast : ISkill
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
            float num = 0.0f;
            switch (level)
            {
                case 1:
                    num = 1.1f;
                    break;
                case 2:
                    num = 1.8f;
                    break;
                case 3:
                    num = 2.5f;
                    break;
                case 4:
                    num = 3.3f;
                    break;
                case 5:
                    num = 4f;
                    break;
            }
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(dActor.MapID).GetActorsArea(dActor, (short)100, true);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                    dActor1.Add(dActor2);
            }
            float MATKBonus = num * (1f / (float)dActor1.Count);
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, Elements.Dark, MATKBonus);
        }
    }
}
