namespace SagaMap.Skill.SkillDefinations.Machinery
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="RobotOverTune" />.
    /// </summary>
    public class RobotOverTune : ISkill
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
            ActorPet pet = Singleton<SkillHandler>.Instance.GetPet((SagaDB.Actor.Actor)sActor);
            return pet == null || !Singleton<SkillHandler>.Instance.CheckMobType((ActorMob)pet, "MACHINE_RIDE_ROBOT") ? -53 : 0;
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
            int lifetime = 30000 + 20000 * (int)level;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(RobotOverTune), lifetime);
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
            int num1 = (int)((double)actor.Status.max_atk_ori * (0.0500000007450581 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("RobotOverTune_max_atk1"))
                skill.Variable.Remove("RobotOverTune_max_atk1");
            skill.Variable.Add("RobotOverTune_max_atk1", num1);
            actor.Status.max_atk1_skill += (short)num1;
            int num2 = (int)((double)actor.Status.max_atk_ori * (0.0500000007450581 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("RobotOverTune_max_atk2"))
                skill.Variable.Remove("RobotOverTune_max_atk2");
            skill.Variable.Add("RobotOverTune_max_atk2", num2);
            actor.Status.max_atk2_skill += (short)num2;
            int num3 = (int)((double)actor.Status.max_atk_ori * (0.0500000007450581 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("RobotOverTune_max_atk3"))
                skill.Variable.Remove("RobotOverTune_max_atk3");
            skill.Variable.Add("RobotOverTune_max_atk3", num3);
            actor.Status.max_atk3_skill += (short)num3;
            int num4 = (int)((double)actor.Status.min_atk_ori * (0.0500000007450581 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("RobotOverTune_min_atk1"))
                skill.Variable.Remove("RobotOverTune_min_atk1");
            skill.Variable.Add("RobotOverTune_min_atk1", num4);
            actor.Status.min_atk1_skill += (short)num4;
            int num5 = (int)((double)actor.Status.min_atk_ori * (0.0500000007450581 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("RobotOverTune_min_atk2"))
                skill.Variable.Remove("RobotOverTune_min_atk2");
            skill.Variable.Add("RobotOverTune_min_atk2", num5);
            actor.Status.min_atk2_skill += (short)num5;
            int num6 = (int)((double)actor.Status.min_atk_ori * (0.0500000007450581 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("RobotOverTune_min_atk3"))
                skill.Variable.Remove("RobotOverTune_min_atk3");
            skill.Variable.Add("RobotOverTune_min_atk3", num6);
            actor.Status.min_atk3_skill += (short)num6;
            int num7 = (int)((double)actor.Status.max_matk * (0.0500000007450581 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("RobotOverTune_max_matk"))
                skill.Variable.Remove("RobotOverTune_max_matk");
            skill.Variable.Add("RobotOverTune_max_matk", num7);
            actor.Status.max_matk_skill += (short)num7;
            int num8 = (int)((double)actor.Status.min_matk * (0.0500000007450581 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("RobotOverTune_min_matk"))
                skill.Variable.Remove("RobotOverTune_min_matk");
            skill.Variable.Add("RobotOverTune_min_matk", num8);
            actor.Status.min_matk_skill += (short)num8;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["RobotOverTune_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["RobotOverTune_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["RobotOverTune_max_atk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["RobotOverTune_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["RobotOverTune_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["RobotOverTune_min_atk3"];
            actor.Status.max_matk_skill -= (short)skill.Variable["RobotOverTune_max_matk"];
            actor.Status.min_matk_skill -= (short)skill.Variable["RobotOverTune_min_matk"];
        }
    }
}
