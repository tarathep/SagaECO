namespace SagaMap.Skill.SkillDefinations.Gardener
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="GardenerSkill" />.
    /// </summary>
    public class GardenerSkill : ISkill
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
            float Damage = 10f;
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)100, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor actor in actorsArea)
            {
                if (actor.type == ActorType.MOB && Singleton<SkillHandler>.Instance.CheckMobType((ActorMob)actor, "plant"))
                    dActor1.Add(actor);
            }
            Singleton<SkillHandler>.Instance.FixAttack(sActor, dActor1, args, Elements.Neutral, Damage);
        }
    }
}
