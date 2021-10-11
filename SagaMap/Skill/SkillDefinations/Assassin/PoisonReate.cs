namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="PoisonReate" />.
    /// </summary>
    public class PoisonReate : ISkill
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
            uint itemID = 10000353;
            if (Singleton<SkillHandler>.Instance.CountItem(pc, itemID) <= 0)
                return -57;
            if (!this.CheckPossible((SagaDB.Actor.Actor)pc))
                return -5;
            Singleton<SkillHandler>.Instance.TakeItem(pc, itemID, (ushort)1);
            return 0;
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
            return Singleton<SkillHandler>.Instance.isEquipmentRight(sActor, ItemType.CLAW) || Singleton<SkillHandler>.Instance.CheckDEMRightEquip(sActor, ItemType.PARTS_SLASH);
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
            SagaDB.Actor.Actor possesionedActor = (SagaDB.Actor.Actor)Singleton<SkillHandler>.Instance.GetPossesionedActor((ActorPC)sActor);
            if (!this.CheckPossible(possesionedActor))
                return;
            int lifetime = 20000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, possesionedActor, nameof(PoisonReate), lifetime);
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
            float num = 0.0f;
            int level = (int)skill.skill.Level;
            int rate = 0;
            switch (level)
            {
                case 1:
                    num = 1.33f;
                    rate = 1;
                    break;
                case 2:
                    rate = 25;
                    num = 1.5f;
                    break;
                case 3:
                    rate = 50;
                    num = 1.67f;
                    break;
            }
            actor.Status.aspd_skill_perc += num;
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(actor, actor, SkillHandler.DefaultAdditions.Poison, rate))
            {
                Poison poison = new Poison(skill.skill, actor, 7000);
                SkillHandler.ApplyAddition(actor, (Addition)poison);
            }
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            float num = 0.0f;
            switch (skill.skill.Level)
            {
                case 1:
                    num = 1.33f;
                    break;
                case 2:
                    num = 1.5f;
                    break;
                case 3:
                    num = 1.67f;
                    break;
            }
            if ((double)actor.Status.aspd_skill_perc > (double)num)
                actor.Status.aspd_skill_perc -= num;
            else
                actor.Status.aspd_skill_perc = 0.0f;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
