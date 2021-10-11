namespace SagaMap.Skill.SkillDefinations.Trader
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="BagDamUp" />.
    /// </summary>
    public class BagDamUp : ISkill
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
            bool ifActivate = false;
            if (Singleton<SkillHandler>.Instance.isEquipmentRight(sActor, ItemType.LEFT_HANDBAG, ItemType.HANDBAG))
                ifActivate = true;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(BagDamUp), ifActivate);
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
            int level = (int)skill.skill.Level;
            int[] numArray1 = new int[6] { 0, 5, 10, 15, 20, 30 };
            int[] numArray2 = new int[6] { 0, 2, 2, 2, 2, 3 };
            int num1 = numArray1[level];
            if (skill.Variable.ContainsKey("BagDamUp_max_atk1"))
                skill.Variable.Remove("BagDamUp_max_atk1");
            skill.Variable.Add("BagDamUp_max_atk1", num1);
            actor.Status.max_atk1_skill += (short)num1;
            int num2 = numArray1[level];
            if (skill.Variable.ContainsKey("BagDamUp_max_atk2"))
                skill.Variable.Remove("BagDamUp_max_atk2");
            skill.Variable.Add("BagDamUp_max_atk2", num2);
            actor.Status.max_atk2_skill += (short)num2;
            int num3 = numArray1[level];
            if (skill.Variable.ContainsKey("BagDamUp_max_atk3"))
                skill.Variable.Remove("BagDamUp_max_atk3");
            skill.Variable.Add("BagDamUp_max_atk3", num3);
            actor.Status.max_atk3_skill += (short)num3;
            int num4 = numArray1[level];
            if (skill.Variable.ContainsKey("BagDamUp_min_atk1"))
                skill.Variable.Remove("BagDamUp_min_atk1");
            skill.Variable.Add("BagDamUp_min_atk1", num4);
            actor.Status.min_atk1_skill += (short)num4;
            int num5 = numArray1[level];
            if (skill.Variable.ContainsKey("BagDamUp_min_atk2"))
                skill.Variable.Remove("BagDamUp_min_atk2");
            skill.Variable.Add("BagDamUp_min_atk2", num5);
            actor.Status.min_atk2_skill += (short)num5;
            int num6 = numArray1[level];
            if (skill.Variable.ContainsKey("BagDamUp_min_atk3"))
                skill.Variable.Remove("BagDamUp_min_atk3");
            skill.Variable.Add("BagDamUp_min_atk3", num6);
            actor.Status.min_atk3_skill += (short)num6;
            int num7 = numArray2[level];
            if (skill.Variable.ContainsKey("BagDamUp_hit_melee"))
                skill.Variable.Remove("BagDamUp_hit_melee");
            skill.Variable.Add("BagDamUp_hit_melee", num7);
            actor.Status.hit_melee_skill += (short)num7;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["BagDamUp_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["BagDamUp_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["BagDamUp_max_atk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["BagDamUp_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["BagDamUp_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["BagDamUp_min_atk3"];
            actor.Status.hit_melee_skill -= (short)skill.Variable["BagDamUp_hit_melee"];
        }
    }
}
