namespace SagaMap.Skill.SkillDefinations.BountyHunter
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;

    /// <summary>
    /// Defines the <see cref="PartsSlash" />.
    /// </summary>
    public class PartsSlash : ISkill
    {
        /// <summary>
        /// Defines the skills.
        /// </summary>
        private static uint[] skills = new uint[4]
    {
      2274U,
      2272U,
      2271U,
      2273U
    };

        /// <summary>
        /// The TryCast.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="ActorPC"/>.</param>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="SkillArg"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int TryCast(ActorPC sActor, SagaDB.Actor.Actor dActor, SkillArg args)
        {
            return Singleton<SkillHandler>.Instance.CheckValidAttackTarget((SagaDB.Actor.Actor)sActor, dActor) ? 0 : -14;
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
            int num = SagaLib.Global.Random.Next(0, 3);
            Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            int[] numArray = new int[3] { 0, 1000, 700 };
            ActorPC actorPc = (ActorPC)sActor;
            for (int index = 0; index < num; ++index)
            {
                uint skill = PartsSlash.skills[SagaLib.Global.Random.Next(0, PartsSlash.skills.Length - 1)];
                if (actorPc.Skills2.ContainsKey(skill))
                {
                    AutoCastInfo autoCastInfo = Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skill, actorPc.Skills2[skill].Level, numArray[index]);
                    args.autoCast.Add(autoCastInfo);
                }
                else if (actorPc.SkillsReserve.ContainsKey(skill))
                {
                    AutoCastInfo autoCastInfo = Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skill, actorPc.SkillsReserve[skill].Level, numArray[index]);
                    args.autoCast.Add(autoCastInfo);
                }
            }
        }
    }
}
