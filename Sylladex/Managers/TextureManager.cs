using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sylladex.Managers
{
    /// <summary>
    /// The TextureManager class stores the texture assets and provides utilities for working with textures.
    /// </summary>
    public class TextureManager : ObjectManager<Texture2D>
    {
        // We cast position vectors to int to avoid blurry textures.
        public static Vector2 GetTextureCenter(Texture2D texture, Vector2 drawPosition = default) => new Vector2((int)(drawPosition.X + texture.Width / 2), (int)(drawPosition.Y + texture.Height / 2));
    }
}
