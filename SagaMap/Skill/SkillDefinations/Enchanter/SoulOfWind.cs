namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    using SagaDB.Actor;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SoulOfWind" />.
    /// </summary>
    public class SoulOfWind : ISkill
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
            foreach (KeyValuePair<string, Addition> addition in dActor.Status.Additions)
            {
                string key = addition.Key;
                if (key.IndexOf("Cancel") >= 0)
                    this.ExtendCancelTypeAddition(dActor, key, level);
            }
        }

        /// <summary>
        /// The ExtendCancelTypeAddition.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="additionName">The additionName<see cref="string"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public void ExtendCancelTypeAddition(SagaDB.Actor.Actor actor, string additionName, byte level)
        {
            if (!actor.Status.Additions.ContainsKey(additionName))
                return;
            actor.Status.Additions[additionName].TotalLifeTime += 5000 * (int)level;
        }
    }
}
