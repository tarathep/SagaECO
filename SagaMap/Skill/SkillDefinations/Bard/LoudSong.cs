namespace SagaMap.Skill.SkillDefinations.Bard
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="LoudSong" />.
    /// </summary>
    public class LoudSong : ISkill
    {
        /// <summary>
        /// Defines the range.
        /// </summary>
        public Dictionary<SkillHandler.ActorDirection, List<int>> range = new Dictionary<SkillHandler.ActorDirection, List<int>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="LoudSong"/> class.
        /// </summary>
        public LoudSong()
        {
            for (int index = 0; index < 8; ++index)
                this.range.Add((SkillHandler.ActorDirection)index, new List<int>());
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 1, 2));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 1, 2));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 1, 2));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 2, 2));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 2, 2));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 2, 2));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 2, 2));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 2, 2));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 0, 2));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 1, 2));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 2, 2));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 1, 2));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 0, 2));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 1, 2));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 0, 2));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -1, 2));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 2, 2));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 1, 2));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 0, 2));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -1, 2));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -2, 2));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 0, 2));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -1, 2));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -2, 2));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -1, 2));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 0, 2));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -1, 2));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -1, 2));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -1, 2));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -2, 2));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -2, 2));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -2, 2));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -2, 2));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -2, 2));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 0, 2));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -1, 2));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -2, 2));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -1, 2));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 0, 2));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 1, 2));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 0, 2));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -1, 2));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 2, 2));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 1, 2));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 0, 2));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -1, 2));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -2, 2));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 0, 2));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 1, 2));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 2, 2));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 1, 2));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 0, 2));
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
            return Singleton<SkillHandler>.Instance.isEquipmentRight((SagaDB.Actor.Actor)sActor, ItemType.STRINGS) || sActor.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count > 0 ? 0 : -5;
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
            float MATKBonus = 1f;
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(sActor, (short)250, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            SkillHandler.ActorDirection direction = Singleton<SkillHandler>.Instance.GetDirection(sActor);
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                {
                    int XDiff;
                    int YDiff;
                    Singleton<SkillHandler>.Instance.GetXYDiff(map, sActor, dActor2, out XDiff, out YDiff);
                    bool flag = false;
                    switch (direction)
                    {
                        case SkillHandler.ActorDirection.South:
                            int num1;
                            switch (YDiff)
                            {
                                case -2:
                                    num1 = 0;
                                    break;
                                case -1:
                                    if (XDiff != -2)
                                    {
                                        num1 = XDiff == 2 ? 1 : 0;
                                        break;
                                    }
                                    goto default;
                                default:
                                    num1 = 1;
                                    break;
                            }
                            if (num1 == 0)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.SouthWest:
                            if (XDiff == 0 && YDiff < 0 || XDiff < 0 && YDiff == 0 || XDiff == -1 && YDiff == -1)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.West:
                            int num2;
                            switch (XDiff)
                            {
                                case -2:
                                    num2 = 0;
                                    break;
                                case -1:
                                    if (YDiff != -2)
                                    {
                                        num2 = YDiff == 2 ? 1 : 0;
                                        break;
                                    }
                                    goto default;
                                default:
                                    num2 = 1;
                                    break;
                            }
                            if (num2 == 0)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.NorthWest:
                            if (XDiff == 0 && YDiff > 0 || XDiff < 0 && YDiff == 0 || XDiff == -1 && YDiff == 1)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.North:
                            int num3;
                            switch (YDiff)
                            {
                                case 1:
                                    if (XDiff != -2)
                                    {
                                        num3 = XDiff == 2 ? 1 : 0;
                                        break;
                                    }
                                    goto default;
                                case 2:
                                    num3 = 0;
                                    break;
                                default:
                                    num3 = 1;
                                    break;
                            }
                            if (num3 == 0)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.NorthEast:
                            if (XDiff == 0 && YDiff > 0 || XDiff > 0 && YDiff == 0 || XDiff == 1 && YDiff == 1)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.East:
                            int num4;
                            switch (XDiff)
                            {
                                case 1:
                                    if (YDiff != -2)
                                    {
                                        num4 = YDiff == 2 ? 1 : 0;
                                        break;
                                    }
                                    goto default;
                                case 2:
                                    num4 = 0;
                                    break;
                                default:
                                    num4 = 1;
                                    break;
                            }
                            if (num4 == 0)
                            {
                                flag = true;
                                break;
                            }
                            break;
                        case SkillHandler.ActorDirection.SouthEast:
                            if (XDiff == 0 && YDiff < 0 || XDiff > 0 && YDiff == 0 || XDiff == 1 && YDiff == -1)
                            {
                                flag = true;
                                break;
                            }
                            break;
                    }
                    if (flag)
                        dActor1.Add(dActor2);
                }
            }
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, Elements.Holy, MATKBonus);
        }
    }
}
