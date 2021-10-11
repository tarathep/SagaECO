namespace SagaLib
{
    using System;

    /// <summary>
    /// Defines the <see cref="BitMask" />.
    /// </summary>
    [Serializable]
    public class BitMask
    {
        /// <summary>
        /// Defines the value.
        /// </summary>
        private int value;

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitMask"/> class.
        /// </summary>
        public BitMask()
        {
            this.value = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitMask"/> class.
        /// </summary>
        /// <param name="value">The value<see cref="int"/>.</param>
        public BitMask(int value)
        {
            this.value = value;
        }

        /// <summary>
        /// The Test.
        /// </summary>
        /// <param name="Mask">标识.</param>
        /// <returns>值.</returns>
        public bool Test(object Mask)
        {
            return this.Test((int)Mask);
        }

        /// <summary>
        /// The Test.
        /// </summary>
        /// <param name="Mask">标识.</param>
        /// <returns>值.</returns>
        public bool Test(int Mask)
        {
            return (this.value & Mask) != 0;
        }

        /// <summary>
        /// The SetValue.
        /// </summary>
        /// <param name="bitmask">标识.</param>
        /// <param name="val">真值.</param>
        public void SetValue(object bitmask, bool val)
        {
            this.SetValue((int)bitmask, val);
        }

        /// <summary>
        /// The SetValue.
        /// </summary>
        /// <param name="bitmask">标识.</param>
        /// <param name="val">真值.</param>
        public void SetValue(int bitmask, bool val)
        {
            if (this.Test(bitmask) == val)
                return;
            if (val)
                this.value |= bitmask;
            else
                this.value ^= bitmask;
        }
    }
}
