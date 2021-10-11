namespace SagaMap.Skill.SkillDefinations
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="ISkill" />.
    /// </summary>
    public interface ISkill
    {
        /// <summary>
        /// The Proc.
        /// </summary>
        /// <param name="sActor">源Actor.</param>
        /// <param name="dActor">目标Actor.</param>
        /// <param name="args">技能参数.</param>
        /// <param name="level">技能等级.</param>
        void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level);

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">源Actor.</param>
        /// <param name="dActor">目标Actor.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>0表示可释放，小于0则为错误代码.</returns>
        int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args);
    }
}
