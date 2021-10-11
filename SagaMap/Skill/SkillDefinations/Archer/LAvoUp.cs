namespace SagaMap.Skill.SkillDefinations.Archer
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="LAvoUp" />.
    /// </summary>
    public class LAvoUp : ISkill
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
            if (sActor.type != ActorType.PC)
                return;
            bool ifActivate = true;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, "LAVOUp", ifActivate);
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
            int num1 = 4 + 4 * (int)skill.skill.Level;
            if (skill.Variable.ContainsKey("LAVOUp-HitUp"))
                skill.Variable.Remove("LAVOUp-HitUp");
            skill.Variable.Add("LAVOUp-HitUp", num1);
            actor.Status.hit_ranged_skill += (short)num1;
            int num2 = 2 + 4 * (int)skill.skill.Level;
            if (skill.Variable.ContainsKey("LAVOUp-VoUp"))
                skill.Variable.Remove("LAVOUp-VoUp");
            skill.Variable.Add("LAVOUp-VoUp", num2);
            actor.Status.avoid_ranged_skill += (short)num2;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            if (actor.type != ActorType.PC)
                return;
            int num1 = skill.Variable["LAVOUp-HitUp"];
            actor.Status.hit_ranged_skill -= (short)num1;
            int num2 = skill.Variable["LAVOUp-VoUp"];
            actor.Status.avoid_ranged_skill -= (short)num2;
        }
    }
}
