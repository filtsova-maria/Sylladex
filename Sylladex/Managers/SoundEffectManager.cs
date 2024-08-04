using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace Sylladex.Managers
{
    /// <summary>
    /// Manages the playback of sound effects in the Sylladex system.
    /// </summary>
    public class SoundEffectManager : ObjectManager<SoundEffect>
    {
        private readonly Dictionary<string, SoundEffectInstance> _instances = new Dictionary<string, SoundEffectInstance>();

        /// <summary>
        /// Plays the sound effect associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the sound effect to play.</param>
        public void Play(string key)
        {
            if (!_instances.ContainsKey(key))
            {
                _instances[key] = GetObject(key).CreateInstance();
            }
            if (_instances[key].State == SoundState.Stopped) // Prevents sound effects from playing over each other
            {
                _instances[key].Play();
            }
        }
    }
}
