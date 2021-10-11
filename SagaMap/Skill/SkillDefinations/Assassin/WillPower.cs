namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="WillPower" />.
    /// </summary>
    public class WillPower : ISkill
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
            ActorPC actorPc = (ActorPC)sActor;
            int[] numArray1 = new int[6] { 0, 1, 1, 2, 2, 3 };
            int[] numArray2 = new int[6] { 0, 30, 50, 30, 50, 50 };
            int num1 = numArray1[(int)level];
            int num2 = numArray2[(int)level];
            int num3 = 0;
            try
            {
                List<string> stringList = new List<string>();
                foreach (KeyValuePair<string, Addition> addition1 in actorPc.Status.Additions)
                {
                    if (num3 < num1)
                    {
                        if (SagaLib.Global.Random.Next(0, 99) < num2)
                        {
                            Addition addition2 = addition1.Value;
                            stringList.Add(addition1.Key);
                            if (addition2.Activated)
                                addition2.AdditionEnd();
                            addition2.Activated = false;
                            ++num3;
                        }
                    }
                    else
                        break;
                }
                foreach (string key in stringList)
                    actorPc.Status.Additions.Remove(key);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
