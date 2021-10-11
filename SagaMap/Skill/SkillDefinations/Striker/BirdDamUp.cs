namespace SagaMap.Skill.SkillDefinations.Striker
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="BirdDamUp" />.
    /// </summary>
    public class BirdDamUp : ISkill
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
            if (pet != null && Singleton<SkillHandler>.Instance.CheckMobType((ActorMob)pet, "BIRD"))
                ifActivate = true;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, dActor, nameof(BirdDamUp), ifActivate);
            defaultPassiveSkill.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEventHandler);
            defaultPassiveSkill.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(dActor, (Addition)defaultPassiveSkill);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            int level = (int)skill.skill.Level;
            int num1 = level == 5 ? 5 : 0;
            int num2 = 5 * level + num1;
            if (skill.Variable.ContainsKey("BirdDamUp_min_atk1"))
                skill.Variable.Remove("BirdDamUp_min_atk1");
            skill.Variable.Add("BirdDamUp_min_atk1", num2);
            actor.Status.min_atk1_skill += (short)num2;
            int num3 = 5 * level + num1;
            if (skill.Variable.ContainsKey("BirdDamUp_min_atk2"))
                skill.Variable.Remove("BirdDamUp_min_atk2");
            skill.Variable.Add("BirdDamUp_min_atk2", num3);
            actor.Status.min_atk2_skill += (short)num3;
            int num4 = 5 * level + num1;
            if (skill.Variable.ContainsKey("BirdDamUp_min_atk3"))
                skill.Variable.Remove("BirdDamUp_min_atk3");
            skill.Variable.Add("BirdDamUp_min_atk3", num4);
            actor.Status.min_atk3_skill += (short)num4;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.min_atk1_skill -= (short)skill.Variable["BirdDamUp_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["BirdDamUp_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["BirdDamUp_min_atk3"];
        }
    }
}
