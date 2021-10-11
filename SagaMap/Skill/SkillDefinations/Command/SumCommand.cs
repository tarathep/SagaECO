namespace SagaMap.Skill.SkillDefinations.Command
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SumCommand" />.
    /// </summary>
    public class SumCommand : ISkill
    {
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
            int lifetime = 10000;
            List<SagaDB.Actor.Actor> sumMob = new List<SagaDB.Actor.Actor>();
            List<uint> uintList = new List<uint>();
            switch (level)
            {
                case 1:
                    uintList.Add(60023400U);
                    break;
                case 2:
                    uintList.Add(60091550U);
                    break;
                case 3:
                    uintList.Add(60023400U);
                    uintList.Add(60091550U);
                    break;
            }
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            foreach (uint mobID in uintList)
                sumMob.Add((SagaDB.Actor.Actor)map.SpawnMob(mobID, (short)((int)sActor.X + SagaLib.Global.Random.Next(-100, 100)), (short)((int)sActor.Y + SagaLib.Global.Random.Next(-100, 100)), (short)2500, sActor));
            SumCommand.SumCommandBuff sumCommandBuff = new SumCommand.SumCommandBuff(args.skill, sActor, sumMob, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)sumCommandBuff);
        }

        /// <summary>
        /// Defines the <see cref="SumCommandBuff" />.
        /// </summary>
        public class SumCommandBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the sumMob.
            /// </summary>
            private List<SagaDB.Actor.Actor> sumMob;

            /// <summary>
            /// Initializes a new instance of the <see cref="SumCommandBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SagaDB.Skill.Skill"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="sumMob">The sumMob<see cref="List{SagaDB.Actor.Actor}"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public SumCommandBuff(SagaDB.Skill.Skill skill, SagaDB.Actor.Actor actor, List<SagaDB.Actor.Actor> sumMob, int lifetime)
        : base(skill, actor, nameof(SumCommand), lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.sumMob = sumMob;
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                foreach (SagaDB.Actor.Actor dActor in this.sumMob)
                {
                    dActor.ClearTaskAddition();
                    map.DeleteActor(dActor);
                }
            }
        }
    }
}
