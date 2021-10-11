namespace SagaMap.Skill.SkillDefinations.Necromancer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="HealLemures" />.
    /// </summary>
    public class HealLemures : ISkill
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
            uint[] numArray = new uint[6]
            {
        0U,
        550U,
        880U,
        1100U,
        1320U,
        1540U
            };
            if (!sActor.Status.Additions.ContainsKey("SummobLemures"))
                return;
            SummobLemures.SummobLemuresBuff addition = (SummobLemures.SummobLemuresBuff)sActor.Status.Additions["SummobLemures"];
            if (addition.mob != null)
            {
                args.affectedActors.Add((SagaDB.Actor.Actor)addition.mob);
                addition.mob.HP += numArray[(int)level];
                if (addition.mob.HP > addition.mob.MaxHP)
                    addition.mob.HP = addition.mob.MaxHP;
                args.Init();
                List<AttackFlag> flag;
                (flag = args.flag)[0] = flag[0] | AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
                Singleton<MapManager>.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)addition.mob, true);
            }
        }
    }
}
