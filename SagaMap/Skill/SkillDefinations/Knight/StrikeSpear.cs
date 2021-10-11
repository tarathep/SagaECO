namespace SagaMap.Skill.SkillDefinations.Knight
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="StrikeSpear" />.
    /// </summary>
    public class StrikeSpear : ISkill
    {
        /// <summary>
        /// Defines the range.
        /// </summary>
        public Dictionary<SkillHandler.ActorDirection, List<int>> range = new Dictionary<SkillHandler.ActorDirection, List<int>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StrikeSpear"/> class.
        /// </summary>
        public StrikeSpear()
        {
            for (int index = 0; index < 8; ++index)
                this.range.Add((SkillHandler.ActorDirection)index, new List<int>());
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 1, 3));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 2, 3));
            this.range[SkillHandler.ActorDirection.North].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, 3, 3));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 1, 3));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 2, 3));
            this.range[SkillHandler.ActorDirection.NorthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, 3, 3));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, 0, 3));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, 0, 3));
            this.range[SkillHandler.ActorDirection.East].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, 0, 3));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(1, -1, 3));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(2, -2, 3));
            this.range[SkillHandler.ActorDirection.SouthEast].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(3, -3, 3));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -1, 3));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -2, 3));
            this.range[SkillHandler.ActorDirection.South].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(0, -3, 3));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, -1, 3));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, -2, 3));
            this.range[SkillHandler.ActorDirection.SouthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, -3, 3));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 0, 3));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 0, 3));
            this.range[SkillHandler.ActorDirection.West].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, 0, 3));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-1, 1, 3));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-2, 2, 3));
            this.range[SkillHandler.ActorDirection.NorthWest].Add(Singleton<SkillHandler>.Instance.CalcPosHashCode(-3, 3, 3));
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
            return Singleton<SkillHandler>.Instance.isEquipmentRight((SagaDB.Actor.Actor)sActor, ItemType.SPEAR, ItemType.RAPIER) || sActor.Inventory.GetContainer(ContainerType.RIGHT_HAND2).Count > 0 ? 0 : -5;
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
            float ATKBonus = (float)(1.79999995231628 + 0.200000002980232 * (double)level);
            uint key = 2138;
            ActorPC actorPc = (ActorPC)sActor;
            if (actorPc.Skills.ContainsKey(key) && actorPc.Skills[key].Level == (byte)5)
                ATKBonus = (float)(2.29999995231628 + 0.200000002980232 * (double)level);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(sActor, (short)300, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            SkillHandler.ActorDirection direction = Singleton<SkillHandler>.Instance.GetDirection(sActor);
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                {
                    int XDiff;
                    int YDiff;
                    Singleton<SkillHandler>.Instance.GetXYDiff(map, sActor, dActor2, out XDiff, out YDiff);
                    if (this.range[direction].Contains(Singleton<SkillHandler>.Instance.CalcPosHashCode(XDiff, YDiff, 3)))
                        dActor1.Add(dActor2);
                }
            }
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Neutral, ATKBonus);
        }
    }
}
