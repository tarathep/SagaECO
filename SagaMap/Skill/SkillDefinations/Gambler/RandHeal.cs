namespace SagaMap.Skill.SkillDefinations.Gambler
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="RandHeal" />.
    /// </summary>
    public class RandHeal : ISkill
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
            float MATKBonus = (float)(0.100000001490116 - 0.600000023841858 * (double)level);
            int num1 = 40 - 10 * (int)level;
            if (SagaLib.Global.Random.Next(0, 99) < num1)
                Singleton<SkillHandler>.Instance.MagicAttack(sActor, sActor, args, Elements.Holy, -MATKBonus);
            else
                Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Holy, MATKBonus);
            uint num2 = (uint)(25 + 5 * (int)level);
            if (dActor.MP + num2 <= dActor.MaxMP)
                dActor.MP += num2;
            else
                dActor.MP = dActor.MaxMP;
            if (dActor.SP + num2 <= dActor.MaxSP)
                dActor.SP += num2;
            else
                dActor.SP = dActor.MaxSP;
            args.affectedActors.Clear();
            args.affectedActors.Add(dActor);
            args.Init();
            List<AttackFlag> flag;
            (flag = args.flag)[0] = flag[0] | AttackFlag.MP_HEAL | AttackFlag.SP_HEAL | AttackFlag.NO_DAMAGE;
            if ((long)SagaLib.Global.Random.Next(0, 99) >= (long)num2)
                return;
            this.RemoveAddition(dActor, "Poison");
            this.RemoveAddition(dActor, "鈍足");
            this.RemoveAddition(dActor, "Stone");
            this.RemoveAddition(dActor, "Silence");
            this.RemoveAddition(dActor, "Stun");
            this.RemoveAddition(dActor, "Frosen");
            this.RemoveAddition(dActor, "Confuse");
        }

        /// <summary>
        /// The RemoveAddition.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="additionName">The additionName<see cref="string"/>.</param>
        public void RemoveAddition(SagaDB.Actor.Actor actor, string additionName)
        {
            if (!actor.Status.Additions.ContainsKey(additionName))
                return;
            Addition addition = actor.Status.Additions[additionName];
            actor.Status.Additions.Remove(additionName);
            if (addition.Activated)
                addition.AdditionEnd();
            addition.Activated = false;
        }
    }
}
