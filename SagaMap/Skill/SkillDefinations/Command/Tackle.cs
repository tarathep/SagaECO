namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="Tackle" />.
    /// </summary>
    public class Tackle : ISkill
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
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)sActor, dActor) ? 0 : -14;
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
        1.1f,
        1.15f,
        1.2f,
        1.25f,
        1.3f
            };
            uint key = 125;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Skills2.ContainsKey(key) && actorPc.Skills2[key].Level == (byte)3)
            {
                numArray[1] = 1.82f;
                numArray[2] = 1.94f;
                numArray[3] = 2.08f;
                numArray[4] = 2.2f;
                numArray[5] = 2.34f;
            }
            if (actorPc.SkillsReserve.ContainsKey(key) && actorPc.SkillsReserve[key].Level == (byte)3)
            {
                numArray[1] = 1.82f;
                numArray[2] = 1.94f;
                numArray[3] = 2.08f;
                numArray[4] = 2.2f;
                numArray[5] = 2.34f;
            }
            float ATKBonus = numArray[(int)level];
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            Singleton<SkillHandler>.Instance.PushBack(sActor, dActor, 1 + (int)level);
        }
    }
}
