namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MobAtkupSelf" />.
    /// </summary>
    public class MobAtkupSelf : ISkill
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
            int lifetime = 50000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, sActor, nameof(MobAtkupSelf), lifetime);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(sActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int num1 = 20;
            if (skill.Variable.ContainsKey("MobAtkupSelf_max_atk1"))
                skill.Variable.Remove("MobAtkupSelf_max_atk1");
            skill.Variable.Add("MobAtkupSelf_max_atk1", num1);
            actor.Status.max_atk1_skill += (short)num1;
            int num2 = 20;
            if (skill.Variable.ContainsKey("MobAtkupSelf_max_atk2"))
                skill.Variable.Remove("MobAtkupSelf_max_atk2");
            skill.Variable.Add("MobAtkupSelf_max_atk2", num2);
            actor.Status.max_atk2_skill += (short)num2;
            int num3 = 20;
            if (skill.Variable.ContainsKey("MobAtkupSelf_max_atk3"))
                skill.Variable.Remove("MobAtkupSelf_max_atk3");
            skill.Variable.Add("MobAtkupSelf_max_atk3", num3);
            actor.Status.max_atk3_skill += (short)num3;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["MobAtkupSelf_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["MobAtkupSelf_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["MobAtkupSelf_max_atk3"];
        }
    }
}
