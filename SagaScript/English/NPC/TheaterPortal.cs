using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

namespace SagaScript
{
    public abstract class TheaterPortal : Event
    {
        uint mapID;
        byte x, y;

        public TheaterPortal()
        {
        
        }

        protected void Init(uint eventID, uint mapID, byte x, byte y)
        {
            this.EventID = eventID;
            this.mapID = mapID;
            this.x = x;
            this.y = y;
        }

        public override void OnEvent(ActorPC pc)
        {
            SagaDB.Theater.Movie nextMovie = GetNextMovie(mapID);
            SagaDB.Theater.Movie currentMovie = GetCurrentMovie(mapID);
            if (nextMovie != null)
            {
                if (currentMovie != null)
                {
                    Say(pc, 131, string.Format("Now showing {0}, $R;" +
                         "Unable to enter! $R;" +
                         "The next movie will start playing at {1:00}:{2:00} $R;{3}",
                         currentMovie.Name, nextMovie.StartTime.Hour, nextMovie.StartTime.Minute, nextMovie.Name));
                }
                else
                {
                    DateTime now = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    TimeSpan span = nextMovie.StartTime - now;
                    if (span.TotalMinutes > 10 || span.TotalMinutes < 0)
                    {
                        DateTime enter = nextMovie.StartTime - new TimeSpan(0, 10, 0);
                        Say(pc, 131, string.Format("The next movie will start playing at {0:00}:{1:00}$R;" +
                              "{2}$R;You can enter at the earliest {3:00}:{4:00}", nextMovie.StartTime.Hour, nextMovie.StartTime.Minute, nextMovie.Name,
                              enter.Hour, enter.Minute));
                        return;
                    }
                    if (span.TotalMinutes < 1)
                    {
                        Say(pc, 131, string.Format("The movie {0} will be released soon, $R; you have already missed the entry time $R; please come again next time!", nextMovie.Name));
                        return;
                    }
                    if (CountItem(pc, nextMovie.Ticket) > 0)
                    {
                        if (pc.PossesionedActors.Count == 0)
                            Warp(pc, mapID, x, y);
                        else
                        {
                            Say(pc, 131, string.Format("The next movie will start playing at {0:00}:{1:00}$R;" +
                             "{2}$R;Huh? Who is relying on it? $R; Hurry down and check in!", nextMovie.StartTime.Hour, nextMovie.StartTime.Minute, nextMovie.Name));
                        }
                    }
                    else
                    {
                        Say(pc, 131, string.Format("The next movie will start playing at {0:00}:{1:00}$R;" +
                             "{2}$R; But you don’t seem to buy a ticket", nextMovie.StartTime.Hour, nextMovie.StartTime.Minute, nextMovie.Name));
                    }
                }
            }
            else
                Warp(pc, mapID + 4000, 11, 21);
        }
    }
}
