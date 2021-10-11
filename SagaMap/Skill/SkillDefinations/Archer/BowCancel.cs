namespace SagaMap.Skill.SkillDefinations.Archer
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="BowCancel" />.
    /// </summary>
    public class BowCancel : ISkill
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
            return this.CheckPossible((SagaDB.Actor.Actor)pc) ? 0 : -5;
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
            return actorPc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND) && (actorPc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType == ItemType.BOW || Singleton<SkillHandler>.Instance.CheckDEMRightEquip(sActor, ItemType.PARTS_BLOW));
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
            args.dActor = 0U;
            SagaDB.Actor.Actor possesionedActor = (SagaDB.Actor.Actor)Singleton<SkillHandler>.Instance.GetPossesionedActor((ActorPC)sActor);
            if (!this.CheckPossible(possesionedActor))
                return;
            int lifetime = 20000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, possesionedActor, nameof(BowCancel), lifetime);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            defaultBuff.OnCheckValid += new DefaultBuff.ValidCheckEventHandler(this.ValidCheck);
            SkillHandler.ApplyAddition(possesionedActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The ValidCheck.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="result">The result<see cref="int"/>.</param>
        private void ValidCheck(ActorPC pc, SagaDB.Actor.Actor dActor, out int result)
        {
            result = this.TryCast(pc, dActor, (SkillArg)null);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.aspd_skill_perc += (float)(1.20000004768372 + 0.300000011920929 * (double)skill.skill.Level);
            actor.Buff.BowDelayCancel = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            float num = (float)(1.20000004768372 + 0.300000011920929 * (double)skill.skill.Level);
            if ((double)actor.Status.aspd_skill_perc > (double)num)
                actor.Status.aspd_skill_perc -= num;
            else
                actor.Status.aspd_skill_perc = 0.0f;
            actor.Buff.BowDelayCancel = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
