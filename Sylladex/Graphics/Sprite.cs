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
        public readonly Texture2D Texture;
        /// <summary>
        /// Logical owner of the sprite, e.g. <see cref="Player"/>.
        /// </summary>
        private readonly Entity _owner;
        public Color Tint = Color.White;

        /// <summary>
        /// Initializes a new instance of the <see cref="Sprite"/> class.
        /// </summary>
        /// <param name="owner">The entity that owns the sprite, e.g. <see cref="Player""/>.</param>
        /// <param name="texture">The texture of the sprite.</param>
        public Sprite(Entity owner, Texture2D texture)
        {
            Texture = texture;
            _owner = owner;
        }

        /// <summary>
        /// Gets the width of the sprite.
        /// </summary>
        /// <returns>The width of the sprite.</returns>
        public int GetWidth() => Texture.Width;

        /// <summary>
        /// Gets the height of the sprite.
        /// </summary>
        /// <returns>The height of the sprite.</returns>
        public int GetHeight() => Texture.Height;

        /// <summary>
        /// Draws the sprite on the screen.
        /// </summary>
        /// <param name="tint">The color tint to apply to the sprite.</param>
        public void Draw()
        {
            GameManager.SpriteBatch.Draw(
                Texture,
                _owner.DrawPosition,
                null,
                Tint,
                0f,
                Vector2.Zero,
                Vector2.One,
                // All sprites are assumed to face right by default so they are flipped to be consistent with the direction.
                _owner.Direction == HorizontalDirection.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                _owner.LayerIndex.Depth
             );
        }
    }
}
