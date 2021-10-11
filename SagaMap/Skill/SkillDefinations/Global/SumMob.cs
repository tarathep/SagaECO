namespace SagaMap.Skill.SkillDefinations.Global
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="SumMob" />.
    /// </summary>
    public class SumMob : ISkill
    {
        /// <summary>
        /// Defines the MobID.
        /// </summary>
        private uint MobID;

        /// <summary>
        /// Defines the Count.
        /// </summary>
        private int Count;

        /// <summary>
        /// Initializes a new instance of the <see cref="SumMob"/> class.
        /// </summary>
        /// <param name="MobID">The MobID<see cref="uint"/>.</param>
        public SumMob(uint MobID)
        {
            this.MobID = MobID;
            this.Count = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SumMob"/> class.
        /// </summary>
        /// <param name="MobID">The MobID<see cref="uint"/>.</param>
        /// <param name="count">The count<see cref="int"/>.</param>
        public SumMob(uint MobID, int count)
        {
            this.MobID = MobID;
            this.Count = count;
        }

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC pc, SagaDB.Actor.Actor dActor, SkillArg args)
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
            if (sActor.Slave.Count == 0)
            {
                for (int index = 0; index < this.Count; ++index)
                {
                    short[] randomPosAroundActor = map.GetRandomPosAroundActor(sActor);
                    ActorMob actorMob = map.SpawnMob(this.MobID, randomPosAroundActor[0], randomPosAroundActor[1], (short)2500, sActor);
                    sActor.Slave.Add((SagaDB.Actor.Actor)actorMob);
                }
            }
            else
            {
                int num = 0;
                for (int index = 0; index < sActor.Slave.Count; ++index)
                {
                    if (sActor.Slave[index].Buff.Dead)
                        ++num;
                }
                if (num == 0)
                {
                    for (int index = 0; index < sActor.Slave.Count; ++index)
                    {
                        if (sActor.Slave[index].Buff.Dead)
                        {
                            short[] randomPosAroundActor = map.GetRandomPosAroundActor(sActor);
                            sActor.Slave[index] = (SagaDB.Actor.Actor)map.SpawnMob(this.MobID, randomPosAroundActor[0], randomPosAroundActor[1], (short)2500, (SagaDB.Actor.Actor)null);
                        }
                    }
                }
            }
        }
    }
}
