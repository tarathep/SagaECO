namespace SagaMap.Manager
{
    using SagaDB.Actor;
    using SagaDB.Map;
    using SagaDB.Skill;
    using SagaLib;
    using SagaMap.Network.Client;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="WRPRankingManager" />.
    /// </summary>
    public class WRPRankingManager : Singleton<WRPRankingManager>
    {
        /// <summary>
        /// Defines the currentRanking.
        /// </summary>
        private List<ActorPC> currentRanking;

        /// <summary>
        /// The GetRanking.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <returns>The <see cref="uint"/>.</returns>
        public uint GetRanking(ActorPC pc)
        {
            if (this.currentRanking == null)
                this.currentRanking = MapServer.charDB.GetWRPRanking();
            IEnumerable<ActorPC> source = this.currentRanking.Where<ActorPC>((Func<ActorPC, bool>)(chr => (int)chr.CharID == (int)pc.CharID));
            if (source.Count<ActorPC>() > 0)
                return source.First<ActorPC>().WRPRanking;
            return 268;
        }

        /// <summary>
        /// The UpdateRanking.
        /// </summary>
        public void UpdateRanking()
        {
            this.currentRanking = MapServer.charDB.GetWRPRanking();
            using (List<MapClient>.Enumerator enumerator = MapClientManager.Instance.OnlinePlayer.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    MapClient i = enumerator.Current;
                    IEnumerable<ActorPC> source = this.currentRanking.Where<ActorPC>((Func<ActorPC, bool>)(chr => (int)chr.CharID == (int)i.Character.CharID));
                    i.Character.WRPRanking = source.Count<ActorPC>() <= 0 ? 268U : source.First<ActorPC>().WRPRanking;
                    if (i.Character.WRPRanking <= 10U)
                    {
                        if (i.map.Info.Flag.Test(MapFlags.Dominion))
                        {
                            if (!i.Character.Skills.ContainsKey(10500U))
                            {
                                SagaDB.Skill.Skill skill = Singleton<SkillFactory>.Instance.GetSkill(10500U, (byte)1);
                                skill.NoSave = true;
                                i.Character.Skills.Add(10500U, skill);
                            }
                        }
                        else if (i.Character.Skills.ContainsKey(10500U))
                            i.Character.Skills.Remove(10500U);
                    }
                    else if (i.Character.Skills.ContainsKey(10500U))
                        i.Character.Skills.Remove(10500U);
                    i.map.SendEventToAllActorsWhoCanSeeActor(SagaMap.Map.EVENT_TYPE.WRP_RANKING_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)i.Character, true);
                }
            }
        }
    }
}
