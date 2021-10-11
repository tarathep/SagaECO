namespace SagaMap.Skill.SkillDefinations.Explorer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="Blinding" />.
    /// </summary>
    public class Blinding : ISkill
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
            float ATKBonus = (float)(1.14999997615814 + 0.100000001490116 * (double)level);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            int lifetime = 6000 + 2000 * (int)level;
            int[] numArray = new int[4] { 0, 69, 78, 84 };
            if (SagaLib.Global.Random.Next(0, 99) >= numArray[(int)level])
                return;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(Blinding), lifetime);
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
            int num1 = -(int)((double)actor.Status.hit_melee * (0.319999992847443 + 0.0399999991059303 * (double)level));
            if (skill.Variable.ContainsKey("Blinding_hit_melee"))
                skill.Variable.Remove("Blinding_hit_melee");
            skill.Variable.Add("Blinding_hit_melee", num1);
            actor.Status.hit_melee_skill += (short)num1;
            int num2 = -(int)((double)actor.Status.hit_ranged * (0.270000010728836 + 0.0399999991059303 * (double)level));
            if (skill.Variable.ContainsKey("Blinding_hit_ranged"))
                skill.Variable.Remove("Blinding_hit_ranged");
            skill.Variable.Add("Blinding_hit_ranged", num2);
            actor.Status.hit_ranged_skill += (short)num2;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_melee_skill -= (short)skill.Variable["Blinding_hit_melee"];
            actor.Status.hit_ranged_skill -= (short)skill.Variable["Blinding_hit_ranged"];
        }
    }
}
