using Microsoft.Xna.Framework.Media;

namespace Sylladex.Managers
{
    /// <summary>
    /// Manages the soundtrack for the game.
    /// </summary>
    public class SoundtrackManager : ObjectManager<Song>
    {
        /// <summary>
        /// Plays the specified soundtrack.
        /// </summary>
        /// <param name="key">The key of the soundtrack to play.</param>
        /// <param name="shouldLoop">Determines whether the soundtrack should loop.</param>
        public void Play(string key, bool shouldLoop = false)
        {
            MediaPlayer.Play(GetObject(key));
            MediaPlayer.IsRepeating = shouldLoop;
        }
    }
}
