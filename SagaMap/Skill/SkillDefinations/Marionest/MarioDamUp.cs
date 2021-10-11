namespace SagaMap.Skill.SkillDefinations.Marionest
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MarioDamUp" />.
    /// </summary>
    public class MarioDamUp : ISkill
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
            bool ifActivate = false;
            if (sActor.type == ActorType.PC && ((ActorPC)sActor).Marionette != null)
                ifActivate = true;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(MarioDamUp), ifActivate);
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
            int level = (int)skill.skill.Level;
            int num1 = (int)((double)actor.Status.max_atk_ori * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_max_atk1"))
                skill.Variable.Remove("MarioDamUp_max_atk1");
            skill.Variable.Add("MarioDamUp_max_atk1", num1);
            actor.Status.max_atk1_skill += (short)num1;
            int num2 = (int)((double)actor.Status.max_atk_ori * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_max_atk2"))
                skill.Variable.Remove("MarioDamUp_max_atk2");
            skill.Variable.Add("MarioDamUp_max_atk2", num2);
            actor.Status.max_atk2_skill += (short)num2;
            int num3 = (int)((double)actor.Status.max_atk_ori * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_max_atk3"))
                skill.Variable.Remove("MarioDamUp_max_atk3");
            skill.Variable.Add("MarioDamUp_max_atk3", num3);
            actor.Status.max_atk3_skill += (short)num3;
            int num4 = (int)((double)actor.Status.min_atk_ori * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_min_atk1"))
                skill.Variable.Remove("MarioDamUp_min_atk1");
            skill.Variable.Add("MarioDamUp_min_atk1", num4);
            actor.Status.min_atk1_skill += (short)num4;
            int num5 = (int)((double)actor.Status.min_atk_ori * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_min_atk2"))
                skill.Variable.Remove("MarioDamUp_min_atk2");
            skill.Variable.Add("MarioDamUp_min_atk2", num5);
            actor.Status.min_atk2_skill += (short)num5;
            int num6 = (int)((double)actor.Status.min_atk_ori * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_min_atk3"))
                skill.Variable.Remove("MarioDamUp_min_atk3");
            skill.Variable.Add("MarioDamUp_min_atk3", num6);
            actor.Status.min_atk3_skill += (short)num6;
            int num7 = (int)((double)actor.Status.min_matk * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_min_matk"))
                skill.Variable.Remove("MarioDamUp_min_matk");
            skill.Variable.Add("MarioDamUp_min_matk", num7);
            actor.Status.min_matk_skill += (short)num7;
            int num8 = (int)((double)actor.Status.def * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_def"))
                skill.Variable.Remove("MarioDamUp_def");
            skill.Variable.Add("MarioDamUp_def", num8);
            actor.Status.def_skill += (short)num8;
            int num9 = (int)((double)actor.Status.def_add * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_def_add"))
                skill.Variable.Remove("MarioDamUp_def_add");
            skill.Variable.Add("MarioDamUp_def_add", num9);
            actor.Status.def_add_skill += (short)num9;
            int num10 = (int)((double)actor.Status.mdef * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_mdef"))
                skill.Variable.Remove("MarioDamUp_mdef");
            skill.Variable.Add("MarioDamUp_mdef", num10);
            actor.Status.mdef_skill += (short)num10;
            int num11 = (int)((double)actor.Status.mdef_add * (0.0700000002980232 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("MarioDamUp_mdef_add"))
                skill.Variable.Remove("MarioDamUp_mdef_add");
            skill.Variable.Add("MarioDamUp_mdef_add", num11);
            actor.Status.mdef_add_skill += (short)num11;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["MarioDamUp_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["MarioDamUp_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["MarioDamUp_max_atk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["MarioDamUp_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["MarioDamUp_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["MarioDamUp_min_atk3"];
            actor.Status.min_matk_skill -= (short)skill.Variable["MarioDamUp_min_matk"];
            actor.Status.def_skill -= (short)skill.Variable["MarioDamUp_def"];
            actor.Status.def_add_skill -= (short)skill.Variable["MarioDamUp_def_add"];
            actor.Status.mdef_skill -= (short)skill.Variable["MarioDamUp_mdef"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["MarioDamUp_mdef_add"];
        }
    }
}
