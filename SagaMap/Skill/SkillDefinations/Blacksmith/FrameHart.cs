namespace SagaMap.Skill.SkillDefinations.Blacksmith
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="FrameHart" />.
    /// </summary>
    public class FrameHart : ISkill
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
            int lifetime = 10000 + 10000 * (int)level;
            ActorPC possesionedActor = Singleton<SkillHandler>.Instance.GetPossesionedActor((ActorPC)sActor);
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, (SagaDB.Actor.Actor)possesionedActor, nameof(FrameHart), lifetime);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition((SagaDB.Actor.Actor)possesionedActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            int level = (int)skill.skill.Level;
            int num1 = (int)((double)actor.Status.max_atk_ori * (0.04 + 0.1 * (double)level));
            int num2 = (int)((double)actor.Status.min_atk_ori * (0.04 + 0.1 * (double)level));
            int num3 = (int)((double)actor.Status.max_atk_ori * (0.04 + 0.1 * (double)level));
            int num4 = (int)((double)actor.Status.min_atk_ori * (0.04 + 0.1 * (double)level));
            int num5 = (int)((double)actor.Status.max_atk_ori * (0.04 + 0.1 * (double)level));
            int num6 = (int)((double)actor.Status.min_atk_ori * (0.04 + 0.1 * (double)level));
            if (skill.Variable.ContainsKey("PoisonReate1_Max"))
                skill.Variable.Remove("PoisonReate1_Max");
            skill.Variable.Add("PoisonReate1_Max", num1);
            actor.Status.max_atk1_skill += (short)num1;
            if (skill.Variable.ContainsKey("PoisonReate2_Max"))
                skill.Variable.Remove("PoisonReate2_Max");
            skill.Variable.Add("PoisonReate2_Max", num3);
            actor.Status.max_atk2_skill += (short)num3;
            if (skill.Variable.ContainsKey("PoisonReate3_Max"))
                skill.Variable.Remove("PoisonReate3_Max");
            skill.Variable.Add("PoisonReate3_Max", num5);
            actor.Status.max_atk3_skill += (short)num5;
            if (skill.Variable.ContainsKey("PoisonReate1_Min"))
                skill.Variable.Remove("PoisonReate1_Min");
            skill.Variable.Add("PoisonReate1_Min", num2);
            actor.Status.min_atk1_skill += (short)num2;
            if (skill.Variable.ContainsKey("PoisonReate2_Min"))
                skill.Variable.Remove("PoisonReate2_Min");
            skill.Variable.Add("PoisonReate2_Min", num4);
            actor.Status.min_atk2_skill += (short)num4;
            if (skill.Variable.ContainsKey("PoisonReate3_Min"))
                skill.Variable.Remove("PoisonReate3_Min");
            skill.Variable.Add("PoisonReate3_Min", num6);
            actor.Status.min_atk3_skill += (short)num6;
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
            actor.Status.max_atk1_skill -= (short)skill.Variable["PoisonReate1_Max"];
            actor.Status.max_atk2_skill -= (short)skill.Variable["PoisonReate2_Max"];
            actor.Status.max_atk3_skill -= (short)skill.Variable["PoisonReate3_Max"];
            actor.Status.min_atk1_skill -= (short)skill.Variable["PoisonReate1_Min"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["PoisonReate2_Min"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["PoisonReate3_Min"];
            actor.Buff.最小攻撃力上昇 = false;
            actor.Buff.最大攻撃力上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
