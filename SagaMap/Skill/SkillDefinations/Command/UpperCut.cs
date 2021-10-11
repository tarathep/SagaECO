namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="UpperCut" />.
    /// </summary>
    public class UpperCut : ISkill
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
        1.2f,
        1.6f,
        2f,
        2.4f,
        2.8f
            };
            uint key = 125;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Skills2.ContainsKey(key) && actorPc.Skills2[key].Level == (byte)3)
            {
                numArray[1] = 1.56f;
                numArray[2] = 2.08f;
                numArray[3] = 2.6f;
                numArray[4] = 3.12f;
                numArray[5] = 3.64f;
            }
            if (actorPc.SkillsReserve.ContainsKey(key) && actorPc.SkillsReserve[key].Level == (byte)3)
            {
                numArray[1] = 1.56f;
                numArray[2] = 2.08f;
                numArray[3] = 2.6f;
                numArray[4] = 3.12f;
                numArray[5] = 3.64f;
            }
            float ATKBonus = numArray[(int)level];
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            Singleton<SkillHandler>.Instance.PushBack(sActor, dActor, 3);
            int num = 15 + 5 * (int)level;
            if (SagaLib.Global.Random.Next(0, 99) >= num)
                return;
            Stun stun = new Stun(args.skill, dActor, 1500);
            SkillHandler.ApplyAddition(dActor, (Addition)stun);
        }
    }
}
