namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="EventSelfDarkStorm" />.
    /// </summary>
    public class EventSelfDarkStorm : ISkill
    {
        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSelfDarkStorm"/> class.
        /// </summary>
        public EventSelfDarkStorm()
        {
            this.MobUse = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EventSelfDarkStorm"/> class.
        /// </summary>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public EventSelfDarkStorm(bool MobUse)
        {
            this.MobUse = MobUse;
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
            if (this.MobUse)
                level = (byte)5;
            float MATKBonus = (float)(2.5 + 0.5 * (double)level);
            short range = (short)(100 * (int)level + 50);
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, range, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                    dActor1.Add(dActor2);
            }
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, Elements.Dark, MATKBonus);
        }
    }
}
