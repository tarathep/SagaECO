namespace SagaDB.Actor
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the <see cref="VariableHolder{K, T}" />.
    /// </summary>
    /// <typeparam name="K">.</typeparam>
    /// <typeparam name="T">.</typeparam>
    public class VariableHolder<K, T> : IDictionary<K, T>, ICollection<KeyValuePair<K, T>>, IEnumerable<KeyValuePair<K, T>>, IEnumerable
    {
        /// <summary>
        /// Defines the content.
        /// </summary>
        public Dictionary<K, T> content = new Dictionary<K, T>();

        /// <summary>
        /// Defines the nullValue.
        /// </summary>
        private T nullValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="VariableHolder{K, T}"/> class.
        /// </summary>
        /// <param name="nullValue">The nullValue<see cref="T"/>.</param>
        public VariableHolder(T nullValue)
        {
            this.nullValue = nullValue;
        }

        /// <summary>
        /// The Add.
        /// </summary>
        /// <param name="key">The key<see cref="K"/>.</param>
        /// <param name="value">The value<see cref="T"/>.</param>
        public void Add(K key, T value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The ContainsKey.
        /// </summary>
        /// <param name="key">The key<see cref="K"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ContainsKey(K key)
        {
            return this.content.ContainsKey(key);
        }

        /// <summary>
        /// Gets the Keys.
        /// </summary>
        public ICollection<K> Keys
        {
            get
            {
                return (ICollection<K>)this.content.Keys;
            }
        }

        /// <summary>
        /// The Remove.
        /// </summary>
        /// <param name="key">The key<see cref="K"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Remove(K key)
        {
            if (this.content.ContainsKey(key))
                return this.content.Remove(key);
            return false;
        }

        /// <summary>
        /// The TryGetValue.
        /// </summary>
        /// <param name="key">The key<see cref="K"/>.</param>
        /// <param name="value">The value<see cref="T"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool TryGetValue(K key, out T value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the Values.
        /// </summary>
        public ICollection<T> Values
        {
            get
            {
                return (ICollection<T>)this.content.Values;
            }
        }


        public T this[K key]
        {
            get
            {
                if (this.content.ContainsKey(key))
                    return this.content[key];
                return this.nullValue;
            }
            set
            {
                if (this.content.ContainsKey(key))
                    this.content[key] = value;
                else
                    this.content.Add(key, value);
            }
        }
        /// <summary>
        /// The Add.
        /// </summary>
        /// <param name="item">The item<see cref="KeyValuePair{K, T}"/>.</param>
        public void Add(KeyValuePair<K, T> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Clear.
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Contains.
        /// </summary>
        /// <param name="item">The item<see cref="KeyValuePair{K, T}"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Contains(KeyValuePair<K, T> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The CopyTo.
        /// </summary>
        /// <param name="array">The array<see cref="KeyValuePair{K, T}[]"/>.</param>
        /// <param name="arrayIndex">The arrayIndex<see cref="int"/>.</param>
        public void CopyTo(KeyValuePair<K, T>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the Count.
        /// </summary>
        public int Count
        {
            get
            {
                return this.content.Count;
            }
        }

        /// <summary>
        /// Gets a value indicating whether IsReadOnly.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// The Remove.
        /// </summary>
        /// <param name="item">The item<see cref="KeyValuePair{K, T}"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool Remove(KeyValuePair<K, T> item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetEnumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator{KeyValuePair{K, T}}"/>.</returns>
        public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
        {
            return (IEnumerator<KeyValuePair<K, T>>)this.content.GetEnumerator();
        }

        /// <summary>
        /// The GetEnumerator.
        /// </summary>
        /// <returns>The <see cref="IEnumerator"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)this.content.GetEnumerator();
        }
    }
}
