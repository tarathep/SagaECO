namespace SagaLib
{
    /// <summary>
    /// Defines the <see cref="BitMask{T}" />.
    /// </summary>
    /// <typeparam name="T">一个枚举类型.</typeparam>
    public class BitMask<T>
    {
        /// <summary>
        /// Defines the ori.
        /// </summary>
        private BitMask ori;

        /// <summary>
        /// Initializes a new instance of the <see cref="BitMask{T}"/> class.
        /// </summary>
        public BitMask()
        {
            this.ori = new BitMask();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitMask{T}"/> class.
        /// </summary>
        /// <param name="ori">The ori<see cref="BitMask"/>.</param>
        public BitMask(BitMask ori)
        {
            this.ori = ori;
        }

        /// <summary>
        /// The Test.
        /// </summary>
        /// <param name="Mask">标识.</param>
        /// <returns>值.</returns>
        public bool Test(T Mask)
        {
            return this.ori.Test((object)Mask);
        }

        /// <summary>
        /// The SetValue.
        /// </summary>
        /// <param name="bitmask">标识.</param>
        /// <param name="val">真值.</param>
        public void SetValue(T bitmask, bool val)
        {
            this.ori.SetValue((object)bitmask, val);
        }

        /// <summary>
        /// Gets or sets the Value.
        /// </summary>
        public int Value
        {
            get
            {
                return this.ori.Value;
            }
            set
            {
                this.ori.Value = value;
            }
        }


        public static implicit operator BitMask<T>(BitMask ori)
        {
            return new BitMask<T>(ori);
        }
    }
}
