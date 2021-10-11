namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="Synthese" />.
    /// </summary>
    public class Synthese : SkillEvent
    {
        /// <summary>
        /// The RunScript.
        /// </summary>
        /// <param name="para">The para<see cref="SkillEvent.Parameter"/>.</param>
        protected override void RunScript(SkillEvent.Parameter para)
        {
            SagaMap.Scripting.SkillEvent.Instance.Synthese((ActorPC)para.sActor, (ushort)para.args.skill.ID, para.level);
        }
    }
}
