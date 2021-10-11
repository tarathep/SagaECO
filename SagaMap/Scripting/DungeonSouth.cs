namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Dungeon;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="DungeonSouth" />.
    /// </summary>
    public class DungeonSouth : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonSouth"/> class.
        /// </summary>
        public DungeonSouth()
        {
            this.EventID = 12001503U;
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
            DungeonMap connectedMap = map.DungeonMap.Gates[GateType.South].ConnectedMap;
            this.Warp(pc, connectedMap.Map.ID, connectedMap.Gates[GateType.North].X, connectedMap.Gates[GateType.North].Y);
        }
    }
}
