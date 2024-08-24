using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Sylladex.Managers
{
    public static class GameManager
    {
        public static float DeltaTime { get; set; }
        public static TextureManager TextureManager { get; set; } = null!;
        public static FontManager FontManager { get; set; } = null!;
        public static AnimationManager AnimationManager { get; set; } = null!;
        public static SoundEffectManager SoundEffectManager { get; set; } = null!;
        public static SoundtrackManager SoundtrackManager { get; set; } = null!;
        public static EntityManager EntityManager { get; set; } = null!;
        public static CanvasManager CanvasManager { get; set; } = null!;
        public static InputManager InputManager { get; set; } = null!;
        public static CollisionManager CollisionManager { get; set; } = null!;
        public static SylladexManager SylladexManager { get; set; } = null!;
        
        public static   SpriteBatch SpriteBatch { get; set; } = null!;
        public static GraphicsDeviceManager Graphics { get; set; } = null!;

        /// <summary>
        /// Updates TotalSeconds property based on the elapsed game time.
        /// </summary>
        /// <param name="gt">The GameTime object containing the elapsed game time.</param>
        public static void Update(GameTime gt)
        {
            DeltaTime = (float)gt.ElapsedGameTime.TotalSeconds;
        }
    }
}
