namespace SagaMap.Skill.SkillDefinations.Trader
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="HumAdditional" />.
    /// </summary>
    public class HumAdditional : ISkill
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
            if (pet == null || !Singleton<SkillHandler>.Instance.CheckMobType((ActorMob)pet, "HUMAN"))
                return;
            uint[] numArray = new uint[6]
            {
        0U,
        50U,
        80U,
        50U,
        120U,
        100U
            };
            ActorPC actorPc = (ActorPC)sActor;
            if ((long)actorPc.Gold >= (long)numArray[(int)level])
            {
                actorPc.Gold -= (int)numArray[(int)level];
                Singleton<SkillHandler>.Instance.GetMobAI(pet).CastSkill(6403U, level, dActor);
            }
        }
    }
}
