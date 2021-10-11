namespace SagaMap.Skill.SkillDefinations.Druid
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="UndeadMdefDownOne" />.
    /// </summary>
    public class UndeadMdefDownOne : ISkill
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
            int rate = 70 + 10 * (int)level;
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, nameof(UndeadMdefDownOne), rate))
                return;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(UndeadMdefDownOne), lifetime);
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
            int num1 = -(int)((double)actor.Status.mdef * 0.5);
            if (skill.Variable.ContainsKey("UndeadMdefDownOne_mdef"))
                skill.Variable.Remove("UndeadMdefDownOne_mdef");
            skill.Variable.Add("UndeadMdefDownOne_mdef", num1);
            actor.Status.mdef_skill += (short)num1;
            int num2 = -(int)((double)actor.Status.mdef_add * 0.5);
            if (skill.Variable.ContainsKey("UndeadMdefDownOne_mdef_add"))
                skill.Variable.Remove("UndeadMdefDownOne_mdef_add");
            skill.Variable.Add("UndeadMdefDownOne_mdef_add", num2);
            actor.Status.mdef_add_skill += (short)num2;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.mdef_skill -= (short)skill.Variable["UndeadMdefDownOne_mdef"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["UndeadMdefDownOne_mdef_add"];
        }
    }
}
