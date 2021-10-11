namespace SagaMap.Skill.SkillDefinations.Machinery
{
    using SagaDB.Actor;
    using SagaDB.Mob;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MachineInfo" />.
    /// </summary>
    public class MachineInfo : ISkill
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
            Knowledge knowledge = new Knowledge(args.skill, sActor, nameof(MachineInfo), new MobType[12]
            {
        MobType.MACHINE,
        MobType.MACHINE_BOSS,
        MobType.MACHINE_BOSS_CHAMP,
        MobType.MACHINE_BOSS_SKILL,
        MobType.MACHINE_MATERIAL,
        MobType.MACHINE_NOTOUCH,
        MobType.MACHINE_NOTPTDROPRANGE,
        MobType.MACHINE_RIDE,
        MobType.MACHINE_RIDE_ROBOT,
        MobType.MACHINE_SKILL,
        MobType.MACHINE_SKILL_BOSS,
        MobType.MACHINE_SMARK_BOSS_SKILL_HETERODOXY_NONBLAST
            });
            SkillHandler.ApplyAddition(sActor, (Addition)knowledge);
        }
    }
}
