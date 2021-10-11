namespace SagaMap.Skill.SkillDefinations.Breeder
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="Akurobattoibeijon" />.
    /// </summary>
    public class Akurobattoibeijon : ISkill
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
            int damage = 2500;
            ActorPet pet = Singleton<SkillHandler>.Instance.GetPet(sActor);
            if (pet == null)
                return;
            foreach (SagaDB.Actor.Actor dActor1 in Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea((SagaDB.Actor.Actor)pet, (short)250, false))
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)pet, dActor1))
                    Singleton<SkillHandler>.Instance.AttractMob((SagaDB.Actor.Actor)pet, dActor1, damage);
            }
        }
    }
}
