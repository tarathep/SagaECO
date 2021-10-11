namespace SagaMap.Skill.SkillDefinations.Sage
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="MonsterSketch" />.
    /// </summary>
    public class MonsterSketch : ISkill
    {
        /// <summary>
        /// Defines the SKETCHBOOK.
        /// </summary>
        private uint SKETCHBOOK = 10020757;

        /// <summary>
        /// Defines the SKETCHBOOK_Finish.
        /// </summary>
        private uint SKETCHBOOK_Finish = 10020758;

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            if (dActor.type != ActorType.MOB)
                return -12;
            return Singleton<SkillHandler>.Instance.CountItem(sActor, this.SKETCHBOOK) > 0 ? 0 : -2;
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
            Singleton<SkillHandler>.Instance.TakeItem((ActorPC)sActor, this.SKETCHBOOK, (ushort)1);
            Singleton<SkillHandler>.Instance.GiveItem((ActorPC)sActor, this.SKETCHBOOK_Finish, (ushort)1, true)[0].PictID = ((ActorMob)dActor).MobID;
            Singleton<SkillHandler>.Instance.AttractMob(sActor, dActor, 1);
        }
    }
}
