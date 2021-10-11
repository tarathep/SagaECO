namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FirstAid" />.
    /// </summary>
    public class FirstAid : ISkill
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
            uint num = (uint)((double)dActor.MaxHP * (0.0599999986588955 + 0.0399999991059303 * (double)level));
            if (dActor.HP + num <= dActor.MaxHP)
                dActor.HP += num;
            else
                dActor.HP = dActor.MaxHP;
            args.affectedActors.Add(dActor);
            args.Init();
            if (args.flag.Count > 0)
            {
                List<AttackFlag> flag;
                (flag = args.flag)[0] = flag[0] | AttackFlag.HP_HEAL | AttackFlag.NO_DAMAGE;
            }
            Singleton<MapManager>.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, dActor, true);
        }
    }
}
