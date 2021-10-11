namespace SagaMap.Skill.SkillDefinations.Shaman
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="StoneWall" />.
    /// </summary>
    public class StoneWall : ISkill
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
            int rand = SagaLib.Global.Random.Next(0, 99);
            StoneWall.StoneWallBuff stoneWallBuff = new StoneWall.StoneWallBuff(args, sActor, 60000, rand);
            SkillHandler.ApplyAddition(sActor, (Addition)stoneWallBuff);
        }

        /// <summary>
        /// Defines the <see cref="StoneWallBuff" />.
        /// </summary>
        public class StoneWallBuff : DefaultBuff
        {
            /// <summary>
            /// Defines the MobLst.
            /// </summary>
            private List<ActorMob> MobLst = new List<ActorMob>();

            /// <summary>
            /// Defines the args.
            /// </summary>
            private SkillArg args;

            /// <summary>
            /// Initializes a new instance of the <see cref="StoneWallBuff"/> class.
            /// </summary>
            /// <param name="skill">The skill<see cref="SkillArg"/>.</param>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            /// <param name="rand">The rand<see cref="int"/>.</param>
            public StoneWallBuff(SkillArg skill, SagaDB.Actor.Actor actor, int lifetime, int rand)
        : base(skill.skill, actor, nameof(StoneWall) + rand.ToString(), lifetime)
            {
                this.OnAdditionStart += new DefaultBuff.StartEventHandler(this.StartEvent);
                this.OnAdditionEnd += new DefaultBuff.EndEventHandler(this.EndEvent);
                this.args = skill.Clone();
            }

            /// <summary>
            /// The StartEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void StartEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                uint mobID = (uint)(30470000 + (int)skill.skill.Level - 1);
                byte[] numArray1 = new byte[3];
                byte[] numArray2 = new byte[3];
                Singleton<SkillHandler>.Instance.GetRelatedPos(actor, 0, 0, this.args.x, this.args.y, out numArray1[0], out numArray2[0]);
                switch (Singleton<SkillHandler>.Instance.GetDirection(actor))
                {
                    case SkillHandler.ActorDirection.South:
                    case SkillHandler.ActorDirection.North:
                        Singleton<SkillHandler>.Instance.GetRelatedPos(actor, 1, 0, this.args.x, this.args.y, out numArray1[1], out numArray2[1]);
                        Singleton<SkillHandler>.Instance.GetRelatedPos(actor, -1, 0, this.args.x, this.args.y, out numArray1[2], out numArray2[2]);
                        break;
                    case SkillHandler.ActorDirection.SouthWest:
                    case SkillHandler.ActorDirection.NorthEast:
                        Singleton<SkillHandler>.Instance.GetRelatedPos(actor, 1, -1, this.args.x, this.args.y, out numArray1[1], out numArray2[1]);
                        Singleton<SkillHandler>.Instance.GetRelatedPos(actor, -1, 1, this.args.x, this.args.y, out numArray1[2], out numArray2[2]);
                        break;
                    case SkillHandler.ActorDirection.West:
                    case SkillHandler.ActorDirection.East:
                        Singleton<SkillHandler>.Instance.GetRelatedPos(actor, 0, -1, this.args.x, this.args.y, out numArray1[1], out numArray2[1]);
                        Singleton<SkillHandler>.Instance.GetRelatedPos(actor, 0, 1, this.args.x, this.args.y, out numArray1[2], out numArray2[2]);
                        break;
                    case SkillHandler.ActorDirection.NorthWest:
                    case SkillHandler.ActorDirection.SouthEast:
                        Singleton<SkillHandler>.Instance.GetRelatedPos(actor, -1, -1, this.args.x, this.args.y, out numArray1[1], out numArray2[1]);
                        Singleton<SkillHandler>.Instance.GetRelatedPos(actor, 1, 1, this.args.x, this.args.y, out numArray1[2], out numArray2[2]);
                        break;
                }
                this.MobLst.Add(map.SpawnMob(mobID, SagaLib.Global.PosX8to16(numArray1[0], map.Width), SagaLib.Global.PosY8to16(numArray2[0], map.Height), (short)50, (SagaDB.Actor.Actor)null));
                this.MobLst.Add(map.SpawnMob(mobID, SagaLib.Global.PosX8to16(numArray1[1], map.Width), SagaLib.Global.PosY8to16(numArray2[1], map.Height), (short)50, (SagaDB.Actor.Actor)null));
                this.MobLst.Add(map.SpawnMob(mobID, SagaLib.Global.PosX8to16(numArray1[2], map.Width), SagaLib.Global.PosY8to16(numArray2[2], map.Height), (short)50, (SagaDB.Actor.Actor)null));
            }

            /// <summary>
            /// The EndEvent.
            /// </summary>
            /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="skill">The skill<see cref="DefaultBuff"/>.</param>
            private void EndEvent(SagaDB.Actor.Actor actor, DefaultBuff skill)
            {
                Map map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                foreach (ActorMob actorMob in this.MobLst)
                    map.DeleteActor((SagaDB.Actor.Actor)actorMob);
            }
        }
    }
}
