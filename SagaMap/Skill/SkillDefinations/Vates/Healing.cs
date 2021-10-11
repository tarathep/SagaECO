namespace SagaMap.Skill.SkillDefinations.Vates
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;

    /// <summary>
    /// Defines the <see cref="Healing" />.
    /// </summary>
    public class Healing : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC pc, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            if (pc.Status.Additions.ContainsKey("Spell"))
                return -7;
            return dActor.type == ActorType.MOB && ((MobEventHandler)dActor.e).AI.Mode.Symbol ? -14 : 0;
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
            float MATKBonus = 0.0f;
            switch (level)
            {
                case 1:
                    MATKBonus = -1f;
                    break;
                case 2:
                    MATKBonus = -1.7f;
                    break;
                case 3:
                    MATKBonus = -2.3f;
                    break;
                case 4:
                    MATKBonus = -2.7f;
                    break;
                case 5:
                    MATKBonus = -3f;
                    break;
            }
            if (dActor.type == ActorType.MOB && dActor.Status.undead)
                MATKBonus = -MATKBonus;
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, Elements.Holy, MATKBonus);
        }
    }
}
