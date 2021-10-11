namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;

    /// <summary>
    /// Defines the <see cref="鈍足" />.
    /// </summary>
    public class 鈍足 : DefaultBuff
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="鈍足"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        public 鈍足(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime)
      : base(skill, actor, nameof(鈍足), lifetime)
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
            actor.Buff.SpeedDown = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
            if (skill.Variable.ContainsKey("SpeedDown"))
                skill.Variable.Remove("SpeedDown");
            int num = (int)actor.Speed / 2;
            skill.Variable.Add("SpeedDown", num);
            actor.Speed -= (ushort)num;
            if (actor.type != ActorType.MOB)
                return;
            MobEventHandler e = (MobEventHandler)actor.e;
            Activity aiActivity = e.AI.AIActivity;
            e.AI.AIActivity = aiActivity;
        }

        /// <summary>
        /// The EndEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            actor.Buff.SpeedDown = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
            int num = skill.Variable["SpeedDown"];
            actor.Speed += (ushort)num;
            if (actor.type != ActorType.MOB)
                return;
            MobEventHandler e = (MobEventHandler)actor.e;
            Activity aiActivity = e.AI.AIActivity;
            e.AI.AIActivity = aiActivity;
        }
    }
}
