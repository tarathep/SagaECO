namespace SagaMap.Skill.SkillDefinations.Cabalist
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="DarkStorm" />.
    /// </summary>
    public class DarkStorm : ISkill
    {
        /// <summary>
        /// Defines the MobUse.
        /// </summary>
        private bool MobUse;

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkStorm"/> class.
        /// </summary>
        public DarkStorm()
        {
            this.MobUse = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DarkStorm"/> class.
        /// </summary>
        /// <param name="MobUse">The MobUse<see cref="bool"/>.</param>
        public DarkStorm(bool MobUse)
        {
            this.MobUse = MobUse;
        }

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
            if (this.MobUse)
                level = (byte)10;
            float MATKBonus = (float)(0.699999988079071 + 0.5 * (double)level);
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            ActorSkill actorSkill = new ActorSkill(args.skill, sActor);
            actorSkill.MapID = sActor.MapID;
            actorSkill.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actorSkill.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            actorSkill.e = (ActorEventHandler)new NullEventHandler();
            List<SagaDB.Actor.Actor> actorsArea = map.GetActorsArea((SagaDB.Actor.Actor)actorSkill, (short)250, false);
            List<SagaDB.Actor.Actor> dActor1 = new List<SagaDB.Actor.Actor>();
            foreach (SagaDB.Actor.Actor dActor2 in actorsArea)
            {
                if (Singleton<SkillHandler>.Instance.CheckValidAttackTarget(sActor, dActor2))
                    dActor1.Add(dActor2);
            }
            Singleton<SkillHandler>.Instance.MagicAttack(sActor, dActor1, args, Elements.Dark, MATKBonus);
        }
    }
}
