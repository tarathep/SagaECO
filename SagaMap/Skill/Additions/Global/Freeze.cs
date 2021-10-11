namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Freeze" />.
    /// </summary>
    public class Freeze : DefaultBuff
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Freeze"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        public Freeze(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime)
      : base(skill, actor, "Frosen", lifetime)
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
            actor.Buff.Frosen = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
            Dictionary<Elements, int> elements;
            (elements = actor.Elements)[Elements.Water] = elements[Elements.Water] + 100;
        }

        /// <summary>
        /// The EndEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            actor.Buff.Frosen = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
            Dictionary<Elements, int> elements;
            (elements = actor.Elements)[Elements.Water] = elements[Elements.Water] - 100;
        }
    }
}
