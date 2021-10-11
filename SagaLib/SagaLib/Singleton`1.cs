namespace SagaLib
{
    using System;

    /// <summary>
    /// Defines the <see cref="Singleton{T}" />.
    /// </summary>
    /// <typeparam name="T">The type to have the singleton instance of.</typeparam>
    [Serializable]
    public abstract class Singleton<T> where T : new()
    {
        /// <summary>
        /// Gets or sets the Instance.
        /// </summary>
        public static T Instance
        {
            get
            {
                return Singleton<T>.SingletonHolder.instance;
            }
            set
            {
                Singleton<T>.SingletonHolder.instance = value;
            }
        }

        /// <summary>
        /// Sealed class to avoid any heritage from this helper class.
        /// </summary>
        private sealed class SingletonHolder
        {
            /// <summary>
            /// Defines the instance.
            /// </summary>
            internal static T instance = new T();
        }
    }
}
