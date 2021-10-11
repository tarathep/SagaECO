namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Dungeon;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="DungeonNorth" />.
    /// </summary>
    public class DungeonNorth : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonNorth"/> class.
        /// </summary>
        public DungeonNorth()
        {
            this.EventID = 12001501U;
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
            DungeonMap connectedMap = map.DungeonMap.Gates[GateType.North].ConnectedMap;
            this.Warp(pc, connectedMap.Map.ID, connectedMap.Gates[GateType.South].X, connectedMap.Gates[GateType.South].Y);
        }
    }
}
