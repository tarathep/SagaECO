namespace SagaMap.Skill.SkillDefinations.Machinery
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="RobotSparkBall" />.
    /// </summary>
    public class RobotSparkBall : ISkill
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
            ActorPet pet = Singleton<SkillHandler>.Instance.GetPet((SagaDB.Actor.Actor)sActor);
            return pet == null || !Singleton<SkillHandler>.Instance.CheckMobType((ActorMob)pet, "MACHINE_RIDE_ROBOT") ? -53 : 0;
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
            float ATKBonus = (float)(0.649999976158142 + 0.100000001490116 * (double)level);
            short[] numArray1 = new short[6]
            {
        (short) 0,
        (short) 600,
        (short) 600,
        (short) 600,
        (short) 700,
        (short) 700
            };
            int[] numArray2 = new int[6] { 0, 4, 4, 4, 4, 4 };
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, numArray1[(int)level], true);
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor dActor1 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor1))
                {
                    for (int index = 0; index < numArray2[(int)level]; ++index)
                        actorList.Add(dActor1);
                }
            }
            if (actorList.Count <= 0)
                return;
            List<SagaDB.Actor.Actor> dActor2 = new List<SagaDB.Actor.Actor>();
            for (int index = 0; index < actorList.Count; ++index)
                dActor2.Add(actorList[SagaLib.Global.Random.Next(0, actorList.Count - 1)]);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor2, args, Elements.Neutral, ATKBonus);
        }
    }
}
