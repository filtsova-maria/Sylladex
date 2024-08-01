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

        public void Draw(Color? tint, bool flipX = false)
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
                0.5f
             );
        }
    }
    // TODO: animation manager, similar to SoundEffectManager manager
    // TODO: window manager, background, UI rendering
}
