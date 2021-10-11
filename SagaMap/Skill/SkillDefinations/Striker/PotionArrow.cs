namespace SagaMap.Skill.SkillDefinations.Striker
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="PotionArrow" />.
    /// </summary>
    public class PotionArrow : ISkill
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
            uint num1 = (uint)(90 * (int)level + 2 * (int)sActor.Level / 3);
            uint num2 = 8;
            uint num3 = 8;
            int[] numArray = new int[4] { 0, 2, 4, 6 };
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            for (int index = 0; index < numArray[(int)level]; ++index)
                dActor1.Add(dActor);
            Singleton<SkillHandler>.Instance.PhysicalAttack(sActor, dActor1, args, Elements.Holy, 0.0f);
            if (dActor.HP + num1 < dActor.MaxHP)
                dActor.HP += num1;
            else
                dActor.HP = dActor.MaxHP;
            if (dActor.MP + num2 < dActor.MaxMP)
                dActor.MP += num2;
            else
                dActor.MP = dActor.MaxMP;
            if (dActor.SP + num3 < dActor.MaxSP)
                dActor.SP += num3;
            else
                dActor.SP = dActor.MaxSP;
            List<AttackFlag> flag;
            (flag = args.flag)[0] = flag[0] | AttackFlag.HP_HEAL | AttackFlag.MP_HEAL | AttackFlag.SP_HEAL | AttackFlag.NO_DAMAGE;
        }
    }
}
