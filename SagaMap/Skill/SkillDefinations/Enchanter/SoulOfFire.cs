namespace SagaMap.Skill.SkillDefinations.Enchanter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="SoulOfFire" />.
    /// </summary>
    public class SoulOfFire : ISkill
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
            int lifetime = 10000 * (int)level;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(SoulOfFire), lifetime);
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
            short num1 = 0;
            short num2 = 0;
            switch (skill.skill.Level)
            {
                case 1:
                    num1 = (short)10;
                    break;
                case 2:
                    num1 = (short)20;
                    break;
                case 3:
                    num1 = (short)30;
                    num2 = (short)40;
                    break;
            }
            if (skill.Variable.ContainsKey("SoulOfFire_max_atk1"))
                skill.Variable.Remove("SoulOfFire_max_atk1");
            skill.Variable.Add("SoulOfFire_max_atk1", (int)num1);
            actor.Status.max_atk1_skill += num1;
            if (skill.Variable.ContainsKey("SoulOfFire_max_atk2"))
                skill.Variable.Remove("SoulOfFire_max_atk2");
            skill.Variable.Add("SoulOfFire_max_atk2", (int)num1);
            actor.Status.max_atk2_skill += num1;
            if (skill.Variable.ContainsKey("SoulOfFire_max_atk3"))
                skill.Variable.Remove("SoulOfFire_max_atk3");
            skill.Variable.Add("SoulOfFire_max_atk3", (int)num1);
            actor.Status.max_atk3_skill += num1;
            if (skill.Variable.ContainsKey("SoulOfFire_min_atk1"))
                skill.Variable.Remove("SoulOfFire_min_atk1");
            skill.Variable.Add("SoulOfFire_min_atk1", (int)num1);
            actor.Status.min_atk1_skill += num1;
            if (skill.Variable.ContainsKey("SoulOfFire_min_atk2"))
                skill.Variable.Remove("SoulOfFire_min_atk2");
            skill.Variable.Add("SoulOfFire_min_atk2", (int)num1);
            actor.Status.min_atk2_skill += num1;
            if (skill.Variable.ContainsKey("SoulOfFire_min_atk3"))
                skill.Variable.Remove("SoulOfFire_min_atk3");
            skill.Variable.Add("SoulOfFire_min_atk3", (int)num1);
            actor.Status.min_atk3_skill = num1;
            actor.Buff.最小攻撃力上昇 = true;
            actor.Buff.最大攻撃力上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.max_atk1_skill -= (short)skill.Variable["SoulOfFire_max_atk1"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["SoulOfFire_max_atk2"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["SoulOfFire_max_atk3"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["SoulOfFire_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["SoulOfFire_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["SoulOfFire_min_atk3"];
            actor.Buff.最小攻撃力上昇 = false;
            actor.Buff.最大攻撃力上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
