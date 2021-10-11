namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="MobElementLoad" />.
    /// </summary>
    public class MobElementLoad : ISkill
    {
        /// <summary>
        /// Defines the NextSkillID.
        /// </summary>
        private uint NextSkillID;

        /// <summary>
        /// Initializes a new instance of the <see cref="MobElementLoad"/> class.
        /// </summary>
        /// <param name="Next_SkillID">The Next_SkillID<see cref="uint"/>.</param>
        public MobElementLoad(uint Next_SkillID)
        {
            this.NextSkillID = Next_SkillID;
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
            byte[] numArray1 = new byte[3];
            byte[] numArray2 = new byte[3];
            Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, 0, out numArray1[0], out numArray2[0]);
            switch (Singleton<SkillHandler>.Instance.GetDirection(sActor))
            {
                case SkillHandler.ActorDirection.South:
                case SkillHandler.ActorDirection.SouthEast:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, -3, out numArray1[1], out numArray2[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, -6, out numArray1[2], out numArray2[2]);
                    break;
                case SkillHandler.ActorDirection.SouthWest:
                case SkillHandler.ActorDirection.East:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 3, 0, out numArray1[1], out numArray2[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 6, 0, out numArray1[2], out numArray2[2]);
                    break;
                case SkillHandler.ActorDirection.West:
                case SkillHandler.ActorDirection.NorthWest:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -3, 0, out numArray1[1], out numArray2[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, -6, 0, out numArray1[2], out numArray2[2]);
                    break;
                case SkillHandler.ActorDirection.North:
                case SkillHandler.ActorDirection.NorthEast:
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, 3, out numArray1[1], out numArray2[1]);
                    Singleton<SkillHandler>.Instance.GetRelatedPos(sActor, 0, 6, out numArray1[2], out numArray2[2]);
                    break;
            }
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(this.NextSkillID, (byte)1, 0, numArray1[0], numArray2[0]));
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(this.NextSkillID, (byte)1, 0, numArray1[1], numArray2[1]));
            args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(this.NextSkillID, (byte)1, 0, numArray1[2], numArray2[2]));
        }
    }
}
