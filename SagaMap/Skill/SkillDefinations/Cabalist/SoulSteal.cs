namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="SoulSteal" />.
    /// </summary>
    public class SoulSteal : ISkill
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
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)sActor, dActor) ? 0 : -14;
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
            float MATKBonus = 1f;
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Neutral, MATKBonus);
            int num1 = 0;
            foreach (int num2 in args.hp)
                num1 += num2;
            sActor.MP += (uint)num1;
            if (sActor.MP > sActor.MaxMP)
                sActor.MP = sActor.MaxMP;
            Singleton<MapManager>.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, sActor, true);
        }
    }
}
