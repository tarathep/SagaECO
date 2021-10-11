namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Dungeon;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="DungeonEast" />.
    /// </summary>
    public class DungeonEast : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonEast"/> class.
        /// </summary>
        public DungeonEast()
        {
            this.EventID = 12001502U;
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
            DungeonMap connectedMap = map.DungeonMap.Gates[GateType.East].ConnectedMap;
            this.Warp(pc, connectedMap.Map.ID, connectedMap.Gates[GateType.West].X, connectedMap.Gates[GateType.West].Y);
        }
    }
}
