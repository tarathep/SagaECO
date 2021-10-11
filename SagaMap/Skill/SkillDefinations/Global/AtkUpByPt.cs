namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="AtkUpByPt" />.
    /// </summary>
    public class AtkUpByPt : ISkill
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
            if (((ActorPC)sActor).Party != null)
                ifActivate = true;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(AtkUpByPt), ifActivate);
            defaultPassiveSkill.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEventHandler);
            defaultPassiveSkill.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(sActor, (Addition)defaultPassiveSkill);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        public void StartEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            int level = (int)skill.skill.Level;
            int num1 = (int)((double)actor.Status.max_atk_ori * (0.0199999995529652 * (double)level - 0.00999999977648258));
            if (skill.Variable.ContainsKey("AtkUpByPt_max_atk1"))
                skill.Variable.Remove("AtkUpByPt_max_atk1");
            skill.Variable.Add("AtkUpByPt_max_atk1", num1);
            actor.Status.max_atk1_skill += (short)num1;
            int num2 = (int)((double)actor.Status.max_atk_ori * (0.0199999995529652 * (double)level - 0.00999999977648258));
            if (skill.Variable.ContainsKey("AtkUpByPt_max_atk2"))
                skill.Variable.Remove("AtkUpByPt_max_atk2");
            skill.Variable.Add("AtkUpByPt_max_atk2", num2);
            actor.Status.max_atk2_skill += (short)num2;
            int num3 = (int)((double)actor.Status.max_atk_ori * (0.0199999995529652 * (double)level - 0.00999999977648258));
            if (skill.Variable.ContainsKey("AtkUpByPt_max_atk3"))
                skill.Variable.Remove("AtkUpByPt_max_atk3");
            skill.Variable.Add("AtkUpByPt_max_atk3", num3);
            actor.Status.max_atk3_skill += (short)num3;
            int num4 = (int)((double)actor.Status.min_atk_ori * (0.0199999995529652 * (double)level - 0.00999999977648258));
            if (skill.Variable.ContainsKey("AtkUpByPt_min_atk1"))
                skill.Variable.Remove("AtkUpByPt_min_atk1");
            skill.Variable.Add("AtkUpByPt_min_atk1", num4);
            actor.Status.min_atk1_skill += (short)num4;
            int num5 = (int)((double)actor.Status.min_atk_ori * (0.0199999995529652 * (double)level - 0.00999999977648258));
            if (skill.Variable.ContainsKey("AtkUpByPt_min_atk2"))
                skill.Variable.Remove("AtkUpByPt_min_atk2");
            skill.Variable.Add("AtkUpByPt_min_atk2", num5);
            actor.Status.min_atk2_skill += (short)num5;
            int num6 = (int)((double)actor.Status.min_atk_ori * (0.0199999995529652 * (double)level - 0.00999999977648258));
            if (skill.Variable.ContainsKey("AtkUpByPt_min_atk3"))
                skill.Variable.Remove("AtkUpByPt_min_atk3");
            skill.Variable.Add("AtkUpByPt_min_atk3", num6);
            actor.Status.min_atk3_skill += (short)num6;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        public void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["AtkUpByPt_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["AtkUpByPt_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["AtkUpByPt_max_atk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkUpByPt_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkUpByPt_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkUpByPt_min_atk3"];
        }
    }
}
