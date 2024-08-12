using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sylladex.Managers
{
    public static class GameManager
    {
        public static float TotalSeconds { get; set; }
        public static TextureManager? TextureManager { get; set; }
        public static FontManager? FontManager { get; set; }
        public static AnimationManager? AnimationManager { get; set; }
        public static SoundEffectManager? SoundEffectManager { get; set; }
        public static SoundtrackManager? SoundtrackManager { get; set; }
        public static EntityManager? EntityManager { get; set; }
        public static CanvasManager? CanvasManager { get; set; }
        public static InputManager? InputManager { get; set; }
        public static CollisionManager? CollisionManager { get; set; }

        public static SpriteBatch? SpriteBatch { get; set; }
        public static GraphicsDeviceManager? Graphics { get; set; }

        /// <summary>
        /// Updates TotalSeconds property based on the elapsed game time.
        /// </summary>
        /// <param name="gt">The GameTime object containing the elapsed game time.</param>
        public static void Update(GameTime gt)
        {
            TotalSeconds = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
