namespace SagaMap.Skill.SkillDefinations.Bard
{
    using SagaDB.Actor;
    using SagaDB.Item;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Skill.Additions.Global;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DeadMarch" />.
    /// </summary>
    public class DeadMarch : ISkill
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
            int rate = 25 + 5 * (int)level;
            float MATKBonus = (float)(1.0 + 0.5 * (double)level);
            int lifetime = 4000;
            List<SagaDB.Actor.Actor> actorsArea = Singleton<MapManager>.Instance.GetMap(sActor.MapID).GetActorsArea(sActor, (short)350, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                    dActor1.Add(dActor2);
            }
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, Elements.Holy, MATKBonus);
            foreach (SagaDB.Actor.Actor actor in dActor1)
            {
                if (!Singleton<SkillHandler>.Instance.isBossMob(actor) && actor != sActor)
                {
                    if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Stun, rate))
                    {
                        Stun stun = new Stun(args.skill, actor, lifetime);
                        SkillHandler.ApplyAddition(actor, (Addition)stun);
                    }
                    if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.鈍足, rate))
                    {
                        鈍足 鈍足 = new 鈍足(args.skill, actor, lifetime);
                        SkillHandler.ApplyAddition(actor, (Addition)鈍足);
                    }
                    if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Silence, rate))
                    {
                        Silence silence = new Silence(args.skill, actor, lifetime);
                        SkillHandler.ApplyAddition(actor, (Addition)silence);
                    }
                    if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.CannotMove, rate))
                    {
                        CannotMove cannotMove = new CannotMove(args.skill, actor, lifetime);
                        SkillHandler.ApplyAddition(actor, (Addition)cannotMove);
                    }
                    if (Singleton<SkillHandler>.Instance.CanAdditionApply(sActor, actor, SkillHandler.DefaultAdditions.Confuse, rate))
                    {
                        Confuse confuse = new Confuse(args.skill, actor, lifetime);
                        SkillHandler.ApplyAddition(actor, (Addition)confuse);
                    }
                }
            }
        }
    }
}
