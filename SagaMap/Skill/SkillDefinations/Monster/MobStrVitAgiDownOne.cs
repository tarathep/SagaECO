namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MobStrVitAgiDownOne" />.
    /// </summary>
    public class MobStrVitAgiDownOne : ISkill
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
            int rate = 30;
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, nameof(MobStrVitAgiDownOne), rate))
                return;
            int lifetime = 24000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(MobStrVitAgiDownOne), lifetime);
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
            int num1 = -11;
            if (skill.Variable.ContainsKey("MobStrVitAgiDownOne_str"))
                skill.Variable.Remove("MobStrVitAgiDownOne_str");
            skill.Variable.Add("MobStrVitAgiDownOne_str", num1);
            actor.Status.str_skill += (short)num1;
            int num2 = -18;
            if (skill.Variable.ContainsKey("MobStrVitAgiDownOne_agi"))
                skill.Variable.Remove("MobStrVitAgiDownOne_agi");
            skill.Variable.Add("MobStrVitAgiDownOne_agi", num2);
            actor.Status.agi_skill += (short)num2;
            int num3 = -12;
            if (skill.Variable.ContainsKey("MobStrVitAgiDownOne_vit"))
                skill.Variable.Remove("MobStrVitAgiDownOne_vit");
            skill.Variable.Add("MobStrVitAgiDownOne_vit", num3);
            actor.Status.vit_skill += (short)num3;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.str_skill -= (short)skill.Variable["MobStrVitAgiDownOne_str"];
            actor.Status.agi_skill -= (short)skill.Variable["MobStrVitAgiDownOne_agi"];
            actor.Status.vit_skill -= (short)skill.Variable["MobStrVitAgiDownOne_vit"];
        }
    }
}
