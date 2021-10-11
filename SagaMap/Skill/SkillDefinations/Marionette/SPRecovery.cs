namespace SagaMap.Skill.SkillDefinations.Marionette
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="SPRecovery" />.
    /// </summary>
    public class SPRecovery : ISkill
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
            SagaMap.Skill.Additions.Global.SPRecovery spRecovery = new SagaMap.Skill.Additions.Global.SPRecovery(args.skill, dActor, int.MaxValue, 5000, true);
            SkillHandler.ApplyAddition(dActor, (Addition)spRecovery);
        }
    }
}
