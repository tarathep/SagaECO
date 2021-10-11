namespace SagaMap.Tasks.PC
{
    using global::System;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="Regeneration" />.
    /// </summary>
    public class Regeneration : MultiRunTask
    {
        /// <summary>
        /// Defines the count.
        /// </summary>
        private int count = 0;

        /// <summary>
        /// Defines the client.
        /// </summary>
        private MapClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Regeneration"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        public Regeneration(MapClient client)
        {
            this.dueTime = 5000;
            this.period = 5000;
            this.client = client;
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
                this.client.Character.HP += (uint)(5 + this.count) + (uint)this.client.Character.Status.hp_recover_skill + (uint)this.client.Character.Status.hp_recover_mario;
                if (this.client.Character.HP > this.client.Character.MaxHP)
                    this.client.Character.HP = this.client.Character.MaxHP;
                this.client.Character.MP += (uint)(2 + this.count) + (uint)this.client.Character.Status.mp_recover_skill + (uint)this.client.Character.Status.mp_recover_mario;
                if (this.client.Character.MP > this.client.Character.MaxMP)
                    this.client.Character.MP = this.client.Character.MaxMP;
                this.client.Character.SP += (uint)(2 + this.count) + (uint)this.client.Character.Status.sp_recover_skill;
                if (this.client.Character.SP > this.client.Character.MaxSP)
                    this.client.Character.SP = this.client.Character.MaxSP;
                this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.client.Character, true);
                ++this.count;
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.client.Character.Tasks.Remove(nameof(Regeneration));
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
