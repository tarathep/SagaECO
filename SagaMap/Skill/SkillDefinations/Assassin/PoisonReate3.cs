namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="PoisonReate3" />.
    /// </summary>
    public class PoisonReate3 : ISkill
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
            uint itemID = 10000355;
            if (Singleton<SkillHandler>.Instance.CountItem(pc, itemID) <= 0)
                return -57;
            Singleton<SkillHandler>.Instance.TakeItem(pc, itemID, (ushort)1);
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
            int lifetime = 100000 - 1000 * (int)level;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, sActor, nameof(PoisonReate3), lifetime);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(sActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            float num1 = 0.0f;
            int level = (int)skill.skill.Level;
            int num2 = 9;
            int num3 = 0;
            int rate = 0;
            switch (level)
            {
                case 1:
                    num2 = (int)(0.07 * (double)actor.Status.max_atk3);
                    num3 = (int)(0.07 * (double)actor.Status.min_atk3);
                    rate = 0;
                    break;
                case 2:
                    num2 = (int)(0.09 * (double)actor.Status.max_atk3);
                    num3 = (int)(0.09 * (double)actor.Status.min_atk3);
                    rate = 12;
                    break;
                case 3:
                    num2 = (int)(0.11 * (double)actor.Status.max_atk3);
                    num3 = (int)(0.11 * (double)actor.Status.min_atk3);
                    rate = 24;
                    break;
                case 4:
                    num2 = (int)(0.13 * (double)actor.Status.max_atk3);
                    num3 = (int)(0.13 * (double)actor.Status.min_atk3);
                    rate = 36;
                    break;
                case 5:
                    num2 = (int)(0.15 * (double)actor.Status.max_atk3);
                    num3 = (int)(0.15 * (double)actor.Status.min_atk3);
                    rate = 50;
                    break;
            }
            int num4 = (int)((double)actor.Status.aspd * (double)num1);
            if (skill.Variable.ContainsKey("PoisonReate3_Max"))
                skill.Variable.Remove("PoisonReate3_Max");
            skill.Variable.Add("PoisonReate3_Max", num2);
            actor.Status.max_atk3_skill += (short)num2;
            if (skill.Variable.ContainsKey("PoisonReate3_Min"))
                skill.Variable.Remove("PoisonReate3_Min");
            skill.Variable.Add("PoisonReate3_Min", num3);
            actor.Status.min_atk3_skill += (short)num3;
            if (Singleton<SkillHandler>.Instance.CanAdditionApply(actor, actor, SkillHandler.DefaultAdditions.Poison, rate))
            {
                Poison poison = new Poison(skill.skill, actor, 7000);
                SkillHandler.ApplyAddition(actor, (Addition)poison);
            }
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
            actor.Status.max_atk3_skill -= (short)skill.Variable["PoisonReate3_Max"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["PoisonReate3_Min"];
            actor.Buff.最小攻撃力上昇 = false;
            actor.Buff.最大攻撃力上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
