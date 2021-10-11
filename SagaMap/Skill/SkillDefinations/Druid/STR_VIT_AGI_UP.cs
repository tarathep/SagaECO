namespace SagaMap.Skill.SkillDefinations.Druid
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="STR_VIT_AGI_UP" />.
    /// </summary>
    public class STR_VIT_AGI_UP : ISkill
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
            int[] numArray = new int[5] { 15, 20, 25, 27, 30 };
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(STR_VIT_AGI_UP), numArray[(int)level - 1] * 1000);
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
            int level = (int)skill.skill.Level;
            short[] numArray1 = new short[5]
            {
        (short) 3,
        (short) 4,
        (short) 5,
        (short) 6,
        (short) 8
            };
            short[] numArray2 = new short[5]
            {
        (short) 4,
        (short) 5,
        (short) 7,
        (short) 9,
        (short) 10
            };
            short[] numArray3 = new short[5]
            {
        (short) 7,
        (short) 9,
        (short) 11,
        (short) 13,
        (short) 15
            };
            if (skill.Variable.ContainsKey("STR_VIT_AGI_UP_STR"))
                skill.Variable.Remove("STR_VIT_AGI_UP_STR");
            skill.Variable.Add("STR_VIT_AGI_UP_STR", (int)numArray1[level - 1]);
            actor.Status.str_skill += numArray1[level - 1];
            if (skill.Variable.ContainsKey("STR_VIT_AGI_UP_VIT"))
                skill.Variable.Remove("STR_VIT_AGI_UP_VIT");
            skill.Variable.Add("STR_VIT_AGI_UP_VIT", (int)numArray2[level - 1]);
            actor.Status.vit_skill += numArray2[level - 1];
            if (skill.Variable.ContainsKey("STR_VIT_AGI_UP_AGI"))
                skill.Variable.Remove("STR_VIT_AGI_UP_AGI");
            skill.Variable.Add("STR_VIT_AGI_UP_AGI", (int)numArray3[level - 1]);
            actor.Status.agi_skill += numArray3[level - 1];
            actor.Buff.STR上昇 = true;
            actor.Buff.AGI上昇 = true;
            actor.Buff.VIT上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.str_skill -= (short)skill.Variable["STR_VIT_AGI_UP_STR"];
            actor.Status.vit_skill -= (short)skill.Variable["STR_VIT_AGI_UP_VIT"];
            actor.Status.agi_skill -= (short)skill.Variable["STR_VIT_AGI_UP_AGI"];
            actor.Buff.STR上昇 = false;
            actor.Buff.AGI上昇 = false;
            actor.Buff.VIT上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
