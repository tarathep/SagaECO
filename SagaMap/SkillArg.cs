namespace SagaMap
{
    using SagaDB.Actor;
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SkillArg" />.
    /// </summary>
    public class SkillArg : MapEventArgs
    {
        /// <summary>
        /// Defines the argType.
        /// </summary>
        public SkillArg.ArgType argType = SkillArg.ArgType.Attack;

        /// <summary>
        /// Defines the affectedActors.
        /// </summary>
        public List<SagaDB.Actor.Actor> affectedActors = new List<SagaDB.Actor.Actor>();

        /// <summary>
        /// Defines the hp.
        /// </summary>
        public List<int> hp = new List<int>();

        /// <summary>
        /// Defines the mp.
        /// </summary>
        public List<int> mp = new List<int>();

        /// <summary>
        /// Defines the sp.
        /// </summary>
        public List<int> sp = new List<int>();

        /// <summary>
        /// Defines the flag.
        /// </summary>
        public List<AttackFlag> flag = new List<AttackFlag>();

        /// <summary>
        /// Defines the autoCast.
        /// </summary>
        public List<AutoCastInfo> autoCast = new List<AutoCastInfo>();

        /// <summary>
        /// Defines the useMPSP.
        /// </summary>
        public bool useMPSP = true;

        /// <summary>
        /// Defines the showEffect.
        /// </summary>
        public bool showEffect = true;

        /// <summary>
        /// Defines the delayRate.
        /// </summary>
        public float delayRate = 1f;

        /// <summary>
        /// Defines the sActor.
        /// </summary>
        public uint sActor;

        /// <summary>
        /// Defines the dActor.
        /// </summary>
        public uint dActor;

        /// <summary>
        /// Defines the item.
        /// </summary>
        public SagaDB.Item.Item item;

        /// <summary>
        /// Defines the skill.
        /// </summary>
        public SagaDB.Skill.Skill skill;

        /// <summary>
        /// Defines the x.
        /// </summary>
        public byte x;

        /// <summary>
        /// Defines the y.
        /// </summary>
        public byte y;

        /// <summary>
        /// Defines the type.
        /// </summary>
        public ATTACK_TYPE type;

        /// <summary>
        /// Defines the delay.
        /// </summary>
        public uint delay;

        /// <summary>
        /// Defines the result.
        /// </summary>
        public short result;

        /// <summary>
        /// Defines the inventorySlot.
        /// </summary>
        public uint inventorySlot;

        /// <summary>
        /// The Clone.
        /// </summary>
        /// <returns>The <see cref="SkillArg"/>.</returns>
        public SkillArg Clone()
        {
            return new SkillArg()
            {
                sActor = this.sActor,
                dActor = this.dActor,
                skill = this.skill,
                x = this.x,
                y = this.y,
                argType = this.argType
            };
        }

        /// <summary>
        /// The Init.
        /// </summary>
        public void Init()
        {
            this.hp = new List<int>();
            this.mp = new List<int>();
            this.sp = new List<int>();
            this.flag = new List<AttackFlag>();
            for (int index = 0; index < this.affectedActors.Count; ++index)
            {
                this.flag.Add(AttackFlag.NONE);
                this.hp.Add(0);
                this.mp.Add(0);
                this.sp.Add(0);
            }
        }

        /// <summary>
        /// The Remove.
        /// </summary>
        /// <param name="actor">The actor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void Remove(SagaDB.Actor.Actor actor)
        {
            if (!this.affectedActors.Contains(actor))
                return;
            this.hp.RemoveAt(this.affectedActors.IndexOf(actor));
            this.mp.RemoveAt(this.affectedActors.IndexOf(actor));
            this.sp.RemoveAt(this.affectedActors.IndexOf(actor));
            this.flag.RemoveAt(this.affectedActors.IndexOf(actor));
            this.affectedActors.Remove(actor);
        }

        /// <summary>
        /// The Extend.
        /// </summary>
        /// <param name="count">The count<see cref="int"/>.</param>
        public void Extend(int count)
        {
            for (int index = 0; index < count; ++index)
                this.hp.Add(0);
            for (int index = 0; index < count; ++index)
                this.mp.Add(0);
            for (int index = 0; index < count; ++index)
                this.sp.Add(0);
            for (int index = 0; index < count; ++index)
                this.flag.Add(AttackFlag.NONE);
        }

        /// <summary>
        /// Defines the ArgType.
        /// </summary>
        public enum ArgType
        {
            /// <summary>
            /// Defines the Attack.
            /// </summary>
            Attack,

            /// <summary>
            /// Defines the Cast.
            /// </summary>
            Cast,

            /// <summary>
            /// Defines the Active.
            /// </summary>
            Active,

            /// <summary>
            /// Defines the Item_Cast.
            /// </summary>
            Item_Cast,

            /// <summary>
            /// Defines the Item_Active.
            /// </summary>
            Item_Active,

            /// <summary>
            /// Defines the Actor_Active.
            /// </summary>
            Actor_Active,
        }
    }
}
