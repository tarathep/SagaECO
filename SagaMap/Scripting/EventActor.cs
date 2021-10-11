namespace SagaMap.Scripting
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="EventActor" />.
    /// </summary>
    public abstract class EventActor : Event
    {
        /// <summary>
        /// Defines the actor.
        /// </summary>
        private ActorEvent actor;

        /// <summary>
        /// Gets or sets the Actor.
        /// </summary>
        public ActorEvent Actor
        {
            get
            {
                return this.actor;
            }
            set
            {
                this.actor = value;
            }
        }

        /// <summary>
        /// The Clone.
        /// </summary>
        /// <returns>The <see cref="EventActor"/>.</returns>
        public abstract EventActor Clone();
    }
}
