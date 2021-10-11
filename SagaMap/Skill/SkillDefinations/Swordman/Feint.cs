namespace SagaMap.Skill.SkillDefinations.Swordman
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="Feint" />.
    /// </summary>
    public class Feint : ISkill
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
            int lifetime = (5 + (int)level) * 1000;
            bool flag = false;
            if (dActor.Status.Additions.ContainsKey("Parry"))
            {
                dActor.Status.Additions["Parry"].AdditionEnd();
                flag = true;
            }
            if (dActor.Status.Additions.ContainsKey("Counter"))
            {
                dActor.Status.Additions["Counter"].AdditionEnd();
                flag = true;
            }
            SagaDB.Actor.Actor actor;
            if (flag)
            {
                actor = dActor;
                if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.硬直, 65))
                    return;
            }
            else
                actor = sActor;
            硬直 硬直 = new 硬直(args.skill, actor, lifetime);
            SkillHandler.ApplyAddition(actor, (Addition)硬直);
        }
    }
}
