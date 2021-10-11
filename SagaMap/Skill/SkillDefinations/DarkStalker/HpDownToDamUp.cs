namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="HpDownToDamUp" />.
    /// </summary>
    public class HpDownToDamUp : ISkill
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
            bool ifActivate = true;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(HpDownToDamUp), ifActivate);
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
            uint num1 = actor.HP / actor.MaxHP * 100U;
            float num2 = 0.0f;
            if (num1 <= 100U && num1 > 80U)
                num2 = 0.0f;
            else if (num1 <= 80U && num1 > 60U)
                num2 = 0.04f;
            else if (num1 <= 60U && num1 > 40U)
                num2 = 0.06f;
            else if (num1 <= 40U && num1 > 20U)
                num2 = 0.08f;
            else if (num1 <= 20U && num1 >= 0U)
                num2 = 0.1f;
            int num3 = (int)((double)actor.Status.max_atk_ori * (double)num2);
            if (skill.Variable.ContainsKey("HpDownToDamUp_max_atk1"))
                skill.Variable.Remove("HpDownToDamUp_max_atk1");
            skill.Variable.Add("HpDownToDamUp_max_atk1", num3);
            actor.Status.max_atk1_skill += (short)num3;
            int num4 = (int)((double)actor.Status.max_atk_ori * (double)num2);
            if (skill.Variable.ContainsKey("HpDownToDamUp_max_atk2"))
                skill.Variable.Remove("HpDownToDamUp_max_atk2");
            skill.Variable.Add("HpDownToDamUp_max_atk2", num4);
            actor.Status.max_atk2_skill += (short)num4;
            int num5 = (int)((double)actor.Status.max_atk_ori * (double)num2);
            if (skill.Variable.ContainsKey("HpDownToDamUp_max_atk3"))
                skill.Variable.Remove("HpDownToDamUp_max_atk3");
            skill.Variable.Add("HpDownToDamUp_max_atk3", num5);
            actor.Status.max_atk3_skill += (short)num5;
            int num6 = (int)((double)actor.Status.min_atk_ori * (double)num2);
            if (skill.Variable.ContainsKey("HpDownToDamUp_min_atk1"))
                skill.Variable.Remove("HpDownToDamUp_min_atk1");
            skill.Variable.Add("HpDownToDamUp_min_atk1", num6);
            actor.Status.min_atk1_skill += (short)num6;
            int num7 = (int)((double)actor.Status.min_atk_ori * (double)num2);
            if (skill.Variable.ContainsKey("HpDownToDamUp_min_atk2"))
                skill.Variable.Remove("HpDownToDamUp_min_atk2");
            skill.Variable.Add("HpDownToDamUp_min_atk2", num7);
            actor.Status.min_atk2_skill += (short)num7;
            int num8 = (int)((double)actor.Status.min_atk_ori * (double)num2);
            if (skill.Variable.ContainsKey("HpDownToDamUp_min_atk3"))
                skill.Variable.Remove("HpDownToDamUp_min_atk3");
            skill.Variable.Add("HpDownToDamUp_min_atk3", num8);
            actor.Status.min_atk3_skill += (short)num8;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["HpDownToDamUp_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["HpDownToDamUp_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["HpDownToDamUp_max_atk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["HpDownToDamUp_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["HpDownToDamUp_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["HpDownToDamUp_min_atk3"];
        }
    }
}
