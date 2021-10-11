namespace SagaDB.Iris
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AbilityVector" />.
    /// </summary>
    public class AbilityVector
    {
        /// <summary>
        /// Defines the abilities.
        /// </summary>
        private Dictionary<byte, Dictionary<ReleaseAbility, int>> abilities = new Dictionary<byte, Dictionary<ReleaseAbility, int>>();

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Gets the Abilities.
        /// </summary>
        public Dictionary<byte, Dictionary<ReleaseAbility, int>> Abilities
        {
            get
            {
                return this.abilities;
            }
        }

        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        public uint ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
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
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.name;
        }
    }
}
