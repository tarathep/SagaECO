namespace SagaDB.Theater
{
    using System;

    /// <summary>
    /// Defines the <see cref="Movie" />.
    /// </summary>
    public class Movie
    {
        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the mapID.
        /// </summary>
        private uint mapID;

        /// <summary>
        /// Defines the ticket.
        /// </summary>
        private uint ticket;

        /// <summary>
        /// Defines the url.
        /// </summary>
        private string url;

        /// <summary>
        /// Defines the startTime.
        /// </summary>
        private DateTime startTime;

        /// <summary>
        /// Defines the duration.
        /// </summary>
        private int duration;

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the MapID.
        /// </summary>
        public uint MapID
        {
            get
            {
                return this.mapID;
            }
            set
            {
                this.mapID = value;
            }
        }

        /// <summary>
        /// Gets or sets the Ticket.
        /// </summary>
        public uint Ticket
        {
            get
            {
                return this.ticket;
            }
            set
            {
                this.ticket = value;
            }
        }

        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        public string URL
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        /// <summary>
        /// Gets or sets the StartTime.
        /// </summary>
        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the Duration.
        /// </summary>
        public int Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }
    }
}
