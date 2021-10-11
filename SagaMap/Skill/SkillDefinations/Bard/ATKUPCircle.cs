namespace SagaMap.Skill.SkillDefinations.Bard
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ATKUPCircle" />.
    /// </summary>
    public class ATKUPCircle : ISkill
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
                    DefaultBuff defaultBuff = new DefaultBuff(args.skill, actor, nameof(ATKUPCircle), lifetime);
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
            int num1 = 6 * level;
            if (skill.Variable.ContainsKey("ATKUPCircle_max_atk1"))
                skill.Variable.Remove("ATKUPCircle_max_atk1");
            skill.Variable.Add("ATKUPCircle_max_atk1", num1);
            actor.Status.max_atk1_skill = (short)num1;
            int num2 = 6 * level;
            if (skill.Variable.ContainsKey("ATKUPCircle_max_atk2"))
                skill.Variable.Remove("ATKUPCircle_max_atk2");
            skill.Variable.Add("ATKUPCircle_max_atk2", num2);
            actor.Status.max_atk2_skill = (short)num2;
            int num3 = 6 * level;
            if (skill.Variable.ContainsKey("ATKUPCircle_max_atk3"))
                skill.Variable.Remove("ATKUPCircle_max_atk3");
            skill.Variable.Add("ATKUPCircle_max_atk3", num3);
            actor.Status.max_atk3_skill = (short)num3;
            int num4 = 4 * level;
            if (skill.Variable.ContainsKey("ATKUPCircle_min_atk1"))
                skill.Variable.Remove("ATKUPCircle_min_atk1");
            skill.Variable.Add("ATKUPCircle_min_atk1", num4);
            actor.Status.min_atk1_skill = (short)num4;
            int num5 = 4 * level;
            if (skill.Variable.ContainsKey("ATKUPCircle_min_atk2"))
                skill.Variable.Remove("ATKUPCircle_min_atk2");
            skill.Variable.Add("ATKUPCircle_min_atk2", num5);
            actor.Status.min_atk2_skill = (short)num5;
            int num6 = 4 * level;
            if (skill.Variable.ContainsKey("ATKUPCircle_min_atk3"))
                skill.Variable.Remove("ATKUPCircle_min_atk3");
            skill.Variable.Add("ATKUPCircle_min_atk3", num6);
            actor.Status.min_atk3_skill = (short)num6;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["ATKUPCircle_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["ATKUPCircle_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["ATKUPCircle_max_atk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["ATKUPCircle_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["ATKUPCircle_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["ATKUPCircle_min_atk3"];
        }
    }
}
