namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="StrVitAgiDownOne" />.
    /// </summary>
    public class StrVitAgiDownOne : ISkill
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
            int num = 10 + 10 * (int)level;
            if (SagaLib.Global.Random.Next(0, 99) >= num)
                return;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(StrVitAgiDownOne), 30000);
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
            int num1 = -(5 + level);
            if (skill.Variable.ContainsKey("StrVitAgiDownOne_str"))
                skill.Variable.Remove("StrVitAgiDownOne_str");
            skill.Variable.Add("StrVitAgiDownOne_str", num1);
            actor.Status.str_skill = (short)num1;
            int num2 = -(8 + level);
            if (skill.Variable.ContainsKey("StrVitAgiDownOne_agi"))
                skill.Variable.Remove("StrVitAgiDownOne_agi");
            skill.Variable.Add("StrVitAgiDownOne_agi", num2);
            actor.Status.agi_skill = (short)num2;
            int num3 = -(7 + level);
            if (skill.Variable.ContainsKey("StrVitAgiDownOne_vit"))
                skill.Variable.Remove("StrVitAgiDownOne_vit");
            skill.Variable.Add("StrVitAgiDownOne_vit", num3);
            actor.Status.vit_skill = (short)num3;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.str_skill -= (short)skill.Variable["StrVitAgiDownOne_str"];
            actor.Status.agi_skill -= (short)skill.Variable["StrVitAgiDownOne_agi"];
            actor.Status.vit_skill -= (short)skill.Variable["StrVitAgiDownOne_vit"];
        }
    }
}
