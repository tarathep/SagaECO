namespace SagaMap.Skill.SkillDefinations.Druid
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="HolyFeather" />.
    /// </summary>
    public class HolyFeather : ISkill
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
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)300, true, false);
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            ActorPC actorPc1 = (ActorPC)sActor;
            if (actorPc1.Party != null)
            {
                foreach (SagaDB.Actor.Actor actor in actorsArea)
                {
                    if (actor.type == ActorType.PC)
                    {
                        ActorPC actorPc2 = (ActorPC)actor;
                        if (actorPc2.Party != null && actorPc1.Party != null && ((int)actorPc2.Party.ID == (int)actorPc1.Party.ID && actorPc2.Party.ID != 0U && !actorPc2.Buff.Dead && actorPc2.PossessionTarget == 0U && (int)actorPc2.Party.ID == (int)actorPc1.Party.ID))
                            actorList.Add(actor);
                    }
                }
            }
            else
                actorList.Add(sActor);
            args.affectedActors = actorList;
            args.Init();
            int lifetime = new int[6]
            {
        0,
        60000,
        75000,
        90000,
        105000,
        120000
            }[(int)level];
            foreach (SagaDB.Actor.Actor actor in actorList)
            {
                MPRecovery mpRecovery = new MPRecovery(args.skill, actor, lifetime, 5000);
                SkillHandler.ApplyAddition(actor, (Addition)mpRecovery);
                SPRecovery spRecovery = new SPRecovery(args.skill, actor, lifetime, 5000);
                SkillHandler.ApplyAddition(actor, (Addition)spRecovery);
                DefaultBuff defaultBuff = new DefaultBuff(args.skill, actor, nameof(HolyFeather), lifetime);
                defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
                defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
                SkillHandler.ApplyAddition(actor, (Addition)defaultBuff);
            }
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.mp_recover_skill += (short)15;
            actor.Status.hp_recover_skill += (short)15;
            actor.Status.sp_recover_skill += (short)15;
            actor.Buff.HolyFeather = true;
            actor.Buff.HP回復率上昇 = true;
            actor.Buff.SP回復率上昇 = true;
            actor.Buff.MP回復率上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.mp_recover_skill -= (short)15;
            actor.Status.hp_recover_skill -= (short)15;
            actor.Status.sp_recover_skill -= (short)15;
            actor.Buff.HolyFeather = false;
            actor.Buff.HP回復率上昇 = false;
            actor.Buff.SP回復率上昇 = false;
            actor.Buff.MP回復率上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
