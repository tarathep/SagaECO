namespace SagaMap.Skill.SkillDefinations.Striker
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="PetDogHateUpCircle" />.
    /// </summary>
    public class PetDogHateUpCircle : ISkill
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
            foreach (SagaDB.Actor.Actor dActor1 in Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)100, false))
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor1))
                {
                    int damage = (int)((double)dActor1.HP * 0.150000005960464 * (double)level);
                    Singleton<SkillHandler>.Instance.AttractMob(sActor, dActor1, damage);
                }
            }
        }
    }
}
