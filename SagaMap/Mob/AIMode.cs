namespace SagaMap.Mob
{
    using SagaLib;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="AIMode" />.
    /// </summary>
    public class AIMode
    {
        /// <summary>
        /// Defines the eventAttacking.
        /// </summary>
        private Dictionary<uint, int> eventAttacking = new Dictionary<uint, int>();

        /// <summary>
        /// Defines the eventMasterCombat.
        /// </summary>
        private Dictionary<uint, int> eventMasterCombat = new Dictionary<uint, int>();

        /// <summary>
        /// Defines the mask.
        /// </summary>
        public BitMask mask;

        /// <summary>
        /// Defines the mobID.
        /// </summary>
        private uint mobID;

        /// <summary>
        /// Defines the eventAttackingSkillRate.
        /// </summary>
        private int eventAttackingSkillRate;

        /// <summary>
        /// Defines the eventMasterCombatRate.
        /// </summary>
        private int eventMasterCombatRate;

        /// <summary>
        /// Initializes a new instance of the <see cref="AIMode"/> class.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        public AIMode(int value)
        {
            this.mask = new BitMask(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AIMode"/> class.
        /// </summary>
        public AIMode()
        {
            this.mask = new BitMask(0);
        }

        /// <summary>
        /// Gets or sets the MobID.
        /// </summary>
        public uint MobID
        {
            get
            {
                return this.mobID;
            }
            set
            {
                this.mobID = value;
            }
        }

        /// <summary>
        /// Gets or sets the AI.
        /// </summary>
        public int AI
        {
            get
            {
                return this.mask.Value;
            }
            set
            {
                this.mask.Value = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether Active.
        /// </summary>
        public bool Active
        {
            get
            {
                return this.mask.Test((object)AIFlag.Active);
            }
        }

        /// <summary>
        /// Gets a value indicating whether NoAttack.
        /// </summary>
        public bool NoAttack
        {
            get
            {
                return this.mask.Test((object)AIFlag.NoAttack);
            }
        }

        /// <summary>
        /// Gets a value indicating whether NoMove.
        /// </summary>
        public bool NoMove
        {
            get
            {
                return this.mask.Test((object)AIFlag.NoMove);
            }
        }

        /// <summary>
        /// Gets a value indicating whether RunAway.
        /// </summary>
        public bool RunAway
        {
            get
            {
                return this.mask.Test((object)AIFlag.RunAway);
            }
        }

        /// <summary>
        /// Gets a value indicating whether HelpSameType.
        /// </summary>
        public bool HelpSameType
        {
            get
            {
                return this.mask.Test((object)AIFlag.HelpSameType);
            }
        }

        /// <summary>
        /// Gets a value indicating whether HateHeal.
        /// </summary>
        public bool HateHeal
        {
            get
            {
                return this.mask.Test((object)AIFlag.HateHeal);
            }
        }

        /// <summary>
        /// Gets a value indicating whether HateMagic.
        /// </summary>
        public bool HateMagic
        {
            get
            {
                return this.mask.Test((object)AIFlag.HateMagic);
            }
        }

        /// <summary>
        /// Gets a value indicating whether Symbol.
        /// </summary>
        public bool Symbol
        {
            get
            {
                return this.mask.Test((object)AIFlag.Symbol);
            }
        }

        /// <summary>
        /// Gets a value indicating whether SymbolTrash.
        /// </summary>
        public bool SymbolTrash
        {
            get
            {
                return this.mask.Test((object)AIFlag.SymbolTrash);
            }
        }

        /// <summary>
        /// Gets or sets the EventAttacking.
        /// </summary>
        public Dictionary<uint, int> EventAttacking
        {
            get
            {
                return this.eventAttacking;
            }
            set
            {
                this.eventAttacking = value;
            }
        }

        /// <summary>
        /// Gets or sets the EventAttackingSkillRate.
        /// </summary>
        public int EventAttackingSkillRate
        {
            get
            {
                return this.eventAttackingSkillRate;
            }
            set
            {
                this.eventAttackingSkillRate = value;
            }
        }

        /// <summary>
        /// Gets or sets the EventMasterCombat.
        /// </summary>
        public Dictionary<uint, int> EventMasterCombat
        {
            get
            {
                return this.eventMasterCombat;
            }
            set
            {
                this.eventMasterCombat = value;
            }
        }

        /// <summary>
        /// Gets or sets the EventMasterCombatSkillRate.
        /// </summary>
        public int EventMasterCombatSkillRate
        {
            get
            {
                return this.eventMasterCombatRate;
            }
            set
            {
                this.eventMasterCombatRate = value;
            }
        }
    }
}
