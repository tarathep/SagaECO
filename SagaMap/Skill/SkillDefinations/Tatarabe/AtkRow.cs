namespace SagaMap.Skill.SkillDefinations.Tatarabe
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="AtkRow" />.
    /// </summary>
    public class AtkRow : ISkill
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
            int lifetime = 70000 - 10000 * (int)level;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(AtkRow), lifetime);
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
            float num1 = 0.1f * (float)skill.skill.Level;
            int num2 = -(int)((double)actor.Status.max_atk_ori * (double)num1);
            int num3 = -(int)((double)actor.Status.min_atk_ori * (double)num1);
            int num4 = -(int)((double)actor.Status.max_atk_ori * (double)num1);
            int num5 = -(int)((double)actor.Status.min_atk_ori * (double)num1);
            int num6 = -(int)((double)actor.Status.max_atk_ori * (double)num1);
            int num7 = -(int)((double)actor.Status.min_atk_ori * (double)num1);
            if (skill.Variable.ContainsKey("AtkRow_max_atk1_add"))
                skill.Variable.Remove("AtkRow_max_atk1_add");
            skill.Variable.Add("AtkRow_max_atk1_add", num2);
            actor.Status.max_atk1_skill += (short)num2;
            if (skill.Variable.ContainsKey("AtkRow_max_atk2_add"))
                skill.Variable.Remove("AtkRow_max_atk2_add");
            skill.Variable.Add("AtkRow_max_atk2_add", num4);
            actor.Status.max_atk2_skill += (short)num4;
            if (skill.Variable.ContainsKey("AtkRow_max_atk3_add"))
                skill.Variable.Remove("AtkRow_max_atk3_add");
            skill.Variable.Add("AtkRow_max_atk3_add", num6);
            actor.Status.max_atk3_skill += (short)num6;
            if (skill.Variable.ContainsKey("AtkRow_min_atk1_add"))
                skill.Variable.Remove("AtkRow_min_atk1_add");
            skill.Variable.Add("AtkRow_min_atk1_add", num3);
            actor.Status.min_atk1_skill += (short)num3;
            if (skill.Variable.ContainsKey("AtkRow_min_atk2_add"))
                skill.Variable.Remove("AtkRow_min_atk2_add");
            skill.Variable.Add("AtkRow_min_atk2_add", num5);
            actor.Status.min_atk2_skill += (short)num5;
            if (skill.Variable.ContainsKey("AtkRow_min_atk3_add"))
                skill.Variable.Remove("AtkRow_min_atk3_add");
            skill.Variable.Add("AtkRow_min_atk3_add", num7);
            actor.Status.min_atk3_skill += (short)num7;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkRow_max_atk1_add"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkRow_max_atk2_add"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkRow_max_atk3_add"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["AtkRow_min_atk1_add"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["AtkRow_min_atk2_add"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["AtkRow_min_atk3_add"];
        }
    }
}
