namespace SagaMap.Skill.SkillDefinations.Gunner
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="VitalShot" />.
    /// </summary>
    public class VitalShot : ISkill
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
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)sActor, dActor) && this.CheckPossible((SagaDB.Actor.Actor)sActor) ? 0 : -5;
        }

        /// <summary>
        /// The CheckPossible.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool CheckPossible(SagaDB.Actor.Actor sActor)
        {
            if (sActor.type != ActorType.PC)
                return true;
            ActorPC actorPc = (ActorPC)sActor;
            return actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) && (actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.GUN || actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.DUALGUN || (actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.RIFLE || actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.BOW) || actorPc.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count > 0);
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
            float ATKBonus = (float)(2.09999990463257 + 0.100000001490116 * (double)level);
            int num = 25 + 5 * (int)level;
            int rate = 20 + 5 * (int)level;
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor, args, Elements.Neutral, ATKBonus);
            if (SagaLib.Global.Random.Next(0, 99) < num)
            {
                DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(VitalShot), 15000);
                defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
                defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
                SkillHandler.ApplyAddition(dActor, (Addition)defaultBuff);
            }
            if (!Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, dActor, SkillHandler.DefaultAdditions.鈍足, rate))
                return;
            int lifetime = 4000 + 500 * (int)level;
            鈍足 鈍足 = new 鈍足(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)鈍足);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int level = (int)skill.skill.Level;
            int num1 = -(int)((double)actor.Status.hit_melee * (0.0500000007450581 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("VitalShot_hit_melee"))
                skill.Variable.Remove("VitalShot_hit_melee");
            skill.Variable.Add("VitalShot_hit_melee", num1);
            actor.Status.hit_melee_skill = (short)num1;
            int num2 = -(int)((double)actor.Status.hit_ranged * (0.129999995231628 + 0.0900000035762787 * (double)level));
            if (skill.Variable.ContainsKey("VitalShot_hit_ranged"))
                skill.Variable.Remove("VitalShot_hit_ranged");
            skill.Variable.Add("VitalShot_hit_ranged", num2);
            actor.Status.hit_ranged_skill = (short)num2;
            int num3 = -(int)((double)actor.Status.avoid_melee * (0.100000001490116 + 0.0199999995529652 * (double)level));
            if (skill.Variable.ContainsKey("VitalShot_avoid_melee"))
                skill.Variable.Remove("VitalShot_avoid_melee");
            skill.Variable.Add("VitalShot_avoid_melee", num3);
            actor.Status.avoid_melee_skill = (short)num3;
            int num4 = -(int)((double)actor.Status.avoid_ranged * (0.0900000035762787 + 0.0299999993294477 * (double)level));
            if (skill.Variable.ContainsKey("VitalShot_avoid_ranged"))
                skill.Variable.Remove("VitalShot_avoid_ranged");
            skill.Variable.Add("VitalShot_avoid_ranged", num4);
            actor.Status.avoid_ranged_skill = (short)num4;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.hit_melee_skill -= (short)skill.Variable["VitalShot_hit_melee"];
            actor.Status.hit_ranged_skill -= (short)skill.Variable["VitalShot_hit_ranged"];
            actor.Status.avoid_melee_skill -= (short)skill.Variable["VitalShot_avoid_melee"];
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["VitalShot_avoid_ranged"];
        }
    }
}
