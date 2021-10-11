namespace SagaMap.Manager
{
    using SagaDB.Actor;
    using SagaLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="RecruitmentManager" />.
    /// </summary>
    public class RecruitmentManager : Singleton<RecruitmentManager>
    {
        /// <summary>
        /// Defines the items.
        /// </summary>
        private List<Recruitment> items = new List<Recruitment>();

        /// <summary>
        /// The CreateRecruiment.
        /// </summary>
        /// <param name="rec">The rec<see cref="Recruitment"/>.</param>
        public void CreateRecruiment(Recruitment rec)
        {
            IEnumerable<Recruitment> source = this.items.Where<Recruitment>((Func<Recruitment, bool>)(r => r.Creator == rec.Creator));
            if (source.Count<Recruitment>() != 0)
            {
                this.items.Remove(source.First<Recruitment>());
                this.items.Add(rec);
            }
            else
                this.items.Add(rec);
        }

        /// <summary>
        /// The DeleteRecruitment.
        /// </summary>
        /// <param name="creator">The creator<see cref="ActorPC"/>.</param>
        public void DeleteRecruitment(ActorPC creator)
        {
            IEnumerable<Recruitment> source = this.items.Where<Recruitment>((Func<Recruitment, bool>)(r => r.Creator == creator));
            if (source.Count<Recruitment>() == 0)
                return;
            this.items.Remove(source.First<Recruitment>());
        }

        /// <summary>
        /// The GetRecruitments.
        /// </summary>
        /// <param name="type">The type<see cref="RecruitmentType"/>.</param>
        /// <param name="page">The page<see cref="int"/>.</param>
        /// <param name="maxPage">The maxPage<see cref="int"/>.</param>
        /// <returns>The <see cref="List{Recruitment}"/>.</returns>
        public List<Recruitment> GetRecruitments(RecruitmentType type, int page, out int maxPage)
        {
            List<Recruitment> list = this.items.Where<Recruitment>((Func<Recruitment, bool>)(r => r.Type == type)).ToList<Recruitment>();
            maxPage = list.Count % 15 != 0 ? list.Count / 15 + 1 : list.Count / 15;
            list = list.Where<Recruitment>((Func<Recruitment, bool>)(r => list.IndexOf(r) >= page * 15 && list.IndexOf(r) < (page + 1) * 15)).ToList<Recruitment>();
            return list;
        }
    }
}
