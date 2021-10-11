namespace SagaMap.Skill.SkillDefinations.Druid
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="RegiAllUp" />.
    /// </summary>
    public class RegiAllUp : ISkill
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
            int lifetime = 40000 + 20000 * (int)level;
            DefaultBuff defaultBuff1 = new DefaultBuff(args.skill, dActor, nameof(RegiAllUp), lifetime);
            defaultBuff1.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff1.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(dActor, (Addition)defaultBuff1);
            string[] strArray = new string[9]
            {
        "Sleep",
        "Poison",
        "Stun",
        "Silence",
        "Stone",
        "Confuse",
        "鈍足",
        "Frosen",
        "硬直"
            };
            foreach (string str in strArray)
            {
                DefaultBuff defaultBuff2 = new DefaultBuff(args.skill, dActor, str + "Regi", lifetime);
                defaultBuff2.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartBuffEventHandler);
                defaultBuff2.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndBuffEventHandler);
                SkillHandler.ApplyAddition(dActor, (Addition)defaultBuff2);
            }
        }

        /// <summary>
        /// The StartBuffEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartBuffEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
        }

        /// <summary>
        /// The EndBuffEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndBuffEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Buff.ConfuseResist = true;
            actor.Buff.FaintResist = true;
            actor.Buff.FrosenResist = true;
            actor.Buff.ParalysisResist = true;
            actor.Buff.PoisonResist = true;
            actor.Buff.SilenceResist = true;
            actor.Buff.SleepResist = true;
            actor.Buff.SpeedDownResist = true;
            actor.Buff.StoneResist = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Buff.ConfuseResist = false;
            actor.Buff.FaintResist = false;
            actor.Buff.FrosenResist = false;
            actor.Buff.ParalysisResist = false;
            actor.Buff.PoisonResist = false;
            actor.Buff.SilenceResist = false;
            actor.Buff.SleepResist = false;
            actor.Buff.SpeedDownResist = false;
            actor.Buff.StoneResist = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
