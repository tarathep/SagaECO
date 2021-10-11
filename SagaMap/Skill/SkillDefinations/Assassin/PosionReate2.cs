namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="PosionReate2" />.
    /// </summary>
    public class PosionReate2 : ISkill
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
            uint itemID = 10000354;
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
            int lifetime = (100 - 10 * (int)level) * 1000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, "PoisonReate2", lifetime);
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
            int rate = 50;
            float[] numArray1 = new float[6]
            {
        0.0f,
        0.05f,
        0.05f,
        0.08f,
        0.08f,
        0.11f
            };
            float[] numArray2 = new float[6]
            {
        0.0f,
        0.1f,
        0.12f,
        0.14f,
        0.16f,
        0.18f
            };
            int num1 = (int)((double)actor.Status.def * (double)numArray1[level]);
            if (skill.Variable.ContainsKey("PosionReate2_def"))
                skill.Variable.Remove("PosionReate2_def");
            skill.Variable.Add("PosionReate2_def", num1);
            actor.Status.def_skill = (short)num1;
            int num2 = (int)((double)actor.Status.def_add * (double)numArray2[level]);
            if (skill.Variable.ContainsKey("PosionReate2_def_add"))
                skill.Variable.Remove("PosionReate2_def_add");
            skill.Variable.Add("PosionReate2_def_add", num2);
            actor.Status.def_add_skill = (short)num2;
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
            actor.Status.def_skill -= (short)skill.Variable["PosionReate2_def"];
            actor.Status.def_add_skill -= (short)skill.Variable["PosionReate2_def_add"];
        }
    }
}
