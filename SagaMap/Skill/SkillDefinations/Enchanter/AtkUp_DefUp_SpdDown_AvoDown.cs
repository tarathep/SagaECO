namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="AtkUp_DefUp_SpdDown_AvoDown" />.
    /// </summary>
    public class AtkUp_DefUp_SpdDown_AvoDown : ISkill
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
            return dActor.type == ActorType.PC ? 0 : -14;
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
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(AtkUp_DefUp_SpdDown_AvoDown), 30000);
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
            int num1 = (int)((double)actor.Status.min_atk1 * (0.119999997317791 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_min_atk1"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_min_atk1");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_min_atk1", num1);
            actor.Status.min_atk1_skill += (short)num1;
            int num2 = (int)((double)actor.Status.min_atk2 * (0.119999997317791 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_min_atk2"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_min_atk2");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_min_atk2", num2);
            actor.Status.min_atk2_skill += (short)num2;
            int num3 = (int)((double)actor.Status.min_atk3 * (0.119999997317791 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_min_atk3"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_min_atk3");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_min_atk3", num3);
            actor.Status.min_atk3_skill += (short)num3;
            int num4 = (int)((double)actor.Status.min_matk * (0.119999997317791 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_min_matk"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_min_matk");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_min_matk", num4);
            actor.Status.min_matk_skill += (short)num4;
            int num5 = (int)((double)actor.Status.def * (0.209999993443489 + 0.00999999977648258 * (double)level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_def"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_def");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_def", num5);
            actor.Status.def_skill += (short)num5;
            int num6 = (int)((double)actor.Status.mdef * (0.209999993443489 + 0.00999999977648258 * (double)level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_mdef"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_mdef");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_mdef", num6);
            actor.Status.mdef_skill += (short)num6;
            int num7 = -(int)((double)actor.Status.aspd * (0.0700000002980232 + 0.0199999995529652 * (double)level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_aspd"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_aspd");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_aspd", num7);
            actor.Status.aspd_skill += (short)num7;
            int num8 = -(int)((double)actor.Status.cspd * (0.0700000002980232 + 0.0199999995529652 * (double)level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_cspd"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_cspd");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_cspd", num8);
            actor.Status.cspd_skill += (short)num8;
            int num9 = -(int)((double)actor.Status.avoid_melee * (0.109999999403954 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("AtkUp_DefUp_SpdDown_AvoDown_avoid_melee"))
                skill.Variable.Remove("AtkUp_DefUp_SpdDown_AvoDown_avoid_melee");
            skill.Variable.Add("AtkUp_DefUp_SpdDown_AvoDown_avoid_melee", num9);
            actor.Status.avoid_melee_skill += (short)num9;
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill", (int)(-0.109999999403954 - 0.0299999993294477 * (double)level));
            actor.Status.mp_recover_skill += (short)(-0.109999999403954 - 0.0299999993294477 * (double)level);
            actor.Buff.防御力上昇 = true;
            actor.Buff.魔法防御力上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_min_atk3"];
            actor.Status.min_matk_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_min_matk"];
            actor.Status.def_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_def"];
            actor.Status.mdef_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_mdef"];
            actor.Status.aspd_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_aspd"];
            actor.Status.cspd_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_cspd"];
            actor.Status.avoid_melee_skill -= (short)skill.Variable["AtkUp_DefUp_SpdDown_AvoDown_avoid_melee"];
            actor.Buff.防御力上昇 = false;
            actor.Buff.魔法防御力上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
