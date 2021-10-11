namespace SagaMap.Tasks.PC
{
    using global::System;
    using SagaLib;
    using SagaMap.Network.Client;

    /// <summary>
    /// Defines the <see cref="Ping" />.
    /// </summary>
    public class Ping : MultiRunTask
    {
        /// <summary>
        /// Defines the pc.
        /// </summary>
        private MapClient pc;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ping"/> class.
        /// </summary>
        /// <param name="pc">The pc<see cref="MapClient"/>.</param>
        public Ping(MapClient pc)
        {
            this.period = 10000;
            this.pc = pc;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            if ((this.pc.ping - DateTime.Now).TotalSeconds <= 60.0)
                return;
            ClientManager.EnterCriticalArea();
            try
            {
                this.pc.netIO.Disconnect();
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ClientManager.LeaveCriticalArea();
        }
    }
}
