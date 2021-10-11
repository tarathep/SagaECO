namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="DefUpAvoDown" />.
    /// </summary>
    public class DefUpAvoDown : ISkill
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
            int lifetime = (45 - 5 * (int)level) * 1000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(DefUpAvoDown), lifetime);
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
            int num1 = -(int)((double)actor.Status.avoid_ranged * 0.100000001490116 * (double)level);
            int num2 = -(int)((double)actor.Status.avoid_melee * 0.100000001490116 * (double)level);
            int num3 = (int)((double)actor.Status.def_add * (0.100000001490116 + 0.100000001490116 * (double)level));
            int num4 = (int)((double)actor.Status.def * (0.100000001490116 + 0.100000001490116 * (double)level));
            if (skill.Variable.ContainsKey("DefUpAvoDown_avo_range_down"))
                skill.Variable.Remove("DefUpAvoDown_avo_range_down");
            skill.Variable.Add("DefUpAvoDown_avo_range_down", num1);
            actor.Status.avoid_ranged_skill += (short)num1;
            if (skill.Variable.ContainsKey("DefUpAvoDown_avo_melee_down"))
                skill.Variable.Remove("DefUpAvoDown_avo_melee_down");
            skill.Variable.Add("DefUpAvoDown_avo_melee_down", num2);
            actor.Status.avoid_melee_skill += (short)num2;
            if (skill.Variable.ContainsKey("DefUpAvoDown_def_add_add"))
                skill.Variable.Remove("DefUpAvoDown_def_add_add");
            skill.Variable.Add("DefUpAvoDown_def_add_add", num3);
            actor.Status.def_add_skill += (short)num3;
            if (skill.Variable.ContainsKey("DefUpAvoDown_def_add"))
                skill.Variable.Remove("DefUpAvoDown_def_add");
            skill.Variable.Add("DefUpAvoDown_def_add", num4);
            actor.Status.def_skill += (short)num4;
            actor.Buff.防御力上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.avoid_ranged_skill -= (short)skill.Variable["DefUpAvoDown_avo_range_down"];
            actor.Status.avoid_melee_skill -= (short)skill.Variable["DefUpAvoDown_avo_melee_down"];
            actor.Status.def_add_skill -= (short)skill.Variable["DefUpAvoDown_def_add_add"];
            actor.Status.def_skill -= (short)skill.Variable["DefUpAvoDown_def_add"];
            actor.Buff.防御力上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
