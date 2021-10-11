namespace SagaMap.Skill.SkillDefinations.Breeder
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="TheTrust" />.
    /// </summary>
    public class TheTrust : ISkill
    {
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
            ActorPet pet = Singleton<SkillHandler>.Instance.GetPet(sActor);
            if (pet == null)
                return;
            MotionType[] motionTypeArray = new MotionType[6]
            {
        MotionType.BREAK,
        MotionType.JOY,
        MotionType.RELAX,
        MotionType.STAND,
        MotionType.NONE,
        MotionType.DOGEZA
            };
            Singleton<SkillHandler>.Instance.NPCMotion((SagaDB.Actor.Actor)pet, motionTypeArray[SagaLib.Global.Random.Next(0, motionTypeArray.Length - 1)]);
        }
    }
}
