namespace SagaMap.Skill.SkillDefinations.Gunner
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="CQB" />.
    /// </summary>
    public class CQB : ISkill
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
            int num = 6000 + 2000 * (int)level;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, dActor, nameof(CQB), true);
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
            int num1 = (int)actor.Status.hit_melee + (2 + 2 * level);
            if (skill.Variable.ContainsKey("CQB_hit_melee"))
                skill.Variable.Remove("CQB_hit_melee");
            skill.Variable.Add("CQB_hit_melee", num1);
            actor.Status.hit_melee_skill = (short)num1;
            int num2 = (int)actor.Status.avoid_melee + (2 + 3 * level);
            if (skill.Variable.ContainsKey("CQB_avoid_melee"))
                skill.Variable.Remove("CQB_avoid_melee");
            skill.Variable.Add("CQB_avoid_melee", num2);
            actor.Status.avoid_melee_skill = (short)num2;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.hit_melee_skill -= (short)skill.Variable["CQB_hit_melee"];
            actor.Status.avoid_melee_skill -= (short)skill.Variable["CQB_avoid_melee"];
        }
    }
}
