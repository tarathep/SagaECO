namespace SagaMap.Packets.Server
{
    using SagaDB.Iris;
    using SagaLib;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="SSMG_IRIS_CARD_ITEM_ABILITY" />.
    /// </summary>
    public class SSMG_IRIS_CARD_ITEM_ABILITY : Packet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SSMG_IRIS_CARD_ITEM_ABILITY"/> class.
        /// </summary>
        public SSMG_IRIS_CARD_ITEM_ABILITY()
        {
            this.data = new byte[11];
            this.offset = (ushort)2;
            this.ID = (ushort)7621;
        }

        /// <summary>
        /// Sets the Type.
        /// </summary>
        public SSMG_IRIS_CARD_ITEM_ABILITY.Types Type
        {
            set
            {
                this.PutByte((byte)value, (ushort)2);
            }
        }

        /// <summary>
        /// Sets the AbilityVectors.
        /// </summary>
        public List<AbilityVector> AbilityVectors
        {
            set
            {
                byte[] numArray = new byte[this.data.Length + 4 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count, (ushort)3);
                foreach (AbilityVector abilityVector in value)
                    this.PutUInt(abilityVector.ID);
            }
        }

        /// <summary>
        /// Sets the VectorValues.
        /// </summary>
        public List<int> VectorValues
        {
            set
            {
                byte[] numArray = new byte[this.data.Length + 2 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count);
                foreach (short s in value)
                    this.PutShort(s);
            }
        }

        /// <summary>
        /// Sets the VectorLevels.
        /// </summary>
        public List<int> VectorLevels
        {
            set
            {
                byte[] numArray = new byte[this.data.Length + value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count);
                foreach (byte b in value)
                    this.PutByte(b);
            }
        }

        /// <summary>
        /// Sets the ReleaseAbilities.
        /// </summary>
        public List<ReleaseAbility> ReleaseAbilities
        {
            set
            {
                byte[] numArray = new byte[this.data.Length + 4 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count);
                foreach (int s in value)
                    this.PutInt(s);
            }
        }

        /// <summary>
        /// Sets the AbilityValues.
        /// </summary>
        public List<int> AbilityValues
        {
            set
            {
                byte[] numArray = new byte[this.data.Length + 4 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count);
                foreach (int s in value)
                    this.PutInt(s);
            }
        }

        /// <summary>
        /// Sets the ElementsAttack.
        /// </summary>
        public Dictionary<Elements, int> ElementsAttack
        {
            set
            {
                byte[] numArray = new byte[this.data.Length + 2 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count);
                for (int index = 0; index < value.Count; ++index)
                    this.PutShort((short)value[(Elements)index]);
            }
        }

        /// <summary>
        /// Sets the ElementsDefence.
        /// </summary>
        public Dictionary<Elements, int> ElementsDefence
        {
            set
            {
                byte[] numArray = new byte[this.data.Length + 2 * value.Count];
                this.data.CopyTo((Array)numArray, 0);
                this.data = numArray;
                this.PutByte((byte)value.Count);
                for (int index = 0; index < value.Count; ++index)
                    this.PutShort((short)value[(Elements)index]);
            }
        }

        /// <summary>
        /// Defines the Types.
        /// </summary>
        public enum Types
        {
            /// <summary>
            /// Defines the Deck.
            /// </summary>
            Deck,

            /// <summary>
            /// Defines the Total.
            /// </summary>
            Total,

            /// <summary>
            /// Defines the Max.
            /// </summary>
            Max,
        }
    }
}
