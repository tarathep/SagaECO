namespace SagaMap.Skill.SkillDefinations.Monster
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="MobWindCrosscircleSeq2" />.
    /// </summary>
    public class MobWindCrosscircleSeq2 : ISkill
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
            float MATKBonus = 3f;
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            short[] numArray = new short[2]
            {
        (short) SagaLib.Global.Random.Next((int) sActor.X - 200, (int) sActor.X + 200),
        (short) SagaLib.Global.Random.Next((int) sActor.Y - 200, (int) sActor.Y + 200)
            };
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(numArray[0], numArray[1], (short)300, (SagaDB.Actor.Actor[])null);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                    dActor1.Add(dActor2);
            }
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, Elements.Wind, MATKBonus);
            args.dActor = uint.MaxValue;
            args.x = SagaLib.Global.PosX16to8(numArray[0], map.Width);
            args.y = SagaLib.Global.PosY16to8(numArray[1], map.Height);
        }
    }
}
