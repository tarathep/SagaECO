namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using System;

    /// <summary>
    /// Defines the <see cref="SkillEvent" />.
    /// </summary>
    public class SkillEvent : Event
    {
        /// <summary>
        /// Defines the instance.
        /// </summary>
        private static SkillEvent instance;

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static SkillEvent Instance
        {
            get
            {
                if (SkillEvent.instance == null)
                    SkillEvent.instance = new SkillEvent();
                return SkillEvent.instance;
            }
        }

        /// <summary>
        /// The OnEvent.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public override void OnEvent(ActorPC pc)
        {
            throw new NotImplementedException();
        }
    }
}
