namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="MPRecovery" />.
    /// </summary>
    public class MPRecovery : DefaultBuff
    {
        /// <summary>
        /// Defines the isMarionette.
        /// </summary>
        private bool isMarionette = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MPRecovery"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        public MPRecovery(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, int period)
      : base(skill, actor, nameof(MPRecovery), lifetime, period)
        {
            this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
            this.OnUpdate += new DefaultBuff.UpdateEventHandler(this.TimerUpdate);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MPRecovery"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        /// <param name="isMarionette">The isMarionette<see cref="bool"/>.</param>
        public MPRecovery(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, int period, bool isMarionette)
      : base(skill, actor, isMarionette ? "Marionette_MPRecovery" : nameof(MPRecovery), lifetime, period)
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
            uint num1 = 0;
            uint num2;
            if (this.isMarionette)
            {
                ActorPC actorPc = (ActorPC)actor;
                if (actorPc.Marionette == null)
                    this.AdditionEnd();
                num2 = (uint)((ulong)actorPc.MaxMP * (ulong)(100 + ((int)actorPc.Mag + (int)actorPc.Status.mag_item + (int)actorPc.Status.mag_mario + (int)actorPc.Status.mag_rev) / 3) / 2000UL);
            }
            else
            {
                ActorPC actorPc = (ActorPC)actor;
                num2 = num1 = (uint)((ulong)((long)actorPc.MaxMP * (long)(100 + ((int)actorPc.Mag + (int)actorPc.Status.mag_item + (int)actorPc.Status.mag_mario + (int)actorPc.Status.mag_rev) / 3) / 2000L) + (ulong)actorPc.Status.mp_recover_skill);
            }
            actor.MP += num2;
            if (actor.MP > actor.MaxMP)
                actor.MP = actor.MaxMP;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor, true);
        }
    }
}
