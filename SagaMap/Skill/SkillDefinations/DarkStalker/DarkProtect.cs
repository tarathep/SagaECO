namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="DarkProtect" />.
    /// </summary>
    public class DarkProtect : ISkill
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
            bool ifActivate = false;
            if (Singleton<MapManager>.Instance.GetMap(sActor.MapID).Info.dark[(int)args.x, (int)args.y] > (byte)0)
                ifActivate = true;
            DefaultPassiveSkill defaultPassiveSkill = new DefaultPassiveSkill(args.skill, sActor, nameof(DarkProtect), ifActivate);
            defaultPassiveSkill.OnAdditionStart += new DefaultPassiveSkill.StartEventHandler(this.StartEventHandler);
            defaultPassiveSkill.OnAdditionEnd += new DefaultPassiveSkill.EndEventHandler(this.EndEventHandler);
            SkillHandler.ApplyAddition(sActor, (Addition)defaultPassiveSkill);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            int level = (int)skill.skill.Level;
            int num1 = 5 + 3 * level;
            if (skill.Variable.ContainsKey("DarkProtect_min_atk1"))
                skill.Variable.Remove("DarkProtect_min_atk1");
            skill.Variable.Add("DarkProtect_min_atk1", num1);
            actor.Status.min_atk1_skill += (short)num1;
            int num2 = 5 + 3 * level;
            if (skill.Variable.ContainsKey("DarkProtect_min_atk2"))
                skill.Variable.Remove("DarkProtect_min_atk2");
            skill.Variable.Add("DarkProtect_min_atk2", num2);
            actor.Status.min_atk2_skill += (short)num2;
            int num3 = 5 + 3 * level;
            if (skill.Variable.ContainsKey("DarkProtect_min_atk3"))
                skill.Variable.Remove("DarkProtect_min_atk3");
            skill.Variable.Add("DarkProtect_min_atk3", num3);
            actor.Status.min_atk3_skill += (short)num3;
            int num4 = 5 + 3 * level;
            if (skill.Variable.ContainsKey("DarkProtect_min_matk"))
                skill.Variable.Remove("DarkProtect_min_matk");
            skill.Variable.Add("DarkProtect_min_matk", num4);
            actor.Status.min_matk_skill += (short)num4;
            int num5 = 5 + 2 * level;
            if (skill.Variable.ContainsKey("DarkProtect_def_add"))
                skill.Variable.Remove("DarkProtect_def_add");
            skill.Variable.Add("DarkProtect_def_add", num5);
            actor.Status.def_add_skill += (short)num5;
            int num6 = 5 + 2 * level;
            if (skill.Variable.ContainsKey("DarkProtect_mdef_add"))
                skill.Variable.Remove("DarkProtect_mdef_add");
            skill.Variable.Add("DarkProtect_mdef_add", num6);
            actor.Status.mdef_add_skill += (short)num6;
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultPassiveSkill"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultPassiveSkill skill)
        {
            actor.Status.min_atk1_skill -= (short)skill.Variable["DarkProtect_min_atk1"];
            actor.Status.min_atk2_skill -= (short)skill.Variable["DarkProtect_min_atk2"];
            actor.Status.min_atk3_skill -= (short)skill.Variable["DarkProtect_min_atk3"];
            actor.Status.min_matk_skill -= (short)skill.Variable["DarkProtect_min_matk"];
            actor.Status.def_add_skill -= (short)skill.Variable["DarkProtect_def_add"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["DarkProtect_mdef_add"];
        }
    }
}
