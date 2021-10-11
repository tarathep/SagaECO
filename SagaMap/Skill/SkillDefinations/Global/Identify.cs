namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="Identify" />.
    /// </summary>
    public class Identify : SkillEvent
    {
        /// <summary>
        /// The RunScript.
        /// </summary>
        /// <param name="para">The para<see cref="SkillEvent.Parameter"/>.</param>
        protected override void RunScript(SkillEvent.Parameter para)
        {
            SagaMap.Scripting.SkillEvent.Instance.Identify((ActorPC)para.sActor);
        }
    }
}
