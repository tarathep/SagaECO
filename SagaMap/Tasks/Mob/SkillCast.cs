namespace SagaMap.Tasks.Mob
{
    using global::System;
    using SagaLib;
    using SagaMap.Mob;

    /// <summary>
    /// Defines the <see cref="SkillCast" />.
    /// </summary>
    public class SkillCast : MultiRunTask
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private MobAI client;

        /// <summary>
        /// Defines the skill.
        /// </summary>
        private SkillArg skill;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillCast"/> class.
        /// </summary>
        /// <param name="ai">The ai<see cref="MobAI"/>.</param>
        /// <param name="skill">The skill<see cref="SkillArg"/>.</param>
        public SkillCast(MobAI ai, SkillArg skill)
        {
            if (skill.argType == SkillArg.ArgType.Cast)
            {
                this.dueTime = (int)skill.delay;
                this.period = (int)skill.delay;
            }
            this.client = ai;
            this.skill = skill;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            ClientManager.EnterCriticalArea();
            try
            {
                this.client.Mob.Tasks.Remove(nameof(SkillCast));
                if (this.skill.argType == SkillArg.ArgType.Cast)
                    this.client.OnSkillCastComplete(this.skill);
                this.Deactivate();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
