namespace SagaDB.Theater
{
    using SagaLib;
    using System;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="TheaterFactory" />.
    /// </summary>
    public class TheaterFactory : FactoryList<TheaterFactory, Movie>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TheaterFactory"/> class.
        /// </summary>
        public TheaterFactory()
        {
            this.loadingTab = "Loading theater schedules";
            this.loadedTab = " schedules loaded.";
            this.databaseName = "schedule";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetNextMovie.
        /// </summary>
        /// <param name="mapID">地图ID.</param>
        /// <returns>要上映的电影.</returns>
        public Movie GetNextMovie(uint mapID)
        {
            if (!this.items.ContainsKey(mapID))
                return (Movie)null;
            DateTime time = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            IOrderedEnumerable<Movie> source1 = this.items[mapID].Where<Movie>((Func<Movie, bool>)(movie => movie.StartTime > time)).OrderBy<Movie, DateTime>((Func<Movie, DateTime>)(movie => movie.StartTime));
            if (source1.Count<Movie>() != 0)
                return source1.First<Movie>();
            IOrderedEnumerable<Movie> source2 = this.items[mapID].OrderBy<Movie, DateTime>((Func<Movie, DateTime>)(movie => movie.StartTime));
            if (source2.Count<Movie>() != 0)
                return source2.First<Movie>();
            return (Movie)null;
        }

        /// <summary>
        /// The GetCurrentMovie.
        /// </summary>
        /// <param name="mapID">影院地图ID.</param>
        /// <returns>.</returns>
        public Movie GetCurrentMovie(uint mapID)
        {
            if (!this.items.ContainsKey(mapID))
                return (Movie)null;
            DateTime time = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            IOrderedEnumerable<Movie> source = this.items[mapID].Where<Movie>((Func<Movie, bool>)(movie =>
           {
               if (movie.StartTime < time)
                   return movie.StartTime + new TimeSpan(0, movie.Duration, 0) > time;
               return false;
           })).OrderBy<Movie, DateTime>((Func<Movie, DateTime>)(movie => movie.StartTime));
            if (source.Count<Movie>() != 0)
                return source.First<Movie>();
            return (Movie)null;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="Movie"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(Movie item)
        {
            return item.MapID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="Movie"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(Movie item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="Movie"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, Movie item)
        {
            switch (root.Name.ToLower())
            {
                case "movie":
                    switch (current.Name.ToLower())
                    {
                        case "name":
                            item.Name = current.InnerText;
                            return;
                        case "mapid":
                            item.MapID = uint.Parse(current.InnerText);
                            return;
                        case "ticket":
                            item.Ticket = uint.Parse(current.InnerText);
                            return;
                        case "url":
                            item.URL = current.InnerText;
                            return;
                        case "starttime":
                            string[] strArray = current.InnerText.Split(':');
                            DateTime dateTime = new DateTime(1970, 1, 1, int.Parse(strArray[0]), int.Parse(strArray[1]), 0);
                            item.StartTime = dateTime;
                            return;
                        case "duration":
                            item.Duration = int.Parse(current.InnerText);
                            return;
                        case null:
                            return;
                        default:
                            return;
                    }
            }
        }
    }
}
