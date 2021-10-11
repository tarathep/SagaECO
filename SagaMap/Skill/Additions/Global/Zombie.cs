namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System;

    /// <summary>
    /// Defines the <see cref="Zombie" />.
    /// </summary>
    public class Zombie : DefaultBuff
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Zombie"/> class.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        public Zombie(SagaDB.Actor.Actor actor)
      : base((SagaDB.Skill.Skill)null, actor, nameof(Zombie), int.MaxValue, 10000)
        {
            this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
            this.OnUpdate += new DefaultBuff.UpdateEventHandler(this.TimerUpdate);
        }

        /// <summary>
        /// The StartEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            actor.Buff.ゾンビ = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            actor.Buff.ゾンビ = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
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
                if (actor.HP > 0U && !actor.Buff.Dead)
                {
                    Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                    int num = (int)(actor.MaxHP / 50U);
                    if (num < 1)
                        num = 1;
                    if ((long)actor.HP > (long)num)
                    {
                        actor.HP = (uint)((ulong)actor.HP - (ulong)num);
                    }
                    else
                    {
                        actor.HP = 0U;
                        actor.Buff.Dead = true;
                        map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
                        actor.e.OnDie();
                        SkillHandler.RemoveAddition(actor, nameof(Zombie));
                    }
                    map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor, true);
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
