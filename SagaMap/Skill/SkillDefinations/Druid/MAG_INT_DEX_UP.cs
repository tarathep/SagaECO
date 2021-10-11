namespace SagaMap.Skill.SkillDefinations.Druid
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MAG_INT_DEX_UP" />.
    /// </summary>
    public class MAG_INT_DEX_UP : ISkill
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
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(MAG_INT_DEX_UP), numArray[(int)level - 1] * 1000);
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
        (short) 6,
        (short) 8,
        (short) 10,
        (short) 12,
        (short) 14
            };
            short[] numArray2 = new short[5]
            {
        (short) 6,
        (short) 7,
        (short) 8,
        (short) 10,
        (short) 11
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
            actor.Buff.MAG上昇 = true;
            actor.Buff.INT上昇 = true;
            actor.Buff.DEX上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
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
            actor.Buff.MAG上昇 = false;
            actor.Buff.INT上昇 = false;
            actor.Buff.DEX上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
