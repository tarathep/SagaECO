namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="MobElementRandcircle" />.
    /// </summary>
    public class MobElementRandcircle : ISkill
    {
        /// <summary>
        /// Defines the NextSkillID.
        /// </summary>
        private uint NextSkillID = 0;

        /// <summary>
        /// Defines the Count.
        /// </summary>
        private int Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobElementRandcircle"/> class.
        /// </summary>
        /// <param name="NextID">The NextID<see cref="uint"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public MobElementRandcircle(uint NextID, int count)
        {
            this.NextSkillID = NextID;
            this.Count = count;
        }

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
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
            for (int index = 0; index < this.Count; ++index)
                args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(this.NextSkillID, level, 0));
        }
    }
}
