namespace SagaMap.Skill.SkillDefinations.Druid
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;

    /// <summary>
    /// Defines the <see cref="CureAll" />.
    /// </summary>
    public class CureAll : ISkill
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
            return dActor.type == ActorType.MOB && ((MobEventHandler)dActor.e).AI.Mode.Symbol ? -14 : 0;
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
            float[] numArray1 = new float[10]
            {
        -0.0f,
        -1f,
        -1.5f,
        -1.7f,
        -2f,
        -2.3f,
        -2.5f,
        -2.7f,
        -2.8f,
        -3f
            };
            int[] numArray2 = new int[10]
            {
        92,
        96,
        96,
        97,
        98,
        99,
        99,
        99,
        99,
        99
            };
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor, args, SkillHandler.DefType.IgnoreAll, Elements.Holy, numArray1[(int)level - 1]);
            bool flag = false;
            if (SagaLib.Global.Random.Next(0, 99) < numArray2[(int)level - 1])
                flag = true;
            if (!flag)
                return;
            this.RemoveAddition(dActor, "Poison");
            this.RemoveAddition(dActor, "鈍足");
            this.RemoveAddition(dActor, "Stone");
            this.RemoveAddition(dActor, "Silence");
            this.RemoveAddition(dActor, "Stun");
            this.RemoveAddition(dActor, "Sleep");
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
