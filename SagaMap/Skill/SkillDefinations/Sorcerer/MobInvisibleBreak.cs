namespace SagaMap.Skill.SkillDefinations.Sorcerer
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MobInvisibleBreak" />.
    /// </summary>
    public class MobInvisibleBreak : ISkill
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
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            ActorSkill actorSkill = new ActorSkill(args.skill, sActor);
            actorSkill.MapID = sActor.MapID;
            actorSkill.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actorSkill.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            actorSkill.e = (ActorEventHandler)new NullEventHandler();
            List<SagaDB.Actor.Actor> actorsArea1 = map.GetActorsArea((SagaDB.Actor.Actor)actorSkill, (short)300, false);
            List<SagaDB.Actor.Actor> actorsArea2 = map.GetActorsArea(sActor, (short)300, false);
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor act in actorsArea1)
            {
                if (act.Buff.Transparent)
                    this.SetVisible(act, map);
            }
            foreach (SagaDB.Actor.Actor act in actorsArea2)
            {
                if (act.Buff.Transparent)
                    this.SetVisible(act, map);
            }
        }

        /// <summary>
        /// The SetVisible.
        /// </summary>
        /// <param name="act">The act<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="map">The map<see cref="Map"/>.</param>
        public void SetVisible(SagaDB.Actor.Actor act, Map map)
        {
            act.Buff.Transparent = false;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, (MapEventArgs)null, act, true);
        }
    }
}
