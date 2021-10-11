namespace SagaMap.Skill.SkillDefinations.DarkStalker
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Manager;
    using System;

    /// <summary>
    /// Defines the <see cref="FlareSting2" />.
    /// </summary>
    public class FlareSting2 : ISkill
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
            new FlareSting2.Activator(sActor, dActor, args, level).Activate();
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the sActor.
            /// </summary>
            private SagaDB.Actor.Actor sActor;

            /// <summary>
            /// Defines the dActor.
            /// </summary>
            private SagaDB.Actor.Actor dActor;

            /// <summary>
            /// Defines the skill.
            /// </summary>
            private SkillArg skill;

            /// <summary>
            /// Defines the factor.
            /// </summary>
            private float factor;

            /// <summary>
            /// Defines the map.
            /// </summary>
            private Map map;

            /// <summary>
            /// Defines the lifetime.
            /// </summary>
            private int lifetime;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="_sActor">The _sActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="_dActor">The _dActor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="_args">The _args<see cref="SkillArg"/>.</param>
            /// <param name="level">The level<see cref="byte"/>.</param>
            public Activator(SagaDB.Actor.Actor _sActor, SagaDB.Actor.Actor _dActor, SkillArg _args, byte level)
            {
                this.sActor = _sActor;
                this.dActor = _dActor;
                this.skill = _args.Clone();
                this.factor = new float[6]
                {
          0.0f,
          2.25f,
          2.5f,
          2.75f,
          3f,
          3.25f
                }[(int)level];
                this.dueTime = 0;
                this.period = 1000;
                this.lifetime = 200000;
                this.map = Singleton<MapManager>.Instance.GetMap(this.sActor.MapID);
                Singleton<SkillHandler>.Instance.MagicAttack(this.sActor, this.dActor, this.skill, Elements.Dark, this.factor);
                this.factor = new float[6]
                {
          0.0f,
          0.009f,
          0.009f,
          0.012f,
          0.012f,
          0.015f
                }[(int)level];
            }

            /// <summary>
            /// The CallBack.
            /// </summary>
            /// <param name="o">The o<see cref="object"/>.</param>
            public override void CallBack(object o)
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    if (this.lifetime > 0)
                    {
                        uint num = (uint)((double)this.dActor.MaxHP * (double)this.factor);
                        if (this.dActor.HP > num)
                        {
                            this.dActor.HP -= num;
                            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, this.dActor, true);
                        }
                        else
                        {
                            this.dActor.HP = 0U;
                            Singleton<SkillHandler>.Instance.MagicAttack(this.sActor, this.dActor, this.skill, Elements.Dark, 1f);
                            this.lifetime = 0;
                        }
                        this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, (MapEventArgs)this.skill, this.dActor, false);
                        this.lifetime -= this.period;
                    }
                    else
                        this.Deactivate();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ClientManager.LeaveCriticalArea();
            }
        }
    }
}
