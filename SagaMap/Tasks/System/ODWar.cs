namespace SagaMap.Tasks.System
{
    using global::System;
    using global::System.Collections.Generic;
    using SagaDB.Actor;
    using SagaDB.ODWar;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="ODWar" />.
    /// </summary>
    public class ODWar : MultiRunTask
    {
        /// <summary>
        /// Defines the instance.
        /// </summary>
        private static SagaMap.Tasks.System.ODWar instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="ODWar"/> class.
        /// </summary>
        public ODWar()
        {
            this.period = 60000;
            this.dueTime = 0;
        }

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static SagaMap.Tasks.System.ODWar Instance
        {
            get
            {
                if (SagaMap.Tasks.System.ODWar.instance == null)
                    SagaMap.Tasks.System.ODWar.instance = new SagaMap.Tasks.System.ODWar();
                return SagaMap.Tasks.System.ODWar.instance;
            }
        }

        /// <summary>
        /// The StartODWar.
        /// </summary>
        /// <param name="mapID">The mapID<see cref="uint"/>.</param>
        public void StartODWar(uint mapID)
        {
            if (!Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items.ContainsKey(mapID))
                return;
            MapClientManager.Instance.Announce("都市防御战开始了！");
            Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID].Started = true;
            if (Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID].StartTime.ContainsKey((int)DateTime.Today.DayOfWeek))
            {
                Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID].StartTime[(int)DateTime.Today.DayOfWeek] = DateTime.Now.Hour;
            }
            else
            {
                Dictionary<int, int> startTime = Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID].StartTime;
                DateTime dateTime = DateTime.Today;
                int dayOfWeek = (int)dateTime.DayOfWeek;
                dateTime = DateTime.Now;
                int hour = dateTime.Hour;
                startTime.Add(dayOfWeek, hour);
            }
            Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items[mapID].Score.Clear();
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            try
            {
                DateTime now = DateTime.Now;
                foreach (SagaDB.ODWar.ODWar odWar in Factory<ODWarFactory, SagaDB.ODWar.ODWar>.Instance.Items.Values)
                {
                    Map map = Singleton<MapManager>.Instance.GetMap(odWar.MapID);
                    if (Singleton<ODWarManager>.Instance.IsDefence(odWar.MapID))
                    {
                        if (odWar.StartTime.ContainsKey((int)now.DayOfWeek))
                        {
                            int num = odWar.StartTime[(int)now.DayOfWeek];
                            if (map != null && Singleton<ODWarManager>.Instance.IsDefence(odWar.MapID))
                            {
                                if (now.Hour + 1 == num && now.Minute >= 45 && now.Minute % 5 == 0)
                                {
                                    MapClientManager.Instance.Announce(string.Format(Singleton<LocalManager>.Instance.Strings.ODWAR_PREPARE, (object)map.Name, (object)(now.Minute - 30)));
                                    MapClientManager.Instance.Announce(Singleton<LocalManager>.Instance.Strings.ODWAR_PREPARE2);
                                }
                                if (now.Hour == num && now.Minute < 15)
                                {
                                    MapClientManager.Instance.Announce(string.Format(Singleton<LocalManager>.Instance.Strings.ODWAR_PREPARE, (object)map.Name, (object)(15 - now.Minute)));
                                    MapClientManager.Instance.Announce(Singleton<LocalManager>.Instance.Strings.ODWAR_PREPARE2);
                                }
                                if (now.Hour == num && now.Minute == 15)
                                {
                                    MapClientManager.Instance.Announce(Singleton<LocalManager>.Instance.Strings.ODWAR_START);
                                    odWar.Started = true;
                                    odWar.Score.Clear();
                                }
                                if (now.Hour == num && now.Minute >= 1 && odWar.Started)
                                {
                                    if (now.Minute % 10 == 0)
                                        Singleton<ODWarManager>.Instance.SpawnMob(odWar.MapID, true);
                                    else
                                        Singleton<ODWarManager>.Instance.SpawnMob(odWar.MapID, false);
                                    if (now.Minute == 58 || now.Minute == 59)
                                        Singleton<ODWarManager>.Instance.SpawnMob(odWar.MapID, true);
                                }
                                if (now.Hour == num && now.Minute == 58 && odWar.Started)
                                    Singleton<ODWarManager>.Instance.SpawnBoss(odWar.MapID);
                                if (now.Hour != num && odWar.Started)
                                {
                                    odWar.Started = false;
                                    Singleton<ODWarManager>.Instance.EndODWar(odWar.MapID, true);
                                }
                            }
                        }
                    }
                    else
                    {
                        int num = (odWar.WaveStrong.DEMNormal + odWar.WaveStrong.DEMChamp) * odWar.Symbols.Count;
                        if (map.CountActorType(ActorType.MOB) <= num)
                        {
                            if (now.Minute % 10 == 0)
                                Singleton<ODWarManager>.Instance.SpawnMob(odWar.MapID, true);
                            else
                                Singleton<ODWarManager>.Instance.SpawnMob(odWar.MapID, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
    }
}
