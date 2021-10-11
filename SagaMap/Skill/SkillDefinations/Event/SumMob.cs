namespace SagaMap.Skill.SkillDefinations.Event
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
        /// Initializes a new instance of the <see cref="SumMob"/> class.
        /// </summary>
        /// <param name="MobID">The MobID<see cref="uint"/>.</param>
        public SumMob(uint MobID)
        {
            this.MobID = MobID;
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
            ActorMob actorMob = Singleton<MapManager>.Instance.GetMap(sActor.MapID).SpawnMob(this.MobID, (short)((int)sActor.X + SagaLib.Global.Random.Next(1, 10)), (short)((int)sActor.Y + SagaLib.Global.Random.Next(1, 10)), (short)50, sActor);
            sActor.Slave.Add((SagaDB.Actor.Actor)actorMob);
        }
    }
}
