namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System;

    /// <summary>
    /// Defines the <see cref="HPRecovery" />.
    /// </summary>
    public class HPRecovery : DefaultBuff
    {
        /// <summary>
        /// Defines the isMarionette.
        /// </summary>
        private bool isMarionette = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="HPRecovery"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        public HPRecovery(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, int period)
      : base(skill, actor, nameof(HPRecovery), lifetime, period)
        {
            this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
            this.OnUpdate += new DefaultBuff.UpdateEventHandler(this.TimerUpdate);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HPRecovery"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        /// <param name="period">The period<see cref="int"/>.</param>
        /// <param name="isMarionette">The isMarionette<see cref="bool"/>.</param>
        public HPRecovery(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime, int period, bool isMarionette)
      : base(skill, actor, isMarionette ? "Marionette_HPRecovery" : nameof(HPRecovery), lifetime, period)
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
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
            if (actor.type != ActorType.PC)
                return;
            ActorPC actorPc = (ActorPC)actor;
            if (actorPc.Marionette != null && this.isMarionette)
                actorPc.Status.hp_recover_skill += (short)15;
        }

        /// <summary>
        /// The EndEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
            if (actor.type != ActorType.PC)
                return;
            ActorPC actorPc = (ActorPC)actor;
            if (actorPc.Marionette == null && this.isMarionette)
                actorPc.Status.hp_recover_skill -= (short)15;
        }

        /// <summary>
        /// The TimerUpdate.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void TimerUpdate(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            ClientManager.EnterCriticalArea();
            try
            {
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                uint num;
                if (this.isMarionette)
                {
                    ActorPC actorPc = (ActorPC)actor;
                    if (actorPc.Marionette == null)
                        this.AdditionEnd();
                    num = (uint)((ulong)actorPc.MaxHP * (ulong)(100 + ((int)actorPc.Vit + (int)actorPc.Status.vit_item + (int)actorPc.Status.vit_mario + (int)actorPc.Status.vit_rev) / 3) / 2000UL);
                }
                else
                    num = (uint)((double)actor.Status.hp_recover_skill / 100.0 * (double)actor.MaxHP);
                actor.HP += num;
                if (actor.HP > actor.MaxHP)
                    actor.HP = actor.MaxHP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor, true);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
