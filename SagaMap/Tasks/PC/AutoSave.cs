namespace SagaMap.Tasks.PC
{
    using global::System;
    using SagaDB.Actor;
    using SagaLib;

    /// <summary>
    /// Defines the <see cref="AutoSave" />.
    /// </summary>
    public class AutoSave : MultiRunTask
    {
        /// <summary>
        /// Defines the pc.
        /// </summary>
        private ActorPC pc;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoSave"/> class.
        /// </summary>
        /// <param name="pc">The pc<see cref="ActorPC"/>.</param>
        public AutoSave(ActorPC pc)
        {
            this.period = 1800000;
            this.pc = pc;
        }

        /// <summary>
        /// The CallBack.
        /// </summary>
        /// <param name="o">The o<see cref="object"/>.</param>
        public override void CallBack(object o)
        {
            if (this.pc.Status == null)
            {
                this.Deactivate();
            }
            else
            {
                ClientManager.EnterCriticalArea();
                try
                {
                    Logger.ShowInfo("Autosaving " + this.pc.Name + "'s data...");
                    MapServer.charDB.SaveChar(this.pc, false);
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
