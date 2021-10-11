namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MobTrPoisonCircle" />.
    /// </summary>
    public class MobTrPoisonCircle : ISkill
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
            int rate = 20;
            int lifetime = 30000;
            if (dActor.type != ActorType.PC)
                return;
            foreach (SagaDB.Actor.Actor possesionedActor in ((ActorPC)dActor).PossesionedActors)
            {
                if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, possesionedActor, SkillHandler.DefaultAdditions.Poison, rate))
                {
                    Poison poison = new Poison(args.skill, possesionedActor, lifetime);
                    SkillHandler.ApplyAddition(possesionedActor, (Addition)poison);
                }
            }
        }
    }
}
