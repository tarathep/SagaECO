namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="Berserk" />.
    /// </summary>
    public class Berserk : DefaultBuff
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Berserk"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        public Berserk(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime)
      : base(skill, actor, nameof(Berserk), lifetime)
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
            int maxAtkOri1 = (int)actor.Status.max_atk_ori;
            if (skill.Variable.ContainsKey("Berserk_max_atk1"))
                skill.Variable.Remove("Berserk_max_atk1");
            skill.Variable.Add("Berserk_max_atk1", maxAtkOri1);
            actor.Status.max_atk1_skill += (short)maxAtkOri1;
            int maxAtkOri2 = (int)actor.Status.max_atk_ori;
            if (skill.Variable.ContainsKey("Berserk_max_atk2"))
                skill.Variable.Remove("Berserk_max_atk2");
            skill.Variable.Add("Berserk_max_atk2", maxAtkOri2);
            actor.Status.max_atk2_skill += (short)maxAtkOri2;
            int maxAtkOri3 = (int)actor.Status.max_atk_ori;
            if (skill.Variable.ContainsKey("Berserk_max_atk3"))
                skill.Variable.Remove("Berserk_max_atk3");
            skill.Variable.Add("Berserk_max_atk3", maxAtkOri3);
            actor.Status.max_atk3_skill += (short)maxAtkOri3;
            int minAtkOri1 = (int)actor.Status.min_atk_ori;
            if (skill.Variable.ContainsKey("Berserk_min_atk1"))
                skill.Variable.Remove("Berserk_min_atk1");
            skill.Variable.Add("Berserk_min_atk1", minAtkOri1);
            actor.Status.min_atk1_skill += (short)minAtkOri1;
            int minAtkOri2 = (int)actor.Status.min_atk_ori;
            if (skill.Variable.ContainsKey("Berserk_min_atk2"))
                skill.Variable.Remove("Berserk_min_atk2");
            skill.Variable.Add("Berserk_min_atk2", minAtkOri2);
            actor.Status.min_atk2_skill += (short)minAtkOri2;
            int minAtkOri3 = (int)actor.Status.min_atk_ori;
            if (skill.Variable.ContainsKey("Berserk_min_atk3"))
                skill.Variable.Remove("Berserk_min_atk3");
            skill.Variable.Add("Berserk_min_atk3", minAtkOri3);
            actor.Status.min_atk3_skill += (short)minAtkOri3;
            int num = (int)-actor.Status.def_add;
            if (skill.Variable.ContainsKey("Berserk_def_add"))
                skill.Variable.Remove("Berserk_def_add");
            skill.Variable.Add("Berserk_def_add", num);
            actor.Status.def_add_skill += (short)num;
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            actor.Buff.狂戦士 = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["Berserk_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["Berserk_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["Berserk_max_atk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["Berserk_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["Berserk_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["Berserk_min_atk3"];
            actor.Status.def_add_skill -= (short)skill.Variable["Berserk_def_add"];
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            actor.Buff.狂戦士 = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
