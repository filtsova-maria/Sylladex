using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sylladex.Graphics;

namespace Sylladex.UI
{
    /// <summary>
    /// Represents an object that can be rendered on the screen.
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Called in the main Update loop.
        /// </summary>
        void Update();

        /// <summary>
        /// Called in the main Draw loop.
        /// </summary>
        void Draw();

        /// <summary>
        /// Texture of the renderable object.
        /// </summary>
        public Texture2D Texture { get; init; }
        /// <summary>
        /// Position of the renderable object on the screen.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Layer index of the renderable object.
        /// </summary>
        public LayerIndex LayerIndex { get; set; }

        /// <summary>
        /// Opacity of the renderable object, 0=invisible, 1=opaque.
        /// </summary>
        public float? Opacity { get; set; }

        /// <summary>
        /// Gets or sets the texture tint color of the renderable object.
        /// </summary>
        public Color? Tint { get; set; }
    }
}
