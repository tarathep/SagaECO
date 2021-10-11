namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MagIntDexDownOne" />.
    /// </summary>
    public class MagIntDexDownOne : ISkill
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
            int rate = 10 + 10 * (int)level;
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, nameof(MagIntDexDownOne), rate))
                return;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(MagIntDexDownOne), 30000);
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
            int num1 = -(7 + level);
            if (skill.Variable.ContainsKey("MagIntDexDownOne_int"))
                skill.Variable.Remove("MagIntDexDownOne_int");
            skill.Variable.Add("MagIntDexDownOne_int", num1);
            actor.Status.int_skill = (short)num1;
            int num2 = -(7 + level);
            if (skill.Variable.ContainsKey("MagIntDexDownOne_mag"))
                skill.Variable.Remove("MagIntDexDownOne_mag");
            skill.Variable.Add("MagIntDexDownOne_mag", num2);
            actor.Status.mag_skill = (short)num2;
            int num3 = -(11 + level);
            if (skill.Variable.ContainsKey("MagIntDexDownOne_dex"))
                skill.Variable.Remove("MagIntDexDownOne_dex");
            skill.Variable.Add("MagIntDexDownOne_dex", num3);
            actor.Status.dex_skill = (short)num3;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.int_skill -= (short)skill.Variable["MagIntDexDownOne_int"];
            actor.Status.mag_skill -= (short)skill.Variable["MagIntDexDownOne_mag"];
            actor.Status.dex_skill -= (short)skill.Variable["MagIntDexDownOne_dex"];
        }
    }
}
