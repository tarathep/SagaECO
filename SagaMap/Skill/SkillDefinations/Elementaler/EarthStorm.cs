namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="EarthStorm" />.
    /// </summary>
    internal class EarthStorm : ISkill
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
            float MATKBonus = 0.0f;
            switch (level)
            {
                case 1:
                    MATKBonus = 1.2f;
                    break;
                case 2:
                    MATKBonus = 1.7f;
                    break;
                case 3:
                    MATKBonus = 2.2f;
                    break;
                case 4:
                    MATKBonus = 3.7f;
                    break;
                case 5:
                    MATKBonus = 3.2f;
                    break;
            }
            ActorSkill actorSkill = new ActorSkill(args.skill, sActor);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea(SagaLib.Global.PosX8to16(args.x, map.Width), SagaLib.Global.PosY8to16(args.y, map.Height), (short)200, (SagaDB.Actor.Actor[])null);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                    dActor1.Add(dActor2);
            }
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, Elements.Earth, MATKBonus);
        }
    }
}
