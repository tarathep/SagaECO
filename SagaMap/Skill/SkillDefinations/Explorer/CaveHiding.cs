namespace SagaMap.Skill.SkillDefinations.Explorer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="CaveHiding" />.
    /// </summary>
    public class CaveHiding : ISkill
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
            args.dActor = 0U;
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, sActor, nameof(CaveHiding), int.MaxValue, 1000);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            defaultBuff.OnUpdate += new DefaultBuff.UpdateEventHandler(this.UpdateEventHandler);
            SkillHandler.ApplyAddition(sActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("CaveHiding_X"))
                skill.Variable.Remove("CaveHiding_X");
            skill.Variable.Add("CaveHiding_X", (int)actor.X);
            if (skill.Variable.ContainsKey("CaveHiding_Y"))
                skill.Variable.Remove("CaveHiding_Y");
            skill.Variable.Add("CaveHiding_Y", (int)actor.Y);
            actor.Buff.Transparent = true;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The EndEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void EndEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            actor.Buff.Transparent = false;
            Singleton<MapManager>.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, actor, true);
        }

        /// <summary>
        /// The UpdateEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void UpdateEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            if (actor.SP > 0U && (int)actor.X == (int)(short)skill.Variable["CaveHiding_X"] && (int)actor.Y == (int)(short)skill.Variable["CaveHiding_Y"])
            {
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                --actor.SP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor, true);
            }
            else
            {
                actor.Status.Additions[nameof(CaveHiding)].AdditionEnd();
                actor.Status.Additions.Remove(nameof(CaveHiding));
            }
        }
    }
}
