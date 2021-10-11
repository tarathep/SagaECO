namespace SagaDB.Iris
{
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="IrisCard" />.
    /// </summary>
    [Serializable]
    public class IrisCard
    {
        /// <summary>
        /// Defines the slots.
        /// </summary>
        [NonSerialized]
        private BitMask<CardSlot> slots = new BitMask<CardSlot>();

        /// <summary>
        /// Defines the elements.
        /// </summary>
        [NonSerialized]
        private Dictionary<SagaLib.Elements, int> elements = new Dictionary<SagaLib.Elements, int>();

        /// <summary>
        /// Defines the abilities.
        /// </summary>
        [NonSerialized]
        private Dictionary<AbilityVector, int> abilities = new Dictionary<AbilityVector, int>();

        /// <summary>
        /// Defines the id.
        /// </summary>
        private uint id;

        /// <summary>
        /// Defines the name.
        /// </summary>
        private string name;

        /// <summary>
        /// Defines the serial.
        /// </summary>
        private string serial;

        /// <summary>
        /// Defines the rank.
        /// </summary>
        private int rank;

        /// <summary>
        /// Defines the nextCard.
        /// </summary>
        private uint nextCard;

        /// <summary>
        /// Defines the rarity.
        /// </summary>
        private Rarity rarity;

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
        /// Gets or sets the Serial.
        /// </summary>
        public string Serial
        {
            get
            {
                return this.serial;
            }
            set
            {
                this.serial = value;
            }
        }

        /// <summary>
        /// Gets or sets the Rank.
        /// </summary>
        public int Rank
        {
            get
            {
                return this.rank;
            }
            set
            {
                this.rank = value;
            }
        }

        /// <summary>
        /// Gets or sets the NextCard.
        /// </summary>
        public uint NextCard
        {
            get
            {
                return this.nextCard;
            }
            set
            {
                this.nextCard = value;
            }
        }

        /// <summary>
        /// Gets or sets the Rarity.
        /// </summary>
        public Rarity Rarity
        {
            get
            {
                return this.rarity;
            }
            set
            {
                this.rarity = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanNeck.
        /// </summary>
        public bool CanNeck
        {
            get
            {
                return this.slots.Test(CardSlot.胸);
            }
            set
            {
                this.slots.SetValue(CardSlot.胸, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanWeapon.
        /// </summary>
        public bool CanWeapon
        {
            get
            {
                return this.slots.Test(CardSlot.武器);
            }
            set
            {
                this.slots.SetValue(CardSlot.武器, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanArmor.
        /// </summary>
        public bool CanArmor
        {
            get
            {
                return this.slots.Test(CardSlot.服);
            }
            set
            {
                this.slots.SetValue(CardSlot.服, value);
            }
        }

        /// <summary>
        /// Gets the Elements.
        /// </summary>
        public Dictionary<SagaLib.Elements, int> Elements
        {
            get
            {
                return this.elements;
            }
        }

        /// <summary>
        /// Gets the Abilities.
        /// </summary>
        public Dictionary<AbilityVector, int> Abilities
        {
            get
            {
                return this.abilities;
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
