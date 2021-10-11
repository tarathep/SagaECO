namespace SagaMap.Skill.SkillDefinations.Event
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="WeepingWillow2" />.
    /// </summary>
    public class WeepingWillow2 : ISkill
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
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            foreach (SagaDB.Actor.Actor actor in map.GetActorsArea(sActor, (short)200, false))
            {
                if (actor.type == ActorType.PC || actor.type == ActorType.PET || actor.type == ActorType.SHADOW)
                {
                    Confuse confuse = new Confuse(args.skill, actor, 3000);
                    SkillHandler.ApplyAddition(actor, (Addition)confuse);
                }
            }
            sActor.ClearTaskAddition();
            map.DeleteActor(sActor);
        }
    }
}
