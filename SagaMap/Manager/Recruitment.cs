namespace SagaMap.Manager
{
    using SagaDB.Actor;

    /// <summary>
    /// Defines the <see cref="Recruitment" />.
    /// </summary>
    public class Recruitment
    {
        /// <summary>
        /// Defines the creator.
        /// </summary>
        private ActorPC creator;

        /// <summary>
        /// Defines the title.
        /// </summary>
        private string title;

        /// <summary>
        /// Defines the content.
        /// </summary>
        private string content;

        /// <summary>
        /// Defines the type.
        /// </summary>
        private RecruitmentType type;

        /// <summary>
        /// Gets or sets the Creator.
        /// </summary>
        public ActorPC Creator
        {
            get
            {
                return this.creator;
            }
            set
            {
                this.creator = value;
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

        /// <summary>
        /// Gets or sets the Type.
        /// </summary>
        public RecruitmentType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
    }
}
