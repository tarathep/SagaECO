namespace SagaDB.KnightWar
{
    using SagaLib;
    using System;
    using System.Linq;
    using System.Xml;

    /// <summary>
    /// Defines the <see cref="KnightWarFactory" />.
    /// </summary>
    public class KnightWarFactory : Factory<KnightWarFactory, SagaDB.KnightWar.KnightWar>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KnightWarFactory"/> class.
        /// </summary>
        public KnightWarFactory()
        {
            this.loadingTab = "Loading KnightWar database";
            this.loadedTab = " KightWars loaded.";
            this.databaseName = "KnightWaw";
            this.FactoryType = FactoryType.XML;
        }

        /// <summary>
        /// The GetNextKnightWar.
        /// </summary>
        /// <returns>要上映的电影.</returns>
        public SagaDB.KnightWar.KnightWar GetNextKnightWar()
        {
            DateTime time = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            IOrderedEnumerable<SagaDB.KnightWar.KnightWar> source1 = this.items.Values.Where<SagaDB.KnightWar.KnightWar>((Func<SagaDB.KnightWar.KnightWar, bool>)(movie => movie.StartTime > time)).OrderBy<SagaDB.KnightWar.KnightWar, DateTime>((Func<SagaDB.KnightWar.KnightWar, DateTime>)(movie => movie.StartTime));
            if (source1.Count<SagaDB.KnightWar.KnightWar>() != 0)
                return source1.First<SagaDB.KnightWar.KnightWar>();
            IOrderedEnumerable<SagaDB.KnightWar.KnightWar> source2 = this.items.Values.OrderBy<SagaDB.KnightWar.KnightWar, DateTime>((Func<SagaDB.KnightWar.KnightWar, DateTime>)(movie => movie.StartTime));
            if (source2.Count<SagaDB.KnightWar.KnightWar>() != 0)
                return source2.First<SagaDB.KnightWar.KnightWar>();
            return (SagaDB.KnightWar.KnightWar)null;
        }

        /// <summary>
        /// The GetCurrentMovie.
        /// </summary>
        /// <returns>.</returns>
        public SagaDB.KnightWar.KnightWar GetCurrentMovie()
        {
            DateTime time = new DateTime(1970, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            IOrderedEnumerable<SagaDB.KnightWar.KnightWar> source = this.items.Values.Where<SagaDB.KnightWar.KnightWar>((Func<SagaDB.KnightWar.KnightWar, bool>)(movie =>
           {
               if (movie.StartTime < time)
                   return movie.StartTime + new TimeSpan(0, movie.Duration, 0) > time;
               return false;
           })).OrderBy<SagaDB.KnightWar.KnightWar, DateTime>((Func<SagaDB.KnightWar.KnightWar, DateTime>)(movie => movie.StartTime));
            if (source.Count<SagaDB.KnightWar.KnightWar>() != 0)
                return source.First<SagaDB.KnightWar.KnightWar>();
            return (SagaDB.KnightWar.KnightWar)null;
        }

        /// <summary>
        /// The GetKey.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.KnightWar.KnightWar"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        protected override uint GetKey(SagaDB.KnightWar.KnightWar item)
        {
            return item.ID;
        }

        /// <summary>
        /// The ParseCSV.
        /// </summary>
        /// <param name="item">The item<see cref="SagaDB.KnightWar.KnightWar"/>.</param>
        /// <param name="paras">The paras<see cref="string[]"/>.</param>
        protected override void ParseCSV(SagaDB.KnightWar.KnightWar item, string[] paras)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ParseXML.
        /// </summary>
        /// <param name="root">The root<see cref="XmlElement"/>.</param>
        /// <param name="current">The current<see cref="XmlElement"/>.</param>
        /// <param name="item">The item<see cref="SagaDB.KnightWar.KnightWar"/>.</param>
        protected override void ParseXML(XmlElement root, XmlElement current, SagaDB.KnightWar.KnightWar item)
        {
            switch (root.Name.ToLower())
            {
                case "movie":
                    switch (current.Name.ToLower())
                    {
                        case "id":
                            item.ID = uint.Parse(current.InnerText);
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
