namespace SagaMap.Tasks.PC
{
    using global::System;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="PossessionRecover" />.
    /// </summary>
    public class PossessionRecover : MultiRunTask
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private MapClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="PossessionRecover"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        public PossessionRecover(MapClient client)
        {
            this.dueTime = 10000;
            this.period = 10000;
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
                if (this.client.Character.PossessionTarget == 0U)
                {
                    this.client.Character.Tasks.Remove(nameof(PossessionRecover));
                    this.Deactivate();
                    ClientManager.LeaveCriticalArea();
                    return;
                }
                this.client.Character.HP += (uint)((ulong)this.client.Character.MaxHP * (ulong)(100 + ((int)this.client.Character.Vit + (int)this.client.Character.Status.vit_item + (int)this.client.Character.Status.vit_mario + (int)this.client.Character.Status.vit_rev) / 3) / 1000UL);
                if (this.client.Character.HP > this.client.Character.MaxHP)
                    this.client.Character.HP = this.client.Character.MaxHP;
                this.client.Character.MP += (uint)((ulong)this.client.Character.MaxMP * (ulong)(100 + ((int)this.client.Character.Mag + (int)this.client.Character.Status.mag_item + (int)this.client.Character.Status.mag_mario + (int)this.client.Character.Status.mag_rev) / 3) / 1000UL);
                if (this.client.Character.MP > this.client.Character.MaxMP)
                    this.client.Character.MP = this.client.Character.MaxMP;
                this.client.Character.SP += (uint)((ulong)this.client.Character.MaxSP * (ulong)(100 + ((int)this.client.Character.Int + (int)this.client.Character.Vit + (int)this.client.Character.Status.int_item + (int)this.client.Character.Status.int_mario + (int)this.client.Character.Status.int_rev + (int)this.client.Character.Status.vit_rev + (int)this.client.Character.Status.vit_mario + (int)this.client.Character.Status.vit_item) / 6) / 1000UL);
                if (this.client.Character.SP > this.client.Character.MaxSP)
                    this.client.Character.SP = this.client.Character.MaxSP;
                this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.client.Character, true);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.client.Character.Tasks.Remove(nameof(PossessionRecover));
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
