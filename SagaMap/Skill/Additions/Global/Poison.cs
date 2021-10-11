namespace SagaMap.Skill.Additions.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System;

    /// <summary>
    /// Defines the <see cref="Poison" />.
    /// </summary>
    public class Poison : DefaultBuff
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Poison"/> class.
        /// </summary>
        /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
        public Poison(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, int lifetime)
      : base(skill, actor, nameof(Poison), lifetime, 2000)
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
            actor.Buff.Poison = true;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
            int num1 = (int)actor.Status.max_atk_ori / 2;
            if (skill.Variable.ContainsKey("Poison_max_atk1"))
                skill.Variable.Remove("Poison_max_atk1");
            skill.Variable.Add("Poison_max_atk1", num1);
            actor.Status.max_atk1_skill -= (short)num1;
            int num2 = (int)actor.Status.max_atk_ori / 2;
            if (skill.Variable.ContainsKey("Poison_max_atk2"))
                skill.Variable.Remove("Poison_max_atk2");
            skill.Variable.Add("Poison_max_atk2", num2);
            actor.Status.max_atk2_skill -= (short)num2;
            int num3 = (int)actor.Status.max_atk_ori / 2;
            if (skill.Variable.ContainsKey("Poison_max_atk3"))
                skill.Variable.Remove("Poison_max_atk3");
            skill.Variable.Add("Poison_max_atk3", num3);
            actor.Status.max_atk3_skill -= (short)num3;
            int num4 = (int)actor.Status.min_atk_ori / 2;
            if (skill.Variable.ContainsKey("Poison_min_atk1"))
                skill.Variable.Remove("Poison_min_atk1");
            skill.Variable.Add("Poison_min_atk1", num4);
            actor.Status.min_atk1_skill -= (short)num4;
            int num5 = (int)actor.Status.min_atk_ori / 2;
            if (skill.Variable.ContainsKey("Poison_min_atk2"))
                skill.Variable.Remove("Poison_min_atk2");
            skill.Variable.Add("Poison_min_atk2", num5);
            actor.Status.min_atk2_skill -= (short)num5;
            int num6 = (int)actor.Status.min_atk_ori / 2;
            if (skill.Variable.ContainsKey("Poison_min_atk3"))
                skill.Variable.Remove("Poison_min_atk3");
            skill.Variable.Add("Poison_min_atk3", num6);
            actor.Status.min_atk3_skill -= (short)num6;
            int num7 = (int)actor.Status.min_matk_ori / 2;
            if (skill.Variable.ContainsKey("Poison_min_matk"))
                skill.Variable.Remove("Poison_min_matk");
            skill.Variable.Add("Poison_min_matk", num7);
            actor.Status.min_matk_skill -= (short)num7;
            int num8 = (int)actor.Status.max_matk_ori / 2;
            if (skill.Variable.ContainsKey("Poison_max_matk"))
                skill.Variable.Remove("Poison_max_matk");
            skill.Variable.Add("Poison_max_matk", num8);
            actor.Status.max_matk_skill -= (short)num8;
        }

        /// <summary>
        /// The EndEvent.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
            actor.Buff.Poison = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
            actor.Status.max_atk1_skill += (short)skill.Variable["Poison_max_atk1"];
            actor.Status.max_atk2_skill += (short)skill.Variable["Poison_max_atk2"];
            actor.Status.max_atk3_skill += (short)skill.Variable["Poison_max_atk3"];
            actor.Status.min_atk1_skill += (short)skill.Variable["Poison_min_atk1"];
            actor.Status.min_atk2_skill += (short)skill.Variable["Poison_min_atk2"];
            actor.Status.min_atk3_skill += (short)skill.Variable["Poison_min_atk3"];
            actor.Status.min_matk_skill += (short)skill.Variable["Poison_min_matk"];
            actor.Status.max_matk_skill += (short)skill.Variable["Poison_max_matk"];
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
                    actor.HP = (long)actor.HP <= (long)num ? 1U : (uint)((ulong)actor.HP - (ulong)num);
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
