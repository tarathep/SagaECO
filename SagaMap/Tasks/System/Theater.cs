namespace SagaMap.Tasks.System
{
    using global::System;
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaDB.Theater;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Network.Client;
    using SagaMap.Packets.Server;

    /// <summary>
    /// Defines the <see cref="Theater" />.
    /// </summary>
    public class Theater : MultiRunTask
    {
        /// <summary>
        /// Defines the instance.
        /// </summary>
        private static SagaMap.Tasks.System.Theater instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="Theater"/> class.
        /// </summary>
        public Theater()
        {
            this.period = 60000;
            this.dueTime = 0;
        }

        /// <summary>
        /// Gets the Instance.
        /// </summary>
        public static SagaMap.Tasks.System.Theater Instance
        {
            get
            {
                if (SagaMap.Tasks.System.Theater.instance == null)
                    SagaMap.Tasks.System.Theater.instance = new SagaMap.Tasks.System.Theater();
                return SagaMap.Tasks.System.Theater.instance;
            }
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            try
            {
                foreach (uint key in FactoryList<TheaterFactory, Movie>.Instance.Items.Keys)
                {
                    Movie nextMovie = FactoryList<TheaterFactory, Movie>.Instance.GetNextMovie(key);
                    Map map = Singleton<MapManager>.Instance.GetMap(key);
                    SagaDB.Actor.Actor[] array = new SagaDB.Actor.Actor[map.Actors.Count];
                    map.Actors.Values.CopyTo(array, 0);
                    DateTime dateTime = new DateTime();

                    int year = 1970;
                    int month = 1;
                    int day = 1;
                    DateTime now = DateTime.Now;
                    int hour = now.Hour;
                    now = DateTime.Now;
                    int minute = now.Minute;
                    now = DateTime.Now;
                    int second = now.Second;
                    DateTime local = new DateTime(year, month, day, hour, minute, second);
                    if (nextMovie != null)
                    {
                        TimeSpan timeSpan = nextMovie.StartTime - dateTime;
                        switch ((int)timeSpan.TotalMinutes)
                        {
                            case 0:
                                Logger.ShowInfo(string.Format("{0} is now playing <{1}>", (object)map.Name, (object)nextMovie.Name));
                                foreach (SagaDB.Actor.Actor actor in array)
                                {
                                    if (actor.type == ActorType.PC)
                                    {
                                        ActorPC pc = (ActorPC)actor;
                                        if (pc.Online)
                                        {
                                            MapClient.FromActorPC(pc).DeleteItemID(nextMovie.Ticket, (ushort)1, true);
                                            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_THEATER_INFO()
                                            {
                                                MessageType = SSMG_THEATER_INFO.Type.PLAY,
                                                Message = ""
                                            });
                                        }
                                    }
                                }
                                break;
                            case 1:
                            case 2:
                            case 3:
                            case 5:
                            case 7:
                            case 10:
                                Logger.ShowInfo(string.Format("{0} is going to play <{1}> in {2:0} minutes", (object)map.Name, (object)nextMovie.Name, (object)timeSpan.TotalMinutes));
                                foreach (SagaDB.Actor.Actor actor in array)
                                {
                                    if (actor.type == ActorType.PC)
                                    {
                                        ActorPC pc = (ActorPC)actor;
                                        if (pc.Online)
                                        {
                                            if ((int)timeSpan.TotalMinutes == 10)
                                            {
                                                if (pc.Inventory.GetItem(nextMovie.Ticket, Inventory.SearchType.ITEM_ID).Stack == (ushort)0)
                                                {
                                                    uint mapid = key - 10000U;
                                                    map.SendActorToMap((SagaDB.Actor.Actor)pc, mapid, (short)10, (short)1);
                                                }
                                                else
                                                {
                                                    MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_THEATER_INFO()
                                                    {
                                                        MessageType = SSMG_THEATER_INFO.Type.MESSAGE,
                                                        Message = Singleton<LocalManager>.Instance.Strings.THEATER_WELCOME
                                                    });
                                                    MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_THEATER_INFO()
                                                    {
                                                        MessageType = SSMG_THEATER_INFO.Type.MOVIE_ADDRESS,
                                                        Message = nextMovie.URL
                                                    });
                                                }
                                            }
                                            MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_THEATER_INFO()
                                            {
                                                MessageType = SSMG_THEATER_INFO.Type.MESSAGE,
                                                Message = string.Format(Singleton<LocalManager>.Instance.Strings.THEATER_COUNTDOWN, (object)nextMovie.Name, (object)(int)timeSpan.TotalMinutes)
                                            });
                                            if ((int)timeSpan.TotalMinutes == 1)
                                                MapClient.FromActorPC(pc).netIO.SendPacket((Packet)new SSMG_THEATER_INFO()
                                                {
                                                    MessageType = SSMG_THEATER_INFO.Type.STOP_BGM
                                                });
                                        }
                                    }
                                }
                                break;
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
