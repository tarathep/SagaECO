namespace SagaMap.Tasks.PC
{
    using global::System;
    using SagaDB.Actor;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="CityDown" />.
    /// </summary>
    public class CityDown : MultiRunTask
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private MapClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="CityDown"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        public CityDown(MapClient client)
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
                if (this.client.Character.HP > 5U)
                    this.client.Character.HP -= 5U;
                else
                    this.client.Character.HP = 1U;
                this.client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, (MapEventArgs)null, (SagaDB.Actor.Actor)this.client.Character, true);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.client.Character.Tasks.Remove(nameof(CityDown));
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
