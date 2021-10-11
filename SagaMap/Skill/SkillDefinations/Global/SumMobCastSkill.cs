namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SumMobCastSkill" />.
    /// </summary>
    public class SumMobCastSkill : ISkill
    {
        /// <summary>
        /// Defines the NextSkill.
        /// </summary>
        private Dictionary<uint, int> NextSkill = new Dictionary<uint, int>();

        /// <summary>
        /// Defines the MobID.
        /// </summary>
        private uint MobID;

        /// <summary>
        /// Initializes a new instance of the <see cref="SumMobCastSkill"/> class.
        /// </summary>
        /// <param name="MobID">The MobID<see cref="uint"/>.</param>
        /// <param name="SkillID">The SkillID<see cref="uint"/>.</param>
        public SumMobCastSkill(uint MobID, uint SkillID)
        {
            this.MobID = MobID;
            this.NextSkill.Add(SkillID, 100);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SumMobCastSkill"/> class.
        /// </summary>
        /// <param name="MobID">The MobID<see cref="uint"/>.</param>
        /// <param name="SkillID1">The SkillID1<see cref="uint"/>.</param>
        /// <param name="rate1">The rate1<see cref="int"/>.</param>
        /// <param name="SkillID2">The SkillID2<see cref="uint"/>.</param>
        /// <param name="rate2">The rate2<see cref="int"/>.</param>
        public SumMobCastSkill(uint MobID, uint SkillID1, int rate1, uint SkillID2, int rate2)
        {
            this.MobID = MobID;
            this.NextSkill.Add(SkillID1, rate1);
            this.NextSkill.Add(SkillID2, rate2);
        }

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return 0;
        }

        /// <summary>
        /// The Proc.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <param name="level">The level<see cref="byte"/>.</param>
        public void Proc(SagaDB.Actor.Actor sActor, SagaDB.Actor.Actor dActor, SkillArg args, byte level)
        {
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            ActorMob actorMob = map.SpawnMob(this.MobID, SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), (short)50, sActor);
            MobEventHandler e = (MobEventHandler)actorMob.e;
            uint skillID = 0;
            int max = 0;
            foreach (KeyValuePair<uint, int> keyValuePair in this.NextSkill)
                max += keyValuePair.Value;
            int num1 = SagaLib.Global.Random.Next(0, max);
            int num2 = 0;
            foreach (KeyValuePair<uint, int> keyValuePair in this.NextSkill)
            {
                num2 += keyValuePair.Value;
                if (num2 > num1)
                    skillID = keyValuePair.Key;
            }
            e.AI.CastSkill(skillID, (byte)1, sActor);
            sActor.Slave.Add((SagaDB.Actor.Actor)actorMob);
        }
    }
}
