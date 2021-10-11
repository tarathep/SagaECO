namespace SagaMap.Scripting
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Dungeon;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="DungeonWest" />.
    /// </summary>
    public class DungeonWest : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DungeonWest"/> class.
        /// </summary>
        public DungeonWest()
        {
            this.EventID = 12001504U;
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
            DungeonMap connectedMap = map.DungeonMap.Gates[GateType.West].ConnectedMap;
            this.Warp(pc, connectedMap.Map.ID, connectedMap.Gates[GateType.East].X, connectedMap.Gates[GateType.East].Y);
        }
    }
}
