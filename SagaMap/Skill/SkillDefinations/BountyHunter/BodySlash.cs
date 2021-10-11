namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="BodySlash" />.
    /// </summary>
    public class BodySlash : Slash, ISkill
    {
        /// <summary>
        /// The Proc.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            this.SkillProc(sActor, dActor, args, level, PossessionPosition.CHEST);
        }
    }
}
