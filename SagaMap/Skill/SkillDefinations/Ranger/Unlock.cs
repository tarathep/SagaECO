namespace SagaMap.Skill.SkillDefinations.Ranger
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="Unlock" />.
    /// </summary>
    public class Unlock : SagaMap.Skill.SkillDefinations.Global.SkillEvent
    {
        /// <summary>
        /// The RunScript.
        /// </summary>
        /// <param name="para">The para<see cref="SagaMap.Skill.SkillDefinations.Global.SkillEvent.Parameter"/>.</param>
        protected override void RunScript(SagaMap.Skill.SkillDefinations.Global.SkillEvent.Parameter para)
        {
            SagaMap.Scripting.SkillEvent.Instance.OpenTreasureBox((ActorPC)para.sActor);
        }
    }
}
