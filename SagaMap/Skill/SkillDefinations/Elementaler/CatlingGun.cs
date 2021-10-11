namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    using SagaDB.Actor;
    using SagaDB.Skill;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="CatlingGun" />.
    /// </summary>
    internal class CatlingGun : ISkill
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
            ActorSkill actor = new ActorSkill(Singleton<SkillFactory>.Instance.GetSkill(7700U, (byte)1), sActor);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            actor.MapID = sActor.MapID;
            actor.X = dActor.X;
            actor.Y = dActor.Y;
            actor.e = (ActorEventHandler)new NullEventHandler();
            actor.Name = "NOT_SHOW_DISAPPEAR";
            map.RegisterActor((SagaDB.Actor.Actor)actor);
            actor.invisble = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)actor);
            new ActivatorA(actor, dActor, sActor, args, level).Activate();
        }
    }
}
