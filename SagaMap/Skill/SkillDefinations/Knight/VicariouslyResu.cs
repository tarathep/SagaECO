namespace SagaMap.Skill.SkillDefinations.Knight
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="VicariouslyResu" />.
    /// </summary>
    public class VicariouslyResu : ISkill
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
            this.ProcessRevive(dActor, level);
            sActor.HP = 0U;
            sActor.e.OnDie();
            args.affectedActors.Add(sActor);
            args.Init();
            args.flag[0] = AttackFlag.DIE;
        }

        /// <summary>
        /// The ProcessRevive.
        /// </summary>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        private void ProcessRevive(SagaDB.Actor.Actor dActor, byte level)
        {
            if (dActor.type != ActorType.PC)
                return;
            MapClient mapClient = MapClient.FromActorPC((ActorPC)dActor);
            if (mapClient.Character.Buff.Dead)
            {
                mapClient.Character.BattleStatus = (byte)0;
                mapClient.SendChangeStatus();
                mapClient.Character.TInt["Revive"] = 5;
                mapClient.EventActivate(4043309056U);
                mapClient.Character.HP = (uint)((double)mapClient.Character.MaxHP * 0.100000001490116 * (double)level);
                Singleton<MapManager>.Instance.GetMap(dActor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, dActor, true);
            }
        }
    }
}
