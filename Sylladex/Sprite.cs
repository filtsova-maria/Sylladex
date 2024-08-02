using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Sylladex
{
    public class Sprite
    {
        private readonly Texture2D _texture;
        private readonly Entity _owner;

        public Sprite(Entity owner, Texture2D texture)
        {
            _texture = texture;
            _owner = owner;
        }

        public int GetWidth() => _texture.Width;
        public int GetHeight() => _texture.Height;


        public void Draw(Color? tint, float depth=0.5f, bool flipX = false)
        {
            GameManager.SpriteBatch.Draw(
                _texture,
                _owner.DrawPosition,
                null,
                tint is null ? Color.White : (Color)tint,
                0f,
                Vector2.Zero,
                Vector2.One,
                flipX ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                depth
             );
        }
    }
}
