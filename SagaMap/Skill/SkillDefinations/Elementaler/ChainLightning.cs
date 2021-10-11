namespace SagaMap.Skill.SkillDefinations.Elementaler
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="ChainLightning" />.
    /// </summary>
    internal class ChainLightning : ISkill
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
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            ActorSkill actorSkill = new ActorSkill(args.skill, sActor);
            actorSkill.MapID = sActor.MapID;
            actorSkill.X = sActor.X;
            actorSkill.Y = sActor.Y;
            List<MapNode> path = new MobAI((SagaDB.Actor.Actor)actorSkill, true).FindPath(SagaLib.Global.PosX16to8(sActor.X, map.Width), SagaLib.Global.PosY16to8(sActor.Y, map.Height), args.x, args.y);
            if (path.Count >= 2)
            {
                int num1 = (int)path[path.Count - 1].x - (int)path[path.Count - 2].x;
                int num2 = (int)path[path.Count - 1].y - (int)path[path.Count - 2].y;
                int num3 = (int)path[path.Count - 1].x + num1;
                int num4 = (int)path[path.Count - 1].y + num2;
                path.Add(new MapNode()
                {
                    x = (byte)num3,
                    y = (byte)num4
                });
            }
            if (path.Count == 1)
            {
                int num1 = (int)path[path.Count - 1].x - (int)SagaLib.Global.PosX16to8(sActor.X, map.Width);
                int num2 = (int)path[path.Count - 1].y - (int)SagaLib.Global.PosY16to8(sActor.Y, map.Height);
                int num3 = (int)path[path.Count - 1].x + num1;
                int num4 = (int)path[path.Count - 1].y + num2;
                path.Add(new MapNode()
                {
                    x = (byte)num3,
                    y = (byte)num4
                });
            }
            short[] numArray = new short[2];
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            for (int index = -1; path.Count > index + 1; ++index)
            {
                numArray[0] = SagaLib.Global.PosX8to16(path[index + 1].x, map.Width);
                numArray[1] = SagaLib.Global.PosY8to16(path[index + 1].y, map.Height);
                foreach (SagaDB.Actor.Actor dActor2 in map.GetActorsArea(numArray[0], numArray[1], (short)50, new SagaDB.Actor.Actor[0]))
                {
                    if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                        dActor1.Add(dActor2);
                }
            }
            ActorPC actorPc = (ActorPC)sActor;
            List<int> intList = new List<int>();
            intList.Add(3028);
            intList.Add(3025);
            intList.Add(3019);
            intList.Add(3018);
            intList.Add(3020);
            intList.Add(3017);
            int num5 = 0;
            foreach (uint key in intList)
            {
                if (actorPc.Skills.ContainsKey(key))
                    num5 += (int)actorPc.Skills[key].BaseData.lv;
                if (actorPc.Skills2.ContainsKey(key))
                    num5 += (int)actorPc.Skills2[key].BaseData.lv;
            }
            float num6 = 1f;
            if (num5 >= 5 && num5 >= 1)
                num6 = 1f;
            else if (num5 >= 8 && num5 >= 6)
                num6 = 1.5f;
            else if (num5 >= 11 && num5 >= 9)
                num6 = 2f;
            else if (num5 >= 35 && num5 >= 12)
                num6 = 2.5f;
            float MATKBonus = num6 * 2.5f;
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, Elements.Wind, MATKBonus);
        }
    }
}
