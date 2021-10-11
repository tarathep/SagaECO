namespace SagaMap.Tasks.PC
{
    using global::System;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="Marionette" />.
    /// </summary>
    public class Marionette : MultiRunTask
    {
        /// <summary>
        /// Defines the client.
        /// </summary>
        private MapClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Marionette"/> class.
        /// </summary>
        /// <param name="client">The client<see cref="MapClient"/>.</param>
        /// <param name="duration">The duration<see cref="int"/>.</param>
        public Marionette(MapClient client, int duration)
        {
            this.dueTime = duration * 1000;
            this.period = duration * 1000;
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
                this.client.MarionetteDeactivate();
                if (this.client.Character.Tasks.ContainsKey(nameof(Marionette)))
                    this.client.Character.Tasks.Remove(nameof(Marionette));
                this.Deactivate();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                this.Deactivate();
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
