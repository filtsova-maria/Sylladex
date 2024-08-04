using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Entities;
using Sylladex.Managers;
using System.Diagnostics;

namespace Sylladex.Graphics
{
    /// <summary>
    /// Represents a sprite that can be drawn on the screen.
    /// </summary>
    public class Sprite
    {
        private readonly Texture2D _texture;
        private readonly Entity _owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="owner">The entity that owns the sprite, e.g. <see cref="Player""/>.</param>
        /// <param name="texture">The texture of the sprite.</param>
        public Sprite(Entity owner, Texture2D texture)
        {
            _texture = texture;
            _owner = owner;
        }

        /// <summary>
        /// Gets the width of the sprite.
        /// </summary>
        /// <returns>The width of the sprite.</returns>
        public int GetWidth() => _texture.Width;

        /// <summary>
        /// Gets the height of the sprite.
        /// </summary>
        /// <returns>The height of the sprite.</returns>
        public int GetHeight() => _texture.Height;

        /// <summary>
        /// Draws the sprite on the screen.
        /// </summary>
        /// <param name="tint">The color tint to apply to the sprite.</param>
        public void Draw(Color? tint)
        {
            GameManager.SpriteBatch.Draw(
                _texture,
                _owner.DrawPosition,
                null,
                tint is null ? Color.White : (Color)tint,
                0f,
                Vector2.Zero,
                Vector2.One,
                // All sprites are assumed to face right by default so they are flipped to be consistent with the direction.
                _owner.Direction == Direction.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                _owner.LayerIndex.Depth
             );
        }
    }
}
