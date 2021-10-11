namespace SagaDB.Actor
{
    /// <summary>
    /// Defines the <see cref="ActorEvent" />.
    /// </summary>
    public class ActorEvent : SagaDB.Actor.Actor
    {
        /// <summary>
        /// Defines the caster.
        /// </summary>
        private ActorPC caster;

        /// <summary>
        /// Defines the eventID.
        /// </summary>
        private uint eventID;

        /// <summary>
        /// Defines the title.
        /// </summary>
        private string title;

        /// <summary>
        /// Defines the atype.
        /// </summary>
        private ActorEventTypes atype;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActorEvent"/> class.
        /// </summary>
        /// <param name="caster">The caster<see cref="ActorPC"/>.</param>
        public ActorEvent(ActorPC caster)
        {
            this.type = ActorType.EVENT;
            this.caster = caster;
        }

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        public ActorEventTypes Type
        {
            get
            {
                return this.atype;
            }
            set
            {
                this.atype = value;
            }
        }

        /// <summary>
        /// Gets or sets the Title.
        /// </summary>
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                if (this.e == null)
                    return;
                this.e.PropertyUpdate(UpdateEvent.EVENT_TITLE, 0);
            }
        }

        /// <summary>
        /// Gets or sets the EventID.
        /// </summary>
        public uint EventID
        {
            get
            {
                return this.eventID;
            }
            set
            {
                this.eventID = value;
            }
        }

        /// <summary>
        /// Gets the Caster.
        /// </summary>
        public ActorPC Caster
        {
            get
            {
                return this.caster;
            }
        }
    }
}
