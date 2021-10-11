namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="ClawMastery" />.
    /// </summary>
    public class ClawMastery : ISkill
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
            bool ifActivate = false;
            if (sActor.type != ActorType.PC)
                return;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) && actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.CLAW)
                ifActivate = true;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(ClawMastery), ifActivate);
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
            int num1 = 10 + 10 * level;
            if (skill.Variable.ContainsKey("ClawMastery_min_atk1"))
                skill.Variable.Remove("ClawMastery_min_atk1");
            skill.Variable.Add("ClawMastery_min_atk1", num1);
            actor.Status.min_atk1_skill += (short)num1;
            int num2 = 10 + 10 * level;
            if (skill.Variable.ContainsKey("ClawMastery_min_atk2"))
                skill.Variable.Remove("ClawMastery_min_atk2");
            skill.Variable.Add("ClawMastery_min_atk2", num2);
            actor.Status.min_atk2_skill += (short)num2;
            int num3 = 10 + 10 * level;
            if (skill.Variable.ContainsKey("ClawMastery_min_atk3"))
                skill.Variable.Remove("ClawMastery_min_atk3");
            skill.Variable.Add("ClawMastery_min_atk3", num3);
            actor.Status.min_atk3_skill += (short)num3;
            int num4 = 4 * level;
            if (skill.Variable.ContainsKey("ClawMastery_hit_melee"))
                skill.Variable.Remove("ClawMastery_hit_melee");
            skill.Variable.Add("ClawMastery_hit_melee", num4);
            actor.Status.hit_melee_skill += (short)num4;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.min_atk1_skill -= (short)skill.Variable["ClawMastery_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["ClawMastery_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["ClawMastery_min_atk3"];
            actor.Status.hit_melee_skill -= (short)skill.Variable["ClawMastery_hit_melee"];
        }
    }
}
