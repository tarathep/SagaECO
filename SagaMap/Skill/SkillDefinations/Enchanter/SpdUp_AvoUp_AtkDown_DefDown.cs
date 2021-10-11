namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="SpdUp_AvoUp_AtkDown_DefDown" />.
    /// </summary>
    public class SpdUp_AvoUp_AtkDown_DefDown : ISkill
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
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(SpdUp_AvoUp_AtkDown_DefDown), 30000);
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
            float num1 = (float)(-0.0500000007450581 - (double)level * 0.0199999995529652);
            float num2 = 0.0f;
            float num3 = 0.0f;
            float num4 = (float)(0.0799999982118607 + (double)level * 0.0399999991059303);
            switch (level)
            {
                case 1:
                    num2 = 0.08f;
                    num3 = -0.18f;
                    break;
                case 2:
                    num2 = 0.14f;
                    num3 = -0.21f;
                    break;
                case 3:
                    num2 = 0.17f;
                    num3 = -0.24f;
                    break;
            }
            int num5 = (int)((double)actor.Status.min_atk_ori * (double)num1);
            int num6 = (int)((double)actor.Status.min_matk_ori * (double)num1);
            int num7 = (int)((double)actor.Status.avoid_ranged * (double)num2);
            int num8 = (int)((double)actor.Status.def * (double)num3);
            int num9 = (int)((double)actor.Status.mdef * (double)num3);
            int num10 = (int)((double)actor.Status.aspd * (double)num4);
            int num11 = (int)((double)actor.Status.cspd * (double)num4);
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_atk"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_atk");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_atk", num5);
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_matk"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_matk");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_matk", num6);
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_avo"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_avo");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_avo", num7);
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill", (int)((double)num2 * 100.0));
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_def"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_def");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_def", num8);
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_mdef"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_mdef");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_mdef", num9);
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_aspd"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_aspd");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_aspd", num10);
            if (skill.Variable.ContainsKey("SpdUp_AvoUp_AtkDown_DefDown_cspd"))
                skill.Variable.Remove("SpdUp_AvoUp_AtkDown_DefDown_cspd");
            skill.Variable.Add("SpdUp_AvoUp_AtkDown_DefDown_cspd", num11);
            actor.Status.min_atk1_skill += (short)num5;
            actor.Status.min_atk2_skill += (short)num5;
            actor.Status.min_atk3_skill += (short)num5;
            actor.Status.min_matk_skill += (short)num6;
            actor.Status.avoid_ranged_skill += (short)num7;
            actor.Status.mp_recover_skill += (short)((double)num2 * 100.0);
            actor.Status.def_skill += (short)num8;
            actor.Status.mdef_skill += (short)num9;
            actor.Status.aspd_skill += (short)num10;
            actor.Status.cspd_skill += (short)num11;
            actor.Buff.攻撃スピード上昇 = true;
            actor.Buff.詠唱スピード上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.min_atk1_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_atk"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_atk"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_atk"];
            actor.Status.min_matk_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_matk"];
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_avo"];
            actor.Status.mp_recover_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_mp_recover_skill"];
            actor.Status.def_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_def"];
            actor.Status.mdef_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_mdef"];
            actor.Status.aspd_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_aspd"];
            actor.Status.cspd_skill -= (short)skill.Variable["SpdUp_AvoUp_AtkDown_DefDown_cspd"];
            actor.Buff.攻撃スピード上昇 = false;
            actor.Buff.詠唱スピード上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
