namespace SagaMap.Skill.SkillDefinations.Knight
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Valkyrie" />.
    /// </summary>
    public class Valkyrie : ISkill
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
            float[] numArray = new float[6]
            {
        0.0f,
        1f,
        1.12f,
        1.25f,
        1.37f,
        1.5f
            };
            if ((uint)((double)((long)dActor.MaxHP * (long)(100 - 10 * (int)level)) / 100.0) >= dActor.MaxHP)
                return;
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, new List<SagaDB.Actor.Actor>()
      {
        dActor
      }, args, SkillHandler.DefType.IgnoreRight, Elements.Holy, 0, numArray[(int)level], false);
            硬直 硬直 = new 硬直(args.skill, dActor, 500);
            SkillHandler.ApplyAddition(dActor, (Addition)硬直);
        }
    }
}
