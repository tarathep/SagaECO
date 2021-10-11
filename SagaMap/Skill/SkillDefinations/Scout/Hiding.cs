namespace SagaMap.Skill.SkillDefinations.Scout
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="Hiding" />.
    /// </summary>
    public class Hiding : ISkill
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
            return pc.Status.Additions.ContainsKey(nameof(Hiding)) ? -7 : 0;
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
            DefaultBuff defaultBuff = new DefaultBuff(args.skill, dActor, nameof(Hiding), int.MaxValue, 1000);
            defaultBuff.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEventHandler);
            defaultBuff.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEventHandler);
            defaultBuff.OnUpdate += new DefaultBuff.UpdateEventHandler(this.UpdateEventHandler);
            SkillHandler.ApplyAddition(dActor, (Addition)defaultBuff);
        }

        /// <summary>
        /// The UpdateEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void UpdateEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
            if (skill.Variable.ContainsKey("count"))
            {
                Dictionary<string, int> variable;
                (variable = skill.Variable)["count"] = variable["count"] + 1;
            }
            else
                skill.Variable.Add("count", 0);
            if (skill.Variable["count"] < 5)
                return;
            ActorPC actorPc = (ActorPC)actor;
            if (actor.SP > 0U && actorPc.Motion != MotionType.SIT)
            {
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                --actor.SP;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, actor, true);
            }
            else
            {
                actor.Status.Additions[nameof(Hiding)].AdditionEnd();
                actor.Status.Additions.Remove(nameof(Hiding));
            }
        }

        /// <summary>
        /// The StartEventHandler.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
        private void StartEventHandler(SagaDB.Actor.Actor actor, DefaultBuff skill)
        {
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
    }
}
