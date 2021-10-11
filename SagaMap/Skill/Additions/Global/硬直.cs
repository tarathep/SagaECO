namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="硬直" />.
    /// </summary>
    public class 硬直 : DefaultBuff
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="硬直"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        public 硬直(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime)
      : base(skill, actor, nameof(硬直), lifetime)
        {
            this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
            this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
        }

        /// <summary>
        /// The StartEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            actor.Buff.硬直 = true;
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
            actor.Buff.硬直 = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
