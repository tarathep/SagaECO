namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="AvoidUp" />.
    /// </summary>
    public class AvoidUp : ISkill
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
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(AvoidUp), false);
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
            float[] numArray1 = new float[4]
            {
        0.0f,
        0.05f,
        0.07f,
        0.11f
            };
            int num1 = (int)((double)actor.Status.avoid_melee * (double)numArray1[(int)skill.skill.Level]);
            if (skill.Variable.ContainsKey("AvoidUp_avoid_melee"))
                skill.Variable.Remove("AvoidUp_avoid_melee");
            skill.Variable.Add("AvoidUp_avoid_melee", num1);
            actor.Status.avoid_melee_skill = (short)num1;
            float[] numArray2 = new float[4]
            {
        0.0f,
        0.05f,
        0.07f,
        0.11f
            };
            int num2 = (int)((double)actor.Status.avoid_ranged * (double)numArray2[(int)skill.skill.Level]);
            if (skill.Variable.ContainsKey("AvoidUp_avoid_ranged"))
                skill.Variable.Remove("AvoidUp_avoid_ranged");
            skill.Variable.Add("AvoidUp_avoid_ranged", num2);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.avoid_melee_skill -= (short)skill.Variable["AvoidUp_avoid_melee"];
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["AvoidUp_avoid_ranged"];
        }
    }
}
