namespace SagaMap.Skill.SkillDefinations.FGarden
{
    using SagaDB.Actor;
    using SagaDB.Map;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Network.Client;
    using SagaMap.Scripting;

    /// <summary>
    /// Defines the <see cref="FGRope" />.
    /// </summary>
    public class FGRope : ISkill
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
            if (sActor.type != ActorType.PC)
                return;
            ActorPC pc = (ActorPC)sActor;
            if (pc.FGarden != null)
            {
                if (pc.FGarden.RopeActor == null)
                {
                    if (Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.FGarden))
                        this.createNewRope(args, pc);
                    else
                        MapClient.FromActorPC(pc).SendSystemMessage(Singleton<LocalManager>.Instance.Strings.FG_CANNOT);
                }
                else if (!Singleton<ScriptManager>.Instance.Events.ContainsKey(pc.FGarden.RopeActor.EventID))
                {
                    if (Singleton<MapManager>.Instance.GetMap(pc.MapID).Info.Flag.Test(MapFlags.FGarden))
                    {
                        Singleton<MapManager>.Instance.GetMap(pc.FGarden.RopeActor.MapID).DeleteActor((SagaDB.Actor.Actor)pc.FGarden.RopeActor);
                        this.createNewRope(args, pc);
                    }
                    else
                        MapClient.FromActorPC(pc).SendSystemMessage(Singleton<LocalManager>.Instance.Strings.FG_CANNOT);
                }
                else
                    MapClient.FromActorPC(pc).SendSystemMessage(Singleton<LocalManager>.Instance.Strings.FG_ALREADY_CALLED);
            }
            else
                MapClient.FromActorPC(pc).SendSystemMessage(Singleton<LocalManager>.Instance.Strings.FG_NOT_FOUND);
        }

        /// <summary>
        /// The createNewRope.
        /// </summary>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        private void createNewRope(SkillArg args, ActorPC pc)
        {
            ActorEvent actorEvent = new ActorEvent(pc);
            SagaMap.Map map = Singleton<MapManager>.Instance.GetMap(pc.MapID);
            actorEvent.MapID = pc.MapID;
            actorEvent.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actorEvent.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            uint freeIdSince = Singleton<ScriptManager>.Instance.GetFreeIDSince(4026532096U, 1000);
            actorEvent.EventID = freeIdSince;
            pc.FGarden.RopeActor = actorEvent;
            if (Singleton<ScriptManager>.Instance.Events.ContainsKey(4026532096U))
            {
                EventActor eventActor = ((EventActor)Singleton<ScriptManager>.Instance.Events[4026532096U]).Clone();
                eventActor.Actor = actorEvent;
                eventActor.EventID = actorEvent.EventID;
                Singleton<ScriptManager>.Instance.Events.Add(eventActor.EventID, eventActor);
            }
            actorEvent.Type = ActorEventTypes.ROPE;
            actorEvent.Title = string.Format(Singleton<LocalManager>.Instance.Strings.FG_NAME, (object)pc.Name);
            actorEvent.e = (ActorEventHandler)new ItemEventHandler((SagaDB.Actor.Actor)actorEvent);
            map.RegisterActor((SagaDB.Actor.Actor)actorEvent);
            actorEvent.invisble = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorEvent);
        }
    }
}
