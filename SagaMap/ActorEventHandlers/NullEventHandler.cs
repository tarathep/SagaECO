namespace SagaMap.ActorEventHandlers
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="NullEventHandler" />.
    /// </summary>
    public class NullEventHandler : ActorEventHandler
    {
        /// <summary>
        /// The OnActorAppears.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorAppears(SagaDB.Actor.Actor aActor)
        {
        }

        /// <summary>
        /// The OnActorChangeEquip.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChangeEquip(SagaDB.Actor.Actor sActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorChat.
        /// </summary>
        /// <param name="cActor">The cActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChat(SagaDB.Actor.Actor cActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorDisappears.
        /// </summary>
        /// <param name="dActor">The dActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorDisappears(SagaDB.Actor.Actor dActor)
        {
        }

        /// <summary>
        /// The OnActorSkillUse.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorSkillUse(SagaDB.Actor.Actor sActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorStartsMoving.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="pos">The pos<see cref="short[]"/>.</param>
        /// <param name="dir">The dir<see cref="ushort"/>.</param>
        /// <param name="speed">The speed<see cref="ushort"/>.</param>
        public void OnActorStartsMoving(SagaDB.Actor.Actor mActor, short[] pos, ushort dir, ushort speed)
        {
        }

        /// <summary>
        /// The OnActorStopsMoving.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="pos">The pos<see cref="short[]"/>.</param>
        /// <param name="dir">The dir<see cref="ushort"/>.</param>
        /// <param name="speed">The speed<see cref="ushort"/>.</param>
        public void OnActorStopsMoving(SagaDB.Actor.Actor mActor, short[] pos, ushort dir, ushort speed)
        {
        }

        /// <summary>
        /// The OnCreate.
        /// </summary>
        /// <param name="success">The success<see cref="bool"/>.</param>
        public void OnCreate(bool success)
        {
        }

        /// <summary>
        /// The OnActorChangeEmotion.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChangeEmotion(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorChangeMotion.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorChangeMotion(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnDelete.
        /// </summary>
        public void OnDelete()
        {
        }

        /// <summary>
        /// The OnCharInfoUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnCharInfoUpdate(SagaDB.Actor.Actor aActor)
        {
        }

        /// <summary>
        /// The OnPlayerSizeChange.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnPlayerSizeChange(SagaDB.Actor.Actor aActor)
        {
        }

        /// <summary>
        /// The OnDie.
        /// </summary>
        public void OnDie()
        {
        }

        /// <summary>
        /// The OnKick.
        /// </summary>
        public void OnKick()
        {
        }

        /// <summary>
        /// The OnMapLoaded.
        /// </summary>
        public void OnMapLoaded()
        {
        }

        /// <summary>
        /// The OnReSpawn.
        /// </summary>
        public void OnReSpawn()
        {
        }

        /// <summary>
        /// The OnSendMessage.
        /// </summary>
        /// <param name="from">The from<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        public void OnSendMessage(string from, string message)
        {
        }

        /// <summary>
        /// The OnSendWhisper.
        /// </summary>
        /// <param name="name">The name<see cref="string"/>.</param>
        /// <param name="message">The message<see cref="string"/>.</param>
        /// <param name="flag">The flag<see cref="byte"/>.</param>
        public void OnSendWhisper(string name, string message, byte flag)
        {
        }

        /// <summary>
        /// The OnTeleport.
        /// </summary>
        /// <param name="x">The x<see cref="short"/>.</param>
        /// <param name="y">The y<see cref="short"/>.</param>
        public void OnTeleport(short x, short y)
        {
        }

        /// <summary>
        /// The OnAttack.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnAttack(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnHPMPSPUpdate.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnHPMPSPUpdate(SagaDB.Actor.Actor sActor)
        {
        }

        /// <summary>
        /// The OnPlayerChangeStatus.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnPlayerChangeStatus(ActorPC aActor)
        {
        }

        /// <summary>
        /// The OnActorChangeBuff.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorChangeBuff(SagaDB.Actor.Actor sActor)
        {
        }

        /// <summary>
        /// The OnLevelUp.
        /// </summary>
        /// <param name="sActor">The sActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnLevelUp(SagaDB.Actor.Actor sActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnPlayerMode.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnPlayerMode(SagaDB.Actor.Actor aActor)
        {
        }

        /// <summary>
        /// The OnShowEffect.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnShowEffect(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorPossession.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        /// <param name="args">The args<see cref="MapEventArgs"/>.</param>
        public void OnActorPossession(SagaDB.Actor.Actor aActor, MapEventArgs args)
        {
        }

        /// <summary>
        /// The OnActorPartyUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorPartyUpdate(ActorPC aActor)
        {
        }

        /// <summary>
        /// The OnActorSpeedChange.
        /// </summary>
        /// <param name="mActor">The mActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnActorSpeedChange(SagaDB.Actor.Actor mActor)
        {
        }

        /// <summary>
        /// The OnSignUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="SagaDB.Actor.Actor"/>.</param>
        public void OnSignUpdate(SagaDB.Actor.Actor aActor)
        {
        }

        /// <summary>
        /// The PropertyUpdate.
        /// </summary>
        /// <param name="arg">The arg<see cref="UpdateEvent"/>.</param>
        /// <param name="para">The para<see cref="int"/>.</param>
        public void PropertyUpdate(UpdateEvent arg, int para)
        {
        }

        /// <summary>
        /// The PropertyRead.
        /// </summary>
        /// <param name="arg">The arg<see cref="UpdateEvent"/>.</param>
        public void PropertyRead(UpdateEvent arg)
        {
        }

        /// <summary>
        /// The OnActorRingUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorRingUpdate(ActorPC aActor)
        {
        }

        /// <summary>
        /// The OnActorWRPRankingUpdate.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorWRPRankingUpdate(ActorPC aActor)
        {
        }

        /// <summary>
        /// The OnActorChangeAttackType.
        /// </summary>
        /// <param name="aActor">The aActor<see cref="ActorPC"/>.</param>
        public void OnActorChangeAttackType(ActorPC aActor)
        {
        }
    }
}
