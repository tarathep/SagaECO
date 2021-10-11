namespace SagaMap.Skill.SkillDefinations.Event
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Gravity" />.
    /// </summary>
    public class Gravity : ISkill
    {
        /// <summary>
        /// Defines the range.
        /// </summary>
        public List<int> range = new List<int>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Gravity"/> class.
        /// </summary>
        public Gravity()
        {
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 0, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 0, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 0, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 0, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 1, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 1, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 1, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 2, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -1, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -1, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -1, 2));
            this.range.Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -2, 2));
        }

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
            level = (byte)5;
            float ATKBonus = (float)(2.0 + 0.5 * (double)level);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(sActor, (short)200, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor actor in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, actor))
                {
                    int XDiff;
                    int YDiff;
                    Singleton<SkillHandler>.Instance.GetXYDiff(map, sActor, actor, out XDiff, out YDiff);
                    if (this.range.Contains(Singleton<SkillHandler>.Instance.CalcPosHashCode(XDiff, YDiff, 2)))
                    {
                        dActor1.Add(actor);
                        Singleton<SkillHandler>.Instance.PushBack(sActor, actor, 4);
                    }
                }
            }
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
