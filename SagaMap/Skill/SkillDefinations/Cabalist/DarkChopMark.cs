namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System;
    using System.Linq;

    /// <summary>
    /// Defines the <see cref="DarkChopMark" />.
    /// </summary>
    public class DarkChopMark : ISkill
    {
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
            uint skillID = 10000;
            uint num = 3166;
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            foreach (ActorSkill actorSkill in map.Actors.Cast<SagaDB.Actor.Actor>().Where<SagaDB.Actor.Actor>((Func<SagaDB.Actor.Actor, bool>)(act => act.type == ActorType.SKILL)))
            {
                if ((int)actorSkill.Skill.ID == (int)num)
                    args.autoCast.Add(Singleton<SkillHandler>.Instance.CreateAutoCastInfo(skillID, level, 0, SagaLib.Global.PosX16to8(actorSkill.X, map.Width), SagaLib.Global.PosY16to8(actorSkill.Y, map.Height)));
            }
        }
    }
}
