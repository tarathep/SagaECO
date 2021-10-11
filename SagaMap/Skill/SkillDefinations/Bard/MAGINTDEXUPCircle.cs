namespace SagaMap.Skill.SkillDefinations.Bard
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MAGINTDEXUPCircle" />.
    /// </summary>
    public class MAGINTDEXUPCircle : ISkill
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
                    DefaultBuff defaultBuff = new DefaultBuff(args.skill, actor, nameof(MAGINTDEXUPCircle), lifetime);
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
            short[] numArray1 = new short[5]
            {
        (short) 6,
        (short) 7,
        (short) 8,
        (short) 9,
        (short) 10
            };
            short[] numArray2 = new short[5]
            {
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 9,
        (short) 10
            };
            short[] numArray3 = new short[5]
            {
        (short) 5,
        (short) 6,
        (short) 7,
        (short) 9,
        (short) 10
            };
            if (skill.Variable.ContainsKey("MAG_INT_DEX_UP_DEX"))
                skill.Variable.Remove("MAG_INT_DEX_UP_DEX");
            skill.Variable.Add("MAG_INT_DEX_UP_DEX", (int)numArray1[level - 1]);
            actor.Status.dex_skill += numArray1[level - 1];
            if (skill.Variable.ContainsKey("MAG_INT_DEX_UP_INT"))
                skill.Variable.Remove("MAG_INT_DEX_UP_INT");
            skill.Variable.Add("MAG_INT_DEX_UP_INT", (int)numArray2[level - 1]);
            actor.Status.int_skill += numArray2[level - 1];
            if (skill.Variable.ContainsKey("MAG_INT_DEX_UP_MAG"))
                skill.Variable.Remove("MAG_INT_DEX_UP_MAG");
            skill.Variable.Add("MAG_INT_DEX_UP_MAG", (int)numArray3[level - 1]);
            actor.Status.mag_skill += numArray3[level - 1];
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.dex_skill -= (short)skill.Variable["MAG_INT_DEX_UP_DEX"];
            actor.Status.int_skill -= (short)skill.Variable["MAG_INT_DEX_UP_INT"];
            actor.Status.mag_skill -= (short)skill.Variable["MAG_INT_DEX_UP_MAG"];
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, (MapEventArgs)null, actor, true);
        }
    }
}
