namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="Soul" />.
    /// </summary>
    public class Soul : ISkill
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
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(Soul), true);
            defaultPassiveSkill.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEventHandler);
            defaultPassiveSkill.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(sActor, (Addition)defaultPassiveSkill);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            this.SetupSkill(actor, skill);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            this.DeleteSkill(actor, skill);
        }

        /// <summary>
        /// The DeleteSkill.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void DeleteSkill(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.min_atk1_possession = (short)0;
            actor.Status.min_atk2_possession = (short)0;
            actor.Status.min_atk3_possession = (short)0;
            actor.Status.max_atk1_skill = (short)0;
            actor.Status.max_atk2_skill = (short)0;
            actor.Status.max_atk3_skill = (short)0;
            actor.Status.hp_possession = (short)0;
            actor.Status.mp_possession = (short)0;
            actor.Status.sp_possession = (short)0;
            actor.Status.max_matk_possession = (short)0;
            actor.Status.min_matk_possession = (short)0;
            actor.Status.avoid_melee_possession = (short)0;
            actor.Status.avoid_ranged_possession = (short)0;
            actor.Status.hit_melee_possession = (short)0;
            actor.Status.hit_ranged_possession = (short)0;
            actor.Status.def_possession = (short)0;
            actor.Status.def_add_possession = (short)0;
            actor.Status.mdef_possession = (short)0;
            actor.Status.mdef_add_possession = (short)0;
        }

        /// <summary>
        /// The SetupSkill.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void SetupSkill(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            ActorPC actorPc = (ActorPC)actor;
            int num1 = 0;
            int num2 = 0;
            int num3 = 0;
            int num4 = 0;
            int num5 = 0;
            int num6 = 0;
            int num7 = 0;
            int num8 = 0;
            int num9 = 0;
            int num10 = 0;
            int num11 = 0;
            int num12 = 0;
            int num13 = 0;
            int num14 = 0;
            int num15 = 0;
            int num16 = 0;
            int num17 = 0;
            int num18 = 0;
            int num19 = 0;
            if (actorPc.PossessionPosition == PossessionPosition.RIGHT_HAND)
            {
                num1 = (int)((double)actor.Status.max_atk1 * 0.14);
                num2 = (int)((double)actor.Status.min_atk1 * 0.14);
                num3 = (int)((double)actor.Status.max_atk2 * 0.14);
                num4 = (int)((double)actor.Status.min_atk2 * 0.14);
                num5 = (int)((double)actor.Status.max_atk3 * 0.14);
                num6 = (int)((double)actor.Status.min_atk3 * 0.14);
                num7 = (int)((double)actor.Status.max_matk * 0.14);
                num8 = (int)((double)actor.Status.min_matk * 0.14);
                num9 = (int)((double)actor.Status.avoid_ranged * 0.06);
                num10 = (int)((double)actor.Status.avoid_ranged * 0.06);
                num11 = (int)((double)actor.Status.avoid_ranged * 0.14);
                num12 = (int)((double)actor.Status.avoid_ranged * 0.14);
                num13 = (int)((double)actor.Status.def * 0.03);
                num15 = (int)((double)actor.Status.mdef * 0.03);
                num17 = (int)((double)actor.MaxHP * 0.05);
                num18 = (int)((double)actor.MaxMP * 0.05);
                num19 = (int)((double)actor.MaxSP * 0.05);
            }
            else if (actorPc.PossessionPosition == PossessionPosition.LEFT_HAND)
            {
                num1 = (int)((double)actor.Status.max_atk1 * 0.07);
                num2 = (int)((double)actor.Status.min_atk1 * 0.07);
                num3 = (int)((double)actor.Status.max_atk2 * 0.07);
                num4 = (int)((double)actor.Status.min_atk2 * 0.07);
                num5 = (int)((double)actor.Status.max_atk3 * 0.07);
                num6 = (int)((double)actor.Status.min_atk3 * 0.07);
                num7 = (int)((double)actor.Status.max_matk * 0.07);
                num8 = (int)((double)actor.Status.min_matk * 0.05);
                num9 = (int)((double)actor.Status.avoid_ranged * 0.14);
                num10 = (int)((double)actor.Status.avoid_ranged * 0.07);
                num11 = (int)((double)actor.Status.avoid_ranged * 0.06);
                num12 = (int)((double)actor.Status.avoid_ranged * 0.06);
                num13 = (int)((double)actor.Status.def * 0.13);
                num15 = (int)((double)actor.Status.mdef * 0.13);
                num17 = (int)((double)actor.MaxHP * 0.07);
                num18 = (int)((double)actor.MaxMP * 0.07);
                num19 = (int)((double)actor.MaxSP * 0.07);
            }
            else if (actorPc.PossessionPosition == PossessionPosition.NECK)
            {
                num1 = (int)((double)actor.Status.max_atk1 * 0.08);
                num2 = (int)((double)actor.Status.min_atk1 * 0.08);
                num3 = (int)((double)actor.Status.max_atk2 * 0.08);
                num4 = (int)((double)actor.Status.min_atk2 * 0.08);
                num5 = (int)((double)actor.Status.max_atk3 * 0.08);
                num6 = (int)((double)actor.Status.min_atk3 * 0.08);
                num7 = (int)((double)actor.Status.max_matk * 0.08);
                num8 = (int)((double)actor.Status.min_matk * 0.08);
                num9 = (int)((double)actor.Status.avoid_ranged * 0.07);
                num10 = (int)((double)actor.Status.avoid_ranged * 0.14);
                num11 = (int)((double)actor.Status.avoid_ranged * 0.09);
                num12 = (int)((double)actor.Status.avoid_ranged * 0.09);
                num13 = (int)((double)actor.Status.def * 0.07);
                num15 = (int)((double)actor.Status.mdef * 0.07);
                num17 = (int)((double)actor.MaxHP * 0.07);
                num18 = (int)((double)actor.MaxMP * 0.07);
                num19 = (int)((double)actor.MaxSP * 0.07);
            }
            else if (actorPc.PossessionPosition == PossessionPosition.CHEST)
            {
                num1 = (int)((double)actor.Status.max_atk1 * 0.05);
                num2 = (int)((double)actor.Status.min_atk1 * 0.05);
                num3 = (int)((double)actor.Status.max_atk2 * 0.05);
                num4 = (int)((double)actor.Status.min_atk2 * 0.05);
                num5 = (int)((double)actor.Status.max_atk3 * 0.05);
                num6 = (int)((double)actor.Status.min_atk3 * 0.05);
                num7 = (int)((double)actor.Status.max_matk * 0.05);
                num8 = (int)((double)actor.Status.min_matk * 0.05);
                num9 = (int)((double)actor.Status.avoid_ranged * 0.08);
                num10 = (int)((double)actor.Status.avoid_ranged * 0.06);
                num11 = (int)((double)actor.Status.avoid_ranged * 0.05);
                num12 = (int)((double)actor.Status.avoid_ranged * 0.05);
                num13 = (int)((double)actor.Status.def * 0.07);
                num15 = (int)((double)actor.Status.mdef * 0.07);
                num17 = (int)((double)actor.MaxHP * 0.14);
                num18 = (int)((double)actor.MaxMP * 0.14);
                num19 = (int)((double)actor.MaxSP * 0.14);
            }
            if (skill.Variable.ContainsKey("Soul_Max_MAtk"))
                skill.Variable.Remove("Soul_Max_MAtk");
            skill.Variable.Add("Soul_Max_MAtk", num7);
            actor.Status.max_matk_possession = (short)num7;
            if (skill.Variable.ContainsKey("Soul_Min_MAtk"))
                skill.Variable.Remove("Soul_Min_MAtk");
            skill.Variable.Add("Soul_Min_MAtk", num8);
            actor.Status.min_matk_possession = (short)num8;
            if (skill.Variable.ContainsKey("Soul_avo_melee_add"))
                skill.Variable.Remove("Soul_avo_melee_add");
            skill.Variable.Add("Soul_avo_melee_add", num9);
            actor.Status.avoid_melee_possession = (short)num9;
            if (skill.Variable.ContainsKey("Soul_avo_ranged_add"))
                skill.Variable.Remove("Soul_avo_ranged_add");
            skill.Variable.Add("Soul_avo_ranged_add", num10);
            actor.Status.avoid_ranged_possession = (short)num10;
            if (skill.Variable.ContainsKey("Soul_hit_melee_add"))
                skill.Variable.Remove("Soul_hit_melee_add");
            skill.Variable.Add("Soul_hit_melee_add", num11);
            actor.Status.hit_melee_possession = (short)num11;
            if (skill.Variable.ContainsKey("Soul_hit_ranged_add"))
                skill.Variable.Remove("Soul_hit_ranged_add");
            skill.Variable.Add("Soul_hit_ranged_add", num12);
            actor.Status.hit_ranged_possession = (short)num12;
            if (skill.Variable.ContainsKey("Soul_left_def_add)"))
                skill.Variable.Remove("Soul_left_def_add");
            skill.Variable.Add("Soul_left_def_add", num13);
            actor.Status.def_possession = (short)num13;
            if (skill.Variable.ContainsKey("Soul_right_def_add"))
                skill.Variable.Remove("Soul_right_def_add");
            skill.Variable.Add("Soul_right_def_add", num14);
            actor.Status.def_add_possession = (short)num14;
            if (skill.Variable.ContainsKey("Soul_left_mdef_add"))
                skill.Variable.Remove("Soul_left_mdef_add");
            skill.Variable.Add("Soul_left_mdef_add", num15);
            actor.Status.mdef_possession = (short)num15;
            if (skill.Variable.ContainsKey("Soul_right_mdef_add"))
                skill.Variable.Remove("Soul_right_mdef_add");
            skill.Variable.Add("Soul_right_mdef_add", num16);
            actor.Status.mdef_add_possession = (short)num16;
            if (skill.Variable.ContainsKey("Soul_hp_add"))
                skill.Variable.Remove("Soul_hp_add");
            skill.Variable.Add("Soul_hp_add", num17);
            actor.Status.hp_possession = (short)num17;
            if (skill.Variable.ContainsKey("Soul_mp_add"))
                skill.Variable.Remove("Soul_mp_add");
            skill.Variable.Add("Soul_mp_add", num18);
            actor.Status.mp_possession = (short)num18;
            if (skill.Variable.ContainsKey("Soul_sp_add"))
                skill.Variable.Remove("Soul_sp_add");
            skill.Variable.Add("Soul_sp_add", num19);
            actor.Status.sp_possession = (short)num19;
            if (skill.Variable.ContainsKey("Soul_max_atk1_add"))
                skill.Variable.Remove("Soul_max_atk1_add");
            skill.Variable.Add("Soul_max_atk1_add", num1);
            actor.Status.max_atk1_possession = (short)num1;
            if (skill.Variable.ContainsKey("Soul_max_atk2_add"))
                skill.Variable.Remove("Soul_max_atk2_add");
            skill.Variable.Add("Soul_max_atk2_add", num3);
            actor.Status.max_atk2_possession = (short)num3;
            if (skill.Variable.ContainsKey("Soul_max_atk3_add"))
                skill.Variable.Remove("Soul_max_atk3_add");
            skill.Variable.Add("Soul_max_atk3_add", num5);
            actor.Status.max_atk3_possession = (short)num5;
            if (skill.Variable.ContainsKey("Soul_min_atk1_add"))
                skill.Variable.Remove("Soul_min_atk1_add");
            skill.Variable.Add("Soul_min_atk1_add", num2);
            actor.Status.min_atk1_possession = (short)num2;
            if (skill.Variable.ContainsKey("Soul_min_atk2_add"))
                skill.Variable.Remove("Soul_min_atk2_add");
            skill.Variable.Add("Soul_min_atk2_add", num4);
            actor.Status.min_atk2_possession = (short)num4;
            if (skill.Variable.ContainsKey("Soul_min_atk3_add"))
                skill.Variable.Remove("Soul_min_atk3_add");
            skill.Variable.Add("Soul_min_atk3_add", num6);
            actor.Status.min_atk3_possession = (short)num6;
        }
    }
}
