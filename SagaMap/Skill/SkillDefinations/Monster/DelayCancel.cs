namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="DelayCancel" />.
    /// </summary>
    public class DelayCancel : ISkill
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
            args.dActor = 0U;
            int lifetime = 20000;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(DelayCancel), lifetime);
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
            actor.Status.aspd_skill_perc += (float)(1.20000004768372 + 0.300000011920929 * (double)skill.skill.Level);
            actor.Buff.DelayCancel = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Status.aspd_skill_perc -= (float)(1.20000004768372 + 0.300000011920929 * (double)skill.skill.Level);
            actor.Buff.DelayCancel = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }
    }
}
