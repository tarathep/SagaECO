namespace SagaMap.Skill.SkillDefinations.Assassin
{
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.ActorEventHandlers;
    using SagaMap.Manager;
    using SagaMap.Mob;
    using System;

    /// <summary>
    /// Defines the <see cref="EventSumNinJa" />.
    /// </summary>
    public class EventSumNinJa : ISkill
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
            Map map = Singleton<MapManager>.Instance.GetMap(sActor.MapID);
            ActorPC creator = (ActorPC)sActor;
            if (level == (byte)1)
            {
                if (sActor.Slave.Count >= 1)
                {
                    PetEventHandler e = (PetEventHandler)sActor.Slave[0].e;
                    e.AI.Pause();
                    e.AI.map.DeleteActor(e.AI.Mob);
                    sActor.Slave.Remove(e.AI.Mob);
                    e.AI.Mob.Tasks["Shadow"].Deactivate();
                    e.AI.Mob.Tasks.Remove("Shadow");
                }
                ActorShadow actor = new ActorShadow(creator);
                actor.Name = Singleton<LocalManager>.Instance.Strings.SKILL_DECOY + creator.Name;
                actor.MapID = creator.MapID;
                actor.X = (short)((int)sActor.X + SagaLib.Global.Random.Next(1, 10));
                actor.Y = (short)((int)sActor.Y + SagaLib.Global.Random.Next(1, 10));
                PetEventHandler petEventHandler = new PetEventHandler((ActorPet)actor);
                actor.e = (ActorEventHandler)petEventHandler;
                actor.Int = creator.Int >= (ushort)10 ? (ushort)((uint)creator.Int - 10U) : (ushort)0;
                actor.Str = creator.Str >= (ushort)10 ? (ushort)((uint)creator.Str - 10U) : (ushort)0;
                actor.Mag = creator.Mag >= (ushort)10 ? (ushort)((uint)creator.Mag - 10U) : (ushort)0;
                actor.Dex = creator.Dex >= (ushort)10 ? (ushort)((uint)creator.Dex - 10U) : (ushort)0;
                actor.Agi = creator.Agi >= (ushort)10 ? (ushort)((uint)creator.Agi - 10U) : (ushort)0;
                actor.Vit = creator.Vit >= (ushort)10 ? (ushort)((uint)creator.Vit - 10U) : (ushort)0;
                petEventHandler.AI.Mode = new AIMode(0);
                map.RegisterActor((SagaDB.Actor.Actor)actor);
                actor.invisble = false;
                map.OnActorVisibilityChange((SagaDB.Actor.Actor)actor);
                map.SendVisibleActorsToActor((SagaDB.Actor.Actor)actor);
                petEventHandler.AI.Start();
                EventSumNinJa.Activator activator = new EventSumNinJa.Activator(sActor, actor, (int)level * 10000);
                actor.Tasks.Add("Shadow", (MultiRunTask)activator);
                activator.Activate();
                sActor.Slave.Add((SagaDB.Actor.Actor)actor);
            }
            else
            {
                if (sActor.Slave.Count >= (int)level - 1)
                {
                    sActor.Slave[0].ClearTaskAddition();
                    map.DeleteActor(sActor.Slave[0]);
                    sActor.Slave.Remove(sActor.Slave[0]);
                }
                ActorShadow actorShadow = new ActorShadow(creator);
                actorShadow.Name = Singleton<LocalManager>.Instance.Strings.SKILL_DECOY + creator.Name;
                actorShadow.MapID = creator.MapID;
                actorShadow.X = creator.X;
                actorShadow.Y = creator.Y;
                actorShadow.Int = creator.Int >= (ushort)10 ? (ushort)((uint)creator.Int - 10U) : (ushort)0;
                actorShadow.Str = creator.Str >= (ushort)10 ? (ushort)((uint)creator.Str - 10U) : (ushort)0;
                actorShadow.Mag = creator.Mag >= (ushort)10 ? (ushort)((uint)creator.Mag - 10U) : (ushort)0;
                actorShadow.Dex = creator.Dex >= (ushort)10 ? (ushort)((uint)creator.Dex - 10U) : (ushort)0;
                actorShadow.Agi = creator.Agi >= (ushort)10 ? (ushort)((uint)creator.Agi - 10U) : (ushort)0;
                actorShadow.Vit = creator.Vit >= (ushort)10 ? (ushort)((uint)creator.Vit - 10U) : (ushort)0;
                actorShadow.MaxHP = (uint)((double)creator.MaxHP * (0.5 * (double)level));
                actorShadow.HP = (uint)((double)creator.HP * (0.5 * (double)level));
                actorShadow.Speed = creator.Speed;
                actorShadow.BaseData.mobSize = 1f;
                PetEventHandler petEventHandler = new PetEventHandler((ActorPet)actorShadow);
                actorShadow.e = (ActorEventHandler)petEventHandler;
                petEventHandler.AI.Mode = new AIMode(1);
                petEventHandler.AI.Master = (SagaDB.Actor.Actor)creator;
                map.RegisterActor((SagaDB.Actor.Actor)actorShadow);
                actorShadow.invisble = false;
                map.OnActorVisibilityChange((SagaDB.Actor.Actor)actorShadow);
                map.SendVisibleActorsToActor((SagaDB.Actor.Actor)actorShadow);
                petEventHandler.AI.Start();
                sActor.Slave.Add((SagaDB.Actor.Actor)actorShadow);
            }
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
