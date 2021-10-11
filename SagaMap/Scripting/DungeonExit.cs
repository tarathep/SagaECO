namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="DungeonExit" />.
    /// </summary>
    public class DungeonExit : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonExit"/> class.
        /// </summary>
        public DungeonExit()
        {
            this.EventID = 12001505U;
        }

        /// <summary>
        /// The OnEvent.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public override void OnEvent(ActorPC pc)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(pc.MapID);
            if (!map.IsDungeon)
                return;
            this.Warp(pc, map.ClientExitMap, map.ClientExitX, map.ClientExitY);
        }
    }
}
