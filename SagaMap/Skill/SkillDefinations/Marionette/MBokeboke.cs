namespace SagaMap.Skill.SkillDefinations.Marionette
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MBokeboke" />.
    /// </summary>
    public class MBokeboke : ISkill
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
            int lifetime = 1000 * (int)level;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(MBokeboke), lifetime);
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
            int num1 = -(int)((double)actor.Status.max_atk1 * 0.100000001490116);
            if (skill.Variable.ContainsKey("MBokeboke_max_atk1"))
                skill.Variable.Remove("MBokeboke_max_atk1");
            skill.Variable.Add("MBokeboke_max_atk1", num1);
            actor.Status.max_atk1_skill += (short)num1;
            int num2 = -(int)((double)actor.Status.max_atk2 * 0.100000001490116);
            if (skill.Variable.ContainsKey("MBokeboke_max_atk2"))
                skill.Variable.Remove("MBokeboke_max_atk2");
            skill.Variable.Add("MBokeboke_max_atk2", num2);
            actor.Status.max_atk2_skill += (short)num2;
            int num3 = -(int)((double)actor.Status.max_atk3 * 0.100000001490116);
            if (skill.Variable.ContainsKey("MBokeboke_max_atk3"))
                skill.Variable.Remove("MBokeboke_max_atk3");
            skill.Variable.Add("MBokeboke_max_atk3", num3);
            actor.Status.max_atk3_skill += (short)num3;
            int num4 = -(int)((double)actor.Status.min_atk1 * 0.100000001490116);
            if (skill.Variable.ContainsKey("MBokeboke_min_atk1"))
                skill.Variable.Remove("MBokeboke_min_atk1");
            skill.Variable.Add("MBokeboke_min_atk1", num4);
            actor.Status.min_atk1_skill += (short)num4;
            int num5 = -(int)((double)actor.Status.min_atk2 * 0.100000001490116);
            if (skill.Variable.ContainsKey("MBokeboke_min_atk2"))
                skill.Variable.Remove("MBokeboke_min_atk2");
            skill.Variable.Add("MBokeboke_min_atk2", num5);
            actor.Status.min_atk2_skill += (short)num5;
            int num6 = -(int)((double)actor.Status.min_atk3 * 0.100000001490116);
            if (skill.Variable.ContainsKey("MBokeboke_min_atk3"))
                skill.Variable.Remove("MBokeboke_min_atk3");
            skill.Variable.Add("MBokeboke_min_atk3", num6);
            actor.Status.min_atk3_skill += (short)num6;
            int num7 = -(int)((double)actor.Status.max_matk * 0.100000001490116);
            if (skill.Variable.ContainsKey("MBokeboke_max_matk"))
                skill.Variable.Remove("MBokeboke_max_matk");
            skill.Variable.Add("MBokeboke_max_matk", num7);
            actor.Status.max_matk_skill += (short)num7;
            int num8 = -(int)((double)actor.Status.min_matk * 0.100000001490116);
            if (skill.Variable.ContainsKey("MBokeboke_min_matk"))
                skill.Variable.Remove("MBokeboke_min_matk");
            skill.Variable.Add("MBokeboke_min_matk", num8);
            actor.Status.min_matk_skill += (short)num8;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["MBokeboke_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["MBokeboke_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["MBokeboke_max_atk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["MBokeboke_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["MBokeboke_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["MBokeboke_min_atk3"];
            actor.Status.max_matk_skill -= (short)skill.Variable["MBokeboke_max_matk"];
            actor.Status.min_matk_skill -= (short)skill.Variable["MBokeboke_min_matk"];
            actor.Status.vit_skill -= (short)skill.Variable["MBokeboke_vit"];
        }
    }
}
