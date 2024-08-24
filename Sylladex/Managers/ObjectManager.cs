using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Sylladex.Managers
{
    /// <summary>
    /// Represents an abstract base class for managing assets and objects with a key-value pair structure.
    /// </summary>
    /// <typeparam name="TKey">The type of the key to retrieve the object or an asset.</typeparam>
    /// <typeparam name="TValue">The type of the value to be retrieved.</typeparam>
    public abstract class ObjectManager<TKey, TValue> where TKey : notnull
    {
        protected Dictionary<TKey, TValue> _objects = new Dictionary<TKey, TValue>();

        /// <summary>
        /// Adds an object to the manager with the specified key.
        /// </summary>
        /// <param name="key">The key of the object.</param>
        /// <param name="asset">The object to add.</param>
        public void AddObject(TKey key, TValue asset)
        {
            _objects[key] = asset;
        }

        /// <summary>
        /// Gets the object with the specified key from the manager.
        /// </summary>
        /// <param name="key">The key of the object.</param>
        /// <returns>The object with the specified key.</returns>
        /// <exception cref="KeyNotFoundException">Thrown when the object with the specified key is not found.</exception>
        public TValue GetObject(TKey key)
        {
            if (_objects.ContainsKey(key))
            {
                return _objects[key];
            }
            else
            {
                throw new KeyNotFoundException($"'{typeof(TValue).GetType()}': Object with key '{key}' not found.");
            }
        }
        /// <summary>
        /// Remove an object and clean up its input bindings.
        /// </summary>
        public void RemoveObject(TKey key)
        {
            if (_objects.ContainsKey(key))
            {
                TValue gameObject = GetObject(key);
                _objects.Remove(key);
                GameManager.InputManager.RemoveActionsFromEntity(gameObject);
            }
        }
    }

    /// <summary>
    /// Represents an abstract base class for managing objects with a string key, the most common case.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public abstract class ObjectManager<T> : ObjectManager<string, T> { }
}
