namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="PosionNeedle" />.
    /// </summary>
    public class PosionNeedle : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            uint itemID = 10038102;
            if (Singleton<SkillHandler>.Instance.CountItem(sActor, itemID) < 1)
                return -57;
            Singleton<SkillHandler>.Instance.TakeItem(sActor, itemID, (ushort)1);
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
            int rate = 25 + 5 * (int)level;
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, nameof(PosionNeedle), rate))
            {
                DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(PosionNeedle), 3000);
                defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                SkillHandler.ApplyAddition(dActor, (Addition)defaultBuff);
            }
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, 1f);
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
            if (skill.Variable.ContainsKey("PosionNeedle_ATK1"))
                skill.Variable.Remove("PosionNeedle_ATK1");
            int num1 = (int)actor.Status.min_atk_ori / 2;
            skill.Variable.Add("PosionNeedle_ATK1", num1);
            actor.Status.min_atk1_skill -= (short)num1;
            actor.Status.min_atk2_skill -= (short)num1;
            actor.Status.min_atk3_skill -= (short)num1;
            if (skill.Variable.ContainsKey("PosionNeedle_ATK2"))
                skill.Variable.Remove("PosionNeedle_ATK2");
            int num2 = (int)actor.Status.max_atk_ori / 2;
            skill.Variable.Add("PosionNeedle_ATK2", num2);
            actor.Status.max_atk1_skill -= (short)num2;
            actor.Status.max_atk2_skill -= (short)num2;
            actor.Status.max_atk3_skill -= (short)num2;
            if (skill.Variable.ContainsKey("PosionNeedle_MATK"))
                skill.Variable.Remove("PosionNeedle_MATK");
            int num3 = (int)actor.Status.min_matk_ori / 2;
            skill.Variable.Add("PosionNeedle_MATK", num3);
            actor.Status.min_matk_skill -= (short)num3;
            if (skill.Variable.ContainsKey("PosionNeedle_MATK2"))
                skill.Variable.Remove("PosionNeedle_MATK2");
            int num4 = (int)actor.Status.max_matk_ori / 2;
            skill.Variable.Add("PosionNeedle_MATK2", num4);
            actor.Status.max_matk_skill -= (short)num4;
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
            int num1 = skill.Variable["PosionNeedle_ATK1"];
            actor.Status.min_atk1_skill += (short)num1;
            actor.Status.min_atk2_skill += (short)num1;
            actor.Status.min_atk3_skill += (short)num1;
            int num2 = skill.Variable["PosionNeedle_ATK2"];
            actor.Status.max_atk1_skill += (short)num2;
            actor.Status.max_atk2_skill += (short)num2;
            actor.Status.max_atk3_skill += (short)num2;
            int num3 = skill.Variable["PosionNeedle_MATK"];
            actor.Status.min_matk_skill += (short)num3;
            int num4 = skill.Variable["PosionNeedle_MATK2"];
            actor.Status.max_matk_skill += (short)num4;
        }
    }
}
