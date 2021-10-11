namespace SagaMap.Skill.SkillDefinations.Gunner
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ApiBullet" />.
    /// </summary>
    public class ApiBullet : ISkill
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
            float ATKBonus = (float)(1.60000002384186 + 0.400000005960464 * (double)level);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(sActor, (short)900, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            SkillHandler.ActorDirection direction = Singleton<SkillHandler>.Instance.GetDirection(sActor);
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                {
                    int XDiff;
                    int YDiff;
                    Singleton<SkillHandler>.Instance.GetXYDiff(map, sActor, dActor2, out XDiff, out YDiff);
                    switch (direction)
                    {
                        case SkillHandler.ActorDirection.South:
                            if (XDiff == 0 && YDiff < 0)
                            {
                                dActor1.Add(dActor2);
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.West:
                            if (YDiff == 0 && XDiff < 0)
                            {
                                dActor1.Add(dActor2);
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.North:
                            if (XDiff == 0 && YDiff > 0)
                            {
                                dActor1.Add(dActor2);
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.East:
                            if (YDiff == 0 && XDiff > 0)
                            {
                                dActor1.Add(dActor2);
                                break;
                            }
                            break;
                    }
                }
            }
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
