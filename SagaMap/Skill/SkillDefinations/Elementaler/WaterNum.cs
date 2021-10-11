namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Skill.Additions.Global;

    /// <summary>
    /// Defines the <see cref="WaterNum" />.
    /// </summary>
    internal class WaterNum : ISkill
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
            int num1 = 0;
            int lifetime = 0;
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, Elements.Water, 0.0f);
            args.flag[0] = AttackFlag.NONE;
            switch (level)
            {
                case 1:
                    num1 = 20;
                    lifetime = 4000;
                    break;
                case 2:
                    num1 = 30;
                    lifetime = 4500;
                    break;
                case 3:
                    num1 = 40;
                    lifetime = 5000;
                    break;
                case 4:
                    num1 = 55;
                    lifetime = 5500;
                    break;
                case 5:
                    num1 = 70;
                    lifetime = 6000;
                    break;
            }
            float num2 = 0.0f;
            int element = dActor.Elements[Elements.Water];
            if (element > 1 && element <= 100)
                num2 = 0.25f;
            if (element > 100 && element <= 200)
                num2 = 0.5f;
            if (element > 200 && element <= 300)
                num2 = 0.75f;
            if (element > 300)
                num2 = 0.9f;
            int num3 = (int)(100.0 - (double)(100 - num1) * (1.0 - (double)num2));
            if (SagaLib.Global.Random.Next(0, 99) >= num3)
                return;
            Freeze freeze = new Freeze(args.skill, dActor, lifetime);
            SkillHandler.ApplyAddition(dActor, (Addition)freeze);
        }
    }
}
