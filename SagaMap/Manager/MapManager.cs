namespace SagaMap.Manager
{
    using SagaDB.Actor;
    using SagaDB.Map;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MapManager" />.
    /// </summary>
    public sealed class MapManager : Singleton<MapManager>
    {
        /// <summary>
        /// Defines the maps.
        /// </summary>
        private Dictionary<uint, SagaMap.Map> maps;

        /// <summary>
        /// Defines the mapInfo.
        /// </summary>
        private Dictionary<uint, MapInfo> mapInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapManager"/> class.
        /// </summary>
        public MapManager()
        {
            this.maps = new Dictionary<uint, SagaMap.Map>();
            this.mapInfo = new Dictionary<uint, MapInfo>();
        }

        /// <summary>
        /// The GetMapName.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetMapName(uint mapID)
        {
            if (this.mapInfo.ContainsKey(mapID))
                return this.mapInfo[mapID].name;
            return "MAP_NAME_NOT_FOUND";
        }

        /// <summary>
        /// Gets the Maps.
        /// </summary>
        public Dictionary<uint, SagaMap.Map> Maps
        {
            get
            {
                return this.maps;
            }
        }

        /// <summary>
        /// Sets the MapInfos.
        /// </summary>
        public Dictionary<uint, MapInfo> MapInfos
        {
            set
            {
                this.mapInfo = value;
            }
        }

        /// <summary>
        /// The GetMapId.
        /// </summary>
        /// <param name="mapName">The mapName<see cref="string"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetMapId(string mapName)
        {
            foreach (KeyValuePair<uint, MapInfo> keyValuePair in this.mapInfo)
            {
                if (keyValuePair.Value.name.ToLower() == mapName.ToLower())
                    return keyValuePair.Key;
            }
            return uint.MaxValue;
        }

        /// <summary>
        /// The LoadMaps.
        /// </summary>
        public void LoadMaps()
        {
            foreach (uint hostedMap in Singleton<Configuration>.Instance.HostedMaps)
            {
                if (this.mapInfo.ContainsKey(hostedMap) && !this.AddMap(new SagaMap.Map(this.mapInfo[hostedMap])))
                    Logger.ShowError("Cannot load map " + (object)hostedMap, (Logger)null);
            }
        }

        /// <summary>
        /// The CreateMapInstance.
        /// </summary>
        /// <param name="creator">The creator<see cref="ActorPC"/>.</param>
        /// <param name="template">The template<see cref="uint"/>.</param>
        /// <param name="exitMap">The exitMap<see cref="uint"/>.</param>
        /// <param name="exitX">The exitX<see cref="byte"/>.</param>
        /// <param name="exitY">The exitY<see cref="byte"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint CreateMapInstance(ActorPC creator, uint template, uint exitMap, byte exitX, byte exitY)
        {
            if (!this.maps.ContainsKey(template))
                return 0;
            SagaMap.Map map = new SagaMap.Map(this.maps[template].Info);
            for (int index = (int)(template % 1000U) + 1; index < 999; ++index)
            {
                if (!this.maps.ContainsKey((uint)((ulong)(template / 1000U * 1000U + template % 1000U) + (ulong)index)))
                {
                    map.ID = (uint)((ulong)(template / 1000U * 1000U + template % 1000U) + (ulong)index);
                    break;
                }
            }
            map.IsMapInstance = true;
            map.ClientExitMap = exitMap;
            map.ClientExitX = exitX;
            map.ClientExitY = exitY;
            map.Creator = creator;
            Singleton<Configuration>.Instance.HostedMaps.Add(map.ID);
            this.maps.Add(map.ID, map);
            return map.ID;
        }

        /// <summary>
        /// The DeleteMapInstance.
        /// </summary>
        /// <param name="id">The id<see cref="uint"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool DeleteMapInstance(uint id)
        {
            if (!this.maps.ContainsKey(id))
                return false;
            this.maps[id].OnDestrory();
            this.maps.Remove(id);
            Singleton<Configuration>.Instance.HostedMaps.Remove(id);
            return true;
        }

        /// <summary>
        /// The AddMap.
        /// </summary>
        /// <param name="addMap">The addMap<see cref="SagaMap.Map"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AddMap(SagaMap.Map addMap)
        {
            foreach (SagaMap.Map map in this.maps.Values)
            {
                if ((int)addMap.ID == (int)map.ID)
                    return false;
            }
            this.maps.Add(addMap.ID, addMap);
            return true;
        }

        /// <summary>
        /// The GetMap.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        /// <returns>The <see cref="SagaMap.Map"/>.</returns>
        public SagaMap.Map GetMap(uint mapID)
        {
            if (this.maps.ContainsKey(mapID))
                return this.maps[mapID];
            Logger.ShowDebug("Requesting unknown mapID:" + mapID.ToString(), Logger.CurrentLogger);
            return (SagaMap.Map)null;
        }
    }
}
