namespace SagaDB.Skill
{
    /// <summary>
    /// Defines the <see cref="Skill" />.
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Defines the baseData.
        /// </summary>
        private SkillData baseData;

        /// <summary>
        /// Defines the lv.
        /// </summary>
        private byte lv;

        /// <summary>
        /// Defines the nosave.
        /// </summary>
        private bool nosave;

        /// <summary>
        /// Gets or sets the BaseData.
        /// </summary>
        public SkillData BaseData
        {
            get
            {
                return this.baseData;
            }
            set
            {
                this.baseData = value;
            }
        }

        /// <summary>
        /// Gets the ID.
        /// </summary>
        public uint ID
        {
            get
            {
                return this.baseData.id;
            }
        }

        /// <summary>
        /// Gets the Name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.baseData.name;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether NoSave.
        /// </summary>
        public bool NoSave
        {
            get
            {
                return this.nosave;
            }
            set
            {
                this.nosave = value;
            }
        }

        /// <summary>
        /// Gets the MaxLevel.
        /// </summary>
        public byte MaxLevel
        {
            get
            {
                return this.baseData.maxLv;
            }
        }

        /// <summary>
        /// Gets or sets the Level.
        /// </summary>
        public byte Level
        {
            get
            {
                return this.lv;
            }
            set
            {
                this.lv = value;
            }
        }

        /// <summary>
        /// Gets the MP.
        /// </summary>
        public ushort MP
        {
            get
            {
                return this.baseData.mp;
            }
        }

        /// <summary>
        /// Gets the SP.
        /// </summary>
        public ushort SP
        {
            get
            {
                return this.baseData.sp;
            }
        }

        /// <summary>
        /// Gets the Range.
        /// </summary>
        public byte Range
        {
            get
            {
                return this.baseData.range;
            }
        }

        /// <summary>
        /// Gets the Effect.
        /// </summary>
        public uint Effect
        {
            get
            {
                return this.baseData.effect;
            }
        }

        /// <summary>
        /// Gets the EffectRange.
        /// </summary>
        public byte EffectRange
        {
            get
            {
                return this.baseData.effectRange;
            }
        }

        /// <summary>
        /// Gets the CastRange.
        /// </summary>
        public byte CastRange
        {
            get
            {
                return this.baseData.castRange;
            }
        }

        /// <summary>
        /// Gets the Target.
        /// </summary>
        public byte Target
        {
            get
            {
                return this.baseData.target;
            }
        }

        /// <summary>
        /// Gets the Target2.
        /// </summary>
        public byte Target2
        {
            get
            {
                return this.baseData.target2;
            }
        }

        /// <summary>
        /// Gets the CastTime.
        /// </summary>
        public int CastTime
        {
            get
            {
                return this.baseData.castTime;
            }
        }

        /// <summary>
        /// Gets the Delay.
        /// </summary>
        public int Delay
        {
            get
            {
                return this.baseData.delay;
            }
        }

        /// <summary>
        /// Gets a value indicating whether Magical.
        /// </summary>
        public bool Magical
        {
            get
            {
                return this.baseData.flag.Test(SkillFlags.MAGIC);
            }
        }

        /// <summary>
        /// Gets a value indicating whether Physical.
        /// </summary>
        public bool Physical
        {
            get
            {
                return this.baseData.flag.Test(SkillFlags.PHYSIC);
            }
        }

        /// <summary>
        /// Gets a value indicating whether PartyOnly.
        /// </summary>
        public bool PartyOnly
        {
            get
            {
                return this.baseData.flag.Test(SkillFlags.PARTY_ONLY);
            }
        }

        /// <summary>
        /// Gets a value indicating whether Attack.
        /// </summary>
        public bool Attack
        {
            get
            {
                return this.baseData.flag.Test(SkillFlags.ATTACK);
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanHasTarget.
        /// </summary>
        public bool CanHasTarget
        {
            get
            {
                return this.baseData.flag.Test(SkillFlags.CAN_HAS_TARGET);
            }
        }

        /// <summary>
        /// Gets a value indicating whether Support.
        /// </summary>
        public bool Support
        {
            get
            {
                return this.baseData.flag.Test(SkillFlags.SUPPORT);
            }
        }

        /// <summary>
        /// Gets a value indicating whether DeadOnly.
        /// </summary>
        public bool DeadOnly
        {
            get
            {
                return this.baseData.flag.Test(SkillFlags.DEAD_ONLY);
            }
        }

        /// <summary>
        /// Gets a value indicating whether NoPossession.
        /// </summary>
        public bool NoPossession
        {
            get
            {
                return this.baseData.flag.Test(SkillFlags.NO_POSSESSION);
            }
        }

        /// <summary>
        /// Gets a value indicating whether NotBeenPossessed.
        /// </summary>
        public bool NotBeenPossessed
        {
            get
            {
                return this.baseData.flag.Test(SkillFlags.NOT_BEEN_POSSESSED);
            }
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.baseData.name;
        }
    }
}
