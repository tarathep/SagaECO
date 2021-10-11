namespace SagaMap.Skill.SkillDefinations.Machinery
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="RobotDefUp" />.
    /// </summary>
    public class RobotDefUp : ISkill
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
            ActorPet pet = Singleton<SkillHandler>.Instance.GetPet(sActor);
            if (pet == null)
                return;
            if (Singleton<SkillHandler>.Instance.CheckMobType((ActorMob)pet, "MACHINE_RIDE_ROBOT"))
                ifActivate = true;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(RobotDefUp), ifActivate);
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
            int num1 = (int)((double)actor.Status.def_add * (0.0799999982118607 + 0.0199999995529652 * (double)level));
            if (skill.Variable.ContainsKey("RobotDefUp_def_add"))
                skill.Variable.Remove("RobotDefUp_def_add");
            skill.Variable.Add("RobotDefUp_def_add", num1);
            actor.Status.def_add_skill += (short)num1;
            int num2 = (int)((double)actor.Status.mdef_add * (0.0799999982118607 + 0.0199999995529652 * (double)level));
            if (skill.Variable.ContainsKey("RobotDefUp_mdef_add"))
                skill.Variable.Remove("RobotDefUp_mdef_add");
            skill.Variable.Add("RobotDefUp_mdef_add", num2);
            actor.Status.mdef_add_skill += (short)num2;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.def_add_skill -= (short)skill.Variable["RobotDefUp_def_add"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["RobotDefUp_mdef_add"];
        }
    }
}
