namespace SagaMap.Tasks.PC
{
    using global::System;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="SkillCast" />.
    /// </summary>
    public class SkillCast : MultiRunTask
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private MapClient client;

        /// <summary>
        /// Defines the skill.
        /// </summary>
        private SkillArg skill;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillCast"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="skill">The skill<see cref="SkillArg"/>.</param>
        public SkillCast(MapClient client, SkillArg skill)
        {
            if (skill.argType == SkillArg.ArgType.Cast)
            {
                this.dueTime = (int)skill.delay;
                this.period = (int)skill.delay;
            }
            else if (skill.argType == SkillArg.ArgType.Item_Cast)
            {
                this.dueTime = (int)skill.item.BaseData.cast;
                this.period = (int)skill.item.BaseData.cast;
            }
            this.client = client;
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
                this.client.Character.Tasks.Remove(nameof(SkillCast));
                if (this.skill.argType == SkillArg.ArgType.Cast)
                    this.client.OnSkillCastComplete(this.skill);
                if (this.skill.argType == SkillArg.ArgType.Item_Cast)
                    this.client.OnItemCastComplete(this.skill);
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
