using Microsoft.Xna.Framework;
using Sylladex.Graphics;

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

        /// <summary>
        /// Gets the sprite associated with the entity.
        /// </summary>
        protected Sprite Sprite { get; init; }

        /// <summary>
        /// Gets or sets the direction of the entity. Sprites are assumed to be facing right unless flipped.
        /// </summary>
        public Direction Direction { get; set; } = Direction.Right;

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
        public abstract void Draw();
    }
}
