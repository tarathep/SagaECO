namespace SagaDB.BBS
{
    using System;

    /// <summary>
    /// Defines the <see cref="Post" />.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Defines the date.
        /// </summary>
        private DateTime date;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the title.
        /// </summary>
        private string title;

        /// <summary>
        /// Defines the content.
        /// </summary>
        private string content;

        /// <summary>
        /// Gets or sets the Date.
        /// </summary>
        public DateTime Date
        {
            get
            {
                return this.date;
            }
            set
            {
                this.date = value;
            }
        }

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
            }
        }

        /// <summary>
        /// Gets or sets the Content.
        /// </summary>
        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }
    }
}
