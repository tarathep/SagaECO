namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="AtkUpHitDown" />.
    /// </summary>
    public class AtkUpHitDown : ISkill
    {
        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC pc, SagaDB.Actor.Actor dActor, SkillArg args)
        {
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
            int lifetime = (45 - 5 * (int)level) * 1000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(AtkUpHitDown), lifetime);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(dActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int level = (int)skill.skill.Level;
            int num1 = -(int)((double)actor.Status.hit_ranged * (0.0300000011920929 * (double)level));
            int num2 = -(int)((double)actor.Status.hit_melee * (0.0300000011920929 * (double)level));
            int num3 = (int)((double)actor.Status.max_atk_ori * (0.00750000029802322 * (double)level));
            int num4 = (int)((double)actor.Status.min_atk_ori * (0.00750000029802322 * (double)level));
            int num5 = (int)((double)actor.Status.max_atk_ori * (0.00750000029802322 * (double)level));
            int num6 = (int)((double)actor.Status.min_atk_ori * (0.00750000029802322 * (double)level));
            int num7 = (int)((double)actor.Status.max_atk_ori * (0.00750000029802322 * (double)level));
            int num8 = (int)((double)actor.Status.min_atk_ori * (0.00750000029802322 * (double)level));
            if (skill.Variable.ContainsKey("AtkUpHitDown_hit_range_down"))
                skill.Variable.Remove("AtkUpHitDown_hit_range_down");
            skill.Variable.Add("AtkUpHitDown_hit_range_down", num1);
            actor.Status.hit_ranged_skill += (short)num1;
            if (skill.Variable.ContainsKey("AtkUpHitDown_hit_melee_down"))
                skill.Variable.Remove("AtkUpHitDown_hit_melee_down");
            skill.Variable.Add("AtkUpHitDown_hit_melee_down", num2);
            actor.Status.hit_melee_skill += (short)num2;
            if (skill.Variable.ContainsKey("AtkUpHitDown_max_atk1_add"))
                skill.Variable.Remove("AtkUpHitDown_max_atk1_add");
            skill.Variable.Add("AtkUpHitDown_max_atk1_add", num3);
            actor.Status.max_atk1_skill += (short)num3;
            if (skill.Variable.ContainsKey("AtkUpHitDown_max_atk2_add"))
                skill.Variable.Remove("AtkUpHitDown_max_atk2_add");
            skill.Variable.Add("AtkUpHitDown_max_atk2_add", num5);
            actor.Status.max_atk2_skill += (short)num5;
            if (skill.Variable.ContainsKey("AtkUpHitDown_max_atk3_add"))
                skill.Variable.Remove("AtkUpHitDown_max_atk3_add");
            skill.Variable.Add("AtkUpHitDown_max_atk3_add", num7);
            actor.Status.max_atk3_skill += (short)num7;
            if (skill.Variable.ContainsKey("AtkUpHitDown_min_atk1_add"))
                skill.Variable.Remove("AtkUpHitDown_min_atk1_add");
            skill.Variable.Add("AtkUpHitDown_min_atk1_add", num4);
            actor.Status.min_atk1_skill += (short)num4;
            if (skill.Variable.ContainsKey("AtkUpHitDown_min_atk2_add"))
                skill.Variable.Remove("AtkUpHitDown_min_atk2_add");
            skill.Variable.Add("AtkUpHitDown_min_atk2_add", num6);
            actor.Status.min_atk2_skill += (short)num6;
            if (skill.Variable.ContainsKey("AtkUpHitDown_min_atk3_add"))
                skill.Variable.Remove("AtkUpHitDown_min_atk3_add");
            skill.Variable.Add("AtkUpHitDown_min_atk3_add", num8);
            actor.Status.min_atk3_skill += (short)num8;
            actor.Buff.最小攻撃力上昇 = true;
            actor.Buff.最大攻撃力上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_ranged_skill -= (short)skill.Variable["AtkUpHitDown_hit_range_down"];
            actor.Status.hit_melee_skill -= (short)skill.Variable["AtkUpHitDown_hit_melee_down"];
            actor.Status.max_atk1_skill -= (short)skill.Variable["AtkUpHitDown_max_atk1_add"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["AtkUpHitDown_max_atk2_add"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["AtkUpHitDown_max_atk3_add"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkUpHitDown_min_atk1_add"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkUpHitDown_min_atk2_add"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkUpHitDown_min_atk3_add"];
            actor.Buff.最小攻撃力上昇 = false;
            actor.Buff.最大攻撃力上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
