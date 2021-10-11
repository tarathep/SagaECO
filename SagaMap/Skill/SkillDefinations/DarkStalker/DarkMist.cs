namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="DarkMist" />.
    /// </summary>
    public class DarkMist : ISkill
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
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)pc, dActor) ? 0 : -14;
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
            float MATKBonus = new float[6]
            {
        0.0f,
        1.2f,
        1.4f,
        1.6f,
        1.8f,
        2.1f
            }[(int)level];
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Dark, MATKBonus);
            int num = 15 + 5 * (int)level;
            if (SagaLib.Global.Random.Next(0, 99) >= num)
                return;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(DarkMist), num * 1000);
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
            int num1 = -(int)((double)actor.Status.avoid_ranged * (0.200000002980232 + 0.0500000007450581 * (double)level));
            int num2 = -(int)((double)actor.Status.avoid_melee * (0.200000002980232 + 0.0500000007450581 * (double)level));
            if (skill.Variable.ContainsKey("DarkMist_avo_range_down"))
                skill.Variable.Remove("DarkMist_avo_range_down");
            skill.Variable.Add("DarkMist_avo_range_down", num1);
            actor.Status.avoid_ranged_skill += (short)num1;
            if (skill.Variable.ContainsKey("DarkMist_avo_melee_down"))
                skill.Variable.Remove("DarkMist_avo_melee_down");
            skill.Variable.Add("DarkMist_avo_melee_down", num2);
            actor.Status.avoid_melee_skill += (short)num2;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["DarkMist_avo_range_down"];
            actor.Status.avoid_melee_skill -= (short)skill.Variable["DarkMist_avo_melee_down"];
        }
    }
}
