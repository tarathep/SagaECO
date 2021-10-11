namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="FlashHandGrenade" />.
    /// </summary>
    public class FlashHandGrenade : ISkill
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
            int[] numArray1 = new int[4] { 0, 40, 55, 70 };
            int[] numArray2 = new int[4] { 0, 10000, 9000, 8000 };
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            ActorSkill actorSkill = new ActorSkill(args.skill, sActor);
            actorSkill.MapID = sActor.MapID;
            actorSkill.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actorSkill.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            actorSkill.e = (ActorEventHandler)new NullEventHandler();
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea((SagaDB.Actor.Actor)actorSkill, (short)350, false);
            List<SagaDB.Actor.Actor> actorList = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor actor in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, actor) && Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.鈍足, numArray1[(int)level]))
                {
                    鈍足 鈍足 = new 鈍足(args.skill, actor, numArray2[(int)level]);
                    SkillHandler.ApplyAddition(actor, (Addition)鈍足);
                    actorList.Add(actor);
                }
            }
        }
    }
}
