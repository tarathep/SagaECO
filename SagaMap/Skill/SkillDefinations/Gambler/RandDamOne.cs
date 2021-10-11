namespace SagaMap.Skill.SkillDefinations.Gambler
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="RandDamOne" />.
    /// </summary>
    public class RandDamOne : ISkill
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
            float ATKBonus = (float)(2.0 + 0.5 * (double)level);
            int num = 85 - 5 * (int)level;
            if (SagaLib.Global.Random.Next(0, 99) < num)
            {
                Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            }
            else
            {
                sActor.HP = 1U;
                int lifetime = 1000 + 1000 * (int)level;
                Confuse confuse = new Confuse(args.skill, dActor, lifetime);
                SkillHandler.ApplyAddition(dActor, (Addition)confuse);
                Singleton<MapManager>.Instance.GetMap(sActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, sActor, true);
            }
        }
    }
}
