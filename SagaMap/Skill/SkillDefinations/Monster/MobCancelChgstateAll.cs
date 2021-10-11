namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MobCancelChgstateAll" />.
    /// </summary>
    public class MobCancelChgstateAll : ISkill
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
            int num = 30;
            foreach (SagaDB.Actor.Actor actor in Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)100, false))
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, actor) && SagaLib.Global.Random.Next(0, 99) < num)
                {
                    List<Addition> additionList = new List<Addition>();
                    foreach (KeyValuePair<string, Addition> addition1 in actor.Status.Additions)
                    {
                        Addition addition2 = addition1.Value;
                        additionList.Add(addition2);
                    }
                    foreach (Addition addition in additionList)
                    {
                        if (addition.Activated)
                            SkillHandler.RemoveAddition(actor, addition);
                    }
                }
            }
        }
    }
}
