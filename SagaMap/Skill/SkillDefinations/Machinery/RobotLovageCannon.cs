namespace SagaMap.Skill.SkillDefinations.Machinery
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="RobotLovageCannon" />.
    /// </summary>
    public class RobotLovageCannon : ISkill
    {
        /// <summary>
        /// Defines the range.
        /// </summary>
        public Dictionary<SkillHandler.ActorDirection, List<int>> range = new Dictionary<SkillHandler.ActorDirection, List<int>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="RobotLovageCannon"/> class.
        /// </summary>
        public RobotLovageCannon()
        {
            for (int index = 0; index < 8; ++index)
                this.range.Add((SkillHandler.ActorDirection)index, new List<int>());
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 1, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 1, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 1, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 2, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 2, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 2, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 3, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 3, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 3, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 4, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 4, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 4, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 5, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 5, 6));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 5, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 0, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 1, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 1, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 1, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 2, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 2, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 2, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 3, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, 3, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, 3, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, 4, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, 4, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(5, 4, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, 5, 6));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, 5, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 1, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 0, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -1, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 1, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 0, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -1, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, 1, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, 0, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, -1, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, 1, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, 0, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, -1, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(5, 1, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(5, 0, 6));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(5, -1, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 0, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -1, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -1, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -1, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -2, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -2, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, -2, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -3, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, -3, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, -3, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, -4, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, -4, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(5, -4, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(4, -5, 6));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(5, -5, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -1, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -1, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -1, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -2, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -2, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -2, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -3, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -3, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -3, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -4, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -4, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -4, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -5, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -5, 6));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -5, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 0, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -1, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -1, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -1, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -2, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -2, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, -2, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -3, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, -3, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-4, -3, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, -4, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-4, -4, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-5, -4, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-4, -5, 6));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-5, -5, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 1, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 0, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -1, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 1, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 0, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -1, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, 1, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, 0, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, -1, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-4, 1, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-4, 0, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-4, -1, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-5, 1, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-5, 0, 6));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-5, -1, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 0, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 1, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 1, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 1, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 2, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 2, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, 2, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 3, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, 3, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-4, 3, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, 4, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-4, 4, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-5, 4, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-4, 5, 6));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-5, 5, 6));
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
            ActorPet pet = Singleton<SkillHandler>.Instance.GetPet((SagaDB.Actor.Actor)sActor);
            return pet == null || !Singleton<SkillHandler>.Instance.CheckMobType((ActorMob)pet, "MACHINE_RIDE_ROBOT") ? -53 : 0;
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
            float ATKBonus = (float)(1.20000004768372 + 0.25 * (double)level);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(sActor, (short)600, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            SkillHandler.ActorDirection direction = Singleton<SkillHandler>.Instance.GetDirection(sActor);
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                {
                    int XDiff;
                    int YDiff;
                    Singleton<SkillHandler>.Instance.GetXYDiff(map, sActor, dActor2, out XDiff, out YDiff);
                    if (this.range[direction].Contains(Singleton<SkillHandler>.Instance.CalcPosHashCode(XDiff, YDiff, 6)))
                        dActor1.Add(dActor2);
                }
            }
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
