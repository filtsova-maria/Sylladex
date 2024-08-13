using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Graphics;
using Sylladex.Managers;

namespace Sylladex.Entities
{
    /// <summary>
    /// Represents an abstract base class for entities in the Sylladex system (e.g. <see cref="Player"/> or items in the game world).
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Gets or sets the draw position of the entity.
        /// </summary>
        public Vector2 DrawPosition { get; set; }
        public Vector2 Position => Sprite is not null ? TextureManager.GetTextureCenter(Sprite.Texture, DrawPosition) : DrawPosition;
        public float BottomPosition => DrawPosition.Y + (Sprite?.Texture.Height ?? 0); // Bottom of the entity sprite, used to determine ground contact
        /// <summary>
        /// Gets the sprite associated with the entity.
        /// </summary>
        protected Sprite? Sprite { get; init; }

        /// <summary>
        /// Gets or sets the direction of the entity. Sprites are assumed to be facing right unless flipped.
        /// </summary>
        public HorizontalDirection Direction { get; set; } = HorizontalDirection.Right;

        /// <summary>
        /// Gets or sets the opacity of the entity.
        /// </summary>
        protected float Opacity { get; set; } = 1.0f;

        /// <summary>
        /// Gets or sets the layer index of the entity. Entities with a lower index get rendered below those with a higher index.
        /// </summary>
        public LayerIndex LayerIndex { get; init; } = new LayerIndex();

        /// <summary>
        /// Updates the entity.
        /// </summary>
        public abstract void Update();

        /// <summary>
        /// Draws the entity.
        /// </summary>
        public virtual void Draw()
        {
            if (Sprite is null)
            {
                throw new System.Exception($"{GetType().Name}: Entity sprite was not set.");
            }
            GameManager.SpriteBatch!.Draw(Sprite.Texture, DrawPosition, null, Sprite.Tint, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, LayerIndex.Depth);
        }

        public override string ToString()
        {
            return $"{GetType().Name}: DrawPosition={DrawPosition}, Sprite={Sprite}, Direction={Direction}, Opacity={Opacity}, LayerIndex={LayerIndex}";
        }
    }
}
