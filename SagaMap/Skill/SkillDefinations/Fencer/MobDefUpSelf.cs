namespace SagaMap.Skill.SkillDefinations.Fencer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="MobDefUpSelf" />.
    /// </summary>
    public class MobDefUpSelf : ISkill
    {
        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobDefUpSelf"/> class.
        /// </summary>
        public MobDefUpSelf()
        {
            this.MobUse = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MobDefUpSelf"/> class.
        /// </summary>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public MobDefUpSelf(bool MobUse)
        {
            this.MobUse = MobUse;
        }

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
            int lifetime = 100000 - (int)level * 10000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(MobDefUpSelf), lifetime);
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
            int num1 = (int)skill.skill.Level;
            if (this.MobUse)
                num1 = 5;
            int num2 = 6 + num1;
            if (skill.Variable.ContainsKey("MobDefUpSelf_def"))
                skill.Variable.Remove("MobDefUpSelf_def");
            skill.Variable.Add("MobDefUpSelf_def", num2);
            actor.Status.def_skill += (short)num2;
            int num3 = 8 + 2 * num1;
            if (skill.Variable.ContainsKey("MobDefUpSelf_def_add"))
                skill.Variable.Remove("MobDefUpSelf_def_add");
            skill.Variable.Add("MobDefUpSelf_def_add", num3);
            actor.Status.def_add_skill += (short)num3;
            int num4 = 6 + num1;
            if (skill.Variable.ContainsKey("MobDefUpSelf_mdef"))
                skill.Variable.Remove("MobDefUpSelf_mdef");
            skill.Variable.Add("MobDefUpSelf_mdef", num4);
            actor.Status.mdef_skill += (short)num4;
            int num5 = 5 + 2 * num1;
            if (skill.Variable.ContainsKey("MobDefUpSelf_mdef_add"))
                skill.Variable.Remove("MobDefUpSelf_mdef_add");
            skill.Variable.Add("MobDefUpSelf_mdef_add", num5);
            actor.Status.mdef_add_skill += (short)num5;
            actor.Buff.防御力上昇 = true;
            actor.Buff.魔法防御力上昇 = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.def_skill -= (short)skill.Variable["MobDefUpSelf_def"];
            actor.Status.def_add_skill -= (short)skill.Variable["MobDefUpSelf_def_add"];
            actor.Status.mdef_skill -= (short)skill.Variable["MobDefUpSelf_mdef"];
            actor.Status.mdef_add_skill -= (short)skill.Variable["MobDefUpSelf_mdef_add"];
            actor.Buff.防御力上昇 = false;
            actor.Buff.魔法防御力上昇 = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
