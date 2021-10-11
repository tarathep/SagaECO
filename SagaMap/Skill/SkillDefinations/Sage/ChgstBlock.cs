namespace SagaMap.Skill.SkillDefinations.Sage
{
    using SagaDB.Actor;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ChgstBlock" />.
    /// </summary>
    public class ChgstBlock : ISkill
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
            int num = 30 + 10 * (int)level;
            if (SagaLib.Global.Random.Next(0, 99) >= num)
                return;
            List<string> stringList = new List<string>();
            foreach (KeyValuePair<string, Addition> addition in dActor.Status.Additions)
                stringList.Add(addition.Value.Name);
            foreach (string name in stringList)
                SkillHandler.RemoveAddition(dActor, name);
        }
    }
}
