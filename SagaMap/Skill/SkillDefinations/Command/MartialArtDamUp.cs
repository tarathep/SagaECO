namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MartialArtDamUp" />.
    /// </summary>
    public class MartialArtDamUp : ISkill
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
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(MartialArtDamUp), true);
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
            int num1 = (int)((double)actor.Status.min_atk1 * 0.100000001490116 * (double)level);
            if (skill.Variable.ContainsKey("MartialArtDamUp_min_atk1"))
                skill.Variable.Remove("MartialArtDamUp_min_atk1");
            skill.Variable.Add("MartialArtDamUp_min_atk1", num1);
            actor.Status.min_atk1_skill = (short)num1;
            int num2 = (int)((double)actor.Status.min_atk2 * 0.100000001490116 * (double)level);
            if (skill.Variable.ContainsKey("MartialArtDamUp_min_atk2"))
                skill.Variable.Remove("MartialArtDamUp_min_atk2");
            skill.Variable.Add("MartialArtDamUp_min_atk2", num2);
            actor.Status.min_atk2_skill = (short)num2;
            int num3 = (int)((double)actor.Status.min_atk3 * 0.100000001490116 * (double)level);
            if (skill.Variable.ContainsKey("MartialArtDamUp_min_atk3"))
                skill.Variable.Remove("MartialArtDamUp_min_atk3");
            skill.Variable.Add("MartialArtDamUp_min_atk3", num3);
            actor.Status.min_atk3_skill = (short)num3;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.min_atk1_skill -= (short)skill.Variable["MartialArtDamUp_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["MartialArtDamUp_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["MartialArtDamUp_min_atk3"];
        }
    }
}
