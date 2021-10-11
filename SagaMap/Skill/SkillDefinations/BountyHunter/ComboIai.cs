namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="ComboIai" />.
    /// </summary>
    public class ComboIai : ISkill
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
            Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            uint key1 = 2115;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Skills.ContainsKey(key1))
            {
                args.autoCast.Add(new AutoCastInfo()
                {
                    skillID = key1,
                    level = actorPc.Skills[key1].Level,
                    delay = 1500
                });
                Singleton<SkillHandler>.Instance.SetNextComboSkill(sActor, 2115U);
            }
            uint key2 = 2201;
            if (actorPc.Skills.ContainsKey(key2))
                args.autoCast.Add(new AutoCastInfo()
                {
                    skillID = key2,
                    level = actorPc.Skills[key2].Level,
                    delay = 1500
                });
            uint key3 = 2202;
            if (!actorPc.Skills.ContainsKey(key3))
                return;
            args.autoCast.Add(new AutoCastInfo()
            {
                skillID = key3,
                level = actorPc.Skills[key3].Level,
                delay = 1500
            });
        }
    }
}
