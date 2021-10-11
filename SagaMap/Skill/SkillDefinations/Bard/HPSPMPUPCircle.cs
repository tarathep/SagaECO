namespace SagaMap.Skill.SkillDefinations.Bard
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="HPSPMPUPCircle" />.
    /// </summary>
    public class HPSPMPUPCircle : ISkill
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
            return Singleton<SkillHandler>.Instance.isEquipmentRight((SagaDB.Actor.Actor)sActor, ItemType.STRINGS) || sActor.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count > 0 ? 0 : -5;
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
            int lifetime = 6000 + 2000 * (int)level;
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)150, true);
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor actor in actorsArea)
            {
                if (actor.type == ActorType.PC || actor.type == ActorType.PET)
                {
                    DefaultBuff defaultBuff = new DefaultBuff(args.skill, actor, nameof(HPSPMPUPCircle), lifetime);
                    defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
                    defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
                    SkillHandler.ApplyAddition(actor, (Addition)defaultBuff);
                }
            }
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int level = (int)skill.skill.Level;
            actor.MaxHP += (uint)(50 + 50 * level);
            actor.MaxMP += (uint)(50 + 50 * level);
            actor.MaxSP += (uint)(50 * level);
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int level = (int)skill.skill.Level;
            actor.MaxHP -= (uint)(50 + 50 * level);
            actor.MaxMP -= (uint)(50 + 50 * level);
            actor.MaxSP -= (uint)(50 * level);
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor, true);
        }
    }
}
