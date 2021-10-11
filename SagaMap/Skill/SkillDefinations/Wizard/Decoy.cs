namespace SagaMap.Skill.SkillDefinations.Wizard
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using System;

    /// <summary>
    /// Defines the <see cref="Decoy" />.
    /// </summary>
    public class Decoy : ISkill
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
            if (sActor.type != ActorType.PC)
                return;
            if (sActor.Slave.Count >= 3)
            {
                PetEventHandler e = (PetEventHandler)sActor.Slave[0].e;
                e.AI.Pause();
                e.AI.map.DeleteActor(e.AI.Mob);
                sActor.Slave.Remove(e.AI.Mob);
                e.AI.Mob.Tasks["Shadow"].Deactivate();
                e.AI.Mob.Tasks.Remove("Shadow");
            }
            ActorPC creator = (ActorPC)sActor;
            ActorShadow actor = new ActorShadow(creator);
            Map map = Singleton<MapManager>.Instance.GetMap(creator.MapID);
            actor.Name = Singleton<LocalManager>.Instance.Strings.SKILL_DECOY + creator.Name;
            actor.MapID = creator.MapID;
            actor.X = SagaLib.Global.PosX8to16(args.x, map.Width);
            actor.Y = SagaLib.Global.PosY8to16(args.y, map.Height);
            PetEventHandler petEventHandler = new PetEventHandler((ActorPet)actor);
            actor.e = (ActorEventHandler)petEventHandler;
            petEventHandler.AI.Mode = new AIMode(0);
            map.RegisterActor((SagaDB.Actor.Actor)actor);
            actor.invisble = false;
            map.OnActorVisibilityChange((SagaDB.Actor.Actor)actor);
            map.SendVisibleActorsToActor((SagaDB.Actor.Actor)actor);
            petEventHandler.AI.Start();
            Decoy.Activator activator = new Decoy.Activator(sActor, actor, (int)level * 10000);
            actor.Tasks.Add("Shadow", (MultiRunTask)activator);
            activator.Activate();
            sActor.Slave.Add((SagaDB.Actor.Actor)actor);
        }

        /// <summary>
        /// Defines the <see cref="Activator" />.
        /// </summary>
        private class Activator : MultiRunTask
        {
            /// <summary>
            /// Defines the actor.
            /// </summary>
            private ActorShadow actor;

            /// <summary>
            /// Defines the castor.
            /// </summary>
            private SagaDB.Actor.Actor castor;

            /// <summary>
            /// Defines the map.
            /// </summary>
            private Map map;

            /// <summary>
            /// Initializes a new instance of the <see cref="Activator"/> class.
            /// </summary>
            /// <param name="castor">The castor<see cref="SagaDB.Actor.Actor"/>.</param>
            /// <param name="actor">The actor<see cref="ActorShadow"/>.</param>
            /// <param name="lifetime">The lifetime<see cref="int"/>.</param>
            public Activator(SagaDB.Actor.Actor castor, ActorShadow actor, int lifetime)
            {
                this.map = Singleton<MapManager>.Instance.GetMap(actor.MapID);
                this.period = lifetime;
                this.dueTime = lifetime;
                this.actor = actor;
                this.castor = castor;
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
                    ((PetEventHandler)this.actor.e).AI.Pause();
                    this.map.DeleteActor((SagaDB.Actor.Actor)this.actor);
                    this.castor.Slave.Remove((SagaDB.Actor.Actor)this.actor);
                    this.actor.Tasks.Remove("Shadow");
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
