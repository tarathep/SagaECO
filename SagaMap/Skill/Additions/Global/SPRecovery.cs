namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="SPRecovery" />.
    /// </summary>
    public class SPRecovery : DefaultBuff
    {
        /// <summary>
        /// Defines the isMarionette.
        /// </summary>
        private bool isMarionette = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SPRecovery"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        public SPRecovery(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, int period)
      : base(skill, actor, nameof(SPRecovery), lifetime, period)
        {
            this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
            this.OnUpdate += new DefaultBuff.UpdateEventHandler(this.TimerUpdate);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SPRecovery"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        /// <param name="isMarionette">The isMarionette<see cref="bool"/>.</param>
        public SPRecovery(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, int period, bool isMarionette)
      : base(skill, actor, isMarionette ? "Marionette_SPRecovery" : nameof(SPRecovery), lifetime, period)
        {
            this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
            this.OnUpdate += new DefaultBuff.UpdateEventHandler(this.TimerUpdate);
            this.isMarionette = isMarionette;
        }

        /// <summary>
        /// The StartEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
        }

        /// <summary>
        /// The EndEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
        }

        /// <summary>
        /// The TimerUpdate.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void TimerUpdate(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            uint num;
            if (this.isMarionette)
            {
                ActorPC actorPc = (ActorPC)actor;
                if (actorPc.Marionette == null)
                    this.AdditionEnd();
                num = (uint)((ulong)actorPc.MaxSP * (ulong)(100 + ((int)actorPc.Int + (int)actorPc.Vit + (int)actorPc.Status.int_item + (int)actorPc.Status.int_mario + (int)actorPc.Status.int_rev + (int)actorPc.Status.vit_rev + (int)actorPc.Status.vit_mario + (int)actorPc.Status.vit_item) / 6) / 2000UL);
            }
            else
            {
                ActorPC actorPc = (ActorPC)actor;
                num = (uint)((ulong)((long)actorPc.MaxSP * (long)(100 + ((int)actorPc.Int + (int)actorPc.Vit + (int)actorPc.Status.int_item + (int)actorPc.Status.int_mario + (int)actorPc.Status.int_rev + (int)actorPc.Status.vit_rev + (int)actorPc.Status.vit_mario + (int)actorPc.Status.vit_item) / 6) / 2000L) + (ulong)actorPc.Status.sp_recover_skill);
            }
            actor.SP += num;
            if (actor.SP > actor.MaxSP)
                actor.SP = actor.MaxSP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor, true);
        }
    }
}
